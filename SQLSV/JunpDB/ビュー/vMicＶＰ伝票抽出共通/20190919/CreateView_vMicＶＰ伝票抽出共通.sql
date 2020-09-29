USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicＶＰ伝票抽出共通]    Script Date: 2019/09/20 9:19:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--CREATE VIEW [dbo].[vMicＶＰ伝票抽出共通]
ALTER VIEW [dbo].[vMicＶＰ伝票抽出共通]
AS
/*
    vMicＶＰ伝票抽出共通

      Ver.1.00 2017/01/05 初版
      Ver.1.10 2017/06/22 受注見込,仮登録も受注予定
      Ver.1.20 2018/05/31 赤伝票の件数を反映するため[システム本数]項目を追加
      Ver.1.21 2018/11/22 パック区分にまとめを含める
      Ver.1.22 2019/09/19 パック区分のまとめの判定に800158 MWS おまとめﾌﾟﾗﾝ48ケ月、800159 MWS おまとめﾌﾟﾗﾝ60ケ月が含まれていなかったので判定方法をPCA商品コードから商品区分3に変更 by SUGURO

      納期日付が前月～翌月で販売種別＝ＶＰの伝票を抽出する

*/
  select              iif(D.f商品コード = '800152', 'ＱＰ', iif(D .f区分 = 204, 'まとめ', 'ＶＰ')) AS パック区分, iif(H.fリプレース = '自社Ｒ', '自社Ｒ', iif(H.fリプレース = '新規' OR
                        H.fリプレース = '新開', '新規・新開', iif(H.fリプレース = 'その他' OR
                        H.fリプレース = 'リプレースなし', '不明', '他社Ｒ'))) AS リプレース区分, H.fリプレース AS リプレース, H.f担当支店名 AS 担当支店名, H.f担当者名 AS 担当者名, CONVERT(nvarchar, H.f受注承認日, 111) AS 受注承認日, 
                        CONVERT(nvarchar, H.f出荷完了日, 111) AS 出荷完了日, CONVERT(nvarchar, H.f売上承認日, 111) AS 売上承認日, H.f納期 AS 納期, iif(H.f売上承認日 IS NOT NULL, '売上', iif(H.f受注承認日 IS NOT NULL, '受注承認', 
                        iif((H.f件名 LIKE '%作成中%') OR
                        (H.f件名 LIKE '%受注見込%') OR
                        (H.f件名 LIKE '%仮登録%'), '受注予定', '受注'))) AS ステータス, H.f受注番号 AS 受注番号, H.f案件ＩＤ AS 案件ＩＤ, H.f受注金額 AS 受注金額, D .f数量 AS システム本数, H.f販売先 AS 販売先, 
                        H.fユーザーコード AS ユーザーコード, H.fユーザー AS ユーザー, H.fSV利用終了年月 AS VP利用終了年月, H.f件名 AS 件名, H.f支払依頼備考 AS 支払依頼備考, H.f備考 AS 備考, iif((((H.f売上承認日 > eomonth(getdate(), 
                        - 2)) AND (H.f売上承認日 <= eomonth(getdate(), - 1))) /* 売上承認が前月 あるいは*/ OR
                        ((H.f売上承認日 IS NULL) /* 売上未承認で*/ AND (((H.f出荷完了日 > eomonth(getdate(), - 2)) AND (H.f出荷完了日 <= eomonth(getdate(), - 1)))))), - 1, 
                        /* 出荷完了日が前月   → -1:前月分*/ iif((((H.f売上承認日 > eomonth(getdate(), - 1)) AND (H.f売上承認日 <= eomonth(getdate(), 0))) /* 売上承認が当月 あるいは*/ OR
                        ((H.f売上承認日 IS NULL) /* 売上未承認で*/ AND (((H.f出荷完了日 > eomonth(getdate(), - 1)) AND (H.f出荷完了日 <= eomonth(getdate(), 0))) /* 出荷完了日が当月 あるいは*/ OR
                        (((H.f納期 > eomonth(getdate(), - 1)) AND (H.f納期 <= eomonth(getdate(), 0))))))), 0, /* 納期が当月         → 0:当月分*/ iif(((H.f売上承認日 IS NULL) /* 売上未承認で*/ AND (((H.f出荷完了日 > eomonth(getdate(), 0)) 
                        AND (H.f出荷完了日 <= eomonth(getdate(), 1))) /* 出荷完了日が翌月  あるいは*/ OR
                        ((H.f納期 > eomonth(getdate(), 0)) AND (H.f納期 <= eomonth(getdate(), 1))))), 1, 999))) /* 納期が翌月         → +1:翌月分*/ AS 前月当月翌月/* -1:前月分、0:当月分、+1:翌月分*/ , H.fBshCode3 AS 支店コード
FROM              JunpDB.dbo.tMih受注ヘッダ H INNER JOIN
                        JunpDB.dbo.tMih受注詳細 D ON D .f受注番号 = H.f受注番号
WHERE             ((H.f販売種別 = 1 OR H.f販売種別 = 4) AND (D .f区分 = 202 OR D .f区分 = 204)) AND (((H.f売上承認日 > eomonth(getdate(), - 2)) AND (H.f売上承認日 <= eomonth(getdate(), 1))) /* 売上承認が前月～当月～翌月 あるいは*/ OR
                        ((H.f売上承認日 IS NULL) /* 売上未承認で*/ AND (((H.f出荷完了日 > eomonth(getdate(), - 2)) AND ((H.f出荷完了日 <= eomonth(getdate(), 1)))) /* 出荷完了日が前月～翌月 あるいは*/ OR
                        (((H.f納期 > eomonth(getdate(), - 1)) AND (H.f納期 <= eomonth(getdate(), 1)))))))

GO
