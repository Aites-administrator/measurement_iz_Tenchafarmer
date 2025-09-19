Imports Common
Imports Common.ClsFunction
Public Class Form_ItemDetail

  Public CallCodeDigits As Integer = ReadSettingIniFile("ITEM_DIGITS", "VALUE")

  '新規:１ 、変更:２
  Public InputMode As Integer

  Public CallCodeTextValue As String
  Public ItemNoTextValue As String
  Public ItemNameTextValue As String
  Public UpperLimitTextValue As String
  Public StandardValueTextValue As String
  Public LowerLimitTextValue As String

  Public SubtotalTargetQtyTextValue As String
  Public SubtotalTargetCntTextValue As String
  Public SubtotalTargetUnitTextValue As String

  Public WeightTextValue As String
  Public WeightUnitTextValue As String
  Public UpperWeightTextValue As Decimal
  Public UpperWeightUnitTextValue As String
  Public LowerWeightTextValue As Decimal
  Public LowerWeightUnitTextValue As String

  Public PackingTextValue As Decimal
  Public PackingUnitTextValue As String

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

    SetPackingComboBox()
    SetUnitComboBox()

    SetInitialProperty()
  End Sub
  Private Sub SetPackingComboBox()
    Dim PackingData As DataTable = GetPackingMasterData()

    PackingComboBox.Items.Clear()

    If PackingData.Rows.Count = 0 Then
      MessageBox.Show("風袋マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Else
      For Each row As DataRow In PackingData.Rows
        PackingComboBox.Items.Add(row(0))
      Next
    End If
    PackingComboBox.SelectedIndex = 0
  End Sub
  Private Function GetPackingMasterData() As DataTable
    Dim PackingData As New DataTable
    Try
      With tmpDb
        SqlServer.GetResult(PackingData, GetSelectPackingMaster)
      End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    End Try

    Return PackingData
  End Function
  Private Function GetSelectPackingMaster() As String
    Dim sql As String = String.Empty
    'sql &= " SELECT PackingNo"
    sql &= " SELECT PackingWeight"
    sql &= " FROM MST_Packing "
    sql &= " ORDER BY PackingNo"
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Sub SetUnitComboBox()
    ' 共通のアイテムリスト
    Dim items As String() = {"mg", "g", "kg", "t"}

    ' コンボボックスのアイテムをクリア
    UpperWeightUnitComboBox.Items.Clear()
    WeightComboBox.Items.Clear()
    LowerWeightUnitComboBox.Items.Clear()
    PackingUnitComboBox.Items.Clear()
    SubtotalTargetUnitComboBox.Items.Clear()

    ' アイテムをコンボボックスに追加
    For Each item As String In items
      UpperWeightUnitComboBox.Items.Add(item)
      WeightComboBox.Items.Add(item)
      LowerWeightUnitComboBox.Items.Add(item)
      PackingUnitComboBox.Items.Add(item)
      SubtotalTargetUnitComboBox.Items.Add(item)
    Next

    ' 初期値を "g" に設定
    UpperWeightUnitComboBox.SelectedItem = "g"
    WeightComboBox.SelectedItem = "g"
    LowerWeightUnitComboBox.SelectedItem = "g"
    PackingUnitComboBox.SelectedItem = "g"
    SubtotalTargetUnitComboBox.SelectedItem = "g"

    'PackingComboBox.Items.Add(10.0)
  End Sub

  Private Sub SetInitialProperty()
    UpperWeightUnitComboBox.Enabled = True
    WeightComboBox.Enabled = True
    LowerWeightUnitComboBox.Enabled = True
    PackingComboBox.Enabled = True
    PackingUnitComboBox.Enabled = True
    SubtotalTargetUnitComboBox.Enabled = True

    Select Case InputMode
      Case 1
        ClearTextBox(Me)
        CallCodeText.Enabled = True
      Case 2
        CallCodeText.Enabled = False

        CallCodeText.Text = CallCodeTextValue
        ItemNoText.Text = ItemNoTextValue
        ItemNameText.Text = ItemNameTextValue

        SubtotalTargetQtyText.Text = SubtotalTargetQtyTextValue
        SubtotalTargetCntText.Text = SubtotalTargetCntTextValue
        SubtotalTargetUnitComboBox.SelectedItem = SubtotalTargetUnitTextValue

        UpperWeightText.Text = WeightTextValue
        WeightText.Text = UpperWeightTextValue
        LowerWeightText.Text = LowerWeightTextValue

        UpperWeightUnitComboBox.SelectedItem = WeightUnitTextValue
        WeightComboBox.SelectedItem = UpperWeightUnitTextValue
        LowerWeightUnitComboBox.SelectedItem = LowerWeightUnitTextValue

        PackingComboBox.SelectedItem = PackingTextValue
        PackingUnitComboBox.SelectedItem = PackingUnitTextValue
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
    'Dim rowSelectionCode As String = String.Empty
    'rowSelectionCode = CodeText.Text
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
            ' 一覧画面件数更新
            'Form_ItemList.GetRowCount(Form_ItemList.DeletedDisplayCheckBox.Checked)

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

    ' オプション項目（NULL許容）
    Dim subtotalTargetQtyInt As Integer
    Dim subtotalTargetCntInt As Integer
    Dim packingDecimal As Decimal
    Dim upperWeightDecimal As Decimal
    Dim weightDecimal As Decimal
    Dim lowerWeightDecimal As Decimal

    Integer.TryParse(SubtotalTargetQtyText.Text, subtotalTargetQtyInt)
    Integer.TryParse(SubtotalTargetCntText.Text, subtotalTargetCntInt)
    Decimal.TryParse(PackingComboBox.SelectedItem?.ToString(), packingDecimal)
    Decimal.TryParse(UpperWeightText.Text, upperWeightDecimal)
    Decimal.TryParse(WeightText.Text, weightDecimal)
    Decimal.TryParse(LowerWeightText.Text, lowerWeightDecimal)

    Dim subtotalTargetUnit As String = If(SubtotalTargetUnitComboBox.SelectedItem IsNot Nothing, SubtotalTargetUnitComboBox.SelectedItem.ToString(), "")
    Dim upperWeightUnit As String = If(WeightComboBox.SelectedItem IsNot Nothing, WeightComboBox.SelectedItem.ToString(), "")
    Dim weightUnit As String = If(UpperWeightUnitComboBox.SelectedItem IsNot Nothing, UpperWeightUnitComboBox.SelectedItem.ToString(), "")
    Dim lowerWeightUnit As String = If(LowerWeightUnitComboBox.SelectedItem IsNot Nothing, LowerWeightUnitComboBox.SelectedItem.ToString(), "")
    Dim packingUnit As String = If(PackingUnitComboBox.SelectedItem IsNot Nothing, PackingUnitComboBox.SelectedItem.ToString(), "")

    sql &= " INSERT INTO MST_Item ("
    sql &= "     call_code,"
    sql &= "     item_number,"
    sql &= "     item_name,"
    sql &= "     packing_bag,"
    sql &= "     packing_bag_unit,"
    sql &= "     upper_limit,"
    sql &= "     upper_limit_unit,"
    sql &= "     reference_value,"
    sql &= "     reference_value_unit,"
    sql &= "     lower_limit,"
    sql &= "     lower_limit_unit,"
    sql &= "     subtotal_target_value,"
    sql &= "     subtotal_target_value_unit,"
    sql &= "     subtotal_target_count,"
    sql &= "     create_date,"
    sql &= "     update_date"
    sql &= " ) VALUES ("
    sql &= "     '" & callCode & "',"
    sql &= "     '" & itemNoTxt & "',"
    sql &= "     '" & itemNameTxt & "',"
    sql &= "     " & FormatValue(packingDecimal) & ","
    sql &= "     " & FormatValue(packingUnit) & ","
    sql &= "     " & FormatValue(upperWeightDecimal) & ","
    sql &= "     " & FormatValue(upperWeightUnit) & ","
    sql &= "     " & FormatValue(weightDecimal) & ","
    sql &= "     " & FormatValue(weightUnit) & ","
    sql &= "     " & FormatValue(lowerWeightDecimal) & ","
    sql &= "     " & FormatValue(lowerWeightUnit) & ","
    sql &= "     " & FormatValue(subtotalTargetQtyInt) & ","
    sql &= "     " & FormatValue(subtotalTargetUnit) & ","
    sql &= "     " & FormatValue(subtotalTargetCntInt) & ","
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
    'Dim rowSelectionCode As String = String.Empty
    'rowSelectionCode = CodeText.Text
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

    Dim subtotalTargetQtyInt As Integer = CInt(SubtotalTargetQtyText.Text)
    Dim subtotalTargetUnit As String = SubtotalTargetUnitComboBox.SelectedItem.ToString()
    Dim subtotalTargetCntInt As Integer = CInt(SubtotalTargetCntText.Text)

    Dim upperWeightDecimal As Decimal = CDec(UpperWeightText.Text)
    Dim upperWeightUnit As String = WeightComboBox.SelectedItem.ToString()
    Dim weightDecimal As Decimal = CInt(WeightText.Text)
    Dim weightUnit As String = UpperWeightUnitComboBox.SelectedItem.ToString()
    Dim lowerWeightDecimal As Decimal = CDec(LowerWeightText.Text)
    Dim lowerWeightUnit As String = LowerWeightUnitComboBox.SelectedItem.ToString()

    Dim packingDecimal As Decimal = CDec(PackingComboBox.SelectedItem.ToString())
    Dim packingUnit As String = PackingUnitComboBox.SelectedItem.ToString()

    sql &= " UPDATE MST_Item"
    sql &= "    SET item_number =  '" & itemNoTxt & "',"
    sql &= "        item_name =  '" & itemNameTxt & "',"
    sql &= "        subtotal_target_value = '" & subtotalTargetQtyInt & "',"
    sql &= "        subtotal_target_value_unit =  '" & subtotalTargetUnit & "',"
    sql &= "        subtotal_target_count = '" & subtotalTargetCntInt & "',"
    sql &= "        upper_limit = '" & upperWeightDecimal & "',"
    sql &= "        upper_limit_unit =  '" & upperWeightUnit & "',"
    sql &= "        reference_value =  '" & weightDecimal & "',"
    sql &= "        reference_value_unit =  '" & weightUnit & "',"
    sql &= "        lower_limit =  '" & lowerWeightDecimal & "',"
    sql &= "        lower_limit_unit =  '" & lowerWeightUnit & "',"
    sql &= "        packing_bag =  '" & packingDecimal & "',"
    sql &= "        packing_bag_unit =  '" & packingUnit & "',"
    sql &= "        update_date =  '" & tmpdate & "'"
    sql &= " WHERE  call_code =  '" & callCode & "'"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub UpperUnitWeightText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles WeightText.KeyPress
    ' テキストボックスに入力された文字が数字、小数点、バックスペースでない場合は入力を無効化する
    If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "." OrElse Char.IsControl(e.KeyChar)) Then
      e.Handled = True
    End If

    ' 小数点の場合、既に小数点が含まれているか、すでに小数点が含まれていて、小数点が2つ以上入力された場合は無効化する
    If e.KeyChar = "." Then
      If WeightText.Text.Contains(".") OrElse WeightText.Text.Length = 0 Then
        e.Handled = True
      End If
    End If

    ' 入力された文字数が7桁以上の場合は無効化する
    If WeightText.Text.Replace(".", "").Length >= 7 AndAlso e.KeyChar <> ControlChars.Back Then
      e.Handled = True
    End If
  End Sub

  Private Sub LowerUnitWeightText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles LowerWeightText.KeyPress
    ' テキストボックスに入力された文字が数字、小数点、バックスペースでない場合は入力を無効化する
    If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "." OrElse Char.IsControl(e.KeyChar)) Then
      e.Handled = True
    End If

    ' 小数点の場合、既に小数点が含まれているか、すでに小数点が含まれていて、小数点が2つ以上入力された場合は無効化する
    If e.KeyChar = "." Then
      If WeightText.Text.Contains(".") OrElse WeightText.Text.Length = 0 Then
        e.Handled = True
      End If
    End If

    ' 入力された文字数が7桁以上の場合は無効化する
    If WeightText.Text.Replace(".", "").Length >= 7 AndAlso e.KeyChar <> ControlChars.Back Then
      e.Handled = True
    End If
  End Sub

  Private Sub CallCodeText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CallCodeText.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub UnitWeightText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles UpperWeightText.KeyPress
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

  Private Sub SubtotalTargetQtyText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SubtotalTargetQtyText.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub SubtotalTargetCntText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles SubtotalTargetCntText.KeyPress
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