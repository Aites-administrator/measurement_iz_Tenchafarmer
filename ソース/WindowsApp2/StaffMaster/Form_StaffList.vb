Imports Common
Imports Common.ClsFunction
Imports ClosedXML.Excel
Public Class Form_StaffList

  Private ReadOnly ResultCsvPath As String = ReadSettingIniFile("RESULT_CSV_PATH", "VALUE")

  Private CheckboxExistFlg As New Boolean
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

  Private Sub Form_StaffList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "担当者マスタ一覧" & " ( " & updateTime & " ) "
    Me.KeyPreview = True
    StaffDetail.RowHeadersVisible = False
    FormBorderStyle = FormBorderStyle.FixedSingle


    StaffDetail.AllowUserToAddRows = False

    StaffDetail.ColumnCount = 2

    ' 残りのヘッダーテキストを設定
    StaffDetail.Columns(0).HeaderText = "担当者ｺｰﾄﾞ"
    StaffDetail.Columns(1).HeaderText = "担当者名"


    ' カラムの幅指定
    StaffDetail.Columns(0).Width = 150
    StaffDetail.Columns(1).Width = 190

    'カラムの整列設定
    For i As Integer = 0 To 1
      StaffDetail.Columns(i).DefaultCellStyle.Alignment =
      DataGridViewContentAlignment.MiddleCenter
    Next

    'ヘッダーの整列設定
    For i As Integer = 0 To 1
      StaffDetail.Columns(i).HeaderCell.Style.Alignment =
      DataGridViewContentAlignment.MiddleCenter
    Next

    SelectStaffMaster()

    ' 選択モードを全カラム選択に設定
    StaffDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    If StaffDetail.Rows.Count > 0 Then
      StaffDetail.CurrentCell = StaffDetail.Rows(0).Cells(0)
      StaffDetail.Rows(0).Selected = True
    End If

    CustomizeDataGridViewHeader() ' ヘッダーのデザイン変更

  End Sub

  Public Sub SelectStaffMaster()
    Dim sql As String = String.Empty
    sql = GetAllSelectSql()
    Try
      With tmpDb
        SqlServer.GetResult(tmpDt, sql)

        If tmpDt.Rows.Count = 0 Then
          MessageBox.Show("担当者マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
          WriteDetail(tmpDt, StaffDetail)
        End If
      End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
    End Try

    '検索結果が存在する場合、先頭行選択
    If StaffDetail.Rows.Count > 0 Then
      StaffDetail.CurrentCell = StaffDetail.Rows(0).Cells(0)
      StaffDetail.Rows(0).Selected = True

    End If
  End Sub
  Private Function GetAllSelectSql() As String

    Dim sql As String = String.Empty

    sql &= " SELECT"
    sql &= "     Staff_Number,"
    sql &= "     Staff_Name"
    sql &= " FROM"
    sql &= "     MST_Staff"
    sql &= " ORDER BY"
    sql &= "     Staff_Number"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Sub CustomizeDataGridViewHeader()
    With StaffDetail
      ' ヘッダーの背景色を変更
      .EnableHeadersVisualStyles = False ' デフォルトの Windows スタイルを無効化
      .ColumnHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow ' ヘッダーの背景色
      .ColumnHeadersDefaultCellStyle.ForeColor = Color.Black ' ヘッダーの文字色
      .ColumnHeadersDefaultCellStyle.Font = New Font("Meiryo", 10, FontStyle.Bold) ' フォント変更
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ' ヘッダー中央寄せ
    End With
  End Sub

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    Form_StaffDetail.InputMode = 1
    Form_StaffDetail.ShowDialog()
  End Sub

  Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
    '詳細画面の項目値セット
    SetListData()
    Form_StaffDetail.InputMode = 2
    Form_StaffDetail.ShowDialog()
  End Sub

  Private Sub SetListData()
    '選択している行の行番号の取得
    Dim i As Integer = StaffDetail.CurrentRow.Index
    Form_StaffDetail.CodeTextValue = StaffDetail.Rows(i).Cells(0).Value
    Form_StaffDetail.NameTextValue = StaffDetail.Rows(i).Cells(1).Value
  End Sub

  Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
    DeleteManufacturerMaster()
  End Sub
  Private Sub DeleteManufacturerMaster()
    Dim sql As String = String.Empty
    Dim rowSelectionCode As String = String.Empty
    Dim confirmation As String
    Dim msg1 As String
    Dim msg2 As String
    With tmpDb
      Try
        sql = GetDeleteSql(True)
        msg1 = "削除します。" & vbCrLf & "よろしいでしょうか。"
        msg2 = "削除処理完了しました。"

        confirmation = MessageBox.Show(msg1, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          ' SQL実行結果が1件か？
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show(msg2, "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelectStaffMaster()
          Else
            ' 削除失敗
            MessageBox.Show("担当者マスタの削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End If
        Else
          Exit Sub
        End If
      Catch ex As Exception
        Call ComWriteErrLog([GetType]().Name,
                      System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
        Throw New Exception(ex.Message)
      End Try
    End With
  End Sub
  Private Sub RefreshText()
    For i As Integer = 0 To StaffDetail.Rows.Count - 1
      StaffDetail.Rows(i).Selected = True
      StaffDetail.FirstDisplayedScrollingRowIndex = i
      StaffDetail.CurrentCell = StaffDetail.Rows(i).Cells(0)
      Exit For
    Next
  End Sub
  Private Function GetDeleteSql(DeleteFlg As Boolean) As String
    Dim sql As String = String.Empty
    Dim currentRow As Integer = StaffDetail.SelectedCells(0).RowIndex
    Dim codeInt As Integer = StaffDetail.Rows(currentRow).Cells(0).Value

    sql &= " DELETE"
    sql &= " FROM"
    sql &= "     MST_Staff"
    sql &= " WHERE"
    sql &= "     Staff_Number = " & codeInt

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub StaffDetail_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles StaffDetail.CellDoubleClick
    '詳細画面の項目値セット
    SetListData()
    Form_StaffDetail.InputMode = 2
    Form_StaffDetail.ShowDialog()
  End Sub

  Private Sub Form_ManufacturerList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F5
        CreateButton.PerformClick()
      Case Keys.F6
        UpdateButton.PerformClick()
      Case Keys.F7
        DeleteButton.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub

  Private Sub OutputButton_Click(sender As Object, e As EventArgs) Handles OutputButton.Click
    Dim sql As String = GetStaffMasterSelectSql()
    Dim OutputDt As New DataTable
    Dim currentYear As Integer = DateTime.Now.Year
    Dim filePath As String = ResultCsvPath & "担当者一覧__" & currentYear & ".xlsx"

    Try
      SqlServer.GetResult(OutputDt, sql)

      Dim wb As New XLWorkbook()
      Dim totalRecords As Integer = OutputDt.Rows.Count
      Dim pageSize As Integer = 20
      Dim pageCount As Integer = Math.Ceiling(totalRecords / pageSize)

      For pageIndex As Integer = 0 To pageCount - 1
        Dim ws = wb.Worksheets.Add("担当者一覧（" & (pageIndex + 1).ToString() & "）")
        Dim startRow As Integer = pageIndex * pageSize
        Dim endRow As Integer = Math.Min(startRow + pageSize - 1, totalRecords - 1)
        CreateSplitSheet(ws, OutputDt, startRow, endRow, pageIndex)
      Next

      wb.Worksheet(1).SetTabActive()
      wb.SaveAs(filePath)
      MessageBox.Show("担当者一覧を出力しました。" & vbCrLf & filePath, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Process.Start(filePath)

    Catch ex As Exception
      MessageBox.Show("担当者一覧の出力エラー:" & ex.Message, "エラー",
                        MessageBoxButtons.OK, MessageBoxIcon.[Error])
      Call ComWriteErrLog(Me.GetType().Name, Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
    Finally
      OutputDt.Dispose()
    End Try
  End Sub

  Private Sub CreateSplitSheet(ws As IXLWorksheet, dt As DataTable, startIndex As Integer, endIndex As Integer, pageIndex As Integer)

    Dim titleText As String = "担当者リスト" & GetCircledNumber(pageIndex + 1)
    Dim titleRange = ws.Range(1, 1, 1, 6)
    titleRange.Merge()
    With titleRange
      .Value = titleText
      .Style.Font.Bold = True
      .Style.Font.FontSize = 72
      .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
      .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    End With
    ws.Row(1).Height = 150

    ws.Cell(3, 1).Value = "コード"
    ws.Cell(3, 2).Value = "名前"
    ws.Range(3, 3, 3, 4).Merge().Value = "バーコード"
    ws.Range(3, 5, 3, 6).Merge().Value = "バーコード"

    With ws.Range(3, 1, 3, 6).Style
      .Font.Bold = True
      .Font.FontSize = 28
      .Fill.BackgroundColor = XLColor.LightSteelBlue
      .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
      .Alignment.Vertical = XLAlignmentVerticalValues.Center
      .Border.OutsideBorder = XLBorderStyleValues.Thin
      .Border.InsideBorder = XLBorderStyleValues.Thin
    End With
    ws.Row(3).Height = 60

    Dim rowOffset As Integer = 4
    For i As Integer = startIndex To endIndex
      Dim row = rowOffset + (i - startIndex)
      Dim code = dt.Rows(i)("担当者コード").ToString()
      Dim name = dt.Rows(i)("担当者名").ToString()
      Dim barcode = "*" & code & "*"

      With ws.Cell(row, 1)
        .Value = code
        .Style.Font.FontSize = 48
        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
      End With

      With ws.Cell(row, 2)
        .Value = name
        .Style.Font.FontSize = 70
        .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
        .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
      End With

      Dim isOdd As Boolean = ((row - rowOffset) Mod 2 = 0)
      ws.Range(row, 3, row, 4).Merge()
      ws.Range(row, 5, row, 6).Merge()

      If isOdd Then

        With ws.Cell(row, 3)
          .Value = barcode
          .Style.Font.FontName = "Code39"
          .Style.Font.FontSize = 100
          .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
          .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
        End With
      Else

        With ws.Cell(row, 5)
          .Value = barcode
          .Style.Font.FontName = "Code39"
          .Style.Font.FontSize = 100
          .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
          .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
        End With
      End If

      ws.Row(row).Height = 200

      If (i - startIndex) Mod 2 = 1 Then
        ws.Range(row, 1, row, 6).Style.Fill.BackgroundColor = XLColor.AliceBlue
      End If

      With ws.Range(row, 1, row, 6).Style.Border
        .OutsideBorder = XLBorderStyleValues.Thin
        .InsideBorder = XLBorderStyleValues.Thin
      End With
    Next

    ws.Column(1).Width = 30
    ws.Column(2).Width = 100
    ws.Column(3).Width = 70
    ws.Column(4).Width = 70
    ws.Column(5).Width = 70
    ws.Column(6).Width = 70

    With ws.PageSetup
      .PagesWide = 1
      .PagesTall = 1
      .CenterHorizontally = True
      .PageOrientation = XLPageOrientation.Portrait
      .PaperSize = XLPaperSize.A4Paper
    End With

    With ws.PageSetup.Margins
      .Top = 0.39
      .Bottom = 0.39
      .Left = 0.39
      .Right = 0.39
      .Header = 0.39
      .Footer = 0.39
    End With
  End Sub

  Private Function GetCircledNumber(n As Integer) As String
    If n >= 1 AndAlso n <= 20 Then
      Return ChrW(&H2460 + (n - 1))
    Else
      Return n.ToString()
    End If
  End Function


  Private Function EncloseDoubleQuotes(field As String) As String
    Return "" & field & ""
  End Function

  Private Function GetStaffMasterSelectSql() As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     Staff_Number AS 担当者コード,"
    sql &= "     Staff_Name AS 担当者名"
    sql &= " FROM"
    sql &= "     MST_Staff"
    sql &= " ORDER BY"
    sql &= "     Staff_Number Asc"

    Call WriteExecuteLog("Form_GarbageTypeList", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

End Class