Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports System.Globalization
Imports Common
Imports Common.ClsFunction

Public Class Form_PersonalExport
  ReadOnly tmpDb As New ClsSqlServer
  ReadOnly tmpDt As New DataTable
  ' SQLサーバー操作オブジェクト
  Private _SqlServer As ClsSqlServer
  Private ReadOnly Property SqlServer As ClsSqlServer
    Get
      If _SqlServer Is Nothing Then
        _SqlServer = New ClsSqlServer
      End If
      Return _SqlServer
    End Get
  End Property
  Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click

    Dim fromDate As Date = DateTimePickerFrom.Value.Date
    Dim toDate As Date = DateTimePickerTo.Value.Date
    Dim yearValue As Integer = fromDate.Year   ' 暦年ベースで集計保存

    Dim folderPath As String = "C:\temp"
    If Not Directory.Exists(folderPath) Then Directory.CreateDirectory(folderPath)

    Dim fileName As String = String.Format("個人別集計_{0}-{1}.xlsx",
                                           fromDate.ToString("yyyyMMdd"),
                                           toDate.ToString("yyyyMMdd"))
    Dim exportPath As String = Path.Combine(folderPath, fileName)

    '---------------------------------
    ' データ取得
    '---------------------------------
    Dim dt As New DataTable
    Dim sql As String = GetPersonalResultSql(fromDate, toDate)

    Try
      SqlServer.GetResult(dt, sql)

      If dt.Rows.Count = 0 Then
        MessageBox.Show("対象期間にデータがありません。",
                        "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Sub
      End If

      '---------------------------------
      ' Excel 出力
      '---------------------------------
      Using wb As New XLWorkbook()

        For Each staffGroup In dt.AsEnumerable().GroupBy(Function(r) r("staff_number").ToString())

          Dim staffName As String = staffGroup.First()("Staff_Name").ToString()
          Dim ws = wb.Worksheets.Add(staffName & " 様")

          ' タイトル
          ws.Cell(1, 1).Value = staffName & " 様"
          ws.Cell(2, 1).Value = String.Format("対象期間: {0} ～ {1}",
                                              fromDate.ToString("yyyy/MM/dd"),
                                              toDate.ToString("yyyy/MM/dd"))

          ' 見出し
          ws.Cell(4, 1).Value = "日付"
          ws.Cell(4, 2).Value = "曜日"
          For i As Integer = 1 To 10
            ws.Cell(4, i + 2).Value = i & "回目"
          Next
          ws.Cell(4, 13).Value = "合計"

          ' 日ごとデータ
          Dim row As Integer = 5
          Dim curDate As Date = fromDate
          While curDate <= toDate
            ws.Cell(row, 1).Value = curDate.ToString("MM/dd")
            ws.Cell(row, 2).Value = curDate.ToString("ddd", New CultureInfo("ja-JP"))

            ' TODO: 雨日判定（RainDayテーブル参照を追加予定）
            Dim isRainDay As Boolean = False

            If isRainDay Then
              ws.Cell(row, 3).Value = "雨休み"
            Else
              Dim records = staffGroup.Where(Function(r) CDate(r("addition_date")) = curDate) _
                                      .OrderBy(Function(r) r("addition_time")).ToList()
              Dim sum As Decimal = 0
              For i As Integer = 0 To records.Count - 1
                ws.Cell(row, i + 3).Value = records(i)("weight")
                sum += CDec(records(i)("weight"))
              Next
              ws.Cell(row, 13).Value = sum
            End If

            row += 1
            curDate = curDate.AddDays(1)
          End While

          ws.PageSetup.FitToPages(1, 1)
        Next

        wb.SaveAs(exportPath)
      End Using

      '---------------------------------
      ' WorkerSummary 登録
      '---------------------------------
      Dim insertSql As String = GetInsertSummarySql(yearValue, fromDate, toDate)
      Dim dummyDt As New DataTable
      SqlServer.GetResult(dummyDt, insertSql)
      dummyDt.Dispose()

      MessageBox.Show("個人別帳票を出力しました。" & vbCrLf & exportPath,
                      "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    Catch ex As Exception
      Call ComWriteErrLog(Me.Name,
                          System.Reflection.MethodBase.GetCurrentMethod().Name,
                          ex.Message)
      Throw New Exception(ex.Message)
    Finally
      dt.Dispose()
    End Try

  End Sub

  ' 個人別出力用 SQL
  Private Function GetPersonalResultSql(fromDate As Date, toDate As Date) As String
    Dim sql As String = ""
    sql &= " SELECT "
    sql &= "     r.addition_date,"
    sql &= "     r.addition_time,"
    sql &= "     CAST(r.weight AS DECIMAL(10,2)) AS weight,"
    sql &= "     r.staff_number,"
    sql &= "     s.Staff_Name"
    sql &= " FROM TRN_Results r"
    sql &= " JOIN MST_Staff s ON r.staff_number = s.Staff_Number"
    sql &= " WHERE r.addition_date BETWEEN '" & fromDate.ToString("yyyy-MM-dd") & "' " &
           " AND '" & toDate.ToString("yyyy-MM-dd") & "'"
    sql &= " ORDER BY r.staff_number, r.addition_date, r.addition_time"
    Return sql
  End Function

  ' WorkerSummary 登録用 SQL
  Private Function GetInsertSummarySql(yearValue As Integer, fromDate As Date, toDate As Date) As String
    Dim sql As String = ""

    sql &= "INSERT INTO TRN_WorkerSummary "
    sql &= "(Staff_Number, Staff_Name, Year, From_Date, To_Date, "
    sql &= " Total_Weight, Wage, Transport_Fee, Kaikin_Fee, Total_Amount) "
    sql &= "SELECT "
    sql &= " s.Staff_Number, "
    sql &= " s.Staff_Name, "
    sql &= " " & yearValue & ", "
    sql &= " '" & fromDate.ToString("yyyy-MM-dd") & "', "
    sql &= " '" & toDate.ToString("yyyy-MM-dd") & "', "
    sql &= " SUM(CAST(r.weight AS DECIMAL(10,2))) AS TotalWeight, "
    sql &= " SUM(CAST(r.weight AS DECIMAL(10,2)) * "
    sql &= "     CASE WHEN r.addition_date BETWEEN p.START_DATE AND ISNULL(p.END_DATE, '9999-12-31') "
    sql &= "          THEN p.PERIOD_UNIT_PRICE ELSE p.REGULAR_UNIT_PRICE END "
    sql &= " ) AS Wage, "
    sql &= " CAST(SUM(CAST(r.weight AS DECIMAL(10,2))) AS INT) * 10 AS TransportFee, "
    sql &= " CASE "
    sql &= "     WHEN COUNT(DISTINCT r.addition_date) = "
    sql &= "          (DATEDIFF(DAY, '" & fromDate.ToString("yyyy-MM-dd") & "', '" & toDate.ToString("yyyy-MM-dd") & "') + 1) "
    sql &= "          - (SELECT COUNT(*) FROM RainDay "
    sql &= "             WHERE rain_date BETWEEN '" & fromDate.ToString("yyyy-MM-dd") & "' AND '" & toDate.ToString("yyyy-MM-dd") & "') "
    sql &= "     THEN CAST(SUM(CAST(r.weight AS DECIMAL(10,2))) AS INT) * 10 "
    sql &= "     ELSE 0 "
    sql &= " END AS KaikinFee, "
    sql &= " SUM(CAST(r.weight AS DECIMAL(10,2)) * "
    sql &= "     CASE WHEN r.addition_date BETWEEN p.START_DATE AND ISNULL(p.END_DATE, '9999-12-31') "
    sql &= "          THEN p.PERIOD_UNIT_PRICE ELSE p.REGULAR_UNIT_PRICE END "
    sql &= " ) "
    sql &= " + CAST(SUM(CAST(r.weight AS DECIMAL(10,2))) AS INT) * 10 "
    sql &= " + CASE "
    sql &= "     WHEN COUNT(DISTINCT r.addition_date) = "
    sql &= "          (DATEDIFF(DAY, '" & fromDate.ToString("yyyy-MM-dd") & "', '" & toDate.ToString("yyyy-MM-dd") & "') + 1) "
    sql &= "          - (SELECT COUNT(*) FROM RainDay "
    sql &= "             WHERE rain_date BETWEEN '" & fromDate.ToString("yyyy-MM-dd") & "' AND '" & toDate.ToString("yyyy-MM-dd") & "') "
    sql &= "     THEN CAST(SUM(CAST(r.weight AS DECIMAL(10,2))) AS INT) * 10 "
    sql &= "     ELSE 0 "
    sql &= " END AS TotalAmount "
    sql &= "FROM TRN_Results r "
    sql &= "JOIN MST_Staff s ON r.staff_number = s.Staff_Number "
    sql &= "LEFT JOIN MST_PeriodUnitPrice p "
    sql &= " ON r.addition_date BETWEEN p.START_DATE AND ISNULL(p.END_DATE, '9999-12-31') "
    sql &= "WHERE r.addition_date BETWEEN '" & fromDate.ToString("yyyy-MM-dd") & "' AND '" & toDate.ToString("yyyy-MM-dd") & "' "
    sql &= "GROUP BY s.Staff_Number, s.Staff_Name "
    sql &= "ORDER BY s.Staff_Number;"

    Return sql
  End Function

End Class
