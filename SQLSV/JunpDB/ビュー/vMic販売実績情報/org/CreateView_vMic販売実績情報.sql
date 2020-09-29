USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic販売実績情報]    Script Date: 2019/11/21 9:32:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[vMic販売実績情報]
--ALTER VIEW [dbo].[実験用作成中ビュー]
AS
/*

  ビュー名称   vMic販売実績情報
    2011/07/21 Ver.1.1 リース下取合計、受注金額3 を追加
    2011/07/27 Ver.1.2 第３四半期の期間対象バグ修正
    2011/09/02 Ver.1.3 リプレース区分に「在庫」を追加
    2012/08/08 Ver.1.4 [上下期]を追加 (1=上期、2=下期)
    2014/10/02 Ver.1.5 [エリア]の分類を '東日本グループ','首都圏グループ','西日本グループ' に変更
    2016/02/06 Ver.1.6 [エリア]の分類を '東日本グループ','首都圏グループ１','首都圏グループ２','西日本グループ' に変更

*/
SELECT
      販売実績.受注番号                                       AS 受注番号
    , YEAR(販売実績.売上承認日)                               AS 売上承認年
    , MONTH(販売実績.売上承認日)                              AS 売上承認月
    , 販売実績.売上承認日                                     AS 売上承認日
    , 販売実績.販売先コード                                   AS 販売先コード
    , RTRIM(Client.fCliName + ' ' + 基本情報.fkj顧客名２)     AS 販売先
    , 代理店リース会社.fdl販売店グループ                      AS 販売店グループコード
    , (CASE WHEN (Pca得意先.fpt得意先区分1<80) OR (Pca得意先.fpt得意先区分1=98) THEN
            ISNULL(販売店グループマスタ.fcm名称, RTRIM(Client.fCliName + ' ' + 基本情報.fkj顧客名２))
            ELSE '直' END)                                    AS 販売店グループ
    , RTRIM(vMic得意先区分１.ems_str)                         AS 得意先区分１
    , RTRIM(vMic得意先区分２.ems_str)                         AS 得意先区分２
    , 支払実績.支払先コード                                   AS 支払先コード
    , 支払実績.支払明細番号                                   AS 支払明細番号
    , 支払実績.支払先WW顧客No                                 AS 支払先WW顧客No
    , 支払実績.支払先名                                       AS 支払先名
    , 支払実績.仕入先名                                       AS 仕入先名
    , 支払実績.支払先顧客区分                                 AS 支払先顧客区分
    , RTRIM(vMic支払先得意先区分２.ems_str)                   AS 支払先得意先区分２
    , 販売実績.ユーザーコード                                 AS ユーザーコード
    , 販売実績.ユーザー                                       AS ユーザー
    , 販売実績.担当者コード                                   AS 担当者コード
    , 販売実績.担当者名                                       AS 担当者名
    , 販売実績.部門コード                                     AS 部門コード
    , 販売実績.部署コード                                     AS 部署コード
 --   2016/02/06 なぜか支店情報から表示していたので、販売実績の伝票担当支店を表示することにした
 -- , ISNULL(拠点名.f支店名,'その他')                         AS 担当支店名
    , 販売実績.担当支店名                                     AS 担当支店名
    , 販売実績.受注金額                                       AS 受注金額
    , 販売実績.システム売上                                   AS システム売上
    , 販売実績.ソフト販売価格                                 AS ソフト販売価格
    , 支払実績.支払金額                                       AS 支払金額
    , 支払実績.支払金額税抜                                   AS 支払金額税抜
    , 販売実績.システム売上 + 支払実績.支払金額税抜           AS 紹介売買契約金額
    , 販売実績.粗利額                                         AS 粗利額
    , 販売実績.商品コード                                     AS 商品コード
    , 販売実績.商品名                                         AS 商品名
    , 販売実績.数量                                           AS 数量
    , 販売実績.区分2                                          AS 商品区分２
    , (CASE WHEN (ISNULL(販売実績.リプレース区分,0) = 0) THEN 'その他'
            WHEN (販売実績.リプレース区分=1) THEN '自社Ｒ'
            WHEN (販売実績.リプレース区分=2) THEN '新規'
            WHEN (販売実績.リプレース区分=3) THEN '新開'
            WHEN (販売実績.リプレース区分=4) THEN '在庫'
            ELSE '他社Ｒ' END)                                AS リプレース区分
    , 販売実績.リプレース                                     AS リプレース
    , 販売実績.県番号                                         AS 県番号
    , 販売実績.都道府県名                                     AS 都道府県名
    , 社員マスタ.fUsrYaku                                     AS 役職コード
    , ((99-ISNULL(社員マスタ.fUsrYaku,0))*10000
        +LEFT(CASE WHEN (販売実績.担当者コード) like 'qr-%' THEN REPLACE((販売実績.担当者コード),'qr-','9')
                   WHEN (販売実績.担当者コード) like 'SP%'  THEN REPLACE((販売実績.担当者コード),'SP', '9')
                   ELSE (販売実績.担当者コード) END,4))       AS 表示順コード
    -- 2014/10/02 Ver.1.5 [エリア]の分類を '東日本グループ','首都圏グループ','西日本グループ' に変更
    , (case
           when 販売実績.売上承認日<'2014-08-01' then
              (CASE WHEN 販売実績.部署コード>=20 THEN 'MET'
                    ELSE 'SAT' END)
           when 販売実績.売上承認日<'2015-08-01' then
              (CASE WHEN 販売実績.部署コード<=07 THEN '東日本グループ'
                    WHEN 販売実績.部署コード>=60 and 販売実績.部署コード<=69 THEN '首都圏グループ'
                    ELSE '西日本グループ' END)
           else
              (CASE 販売実績.部門コード
                  when 50 THEN '東日本グループ'
                  when 60 THEN '首都圏グループ１'
                  when 70 THEN '首都圏グループ２'
                  when 80 THEN '西日本グループ'
                  else 
                    (case
                       when 販売実績.部署コード = 61
					     or 販売実績.部署コード = 63
						 or 販売実績.部署コード = 64 THEN '首都圏グループ１'
                       when 販売実績.部署コード = 62
					     or 販売実績.部署コード = 65 THEN '首都圏グループ２'
                       else 'その他'
                     end)
                  END)
       end)                                                   AS エリア
    , 社員マスタ.fBshCode3                                    AS 現在部署コード
    , 社員マスタ.fBshName3                                    AS 現在支店名
    , (CASE WHEN 販売実績.売上承認日 IS NULL THEN NULL
            ELSE (YEAR(販売実績.売上承認日)-
                   (CASE WHEN MONTH(販売実績.売上承認日)<8 THEN 1975
                         ELSE 1974 END)) END)                 AS 会計年度
    , (CASE WHEN 販売実績.売上承認日 IS NULL THEN NULL
            WHEN MONTH(販売実績.売上承認日)= 8 OR MONTH(販売実績.売上承認日)= 9 OR MONTH(販売実績.売上承認日)=10 THEN 1
            WHEN MONTH(販売実績.売上承認日)=11 OR MONTH(販売実績.売上承認日)=12 OR MONTH(販売実績.売上承認日)= 1 THEN 2
            WHEN MONTH(販売実績.売上承認日)= 2 OR MONTH(販売実績.売上承認日)= 3 OR MONTH(販売実績.売上承認日)= 4 THEN 3
            ELSE 4 END)                                       AS 四半期
    , (CASE WHEN (Pca得意先.fpt得意先区分1 < 80) OR (Pca得意先.fpt得意先区分1 = 98) THEN 3
            WHEN 支払実績.支払金額<>0                                               THEN 2
            ELSE 1 END)                                       AS 販売区分
    , 販売実績.リース下取合計                                 AS リース下取合計
    , 販売実績.受注金額3                                      AS 受注金額3
    , (CASE WHEN 販売実績.売上承認日 IS NULL THEN NULL
            WHEN MONTH(販売実績.売上承認日)= 8 OR MONTH(販売実績.売上承認日)= 9 OR MONTH(販売実績.売上承認日)=10 OR
                 MONTH(販売実績.売上承認日)=11 OR MONTH(販売実績.売上承認日)=12 OR MONTH(販売実績.売上承認日)= 1 THEN 1
            ELSE 2 END)                                       AS 上下期
FROM
    dbo.tMikPca得意先             Pca得意先                               LEFT OUTER JOIN
    dbo.vMicPCA区分マスタ         vMic得意先区分２
      ON vMic得意先区分２.ems_id = 12 AND
         Pca得意先.fpt得意先区分2 = vMic得意先区分２.ems_kbn              LEFT OUTER JOIN
    dbo.vMicPCA区分マスタ         vMic得意先区分１
      ON vMic得意先区分１.ems_id = 11 AND
         Pca得意先.fpt得意先区分1 = vMic得意先区分１.ems_kbn              RIGHT OUTER JOIN
    dbo.vMicPCA区分マスタ         vMic支払先得意先区分２                  RIGHT OUTER JOIN
    dbo.vMic販売手数料支払実績    支払実績                                LEFT OUTER JOIN
    dbo.tMikPca得意先             支払先Pca得意先
      ON 支払実績.支払先WW顧客No = 支払先Pca得意先.fptCliMicID
      ON vMic支払先得意先区分２.ems_id = 12 AND
         vMic支払先得意先区分２.ems_kbn = 支払先Pca得意先.fpt得意先区分2  RIGHT OUTER JOIN
    dbo.vMic販売実績              販売実績                                LEFT OUTER JOIN
    dbo.vMic担当者                社員マスタ
      ON 販売実績.担当者コード = 社員マスタ.fUsrID                        LEFT OUTER JOIN
    dbo.tMih支店情報              拠点名
      ON 販売実績.部署コード = 拠点名.fBshCode3 AND
         拠点名.fBshCode1 = 01 AND
         販売実績.部門コード = 拠点名.fBshCode2                           LEFT OUTER JOIN
    dbo.tMik基本情報              基本情報
      ON 販売実績.販売先コード = 基本情報.fkjCliMicID
      ON 支払実績.受注番号 = 販売実績.受注番号
      ON Pca得意先.fptCliMicID = 販売実績.販売先コード                    LEFT OUTER JOIN
    dbo.tClient                   Client
      ON 販売実績.販売先コード = Client.fCliID                            LEFT OUTER JOIN
    dbo.tMikコードマスタ          販売店グループマスタ                    RIGHT OUTER JOIN
    dbo.tMik代理店リース会社      代理店リース会社
      ON 販売店グループマスタ.fcmコード種別 = 12 AND
         販売店グループマスタ.fcmコード = 代理店リース会社.fdl販売店グループ
      ON 販売実績.販売先コード = 代理店リース会社.fdlCliMicID





GO

