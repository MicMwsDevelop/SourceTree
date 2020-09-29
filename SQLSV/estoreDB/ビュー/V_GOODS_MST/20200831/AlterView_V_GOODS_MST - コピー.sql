USE [estoreDB]
GO

/****** Object:  View [dbo].[V_GOODS_MST]    Script Date: 2020/08/31 11:07:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





--CREATE VIEW [dbo].[V_GOODS_MST]
CREATE VIEW [dbo].[V_GOODS_MST]
AS
SELECT WebSMS.ID AS GOODS_MST_ID, WebSMS.商品コード AS GOODS_CODE, RTRIM(PCASMS.sms_mei) AS GOODS_NAME, PCASMS.sms_bai1 AS GENERAL_PRICE, 
       WebSMS.web提供価格 AS WEB_PRICE, WebSMS.Q通常提供価格 AS Q_GENERAL_PRICE, WebSMS.Qweb提供価格 AS Q_WEB_PRICE, 
       WebSMS.カテゴリNo AS CATEGORY_NO, WebSMS.商品カテゴリ AS GOODS_CATEGORY, WebSMS.商品説明 AS GOODS_COMMENT, WebSMS.削除 AS DEL_FLG, 
       WebSMS.表示開始日時 AS GOODS_VISIBLE_FROM_DT, WebSMS.表示終了日時 AS GOODS_VISIBLE_UNTIL_DT, WebSMS.付加情報 AS GOODS_ADD_INFO, 
       WebSMS.商品詳細のリンク設定 AS GOODS_DETAIL_URL
       ,OS明細印字.fms発注単位 AS BO_ORDER_UNIT
FROM JunpDB.dbo.tMicWebSMS AS WebSMS
INNER JOIN JunpDB.dbo.vMicPCA商品マスタ AS PCASMS ON WebSMS.商品コード = PCASMS.sms_scd
LEFT JOIN JunpDB.dbo.tMikOS明細印字 AS OS明細印字 ON OS明細印字.fms印字必要 = '1' AND OS明細印字.fmsコード種別 <> '11'
          AND (WebSMS.商品コード = OS明細印字.fms商品コード１ OR WebSMS.商品コード = OS明細印字.fms商品コード2 OR WebSMS.商品コード = OS明細印字.fms商品コード3 OR WebSMS.商品コード = OS明細印字.fms商品コード4
				OR WebSMS.商品コード = OS明細印字.fms商品コード5 OR WebSMS.商品コード = OS明細印字.fms商品コード6 OR WebSMS.商品コード = OS明細印字.fms商品コード7 OR WebSMS.商品コード = OS明細印字.fms商品コード8)
WHERE NOT ((WebSMS.商品コード = '003501' AND OS明細印字.fmsコード = '004') OR (WebSMS.商品コード = '003503' AND OS明細印字.fmsコード = '004') OR (WebSMS.商品コード = '003505' AND OS明細印字.fmsコード = '004') OR (WebSMS.商品コード = '003507' AND OS明細印字.fmsコード = '004'))
GO

