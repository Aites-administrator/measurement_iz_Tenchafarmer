<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_OutPut
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PersonalExportButton = New System.Windows.Forms.Button()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.SummaryExportButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'PersonalExportButton
        '
        Me.PersonalExportButton.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.PersonalExportButton.Location = New System.Drawing.Point(12, 72)
        Me.PersonalExportButton.Name = "PersonalExportButton"
        Me.PersonalExportButton.Size = New System.Drawing.Size(407, 79)
        Me.PersonalExportButton.TabIndex = 5
        Me.PersonalExportButton.Text = "F1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "茶摘日報（個人別）"
        Me.PersonalExportButton.UseVisualStyleBackColor = True
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(229, 45)
        Me.TitleLabel.TabIndex = 15
        Me.TitleLabel.Text = "累計データ管理"
        Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CloseButton.Location = New System.Drawing.Point(710, 586)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 50)
        Me.CloseButton.TabIndex = 16
        Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'SummaryExportButton
        '
        Me.SummaryExportButton.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.SummaryExportButton.Location = New System.Drawing.Point(425, 72)
        Me.SummaryExportButton.Name = "SummaryExportButton"
        Me.SummaryExportButton.Size = New System.Drawing.Size(407, 79)
        Me.SummaryExportButton.TabIndex = 17
        Me.SummaryExportButton.Text = "F2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "茶摘報酬集計表"
        Me.SummaryExportButton.UseVisualStyleBackColor = True
        '
        'Form_OutPut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 648)
        Me.Controls.Add(Me.SummaryExportButton)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.PersonalExportButton)
        Me.Name = "Form_OutPut"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "累計データ管理"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PersonalExportButton As Button
    Friend WithEvents TitleLabel As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents SummaryExportButton As Button
End Class
