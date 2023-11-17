SELECT
choose(SL.区分No, '保守前受月額売上', 'ＰＣＡ－売上計上対象', 'ＷＷ受注残－ＶＰ', 'ＷＷ受注残－ＶＰ以外') as 売上区分
, SL.部門コード as 部門コード
, RTRIM(BM.emsb_str) as 部門名
, SL.商品区分２ as 商品区分コード
, KM.ems_str as 商品区分名
, CAST(SL.金額 as integer) as 金額
FROM (
		SELECT
		2 as 区分No
		, RTRIM(D.sykd_jbmn) as 部門コード
		, SUM(D.sykd_kingaku) as 金額
		, M.sms_skbn2 as 商品区分２
		FROM vMicPCA売上明細 as D
		INNER JOIN vMicPCA商品マスタ as M on M.sms_scd = D.sykd_scd
		WHERE D.sykd_kingaku <> 0
		AND D.sykd_uribi >= 20210901
		AND D.sykd_uribi <= 20210930
		AND NOT(M.sms_skbn2 = 5)
		GROUP BY D.sykd_jbmn, M.sms_skbn2
	UNION
		SELECT IIF(H.f販売種別 = 1, 3, 4)
		, RIGHT('00' + CAST(B.fPca部門コード as varchar), 3)
		, SUM(D.f提供価格)
		, D.f商品区分2
		FROM tMih受注ヘッダ as H
		INNER JOIN tMih受注詳細 as D on D.f受注番号 = H.f受注番号
		LEFT JOIN tMih支店情報 as B on B.f支店コード = H.fBshCode3
		WHERE H.f売上計上日 IS NULL
			AND H.f納期 >= '2021/09/01'
			AND H.f納期 <= '2021/09/30'
			AND NOT(D.f商品区分2 = 5)
		GROUP BY H.f販売種別, B.fPca部門コード, D.f商品区分2
	) SL
	LEFT JOIN vMicPCA部門マスタ as BM on BM.emsb_kbn = SL.部門コード
	LEFT JOIN vMicPCA区分マスタ as KM on KM.ems_kbn = SL.商品区分２ AND KM.ems_id = 22

	--where SL.商品区分２ = 1


								-- 								, today.FirstDayOfTheMonth().ToIntYMD()		0
								--, today.LastDayOfTheMonth().ToIntYMD()		1
								--, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]	2
								--, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]	3
								--, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]	4
								--, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注詳細]	5
								--, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]	6
								--, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA部門マスタ]	7
								--, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA区分マスタ]	8
								--, today.FirstDayOfTheMonth().ToString()	9
								--, today.LastDayOfTheMonth().ToString());	10
