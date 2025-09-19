Imports Common
Imports Common.ClsFunction
Public Class Form_PeriodUnitPriceDetail

  Public StartDateTextValue As DateTime
  Public EndDateTextValue As DateTime
  Public PeriodRateTextValue As Integer
  Public RegularUnitPriceTextValue As Integer

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
  Private Sub Form_PeriodUnitPriceDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "期間別単価マスタ詳細" & " ( " & updateTime & " ) "
    Me.KeyPreview = True
    FormBorderStyle = FormBorderStyle.FixedSingle
    SetInitialProperty()
  End Sub
  Private Sub SetInitialProperty()
    StartDateText.Text = StartDateTextValue
    EndDateText.Text = EndDateTextValue
    PeriodRateText.Text = PeriodRateTextValue
    RegularUnitPriceText.Text = RegularUnitPriceTextValue
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
    If CheckValue() = False Then
      Exit Sub
    End If
    InsertPeriodUnitPriceMaster()
  End Sub

  Private Sub InsertPeriodUnitPriceMaster()
    Dim sql As String = String.Empty
    With tmpDb
      Try
        sql = GetDeleteInsertSql()

        Dim confirmation As DialogResult
        confirmation = MessageBox.Show("登録します。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmation = DialogResult.Yes Then
          .Execute(sql)

          .TrnCommit()
          MessageBox.Show("登録処理完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

          Form_PeriodUnitPriceList.SelectPeriodUnitPriceMaster()
          Close()
        End If

      Catch ex As Exception
        .TrnRollBack() ' 
        ComWriteErrLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
        MessageBox.Show("期間別単価マスタの登録に失敗しました。" & vbCrLf & ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try
    End With
  End Sub

  Private Function CheckValue() As Boolean
    Dim errorMessages As New List(Of String)
    Dim focusControl As Control = Nothing

    ' ① 必須チェック（空欄）
    If String.IsNullOrWhiteSpace(PeriodRateText.Text) Then
      errorMessages.Add("期間単価は必須です。")
      If focusControl Is Nothing Then focusControl = PeriodRateText
    ElseIf CInt(PeriodRateText.Text) = 0 Then
      errorMessages.Add("期間単価に0は入力できません。")
      If focusControl Is Nothing Then focusControl = PeriodRateText
    End If

    If String.IsNullOrWhiteSpace(RegularUnitPriceText.Text) Then
      errorMessages.Add("通常単価は必須です。")
      If focusControl Is Nothing Then focusControl = RegularUnitPriceText
    ElseIf CInt(RegularUnitPriceText.Text) = 0 Then
      errorMessages.Add("通常単価に0は入力できません。")
      If focusControl Is Nothing Then focusControl = RegularUnitPriceText
    End If

    ' ② 開始日 <= 終了日 チェック
    If StartDateText.Value > EndDateText.Value Then
      errorMessages.Add("開始日は終了日以前の日付を指定してください。")
      If focusControl Is Nothing Then focusControl = StartDateText
    End If

    ' エラーがある場合
    If errorMessages.Count > 0 Then
      MessageBox.Show(String.Join(vbCrLf, errorMessages), "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
      If focusControl IsNot Nothing Then
        focusControl.Focus()
      End If
      Return False
    End If

    Return True
  End Function




  Private Function GetDeleteInsertSql() As String
    Dim sql As New System.Text.StringBuilder()
    Dim StartDate As DateTime = StartDateText.Text
    Dim EndDate As DateTime = EndDateText.Text
    Dim PeriodRate As Integer = Integer.Parse(PeriodRateText.Text)
    Dim RegularUnitPrice As Integer = Integer.Parse(RegularUnitPriceText.Text)
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    ' 1件存在すれば削除
    sql.AppendLine("IF EXISTS (SELECT 1 FROM MST_PeriodUnitPrice)")
    sql.AppendLine("BEGIN")
    sql.AppendLine("    DELETE FROM MST_PeriodUnitPrice")
    sql.AppendLine("END")

    ' 追加処理
    sql.AppendLine("INSERT INTO MST_PeriodUnitPrice (")
    sql.AppendLine("    START_DATE,")
    sql.AppendLine("    END_DATE,")
    sql.AppendLine("    REGULAR_UNIT_PRICE,")
    sql.AppendLine("    PERIOD_UNIT_PRICE,")
    sql.AppendLine("    CREATE_DATE,")
    sql.AppendLine("    UPDATE_DATE")
    sql.AppendLine(") VALUES (")
    sql.AppendLine("    '" & StartDate.ToString("yyyy-MM-dd") & "',")
    sql.AppendLine("    '" & EndDate.ToString("yyyy-MM-dd") & "',")
    sql.AppendLine("    " & RegularUnitPrice & ",")
    sql.AppendLine("    " & PeriodRate & ",")
    sql.AppendLine("    '" & tmpdate.ToString("yyyy-MM-dd HH:mm:ss") & "',")
    sql.AppendLine("    '" & tmpdate.ToString("yyyy-MM-dd HH:mm:ss") & "'")
    sql.AppendLine(")")

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql.ToString())
    Return sql.ToString()

  End Function

  Private Sub RegularUnitPriceText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RegularUnitPriceText.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub PeriodRateText_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PeriodRateText.KeyPress
    'キーが [0]～[9] または [BackSpace] 以外の場合イベントをキャンセル
    If Not (("0"c <= e.KeyChar And e.KeyChar <= "9"c) Or e.KeyChar = ControlChars.Back) Then
      e.Handled = True
    End If
  End Sub

  Private Sub Form_PeriodUnitPriceDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F5
        OkButton.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub
End Class
