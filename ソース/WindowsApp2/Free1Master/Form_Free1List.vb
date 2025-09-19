﻿Imports Common
Imports Common.ClsFunction
Public Class Form_Free1List
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
  Private Sub Form_Free1List_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "フリーマスタ１一覧" & " ( " & updateTime & " ) "

    ' キーイベントをフォーム全体で受け取るようにする
    Me.KeyPreview = True

    Free1Detail.RowHeadersVisible = False
    FormBorderStyle = FormBorderStyle.FixedSingle

    Free1Detail.AllowUserToAddRows = False

    Free1Detail.ColumnCount = 2

    ' 残りのヘッダーテキストを設定
    Free1Detail.Columns(0).HeaderText = "フリー1No."
    Free1Detail.Columns(1).HeaderText = "フリー1 名称"

    ' カラムの幅指定
    Free1Detail.Columns(0).Width = 150
    Free1Detail.Columns(1).Width = 200

    'カラムの整列設定
    For i As Integer = 0 To 1
      Free1Detail.Columns(i).DefaultCellStyle.Alignment =
      DataGridViewContentAlignment.MiddleCenter
    Next

    'ヘッダーの整列設定
    For i As Integer = 0 To 1
      Free1Detail.Columns(i).HeaderCell.Style.Alignment =
      DataGridViewContentAlignment.MiddleCenter
    Next

    SelectFree1Master()

    '選択モードを全カラム選択に設定
    Free1Detail.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    If Free1Detail.Rows.Count > 0 Then
      Free1Detail.CurrentCell = Free1Detail.Rows(0).Cells(0)
      Free1Detail.Rows(0).Selected = True
    End If

    CustomizeDataGridViewHeader()

  End Sub

  ' DataGridView のヘッダーのデザインを変更
  Private Sub CustomizeDataGridViewHeader()
    With Free1Detail
      ' ヘッダーの背景色を変更
      .EnableHeadersVisualStyles = False ' デフォルトの Windows スタイルを無効化
      .ColumnHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow ' ヘッダーの背景色
      .ColumnHeadersDefaultCellStyle.ForeColor = Color.Black ' ヘッダーの文字色
      .ColumnHeadersDefaultCellStyle.Font = New Font("Meiryo", 10, FontStyle.Bold) ' フォント変更
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ' ヘッダー中央寄せ
    End With
  End Sub

  Public Sub SelectFree1Master()
    Dim sql As String = String.Empty
    sql = GetAllSelectSql()
    Try
      With tmpDb
        SqlServer.GetResult(tmpDt, sql)

        If tmpDt.Rows.Count = 0 Then
          MessageBox.Show("フリー１マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
          WriteDetail(tmpDt, Free1Detail)
        End If
      End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
    End Try

    If Free1Detail.Rows.Count > 0 Then
      Free1Detail.Rows(0).Selected = True
    End If
  End Sub
  Private Function GetAllSelectSql() As String
    Dim sql As String = String.Empty

    sql &= " SELECT"
    sql &= "     free1_number,"
    sql &= "     free1_name"
    sql &= " FROM"
    sql &= "     MST_Free1"
    sql &= " ORDER BY"
    sql &= "     free1_number"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Me.Dispose()
  End Sub

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    Form_Free1Detail.InputMode = 1
    Form_Free1Detail.ShowDialog()
  End Sub

  Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
    '詳細画面の項目値セット
    SetListData()
    Form_Free1Detail.InputMode = 2
    Form_Free1Detail.ShowDialog()
  End Sub

  Private Sub SetListData()
    '選択している行の行番号の取得
    Dim i As Integer = Free1Detail.CurrentRow.Index
    Form_Free1Detail.free1CodeTextValue = Free1Detail.Rows(i).Cells(0).Value
    Form_Free1Detail.free1NameTextValue = Free1Detail.Rows(i).Cells(1).Value
  End Sub

  Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
    DeleteFree1Master()
  End Sub

  Private Sub DeleteFree1Master()
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
            SelectFree1Master()
          Else
            ' 削除失敗
            MessageBox.Show("フリー1マスタの削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Dim currentRow As Integer = Free1Detail.SelectedCells(0).RowIndex
    Dim free1Code As Integer = Free1Detail.Rows(currentRow).Cells(0).Value

    sql &= " DELETE"
    sql &= " FROM"
    sql &= "     MST_Free1"
    sql &= " WHERE"
    sql &= "     free1_number = " & free1Code

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub Free1Detail_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Free1Detail.CellDoubleClick
    '詳細画面の項目値セット
    SetListData()
    Form_Free1Detail.InputMode = 2
    Form_Free1Detail.ShowDialog()
  End Sub

  Private Sub Form_Free1List_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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