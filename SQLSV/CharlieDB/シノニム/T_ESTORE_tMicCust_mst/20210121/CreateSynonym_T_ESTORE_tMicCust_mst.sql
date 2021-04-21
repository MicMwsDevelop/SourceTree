USE [charlieDB]
GO

/*
    T_ESTORE_tMicCust_mstビュー
        Ver.1.00 2021/01/21 初版 by 勝呂

	T_PRODUCT_CONTROLの追加トリガー内で[estoreDB].[dbo].[tMicCust_mst]へのアクセスの為に作成
*/
/****** Object:  Synonym [dbo].[T_ESTORE_tMicCust_mst]    Script Date: 2021/01/25 9:40:11 ******/
CREATE SYNONYM [dbo].[T_ESTORE_tMicCust_mst] FOR [estoreDB].[dbo].[tMicCust_mst]
GO

