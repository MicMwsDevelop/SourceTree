USE [JunpDB]
GO

/****** Object:  View [dbo].[vMicPCÀST|[gãÀÑ]    Script Date: 2019/10/29 12:42:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vMicPCÀST|[gãÀÑ]
AS

/*
    vMicPCÀST|[gãÀÑ

      Ver.1.00 2019/10/10 Å by C
      Ver.1.01 2019/10/29 MWSTCg\µÝªðÜßé by C

*/ 

--MWSTCg\ª(PCAo^ª)
SELECT 
	  LEFT(CONVERT(nvarchar, [sykd_uribi], 112), 6) AS ã
      ,[sykd_uribi] AS ãú
      ,[sykd_denno] AS `[No
	  ,B.[fkjCliMicID] AS ÚqNo
      ,[sykd_tcd] AS ¾ÓæNo
	  ,U.[Úq¼P] + U.[Úq¼Q] AS Úq¼
	  ,U.[s¹{§¼] AS s¹{§¼
	  ,U.[xXR[h] AS XR[h
	  ,U.[xX¼] AS X¼
      ,'' AS SÒR[h
      ,'' AS  SÒ
	  ,U.[cÆSÒR[h] AS cÆSÒR[h
	  ,U.[cÆSÒ¼] AS cÆSÒ¼
      ,[sykd_tekmei] AS Ev
      ,[sykd_scd] AS ¤iR[h
      ,[sykd_mei] AS ¤i¼
      ,CONVERT(int, [sykd_kingaku]) AS ñ¿i
      ,CONVERT(int, [sykd_suryo]) AS Ê
  FROM [JunpDB].[dbo].[vMicPCAã¾×] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMikî{îñ] AS B
  ON B.[fkj¾Óæîñ] = PM.[sykd_tcd]
  LEFT JOIN [JunpDB].[dbo].[vMicS[U[2] AS U
  ON B.[fkjCliMicID] = U.[ÚqNo]
  WHERE LEFT([sykd_denno], 1) = '8' AND [sykd_suryo] > 0 AND ([sykd_scd] = '001871' OR [sykd_scd] = '001872')
  
UNION ALL

--WonderWebN[ª
SELECT
       LEFT(CONVERT(nvarchar, [sykd_uribi], 112), 6) AS ã
      ,[sykd_uribi] AS ãú
      ,[sykd_denno] AS `[No
	  ,JH.[f[U[R[h] AS ÚqNo
      ,[sykd_tcd] AS ¾ÓæNo
	  ,U.[Úq¼P] + U.[Úq¼Q] AS Úq¼
	  ,U.[s¹{§¼] AS s¹{§¼
	  ,U.[xXR[h] AS XR[h
	  ,U.[xX¼] AS X¼
      ,JH.[fSÒR[h] AS SÒR[h
      ,JH.[fSÒ¼] AS SÒ
	  ,U.[cÆSÒR[h] AS cÆSÒR[h
	  ,U.[cÆSÒ¼] AS cÆSÒ¼
      ,[sykd_tekmei] AS Ev
      ,[sykd_scd] AS ¤iR[h
      ,[sykd_mei] AS ¤i¼
      ,CONVERT(int, [sykd_kingaku]) AS ñ¿i
      ,CONVERT(int, [sykd_suryo]) AS Ê
  FROM [JunpDB].[dbo].[vMicPCAã¾×] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMihówb_] AS JH
  ON PM.[sykd_denno] = JH.[fóÔ]
  LEFT JOIN [JunpDB].[dbo].[vMicS[U[2] AS U
  ON JH.[f[U[R[h] = U.[ÚqNo]
  WHERE LEFT([sykd_denno], 1) = '5' AND [sykd_suryo] > 0 AND ([sykd_scd] = '001871' OR [sykd_scd] = '001872')


UNION ALL

--MWSTCg\ª(PCA¢o^ª)
SELECT LEFT(CONVERT(nvarchar, [fContractStartDate], 112), 6) AS ã
,CONVERT(nvarchar, PC.[fContractStartDate], 112) AS ãú
,'' AS `[No
,[fCustomerID] AS ÚqNo
,U.[¾ÓæNo] AS ¾ÓæNo
,U.[Úq¼P] + U.[Úq¼Q] AS Úq¼
,U.[s¹{§¼] AS s¹{§¼
,U.[xXR[h] AS XR[h
,U.[xX¼] AS X¼
,'' AS SÒR[h
,'' AS SÒ
,U.[cÆSÒR[h] AS cÆSÒR[h
,U.[cÆSÒ¼] AS cÆSÒ¼
,'' AS Ev
,PC.[fGoodsID] AS ¤iR[h
,M.[¤i¼] AS ¤i¼
,CONVERT(int, M.[W¿i]) AS ñ¿i
,'1' AS Ê
 FROM [charlieDB].[dbo].[T_USE_PCCSUPPORT] AS PC
 LEFT JOIN [JunpDB].[dbo].[vMicS[U[2] AS U
 ON PC.fCustomerID = U.[ÚqNo]
 LEFT JOIN [charlieDB].[dbo].[PCA¤i}X^QÆr[] AS M
 ON PC.[fGoodsID] = M.¤iID
WHERE [fBillingStartDate] is null

GO

