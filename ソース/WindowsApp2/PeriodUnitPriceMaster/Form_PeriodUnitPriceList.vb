Imports Common
Imports Common.ClsFunction
Public Class Form_PeriodUnitPriceList
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
  Private Sub Form_PeriodUnitPriceList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "期間別単価マスタ一覧" & " ( " & updateTime & " ) "
    Me.KeyPreview = True
    FormBorderStyle = FormBorderStyle.FixedSingle

    SelectPeriodUnitPriceMaster()
  End Sub

  Public Sub SelectPeriodUnitPriceMaster()
    Dim sql As String = String.Empty
    sql = GetSelectSql()
    Try
      With tmpDb
        SqlServer.GetResult(tmpDt, sql)

        If tmpDt.Rows.Count = 0 Then
          StartDateText.Text = ""
          EndDateText.Text = ""
          PeriodRateText.Text = ""
          RegularUnitPriceText.Text = ""
        Else
          Dim StartDateTxt As DateTime = tmpDt.Rows(0)("START_DATE")
          StartDateText.Text = StartDateTxt.ToString("yyyy年 M月 d日")
          Dim EndDateTxt As DateTime = tmpDt.Rows(0)("END_DATE")
          EndDateText.Text = EndDateTxt.ToString("yyyy年 M月 d日")
          Dim PeriodUnitPriceTxt As Integer = tmpDt.Rows(0)("PERIOD_UNIT_PRICE")
          PeriodRateText.Text = PeriodUnitPriceTxt
          Dim RegularUnitPriceTxt As Integer = tmpDt.Rows(0)("REGULAR_UNIT_PRICE")
          RegularUnitPriceText.Text = RegularUnitPriceTxt
        End If
      End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
    End Try
  End Sub

  Private Function GetSelectSql() As String

    Dim sql As String = String.Empty

    sql &= " SELECT"
    sql &= "     START_DATE,"
    sql &= "     END_DATE,"
    sql &= "     PERIOD_UNIT_PRICE,"
    sql &= "     REGULAR_UNIT_PRICE"
    sql &= " FROM"
    sql &= "     MST_PeriodUnitPrice"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
    SetListData()
    Form_PeriodUnitPriceDetail.ShowDialog()
  End Sub

  Private Sub SetListData()
    With Form_PeriodUnitPriceDetail
      .StartDateTextValue = If(String.IsNullOrWhiteSpace(StartDateText.Text),
                                 Date.Today,
                                 CDate(StartDateText.Text))
      .EndDateTextValue = If(String.IsNullOrWhiteSpace(EndDateText.Text),
                               Date.Today,
                               CDate(EndDateText.Text))

      .PeriodRateTextValue = If(String.IsNullOrWhiteSpace(PeriodRateText.Text),
                                  0,
                                  CInt(PeriodRateText.Text))
      .RegularUnitPriceTextValue = If(String.IsNullOrWhiteSpace(RegularUnitPriceText.Text),
                                        0,
                                        CInt(RegularUnitPriceText.Text))
    End With
  End Sub



  Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
    DeletePeriodUnitPriceMaster
  End Sub

  Private Sub DeletePeriodUnitPriceMaster()
    Dim sql As String = String.Empty
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
            SelectPeriodUnitPriceMaster()
          Else
            ' 削除失敗
            MessageBox.Show("期間別単価マスタの削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    sql &= " DELETE FROM MST_PeriodUnitPrice"

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function


  Private Sub Form_PeriodUnitPriceList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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