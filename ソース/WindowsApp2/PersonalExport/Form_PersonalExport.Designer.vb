<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_PersonalExport
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePickerTo = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.Label1.Location = New System.Drawing.Point(11, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(212, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "期間を指定してください"
        '
        'DateTimePickerFrom
        '
        Me.DateTimePickerFrom.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.DateTimePickerFrom.Location = New System.Drawing.Point(20, 115)
        Me.DateTimePickerFrom.Name = "DateTimePickerFrom"
        Me.DateTimePickerFrom.Size = New System.Drawing.Size(150, 31)
        Me.DateTimePickerFrom.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.Label2.Location = New System.Drawing.Point(180, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 24)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "～"
        '
        'DateTimePickerTo
        '
        Me.DateTimePickerTo.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.DateTimePickerTo.Location = New System.Drawing.Point(210, 115)
        Me.DateTimePickerTo.Name = "DateTimePickerTo"
        Me.DateTimePickerTo.Size = New System.Drawing.Size(150, 31)
        Me.DateTimePickerTo.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(211, 30)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "茶摘日報（個人別）"
        '
        'BtnExport
        '
        Me.BtnExport.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.BtnExport.Location = New System.Drawing.Point(136, 168)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(123, 50)
        Me.BtnExport.TabIndex = 12
        Me.BtnExport.Text = "F5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "出力"
        Me.BtnExport.UseVisualStyleBackColor = True
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CloseButton.Location = New System.Drawing.Point(265, 168)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 50)
        Me.CloseButton.TabIndex = 13
        Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(351, 232)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(200, 31)
        Me.DateTimePicker1.TabIndex = 14
        '
        'Form_PersonalExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 230)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.BtnExport)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePickerFrom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePickerTo)
        Me.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.Name = "Form_PersonalExport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "茶摘日報（個人別）"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePickerFrom As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents DateTimePickerTo As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents BtnExport As Button
    Friend WithEvents CloseButton As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
End Class
