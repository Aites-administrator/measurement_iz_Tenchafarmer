Imports Common
Imports Common.ClsFunction
Public Class Form_RainDayDetail

  Public RainDayValue As DateTime

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
  Private Sub Form_RainDayDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "雨の日詳細" & " ( " & updateTime & " ) "
    Me.KeyPreview = True
    FormBorderStyle = FormBorderStyle.FixedSingle
    SetInitialProperty()

    With RainDayDateTimePicker
      .Format = DateTimePickerFormat.Custom
      .CustomFormat = "yyyy年 MM月dd日 (ddd)" ' 曜日付き
    End With

  End Sub
  Private Sub SetInitialProperty()
    RainDayDateTimePicker.Text = RainDayValue
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
    If CheckValue() = False Then
      Exit Sub
    End If

    InsertRainDay()
  End Sub

  Function CheckValue() As Boolean
    If Form_RainDayList.RainDayDetail.Rows.Count > 0 Then
      Dim selectedDate As Date = RainDayDateTimePicker.Value.Date
      For Each row As DataGridViewRow In Form_RainDayList.RainDayDetail.Rows
        If row.IsNewRow Then Continue For

        Dim cellValue = row.Cells(0).Value
        If cellValue IsNot Nothing AndAlso TypeOf cellValue Is Date Then
          Dim existingDate As Date = CType(cellValue, Date).Date
          If selectedDate = existingDate Then
            MessageBox.Show("既に登録されている日です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RainDayDateTimePicker.Focus()
            Return False
          End If
        ElseIf cellValue IsNot Nothing Then
          Dim parsedDate As Date
          If Date.TryParse(cellValue.ToString(), parsedDate) Then
            If selectedDate = parsedDate.Date Then
              MessageBox.Show("既に登録されている日です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
              RainDayDateTimePicker.Focus()
              Return False
            End If
          End If
        End If
      Next
    End If

    Return True
  End Function


  Private Sub InsertRainDay()
    Dim sql As String = String.Empty
    With tmpDb
      Try
        sql = GetInsertSql()

        Dim confirmation As DialogResult
        confirmation = MessageBox.Show("登録します。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmation = DialogResult.Yes Then
          .Execute(sql)

          .TrnCommit()
          MessageBox.Show("登録処理完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

          Form_RainDayList.SelectRainDay()
          Close()
        End If

      Catch ex As Exception
        .TrnRollBack() ' 
        ComWriteErrLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
        MessageBox.Show("期間別単価マスタの登録に失敗しました。" & vbCrLf & ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try
    End With
  End Sub

  Private Function GetInsertSql() As String
    Dim sql As New System.Text.StringBuilder()
    Dim wkRainDayText As DateTime = RainDayDateTimePicker.Text
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    ' 追加処理
    sql.AppendLine("INSERT INTO TRN_RainDay (")
    sql.AppendLine("    rain_date,")
    sql.AppendLine("    create_date,")
    sql.AppendLine("    update_date")
    sql.AppendLine(") VALUES (")
    sql.AppendLine("    '" & wkRainDayText.ToString("yyyy-MM-dd") & "',")
    sql.AppendLine("    '" & tmpdate.ToString("yyyy-MM-dd HH:mm:ss") & "',")
    sql.AppendLine("    '" & tmpdate.ToString("yyyy-MM-dd HH:mm:ss") & "'")
    sql.AppendLine(")")

    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql.ToString())
    Return sql.ToString()

  End Function

  Private Sub Form_RainDayDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F5
        OkButton.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub
End Class
