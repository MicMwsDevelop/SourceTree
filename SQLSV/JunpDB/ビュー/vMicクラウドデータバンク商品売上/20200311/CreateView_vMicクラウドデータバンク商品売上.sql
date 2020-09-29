USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicクラウドデータバンク商品売上]    Script Date: 2020/03/10 12:38:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





--CREATE VIEW [dbo].[vMicクラウドデータバンク商品売上]
ALTER VIEW [dbo].[vMicクラウドデータバンク商品売上]
AS
/*
    vMicナルコーム商品売上

      Ver.1.00 2020/03/10 初版 by 勝呂

*/ 
SELECT
仕入先
, sykd_jbmn
, sykd_jtan
, 仕入商品コード
, sykd_mkbn
, sms_mei
, sykd_suryo
, sykd_tani
, 仕入価格
, sykd_uribi
, 仕入フラグ
, sykd_rate
  FROM [JunpDB].[dbo].[vMicPCA売上明細] as D
  INNER JOIN [JunpDB].[dbo].[M_クラウドデータバンク商品] AS C ON D.sykd_scd = C.商品コード
  INNER JOIN [JunpDB].[dbo].[vMicPCA商品マスタ] AS M ON C.仕入商品コード = M.sms_scd
  WHERE  sykd_kingaku <> 0

GO

