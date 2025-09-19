<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_MasterMente
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
        Me.ItemMasterButton = New System.Windows.Forms.Button()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.StaffMasterButton = New System.Windows.Forms.Button()
        Me.PeriodUnitPriceMasterButton = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ItemMasterButton
        '
        Me.ItemMasterButton.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ItemMasterButton.Location = New System.Drawing.Point(12, 72)
        Me.ItemMasterButton.Name = "ItemMasterButton"
        Me.ItemMasterButton.Size = New System.Drawing.Size(407, 79)
        Me.ItemMasterButton.TabIndex = 1
        Me.ItemMasterButton.Text = "F1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "商品"
        Me.ItemMasterButton.UseVisualStyleBackColor = True
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(166, 45)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "マスタメンテ"
        Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseButton.Location = New System.Drawing.Point(710, 586)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 50)
        Me.CloseButton.TabIndex = 2
        Me.CloseButton.Text = "ESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'StaffMasterButton
        '
        Me.StaffMasterButton.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StaffMasterButton.Location = New System.Drawing.Point(426, 72)
        Me.StaffMasterButton.Name = "StaffMasterButton"
        Me.StaffMasterButton.Size = New System.Drawing.Size(407, 79)
        Me.StaffMasterButton.TabIndex = 11
        Me.StaffMasterButton.Text = "F2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "担当者"
        Me.StaffMasterButton.UseVisualStyleBackColor = True
        '
        'PeriodUnitPriceMasterButton
        '
        Me.PeriodUnitPriceMasterButton.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PeriodUnitPriceMasterButton.Location = New System.Drawing.Point(12, 157)
        Me.PeriodUnitPriceMasterButton.Name = "PeriodUnitPriceMasterButton"
        Me.PeriodUnitPriceMasterButton.Size = New System.Drawing.Size(407, 79)
        Me.PeriodUnitPriceMasterButton.TabIndex = 12
        Me.PeriodUnitPriceMasterButton.Text = "F3" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "期間別単価"
        Me.PeriodUnitPriceMasterButton.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(426, 157)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(407, 79)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "F4" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "休日（雨休み）"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form_MasterMente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(845, 648)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PeriodUnitPriceMasterButton)
        Me.Controls.Add(Me.StaffMasterButton)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.ItemMasterButton)
        Me.KeyPreview = True
        Me.Name = "Form_MasterMente"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "マスタメンテナンス"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ItemMasterButton As Button
    Friend WithEvents TitleLabel As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents StaffMasterButton As Button
    Friend WithEvents PeriodUnitPriceMasterButton As Button
    Friend WithEvents Button1 As Button
End Class
