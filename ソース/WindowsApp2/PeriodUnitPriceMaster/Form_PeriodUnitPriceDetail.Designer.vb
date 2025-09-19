<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_PeriodUnitPriceDetail
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.EndDateText = New System.Windows.Forms.DateTimePicker()
        Me.StartDateText = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RegularUnitPriceText = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PeriodRateText = New System.Windows.Forms.TextBox()
        Me.NameLabel = New System.Windows.Forms.Label()
        Me.CodeLabel = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.OkButton = New System.Windows.Forms.Button()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.GroupBox1.Size = New System.Drawing.Size(494, 289)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'EndDateText
        '
        Me.EndDateText.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.EndDateText.Location = New System.Drawing.Point(235, 57)
        Me.EndDateText.Name = "EndDateText"
        Me.EndDateText.Size = New System.Drawing.Size(177, 35)
        Me.EndDateText.TabIndex = 10
        '
        'StartDateText
        '
        Me.StartDateText.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StartDateText.Location = New System.Drawing.Point(11, 57)
        Me.StartDateText.Name = "StartDateText"
        Me.StartDateText.Size = New System.Drawing.Size(177, 35)
        Me.StartDateText.TabIndex = 9
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
        Me.RegularUnitPriceText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.RegularUnitPriceText.Location = New System.Drawing.Point(11, 243)
        Me.RegularUnitPriceText.MaxLength = 48
        Me.RegularUnitPriceText.Name = "RegularUnitPriceText"
        Me.RegularUnitPriceText.Size = New System.Drawing.Size(177, 33)
        Me.RegularUnitPriceText.TabIndex = 7
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
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CloseButton.Location = New System.Drawing.Point(450, 344)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 50)
        Me.CloseButton.TabIndex = 7
        Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'OkButton
        '
        Me.OkButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.OkButton.Location = New System.Drawing.Point(321, 344)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(123, 50)
        Me.OkButton.TabIndex = 6
        Me.OkButton.Text = "F5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "登録"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(216, 30)
        Me.TitleLabel.TabIndex = 4
        Me.TitleLabel.Text = "期間別単価マスタ詳細"
        '
        'Form_PeriodUnitPriceDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(585, 406)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.Name = "Form_PeriodUnitPriceDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "期間別単価マスタ詳細"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents PeriodRateText As TextBox
    Friend WithEvents NameLabel As Label
    Friend WithEvents CodeLabel As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents OkButton As Button
    Friend WithEvents TitleLabel As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents RegularUnitPriceText As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents EndDateText As DateTimePicker
    Friend WithEvents StartDateText As DateTimePicker
End Class
