Imports Common.ClsFunction
Public Class Form_MasterMente

  Private Sub Form_MasterMente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    FormBorderStyle = FormBorderStyle.FixedSingle
    ' キーイベントをフォーム全体で受け取るようにする
    Me.KeyPreview = True
  End Sub
  Private Sub ItemMasterButton_Click(sender As Object, e As EventArgs) Handles ItemMasterButton.Click
    OpenForm("M01")
  End Sub

  Private Sub StaffMasterButton_Click(sender As Object, e As EventArgs) Handles StaffMasterButton.Click
    OpenForm("M03")
  End Sub

  Private Sub PeriodUnitPriceMasterButton_Click(sender As Object, e As EventArgs) Handles PeriodUnitPriceMasterButton.Click
    OpenForm("M12")
  End Sub
  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub Form_MasterMente_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F1
        ItemMasterButton.PerformClick()
      Case Keys.F3
        StaffMasterButton.PerformClick()
      Case Keys.F12
        PeriodUnitPriceMasterButton.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub
End Class