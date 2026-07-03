/*
  茶摘報酬集計表用テストデータ（200名）

  作業者番号 : 8001 ～ 8200
  作業日     : 2026/06/01 ～ 2026/06/30
  実績件数   : 1名につき1件

  同じ内容で再実行しても、登録済みの作業者・実績は追加しません。
*/

SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @StartStaffNumber int = 8001;
DECLARE @StaffCount int = 200;
DECLARE @StartDate date = '2026-06-01';
DECLARE @EndDate date = '2026-06-30';
DECLARE @TestTerminal varchar(99) = 'TEST200';

BEGIN TRY
    BEGIN TRANSACTION;

    /* 1. 作業者マスタを200名登録 */
    ;WITH Numbers AS
    (
        SELECT 1 AS No
        UNION ALL
        SELECT No + 1
        FROM Numbers
        WHERE No < @StaffCount
    )
    INSERT INTO MST_Staff
    (
        Staff_Number,
        Staff_Name,
        create_date,
        update_date
    )
    SELECT
        CONVERT(nvarchar(4), @StartStaffNumber + No - 1),
        'TEST' + RIGHT('0000' + CONVERT(varchar(4), No), 4),
        GETDATE(),
        GETDATE()
    FROM Numbers
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM MST_Staff AS s
        WHERE s.Staff_Number = CONVERT(nvarchar(4), @StartStaffNumber + No - 1)
    )
    OPTION (MAXRECURSION 0);

    /* 2. 1名につき1件の計量実績を登録 */
    ;WITH Numbers AS
    (
        SELECT 1 AS No
        UNION ALL
        SELECT No + 1
        FROM Numbers
        WHERE No < @StaffCount
    ),
    TestResults AS
    (
        SELECT
            No,
            CONVERT(varchar(4), @StartStaffNumber + No - 1) AS StaffNumber,
            'TEST' + RIGHT('0000' + CONVERT(varchar(4), No), 4) AS StaffName,
            DATEADD(day, (No - 1) % 30, @StartDate) AS AdditionDate,
            TIMEFROMPARTS(8 + ((No - 1) / 30), 0, 0, 0, 0) AS AdditionTime,
            CONVERT(varchar(20), CONVERT(decimal(10, 1), 10.0 + (((No - 1) % 50) / 10.0))) AS Weight
        FROM Numbers
    )
    INSERT INTO TRN_Results
    (
        addition_date,
        addition_time,
        terminal_number,
        item_number,
        item_name,
        weight,
        weight_unit,
        staff_number,
        staff_name,
        delete_flg,
        create_date,
        update_date
    )
    SELECT
        tr.AdditionDate,
        tr.AdditionTime,
        @TestTerminal,
        'TEST',
        'TEST',
        tr.Weight,
        'kg',
        tr.StaffNumber,
        tr.StaffName,
        '0',
        CONVERT(varchar(19), GETDATE(), 120),
        CONVERT(varchar(19), GETDATE(), 120)
    FROM TestResults AS tr
    WHERE tr.AdditionDate BETWEEN @StartDate AND @EndDate
      AND NOT EXISTS
      (
          SELECT 1
          FROM TRN_Results AS r
          WHERE r.staff_number = tr.StaffNumber
            AND r.addition_date BETWEEN @StartDate AND @EndDate
            AND ISNULL(r.delete_flg, '0') = '0'
      )
    OPTION (MAXRECURSION 0);

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;

/* 登録結果確認 */
SELECT
    COUNT(*) AS StaffCount
FROM MST_Staff
WHERE TRY_CONVERT(bigint, Staff_Number)
      BETWEEN @StartStaffNumber AND @StartStaffNumber + @StaffCount - 1;

SELECT
    COUNT(*) AS ResultCount,
    COUNT(DISTINCT staff_number) AS ResultStaffCount,
    MIN(addition_date) AS MinAdditionDate,
    MAX(addition_date) AS MaxAdditionDate
FROM TRN_Results
WHERE TRY_CONVERT(bigint, staff_number)
      BETWEEN @StartStaffNumber AND @StartStaffNumber + @StaffCount - 1
  AND addition_date BETWEEN @StartDate AND @EndDate
  AND ISNULL(delete_flg, '0') = '0';

/*
  テストデータ削除用（必要なときだけ実行）

BEGIN TRANSACTION;

DELETE FROM TRN_Results
WHERE terminal_number = 'TEST200'
  AND TRY_CONVERT(int, staff_number) BETWEEN 8001 AND 8200;

DELETE FROM MST_Staff
WHERE TRY_CONVERT(int, Staff_Number) BETWEEN 8001 AND 8200;

COMMIT TRANSACTION;
*/
