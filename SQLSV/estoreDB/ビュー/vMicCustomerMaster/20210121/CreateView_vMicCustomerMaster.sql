USE [estoreDB]
GO

/****** Object:  View [dbo].[vMicCustomerMaster]    Script Date: 2021/01/25 9:37:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[vMicCustomerMaster]
AS
/*
    vMicCustomerMasterビュー
        Ver.1.00 2021/01/21 初版 by 勝呂

	istationからMWSサイトへの入替で [estoreDB].[dbo].[tMicCust_mst] は estoreメールアドレスとメルマガ購読フラグを保存する領域となり、
	その他のフィールドは無意味となった。従ってその他のフィールドはWonderWebやCharlieのデータを参照するように変更。
	本ビューはweb受注出荷済メール送信アプリで使用する。
*/
SELECT 顧客マスタ.[顧客No]
      ,顧客マスタ.[顧客名]
      ,convert(int, 顧客マスタ.[システムコード]) as システムコード
      ,顧客マスタ.[システム名] as システム名称
      ,顧客マスタ.[お支払方法]
      ,convert(int, 顧客マスタ.[領収証コード]) as 領収証コード
      ,顧客マスタ.[領収証用紙]
      ,convert(int, 顧客マスタ.[カルテコード]) as カルテコード
      ,顧客マスタ.[カルテ用紙]
      ,convert(int, 顧客マスタ.[トナーコード]) as トナーコード
      ,顧客マスタ.[トナー]
      ,convert(int, 顧客マスタ.[県番号]) as 県番号
      ,顧客マスタ.[都道府県名]
      ,convert(int, 顧客マスタ.[ライセンス発行]) as 製品ライセンス発行済みフラグ
      ,代行回収.fdaAPLUSコード AS APLUSコード
      ,'' as パスワード
      ,'' as パスワード読み
      ,iif(mst.[メールアドレス] is null, ユーザ.[fusﾒｰﾙｱﾄﾞﾚｽ], mst.[メールアドレス]) as メールアドレス
      ,iif(mst.[メルマガ購読] is null, 0, mst.[メルマガ購読]) as メルマガ購読
      ,mst.[新規登録日時] 
      ,mst.[最終ログイン日時]
  FROM [estoreDB].[dbo].[vMic顧客マスタ] as 顧客マスタ
  LEFT JOIN [estoreDB].[dbo].[tMicCust_mst] as mst on mst.[顧客No] = 顧客マスタ.[顧客No]
  INNER JOIN [dbo].[tMik代行回収] as 代行回収 ON 顧客マスタ.顧客No = 代行回収.fdaCliMicID
  INNER JOIN [dbo].[tMikユーザ] as ユーザ ON 顧客マスタ.顧客No = ユーザ.fusCliMicID

GO

