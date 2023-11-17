SELECT
 choose(SL.区分No,'保守前受月額売上','ＰＣＡ－売上計上対象','ＷＷ受注残－ＶＰ','ＷＷ受注残－ＶＰ以外') AS 売上区分
,SL.部門コード AS 部門コード
,RTRIM(BM.emsb_str) AS 部門名
,SL.商品区分２ AS 商品区分コード
,KM.ems_str as 商品区分名
,CAST(SL.金額 AS integer) AS 金額
FROM (
SELECT
2 AS 区分No
,RTRIM(D.sykd_jbmn) AS 部門コード
,SUM(D.sykd_kingaku) AS 金額
,M.sms_skbn2 AS 商品区分２
FROM vMicPCA売上明細 AS D
INNER JOIN vMicPCA商品マスタ AS M on M.sms_scd = D.sykd_scd
WHERE D.sykd_kingaku <> 0
AND D.sykd_uribi >= 20220501
AND D.sykd_uribi <= 20220531
AND M.sms_skbn2 <> 5 AND M.sms_skbn2 <> 3
GROUP BY D.sykd_jbmn, M.sms_skbn2

UNION
									
SELECT 
 IIF(H.f販売種別 = 1, 3, 4)
,RIGHT('00' + CAST(B.fPca部門コード as varchar), 3)
,SUM(D.f提供価格)
,D.f商品区分2
FROM tMih受注ヘッダ AS H
INNER JOIN .tMih受注詳細 AS D ON D.f受注番号 = H.f受注番号
LEFT JOIN tMih支店情報 AS B ON B.f支店コード = H.fBshCode3
WHERE H.f売上計上日 IS NULL
AND H.f納期 >= '2022/05/01'
AND H.f納期 <= '2022/05/31'
AND D.f商品区分2 <> 5 AND D.f商品区分2 <> 3
GROUP BY H.f販売種別, B.fPca部門コード, D.f商品区分2
) AS SL
LEFT JOIN vMicPCA部門マスタ AS BM ON BM.emsb_kbn = SL.部門コード
LEFT JOIN vMicPCA区分マスタ AS KM ON KM.ems_kbn = SL.商品区分２ AND KM.ems_id = 22


--, today.FirstDayOfTheMonth().ToIntYMD()0
--, today.LastDayOfTheMonth().ToIntYMD()1
--, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]2
--, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]3
--, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]4
--, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注詳細]5
--, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]6
--, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA部門マスタ]7
--, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA区分マスタ]8
--, today.FirstDayOfTheMonth().ToString()9
--, today.LastDayOfTheMonth().ToString());10
