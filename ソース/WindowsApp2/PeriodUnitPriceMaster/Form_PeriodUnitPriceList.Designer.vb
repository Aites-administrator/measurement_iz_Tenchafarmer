<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_PeriodUnitPriceList
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
    Me.TitleLabel = New System.Windows.Forms.Label()
    Me.CloseButton = New System.Windows.Forms.Button()
    Me.DeleteButton = New System.Windows.Forms.Button()
    Me.CreateButton = New System.Windows.Forms.Button()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.RegularUnitPriceText = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.PeriodRateText = New System.Windows.Forms.TextBox()
    Me.NameLabel = New System.Windows.Forms.Label()
    Me.CodeLabel = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.StartDateText = New System.Windows.Forms.TextBox()
    Me.EndDateText = New System.Windows.Forms.TextBox()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'TitleLabel
    '
    Me.TitleLabel.AutoSize = True
    Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TitleLabel.Location = New System.Drawing.Point(12, 10)
    Me.TitleLabel.Name = "TitleLabel"
    Me.TitleLabel.Size = New System.Drawing.Size(216, 30)
    Me.TitleLabel.TabIndex = 6
    Me.TitleLabel.Text = "期間別単価マスタ一覧"
    '
    'CloseButton
    '
    Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
    Me.CloseButton.Location = New System.Drawing.Point(454, 281)
    Me.CloseButton.Name = "CloseButton"
    Me.CloseButton.Size = New System.Drawing.Size(123, 50)
    Me.CloseButton.TabIndex = 11
    Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
    Me.CloseButton.UseVisualStyleBackColor = True
    '
    'DeleteButton
    '
    Me.DeleteButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
    Me.DeleteButton.Location = New System.Drawing.Point(454, 157)
    Me.DeleteButton.Name = "DeleteButton"
    Me.DeleteButton.Size = New System.Drawing.Size(123, 50)
    Me.DeleteButton.TabIndex = 10
    Me.DeleteButton.Text = "F7" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "削除"
    Me.DeleteButton.UseVisualStyleBackColor = True
    '
    'CreateButton
    '
    Me.CreateButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
    Me.CreateButton.Location = New System.Drawing.Point(454, 45)
    Me.CreateButton.Name = "CreateButton"
    Me.CreateButton.Size = New System.Drawing.Size(123, 50)
    Me.CreateButton.TabIndex = 8
    Me.CreateButton.Text = "F5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "新規・修正"
    Me.CreateButton.UseVisualStyleBackColor = True
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!)
    Me.Label3.Location = New System.Drawing.Point(194, 62)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(35, 30)
    Me.Label3.TabIndex = 8
    Me.Label3.Text = "～"
    '
    'RegularUnitPriceText
    '
    Me.RegularUnitPriceText.Enabled = False
    Me.RegularUnitPriceText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.RegularUnitPriceText.Location = New System.Drawing.Point(11, 243)
    Me.RegularUnitPriceText.MaxLength = 48
    Me.RegularUnitPriceText.Name = "RegularUnitPriceText"
    Me.RegularUnitPriceText.Size = New System.Drawing.Size(177, 33)
    Me.RegularUnitPriceText.TabIndex = 7
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!)
    Me.Label1.Location = New System.Drawing.Point(230, 23)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(79, 30)
    Me.Label1.TabIndex = 5
    Me.Label1.Text = "終了日"
    '
    'PeriodRateText
    '
    Me.PeriodRateText.Enabled = False
    Me.PeriodRateText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.PeriodRateText.Location = New System.Drawing.Point(11, 128)
    Me.PeriodRateText.MaxLength = 48
    Me.PeriodRateText.Name = "PeriodRateText"
    Me.PeriodRateText.Size = New System.Drawing.Size(177, 33)
    Me.PeriodRateText.TabIndex = 3
    '
    'NameLabel
    '
    Me.NameLabel.AutoSize = True
    Me.NameLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!)
    Me.NameLabel.Location = New System.Drawing.Point(6, 95)
    Me.NameLabel.Name = "NameLabel"
    Me.NameLabel.Size = New System.Drawing.Size(101, 30)
    Me.NameLabel.TabIndex = 2
    Me.NameLabel.Text = "期間単価"
    '
    'CodeLabel
    '
    Me.CodeLabel.AutoSize = True
    Me.CodeLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!)
    Me.CodeLabel.Location = New System.Drawing.Point(6, 23)
    Me.CodeLabel.Name = "CodeLabel"
    Me.CodeLabel.Size = New System.Drawing.Size(79, 30)
    Me.CodeLabel.TabIndex = 0
    Me.CodeLabel.Text = "開始日"
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.EndDateText)
    Me.GroupBox1.Controls.Add(Me.StartDateText)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Controls.Add(Me.RegularUnitPriceText)
    Me.GroupBox1.Controls.Add(Me.Label2)
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.PeriodRateText)
    Me.GroupBox1.Controls.Add(Me.NameLabel)
    Me.GroupBox1.Controls.Add(Me.CodeLabel)
    Me.GroupBox1.Location = New System.Drawing.Point(17, 42)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(431, 289)
    Me.GroupBox1.TabIndex = 12
    Me.GroupBox1.TabStop = False
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!)
    Me.Label2.Location = New System.Drawing.Point(6, 210)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(101, 30)
    Me.Label2.TabIndex = 6
    Me.Label2.Text = "通常単価"
    '
    'StartDateText
    '
    Me.StartDateText.Enabled = False
    Me.StartDateText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.StartDateText.Location = New System.Drawing.Point(11, 63)
    Me.StartDateText.MaxLength = 48
    Me.StartDateText.Name = "StartDateText"
    Me.StartDateText.Size = New System.Drawing.Size(177, 33)
    Me.StartDateText.TabIndex = 9
    '
    'EndDateText
    '
    Me.EndDateText.Enabled = False
    Me.EndDateText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.EndDateText.Location = New System.Drawing.Point(235, 62)
    Me.EndDateText.MaxLength = 48
    Me.EndDateText.Name = "EndDateText"
    Me.EndDateText.Size = New System.Drawing.Size(177, 33)
    Me.EndDateText.TabIndex = 10
    '
    'Form_PeriodUnitPriceList
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(585, 406)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.TitleLabel)
    Me.Controls.Add(Me.CloseButton)
    Me.Controls.Add(Me.DeleteButton)
    Me.Controls.Add(Me.CreateButton)
    Me.Name = "Form_PeriodUnitPriceList"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "期間別単価マスタ一覧"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents TitleLabel As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents DeleteButton As Button
    Friend WithEvents CreateButton As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents RegularUnitPriceText As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PeriodRateText As TextBox
    Friend WithEvents NameLabel As Label
    Friend WithEvents CodeLabel As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents EndDateText As TextBox
    Friend WithEvents StartDateText As TextBox
End Class
