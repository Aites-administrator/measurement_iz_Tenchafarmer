Imports Common
Imports Common.ClsFunction
Imports ClosedXML.Excel
Public Class Form_StaffList
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
    StaffDetail.Columns(1).Width = 200

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
    Dim filePath As String = "C:\Temp\担当者一覧_" & currentYear & ".xlsx"

    Try
      SqlServer.GetResult(OutputDt, sql)

      Dim wb As New XLWorkbook()
      Dim totalRecords As Integer = OutputDt.Rows.Count
      Dim pageSize As Integer = 70 ' 1シートに70件（35 × 左右）
      Dim pageCount As Integer = Math.Ceiling(totalRecords / pageSize)

      For pageIndex As Integer = 0 To pageCount - 1
        Dim ws = wb.Worksheets.Add("担当者一覧（" & (pageIndex + 1).ToString() & "）")
        Dim startRow As Integer = pageIndex * pageSize
        Dim endRow As Integer = Math.Min(startRow + pageSize - 1, totalRecords - 1)
        CreateSplitSheet(ws, OutputDt, startRow, endRow)
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

  Private Sub CreateSplitSheet(ws As IXLWorksheet, dt As DataTable, startIndex As Integer, endIndex As Integer)
    ' === タイトル（1行目）===
    Dim titleRange = ws.Range(1, 1, 1, 9)
    titleRange.Merge()
    With titleRange
      .Value = "担当者リスト（※ あ・い・う・え・お順）"
      .Style.Font.Bold = True
      .Style.Font.FontSize = 28
      .Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
      .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    End With
    ws.Row(1).Height = 35

    ' === ヘッダー（3行目）===
    ws.Cell(3, 1).Value = "コード"
    ws.Cell(3, 2).Value = "名前"
    ws.Range(3, 3, 3, 4).Merge().Value = "バーコード"

    ws.Cell(3, 6).Value = "コード"
    ws.Cell(3, 7).Value = "名前"
    ws.Range(3, 8, 3, 9).Merge().Value = "バーコード"

    With ws.Range(3, 1, 3, 9).Style
      .Font.Bold = True
      .Font.FontSize = 26
      .Fill.BackgroundColor = XLColor.LightSteelBlue
      .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
      .Alignment.Vertical = XLAlignmentVerticalValues.Center
      .Border.OutsideBorder = XLBorderStyleValues.Thin
      .Border.InsideBorder = XLBorderStyleValues.Thin
    End With
    ws.Row(3).Height = 45

    ' === データ（4行～38行）===
    Dim dataPerSide As Integer = 35
    Dim rowOffsetL As Integer = 4
    Dim rowOffsetR As Integer = 4

    For i As Integer = startIndex To endIndex
      Dim localIndex = i - startIndex
      Dim code = dt.Rows(i)("担当者コード").ToString()
      Dim name = dt.Rows(i)("担当者名").ToString()
      Dim barcode = "*" & code & "*"

      If localIndex < dataPerSide Then
        ' 左側
        Dim row = rowOffsetL + localIndex
        ws.Cell(row, 1).Value = code
        ws.Cell(row, 2).Value = name

        Dim barcodeCol = If(localIndex Mod 2 = 0, 3, 4)
        ws.Cell(row, barcodeCol).Value = barcode
        ws.Cell(row, 1).Style.Font.FontSize = 24
        ws.Cell(row, 2).Style.Font.FontSize = 24

        With ws.Cell(row, barcodeCol).Style
          .Font.FontName = "Code39"
          .Font.FontSize = 72
          .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
          .Alignment.Vertical = XLAlignmentVerticalValues.Center
        End With

        ' 行の共通設定
        ws.Row(row).Height = 75
        With ws.Range(row, 1, row, 4).Style
          .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
          .Alignment.Vertical = XLAlignmentVerticalValues.Center
          .Border.OutsideBorder = XLBorderStyleValues.Thin
          .Border.InsideBorder = XLBorderStyleValues.Thin

          If (i - startIndex) Mod 2 = 1 Then
            .Fill.BackgroundColor = XLColor.AliceBlue
          End If
        End With
      Else
        ' 右側
        Dim row = rowOffsetR + (localIndex - dataPerSide)
        ws.Cell(row, 6).Value = code
        ws.Cell(row, 7).Value = name

        Dim barcodeCol = If(localIndex Mod 2 = 0, 8, 9)
        ws.Cell(row, barcodeCol).Value = barcode
        ws.Cell(row, 6).Style.Font.FontSize = 24
        ws.Cell(row, 7).Style.Font.FontSize = 24

        With ws.Cell(row, barcodeCol).Style
          .Font.FontName = "Code39"
          .Font.FontSize = 72
          .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
          .Alignment.Vertical = XLAlignmentVerticalValues.Center
        End With

        ws.Row(row).Height = 75
        With ws.Range(row, 6, row, 9).Style
          .Alignment.Horizontal = XLAlignmentHorizontalValues.Center
          .Alignment.Vertical = XLAlignmentVerticalValues.Center
          .Border.OutsideBorder = XLBorderStyleValues.Thin
          .Border.InsideBorder = XLBorderStyleValues.Thin

          If (i - startIndex) Mod 2 = 1 Then
            .Fill.BackgroundColor = XLColor.AliceBlue
          End If
        End With
      End If
    Next

    ' === 列幅調整 ===
    Dim widths = New Dictionary(Of Integer, Double) From {
        {1, 20}, {2, 40}, {3, 45}, {4, 45}, {5, 5},
        {6, 20}, {7, 40}, {8, 45}, {9, 45}
    }
    For Each kvp In widths
      ws.Column(kvp.Key).Width = kvp.Value
    Next

    ' === 印刷設定 ===
    With ws.PageSetup
      .PagesWide = 1
      .PagesTall = 1
      .CenterHorizontally = True
      .PageOrientation = XLPageOrientation.Portrait
    End With
  End Sub

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
    sql &= "     Staff_Name COLLATE Japanese_XJIS_100_CI_AS"

    Call WriteExecuteLog("Form_GarbageTypeList", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

End Class