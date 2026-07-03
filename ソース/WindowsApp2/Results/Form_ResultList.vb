Imports System.IO
Imports Common
Imports Common.ClsFunction
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Public Class Form_ResultList
  Private ReadOnly StaffDigits As Integer = ReadSettingIniFile("STAFF_DIGITS", "VALUE")
  Private ReadOnly ResultCsvPath As String = ReadSettingIniFile("RESULT_CSV_PATH", "VALUE")
  Private ReadOnly tmpDb As New ClsSqlServer

  Dim tmpDt As New DataTable
  ' SQLサーバー操作オブジェクト
  Private _SqlServer As ClsSqlServer

  Private ReadOnly Property SqlServer As ClsSqlServer
    Get
      If _SqlServer Is Nothing Then
        _SqlServer = New ClsSqlServer
      End If
      Return _SqlServer
    End Get
  End Property
  Private Sub ResultList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False

    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "実績一覧" & " ( " & updateTime & " ) "

    Me.KeyPreview = True

    ' データベースアクセスのための一時的なオブジェクトを作成
    Dim tmpDb As New ClsSqlServer
    Dim tmpDt As New DataTable

    FormBorderStyle = FormBorderStyle.FixedSingle

    ResultDetail.RowHeadersVisible = False

    Dim dtNow As DateTime = DateTime.Now
    DateTimeFrom.Text = New Date(dtNow.Year, dtNow.Month, 1)
    DateTimeTo.Text = New Date(dtNow.Year, dtNow.Month, 1).AddMonths(1).AddDays(-1)

    SetStaffNumberComboBox()

    ResultDetail.AllowUserToAddRows = False

    ResultDetail.ColumnCount = 50

    ResultDetail.Columns(0).HeaderText = "連番"
    ResultDetail.Columns(1).HeaderText = "日付"
    ResultDetail.Columns(2).HeaderText = "時刻"
    ResultDetail.Columns(3).HeaderText = "号機"
    ResultDetail.Columns(4).HeaderText = "呼出ｺｰﾄﾞ"
    ResultDetail.Columns(5).HeaderText = "品番"
    ResultDetail.Columns(6).HeaderText = "品名"
    ResultDetail.Columns(7).HeaderText = "風袋"
    ResultDetail.Columns(8).HeaderText = "風袋単位"
    ResultDetail.Columns(9).HeaderText = "風袋１重量"
    ResultDetail.Columns(10).HeaderText = "風袋１重量単位"
    ResultDetail.Columns(11).HeaderText = "風袋２重量"
    ResultDetail.Columns(12).HeaderText = "風袋２重量単位"
    ResultDetail.Columns(13).HeaderText = "風袋２の掛け算"
    ResultDetail.Columns(14).HeaderText = "風袋１№"
    ResultDetail.Columns(15).HeaderText = "風袋１名称"
    ResultDetail.Columns(16).HeaderText = "風袋２№"
    ResultDetail.Columns(17).HeaderText = "風袋２名称"
    ResultDetail.Columns(18).HeaderText = "フリー１№"
    ResultDetail.Columns(19).HeaderText = "フリー１名称"
    ResultDetail.Columns(20).HeaderText = "フリー２№"
    ResultDetail.Columns(21).HeaderText = "フリー２名称"
    ResultDetail.Columns(22).HeaderText = "フリー３№"
    ResultDetail.Columns(23).HeaderText = "フリー３名称"
    ResultDetail.Columns(24).HeaderText = "フリー４№"
    ResultDetail.Columns(25).HeaderText = "フリー４名称"
    ResultDetail.Columns(26).HeaderText = "フリー５№"
    ResultDetail.Columns(27).HeaderText = "フリー５名称"
    ResultDetail.Columns(28).HeaderText = "製造者ｺｰﾄﾞ"
    ResultDetail.Columns(29).HeaderText = "製造者名"
    ResultDetail.Columns(30).HeaderText = "担当者ｺｰﾄﾞ"
    ResultDetail.Columns(31).HeaderText = "担当者名"
    ResultDetail.Columns(32).HeaderText = "ﾛｯﾄ1"
    ResultDetail.Columns(33).HeaderText = "区分"
    ResultDetail.Columns(34).HeaderText = "重量"
    ResultDetail.Columns(35).HeaderText = "単位"
    ResultDetail.Columns(36).HeaderText = "グロス単位"
    ResultDetail.Columns(37).HeaderText = "グロス重量単位"
    ResultDetail.Columns(38).HeaderText = "商品温度"
    ResultDetail.Columns(39).HeaderText = "商品温度単位"
    ResultDetail.Columns(40).HeaderText = "加工日"
    ResultDetail.Columns(41).HeaderText = "加工時刻"
    ResultDetail.Columns(42).HeaderText = "有効日"
    ResultDetail.Columns(43).HeaderText = "有効時刻"
    ResultDetail.Columns(44).HeaderText = "作業指示№"
    ResultDetail.Columns(45).HeaderText = "明細№"
    ResultDetail.Columns(46).HeaderText = "指示数"
    ResultDetail.Columns(47).HeaderText = "実績数"
    ResultDetail.Columns(48).HeaderText = "作業指示名称"
    ResultDetail.Columns(49).HeaderText = "削除フラグ"

    ResultDetail.Columns(3).Visible = False
    ResultDetail.Columns(4).Visible = False
    ResultDetail.Columns(5).Visible = False
    ResultDetail.Columns(6).Visible = False
    ResultDetail.Columns(7).Visible = False
    ResultDetail.Columns(8).Visible = False
    ResultDetail.Columns(9).Visible = False
    ResultDetail.Columns(10).Visible = False
    ResultDetail.Columns(11).Visible = False
    ResultDetail.Columns(12).Visible = False
    ResultDetail.Columns(13).Visible = False
    ResultDetail.Columns(14).Visible = False
    ResultDetail.Columns(15).Visible = False
    ResultDetail.Columns(16).Visible = False
    ResultDetail.Columns(17).Visible = False
    ResultDetail.Columns(18).Visible = False
    ResultDetail.Columns(19).Visible = False
    ResultDetail.Columns(20).Visible = False
    ResultDetail.Columns(21).Visible = False
    ResultDetail.Columns(22).Visible = False
    ResultDetail.Columns(23).Visible = False
    ResultDetail.Columns(24).Visible = False
    ResultDetail.Columns(25).Visible = False
    ResultDetail.Columns(26).Visible = False
    ResultDetail.Columns(27).Visible = False
    ResultDetail.Columns(28).Visible = False
    ResultDetail.Columns(29).Visible = False
    ResultDetail.Columns(32).Visible = False
    ResultDetail.Columns(33).Visible = False
    ResultDetail.Columns(36).Visible = False
    ResultDetail.Columns(37).Visible = False
    ResultDetail.Columns(38).Visible = False
    ResultDetail.Columns(39).Visible = False
    ResultDetail.Columns(40).Visible = False
    ResultDetail.Columns(41).Visible = False
    ResultDetail.Columns(42).Visible = False
    ResultDetail.Columns(43).Visible = False
    ResultDetail.Columns(44).Visible = False
    ResultDetail.Columns(45).Visible = False
    ResultDetail.Columns(46).Visible = False
    ResultDetail.Columns(47).Visible = False
    ResultDetail.Columns(48).Visible = False
    ResultDetail.Columns(49).Visible = False

    ' ヘッダーとセルの内容を中央寄せに設定
    For i As Integer = 0 To 49
      ResultDetail.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
      ResultDetail.Columns(i).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
    Next
    ResultDetail.Columns(34).DefaultCellStyle.Format = "0.0"

    ' マルチ選択を無効にする
    ResultDetail.MultiSelect = False

    ' 選択モードを全カラム選択に設定
    ResultDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    ' データを読み込むための関数を呼び出し
    SelectResults()

    If ResultDetail.Rows.Count > 0 Then
      ResultDetail.CurrentCell = ResultDetail.Rows(0).Cells(0)
      ResultDetail.Rows(0).Selected = True
    End If

    CustomizeDataGridViewHeader() ' ヘッダーのデザイン変更

  End Sub
  ' DataGridView のヘッダーのデザインを変更
  Private Sub CustomizeDataGridViewHeader()
    With ResultDetail
      ' ヘッダーの背景色を変更
      .EnableHeadersVisualStyles = False ' デフォルトの Windows スタイルを無効化
      .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
      .ColumnHeadersHeight = 40
      .ColumnHeadersDefaultCellStyle.BackColor = Color.LightGoldenrodYellow ' ヘッダーの背景色
      .ColumnHeadersDefaultCellStyle.ForeColor = Color.Black ' ヘッダーの文字色
      .ColumnHeadersDefaultCellStyle.Font = New Font("Meiryo", 16, FontStyle.Regular) ' フォント変更
      .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ' ヘッダー中央寄せ

      .AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
    End With
  End Sub
  Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
    '詳細画面の項目値セット
    SetListData()
    Form_ResultDetail.ShowDialog()
  End Sub

  Private Sub SetStaffNumberComboBox()
    Try
      Dim StaffNumberData As DataTable = GetDataStaffNumber()

      If StaffNumberData.Rows.Count = 0 Then
        ' エラーメッセージを表示して終了
        MessageBox.Show("担当者マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Else
        FromStaffCode_ComboBox.Items.Clear()
        ToStaffCode_ComboBox.Items.Clear()

        FromStaffCode_ComboBox.Items.Add("")
        ToStaffCode_ComboBox.Items.Add("")

        For Each row As DataRow In StaffNumberData.Rows
          Dim StaffNumber As String = row(0).ToString()
          FromStaffCode_ComboBox.Items.Add(StaffNumber)
          ToStaffCode_ComboBox.Items.Add(StaffNumber)
        Next
      End If
    Catch ex As Exception
      ComWriteErrLog(Me.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Private Function GetDataStaffNumber() As DataTable
    Dim tmpDb As New ClsSqlServer
    Dim tmpDt As New DataTable

    Try
      SqlServer.GetResult(tmpDt, GetSelectStaffMaster)
      Return tmpDt
    Catch ex As Exception
      ComWriteErrLog(Me.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
    End Try
  End Function

  Private Function GetSelectStaffMaster() As String
    Dim sql As String = String.Empty

    sql &= " SELECT CONVERT(VARCHAR, Staff_Number) + ' ' + Staff_Name"
    sql &= " FROM MST_Staff "
    sql &= " ORDER BY Staff_Number "

    Call WriteExecuteLog(Me.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Public Sub SelectResults()
    Dim tmpDb As New ClsSqlServer
    Dim tmpDt As New DataTable
    Try
      With tmpDb
        SqlServer.GetResult(tmpDt, GetSelectSql)
        WriteDetail(tmpDt, ResultDetail)
        If tmpDt.Rows.Count = 0 Then
          UpdateButton.Enabled = False
          DeleteButton.Enabled = False
        Else
          WriteDetail(tmpDt, ResultDetail)
          UpdateButton.Enabled = True
          DeleteButton.Enabled = True

          '行色付
          If ResultDetail.Rows.Count > 0 Then
            For i As Integer = 0 To ResultDetail.Rows.Count - 1
              If ResultDetail.Rows(i).Cells(49).Value = True Then
                ResultDetail.Rows(i).DefaultCellStyle.BackColor = Color.DarkGray
              End If
            Next
          End If
        End If

        CustomizeDataGridViewHeader

      End With
    Catch ex As Exception
      Call ComWriteErrLog(Me.GetType().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
    End Try
  End Sub

  Private Sub DisPlayDeleteRow(RowDisplayFlg As Boolean)
    '表示・非表示
    If ResultDetail.Rows.Count > 0 Then
      For i As Integer = 0 To ResultDetail.Rows.Count - 1
        If RowDisplayFlg Then
          If ResultDetail.Rows(i).Cells(49).Value = "0" Then
            ResultDetail.Rows(i).Visible = True
          End If
        Else
          If ResultDetail.Rows(i).Cells(49).Value = "1" Then
            ResultDetail.Rows(i).Visible = False
          End If
        End If
      Next
    End If
  End Sub

  Private Sub AddTotalRow()
    ' データグリッドビューの参照
    Dim dataGridView As DataGridView = Me.ResultDetail

    ' 合計値を計算する列のインデックス (35列目なので、インデックスは34)
    Dim columnIndex As Integer = 34

    ' 合計を計算する変数
    Dim total As Decimal = 0

    ' データグリッドビューの各行をループして合計を計算
    For Each row As DataGridViewRow In dataGridView.Rows
      If Not row.IsNewRow Then
        Dim cellValue As Object = row.Cells(columnIndex).Value
        Dim cellValueDecimal As Decimal

        If Decimal.TryParse(cellValue.ToString(), cellValueDecimal) Then
          total += cellValueDecimal
        End If
      End If
    Next

    ' 合計行を追加
    Dim totalRow As DataGridViewRow = CType(dataGridView.Rows(0).Clone(), DataGridViewRow)

    ' すべてのセルに空の値を設定し、合計を表示するセルに合計値を設定
    For i As Integer = 0 To dataGridView.Columns.Count - 1
      totalRow.Cells(i).Value = ""
    Next
    totalRow.Cells(columnIndex).Value = "合計: " & total.ToString()

    ' 合計行をデータグリッドビューに追加
    dataGridView.Rows.Add(totalRow)
  End Sub


  Private Function GetSelectSql() As String
    Dim tmpDb As New ClsSqlServer

    Dim sql As String = String.Empty

    Dim wkFromDate As String = DateTimeFrom.Text
    Dim wkToDate As String = DateTimeTo.Text

    ' フィルタリング条件に使用する一時的な変数を宣言
    Dim wkFromItemCode As String = String.Empty
    Dim wkToItemCode As String = String.Empty
    Dim wkFromStaffCode As String = String.Empty
    Dim wkToStaffCode As String = String.Empty

    ' StaffCode の範囲を設定
    If FromStaffCode_ComboBox.Text <> "" Then
      wkFromStaffCode = FromStaffCode_ComboBox.Text.Substring(0, FromStaffCode_ComboBox.Text.IndexOf(" "))
    ElseIf FromStaffCode_ComboBox.Items.Count > 0 Then
      wkFromStaffCode = FromStaffCode_ComboBox.Items(0).ToString().Split(" "c)(0)
    Else
      wkFromStaffCode = 0
    End If

    If ToStaffCode_ComboBox.Text <> "" Then
      wkToStaffCode = ToStaffCode_ComboBox.Text.Substring(0, ToStaffCode_ComboBox.Text.IndexOf(" "))
    ElseIf ToStaffCode_ComboBox.Items.Count > 0 Then
      wkToStaffCode = ToStaffCode_ComboBox.Items(ToStaffCode_ComboBox.Items.Count - 1).ToString().Split(" "c)(0)
    End If

    sql &= " SELECT "
    sql &= "     ROW_NUMBER() OVER (ORDER BY CAST(addition_date AS DATETIME) DESC, CAST(addition_time AS DATETIME) DESC, terminal_number) AS serial_number, "
    sql &= "     addition_date, "
    sql &= "     addition_time, "
    sql &= "     terminal_number, "
    sql &= "     call_code, "
    sql &= "     item_number, "
    sql &= "     item_name, "
    sql &= "     packing, "
    sql &= "     packing_unit, "
    sql &= "     packing1_weight, "
    sql &= "     packing1_weight_unit, "
    sql &= "     packing2_weight, "
    sql &= "     packing2_weight_unit, "
    sql &= "     packing2_multiplier, "
    sql &= "     packing1_number, "
    sql &= "     packing1_name, "
    sql &= "     packing2_number, "
    sql &= "     packing2_name, "
    sql &= "     free1_number, "
    sql &= "     free1_name, "
    sql &= "     free2_number, "
    sql &= "     free2_name, "
    sql &= "     free3_number, "
    sql &= "     free3_name, "
    sql &= "     free4_number, "
    sql &= "     free4_name, "
    sql &= "     free5_number, "
    sql &= "     free5_name, "
    sql &= "     manufacturer_code, "
    sql &= "     manufacturer_name, "
    sql &= "     RIGHT('0000' + CAST(staff_number AS VARCHAR), 4) AS staff_number, "
    sql &= "     staff_name, "
    sql &= "     lot1, "
    sql &= "     category,   "
    sql &= "     CAST(FLOOR(CAST(weight AS DECIMAL(18,3)) * 10) / 10.0 AS DECIMAL(18,1)) AS weight, "
    sql &= "     weight_unit, "
    sql &= "     gross_weight, "
    sql &= "     gross_weight_unit, "
    sql &= "     product_temperature, "
    sql &= "     product_temperature_unit, "
    sql &= "     processing_date, "
    sql &= "     processing_time, "
    sql &= "     valid_date, "
    sql &= "     valid_time, "
    sql &= "     work_instruction_number, "
    sql &= "     detail_number, "
    sql &= "     instruction_quantity, "
    sql &= "     actual_quantity, "
    sql &= "     work_instruction_name, "
    sql &= "     delete_flg "
    sql &= " FROM "
    sql &= "     TRN_Results "
    sql &= " WHERE "
    sql &= "    addition_date BETWEEN '" & wkFromDate & "' AND '" & wkToDate & "' "
    sql &= "    AND CAST(staff_number AS INT) BETWEEN '" & wkFromStaffCode & "' AND '" & wkToStaffCode & "'"
    sql &= " ORDER BY "
    sql &= "     CAST(addition_date AS DATETIME) DESC, "
    sql &= "     CAST(addition_time AS DATETIME) DESC, "
    sql &= "     terminal_number; "

    Call WriteExecuteLog(Me.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
    If Not CheckValue() Then
      Exit Sub
    End If

    SelectResults()
  End Sub

  Private Sub DateTimeFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimeFrom.KeyPress
    If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ControlChars.Back Or e.KeyChar = "/"c) Then
      e.Handled = True
    End If
  End Sub
  Private Sub DateTimeTo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimeTo.KeyPress
    If Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = ControlChars.Back Or e.KeyChar = "/"c) Then
      e.Handled = True
    End If
  End Sub

  Private Sub DateTimeFrom_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DateTimeFrom.Validating
    If ActiveControl.Name <> "DateTimeFrom" And ActiveControl.Name <> "CloseButton" Then

      Dim inputText As String = DateTimeFrom.Text.Replace("/", "").Trim()

      If DateTypeCheck(inputText) Then
        DateTimeFrom.Text = DateTxt2DateTxt(inputText)
      Else
        MessageBox.Show("正しい日付形式を入力してください。（ YYYY/MM/DD：西暦 ）", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        DateTimeFrom.SelectAll()
        e.Cancel = True
      End If
    End If
  End Sub

  Private Sub DateTimeTo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DateTimeTo.Validating
    If ActiveControl.Name <> "DateTimeTo" And ActiveControl.Name <> "CloseButton" Then
      Dim inputText As String = DateTimeTo.Text.Replace("/", "").Trim()

      If DateTypeCheck(inputText) Then
        DateTimeTo.Text = DateTxt2DateTxt(inputText)
      Else
        MessageBox.Show("正しい日付形式を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        DateTimeTo.SelectAll()
        e.Cancel = True
      End If
    End If
  End Sub
  Private Function CheckValue() As Boolean
    Dim dtFrom As DateTime
    Dim dtTo As DateTime
    Dim fromStaffCode As String
    Dim ToStaffCode As String = ""

    dtFrom = DateTime.Parse(DateTimeFrom.Text)
    dtTo = DateTime.Parse(DateTimeTo.Text)

    If FromStaffCode_ComboBox.SelectedIndex = -1 Or FromStaffCode_ComboBox.SelectedIndex = 0 Then
      fromStaffCode = 1.ToString("D" & StaffDigits)
    Else
      fromStaffCode = FromStaffCode_ComboBox.Text.Substring(0, StaffDigits)
    End If

    If ToStaffCode_ComboBox.SelectedIndex = -1 Or ToStaffCode_ComboBox.SelectedIndex = 0 Then
      ToStaffCode = ToStaffCode.PadLeft(StaffDigits, "9"c)
    Else
      ToStaffCode = ToStaffCode_ComboBox.Text.Substring(0, StaffDigits)
    End If

    Dim CheckResult As Boolean = True

    '日付の相関チェック
    If dtFrom > dtTo Then
      MessageBox.Show("開始日は終了日より前の日付を指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      CheckResult = False
    End If

    '担当者の相関チェック
    If fromStaffCode > ToStaffCode Then
      MessageBox.Show("担当者コード(開始)は担当者コード(終了)より前のコードを指定してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      CheckResult = False
    End If

    Return CheckResult
  End Function
  Private Sub SetListData()
    '選択している行の行番号の取得
    Dim i As Integer = ResultDetail.CurrentRow.Index
    Form_ResultDetail.additionDateTxTValue = ResultDetail.Rows(i).Cells(1).Value
    Form_ResultDetail.additionTimeTxTValue = ResultDetail.Rows(i).Cells(2).Value
    Form_ResultDetail.terminalNumberTxTValue = ResultDetail.Rows(i).Cells(3).Value
    Form_ResultDetail.callCodeTxTValue = ResultDetail.Rows(i).Cells(4).Value
    Form_ResultDetail.itemNoTxTValue = ResultDetail.Rows(i).Cells(5).Value
    Form_ResultDetail.itemNameTxTValue = ResultDetail.Rows(i).Cells(6).Value

    Form_ResultDetail.packingTxTValue = ResultDetail.Rows(i).Cells(7).Value
    Form_ResultDetail.packingUnitTxTValue = ResultDetail.Rows(i).Cells(8).Value
    Form_ResultDetail.packing1WeightTxTValue = ResultDetail.Rows(i).Cells(9).Value
    Form_ResultDetail.packing1WeightUnitTxTValue = ResultDetail.Rows(i).Cells(10).Value
    Form_ResultDetail.packing2WeightTxTValue = ResultDetail.Rows(i).Cells(11).Value
    Form_ResultDetail.packing2WeightUnitTxTValue = ResultDetail.Rows(i).Cells(12).Value
    Form_ResultDetail.packing2MultiplicationTxTValue = ResultDetail.Rows(i).Cells(13).Value
    Form_ResultDetail.packing1NumberTxTValue = ResultDetail.Rows(i).Cells(14).Value
    Form_ResultDetail.packing1NameTxTValue = ResultDetail.Rows(i).Cells(15).Value
    Form_ResultDetail.packing2NumberTxTValue = ResultDetail.Rows(i).Cells(16).Value
    Form_ResultDetail.packing2NameTxTValue = ResultDetail.Rows(i).Cells(17).Value

    Form_ResultDetail.free1NumberTxTValue = ResultDetail.Rows(i).Cells(18).Value
    Form_ResultDetail.free1NameTxTValue = ResultDetail.Rows(i).Cells(19).Value
    Form_ResultDetail.free2NumberTxTValue = ResultDetail.Rows(i).Cells(20).Value
    Form_ResultDetail.free2NameTxTValue = ResultDetail.Rows(i).Cells(21).Value
    Form_ResultDetail.free3NumberTxTValue = ResultDetail.Rows(i).Cells(22).Value
    Form_ResultDetail.free3NameTxTValue = ResultDetail.Rows(i).Cells(23).Value
    Form_ResultDetail.free4NumberTxTValue = ResultDetail.Rows(i).Cells(24).Value
    Form_ResultDetail.free4NameTxTValue = ResultDetail.Rows(i).Cells(25).Value
    Form_ResultDetail.free5NumberTxTValue = ResultDetail.Rows(i).Cells(26).Value
    Form_ResultDetail.free5NameTxTValue = ResultDetail.Rows(i).Cells(27).Value

    Form_ResultDetail.manufacturerCodeTxTValue = ResultDetail.Rows(i).Cells(28).Value
    Form_ResultDetail.manufacturerNameTxTValue = ResultDetail.Rows(i).Cells(29).Value
    Form_ResultDetail.staffNumberTxTValue = ResultDetail.Rows(i).Cells(30).Value
    Form_ResultDetail.staffNameTxTValue = ResultDetail.Rows(i).Cells(31).Value
    Form_ResultDetail.lot1TxTValue = ResultDetail.Rows(i).Cells(32).Value
    Form_ResultDetail.classificationTxTValue = ResultDetail.Rows(i).Cells(33).Value

    Form_ResultDetail.weightTxTValue = ResultDetail.Rows(i).Cells(34).Value
    Form_ResultDetail.weightUnitTxTValue = ResultDetail.Rows(i).Cells(35).Value
    Form_ResultDetail.grossWeightTxTValue = ResultDetail.Rows(i).Cells(36).Value
    Form_ResultDetail.grossWeightUnitTxTValue = ResultDetail.Rows(i).Cells(37).Value
    Form_ResultDetail.temperatureTxTValue = ResultDetail.Rows(i).Cells(38).Value
    Form_ResultDetail.temperatureUnitTxTValue = ResultDetail.Rows(i).Cells(39).Value

    Form_ResultDetail.processingDateTxTValue = ResultDetail.Rows(i).Cells(40).Value
    Form_ResultDetail.processingTimeTxTValue = ResultDetail.Rows(i).Cells(41).Value
    Form_ResultDetail.effectiveDateTxTValue = ResultDetail.Rows(i).Cells(42).Value
    Form_ResultDetail.effectiveTimeTxTValue = ResultDetail.Rows(i).Cells(43).Value
    Form_ResultDetail.workOrderNumberTxTValue = ResultDetail.Rows(i).Cells(44).Value
    Form_ResultDetail.detailNumberTxTValue = ResultDetail.Rows(i).Cells(45).Value
    Form_ResultDetail.instructionQtyTxTValue = ResultDetail.Rows(i).Cells(46).Value
    Form_ResultDetail.actualQtyTxTValue = ResultDetail.Rows(i).Cells(47).Value
    Form_ResultDetail.workOrderNameTxTValue = ResultDetail.Rows(i).Cells(48).Value
  End Sub

  Private Sub CsvExportButton_Click(sender As Object, e As EventArgs) Handles CsvExportButton.Click
    If ResultDetail.Rows.Count = 0 Then
      MessageBox.Show("CSVファイルを出力するデータがありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Exit Sub
    End If

    Dim fileName As String = ResultCsvPath & "Results" & ".CSV"

    Try
      ExportToCSV(ResultDetail, fileName, ResultCsvPath)
    Catch ex As Exception
      MessageBox.Show("エラーが発生しました: " & ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub
  Private Sub ExportToCSV(dgv As DataGridView, baseFileName As String, filePath As String)
    Dim timestamp As String = DateTime.Now.ToString("yyyyMMddHHmmss")
    Dim fullFileName As String = Path.Combine(filePath, baseFileName & "_" & timestamp & ".csv")

    ' ファイルに書き込み
    Using writer As New StreamWriter(fullFileName, False, System.Text.Encoding.UTF8)
      ' ヘッダー書き込み
      For i As Integer = 0 To dgv.Columns.Count - 1
        writer.Write(dgv.Columns(i).HeaderText)
        If i < dgv.Columns.Count - 1 Then writer.Write(",")
      Next
      writer.WriteLine()

      ' データ行書き込み
      For Each row As DataGridViewRow In dgv.Rows
        If Not row.IsNewRow Then
          For i As Integer = 0 To row.Cells.Count - 1
            writer.Write(row.Cells(i).Value?.ToString())
            If i < row.Cells.Count - 1 Then writer.Write(",")
          Next
          writer.WriteLine()
        End If
      Next
    End Using

    ' メッセージ表示
    MessageBox.Show("CSVファイルの出力が完了しました。", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    ' フォルダを開く
    Process.Start("explorer.exe", Path.GetDirectoryName(fullFileName))
  End Sub


  ' COMオブジェクトを解放するためのヘルパー関数
  Private Sub ReleaseObject(ByVal obj As Object)
    Try
      Marshal.ReleaseComObject(obj)
      obj = Nothing
    Catch ex As Exception
      obj = Nothing
    Finally
      GC.Collect()
    End Try
  End Sub
  Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
    DeleteResults()
  End Sub

  Private Sub DeleteResults()
    Dim sql As String = String.Empty
    Dim DeleteRowFlg As Boolean = ResultDetail.CurrentRow.Cells(49).Value
    Dim confirmation As String
    Dim msg1 As String
    Dim msg2 As String
    With tmpDb
      Try
        If DeleteRowFlg Then
          sql = GetDeleteSql("0")
          msg1 = "削除取消します。" & vbCrLf & "よろしいでしょうか。"
          msg2 = "削除取消処理完了しました。"
        Else
          sql = GetDeleteSql("1")
          msg1 = "削除します。" & vbCrLf & "よろしいでしょうか。"
          msg2 = "削除処理完了しました。"
        End If

        confirmation = MessageBox.Show(msg1, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmation = DialogResult.Yes Then
          ' SQL実行結果が1件か？
          If .Execute(sql) = 1 Then
            ' 更新成功
            .TrnCommit()
            MessageBox.Show(msg2, "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelectResults()
          End If
        Else
          Exit Sub
        End If
      Catch ex As Exception
        Call ComWriteErrLog([GetType]().Name,
                      System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      End Try
    End With
  End Sub

  Private Function GetDeleteSql(DeleteFlg As String) As String
    Dim sql As String = String.Empty
    Dim tmpdate As DateTime = CDate(ComGetProcTime())

    Dim wkAdditionDate As String = ResultDetail.Rows(ResultDetail.CurrentRow.Index).Cells(1).Value
    Dim wkAdditionTime As String = ResultDetail.Rows(ResultDetail.CurrentRow.Index).Cells(2).Value
    Dim wkTerminalNumber As String = ResultDetail.Rows(ResultDetail.CurrentRow.Index).Cells(3).Value

    sql &= " UPDATE TRN_Results"
    sql &= "    SET DELETE_FLG = '" & DeleteFlg & "'"
    sql &= "       ,UPDATE_DATE = '" & tmpdate & "'"
    sql &= " WHERE "
    sql &= "     addition_date = '" & wkAdditionDate & "'"
    sql &= " AND addition_time = '" & wkAdditionTime & "'"
    sql &= " AND terminal_number = '" & wkTerminalNumber & "'"
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub ResultDetail_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles ResultDetail.CellDoubleClick
    '詳細画面の項目値セット
    SetListData()
    Form_ResultDetail.ShowDialog()
  End Sub

  Private Sub Form_ResultList_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F1
        SearchButton.PerformClick()
      Case Keys.F2
        CsvExportButton.PerformClick()
      'Case Keys.F5
      '  CreateButton.PerformClick()
      Case Keys.F6
        UpdateButton.PerformClick()
      Case Keys.F7
        DeleteButton.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub

  Private Sub FromStaffCode_ComboBox_DropDown(sender As Object, e As EventArgs) Handles FromStaffCode_ComboBox.DropDown
    AdjustDropDownWidth(FromStaffCode_ComboBox)
  End Sub

  Private Sub ToStaffCode_ComboBox_DropDown(sender As Object, e As EventArgs) Handles ToStaffCode_ComboBox.DropDown
    AdjustDropDownWidth(ToStaffCode_ComboBox)
  End Sub
  Private Sub AdjustDropDownWidth(cb As ComboBox)
    Dim maxItemWidth As Integer = 0
    Using g As Graphics = cb.CreateGraphics()
      Dim font As Font = cb.Font
      For Each item In cb.Items
        Dim itemWidth As Integer = CInt(g.MeasureString(item.ToString(), font).Width)
        If itemWidth > maxItemWidth Then
          maxItemWidth = itemWidth
        End If
      Next
    End Using

    ' 現在の幅より狭いときだけ拡張する（）
    If maxItemWidth > cb.Width Then
      cb.DropDownWidth = maxItemWidth + 10
    Else
      cb.DropDownWidth = cb.Width
    End If
  End Sub

  Private Sub ResultDetail_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles ResultDetail.CellClick
    If ResultDetail.CurrentRow.Cells(49).Value Then
      DeleteButton.Text = "F7" & vbCrLf & "削除取消"
    Else
      DeleteButton.Text = "F7" & vbCrLf & "削除"
    End If
  End Sub

End Class
