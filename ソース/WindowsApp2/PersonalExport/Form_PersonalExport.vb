Imports ClosedXML.Excel
Imports System.IO
Imports Common
Imports Common.ClsFunction

Public Class Form_PersonalExport

  Private ReadOnly ResultCsvPath As String = ReadSettingIniFile("RESULT_CSV_PATH", "VALUE")
  Private Const SheetProtectionPassword As String = "495344"

  Private _SqlServer As ClsSqlServer
  Private ReadOnly Property SqlServer As ClsSqlServer
    Get
      If _SqlServer Is Nothing Then _SqlServer = New ClsSqlServer
      Return _SqlServer
    End Get
  End Property

  ' 文字/DBNull を安全に Decimal に変換
  Private Function SafeToDecimal(obj As Object) As Decimal
    If obj Is Nothing OrElse obj Is DBNull.Value Then Return 0D
    Dim s As String = obj.ToString().Trim()
    If s = "" Then Return 0D
    Dim v As Decimal
    If Decimal.TryParse(s, v) Then Return v Else Return 0D
  End Function

  ' 出力ボタンクリック
  Private Sub BtnExport_Click(sender As Object, e As EventArgs) Handles BtnExport.Click

    Dim fromDate As Date = DateTimePickerFrom.Value.Date
    Dim toDate As Date = DateTimePickerTo.Value.Date
    Dim yearValue As Integer = fromDate.Year

    Try
      SqlServer.Execute(GetDeleteSummarySql(yearValue, fromDate, toDate))
      SqlServer.Execute(GetInsertSummarySql(yearValue, fromDate, toDate))
    Catch ex As Exception
      MessageBox.Show("WorkerSummary登録エラー:" & ex.Message, "エラー",
                      MessageBoxButtons.OK, MessageBoxIcon.[Error])
      Exit Sub
    End Try

    '--- 明細データ取得（期間内の全レコード）
    Dim dt As New DataTable
    Dim sql As String = ""
    sql &= "SELECT CAST(r.staff_number AS INT) AS staff_number, r.staff_name, r.addition_date, r.addition_time, "
    sql &= "       FLOOR(CAST(r.weight AS DECIMAL(18,3)) * 10) / 10.0 AS weight "
    sql &= "FROM TRN_Results r "
    sql &= "WHERE r.addition_date BETWEEN '" & fromDate & "' AND '" & toDate & "' "
    sql &= "AND r.delete_flg = '0' "
    sql &= "ORDER BY CAST(r.staff_number AS INT), r.addition_date, r.addition_time"

    Try
      SqlServer.GetResult(dt, sql)
      If dt.Rows.Count = 0 Then
        MessageBox.Show("対象期間にデータがありません。", "情報",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Sub
      End If
    Catch ex As Exception
      MessageBox.Show("データ取得エラー:" & ex.Message, "エラー",
                      MessageBoxButtons.OK, MessageBoxIcon.[Error])
      Exit Sub
    End Try

    '--- 雨休み日取得
    Dim rainDays As New HashSet(Of Date)
    Dim rainDt As New DataTable
    Dim rainSql As String = "SELECT rain_date FROM Trn_RainDay " &
                            "WHERE rain_date BETWEEN '" & fromDate & "' AND '" & toDate & "'"
    SqlServer.GetResult(rainDt, rainSql)
    For Each r As DataRow In rainDt.Rows
      rainDays.Add(CDate(r("rain_date")))
    Next

    '--- 期間別単価マスタ取得
    Dim priceDt As New DataTable
    SqlServer.GetResult(priceDt, "SELECT START_DATE, END_DATE, REGULAR_UNIT_PRICE, PERIOD_UNIT_PRICE FROM MST_PeriodUnitPrice")
    Dim pStart As Date = If(priceDt.Rows.Count > 0, CDate(priceDt.Rows(0)("START_DATE")), Date.MinValue)
    Dim pEnd As Date = If(priceDt.Rows.Count > 0, CDate(priceDt.Rows(0)("END_DATE")), Date.MinValue)
    Dim regularPrice As Decimal = If(priceDt.Rows.Count > 0, SafeToDecimal(priceDt.Rows(0)("REGULAR_UNIT_PRICE")), 0D)
    Dim periodPrice As Decimal = If(priceDt.Rows.Count > 0, SafeToDecimal(priceDt.Rows(0)("PERIOD_UNIT_PRICE")), 0D)

    '--- Excel 出力
    Try

      Dim filePath As String = ResultCsvPath & "茶摘日報（個人別）_" & yearValue & ".xlsx"

      Using wb As New XLWorkbook()
        Const ReportFontSize As Integer = 18
        Const NumericFontSize As Integer = 17
        Const HeaderFontSize As Integer = 16
        Const CountHeaderFontSize As Integer = 14
        Const TitleFontSize As Integer = 32
        Const SummaryFontSize As Integer = 18
        Const GrandTotalFontSize As Integer = 24
        Const GrandTotalValueFontSize As Integer = 23
        Const TitleRowHeight As Double = 44
        Const HeaderRowHeight As Double = 26
        Const BodyRowHeight As Double = 24
        Const SummaryRowHeight As Double = 28
        Const GrandTotalRowHeight As Double = 34

        ' 作業者ごとに 1 シート
        For Each g In dt.AsEnumerable().GroupBy(Function(r) r("staff_number").ToString())

          Dim staffNo As String = g.Key
          Dim staffName As String = g.First()("staff_name").ToString()
          Dim ws = wb.Worksheets.Add(staffNo & "_" & staffName & "様")

          ' ==== タイトル（氏名）====
          ws.Cell(1, 1).Value = staffNo & "_" & staffName & " 様"
          ws.Range("A1:L1").Merge()
          With ws.Range("A1:L1").Style
            .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
            .Alignment.Vertical = XLAlignmentVerticalValues.Center
            .Font.FontSize = TitleFontSize
            .Font.Bold = True
          End With
          ws.Row(1).Height = TitleRowHeight

          ' ==== 全体フォント ====
          ws.Style.Font.FontSize = ReportFontSize

          ' ==== ヘッダー ====
          ws.Cell(2, 1).Value = "日付"
          For i As Integer = 1 To 10
            ws.Cell(2, i + 1).Value = i & "回目"
          Next
          ws.Cell(2, 12).Value = "合計"
          ws.Range("A2:L2").Style.Fill.BackgroundColor = XLColor.LightGray
          ws.Range("A2:L2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
          ws.Range("A2:L2").Style.Font.FontSize = HeaderFontSize
          ws.Range("A2:L2").Style.Font.Bold = True
          ws.Range("B2:K2").Style.Font.FontSize = CountHeaderFontSize
          ws.Range("B2:K2").Style.Alignment.ShrinkToFit = True
          ws.Row(2).Height = HeaderRowHeight

          ' ==== 本体（日付は期間すべて表示／期間内を青）====
          Dim rowIndex As Integer = 3
          Dim d As Date = fromDate
          Do While d <= toDate
            ' 日付＋曜日を表示
            ws.Cell(rowIndex, 1).Value = d.ToString("yyyy/MM/dd (ddd)")

            ' 期間内を青塗り
            If d >= pStart AndAlso d <= pEnd Then
              ws.Range(rowIndex, 1, rowIndex, 12).Style.Fill.BackgroundColor = XLColor.LightBlue
            End If

            If rainDays.Contains(d) Then
              ws.Cell(rowIndex, 1).Value = d.ToString("yyyy/MM/dd (ddd) ☂")

              ' 行全体を明るいグレーに塗る（日付列含む）
              ws.Range(rowIndex, 1, rowIndex, 12).Style.Fill.BackgroundColor = XLColor.LightGray
            End If

            Dim dayRecs = g.Where(Function(r) CDate(r("addition_date")) = d) _
                 .OrderBy(Function(r) TimeSpan.Parse(r("addition_time").ToString())) _
                 .ToList()
              Dim col As Integer = 2
              Dim daily As Decimal = 0D
              For Each rec In dayRecs.Take(10)
                Dim w As Decimal = SafeToDecimal(rec("weight"))
                ws.Cell(rowIndex, col).Value = w
                ws.Cell(rowIndex, col).Style.NumberFormat.Format = "#,##0.0"
                daily += w
                col += 1
              Next
              ws.Cell(rowIndex, 12).Value = daily
              ws.Cell(rowIndex, 12).Style.NumberFormat.Format = "#,##0.0"

            rowIndex += 1
            d = d.AddDays(1)
          Loop

          ' ==== 日次合計行 ====
          ws.Cell(rowIndex, 1).Value = "総合計"

          For i As Integer = 1 To 10
            Dim colLetter As String = Chr(Asc("A"c) + i)
            Dim formulaCell = ws.Cell(rowIndex, i + 1)

            formulaCell.FormulaA1 = "=SUM(" & colLetter & "3:" & colLetter & (rowIndex - 1).ToString() & ")"
            formulaCell.Style.NumberFormat.Format = "0.0" ' 小数点1桁まで表示
          Next


          ws.Cell(rowIndex, 12).FormulaA1 = "=SUM(L3:L" & (rowIndex - 1).ToString() & ")"

          ws.Rows(3, rowIndex).Height = BodyRowHeight

          ws.Range(rowIndex, 1, rowIndex, 12).Style.Font.Bold = True
          ws.Range(rowIndex, 1, rowIndex, 12).Style.Fill.BackgroundColor = XLColor.LightGray
          ws.Cell(rowIndex, 12).Style.NumberFormat.Format = "#,##0.0"

          ' ==== WorkerSummary から交通費/皆勤賞/総額 ====
          Dim sumDt As New DataTable
          Dim sumSql As String = ""
          sumSql &= "SELECT Total_Weight, Wage, Transport_Fee, Kaikin_Fee, Total_Amount "
          sumSql &= "FROM TRN_WorkerSummary "
          sumSql &= "WHERE Staff_Number='" & staffNo & "' "
          sumSql &= "AND Year=" & yearValue & " "
          sumSql &= "AND From_Date='" & fromDate & "' "
          sumSql &= "AND To_Date='" & toDate & "'"
          SqlServer.GetResult(sumDt, sumSql)

          ' ==== 期間内・期間外の重量を集計 ====
          Dim splitDt As New DataTable
          Dim splitSql As String = ""
          splitSql &= "SELECT "
          splitSql &= " SUM(CASE WHEN r.addition_date BETWEEN p.START_DATE AND p.END_DATE "
          splitSql &= "          THEN COALESCE(FLOOR(TRY_CAST(r.weight AS DECIMAL(18,3)) * 10) / 10.0, 0) "
          splitSql &= "          ELSE 0 END) AS PeriodWeight, "
          splitSql &= " SUM(CASE WHEN r.addition_date BETWEEN p.START_DATE AND p.END_DATE "
          splitSql &= "          THEN 0 "
          splitSql &= "          ELSE COALESCE(FLOOR(TRY_CAST(r.weight AS DECIMAL(18,3)) * 10) / 10.0, 0) "
          splitSql &= "     END) AS RegularWeight "
          splitSql &= "FROM TRN_Results r CROSS JOIN MST_PeriodUnitPrice p "
          splitSql &= "WHERE r.staff_number='" & staffNo & "' "
          splitSql &= "AND r.addition_date BETWEEN '" & fromDate & "' AND '" & toDate & "' "
          splitSql &= "AND r.delete_flg = '0'"
          SqlServer.GetResult(splitDt, splitSql)

          Dim periodWeight As Decimal = If(splitDt.Rows.Count > 0, SafeToDecimal(splitDt.Rows(0)("PeriodWeight")), 0D)
          Dim regularWeight As Decimal = If(splitDt.Rows.Count > 0, SafeToDecimal(splitDt.Rows(0)("RegularWeight")), 0D)
          Dim wageInside As Decimal = periodWeight * periodPrice
          Dim wageOutside As Decimal = regularWeight * regularPrice
          Dim transportFee As Decimal = If(sumDt.Rows.Count > 0, SafeToDecimal(sumDt.Rows(0)("Transport_Fee")), 0D)
          Dim kaikinFee As Decimal = If(sumDt.Rows.Count > 0, SafeToDecimal(sumDt.Rows(0)("Kaikin_Fee")), 0D)

          ' ==== 下部明細 ====
          Dim totalRowIndex As Integer = rowIndex + 1
          rowIndex += 2

          ' ==== 下部明細 ====
          Dim detailFontSize As Integer = SummaryFontSize
          Dim totalFontSize As Integer = GrandTotalFontSize
          For summaryRow As Integer = rowIndex To rowIndex + 4
            ws.Range(summaryRow, 2, summaryRow, 5).Merge()
            ws.Range(summaryRow, 6, summaryRow, 12).Merge()
          Next

          ws.Cell(rowIndex, 2).Value = "茶摘賃（期間内）"
          ws.Cell(rowIndex, 2).Style.Font.Bold = True
          ws.Cell(rowIndex, 2).Style.Font.FontSize = detailFontSize

          ws.Cell(rowIndex, 6).Value = periodWeight.ToString("#,##0.0") & " kg × " &
                             periodPrice.ToString("#,##0") & " 円 = " &
                             wageInside.ToString("#,##0") & " 円"
          ws.Cell(rowIndex, 6).Style.Font.Bold = True
          ws.Cell(rowIndex, 6).Style.Font.FontSize = NumericFontSize

          ws.Cell(rowIndex + 1, 2).Value = "茶摘賃（期間外）"
          ws.Cell(rowIndex + 1, 2).Style.Font.Bold = True
          ws.Cell(rowIndex + 1, 2).Style.Font.FontSize = detailFontSize

          ws.Cell(rowIndex + 1, 6).Value = regularWeight.ToString("#,##0.0") & " kg × " &
                                 regularPrice.ToString("#,##0") & " 円 = " &
                                 wageOutside.ToString("#,##0") & " 円"
          ws.Cell(rowIndex + 1, 6).Style.Font.Bold = True
          ws.Cell(rowIndex + 1, 6).Style.Font.FontSize = NumericFontSize

          ws.Cell(rowIndex + 2, 2).Value = "交通費"
          ws.Cell(rowIndex + 2, 2).Style.Font.Bold = True
          ws.Cell(rowIndex + 2, 2).Style.Font.FontSize = detailFontSize

          ws.Cell(rowIndex + 2, 6).Value = transportFee.ToString("#,##0") & " 円"
          ws.Cell(rowIndex + 2, 6).Style.Font.Bold = True
          ws.Cell(rowIndex + 2, 6).Style.Font.FontSize = NumericFontSize

          ws.Cell(rowIndex + 3, 2).Value = "皆勤賞"
          ws.Cell(rowIndex + 3, 2).Style.Font.Bold = True
          ws.Cell(rowIndex + 3, 2).Style.Font.FontSize = detailFontSize

          ws.Cell(rowIndex + 3, 6).Value = kaikinFee.ToString("#,##0") & " 円"
          ws.Cell(rowIndex + 3, 6).Style.Font.Bold = True
          ws.Cell(rowIndex + 3, 6).Style.Font.FontSize = NumericFontSize

          ws.Cell(rowIndex + 4, 2).Value = "総合計"
          ws.Cell(rowIndex + 4, 2).Style.Font.Bold = True
          ws.Cell(rowIndex + 4, 2).Style.Font.FontSize = totalFontSize

          ws.Cell(rowIndex + 4, 6).Value = (wageInside + wageOutside + transportFee + kaikinFee).ToString("#,##0") & " 円"
          ws.Cell(rowIndex + 4, 6).Style.Font.Bold = True
          ws.Cell(rowIndex + 4, 6).Style.Font.FontSize = GrandTotalValueFontSize

          ' 背景色（任意／目立たせる場合）
          ws.Range(rowIndex, 2, rowIndex + 4, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
          ws.Range(rowIndex, 6, rowIndex + 4, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
          ws.Range(rowIndex, 2, rowIndex + 4, 12).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
          ws.Range(rowIndex + 4, 1, rowIndex + 4, 12).Style.Fill.BackgroundColor = XLColor.LightGray
          ws.Rows(rowIndex, rowIndex + 3).Height = SummaryRowHeight
          ws.Row(rowIndex + 4).Height = GrandTotalRowHeight

          ' ==== 体裁（フォント/枠/列幅/印刷）====
          ws.Range(3, 1, totalRowIndex - 1, 12).Style.Font.FontSize = ReportFontSize
          ws.Range(3, 2, totalRowIndex - 1, 12).Style.Font.FontSize = NumericFontSize
          ws.Column(1).Width = 30
          For c As Integer = 2 To 12
            ws.Column(c).Width = 8.5
          Next
          ws.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin
          ws.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Thin

          ' 計算部分の枠線を消す
          ws.Range(totalRowIndex, 1, rowIndex + 4, 12).Style.Border.OutsideBorder = XLBorderStyleValues.None
          ws.Range(totalRowIndex, 1, rowIndex + 4, 12).Style.Border.InsideBorder = XLBorderStyleValues.None

          ws.PageSetup.PageOrientation = XLPageOrientation.Portrait
          ws.PageSetup.PaperSize = XLPaperSize.A4Paper
          ws.PageSetup.FitToPages(1, 1)
          ws.PageSetup.CenterHorizontally = True
          With ws.PageSetup.Margins
            .Left = 0.6
            .Right = 0.4
            .Top = 0.4
            .Bottom = 0.2
            .Header = 0
            .Footer = 0
          End With

          ' シート全体をパスワード付きで保護
          ws.Protect(SheetProtectionPassword)
        Next

        wb.SaveAs(filePath)
      End Using

      MessageBox.Show("個人別帳票を出力しました。" & vbCrLf & filePath,
                      "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Process.Start(filePath)

    Catch ex As Exception
      MessageBox.Show("Excel出力エラー:" & ex.Message, "エラー",
                      MessageBoxButtons.OK, MessageBoxIcon.[Error])
    Finally
      dt.Dispose()
    End Try

  End Sub

  ' WorkerSummary：同一年度＋期間の既存データを削除
  Private Function GetDeleteSummarySql(yearValue As Integer, fromDate As Date, toDate As Date) As String
    Dim sql As String = ""
    sql &= "DELETE FROM TRN_WorkerSummary "
    sql &= "WHERE Year=" & yearValue
    Return sql
  End Function

  ' WorkerSummary：期間集計して INSERT（合算賃金＋交通費＋皆勤賞）
  Private Function GetInsertSummarySql(yearValue As Integer, fromDate As Date, toDate As Date) As String
    Dim sql As String = ""

    sql &= "WITH WeightSplit AS ( "
    sql &= " SELECT "
    sql &= "     r.staff_number, "
    sql &= "     r.staff_name, "
    sql &= "     SUM(CASE "
    sql &= "         WHEN r.addition_date BETWEEN p.START_DATE AND p.END_DATE THEN FLOOR(CAST(r.weight AS DECIMAL(18, 3)) * 10) / 10.0 "
    sql &= "         ELSE 0 "
    sql &= "     END) AS PeriodWeight, "
    sql &= "     SUM(CASE "
    sql &= "         WHEN r.addition_date NOT BETWEEN p.START_DATE AND p.END_DATE THEN FLOOR(CAST(r.weight AS DECIMAL(18, 3)) * 10) / 10.0 "
    sql &= "         ELSE 0 "
    sql &= "     END) AS RegularWeight "
    sql &= " FROM TRN_Results r "
    sql &= " OUTER APPLY (SELECT TOP 1 * FROM MST_PeriodUnitPrice) p "
    sql &= " WHERE r.addition_date BETWEEN '" & fromDate & "' AND '" & toDate & "' "
    sql &= "   AND r.delete_flg = '0' "
    sql &= " GROUP BY r.staff_number, r.staff_name "
    sql &= ") "

    sql &= "INSERT INTO TRN_WorkerSummary ( "
    sql &= " Staff_Number, Staff_Name, Year, From_Date, To_Date, "
    sql &= " Total_Weight, Wage, Transport_Fee, Kaikin_Fee, Total_Amount "
    sql &= ") "
    sql &= "SELECT "
    sql &= " ws.staff_number, "
    sql &= " ws.staff_name, "
    sql &= " " & yearValue & ", "
    sql &= " '" & fromDate & "', "
    sql &= " '" & toDate & "', "
    sql &= " FLOOR((ws.PeriodWeight + ws.RegularWeight) * 10) / 10.0 AS TotalWeight, "
    sql &= " CAST((ws.PeriodWeight * p.PERIOD_UNIT_PRICE) + (ws.RegularWeight * p.REGULAR_UNIT_PRICE) AS INT) AS Wage, "
    sql &= " CAST(FLOOR((ws.PeriodWeight + ws.RegularWeight) * 10) AS INT) AS TransportFee, "
    sql &= " CASE "
    sql &= "     WHEN ((DATEDIFF(DAY,'" & fromDate & "','" & toDate & "') + 1) "
    sql &= "           - (SELECT COUNT(*) FROM Trn_RainDay WHERE rain_date BETWEEN '" & fromDate & "' AND '" & toDate & "')) = "
    sql &= "          (SELECT COUNT(DISTINCT r2.addition_date) "
    sql &= "           FROM TRN_Results r2 "
    sql &= "           WHERE r2.staff_number = ws.staff_number "
    sql &= "             AND r2.delete_flg = '0' "
    sql &= "             AND r2.addition_date BETWEEN '" & fromDate & "' AND '" & toDate & "' "
    sql &= "             AND r2.addition_date NOT IN ( "
    sql &= "                 SELECT rain_date FROM Trn_RainDay WHERE rain_date BETWEEN '" & fromDate & "' AND '" & toDate & "' "
    sql &= "             )) "
    sql &= "     THEN CAST(FLOOR((ws.PeriodWeight + ws.RegularWeight) * 10) AS INT) "
    sql &= "     ELSE 0 "
    sql &= " END AS KaikinFee, "
    sql &= " CAST(( "
    sql &= "     (ws.PeriodWeight * p.PERIOD_UNIT_PRICE) + "
    sql &= "     (ws.RegularWeight * p.REGULAR_UNIT_PRICE) + "
    sql &= "     CAST(FLOOR((ws.PeriodWeight + ws.RegularWeight) * 10) AS INT) + "
    sql &= "     CASE "
    sql &= "         WHEN ((DATEDIFF(DAY,'" & fromDate & "','" & toDate & "') + 1) "
    sql &= "               - (SELECT COUNT(*) FROM Trn_RainDay WHERE rain_date BETWEEN '" & fromDate & "' AND '" & toDate & "')) = "
    sql &= "              (SELECT COUNT(DISTINCT r2.addition_date) "
    sql &= "               FROM TRN_Results r2 "
    sql &= "               WHERE r2.staff_number = ws.staff_number "
    sql &= "                 AND r2.delete_flg = '0' "
    sql &= "                 AND r2.addition_date BETWEEN '" & fromDate & "' AND '" & toDate & "' "
    sql &= "                 AND r2.addition_date NOT IN ( "
    sql &= "                     SELECT rain_date FROM Trn_RainDay WHERE rain_date BETWEEN '" & fromDate & "' AND '" & toDate & "' "
    sql &= "                 )) "
    sql &= "         THEN CAST(FLOOR((ws.PeriodWeight + ws.RegularWeight) * 10) AS INT) "
    sql &= "         ELSE 0 "
    sql &= "     END "
    sql &= " ) AS INT) AS TotalAmount "
    sql &= "FROM WeightSplit ws "
    sql &= " OUTER APPLY (SELECT TOP 1 * FROM MST_PeriodUnitPrice) p "
    sql &= " ORDER BY CAST(ws.staff_number AS INT); "

    Return sql
  End Function

  Private Sub Form_PersonalExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "茶摘日報（個人別）" & " ( " & updateTime & " ) "
    Me.KeyPreview = True
    FormBorderStyle = FormBorderStyle.FixedSingle
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Me.Dispose()
  End Sub

  Private Sub Form_PersonalExport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F5
        BtnExport.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub
End Class
