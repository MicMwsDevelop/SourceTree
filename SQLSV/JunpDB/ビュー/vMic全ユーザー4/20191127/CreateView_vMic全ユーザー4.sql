USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic全ユーザー４]    Script Date: 2019/11/27 17:28:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE  VIEW [dbo].[vMic全ユーザー４]
--ALTER VIEW [dbo].[vMic全ユーザー４]
AS
/*
    vMic全ユーザー４
       Ver.1.00 2019/11/27 初版 ユーザー分析表のpaletteES対応のため全ユーザー２を改良 by 勝呂
*/

SELECT
 T.fBshCode2 AS 営業部コード
,T.fBshName2 AS 営業部名
,U.支店コード AS 拠点コード
,U.支店名 AS 拠点名
,U.システム名称
,U.終了フラグ
,U.顧客No
,U.得意先No
,U.顧客名１+U.顧客名２ as 顧客名
,U.システム名
,U.納品月
,U.売上月
,U.メールアドレス
,U.前システム名
,U.前システム終了
,U.前システム名称
,U.請求区分
,U.県番号
,U.都道府県名
,U.システム略称
,U.営業担当者コード
,U.営業担当者名
,U.インスト担当者コード
,U.インスト担当者名
,U.eStore登録フラグ
,U.メルマガ購読フラグ
,U.eStore登録メールアドレス
,U.MWS_ID
,U.MWS_申込種別
,U.MWS_販売種別
,U.MWS_使用許諾期限
,U.販売店ID
,U.販売店名称
,U.販売店グループコード
,U.販売店グループ名称
,U.販売店区分コード
,U.販売店区分名称
,U.有効ユーザーフラグ
,iif(H.fGoodsID='800121', 5, U.MWS_申込種別) AS MWS_申込種別2
,iif(H.fGoodsID='800121', 'ＥＳ', choose(U.MWS_申込種別+1, 'その他', 'ＶＰ', 'ＵＧ', '月額課金', 'まとめ')) AS MWS_申込種別名
,iif(H.fGoodsID='800121', 'paletteES', U.システム略称) AS システム略称2
,left(U.売上月,4) AS 売上年
,iif(PC.fEndFlag = 0, '1', '0') AS S保守
,PC.fYears AS S契約年数
,PC.fContractStartDate AS Sメンテ契約開始
,PC.fContractEndDate AS Sメンテ契約終了
FROM JunpDB.dbo.vMic全ユーザー2 AS U
LEFT JOIN JunpDB.dbo.T_USE_CONTRACT_HEADER AS H
ON U.顧客No = H.fCustomerID AND H.fContractType = 'ＶＰ'
LEFT JOIN JunpDB.dbo.vMih担当者 AS T
ON U.担当者コード = T.fUsrID
LEFT JOIN JunpDB.dbo.T_USE_PCCSUPPORT AS PC
ON U.顧客No = PC.fCustomerID
GO

