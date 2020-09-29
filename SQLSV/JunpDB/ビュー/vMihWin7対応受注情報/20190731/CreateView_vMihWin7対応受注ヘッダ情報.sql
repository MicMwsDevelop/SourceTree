USE [JunpDB]
GO

/****** Object:  View [dbo].[vMihWin7対応受注ヘッダ情報]    Script Date: 2019/09/20 9:17:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--CREATE VIEW [dbo].[vMihWin7対応受注ヘッダ情報]
ALTER VIEW [dbo].[vMihWin7対応受注ヘッダ情報]
AS
SELECT TA.fBshCode2 AS 営業部コード
	, TA.fBshName2 AS 営業部
	, H.fBshCode3 AS 拠店コード
	, H.f担当支店名 AS 拠店名
	, H.f担当者コード AS 担当者コード
	, H.f担当者名 AS 担当者名
	, iif(SA.営業担当者コード IS NOT NULL, SA.営業担当者コード, H.f担当者コード) AS 営業担当者コード
	, iif(SA.営業担当者コード IS NOT NULL, SA.営業担当者名, H.f担当者名) AS 営業担当者名
	, H.f年度 AS 年度
	, H.f受注番号 AS 受注番号
	, H.f納期 AS 納品日
	, CONVERT(VARCHAR, H.f受注承認日, 111) AS 受注承認日
	, CONVERT(VARCHAR, H.f売上承認日, 111) AS 売上承認日
	, H.fユーザーコード AS 顧客No
	, CL.fCliName AS 顧客名
	, KN.県番号 AS 県番号
	, KN.都道府県名 AS 都道府県名
	, B.fkj住所１ + B.fkj住所２ AS 住所
	, B.fkj電話番号 AS 電話番号
	, H.f受注金額 AS 受注金額
	, H.f備考 AS 備考
	, LEFT(CONVERT(VARCHAR, H.f納期, 111), 7) AS 納品月
	, LEFT(CONVERT(VARCHAR, H.f売上承認日, 111), 7) AS 売上承認月

FROM dbo.tMih受注ヘッダ AS H 
		INNER JOIN (SELECT D1.*
					FROM dbo.tMih受注詳細 AS D1 
						INNER JOIN (SELECT f受注番号, f年度, MIN(f表示順) AS 最小値
			                        FROM dbo.tMih受注詳細
			                        WHERE f商品コード <> '001871' 
											AND f商品コード <> '001872'
			                        GROUP BY f受注番号, f年度) AS D2 
							ON D1.f年度 = D2.f年度 AND D1.f受注番号 = D2.f受注番号 AND D1.f表示順 = D2.最小値) AS D
			 ON H.f年度 = D.f年度 AND H.f受注番号 = D.f受注番号 

		LEFT JOIN dbo.tClient AS CL 
			ON H.fユーザーコード = CL.fCliID 

		LEFT JOIN dbo.vMic営業担当 AS SA 
			ON H.fユーザーコード = SA.顧客No 

		LEFT JOIN dbo.tMik基本情報 AS B 
			ON H.fユーザーコード = B.fkjCliMicID 

		LEFT JOIN dbo.vMih担当者 AS TA 
			ON H.f担当者コード = TA.fUsrID 

		LEFT JOIN dbo.tMik県番号 AS KN 
			ON LEFT(B.fkj住所１, 3) = LEFT(KN.都道府県名, 3)

WHERE (H.f年度 >= 2019) AND (H.f備考 LIKE '%Win7対応%')

