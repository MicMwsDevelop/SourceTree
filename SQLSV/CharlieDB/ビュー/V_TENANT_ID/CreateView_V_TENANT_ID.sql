USE [charlieDB]
GO

/****** Object:  View [dbo].[V_TENANT_ID]    Script Date: 2021/01/29 17:42:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--ALTER VIEW [dbo].[V_TENANT_ID]
CREATE VIEW [dbo].[V_TENANT_ID]
as
/*

    V_TENANT_ID

        Web予約受付で使用するテナントIDの参照ビュー

        Ver.1.00 2021/01/29 初版 by 勝呂

*/
SELECT
    U.[支店ＩＤ] as 拠点コード
   ,U.[支店名] as 拠点名
   ,W.[fCustomerID] as 顧客No
   ,U.[顧客名１] + U.[顧客名2] as 顧客名
   ,W.[fTenantID] as テナントID
  FROM [charlieDB].[dbo].[T_USE_WEBAPPOINT_TENANTID] as W
  INNER JOIN [charlieDB].[dbo].[顧客マスタ参照ビュー] as U on W.[fCustomerID] = U.[顧客ＩＤ]
  WHERE W.[fCustomerID] is not null AND U.終了 = '0'

GO

