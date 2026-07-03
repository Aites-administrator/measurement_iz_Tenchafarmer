Imports Common
Imports Common.ClsFunction

Public Class Form_RainDayList
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

  Private Sub Form_RainDayList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "雨休み一覧" & " ( " & updateTime & " ) "
    Me.KeyPreview = True
    RainDayDetail.RowHeadersVisible = False
    FormBorderStyle = FormBorderStyle.FixedSingle

    RainDayDetail.AllowUserToAddRows = False

    RainDayDetail.ColumnCount = 2

    ' 残りのヘッダーテキストを設定
    RainDayDetail.Columns(0).HeaderText = "日付"
    RainDayDetail.Columns(1).HeaderText = "曜日"


    ' カラムの幅指定
    RainDayDetail.Columns(0).Width = 200
    RainDayDetail.Columns(1).Width = 200

    'カラムの整列設定
    For i As Integer = 0 To 1
      RainDayDetail.Columns(i).DefaultCellStyle.Alignment =
        DataGridViewContentAlignment.MiddleCenter
    Next

    'ヘッダーの整列設定
    For i As Integer = 0 To 1
      RainDayDetail.Columns(i).HeaderCell.Style.Alignment =
        DataGridViewContentAlignment.MiddleCenter
    Next

    SelectRainDay()

    ' 選択モードを全カラム選択に設定
    RainDayDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    If RainDayDetail.Rows.Count > 0 Then
      RainDayDetail.CurrentCell = RainDayDetail.Rows(0).Cells(0)
      RainDayDetail.Rows(0).Selected = True
    End If
  End Sub

  Public Sub SelectRainDay()
    Dim sql As String = String.Empty
    sql = GetAllSelectSql()
    Try
      With tmpDb
        SqlServer.GetResult(tmpDt, sql)

        If tmpDt.Rows.Count = 0 Then
          'MessageBox.Show("雨の日が登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
          WriteDetail(tmpDt, RainDayDetail)
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
    If RainDayDetail.Rows.Count > 0 Then
      RainDayDetail.CurrentCell = RainDayDetail.Rows(0).Cells(0)
      RainDayDetail.Rows(0).Selected = True
    End If

    CustomizeDataGridViewHeader() ' ヘッダーのデザイン変更
  End Sub
  Private Function GetAllSelectSql() As String
    Dim sql As String = String.Empty

    sql &= " SELECT"
    sql &= "     rain_date,"
    sql &= "     Format(rain_date, 'dddd', 'ja-JP') AS 曜日"
    sql &= " FROM"
    sql &= "     TRN_RainDay"
    sql &= " ORDER BY"
    sql &= "     rain_date"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Sub CustomizeDataGridViewHeader()
    With RainDayDetail
      ' ヘッダーの背景色を変更
      .EnableHeadersVisualStyles = False ' デフォルトの Windows スタイルを無効化
      .ColumnHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow ' ヘッダーの背景色
      .ColumnHeadersDefaultCellStyle.ForeColor = Color.Black ' ヘッダーの文字色
      .ColumnHeadersDefaultCellStyle.Font = New Font("Meiryo", 10, FontStyle.Bold) ' フォント変更
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ' ヘッダー中央寄せ
    End With
  End Sub

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    Form_RainDayDetail.ShowDialog()
  End Sub

  Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
    DeleteRainDay()
  End Sub
  Private Sub DeleteRainDay()
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
            SelectRainDay()
          Else
            ' 削除失敗
            MessageBox.Show("削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Dim currentRow As Integer = RainDayDetail.SelectedCells(0).RowIndex
    Dim rainDate As Date = RainDayDetail.Rows(currentRow).Cells(0).Value

    sql &= " DELETE"
    sql &= " FROM"
    sql &= "     TRN_RainDay"
    sql &= " WHERE rain_date = '" & rainDate.ToString("yyyy-MM-dd") & "'"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub Form_ManufacturerList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F5
        CreateButton.PerformClick()
      Case Keys.F7
        DeleteButton.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub
End Class