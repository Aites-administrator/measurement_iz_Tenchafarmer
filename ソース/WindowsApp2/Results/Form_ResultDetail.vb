Imports Common
Imports Common.ClsFunction
Public Class Form_ResultDetail
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
    SetInitialProperty()
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Me.Dispose()
  End Sub

  Private Sub SetMstComboBox()
    staffNumberComboBox.Items.Clear()

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

  Private Sub SetPackingComboBox()

  End Sub
  Private Sub SetInitialProperty()

    additionDateText.Enabled = False
    additionTimeText.Enabled = False
    weightText.Enabled = False

    additionDateText.Text = additionDateTxTValue
    additionTimeText.Text = additionTimeTxTValue
    weightText.Text = weightTxTValue.ToString() & " kg"
    staffNumberComboBox.SelectedItem = staffNumberTxTValue
    staffNameText.Text = staffNameTxTValue
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
    UpdateResults()
  End Sub

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
            MessageBox.Show("担当者が変更になりました。" & vbCrLf &
                        "茶摘日報（個人別）から出力してください。",
                        "お知らせ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            DeleteTrnWorkerSummary()
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

    Dim trimmedStaffNumber As String = staffNumberComboBox.Text.TrimStart("0"c)

    sql &= "  UPDATE TRN_Results "
    sql &= "  SET "
    sql &= "      weight = '" & weightTxTValue.ToString() & "'"
    sql &= "     ,staff_number = '" & trimmedStaffNumber & "'"
    sql &= "     ,staff_name = '" & staffNameText.Text & "'"
    sql &= "  WHERE "
    sql &= "      addition_date = '" & additionDateText.Text & "'"
    sql &= "      AND addition_time = '" & additionTimeText.Text & "'"

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

  Private Sub DeleteTrnWorkerSummary()
    Dim sql As String = String.Empty
    With tmpDb
      Try
        sql = GetDeleteSql()
        If .Execute(sql) <> 0 Then
          .TrnCommit()
        End If
      Catch ex As Exception
        Call ComWriteErrLog([GetType]().Name,
                                      System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
        Throw New Exception(ex.Message)
      End Try
    End With
  End Sub

  Private Function GetDeleteSql() As String
    Dim sql As String = String.Empty

    sql &= " DELETE FROM TRN_WorkerSummary "

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