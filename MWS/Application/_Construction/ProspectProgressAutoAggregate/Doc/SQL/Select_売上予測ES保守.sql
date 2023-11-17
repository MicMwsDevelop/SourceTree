use junpdb

SELECT
部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上月
FROM
(
SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上1年目 AS 計上月 FROM vMicES保守売上予測
UNION SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上2年目 AS 計上月 FROM vMicES保守売上予測
UNION SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上3年目 AS 計上月 FROM vMicES保守売上予測
UNION SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上4年目 AS 計上月 FROM vMicES保守売上予測
UNION SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上5年目 AS 計上月 FROM vMicES保守売上予測
UNION SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上6年目 AS 計上月 FROM vMicES保守売上予測
) AS ES
WHERE 計上月 >= '2021/08' AND 計上月 <= '2021/08'
ORDER BY 部門コード, 顧客No, 計上月

--, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicES売上予想]
--, start.FirstDayOfTheMonth().ToYearMonth().ToString()
--, end.LastDayOfTheMonth().ToYearMonth().ToString()
