Imports Common.ClsFunction
Public Class Form_OutPut
  Private Sub Form_OutPut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    FormBorderStyle = FormBorderStyle.FixedSingle
    ' キーイベントをフォーム全体で受け取るようにする
    Me.KeyPreview = True
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub


  Private Sub Form_OutPut_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F1
        PersonalExportButton.PerformClick()
      Case Keys.F2
        SummaryExportButton.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub

  Private Sub PersonalExportButton_Click(sender As Object, e As EventArgs) Handles PersonalExportButton.Click
    OpenForm("OTH03")
  End Sub
  Private Sub SummaryExportButton_Click(sender As Object, e As EventArgs) Handles SummaryExportButton.Click
    OpenForm("OTH04")
  End Sub

End Class