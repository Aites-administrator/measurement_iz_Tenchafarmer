Imports Common
Imports Common.ClsFunction
Public Class Form_ResultDetail
  '新規:１ 、変更:２
  Public InputMode As Integer

  'Public additionDateTxTValue As String
  'Public additionTimeTxTValue As String
  'Public terminalNumberTxTValue As String
  'Public callCodeTxTValue As String
  'Public itemNoTxTValue As String
  'Public itemNameTxTValue As String

  'Public packingTxTValue As Decimal
  'Public packingUnitTxTValue As String
  'Public packing1WeightTxTValue As Decimal
  'Public packing1WeightUnitTxTValue As String
  'Public packing2WeightTxTValue As Decimal
  'Public packing2WeightUnitTxTValue As String
  'Public packing2MultiplicationTxTValue As Decimal
  'Public packing1NumberTxTValue As String
  'Public packing1NameTxTValue As String
  'Public packing2NumberTxTValue As String
  'Public packing2NameTxTValue As String

  'Public free1NumberTxTValue As Integer
  'Public free1NameTxTValue As String
  'Public free2NumberTxTValue As Integer
  'Public free2NameTxTValue As String
  'Public free3NumberTxTValue As Integer
  'Public free3NameTxTValue As String
  'Public free4NumberTxTValue As Integer
  'Public free4NameTxTValue As String
  'Public free5NumberTxTValue As Integer
  'Public free5NameTxTValue As String

  'Public manufacturerCodeTxTValue As Integer
  'Public manufacturerNameTxTValue As String
  'Public staffNumberTxTValue As Integer
  'Public staffNameTxTValue As String
  'Public lot1TxTValue As String
  'Public lot2TxTValue As String
  'Public classificationTxTValue As String

  'Public weightTxTValue As Decimal
  'Public weightUnitTxTValue As String
  'Public grossWeightTxTValue As Decimal
  'Public grossWeightUnitTxTValue As String
  'Public temperatureTxTValue As String
  'Public temperatureUnitTxTValue As String

  'Public processingDateTxTValue As String
  'Public processingTimeTxTValue As String
  'Public effectiveDateTxTValue As String
  'Public effectiveTimeTxTValue As String
  'Public workOrderNumberTxTValue As String
  'Public detailNumberTxTValue As String
  'Public instructionQtyTxTValue As Integer
  'Public actualQtyTxTValue As Integer
  'Public workOrderNameTxTValue As String

  Public additionDateTxTValue As String
  Public additionTimeTxTValue As String
  Public terminalNumberTxTValue As String
  Public callCodeTxTValue As String
  Public itemNoTxTValue As String
  Public itemNameTxTValue As String

  Public packingTxTValue As String
  Public packingUnitTxTValue As String
  Public packing1WeightTxTValue As String
  Public packing1WeightUnitTxTValue As String
  Public packing2WeightTxTValue As String
  Public packing2WeightUnitTxTValue As String
  Public packing2MultiplicationTxTValue As String
  Public packing1NumberTxTValue As String
  Public packing1NameTxTValue As String
  Public packing2NumberTxTValue As String
  Public packing2NameTxTValue As String

  Public free1NumberTxTValue As String
  Public free1NameTxTValue As String
  Public free2NumberTxTValue As String
  Public free2NameTxTValue As String
  Public free3NumberTxTValue As String
  Public free3NameTxTValue As String
  Public free4NumberTxTValue As String
  Public free4NameTxTValue As String
  Public free5NumberTxTValue As String
  Public free5NameTxTValue As String

  Public manufacturerCodeTxTValue As String
  Public manufacturerNameTxTValue As String
  Public staffNumberTxTValue As String
  Public staffNameTxTValue As String
  Public lot1TxTValue As String
  Public lot2TxTValue As String
  Public classificationTxTValue As String

  Public weightTxTValue As String
  Public weightUnitTxTValue As String
  Public grossWeightTxTValue As String
  Public grossWeightUnitTxTValue As String
  Public temperatureTxTValue As String
  Public temperatureUnitTxTValue As String

  Public processingDateTxTValue As String
  Public processingTimeTxTValue As String
  Public effectiveDateTxTValue As String
  Public effectiveTimeTxTValue As String
  Public workOrderNumberTxTValue As String
  Public detailNumberTxTValue As String
  Public instructionQtyTxTValue As String
  Public actualQtyTxTValue As String
  Public workOrderNameTxTValue As String

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
  Private Sub Form_ResultDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ' フォームの最大化ボタンを無効にする
    MaximizeBox = False

    ' アセンブリの最終更新日時を取得し、フォームのタイトルに表示するテキストを設定
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "実績詳細" & " ( " & updateTime & " ) "

    Me.KeyPreview = True

    FormBorderStyle = FormBorderStyle.FixedSingle

    SetMstComboBox()
    SetUnitComboBox()
    SetInitialProperty()
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Me.Dispose()
  End Sub

  Private Sub SetMstComboBox()
    callCodeComboBox.Items.Clear()
    staffNumberComboBox.Items.Clear()
    ' 商品マスタ
    Dim ItemData As DataTable = GetMasterData("SELECT call_code FROM MST_Item ORDER BY call_code")
    If ItemData.Rows.Count = 0 Then
      MessageBox.Show("商品マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Else
      For Each row As DataRow In ItemData.Rows
        callCodeComboBox.Items.Add(row(0))
      Next
    End If

    ' 担当者
    Dim staffData As DataTable = GetMasterData("SELECT staff_number FROM MST_Staff ORDER BY staff_number")
    If staffData.Rows.Count = 0 Then
      MessageBox.Show("担当者マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Else
      For Each row As DataRow In staffData.Rows
        staffNumberComboBox.Items.Add(row(0))
      Next
    End If
  End Sub

  Private Function GetMasterData(query As String) As DataTable
    Dim data As New DataTable
    Try
      SqlServer.GetResult(data, query)
    Catch ex As Exception
      Call ComWriteErrLog(Me.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    End Try
    Return data
  End Function


  Private Sub SetUnitComboBox()
    ' 共通のアイテムリスト
    Dim items As String() = {"mg", "g", "kg", "t"}

    ' コンボボックスのアイテムをクリア
    weightComboBox.Items.Clear()

    ' アイテムをコンボボックスに追加
    For Each item As String In items
      weightComboBox.Items.Add(item)
    Next
  End Sub
  Private Sub SetPackingComboBox()

  End Sub
  Private Sub SetInitialProperty()
    Select Case InputMode
      Case 1
        ClearTextBox(Me)
        additionDateText.Enabled = True
        additionTimeText.Enabled = True
        callCodeComboBox.Enabled = True
      Case 2
        additionDateText.Enabled = False
        additionTimeText.Enabled = False
        weightText.Enabled = False
        weightComboBox.Enabled = False

        additionDateText.Text = additionDateTxTValue
        additionTimeText.Text = additionTimeTxTValue
        callCodeComboBox.SelectedItem = callCodeTxTValue
        itemNoText.Text = itemNoTxTValue
        itemNameText.Text = itemNameTxTValue
        weightText.Text = weightTxTValue.ToString()
        weightComboBox.SelectedItem = weightUnitTxTValue
        staffNumberComboBox.SelectedItem = staffNumberTxTValue
        staffNameText.Text = staffNameTxTValue



    End Select
  End Sub

  Private Sub staffNumberComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles staffNumberComboBox.SelectedIndexChanged
    Dim selectedOption As String = DirectCast(sender, ComboBox).SelectedItem.ToString()

    SetStaffName(selectedOption)
  End Sub

  Private Sub SetStaffName(selectedOption As String)
    Dim StaffData As DataTable = GetStaffName(selectedOption)

    If StaffData.Rows.Count = 0 Then
      MessageBox.Show("担当者マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Else
      Dim StaffName As String = StaffData.Rows(0)("staff_name").ToString()
      staffNameText.Text = StaffName
    End If
  End Sub

  Private Function GetStaffName(selectedOption As String) As DataTable
    Dim freeData As New DataTable
    Try
      SqlServer.GetResult(freeData, GetSelectStaffNameQuery(selectedOption))
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    End Try

    Return freeData
  End Function

  Private Function GetSelectStaffNameQuery(selectedOption As String) As String
    Dim sql As String = String.Empty
    sql &= " SELECT staff_name "
    sql &= " FROM MST_Staff "
    sql &= " WHERE staff_number = '" & selectedOption & "'"
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
    If Not ValidateInputs() Then
      Return
    End If

    Select Case InputMode
      Case 1
        ' 新規登録メソッド呼び出し
        InsertResults()
      Case 2
        ' 更新メソッド呼び出し
        UpdateResults()
    End Select
  End Sub

  Private Sub InsertResults()
    Dim sql As String = GetInsertSql()

    Dim confirmation As DialogResult
    confirmation = MessageBox.Show("登録します。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

    If confirmation = DialogResult.Yes Then
      If ExecuteAndCommit(sql) Then
        MessageBox.Show("登録処理完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Form_ResultList.SelectResults()
        Close()
      Else
        MessageBox.Show("実績管理の登録に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End If
    End If
  End Sub
  Private Function GetInsertSql() As String
    Dim sql As String = String.Empty
    Dim Tmpdate As DateTime = CDate(ComGetProcTime())

    sql &= "INSERT INTO TRN_Results("
    sql &= "    addition_date,"
    sql &= "    addition_time,"
    sql &= "    terminal_number,"
    sql &= "    call_code,"
    sql &= "    item_number,"
    sql &= "    item_name,"
    sql &= "    weight,"
    sql &= "    weight_unit,"
    sql &= "    staff_number,"
    sql &= "    staff_name,"
    sql &= "    create_date,"
    sql &= "    update_date"
    sql &= ")"
    sql &= "VALUES("
    sql &= "    '" & additionDateText.Text & "',"
    sql &= "    '" & additionTimeText.Text & "',"
    sql &= "    '" & "01" & "',"
    sql &= "    '" & callCodeComboBox.SelectedItem & "',"
    sql &= "    '" & itemNoText.Text & "',"
    sql &= "    '" & itemNameText.Text & "',"
    sql &= "    " & weightText.Text & ","
    sql &= "    '" & weightComboBox.SelectedItem & "',"
    sql &= "    '" & staffNumberComboBox.SelectedItem & "',"
    sql &= "    '" & staffNameText.Text & "',"
    sql &= "    '" & Tmpdate & "',"
    sql &= "    '" & Tmpdate & "'"
    sql &= ")"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub UpdateResults()
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
            Form_ResultList.SelectResults()
            Close()
          Else
            ' 更新失敗
            MessageBox.Show("実績管理の更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    sql &= "  UPDATE TRN_Results "
    sql &= "  SET "
    sql &= "      weight = '" & weightText.Text & "'"
    sql &= "     ,weight_unit = '" & weightComboBox.Text & "'"
    sql &= "     ,staff_number = '" & staffNumberComboBox.Text & "'"
    sql &= "     ,staff_name = '" & staffNameText.Text & "'"
    sql &= "  WHERE "
    sql &= "      addition_date = '" & additionDateText.Text & "'"
    sql &= "      AND addition_time = '" & additionTimeText.Text & "'"
    sql &= "      AND call_code = '" & callCodeComboBox.Text & "'"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
  Private Function ExecuteAndCommit(sql As String) As Boolean
    Try
      With tmpDb
        If .Execute(sql) = 1 Then
          .TrnCommit()
          Return True
        End If
      End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    End Try
    Return False
  End Function
  Private Function ValidateInputs() As Boolean
    Dim isValid As Boolean = True

    ' 日付チェック
    If String.IsNullOrWhiteSpace(additionDateText.Text) Then
      MessageBox.Show("日付を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      additionDateText.Focus()
      isValid = False
    End If

    ' 時刻チェック
    If String.IsNullOrWhiteSpace(additionTimeText.Text) Then
      MessageBox.Show("時刻を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      additionTimeText.Focus()
      isValid = False
    End If

    ' 呼出コードチェック
    If callCodeComboBox.SelectedIndex = -1 Then
      MessageBox.Show("呼出コードを選択してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      callCodeComboBox.Focus()
      isValid = False
    End If

    Return isValid
  End Function

  Private Sub callCodeComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles callCodeComboBox.SelectedIndexChanged
    Dim selectedOption As String = DirectCast(sender, ComboBox).SelectedItem

    SetCallCodeData(selectedOption)
  End Sub

  Private Sub SetCallCodeData(selectedOption As String)
    Dim CallCodeData As DataTable = GetCallCode(selectedOption)

    If CallCodeData.Rows.Count = 0 Then
      MessageBox.Show("商品マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Else
      Dim wItemNoText As String = CallCodeData.Rows(0)("item_number").ToString()
      Dim wItemNameText As String = CallCodeData.Rows(0)("item_name").ToString()
      itemNoText.Text = wItemNoText
      itemNameText.Text = wItemNameText
    End If
  End Sub

  Private Function GetCallCode(selectedOption As String) As DataTable
    Dim freeData As New DataTable
    Try
      SqlServer.GetResult(freeData, GetSelectCallCodeQuery(selectedOption))
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    End Try

    Return freeData
  End Function

  Private Function GetSelectCallCodeQuery(selectedOption As String) As String
    Dim sql As String = String.Empty
    sql &= " SELECT item_number,item_name "
    sql &= " FROM MST_Item "
    sql &= " WHERE call_code = '" & selectedOption & "'"
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub Form_ResultDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F5
        OkButton.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub
End Class