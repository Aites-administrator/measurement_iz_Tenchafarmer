﻿Imports Common
Imports Common.ClsFunction
Public Class Form_Free3List
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
  Private Sub Form_Free3List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "フリーマスタ３一覧" & " ( " & updateTime & " ) "

    ' キーイベントをフォーム全体で受け取るようにする
    Me.KeyPreview = True

    Free3Detail.RowHeadersVisible = False
    FormBorderStyle = FormBorderStyle.FixedSingle

    Free3Detail.AllowUserToAddRows = False

    Free3Detail.ColumnCount = 2

    ' 残りのヘッダーテキストを設定
    Free3Detail.Columns(0).HeaderText = "フリー3No."
    Free3Detail.Columns(1).HeaderText = "フリー3 名称"

    ' カラムの幅指定
    Free3Detail.Columns(0).Width = 150
    Free3Detail.Columns(1).Width = 200

    'カラムの整列設定
    For i As Integer = 0 To 1
      Free3Detail.Columns(i).DefaultCellStyle.Alignment =
      DataGridViewContentAlignment.MiddleCenter
    Next

    'ヘッダーの整列設定
    For i As Integer = 0 To 1
      Free3Detail.Columns(i).HeaderCell.Style.Alignment =
      DataGridViewContentAlignment.MiddleCenter
    Next

    SelectFree3Master()

    '選択モードを全カラム選択に設定
    Free3Detail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    If Free3Detail.Rows.Count > 0 Then
      Free3Detail.CurrentCell = Free3Detail.Rows(0).Cells(0)
      Free3Detail.Rows(0).Selected = True
    End If

    CustomizeDataGridViewHeader() ' ヘッダーのデザイン変更

  End Sub

  ' DataGridView のヘッダーのデザインを変更
  Private Sub CustomizeDataGridViewHeader()
    With Free3Detail
      ' ヘッダーの背景色を変更
      .EnableHeadersVisualStyles = False ' デフォルトの Windows スタイルを無効化
      .ColumnHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow ' ヘッダーの背景色
      .ColumnHeadersDefaultCellStyle.ForeColor = Color.Black ' ヘッダーの文字色
      .ColumnHeadersDefaultCellStyle.Font = New Font("Meiryo", 10, FontStyle.Bold) ' フォント変更
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ' ヘッダー中央寄せ
    End With
  End Sub
  Public Sub SelectFree3Master()
    Dim sql As String = String.Empty
    sql = GetAllSelectSql()
    Try
      With tmpDb
        SqlServer.GetResult(tmpDt, sql)

        If tmpDt.Rows.Count = 0 Then
          MessageBox.Show("フリー３マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
          WriteDetail(tmpDt, Free3Detail)
        End If
      End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
    End Try

    If Free3Detail.Rows.Count > 0 Then
      Free3Detail.Rows(0).Selected = True
    End If
  End Sub
  Private Function GetAllSelectSql() As String
    Dim sql As String = String.Empty

    sql &= " SELECT"
    sql &= "     free3_number,"
    sql &= "     free3_name"
    sql &= " FROM"
    sql &= "     MST_Free3"
    sql &= " ORDER BY"
    sql &= "     free3_number"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Me.Dispose()
  End Sub

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    Form_Free3Detail.InputMode = 1
    Form_Free3Detail.ShowDialog()
  End Sub

  Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
    '詳細画面の項目値セット
    SetListData()
    Form_Free3Detail.InputMode = 2
    Form_Free3Detail.ShowDialog()
  End Sub
  Private Sub SetListData()
    '選択している行の行番号の取得
    Dim i As Integer = Free3Detail.CurrentRow.Index
    Form_Free3Detail.free3CodeTextValue = Free3Detail.Rows(i).Cells(0).Value
    Form_Free3Detail.free3NameTextValue = Free3Detail.Rows(i).Cells(1).Value
  End Sub
  Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
    DeleteFree3Master()
  End Sub
  Private Sub DeleteFree3Master()
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
            SelectFree3Master()
          Else
            ' 削除失敗
            MessageBox.Show("フリー3マスタの削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Dim currentRow As Integer = Free3Detail.SelectedCells(0).RowIndex
    Dim free3Code As Integer = Free3Detail.Rows(currentRow).Cells(0).Value

    sql &= " DELETE"
    sql &= " FROM"
    sql &= "     MST_Free3"
    sql &= " WHERE"
    sql &= "     free3_number = " & free3Code

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub Free3Detail_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Free3Detail.CellDoubleClick
    '詳細画面の項目値セット
    SetListData()
    Form_Free3Detail.InputMode = 2
    Form_Free3Detail.ShowDialog()
  End Sub

  Private Sub Form_Free3List_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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