Imports Common
Imports Common.ClsFunction
Public Class Form_ItemList
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

  Private Sub Form_ItemList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "商品マスタ一覧" & " ( " & updateTime & " ) "

    Me.KeyPreview = True

    ItemDetail.RowHeadersVisible = False
    FormBorderStyle = FormBorderStyle.FixedSingle
    ItemDetail.AllowUserToAddRows = False

    ItemDetail.ColumnCount = 3
    ' 残りのヘッダーテキストを設定
    ItemDetail.Columns(0).HeaderText = "呼出ｺｰﾄﾞ"
    ItemDetail.Columns(1).HeaderText = "品番"
    ItemDetail.Columns(2).HeaderText = "品名"

    'カラムの幅指定
    ItemDetail.Columns(0).Width = 100
    ItemDetail.Columns(1).Width = 80
    ItemDetail.Columns(2).Width = 250
    'カラムの整列設定
    For i As Integer = 0 To 2
      ItemDetail.Columns(i).DefaultCellStyle.Alignment =
      DataGridViewContentAlignment.MiddleCenter
    Next

    'ヘッダーの整列設定
    For i As Integer = 0 To 2
      ItemDetail.Columns(i).HeaderCell.Style.Alignment =
      DataGridViewContentAlignment.MiddleCenter
    Next

    SelectItemMaster()

    ' 選択モードを全カラム選択に設定
    ItemDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    If ItemDetail.Rows.Count > 0 Then
      ItemDetail.CurrentCell = ItemDetail.Rows(0).Cells(0)
      ItemDetail.Rows(0).Selected = True
    End If

    CustomizeDataGridViewHeader() ' ヘッダーのデザイン変更

  End Sub
  ' DataGridView のヘッダーのデザインを変更
  Private Sub CustomizeDataGridViewHeader()
    With ItemDetail
      ' ヘッダーの背景色を変更
      .EnableHeadersVisualStyles = False ' デフォルトの Windows スタイルを無効化
      .ColumnHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow ' ヘッダーの背景色
      .ColumnHeadersDefaultCellStyle.ForeColor = Color.Black ' ヘッダーの文字色
      .ColumnHeadersDefaultCellStyle.Font = New Font("Meiryo", 10, FontStyle.Bold) ' フォント変更
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ' ヘッダー中央寄せ
    End With
  End Sub
  Public Sub SelectItemMaster()
    Dim sql As String = String.Empty
    sql = GetAllSelectSql()
    Try
      With tmpDb
        SqlServer.GetResult(tmpDt, sql)

        If tmpDt.Rows.Count = 0 Then
          MessageBox.Show("商品マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
          WriteDetail(tmpDt, ItemDetail)
          UpdateButton.Enabled = True
          DeleteButton.Enabled = True
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
    If ItemDetail.Rows.Count > 0 Then
      ItemDetail.CurrentCell = ItemDetail.Rows(0).Cells(0)
      ItemDetail.Rows(0).Selected = True
    End If
  End Sub

  Private Function GetAllSelectSql() As String

    Dim sql As String = String.Empty

    sql &= " SELECT"
    sql &= "     call_code"
    sql &= "    ,item_number "
    sql &= "    ,item_name "
    sql &= " FROM"
    sql &= "     MST_Item"
    sql &= " ORDER BY"
    sql &= "     call_code"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    Form_ItemDetail.InputMode = 1
    Form_ItemDetail.ShowDialog()
  End Sub
  Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
    '詳細画面の項目値セット
    SetListData()
    Form_ItemDetail.InputMode = 2
    Form_ItemDetail.ShowDialog()
  End Sub
  Private Sub SetListData()
    '選択している行の行番号の取得
    Dim i As Integer = ItemDetail.CurrentRow.Index
    Form_ItemDetail.CallCodeTextValue = ItemDetail.Rows(i).Cells(0).Value
    Form_ItemDetail.ItemNoTextValue = ItemDetail.Rows(i).Cells(1).Value
    Form_ItemDetail.ItemNameTextValue = ItemDetail.Rows(i).Cells(2).Value
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
    DeleteItemMaster()
  End Sub

  Private Sub DeleteItemMaster()
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
            SelectItemMaster()
          Else
            ' 削除失敗
            MessageBox.Show("商品マスタの削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
  Private Function GetDeleteSql(DeleteFlg As Boolean) As String
    Dim sql As String = String.Empty
    Dim currentRow As Integer = ItemDetail.SelectedCells(0).RowIndex
    Dim callCode As String = ItemDetail.Rows(currentRow).Cells(0).Value

    sql &= " DELETE"
    sql &= " FROM"
    sql &= "     MST_Item"
    sql &= " WHERE"
    sql &= "     call_code = " & callCode

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub ItemDetail_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles ItemDetail.CellDoubleClick
    ' 詳細画面の項目値セット
    SetListData()
    ' 入力モードを設定
    Form_ItemDetail.InputMode = 2
    ' 詳細画面を表示
    Form_ItemDetail.ShowDialog()
  End Sub

  Private Sub Form_ItemList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
End Class
