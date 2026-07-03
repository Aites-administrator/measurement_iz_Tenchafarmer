<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_SummaryExport
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePickerYear = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.Label1.Location = New System.Drawing.Point(20, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(212, 30)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "年度を選択してください"
        '
        'DateTimePickerYear
        '
        Me.DateTimePickerYear.CustomFormat = "yyyy"
        Me.DateTimePickerYear.Font = New System.Drawing.Font("メイリオ", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DateTimePickerYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerYear.Location = New System.Drawing.Point(25, 113)
        Me.DateTimePickerYear.Name = "DateTimePickerYear"
        Me.DateTimePickerYear.ShowUpDown = True
        Me.DateTimePickerYear.Size = New System.Drawing.Size(120, 39)
        Me.DateTimePickerYear.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(167, 30)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "茶摘報酬集計表"
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CloseButton.Location = New System.Drawing.Point(265, 185)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 50)
        Me.CloseButton.TabIndex = 15
        Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'BtnExport
        '
        Me.BtnExport.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.BtnExport.Location = New System.Drawing.Point(136, 185)
        Me.BtnExport.Name = "BtnExport"
        Me.BtnExport.Size = New System.Drawing.Size(123, 50)
        Me.BtnExport.TabIndex = 14
        Me.BtnExport.Text = "F5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "出力"
        Me.BtnExport.UseVisualStyleBackColor = True
        '
        'Form_SummaryExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 247)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.BtnExport)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePickerYear)
        Me.Font = New System.Drawing.Font("メイリオ", 12.0!)
        Me.Name = "Form_SummaryExport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "茶摘報酬集計表"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePickerYear As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents BtnExport As Button
End Class
