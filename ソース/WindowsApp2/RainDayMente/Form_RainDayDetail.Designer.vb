<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_RainDayDetail
  Inherits System.Windows.Forms.Form

  'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Windows フォーム デザイナーで必要です。
  Private components As System.ComponentModel.IContainer

  'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
  'Windows フォーム デザイナーを使用して変更できます。  
  'コード エディターを使って変更しないでください。
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
        Me.RainDayDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.CodeLabel = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.OkButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'RainDayDateTimePicker
        '
        Me.RainDayDateTimePicker.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RainDayDateTimePicker.Location = New System.Drawing.Point(17, 64)
        Me.RainDayDateTimePicker.Name = "RainDayDateTimePicker"
        Me.RainDayDateTimePicker.Size = New System.Drawing.Size(251, 35)
        Me.RainDayDateTimePicker.TabIndex = 11
        '
        'CodeLabel
        '
        Me.CodeLabel.AutoSize = True
        Me.CodeLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.CodeLabel.Location = New System.Drawing.Point(12, 9)
        Me.CodeLabel.Name = "CodeLabel"
        Me.CodeLabel.Size = New System.Drawing.Size(121, 30)
        Me.CodeLabel.TabIndex = 10
        Me.CodeLabel.Text = "雨休み登録"
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CloseButton.Location = New System.Drawing.Point(211, 143)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 50)
        Me.CloseButton.TabIndex = 13
        Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'OkButton
        '
        Me.OkButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.OkButton.Location = New System.Drawing.Point(82, 143)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(123, 50)
        Me.OkButton.TabIndex = 12
        Me.OkButton.Text = "F5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "登録"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'Form_RainDayDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(346, 205)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.RainDayDateTimePicker)
        Me.Controls.Add(Me.CodeLabel)
        Me.Name = "Form_RainDayDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "雨休み登録"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents RainDayDateTimePicker As DateTimePicker
    Friend WithEvents CodeLabel As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents OkButton As Button
End Class
