USE [JunpDB]
GO

/****** Object:  View [dbo].[vMihWin7対応受注情報]    Script Date: 2019/09/20 9:18:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--CREATE VIEW [dbo].[vMihWin7対応受注情報]
ALTER VIEW [dbo].[vMihWin7対応受注情報]
AS
SELECT                      TA.fBshCode2 AS 営業部コード
                            , TA.fBshName2 AS 営業部
							, H.fBshCode3 AS 拠店コード
							, H.f担当支店名 AS 拠店名
							, H.f担当者コード AS 担当者コード
							, H.f担当者名 AS 担当者名
							, iif(SA.営業担当者コード IS NOT NULL
							, SA.営業担当者コード, H.f担当者コード) AS 営業担当者コード
							, iif(SA.営業担当者コード IS NOT NULL
							, SA.営業担当者名, H.f担当者名) AS 営業担当者名
							, H.f年度 AS 年度, H.f受注番号 AS 受注番号
							, H.f納期 AS 納品日
							, CONVERT(VARCHAR, H.f受注承認日, 111) AS 受注承認日
							, CONVERT(VARCHAR, H.f売上承認日, 111) AS 売上承認日
							, H.fユーザーコード AS 顧客No, CL.fCliName AS 顧客名
							, B.fkj住所１ + B.fkj住所２ AS 住所
							, B.fkj電話番号 AS 電話番号
							, H.f受注金額 AS 受注金額
							, D.f表示順 AS 表示順
							, D.f商品コード AS 商品コード
							, MS.sms_mei AS 商品名
							, D.f区分 AS 区分コード
							, K.ems_str AS 区分名
							, D.f数量 AS 数量
							, D.f標準価格 AS 標準価格
							, D.f金額 AS 金額
							, D.f提供価格 AS 提供価格
							, D.f税区分 AS 税区分
							, D.f税率 AS 税率
							, D.f税込区分 AS 税込区分
							, D.f売上原価 AS 売上原価
							, D.f掛率 AS 掛率
							, D.f商品区分1 AS 商品区分1
							, D.f商品区分2 AS 商品区分2
							, H.f備考 AS 備考
							, LEFT(CONVERT(VARCHAR, H.f納期, 111), 7) AS 納品月
							, LEFT(CONVERT(VARCHAR, H.f売上承認日, 111), 7) AS 売上承認月
FROM                         dbo.tMih受注ヘッダ AS H LEFT OUTER JOIN
                                      dbo.tMih受注詳細 AS D ON H.f受注番号 = D.f受注番号 AND H.f年度 = D.f年度 LEFT JOIN
                                      dbo.tClient AS CL ON H.fユーザーコード = CL.fCliID LEFT JOIN
                                      dbo.vMic営業担当 AS SA ON H.fユーザーコード = SA.顧客No LEFT JOIN
                                      dbo.tMik基本情報 AS B ON H.fユーザーコード = B.fkjCliMicID LEFT JOIN
                                      dbo.vMicPCA商品マスタ AS MS ON D.f商品コード = MS.sms_scd LEFT JOIN
                                      dbo.vMicPCA区分マスタ AS K ON K.ems_id = 23 AND K.ems_kbn = D.f区分 LEFT JOIN
                                      dbo.vMih担当者 AS TA ON H.f担当者コード = TA.fUsrID
WHERE                       (H.f年度 >= 2019) AND D.f商品コード <> '001871' AND D.f商品コード <> '001872' AND (H.f備考 LIKE '%Win7対応%')
GO
