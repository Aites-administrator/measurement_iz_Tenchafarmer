<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_ResultList
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DateTimeTo = New System.Windows.Forms.TextBox()
        Me.DateTimeFrom = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PeriodLabel = New System.Windows.Forms.Label()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.UpdateButton = New System.Windows.Forms.Button()
        Me.ResultDetail = New System.Windows.Forms.DataGridView()
        Me.CsvExportButton = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ToStaffCode_ComboBox = New System.Windows.Forms.ComboBox()
        Me.FromStaffCode_ComboBox = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.ResultDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateTimeTo
        '
        Me.DateTimeTo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.DateTimeTo.Location = New System.Drawing.Point(339, 47)
        Me.DateTimeTo.MaxLength = 10
        Me.DateTimeTo.Name = "DateTimeTo"
        Me.DateTimeTo.Size = New System.Drawing.Size(160, 33)
        Me.DateTimeTo.TabIndex = 4
        '
        'DateTimeFrom
        '
        Me.DateTimeFrom.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.DateTimeFrom.Location = New System.Drawing.Point(138, 47)
        Me.DateTimeFrom.MaxLength = 10
        Me.DateTimeFrom.Name = "DateTimeFrom"
        Me.DateTimeFrom.Size = New System.Drawing.Size(160, 33)
        Me.DateTimeFrom.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(302, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 21)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "～"
        '
        'PeriodLabel
        '
        Me.PeriodLabel.AutoSize = True
        Me.PeriodLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PeriodLabel.Location = New System.Drawing.Point(16, 50)
        Me.PeriodLabel.Name = "PeriodLabel"
        Me.PeriodLabel.Size = New System.Drawing.Size(94, 30)
        Me.PeriodLabel.TabIndex = 1
        Me.PeriodLabel.Text = "日　付："
        '
        'SearchButton
        '
        Me.SearchButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.SearchButton.Location = New System.Drawing.Point(817, 127)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.Size = New System.Drawing.Size(123, 50)
        Me.SearchButton.TabIndex = 20
        Me.SearchButton.Text = "F1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "検索"
        Me.SearchButton.UseVisualStyleBackColor = True
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(105, 30)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "実績一覧"
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CloseButton.Location = New System.Drawing.Point(817, 639)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 50)
        Me.CloseButton.TabIndex = 25
        Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'DeleteButton
        '
        Me.DeleteButton.Enabled = False
        Me.DeleteButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.DeleteButton.Location = New System.Drawing.Point(817, 463)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(123, 50)
        Me.DeleteButton.TabIndex = 24
        Me.DeleteButton.Text = "F7" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "削除"
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'UpdateButton
        '
        Me.UpdateButton.Enabled = False
        Me.UpdateButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.UpdateButton.Location = New System.Drawing.Point(817, 407)
        Me.UpdateButton.Name = "UpdateButton"
        Me.UpdateButton.Size = New System.Drawing.Size(123, 50)
        Me.UpdateButton.TabIndex = 23
        Me.UpdateButton.Text = "F6" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "修正"
        Me.UpdateButton.UseVisualStyleBackColor = True
        '
        'ResultDetail
        '
        Me.ResultDetail.AllowUserToAddRows = False
        Me.ResultDetail.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ResultDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.ResultDetail.ColumnHeadersHeight = 30
        Me.ResultDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ResultDetail.DefaultCellStyle = DataGridViewCellStyle5
        Me.ResultDetail.Location = New System.Drawing.Point(12, 127)
        Me.ResultDetail.Name = "ResultDetail"
        Me.ResultDetail.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ResultDetail.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.ResultDetail.RowTemplate.Height = 30
        Me.ResultDetail.Size = New System.Drawing.Size(796, 562)
        Me.ResultDetail.TabIndex = 19
        Me.ResultDetail.TabStop = False
        '
        'CsvExportButton
        '
        Me.CsvExportButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CsvExportButton.Location = New System.Drawing.Point(817, 183)
        Me.CsvExportButton.Name = "CsvExportButton"
        Me.CsvExportButton.Size = New System.Drawing.Size(123, 50)
        Me.CsvExportButton.TabIndex = 21
        Me.CsvExportButton.Text = "F2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "CSV出力"
        Me.CsvExportButton.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(302, 97)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 21)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "～"
        '
        'ToStaffCode_ComboBox
        '
        Me.ToStaffCode_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ToStaffCode_ComboBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ToStaffCode_ComboBox.FormattingEnabled = True
        Me.ToStaffCode_ComboBox.Location = New System.Drawing.Point(339, 88)
        Me.ToStaffCode_ComboBox.Name = "ToStaffCode_ComboBox"
        Me.ToStaffCode_ComboBox.Size = New System.Drawing.Size(160, 33)
        Me.ToStaffCode_ComboBox.TabIndex = 12
        '
        'FromStaffCode_ComboBox
        '
        Me.FromStaffCode_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FromStaffCode_ComboBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FromStaffCode_ComboBox.FormattingEnabled = True
        Me.FromStaffCode_ComboBox.Location = New System.Drawing.Point(138, 87)
        Me.FromStaffCode_ComboBox.Name = "FromStaffCode_ComboBox"
        Me.FromStaffCode_ComboBox.Size = New System.Drawing.Size(160, 33)
        Me.FromStaffCode_ComboBox.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(16, 90)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(101, 30)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "担当者："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(505, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(201, 21)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "（ YYYY/MM/DD：西暦 ）"
        '
        'Form_ResultList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(957, 701)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.ToStaffCode_ComboBox)
        Me.Controls.Add(Me.FromStaffCode_ComboBox)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CsvExportButton)
        Me.Controls.Add(Me.ResultDetail)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.UpdateButton)
        Me.Controls.Add(Me.DateTimeTo)
        Me.Controls.Add(Me.DateTimeFrom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PeriodLabel)
        Me.Controls.Add(Me.SearchButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.Name = "Form_ResultList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.ResultDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DateTimeTo As TextBox
    Friend WithEvents DateTimeFrom As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents PeriodLabel As Label
    Friend WithEvents SearchButton As Button
    Friend WithEvents TitleLabel As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents DeleteButton As Button
    Friend WithEvents UpdateButton As Button
    Friend WithEvents ResultDetail As DataGridView
    Friend WithEvents CsvExportButton As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents ToStaffCode_ComboBox As ComboBox
    Friend WithEvents FromStaffCode_ComboBox As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label4 As Label
End Class
