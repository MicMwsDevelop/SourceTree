USE [charlieDB]
GO

/****** Object:  View [dbo].[V_CLIENT_INFO]    Script Date: 2020/06/09 17:32:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--CREATE VIEW [dbo].[V_CLIENT_INFO]
alter VIEW [dbo].[V_CLIENT_INFO]
as
/*

    V_CLIENT_INFO

        ＭＷＳエントリーサイト および ＭＷＳ顧客情報変更サイト
        で参照する医院情報をWonderWebの顧客データから取り出すビュー

        Ver.1.00 2015/06/11 初版 
        Ver.1.10 2015/06/22 全ての歯科医院を抽出するため、見込み客とユーザーを対象とした
        Ver.1.20 2015/07/01 住所フリガナ、開設者フリガナ、院長名フリガナ を削除
                            銀行名、支店名、口座番号 を追加
        Ver.1.30 2015/07/15 電話番号・ファックス番号を数字・ハイフンのみに整形して出力するよう変更
        Ver.1.40 2015/08/06 開設者名をfus開設者から出力(8/1作業漏れ)
        Ver.1.50 2015/10/02 顧客名ふりがなを 半角ｶﾅ→全角ひらがな に変換して出力するよう変更
        Ver.1.60 2016/01/30 ＰＣＡ商魂商管Ｘの銀行マスターに対応
                            商魂商管Ｘのデータベース照合順序がJapanese_90_CI_ASに変更されたので、
                            ここでは文字列比較照合順序をJapanese_CI_ASとして明示追加
        Ver.1.70 2017/08/17 [tEmployeeID]項目の内容を拠点名に変更
                          JunpDB.dbo.vMic営業担当 を tClient.fCliFirstcMan から
                          tCliTanto.fCltUsrID の営業職抽出に変更したことにより、
                          問い合わせメールが送信されなくなったため、
                          必ず送信するよう fCliFirstcMan 値をtEmployeeIDとすることにした
        Ver.1.80 2020/08/27 estoreメールアドレスとメルマガ購読フラグの追加 by 勝呂

*/
select C.fCliID                                  as fClientID
     , C.fCliName                                as fClientName1
     , F.fkj顧客名２                             as fClientName2
     , dbo.ToHiragana(dbo.ToWide(C.fCliYomi))    as fClientNameKana      -- 半角ｶﾅ→全角ひらがな
     , U.fusﾒｰﾙｱﾄﾞﾚｽ                             as fMailAddress
     , F.fkj郵便番号                             as fZipCode
     , F.fkj住所１                               as fLocationAddress1
     , F.fkj住所２                               as fLocationAddress2
     , dbo.AjustPhoneNumber(F.fkj電話番号)       as fPhoneNumber         -- 数字・ハイフンのみに整形
     , dbo.AjustPhoneNumber(F.fkjファックス番号) as fFaxNumber           -- 数字・ハイフンのみに整形
 --  , U.fus院長名                               as fEstablisherName     -- 項目定義では[開設者名]だが暫定で医院長名を出力
     , U.fus開設者                               as fEstablisherName     -- 開設者名を出力に変更(2015/08/06)
     , U.fus医保医療コード                       as fInstitutionCode
     , P.fBshCode3                               as tBranchCode          -- 参照のみ
--   , S.営業担当者コード                        as tEmployeeID          -- 参照のみ  Ver.1.70 2017/08/17 修正
     , C.fCliFirstcMan                           as tEmployeeID          -- 参照のみ
     , M.MWS_申込種別                            as tApplyType           -- CharlieDB.T_CUSTOMER_FOUNDATIONS.APPLY_TYPEを出力(更新可)
     , B.銀行名                                  as fBankName
     , B.支店名                                  as fBankBranchName
     , '****'+right(K.fda口座番号,3)             as fBankAccountNamber
	 , E.メールアドレス                          as fEstoreMailAddress   -- Ver.1.80 2020/08/27 追加
	 , E.メルマガ購読                            as fMailMagazineFlg     -- Ver.1.80 2020/08/27 追加
 --  , F.fkj住所フリガナ    as fLocationAddressKana
 --  , U.fus院長名フリガナ  as fEstablisherNameKana  -- 項目定義では[開設者フリガナ]だが暫定で医院長名フリガナを出力
 --  , U.fus院長名          as fHeadDoctorName
 --  , U.fus院長名フリガナ  as fHeadDoctorNameKana
from         JunpDB.dbo.tClient           C
  inner join JunpDB.dbo.tMik基本情報      F on (C.fCliID=F.fkjCliMicID)
  inner join JunpDB.dbo.vMih担当者        P on (C.fCliFirstcMan=P.fUsrID)
  left  join JunpDB.dbo.tMikユーザ        U on (C.fCliID=U.fusCliMicID)
--left  join JunpDB.dbo.vMic営業担当      S on (C.fCliID=S.顧客No)         Ver.1.70 2017/08/17 修正
  left  join dbo.view_MWS顧客状況         M on (C.fCliID=M.顧客ID)
  left  join JunpDB.dbo.tMik代行回収      K on (C.fCliID=K.fdaCliMicID and K.fda状態='継続')
  left  join JunpDB.dbo.vMicPCA銀行マスタ B on (B.銀行コード=K.fda銀行コード collate Japanese_ci_as 
                                            and B.支店コード=K.fda支店コード collate Japanese_ci_as )
  left join estoreDB.dbo.tMicCust_mst E on C.fCliID = E.顧客No
where (F.fkj削除フラグ ='0')
  and (F.fkj顧客区分=2 or F.fkj顧客区分=18)                     -- 顧客区分=1 (見込み客) 廃止のため修正
--and (F.fkj顧客区分=1 or F.fkj顧客区分=2 or F.fkj顧客区分=18)



GO

