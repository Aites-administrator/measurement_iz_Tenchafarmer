Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports System.Globalization
Imports Common
Imports Common.ClsFunction

Public Class Form_SummaryExport
  Private ReadOnly ResultCsvPath As String = ReadSettingIniFile("RESULT_CSV_PATH", "VALUE")

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
    Dim yearValue As Integer = DateTimePickerYear.Value.Year

    Dim fileName As String = ResultCsvPath & "茶摘報酬集計表_" & yearValue & ".xlsx"

    Dim dt As New DataTable
    Try
      ' 年度データ存在確認
      Dim sqlCheck As String = "SELECT COUNT(*) FROM TRN_WorkerSummary WHERE Year = " & yearValue
      Dim checkDt As New DataTable
      SqlServer.GetResult(checkDt, sqlCheck)
      If CInt(checkDt.Rows(0)(0)) = 0 Then
        MessageBox.Show(yearValue & "年度のデータが存在しません。" & vbCrLf &
                        "先に『茶摘日報（個人別）』を実行してください。",
                        "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Exit Sub
      End If

      ' 年度データ取得
      Dim sqlSelect As String =
          "SELECT Staff_Number, Staff_Name, Total_Weight, Total_Amount, From_Date, To_Date " &
          "FROM TRN_WorkerSummary WHERE Year = " & yearValue &
          " ORDER BY CAST(Staff_Number AS INT)"
      SqlServer.GetResult(dt, sqlSelect)

      ' 和暦表記
      Dim cultureJP As New CultureInfo("ja-JP", True)
      cultureJP.DateTimeFormat.Calendar = New JapaneseCalendar()
      Dim warekiText As String = New DateTime(yearValue, 1, 1).ToString("ggyy年度", cultureJP)

      ' 出力期間
      Dim fromDate As Date = CDate(dt.Rows(0)("From_Date"))
      Dim toDate As Date = CDate(dt.Rows(0)("To_Date"))

      ' 合計金額 / 作業人数
      Dim totalAmount As Decimal = dt.AsEnumerable().Sum(Function(r) Convert.ToDecimal(r("Total_Amount")))
      Dim workerCount As Integer = dt.Rows.Count

      Const workersPerPage As Integer = 120
      Const maxPages As Integer = 2
      If workerCount > workersPerPage * maxPages Then
        MessageBox.Show("作業人数が出力上限の240名を超えています。",
                        "出力上限", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Exit Sub
      End If

      Dim pageCount As Integer = Math.Max(1, CInt(Math.Ceiling(workerCount / CDbl(workersPerPage))))

      ' Excel 出力
      Using wb As New XLWorkbook()
        Dim ws = wb.Worksheets.Add("茶摘報酬集計表")

        Const pageRowCount As Integer = 47
        For pageIndex As Integer = 0 To pageCount - 1
          Dim pageTopRow As Integer = 1 + (pageIndex * pageRowCount)
          WriteSummaryPage(ws, pageTopRow, pageIndex + 1, pageCount,
                           pageIndex * workersPerPage, dt, warekiText,
                           fromDate, toDate, totalAmount, workerCount)
        Next

        If pageCount = 2 Then
          ws.PageSetup.AddHorizontalPageBreak(pageRowCount)
        End If

        ' 列幅（番号・名前・金額を3ブロック配置）
        For Each startColumn In {1, 5, 9}
          ws.Column(startColumn).Width = 11
          ws.Column(startColumn + 1).Width = 18
          ws.Column(startColumn + 2).Width = 15
        Next
        ws.Column(4).Width = 2.5
        ws.Column(8).Width = 2.5

        ' A4横向きで、人数に応じて1～2ページに固定
        ws.PageSetup.PageOrientation = XLPageOrientation.Landscape
        ws.PageSetup.PaperSize = XLPaperSize.A4Paper
        ws.PageSetup.FitToPages(1, pageCount)
        ws.PageSetup.CenterHorizontally = True
        With ws.PageSetup.Margins
          .Left = 0.25
          .Right = 0.25
          .Top = 0.3
          .Bottom = 0.3
          .Header = 0
          .Footer = 0
        End With

        wb.SaveAs(fileName)
      End Using

      MessageBox.Show("茶摘報酬集計表を出力しました。" & vbCrLf & fileName,
                      "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Process.Start(fileName)

    Catch ex As Exception
      MessageBox.Show("Excel出力エラー:" & ex.Message, "エラー",
                      MessageBoxButtons.OK, MessageBoxIcon.[Error])
    Finally
      dt.Dispose()
    End Try
  End Sub

  Private Sub WriteSummaryPage(ws As IXLWorksheet,
                               pageTopRow As Integer,
                               pageNumber As Integer,
                               pageCount As Integer,
                               dataStartIndex As Integer,
                               dt As DataTable,
                               warekiText As String,
                               fromDate As Date,
                               toDate As Date,
                               totalAmount As Decimal,
                               workerCount As Integer)

    Const rowsPerBlock As Integer = 40
    Const blocks As Integer = 3

    Dim summaryStartRow As Integer = pageTopRow + 1
    Dim headerRow As Integer = pageTopRow + 5
    Dim dataStartRow As Integer = pageTopRow + 6

    ' タイトル
    ws.Cell(pageTopRow, 1).Value = warekiText & " 茶摘報酬集計表"
    ws.Range(pageTopRow, 1, pageTopRow, 11).Merge()
    With ws.Range(pageTopRow, 1, pageTopRow, 11).Style
      .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
      .Alignment.Vertical = XLAlignmentVerticalValues.Center
      .Font.Bold = True
      .Font.FontSize = 22
    End With
    ws.Row(pageTopRow).Height = 40

    ' 出力期間・総合計・作業人数（全ページ共通）
    ws.Cell(summaryStartRow, 1).Value = "出力期間"
    ws.Cell(summaryStartRow, 2).Value = fromDate.ToString("yyyy/MM/dd") & " ～ " & toDate.ToString("yyyy/MM/dd")
    ws.Range(summaryStartRow, 2, summaryStartRow, 4).Merge()
    ws.Cell(summaryStartRow + 1, 1).Value = "総合計"
    ws.Cell(summaryStartRow + 1, 2).Value = totalAmount
    ws.Range(summaryStartRow + 1, 2, summaryStartRow + 1, 4).Merge()
    ws.Cell(summaryStartRow + 1, 2).Style.NumberFormat.Format = "#,##0円"
    ws.Cell(summaryStartRow + 2, 1).Value = "作業人数"
    ws.Cell(summaryStartRow + 2, 2).Value = workerCount & "名"
    ws.Range(summaryStartRow + 2, 2, summaryStartRow + 2, 4).Merge()
    ws.Cell(summaryStartRow + 2, 11).Value = pageNumber & " / " & pageCount & " ページ"
    ws.Cell(summaryStartRow + 2, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

    For r As Integer = summaryStartRow To summaryStartRow + 2
      ws.Row(r).Height = 27
    Next
    With ws.Range(summaryStartRow, 1, summaryStartRow + 2, 2).Style
      .Font.FontSize = 13
      .Font.Bold = True
      .Alignment.Vertical = XLAlignmentVerticalValues.Center
    End With
    ws.Cell(summaryStartRow + 2, 11).Style.Font.FontSize = 11

    ' 明細（40名×3ブロック＝1ページ120名）
    For b As Integer = 0 To blocks - 1
      Dim colOffset As Integer = b * 4
      Dim rowEnd As Integer = dataStartRow + rowsPerBlock - 1

      ws.Cell(headerRow, 1 + colOffset).Value = "番号"
      ws.Cell(headerRow, 2 + colOffset).Value = "名前"
      ws.Cell(headerRow, 3 + colOffset).Value = "金額"
      With ws.Range(headerRow, 1 + colOffset, headerRow, 3 + colOffset).Style
        .Font.Bold = True
        .Font.FontSize = 12
        .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        .Alignment.Vertical = XLAlignmentVerticalValues.Center
      End With
      ws.Row(headerRow).Height = 22

      For i As Integer = 0 To rowsPerBlock - 1
        Dim rowIdx As Integer = dataStartRow + i
        Dim dataIdx As Integer = dataStartIndex + (b * rowsPerBlock) + i
        If dataIdx < dt.Rows.Count Then
          Dim dr As DataRow = dt.Rows(dataIdx)
          ws.Cell(rowIdx, 1 + colOffset).Value = dr("Staff_Number").ToString().PadLeft(4, "0"c)
          ws.Cell(rowIdx, 1 + colOffset).Style.NumberFormat.Format = "@"
          ws.Cell(rowIdx, 2 + colOffset).Value = dr("Staff_Name").ToString()
          ws.Cell(rowIdx, 3 + colOffset).Value = Convert.ToDecimal(dr("Total_Amount"))
          ws.Cell(rowIdx, 3 + colOffset).Style.NumberFormat.Format = "#,##0円"
        Else
          ws.Cell(rowIdx, 1 + colOffset).Value = ""
          ws.Cell(rowIdx, 2 + colOffset).Value = ""
          ws.Cell(rowIdx, 3 + colOffset).Value = ""
        End If
      Next

      Dim used = ws.Range(headerRow, 1 + colOffset, rowEnd, 3 + colOffset)
      ws.Range(dataStartRow, 1 + colOffset, rowEnd, 3 + colOffset).Style.Font.FontSize = 11
      used.Style.Border.OutsideBorder = XLBorderStyleValues.Thin
      used.Style.Border.InsideBorder = XLBorderStyleValues.Thin
    Next
  End Sub

  Private Sub Form_SummaryExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "茶摘報酬集計表" & " ( " & updateTime & " ) "
    Me.KeyPreview = True
    FormBorderStyle = FormBorderStyle.FixedSingle
  End Sub

  Private Sub Form_SummaryExport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F5
        BtnExport.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Me.Dispose()
  End Sub
End Class
