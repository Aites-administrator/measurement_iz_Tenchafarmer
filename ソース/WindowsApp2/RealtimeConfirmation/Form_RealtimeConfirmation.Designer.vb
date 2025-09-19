<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_RealtimeConfirmation
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.ResultDetail = New System.Windows.Forms.DataGridView()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.USB_ReceiveButton = New System.Windows.Forms.Button()
        Me.USB_SendButton = New System.Windows.Forms.Button()
        CType(Me.ResultDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(123, 30)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "計量器通信"
        '
        'ResultDetail
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ResultDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.ResultDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ResultDetail.DefaultCellStyle = DataGridViewCellStyle2
        Me.ResultDetail.Location = New System.Drawing.Point(18, 51)
        Me.ResultDetail.Name = "ResultDetail"
        Me.ResultDetail.ReadOnly = True
        Me.ResultDetail.RowTemplate.Height = 30
        Me.ResultDetail.Size = New System.Drawing.Size(381, 230)
        Me.ResultDetail.TabIndex = 1
        Me.ResultDetail.TabStop = False
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CloseButton.Location = New System.Drawing.Point(407, 231)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 50)
        Me.CloseButton.TabIndex = 7
        Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'USB_ReceiveButton
        '
        Me.USB_ReceiveButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.USB_ReceiveButton.Location = New System.Drawing.Point(405, 51)
        Me.USB_ReceiveButton.Name = "USB_ReceiveButton"
        Me.USB_ReceiveButton.Size = New System.Drawing.Size(123, 50)
        Me.USB_ReceiveButton.TabIndex = 5
        Me.USB_ReceiveButton.Text = "F1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "実績受信"
        Me.USB_ReceiveButton.UseVisualStyleBackColor = True
        '
        'USB_SendButton
        '
        Me.USB_SendButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.USB_SendButton.Location = New System.Drawing.Point(405, 107)
        Me.USB_SendButton.Name = "USB_SendButton"
        Me.USB_SendButton.Size = New System.Drawing.Size(123, 50)
        Me.USB_SendButton.TabIndex = 6
        Me.USB_SendButton.Text = "F2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "マスタ送信"
        Me.USB_SendButton.UseVisualStyleBackColor = True
        '
        'Form_RealtimeConfirmation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 295)
        Me.Controls.Add(Me.USB_SendButton)
        Me.Controls.Add(Me.USB_ReceiveButton)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.ResultDetail)
        Me.Controls.Add(Me.TitleLabel)
        Me.Name = "Form_RealtimeConfirmation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "計量器通信"
        CType(Me.ResultDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleLabel As Label
    Friend WithEvents ResultDetail As DataGridView
    Friend WithEvents CloseButton As Button
    Friend WithEvents USB_ReceiveButton As Button
    Friend WithEvents USB_SendButton As Button
End Class
