<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_RainDayList
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.RainDayDetail = New System.Windows.Forms.DataGridView()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.CreateButton = New System.Windows.Forms.Button()
        CType(Me.RainDayDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(13, 12)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(121, 30)
        Me.TitleLabel.TabIndex = 12
        Me.TitleLabel.Text = "雨休み一覧"
        '
        'RainDayDetail
        '
        Me.RainDayDetail.AllowUserToAddRows = False
        Me.RainDayDetail.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RainDayDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.RainDayDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.RainDayDetail.DefaultCellStyle = DataGridViewCellStyle5
        Me.RainDayDetail.Location = New System.Drawing.Point(19, 54)
        Me.RainDayDetail.Name = "RainDayDetail"
        Me.RainDayDetail.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RainDayDetail.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.RainDayDetail.RowTemplate.Height = 30
        Me.RainDayDetail.Size = New System.Drawing.Size(439, 359)
        Me.RainDayDetail.TabIndex = 13
        Me.RainDayDetail.TabStop = False
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CloseButton.Location = New System.Drawing.Point(467, 362)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 50)
        Me.CloseButton.TabIndex = 17
        Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'DeleteButton
        '
        Me.DeleteButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.DeleteButton.Location = New System.Drawing.Point(467, 167)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(123, 50)
        Me.DeleteButton.TabIndex = 16
        Me.DeleteButton.Text = "F7" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "削除"
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'CreateButton
        '
        Me.CreateButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CreateButton.Location = New System.Drawing.Point(467, 54)
        Me.CreateButton.Name = "CreateButton"
        Me.CreateButton.Size = New System.Drawing.Size(123, 50)
        Me.CreateButton.TabIndex = 14
        Me.CreateButton.Text = "F5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "新規"
        Me.CreateButton.UseVisualStyleBackColor = True
        '
        'Form_RainDayList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(602, 424)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.RainDayDetail)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.CreateButton)
        Me.Name = "Form_RainDayList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "雨休み一覧"
        CType(Me.RainDayDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleLabel As Label
    Friend WithEvents RainDayDetail As DataGridView
    Friend WithEvents CloseButton As Button
    Friend WithEvents DeleteButton As Button
    Friend WithEvents CreateButton As Button
End Class
