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
        Me.BtnExport = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Form_PersonalExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 180)
        Me.Font = New System.Drawing.Font("Meiryo", 12.0!)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "作業者ごと帳票出力"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New Font("Meiryo", 12.0!, FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(20, 20)
        Me.Label1.Text = "期間を指定してください"
        '
        'DateTimePickerFrom
        '
        Me.DateTimePickerFrom.Font = New Font("Meiryo", 12.0!)
        Me.DateTimePickerFrom.Location = New System.Drawing.Point(20, 60)
        Me.DateTimePickerFrom.Size = New System.Drawing.Size(150, 29)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New Font("Meiryo", 12.0!)
        Me.Label2.Location = New System.Drawing.Point(180, 65)
        Me.Label2.Text = "～"
        '
        'DateTimePickerTo
        '
        Me.DateTimePickerTo.Font = New Font("Meiryo", 12.0!)
        Me.DateTimePickerTo.Location = New System.Drawing.Point(210, 60)
        Me.DateTimePickerTo.Size = New System.Drawing.Size(150, 29)
        '
        'BtnExport
        '
        Me.BtnExport.Font = New Font("Meiryo", 12.0!, FontStyle.Bold)
        Me.BtnExport.BackColor = Color.SteelBlue
        Me.BtnExport.ForeColor = Color.White
        Me.BtnExport.Location = New System.Drawing.Point(20, 110)
        Me.BtnExport.Size = New System.Drawing.Size(180, 45)
        Me.BtnExport.Text = "個人別帳票を出力"
        Me.BtnExport.UseVisualStyleBackColor = False
        '
        'Add Controls
        '
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DateTimePickerFrom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePickerTo)
        Me.Controls.Add(Me.BtnExport)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents DateTimePickerFrom As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents DateTimePickerTo As DateTimePicker
    Friend WithEvents BtnExport As Button
End Class
