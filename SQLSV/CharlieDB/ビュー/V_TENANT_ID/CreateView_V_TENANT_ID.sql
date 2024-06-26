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

        Web\ñótÅgp·éeigIDÌQÆr[

        Ver.1.00 2021/01/29 Å by C

*/
SELECT
    U.[xXhc] as _R[h
   ,U.[xX¼] as _¼
   ,W.[fCustomerID] as ÚqNo
   ,U.[Úq¼P] + U.[Úq¼2] as Úq¼
   ,W.[fTenantID] as eigID
  FROM [charlieDB].[dbo].[T_USE_WEBAPPOINT_TENANTID] as W
  INNER JOIN [charlieDB].[dbo].[Úq}X^QÆr[] as U on W.[fCustomerID] = U.[Úqhc]
  WHERE W.[fCustomerID] is not null AND U.I¹ = '0'

GO

