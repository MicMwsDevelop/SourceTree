USE [estoreDB]
GO

/****** Object:  View [dbo].[vMicOrder_accept]    Script Date: 2020/08/28 11:40:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
    2009/01/16 vMicOrder_accept
        Ｗｅｂ受注サイト用ミック社内用受注データ受信サーバ E-STORESV の受注データを参照するビュー
	2020/08/27 estore入替作業に伴い、本ビューの参照先をE-STORESVからestoredb.dbo.order_acceptに変更する by 勝呂
*/
ALTER VIEW [dbo].[vMicOrder_accept]
AS
SELECT          order_accept_id, order_no, customer_no, pref_arrival_date, goods_code, 
                      web_price, order_num, order_dt, send_dt
FROM            dbo.order_accept
GO

