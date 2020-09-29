USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic_estore_Cust_mst]    Script Date: 2020/07/30 10:20:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








--CREATE VIEW [dbo].[vMic_estore_Cust_mst]
ALTER VIEW [dbo].[vMic_estore_Cust_mst]
/*
    vMic_estore_Cust_mst
      eStore ユーザー情報 参照用ビュー

        Ver1.0 2010/07/06 初版
        Ver1.1 2018/03/13 文字列型項目を RTRIM した
		Ver1.2 2020/08/27 estore入替に伴い、メルマガ購読が２だとWWの医院情報のメルマガ購読フラグが×で表示されてしまうので、2を1で出力するように修正 by 勝呂
*/
AS
SELECT C.顧客No                       as 顧客No
      ,RTRIM(C.顧客名)                as 顧客名
      ,C.システムコード               as システムコード
      ,RTRIM(C.システム名称)          as システム名称
      ,RTRIM(C.お支払方法)            as お支払方法
      ,C.領収証コード                 as 領収証コード
      ,RTRIM(C.領収証用紙)            as 領収証用紙
      ,C.カルテコード                 as カルテコード
      ,RTRIM(C.カルテ用紙)            as カルテ用紙
      ,C.トナーコード                 as トナーコード
      ,RTRIM(C.トナー)                as トナー
      ,C.県番号                       as 県番号
      ,RTRIM(C.都道府県名)            as 都道府県名
      ,C.製品ライセンス発行済みフラグ as 製品ライセンス発行済みフラグ
      ,RTRIM(C.パスワード)            as パスワード
      ,RTRIM(C.パスワード読み)        as パスワード読み
      ,RTRIM(C.メールアドレス)        as メールアドレス

      --,C.メルマガ購読                 as メルマガ購読
      ,iif(C.メルマガ購読=0, 0, 1)    as メルマガ購読

      ,C.新規登録日時                 as 新規登録日時
      ,C.最終ログイン日時             as 最終ログイン日時
FROM estoreDB.dbo.tMicCust_mst as C
