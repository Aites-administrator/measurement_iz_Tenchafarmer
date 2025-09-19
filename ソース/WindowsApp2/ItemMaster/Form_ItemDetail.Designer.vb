<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_ItemDetail
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
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.OkButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ItemNoText = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ItemNameText = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.CallCodeText = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.SubtotalTargetUnitComboBox = New System.Windows.Forms.ComboBox()
        Me.SubtotalTargetCntText = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SubtotalTargetQtyText = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.LowerWeightText = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LowerWeightUnitComboBox = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.WeightText = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.WeightComboBox = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.UpperWeightText = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.UpperWeightUnitComboBox = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.PackingComboBox = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PackingUnitComboBox = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(150, 30)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "商品マスタ詳細"
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.CloseButton.Location = New System.Drawing.Point(619, 524)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 50)
        Me.CloseButton.TabIndex = 6
        Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'OkButton
        '
        Me.OkButton.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.OkButton.Location = New System.Drawing.Point(490, 524)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(123, 50)
        Me.OkButton.TabIndex = 5
        Me.OkButton.Text = "F5" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "登録"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ItemNoText)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.ItemNameText)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.CallCodeText)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Location = New System.Drawing.Point(42, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(344, 281)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'ItemNoText
        '
        Me.ItemNoText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.ItemNoText.Location = New System.Drawing.Point(183, 57)
        Me.ItemNoText.MaxLength = 20
        Me.ItemNoText.Name = "ItemNoText"
        Me.ItemNoText.Size = New System.Drawing.Size(147, 33)
        Me.ItemNoText.TabIndex = 3
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(6, 60)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(57, 30)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "品番"
        '
        'ItemNameText
        '
        Me.ItemNameText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.ItemNameText.Location = New System.Drawing.Point(183, 97)
        Me.ItemNameText.MaxLength = 99
        Me.ItemNameText.Name = "ItemNameText"
        Me.ItemNameText.Size = New System.Drawing.Size(147, 33)
        Me.ItemNameText.TabIndex = 5
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(6, 100)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(57, 30)
        Me.Label22.TabIndex = 4
        Me.Label22.Text = "品名"
        '
        'CallCodeText
        '
        Me.CallCodeText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CallCodeText.Location = New System.Drawing.Point(183, 17)
        Me.CallCodeText.MaxLength = 8
        Me.CallCodeText.Name = "CallCodeText"
        Me.CallCodeText.Size = New System.Drawing.Size(147, 33)
        Me.CallCodeText.TabIndex = 1
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(6, 20)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(106, 30)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "呼出コード"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.SubtotalTargetUnitComboBox)
        Me.GroupBox2.Controls.Add(Me.SubtotalTargetCntText)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.SubtotalTargetQtyText)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Location = New System.Drawing.Point(42, 329)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(344, 193)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'SubtotalTargetUnitComboBox
        '
        Me.SubtotalTargetUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SubtotalTargetUnitComboBox.Enabled = False
        Me.SubtotalTargetUnitComboBox.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.SubtotalTargetUnitComboBox.FormattingEnabled = True
        Me.SubtotalTargetUnitComboBox.Location = New System.Drawing.Point(183, 101)
        Me.SubtotalTargetUnitComboBox.Name = "SubtotalTargetUnitComboBox"
        Me.SubtotalTargetUnitComboBox.Size = New System.Drawing.Size(147, 33)
        Me.SubtotalTargetUnitComboBox.TabIndex = 5
        '
        'SubtotalTargetCntText
        '
        Me.SubtotalTargetCntText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.SubtotalTargetCntText.Location = New System.Drawing.Point(183, 57)
        Me.SubtotalTargetCntText.MaxLength = 6
        Me.SubtotalTargetCntText.Name = "SubtotalTargetCntText"
        Me.SubtotalTargetCntText.Size = New System.Drawing.Size(147, 33)
        Me.SubtotalTargetCntText.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 30)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "小計目標回数"
        '
        'SubtotalTargetQtyText
        '
        Me.SubtotalTargetQtyText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.SubtotalTargetQtyText.Location = New System.Drawing.Point(183, 17)
        Me.SubtotalTargetQtyText.MaxLength = 6
        Me.SubtotalTargetQtyText.Name = "SubtotalTargetQtyText"
        Me.SubtotalTargetQtyText.Size = New System.Drawing.Size(147, 33)
        Me.SubtotalTargetQtyText.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(123, 30)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "小計目標値"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(6, 100)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(167, 30)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "小計目標値単位"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.LowerWeightText)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.LowerWeightUnitComboBox)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.WeightText)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.WeightComboBox)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.UpperWeightText)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.UpperWeightUnitComboBox)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Location = New System.Drawing.Point(398, 42)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(343, 281)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'LowerWeightText
        '
        Me.LowerWeightText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.LowerWeightText.Location = New System.Drawing.Point(183, 181)
        Me.LowerWeightText.MaxLength = 10
        Me.LowerWeightText.Name = "LowerWeightText"
        Me.LowerWeightText.Size = New System.Drawing.Size(147, 33)
        Me.LowerWeightText.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 180)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 30)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "下限値"
        '
        'LowerWeightUnitComboBox
        '
        Me.LowerWeightUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LowerWeightUnitComboBox.Enabled = False
        Me.LowerWeightUnitComboBox.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LowerWeightUnitComboBox.FormattingEnabled = True
        Me.LowerWeightUnitComboBox.Location = New System.Drawing.Point(183, 221)
        Me.LowerWeightUnitComboBox.Name = "LowerWeightUnitComboBox"
        Me.LowerWeightUnitComboBox.Size = New System.Drawing.Size(147, 33)
        Me.LowerWeightUnitComboBox.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 220)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 30)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "下限値単位"
        '
        'WeightText
        '
        Me.WeightText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.WeightText.Location = New System.Drawing.Point(183, 97)
        Me.WeightText.MaxLength = 10
        Me.WeightText.Name = "WeightText"
        Me.WeightText.Size = New System.Drawing.Size(147, 33)
        Me.WeightText.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 30)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "基準値"
        '
        'WeightComboBox
        '
        Me.WeightComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.WeightComboBox.Enabled = False
        Me.WeightComboBox.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.WeightComboBox.FormattingEnabled = True
        Me.WeightComboBox.Location = New System.Drawing.Point(183, 141)
        Me.WeightComboBox.Name = "WeightComboBox"
        Me.WeightComboBox.Size = New System.Drawing.Size(147, 33)
        Me.WeightComboBox.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(6, 140)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(123, 30)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "基準値単位"
        '
        'UpperWeightText
        '
        Me.UpperWeightText.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.UpperWeightText.Location = New System.Drawing.Point(183, 17)
        Me.UpperWeightText.MaxLength = 10
        Me.UpperWeightText.Name = "UpperWeightText"
        Me.UpperWeightText.Size = New System.Drawing.Size(147, 33)
        Me.UpperWeightText.TabIndex = 1
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(6, 20)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(79, 30)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "上限値"
        '
        'UpperWeightUnitComboBox
        '
        Me.UpperWeightUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.UpperWeightUnitComboBox.Enabled = False
        Me.UpperWeightUnitComboBox.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.UpperWeightUnitComboBox.FormattingEnabled = True
        Me.UpperWeightUnitComboBox.Location = New System.Drawing.Point(183, 57)
        Me.UpperWeightUnitComboBox.Name = "UpperWeightUnitComboBox"
        Me.UpperWeightUnitComboBox.Size = New System.Drawing.Size(147, 33)
        Me.UpperWeightUnitComboBox.TabIndex = 3
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(6, 60)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(123, 30)
        Me.Label24.TabIndex = 2
        Me.Label24.Text = "上限値単位"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.PackingComboBox)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.PackingUnitComboBox)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Location = New System.Drawing.Point(398, 329)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(343, 192)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        '
        'PackingComboBox
        '
        Me.PackingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PackingComboBox.Enabled = False
        Me.PackingComboBox.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PackingComboBox.FormattingEnabled = True
        Me.PackingComboBox.Location = New System.Drawing.Point(183, 17)
        Me.PackingComboBox.Name = "PackingComboBox"
        Me.PackingComboBox.Size = New System.Drawing.Size(147, 33)
        Me.PackingComboBox.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 30)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "風袋"
        '
        'PackingUnitComboBox
        '
        Me.PackingUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PackingUnitComboBox.Enabled = False
        Me.PackingUnitComboBox.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PackingUnitComboBox.FormattingEnabled = True
        Me.PackingUnitComboBox.Location = New System.Drawing.Point(183, 61)
        Me.PackingUnitComboBox.Name = "PackingUnitComboBox"
        Me.PackingUnitComboBox.Size = New System.Drawing.Size(147, 33)
        Me.PackingUnitComboBox.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 30)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "風袋単位"
        '
        'Form_ItemDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 586)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.Name = "Form_ItemDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ItemDetail"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TitleLabel As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents OkButton As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ItemNoText As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents ItemNameText As TextBox
    Friend WithEvents Label22 As Label
    Friend WithEvents CallCodeText As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents SubtotalTargetCntText As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents SubtotalTargetQtyText As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents LowerWeightText As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents LowerWeightUnitComboBox As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents WeightText As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents WeightComboBox As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents UpperWeightText As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents UpperWeightUnitComboBox As ComboBox
    Friend WithEvents Label24 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents PackingComboBox As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents PackingUnitComboBox As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents SubtotalTargetUnitComboBox As ComboBox
End Class
