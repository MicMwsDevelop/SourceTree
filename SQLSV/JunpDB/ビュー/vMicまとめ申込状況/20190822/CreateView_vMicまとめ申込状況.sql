USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicまとめ申込状況]    Script Date: 2019/08/22 11:54:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--CREATE VIEW [dbo].[vMicまとめ申込状況]
ALTER VIEW [dbo].[vMicまとめ申込状況]
AS
/*
    vMicまとめ申込状況

      Ver.1.00 2019/08/22 初版

*/
SELECT
  LEFT(CONVERT(NVARCHAR, EOMONTH(H.fContractStartDate , -1), 111), 7) AS 売上月
  ,H.fCustomerID AS 顧客No
  ,M.得意先コード AS 得意先No
  ,M.顧客名 AS 顧客名
  ,M.支店コード AS 支店コード
  ,M.支店名 AS 支店名
  ,iif(SA.営業担当者名 is null, '', SA.営業担当者名) AS 営業担当
  ,M.課金対象外ユーザー AS 終了フラグ
  ,C.利用システム名 AS システム名称
  ,iif(C.前システム名称 is null, '', C.前システム名称) AS 前システム名称
  ,H.fTotalAmount AS 契約金額
  ,CONVERT(NVARCHAR, H.fApplyDate, 111) AS 申込日
  ,H.fMonths AS 月数
  ,H.fContractType AS 契約タイプ
  ,CONVERT(NVARCHAR, H.fContractStartDate, 111) AS 契約開始日
  ,iif(H.fBillingEndDate is null, '', CONVERT(NVARCHAR, H.fBillingEndDate, 111)) AS 課金終了日
  ,'' AS 列名なし
  ,iif(M.申込種別 = 1, 'VP', iif(M.申込種別 = 2, 'UG', iif(M.申込種別 = 3, '課金', iif(M.申込種別 = 4, 'まとめ', 'その他')))) AS 区分
  FROM charlieDB.dbo.T_USE_CONTRACT_HEADER AS H
  LEFT JOIN charlieDB.dbo.view_MWSユーザー AS M
  ON H.fCustomerID = M.顧客ID
  LEFT JOIN charlieDB.dbo.顧客マスタ参照ビュー AS C
  ON H.fCustomerID = C.顧客ＩＤ
  LEFT JOIN dbo.vMic営業担当 AS SA 
  ON H.fCustomerID = SA.顧客No
  /* 契約開始日 >= 翌月初日 */
  WHERE H.fContractType = 'まとめ' AND CONVERT(date, DATEADD(dd, 1, EOMONTH(getdate()))) <= CONVERT(date, H.fContractStartDate)
GO
