Imports Common
Imports Common.ClsFunction
Public Class Form_ItemDetail

  Public CallCodeDigits As Integer = ReadSettingIniFile("ITEM_DIGITS", "VALUE")

  '新規:１ 、変更:２
  Public InputMode As Integer

  Public CallCodeTextValue As String
  Public ItemNoTextValue As String
  Public ItemNameTextValue As String


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
  Private Sub Form_ItemDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "商品マスタ詳細" & " ( " & updateTime & " ) "

    Me.KeyPreview = True

    FormBorderStyle = FormBorderStyle.FixedSingle
    SetInitialProperty()
  End Sub

  Private Sub SetInitialProperty()
    Select Case InputMode
      Case 1
        ClearTextBox(Me)
        CallCodeText.Enabled = True
      Case 2
        CallCodeText.Enabled = False

        CallCodeText.Text = CallCodeTextValue
        ItemNoText.Text = ItemNoTextValue
        ItemNameText.Text = ItemNameTextValue
    End Select
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Me.Dispose()
  End Sub

  Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
    Select Case InputMode
      Case 1
        If CheckValue() = False Then
          Exit Sub
        End If
        '新規登録メソッド呼出し
        InsertItemMaster()
      Case 2
        '更新メソッド呼出し
        UpdateItemMaster()
    End Select
  End Sub

  Function CheckValue() As Boolean
    Dim errorMessages As New List(Of String)

    ' 必須項目チェック
    If String.IsNullOrEmpty(CallCodeText.Text) Then
      errorMessages.Add("呼出コードを入力してください。")
      CallCodeText.Focus()
    ElseIf CallCodeText.Text = "000000" Then
      errorMessages.Add("呼出コードに000000は登録できません。")
      CallCodeText.Focus()
    End If

    If String.IsNullOrEmpty(ItemNoText.Text) Then
      errorMessages.Add("品番を入力してください。")
      ItemNoText.Focus()
    End If
    If String.IsNullOrEmpty(ItemNameText.Text) Then
      errorMessages.Add("品名を入力してください。")
      ItemNameText.Focus()
    End If

    ' 必須項目のエラーがある場合、まとめてメッセージを表示
    If errorMessages.Count > 0 Then
      MessageBox.Show(String.Join(vbCrLf, errorMessages), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)

      ' 最初のエラー項目にフォーカスを設定
      If String.IsNullOrEmpty(CallCodeText.Text) Then
        CallCodeText.Focus()
      ElseIf String.IsNullOrEmpty(ItemNoText.Text) Then
        ItemNoText.Focus()
      ElseIf String.IsNullOrEmpty(ItemNameText.Text) Then
        ItemNameText.Focus()
      End If

      Return False
    End If

    ' 重複チェック
    If Form_ItemList.ItemDetail.Rows.Count > 0 Then
      For Each row As DataGridViewRow In Form_ItemList.ItemDetail.Rows
        If CallCodeText.Text.Equals(row.Cells(0).Value?.ToString()) Then
          MessageBox.Show("既に登録されている商品コードです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
          CallCodeText.Focus()
          Return False
        End If
      Next
    End If

    Return True
  End Function

  Private Sub InsertItemMaster()
    Dim sql As String = String.Empty
    With tmpDb
      Try
        sql = GetInsertSql()
        Dim confirmation As String
        confirmation = MessageBox.Show("登録します。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          ' SQL実行結果が1件か？
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show("登録処理完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' 一覧画面データ更新
            Form_ItemList.SelectItemMaster()

            Close()
          Else
            ' 登録失敗
            MessageBox.Show("商品マスタの登録に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

  Private Function GetInsertSql() As String
    Dim sql As String = String.Empty
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    ' 必須項目（入力必須）
    Dim callCode As String = CallCodeText.Text
    Dim itemNoTxt As String = ItemNoText.Text
    Dim itemNameTxt As String = ItemNameText.Text


    sql &= " INSERT INTO MST_Item ("
    sql &= "     call_code,"
    sql &= "     item_number,"
    sql &= "     item_name,"
    sql &= "     create_date,"
    sql &= "     update_date"
    sql &= " ) VALUES ("
    sql &= "     '" & callCode & "',"
    sql &= "     '" & itemNoTxt & "',"
    sql &= "     '" & itemNameTxt & "',"
    sql &= "     '" & tmpdate & "',"
    sql &= "     '" & tmpdate & "'"
    sql &= " )"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  ' NULL許容の値をフォーマット
  Private Function FormatValue(value As Object) As String
    If value Is Nothing OrElse value.ToString() = "" Then
      Return "NULL"
    ElseIf TypeOf value Is String Then
      Return "'" & value & "'"
    Else
      Return value.ToString()
    End If
  End Function
  Private Sub UpdateItemMaster()
    Dim sql As String = String.Empty
    With tmpDb
      Try
        sql = GetUpdateSql()
        Dim confirmation As String
        confirmation = MessageBox.Show("更新します。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          ' SQL実行結果が1件か？
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show("更新処理完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim CurrentRow As Integer = Form_ItemList.ItemDetail.CurrentRow.Index
            Form_ItemList.SelectItemMaster()
            Form_ItemList.ItemDetail.Rows(CurrentRow).Selected = True
            Form_ItemList.ItemDetail.FirstDisplayedScrollingRowIndex = CurrentRow
            '選択している行の行番号の取得
            Form_ItemList.ItemDetail.CurrentCell = Form_ItemList.ItemDetail.Rows(CurrentRow).Cells(0)
            Close()
          Else
            ' 更新失敗
            MessageBox.Show("商品マスタの更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
  Private Function GetUpdateSql() As String
    Dim sql As String = String.Empty
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    ' 各フィールドの型に応じた変換
    Dim callCode As String = CallCodeText.Text
    Dim itemNoTxt As String = ItemNoText.Text
    Dim itemNameTxt As String = ItemNameText.Text
    sql &= " UPDATE MST_Item"
    sql &= "    SET item_number =  '" & itemNoTxt & "',"
    sql &= "        item_name =  '" & itemNameTxt & "',"
    sql &= "        update_date =  '" & tmpdate & "'"
    sql &= " WHERE  call_code =  '" & callCode & "'"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub CallCodeText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CallCodeText.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub UnitWeightText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub SafetyFactorText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub TargetQtyText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub UpperLimitText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub
  Private Sub StandardText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub
  Private Sub LowerLimitText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub
  Private Sub SubtotalTargetQtyText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub SubtotalTargetCntText_KeyPress(sender As Object, e As KeyPressEventArgs)
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub
  Private Sub CallCodeText_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CallCodeText.Validating
    If String.IsNullOrEmpty(CallCodeText.Text) = False Then
      CallCodeText.Text = CallCodeText.Text.PadLeft(CallCodeDigits, "0"c)
    End If
  End Sub
  Private Sub Form_ItemDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F5
        OkButton.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub
End Class