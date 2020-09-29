USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic‚Ü‚Æ‚ß”„ãŽÀÑ]    Script Date: 2019/10/10 12:48:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vMic‚Ü‚Æ‚ß”„ãŽÀÑ]
--ALTER VIEW [dbo].[vMic‚Ü‚Æ‚ß”„ãŽÀÑ]
AS

/*
    vMic‚Ü‚Æ‚ß”„ãŽÀÑ

      Ver.1.00 2019/10/10 ‰”Å by Ÿ˜C

*/ 
--MWSƒTƒCƒg\ž•ª
SELECT 
	  LEFT(CONVERT(nvarchar, [sykd_uribi], 112), 6) AS ”„ãŒŽ
      ,[sykd_uribi] AS ”„ã“ú
      ,[sykd_denno] AS “`•[No
	  ,B.[fkjCliMicID] AS ŒÚ‹qNo
      ,[sykd_tcd] AS “¾ˆÓæNo
	  ,U.[ŒÚ‹q–¼‚P] + U.[ŒÚ‹q–¼‚Q] AS ŒÚ‹q–¼
	  ,U.[“s“¹•{Œ§–¼] AS “s“¹•{Œ§–¼
	  ,U.[Žx“XƒR[ƒh] AS ‹’“XƒR[ƒh
	  ,U.[Žx“X–¼] AS ‹’“X–¼
      ,'' AS ’S“–ŽÒƒR[ƒh
      ,'' AS  ’S“–ŽÒ
	  ,U.[‰c‹Æ’S“–ŽÒƒR[ƒh] AS ‰c‹Æ’S“–ŽÒƒR[ƒh
	  ,U.[‰c‹Æ’S“–ŽÒ–¼] AS ‰c‹Æ’S“–ŽÒ–¼
      ,[sykd_tekmei] AS “E—v
      ,[sykd_scd] AS ¤•iƒR[ƒh
      ,[sykd_mei] AS ¤•i–¼
      ,CONVERT(int, [sykd_kingaku]) AS ’ñ‹Ÿ‰¿Ši
      ,CONVERT(int, [sykd_suryo]) AS ”—Ê
      ,iif(M.\žŽí•Ê = 1, 'VP', iif(M.\žŽí•Ê = 2, 'UG', iif(M.\žŽí•Ê = 3, '‰Û‹à', iif(M.\žŽí•Ê = 4, '‚Ü‚Æ‚ß', '‚»‚Ì‘¼')))) AS ‹æ•ª
	  ,'' AS ƒŠƒvƒŒ[ƒX‹æ•ª
	  ,'' AS ƒŠƒvƒŒ[ƒX
  FROM [JunpDB].[dbo].[vMicPCA”„ã–¾×] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMikŠî–{î•ñ] AS B
  ON B.[fkj“¾ˆÓæî•ñ] = PM.[sykd_tcd]
  LEFT JOIN [JunpDB].[dbo].[vMic‘Sƒ†[ƒU[2] AS U
  ON B.[fkjCliMicID] = U.[ŒÚ‹qNo]
  LEFT JOIN [charlieDB].[dbo].[view_MWSƒ†[ƒU[] AS M
  ON B.[fkjCliMicID] = M.[ŒÚ‹qID]
  WHERE LEFT([sykd_denno], 1) = '8' AND [sykd_suryo] > 0 AND ([sykd_scd] = '800155' OR [sykd_scd] = '800156' OR [sykd_scd] = '800157' OR [sykd_scd] = '800158' OR [sykd_scd] = '800159')

UNION ALL

--WonderWeb‹N•[•ª
SELECT
       LEFT(CONVERT(nvarchar, [sykd_uribi], 112), 6) AS ”„ãŒŽ
      ,[sykd_uribi] AS ”„ã“ú
      ,[sykd_denno] AS “`•[No
	  ,JH.[fƒ†[ƒU[ƒR[ƒh] AS ŒÚ‹qNo
      ,[sykd_tcd] AS “¾ˆÓæNo
	  ,U.[ŒÚ‹q–¼‚P] + U.[ŒÚ‹q–¼‚Q] AS ŒÚ‹q–¼
	  ,U.[“s“¹•{Œ§–¼] AS “s“¹•{Œ§–¼
	  ,U.[Žx“XƒR[ƒh] AS ‹’“XƒR[ƒh
	  ,U.[Žx“X–¼] AS ‹’“X–¼
      ,JH.[f’S“–ŽÒƒR[ƒh] AS ’S“–ŽÒƒR[ƒh
      ,JH.[f’S“–ŽÒ–¼] AS ’S“–ŽÒ
	  ,U.[‰c‹Æ’S“–ŽÒƒR[ƒh] AS ‰c‹Æ’S“–ŽÒƒR[ƒh
	  ,U.[‰c‹Æ’S“–ŽÒ–¼] AS ‰c‹Æ’S“–ŽÒ–¼
      ,[sykd_tekmei] AS “E—v
      ,[sykd_scd] AS ¤•iƒR[ƒh
      ,[sykd_mei] AS ¤•i–¼
      ,CONVERT(int, [sykd_kingaku]) AS ’ñ‹Ÿ‰¿Ši
      ,CONVERT(int, [sykd_suryo]) AS ”—Ê
      ,iif(M.\žŽí•Ê = 1, 'VP', iif(M.\žŽí•Ê = 2, 'UG', iif(M.\žŽí•Ê = 3, '‰Û‹à', iif(M.\žŽí•Ê = 4, '‚Ü‚Æ‚ß', '‚»‚Ì‘¼')))) AS ‹æ•ª
	  ,iif(JH.fƒŠƒvƒŒ[ƒX = 'Ž©ŽÐ‚q', 'Ž©ŽÐ‚q', iif(JH.fƒŠƒvƒŒ[ƒX = 'V‹K' OR JH.fƒŠƒvƒŒ[ƒX = 'VŠJ', 'V‹KEVŠJ', iif(JH.fƒŠƒvƒŒ[ƒX = '‚»‚Ì‘¼' OR JH.fƒŠƒvƒŒ[ƒX = 'ƒŠƒvƒŒ[ƒX‚È‚µ', '•s–¾', '‘¼ŽÐ‚q'))) AS ƒŠƒvƒŒ[ƒX‹æ•ª
	  ,JH.fƒŠƒvƒŒ[ƒX AS ƒŠƒvƒŒ[ƒX
  FROM [JunpDB].[dbo].[vMicPCA”„ã–¾×] AS PM
  LEFT JOIN [JunpDB].[dbo].[tMihŽó’ƒwƒbƒ_] AS JH
  ON PM.[sykd_denno] = JH.[fŽó’”Ô†]
  LEFT JOIN [JunpDB].[dbo].[vMic‘Sƒ†[ƒU[2] AS U
  ON JH.[fƒ†[ƒU[ƒR[ƒh] = U.[ŒÚ‹qNo]
  LEFT JOIN [charlieDB].[dbo].[view_MWSƒ†[ƒU[] AS M
  ON JH.[fƒ†[ƒU[ƒR[ƒh] = M.[ŒÚ‹qID]
  WHERE LEFT([sykd_denno], 1) = '5' AND [sykd_suryo] > 0 AND ([sykd_scd] = '800155' OR [sykd_scd] = '800156' OR [sykd_scd] = '800157' OR [sykd_scd] = '800158' OR [sykd_scd] = '800159')

GO

