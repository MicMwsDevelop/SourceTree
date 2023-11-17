USE [JunpDB]
GO

DECLARE @InputDate as datetime
SET @InputDate = '2021-09-24'

SELECT choose(SL.区分No,
              '保守前受月額売上', 
              'ＰＣＡ－売上計上対象',
              'ＷＷ受注残－ＶＰ',
              'ＷＷ受注残－ＶＰ以外') as 売上区分
      , SL.部門コード as 部門コード
      , RTRIM(BM.emsb_str) as 部門名
      , SL.商品区分２ as 商品区分コード
      , KM.ems_str as 商品区分名
      , CAST(SL.金額 as integer) as 金額
FROM (
    SELECT
           2 as 区分No
          ,RTRIM(D.sykd_jbmn) as 部門コード
          ,SUM(D.sykd_kingaku) as 金額
          ,M.sms_skbn2 as 商品区分２
    FROM vMicPCA売上明細 as D
    INNER JOIN vMicPCA商品マスタ as M on M.sms_scd = D.sykd_scd
    WHERE D.sykd_kingaku <> 0
      AND D.sykd_uribi > CAST(CONVERT(nvarchar, EOMONTH(@InputDate, -1), 112) as integer)
      AND D.sykd_uribi <= CAST(CONVERT(nvarchar, EOMONTH(@InputDate, 0), 112) as integer)
      AND M.sms_skbn2 <> 5 AND M.sms_skbn2 <> 3
    GROUP BY D.sykd_jbmn, M.sms_skbn2

  UNION
    SELECT IIF(H.f販売種別 = 1, 3, 4)                          -- as 区分No
          , RIGHT('00' + CAST(B.fPca部門コード as varchar), 3) -- as 部門コード
          , SUM(D.f提供価格)                                   -- as 金額
          , D.f商品区分2                                       -- as 商品区分２
    FROM tMih受注ヘッダ as H
      INNER JOIN tMih受注詳細 as D on D.f受注番号 = H.f受注番号
      LEFT JOIN tMih支店情報 as B on B.f支店コード = H.fBshCode3
    WHERE H.f売上計上日 IS NULL
      AND H.f納期 > EOMONTH(@InputDate, -1)
      AND H.f納期 <= EOMONTH(@InputDate, 0)
      AND D.f商品区分2 <> 5 AND D.f商品区分2 <> 3
    GROUP BY H.f販売種別, B.fPca部門コード, D.f商品区分2
) SL
LEFT JOIN vMicPCA部門マスタ as BM on BM.emsb_kbn = SL.部門コード
LEFT JOIN vMicPCA区分マスタ as KM on KM.ems_kbn = SL.商品区分２ AND KM.ems_id = 22

--where SL.部門コード = '081'

