Imports System.IO
Imports System.IO.SearchOption
Imports System.Text
Imports Common
Imports Common.ClsFunction
Imports Microsoft.VisualBasic.FileIO

Public Class Form_RealtimeConfirmation
  Dim CheckboxExistFlg As New Boolean
  Dim PathName As String
  Dim TableName As String
  Dim DefText As String

  Private ReadOnly FileNameDigits As String = ReadSettingIniFile("FILENAME_DIGITS", "VALUE")
  Private ReadOnly BackupPath As String = ReadSettingIniFile("BACKUP_PATH", "VALUE")

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
  Private Sub Form_RealtimeConfirmation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    'ユーザーからのデータ追加を不可にしておく
    MaximizeBox = False
    CheckboxExistFlg = True
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = "計量器通信" & " ( " & updateTime & " ) "
    Me.KeyPreview = True

    MaximizeBox = False
    FormBorderStyle = FormBorderStyle.FixedSingle
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub
  Private Function DuplicateCheck(PR_PROCESS_DATE As String, PR_PROCESS_TIME As String, PR_MACHINE_NO As String)
    Dim Result As Boolean
    Dim sql As String = String.Empty
    sql = GetDuplicateSelectSql(PR_PROCESS_DATE, PR_PROCESS_TIME, PR_MACHINE_NO)
    Try
      With tmpDb
        SqlServer.GetResult(tmpDt, sql)
        If tmpDt.Rows(0).Item("ROWC") = 0 Then
          Result = False
        Else
          Result = True
        End If
      End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name,
                  System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
    End Try
    Return Result
  End Function

  Private Function GetDuplicateSelectSql(PR_PROCESS_DATE As String, PR_PROCESS_TIME As String, PR_MACHINE_NO As String)
    Dim sql As String = String.Empty

    sql &= " Select"
    sql &= "     count(*) As ROWC"
    sql &= " From"
    sql &= "     TRN_Results"
    sql &= " Where"
    sql &= "     terminal_number = '" & PR_MACHINE_NO & "' "
    sql &= " and addition_date = '" & PR_PROCESS_DATE & "' "
    sql &= " and addition_time = '" & PR_PROCESS_TIME & "' "
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub USB_SendButton_Click(sender As Object, e As EventArgs) Handles USB_SendButton.Click
    Dim ScaleNumber As String
    Dim UsbPath As String = GetUsbDriveRootPath() ' USBメモリのパスを取得

    If String.IsNullOrEmpty(UsbPath) Then
      MessageBox.Show("USBメモリが見つかりません。USBメモリを挿入してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Exit Sub
    End If

    ' 保存先フォルダを設定
    UsbPath = Path.Combine(UsbPath, "IM-7000\TEMP")

    ' フォルダが存在しない場合は作成
    If Not Directory.Exists(UsbPath) Then
      Directory.CreateDirectory(UsbPath)
    End If

    ScaleNumber = "01"

        ' ファイルの保存処理
        CreateItemMasterCSV(ScaleNumber, UsbPath)
        CreateStaffMasterCSV(ScaleNumber, UsbPath)

    ' エクスプローラーでフォルダを開く
    Try
        Process.Start("explorer.exe", UsbPath)
        MessageBox.Show("データを保存しました。" & vbCrLf & "保存先: " & UsbPath, "完了", MessageBoxButtons.OK, MessageBoxIcon.Information)
      Catch ex As Exception
        MessageBox.Show("フォルダを開く際にエラーが発生しました。")
      End Try


  End Sub

  ''' <summary>
  ''' USBメモリのドライブを探し、そのパスを返す
  ''' </summary>
  ''' <returns>USBメモリのパス (例: "E:\")、見つからなければ空文字</returns>
  Private Function GetUsbDriveRootPath() As String
    For Each drive As DriveInfo In DriveInfo.GetDrives()
      If drive.DriveType = DriveType.Removable AndAlso drive.IsReady Then
        Return drive.RootDirectory.FullName ' USBが見つかったらルートパスを返す
      End If
    Next
    Return String.Empty ' USBが見つからない場合
  End Function

  Function GetInsertSql(columnNames() As String, dr As DataRow) As String
    Dim sql As String = "INSERT INTO TRN_Results ("
    Dim values As String = "VALUES ("

    ' カラム名と値をセット
    For i As Integer = 0 To columnNames.Length - 1

      Dim value As String = dr(i).ToString().Replace("'", "''")

      ' freeX_numberの場合は先頭に000を付与
      If columnNames(i) Like "free?_number" Then
        value = "000" & value
      End If

      sql &= columnNames(i)
      values &= "'" & value & "'"

      If i < columnNames.Length - 1 Then
        sql &= ", "
        values &= ", "
      End If
    Next

    sql &= ") " & values & ")"
    Call WriteExecuteLog("Form_RealtimeConfirmation", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub CreateItemMasterCSV(ScaleNumber As String, UsbPath As String)
    PathName = "40MAS1"
    TableName = "MST_Item"
    DefText = "呼出コード:40001,品番:40002,風袋:40007,風袋単位:40008,上限値:40009,上限値単位:40010,基準値:40011,基準値単位:40012,下限値:40013,下限値単位:40014,小計目標値:40028,小計目標値単位:40029,小計目標回数:40030,品名:40031"
    CreateCsv(PathName, TableName, DefText, ScaleNumber, UsbPath)
  End Sub

  Private Sub CreateStaffMasterCSV(ScaleNumber As String, UsbPath As String)
    PathName = "40OPTR"
    TableName = "MST_Staff"
    DefText = "担当者№:40361,担当者名称:40362"
    CreateCsv(PathName, TableName, DefText, ScaleNumber, UsbPath)
  End Sub

  Private Sub CreateCsv(PathName As String, TableName As String, DefText As String, ScaleNumber As String, UsbPath As String)
    Dim CsvPath As String
    Dim DefPath As String
    Dim isWriteHeader As Boolean = True
    Dim sql As String = String.Empty
    Dim OutputDb As New DataTable
    Dim OutputDt As New DataTable

    CsvPath = UsbPath & "\" & PathName & FileNameDigits & ScaleNumber & ".CSV"
    DefPath = UsbPath & "\" & PathName & FileNameDigits & ScaleNumber & ".DEF"
    'CSVファイル出力時に使うEncoding
    '「Shift_JIS」を使用
    Dim encoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
    '書き込むファイルを開く
    Dim wrCsv As New System.IO.StreamWriter(CsvPath, False, encoding)
    Dim wrDef As New System.IO.StreamWriter(DefPath, False, encoding)
    wrDef.Write(DefText)
    wrDef.Close()

    Select Case TableName
      Case "MST_Item"
        sql = GetItemMasterSelectSql()
      Case "MST_Staff"
        sql = GetStaffMasterSelectSql()
    End Select

    Try
      With OutputDb
        SqlServer.GetResult(OutputDt, sql)
        If OutputDt.Rows.Count = 0 Then
          Select Case TableName
            Case "MST_Item"
              MessageBox.Show("商品マスタのデータがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Case "MST_Staff"
              MessageBox.Show("担当者マスタのデータがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End Select
        Else
          Dim colCount As Integer = OutputDt.Columns.Count
          Dim lastColIndex As Integer = colCount - 1
          Dim i As Integer
          'ヘッダを書き込む
          If isWriteHeader Then
            For i = 0 To colCount - 1
              'ヘッダの取得
              Dim field As String = OutputDt.Columns(i).Caption
              '"で囲み書き込む
              field = EncloseDoubleQuotes(field)
              wrCsv.Write(field)
              'カンマ付与
              If lastColIndex > i Then
                wrCsv.Write(","c)
              End If
            Next
            '改行
            wrCsv.Write(vbCrLf)
          End If
          'レコードを書き込む
          Dim row As DataRow
          For Each row In OutputDt.Rows
            For i = 0 To colCount - 1
              'フィールドの取得
              Dim field As String = row(i).ToString()
              '"で囲み書き込む
              field = EncloseDoubleQuotes(field)
              wrCsv.Write(field)
              'カンマ付与
              If lastColIndex > i Then
                wrCsv.Write(","c)
              End If
            Next
            '改行
            wrCsv.Write(vbCrLf)
          Next
          '閉じる
          wrCsv.Close()
        End If
      End With
    Catch ex As Exception
      Call ComWriteErrLog([GetType]().Name,
                        System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
      Throw New Exception(ex.Message)
    Finally
      OutputDt.Dispose()
    End Try
  End Sub
  Private Function EncloseDoubleQuotes(field As String) As String
    Return "" & field & ""
  End Function
  Private Function GetItemMasterSelectSql() As String
    Dim sql As String = String.Empty
    sql &= " SELECT "
    sql &= "     call_code As 呼出コード, "
    sql &= "     item_number As 品番, "
    sql &= "     packing_bag As 風袋, "
    sql &= "     packing_bag_unit As 風袋単位, "
    sql &= "     upper_limit As 上限値, "
    sql &= "     upper_limit_unit As 上限値単位, "
    sql &= "     reference_value As 基準値, "
    sql &= "     reference_value_unit As 基準値単位, "
    sql &= "     lower_limit As 下限値, "
    sql &= "     lower_limit_unit As 下限値単位, "
    sql &= "     subtotal_target_value As 小計目標値, "
    sql &= "     subtotal_target_value_unit As 小計目標値単位, "
    sql &= "     subtotal_target_count As 小計目標回数, "
    sql &= "     item_name As 品名 "
    sql &= " FROM "
    sql &= "     MST_Item "
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetStaffMasterSelectSql() As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     Staff_Number As [担当者№],"
    sql &= "     Staff_Name As [担当者名称]"
    sql &= " FROM"
    sql &= "     MST_Staff"
    Call WriteExecuteLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Sub USB_ReceiveButton_Click(sender As Object, e As EventArgs) Handles USB_ReceiveButton.Click
    Dim DuplicateCount As Integer = 0
    Dim InsertCount As Integer = 0
    Dim tmpDb As New ClsSqlServer

    ' USBメモリのパスを取得
    Dim usbPath As String = GetUsbDriveRootPath()
    If String.IsNullOrEmpty(usbPath) Then
      MessageBox.Show("USBメモリが見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Exit Sub
    End If

    ' バックアップパスを取得（フォルダが存在しない場合、バックアップフォルダ作成）
    If String.IsNullOrEmpty(BackupPath) Then
      Directory.CreateDirectory(BackupPath)
    End If

    ' 確認ダイアログ
    Dim confirmation As DialogResult
    confirmation = MessageBox.Show("実績取り込みを開始しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    If confirmation <> DialogResult.Yes Then Exit Sub

    Try
      ' USB内の該当ファイルを取得
      Dim usbDir As String = Path.Combine(usbPath, "IM-7000\TEMP")
      Dim files As String() = Directory.GetFiles(usbDir, "40TRAN*.CSV", System.IO.SearchOption.TopDirectoryOnly)

      If files.Length = 0 Then
        MessageBox.Show("該当するCSVファイルが見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Exit Sub
      End If

      For Each filePath In files

        InsertCount = 0
        DuplicateCount = 0
        ' ファイルパスから号機番号を取得
        Dim fileName As String = System.IO.Path.GetFileNameWithoutExtension(filePath) ' ファイル名（拡張子なし）
        Dim machineNumber As String = fileName.Substring(fileName.Length - 2, 2) & "号機" ' 号機番号 + "号機"

        ' CSVを処理
        Dim dt As New DataTable("TRN_Results")

        ' CSVカラム定義（既存と同じ）
        Dim columnNames() As String = {
          "addition_date", "addition_time", "terminal_number", "call_code", "item_number",
          "item_name", "packing", "packing_unit", "weight", "weight_unit",
          "manufacturer_code", "manufacturer_name", "lot1", "lot2", "category",
          "gross_weight", "gross_weight_unit", "packing1_weight", "packing1_weight_unit",
          "packing2_weight", "packing2_weight_unit", "packing2_multiplier",
          "packing1_number", "packing1_name", "packing2_number", "packing2_name",
          "staff_number", "staff_name", "free1_number", "free1_name", "free2_number", "free2_name",
          "free3_number", "free3_name", "free4_number", "free4_name", "free5_number", "free5_name",
          "processing_date", "processing_time", "valid_date", "valid_time",
          "work_instruction_number", "detail_number", "instruction_quantity", "actual_quantity",
          "work_instruction_name", "product_temperature", "product_temperature_unit",
          "create_date", "update_date"
      }
        ' データテーブルにカラム追加
        For Each columnName As String In columnNames
          dt.Columns.Add(New DataColumn(columnName, GetType(String)))
        Next

        ' CSVを読み込む（Shift-JISを想定）
        Using textParser As New TextFieldParser(filePath, System.Text.Encoding.GetEncoding("Shift-JIS"))
          textParser.TextFieldType = FieldType.Delimited
          textParser.SetDelimiters(",")

          ' ヘッダーをスキップ
          If Not textParser.EndOfData Then
            textParser.ReadFields()
          End If

          ' 各行を処理
          While Not textParser.EndOfData
            Dim csvRow As String() = textParser.ReadFields()
            Dim dr As DataRow = dt.NewRow()

            ' 行データ追加
            Dim i As Integer
            For i = 0 To Math.Min(csvRow.Length - 1, columnNames.Length - 1)
              dr(i) = csvRow(i)
            Next

            ' 実行時の時刻を追加
            dr("create_date") = DateTime.Now.ToString("yyyy-MM-dd")
            dr("update_date") = DateTime.Now.ToString("yyyy-MM-dd")

            ' 重複チェック
            If DuplicateCheck(dr(0), dr(1), dr(2)) Then
              DuplicateCount += 1
              Continue While
            End If

            dt.Rows.Add(dr)
          End While
        End Using

        ' データをDBに登録
        For Each dr As DataRow In dt.Rows
          Dim sql As String = GetInsertSql(columnNames, dr)
          With tmpDb
            Try
              If .Execute(sql) = 1 Then
                .TrnCommit()
                InsertCount += 1
              Else
                MessageBox.Show("実績管理の登録処理に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
              End If
            Catch ex As Exception
              Call ComWriteErrLog([GetType]().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message)
              Throw New Exception(ex.Message)
            End Try
          End With
        Next

        ' 処理結果メッセージ
        If InsertCount > 0 Then
          If DuplicateCount > 0 Then
            MessageBox.Show(
            machineNumber & "：" & InsertCount & "件 取り込み完了しました。" & vbCrLf &
            "既に登録されている実績データが" & DuplicateCount & "件存在します。",
            "確認", MessageBoxButtons.OK, MessageBoxIcon.Information
        )
          Else
            MessageBox.Show(
            machineNumber & "：" & InsertCount & "件 取り込み完了しました。",
            "完了", MessageBoxButtons.OK, MessageBoxIcon.Information
        )
          End If
        Else
          If DuplicateCount > 0 Then
            MessageBox.Show(
            machineNumber & "：取り込み失敗しました。" & vbCrLf &
            "既に登録されている実績データが" & DuplicateCount & "件存在します。",
            "確認", MessageBoxButtons.OK, MessageBoxIcon.Error
        )
          Else
            MessageBox.Show(
            machineNumber & "：取り込みに失敗しました。データがありません。",
            "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error
        )
          End If
        End If

        ' ----------------------------------------------------
        ' 取り込みが完了した CSVファイルをバックアップ用に
        ' タイムスタンプ付きファイル名に変更しコピー → USB 上の元ファイル削除
        ' ----------------------------------------------------
        If dt.Rows.Count > 0 Then
          Try
            ' 新しいファイル名作成（例: 40TRAN999_20250227142801.CSV）
            Dim timeStamp As String = DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim newFileName As String = Path.GetFileNameWithoutExtension(filePath) & "_" & timeStamp & Path.GetExtension(filePath)
            Dim newFilePath As String = Path.Combine(BackupPath, newFileName)

            ' コピー先のディレクトリが存在しない場合は作成
            If Not Directory.Exists(BackupPath) Then
              Directory.CreateDirectory(BackupPath)
            End If

            ' ファイルをコピーして、コピーできたら USB のファイルを削除
            File.Copy(filePath, newFilePath, overwrite:=False)

            ' 元ファイルとコピー先のファイルのサイズを比較
            If File.Exists(newFilePath) AndAlso
                 New FileInfo(filePath).Length = New FileInfo(newFilePath).Length Then
              File.Delete(filePath)
            Else
              MessageBox.Show("コピー検証に失敗しました。元ファイルは削除されません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If


          Catch ex As Exception
            MessageBox.Show("バックアップファイルの処理に失敗しました：" & ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End Try
        End If
      Next
    Catch ex As Exception
      MessageBox.Show("エラーが発生しました：" & ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
  End Sub


  Private Sub Form_RealtimeConfirmation_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F1
        USB_ReceiveButton.PerformClick()
      Case Keys.F2
        USB_SendButton.PerformClick()
      Case Keys.Escape
        Me.Close()
    End Select
  End Sub
End Class
