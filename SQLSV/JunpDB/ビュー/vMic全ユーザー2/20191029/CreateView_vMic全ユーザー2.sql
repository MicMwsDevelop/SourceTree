USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic全ユーザー2]    Script Date: 2019/10/29 16:34:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--CREATE VIEW [dbo].[vMic全ユーザー2]
ALTER VIEW [dbo].[vMic全ユーザー2]
AS
/*
    vMic全ユーザー2

     2011/03/02  [代引配送]フィールドを追加
                   請求先設定があるときは請求先が
                   それ以外はユーザーが 得意先区分３＝７０のとき  代引配送=1 とした
                 [代引配送]フィールド追加に併せて既存の[請求締日]フィールドも
                 請求先設定があるときは請求先の締日を参照するよう変更した
                 さらに、[請求回収日]フィールドも追加
                 請求先設定があるときは請求先の、それ以外はユーザーの回収日を参照可能にした
     2011/03/10  [請求区分No]を追加
                   請求先設定があるときは請求先の
                   それ以外はユーザーの 得意先区分３ を[請求区分No]で参照することにした
                   PCADBの区分マスタから請求区分文字列を参照しようとしたが実行に時間がかかり断念
     2011/04/27  [請求区分]をコメントアウト
     2011/08/08  レセ電算関連フィールドを追加
                 コメントアウトしていた[請求区分]を HASH 使用で高速化により復活
     2011/11/21  営業担当者コード、営業担当者名フィールドを追加
     2011/12/06  eStore登録フラグ、eStore登録メールアドレス フィールドを追加
     2012/09/10  eStore_パスワード、eStore_パスワード読み、MWS_ID、MWS_パスワード、MWS_パスワード読み フィールドを追加
     2012/10/18  参照先ビューの名称変更 vMicCHARLIE顧客状況 → vMic_MWS顧客状況
     2013/01/16  [MWS_申込種別],[MWS_申込書回収日],[MWS_販売種別]項目追加
     2013/04/05  [MWS_使用許諾期限]項目追加
     2013/05/28  [UPG時利用期限][残月数]項目追加
     2013/08/07  [販売店ID][販売店名称][販売店グループコード][販売店グループ名称][販売店区分コード][販売店区分名称]項目追加
                   ＭＷＳユーザーはＣｈａｒｌｉｅに登録された販売店情報から、
                   旧システムユーザーはＷＷユーザー情報の販売店名(WWNo:xxxxxxxx/・・・)の情報から
                   販売店IDをとりだし、名称、グループ、区分はＷＷ顧客の販売店データを参照
     2013/12/04  [レセ電算予定年月]項目追加
                 高速化（ほとんど効果なし）
     2014/01/10  [レセ電算媒体],[レセ電算オンライン],[レセ電算請求済]項目追加
     2014/03/12  [販売店担当者名]項目追加
     2014/04/22  [インスト担当者コード],[インスト担当者名]項目追加 (塚田)
     2014/04/23  少しだけ高速化
     2014/05/29  [メルマガ購読フラグ]追加、[インスト担当者コード],[インスト担当者名]項目列位置変更
     2014/06/27  [更新日時]項目追加
                 dbo.tClient、dbo.tMik基本情報、dbo.tMikユーザ の最終更新日時
     2014/08/04  [有効ユーザーフラグ]追加（[システム名]が登録されていて、終了ユーザーではなく改正対応ユーザーでもない時=1）
     2014/09/03  少しだけ高速化 
     2014/09/10  PCAで新請求締日グループ作成したがUSER検索.MDBの表示で誤解のないように対応
     2015/07/30  [同時接続クライアント数]項目追加
     2015/09/03  販売店情報を新WonderWebで新規追加した[販売店No](dbo.tMikユーザ.fus販売店No)から導出に変更
     2015/09/09  H保守 関連項目 NULL 出力
     2015/09/15  ODBC接続エラーのためH保守関連項目を元に戻す
     2015/10/19  再度H保守 関連項目 NULL 出力
     2016/03/24  レセ電    関連項目 NULL 出力,case → iif
     2016/04/13  [レセ電算請求種別],[レセ電算請求開始]項目を新WonderWebで新規追加したdbo.tMikユーザからから導出に変更
	 2019/07/22	ソフト保守契約の情報をPC安心サポート契約テーブルに変更 by 勝呂
	 2019/10/29	Sメンテ契約期間を[fBillingStartDate]から[fContractStartDate]、[fBillingEndDate]から[fContractEndDate]にそれぞれ変更、 by 勝呂
*/
SELECT vUSER基本.担当者コード               as 担当者コード
     , vUSER基本.担当者名                   as 担当者名
     , vUSER基本.支店コード                 as 支店コード
     , vUSER基本.支店名                     as 支店名
     , vUSER基本.システム名称               as システム名称
     , vUSER基本.終了フラグ                 as 終了フラグ
     , vUSER基本.顧客No                     as 顧客No
     , vUSER基本.得意先No                   as 得意先No
     , vUSER基本.顧客名１                   as 顧客名１
     , vUSER基本.顧客名２                   as 顧客名２
     , vUSER基本.フリガナ                   as フリガナ
     , vUSER基本.郵便番号                   as 郵便番号
     , vUSER基本.住所１                     as 住所１
     , vUSER基本.住所２                     as 住所２
     , vUSER基本.住所フリガナ               as 住所フリガナ
     , vUSER基本.電話番号                   as 電話番号
     , vUSER基本.FAX番号                    as FAX番号
     , vUSER基本.売伝No                     as 売伝No
     , vUSER基本.医保医療コード             as 医保医療コード
     , vUSER基本.国保医療コード             as 国保医療コード
     , vUSER基本.院長名                     as 院長名
     , vUSER基本.院長名フリガナ             as 院長名フリガナ
     , vUSER基本.発送先名                   as 発送先名
     , vUSER基本.発送先郵便番号             as 発送先郵便番号
     , vUSER基本.発送先住所                 as 発送先住所
     , vUSER基本.発送先電話番号             as 発送先電話番号
     , vUSER基本.発送先備考                 as 発送先備考
     , vUSER基本.請求先コード               as 請求先コード
     , vUSER基本.請求先名                   as 請求先名
     , vUSER基本.請求先郵便番号             as 請求先郵便番号
     , vUSER基本.請求先住所                 as 請求先住所
     , vUSER基本.請求先電話番号             as 請求先電話番号
     , vUSER基本.請求先備考                 as 請求先備考
     , vUSER基本.システム名                 as システム名
     , vUSER基本.オプション1                as オプション1
     , vUSER基本.オプション2                as オプション2
     , vUSER基本.オプション3                as オプション3
     , vUSER基本.オプション4                as オプション4
     , vUSER基本.オプション5                as オプション5
     , vUSER基本.オプション6                as オプション6
     , vUSER基本.レセプト用紙               as レセプト用紙
     , vUSER基本.連単                       as 連単
     , vUSER基本.カルテ用紙                 as カルテ用紙
     , vUSER基本.処方箋用紙                 as 処方箋用紙
     , vUSER基本.領収書用紙                 as 領収書用紙
     , vUSER基本.領収書用紙２               as 領収書用紙２
     , vUSER基本.メディア                   as メディア
     , vUSER基本.ＦＤ種                     as ＦＤ種
     , vUSER基本.納品月                     as 納品月
     , vUSER基本.売上月                     as 売上月
     , vUSER基本.単体                       as 単体
     , vUSER基本.サーバー                   as サーバー
     , vUSER基本.クライアント               as クライアント
     , vUSER基本.販売店名                   as 販売店名
     , vUSER基本.LicensedKey                as LicensedKey
     , vUSER基本.バージョン情報             as バージョン情報
     , vUSER基本.販売形態                   as 販売形態
     , vUSER基本.代行回収                   as 代行回収
     , vUSER基本.S保守契約                  as S保守契約
     , vUSER基本.H保守契約                  as H保守契約
     , vUSER基本.ハード構成                 as ハード構成
     , vUSER基本.リース情報                 as リース情報
     , vUSER基本.登録カード回収             as 登録カード回収
     , vUSER基本.保守契約書回収             as 保守契約書回収
     , vUSER基本.代行回収回収               as 代行回収回収
     , vUSER基本.改正時情報                 as 改正時情報
     , vUSER基本.休診日                     as 休診日
     , vUSER基本.診療時間                   as 診療時間
     , vUSER基本.メールアドレス             as メールアドレス
     , vUSER基本.ClientLicense1             as ClientLicense1
     , vUSER基本.ClientLicense2             as ClientLicense2
     , vUSER基本.ClientLicense3             as ClientLicense3
     , vUSER基本.ClientLicense4             as ClientLicense4
     , vUSER基本.ClientLicense6             as ClientLicense6
     , vUSER基本.ClientLicense5             as ClientLicense5
     , vUSER基本.ClientLicense7             as ClientLicense7
     , vUSER基本.ClientLicense8             as ClientLicense8
     , vUSER基本.ClientLicense9             as ClientLicense9
     , vUSER基本.ClientLicense10            as ClientLicense10
     , vUSER基本.ClientLicense11            as ClientLicense11
     , vUSER基本.ClientLicense12            as ClientLicense12
     , vUSER基本.ＯＳ                       as ＯＳ
     , vUSER基本.登録カード回収日           as 登録カード回収日
     , vUSER基本.ODeS加入                   as ODeS加入
     , vUSER基本.前システム名               as 前システム名
     , vUSER基本.前システム終了             as 前システム終了
     , vUSER基本.備考                       as 備考
     , vUSER基本.前システム名称             as 前システム名称
     , tリース情報.fleリース店名            as リース店名
     , tリース情報.fle契約No                as 契約No
     , tリース情報.fle期間                  as 期間
     , tリース情報.fleリース開始            as リース開始
     , tリース情報.fleリース終了            as リース終了
     , tリース情報.fleリース料              as リース料
     , tリース情報.fle残回数                as 残回数
     , tリース情報.fle残金額                as 残金額
     , tリース情報.fleリース契約備考        as リース契約備考
      ,iif(PCS.fApplyNo is null, '0', '1') as S保守		-- 2019/07/22 変更
      ,CONVERT(NVARCHAR, PCS.fApplyDate, 111) as S契約書回収年月
      ,NULL AS S売計上
      ,PCS.fYears AS S契約年数
      ,CAST(サービス一覧.商品単価 AS int) AS Sメンテ料金
      ,FORMAT(PCS.fContractStartDate, 'yyyy/MM') as Sメンテ契約開始	-- 2019/10/29 変更
      ,FORMAT(PCS.fContractEndDate, 'yyyy/MM') as Sメンテ契約終了	-- 2019/10/29 変更
      ,NULL AS Sメンテ契約備考1
      ,NULL AS Sメンテ契約備考2
      ,NULL AS S契約名義
      ,NULL AS Sメンテ請求先コード
      ,NULL AS Sメンテ請求先名
      ,NULL AS Sメンテ区分
      ,NULL AS S卸BM先名
      ,NULL AS S金額
     , NULL                                 as H保守                  -- 2015/10/19 変更
     , NULL                                 as H契約書回収年月
     , NULL                                 as H売計上
     , NULL                                 as H契約年数
     , NULL                                 as Hメンテ料金
     , NULL                                 as Hメンテ契約開始
     , NULL                                 as Hメンテ契約終了
     , NULL                                 as Hメンテ契約備考1
     , NULL                                 as Hメンテ契約備考2
     , NULL                                 as H契約名義
     , NULL                                 as Hメンテ請求先コード
     , NULL                                 as Hメンテ請求先名
     , NULL                                 as Hメンテ区分
     , NULL                                 as H卸BM先名
     , NULL                                 as H金額
     , t代行回収.fdaAPLUSコード             as 代行回収APLUSコード
     , t代行回収.fda銀行名カナ              as 代行回収銀行名カナ
     , t代行回収.fda銀行コード              as 代行回収銀行コード
     , t代行回収.fda支店名カナ              as 代行回収支店名カナ
     , t代行回収.fda支店コード              as 代行回収支店コード
     , t代行回収.fda預金種別                as 代行回収預金種別
     , t代行回収.fda口座番号                as 代行回収口座番号
     , t代行回収.fda預金者名                as 代行回収預金者名
     , t代行回収.fda上限金額                as 代行回収上限金額
     , t代行回収.fda最終引落日              as 代行回収最終引落日
     , t代行回収.fda状態                    as 代行回収状態
     , t代行回収.fda備考                    as 代行回収備考
     , t得意先.fpt適用売価No                as 適用売価No
     , q請求方法.請求締日                   as 請求締日
     , q請求方法.請求回収日                 as 請求回収日
     , q請求方法.請求得意先区分             as 請求区分No
     , q請求方法.請求区分                   as 請求区分
     , q請求方法.代引配送                   as 代引配送
     , t県番号.県番号                       as 県番号
     , t県番号.都道府県名                   as 都道府県名
     , t略名マスタ.fcm名称                  as システム略称
     , NULL                                 as オンライン             -- 2016/03/24 変更
     , vUSER基本.レセ電算請求種別           as レセ電算請求種別
     , NULL                                 as レセ電算確認試験
     , vUSER基本.レセ電算請求開始           as レセ電算請求開始
     , NULL                                 as レセ電算オンライン提出予定フラグ
     , NULL                                 as レセ電算インターネット利用情報
     , NULL                                 as レセ電算利用プロバイダ
     , NULL                                 as レセ電算利用回線
     , NULL                                 as レセ電算回線既設場所
     , NULL                                 as レセ電算オンライン請求PC
     , NULL                                 as レセ電算オンライン確認試験
     , NULL                                 as レセ電算オンライン請求開始
     , NULL                                 as レセ電算リンク作業状況
     , NULL                                 as レセ電算予定年月
     , NULL                                 as レセ電算エントリーチェック
     , NULL                                 as レセ電算受付担当者
     , NULL                                 as レセ電算受付日
     , NULL                                 as レセ電算作業区分
     , NULL                                 as レセ電算発送依頼者
     , NULL                                 as レセ電算発送日
     , NULL                                 as レセ電算作業担当者
     , NULL                                 as レセ電算作業完了日
     , NULL                                 as レセ電算作業完了フラグ
     , NULL                                 as レセ電算備考
     , NULL                                 as レセ電算媒体
     , NULL                                 as レセ電算オンライン
     , NULL                                 as レセ電算請求済
     , v営業担当.営業担当者コード           as 営業担当者コード
     , v営業担当.営業担当者名               as 営業担当者名
     , vインスト担当.インスト担当者コード   as インスト担当者コード
     , vインスト担当.インスト担当者名       as インスト担当者名
     , iif(veStoreCust.メールアドレス<>'', 1, 0)
                                            as eStore登録フラグ
     , iif(veStoreCust.メールアドレス<>'' and veStoreCust.メルマガ購読='1', 1, 0)
                                            as メルマガ購読フラグ
     , veStoreCust.メールアドレス           as eStore登録メールアドレス
     , veStoreCust.パスワード               as eStore_パスワード
     , veStoreCust.パスワード読み           as eStore_パスワード読み
     , vCharlie顧客.MWS_ID                  as MWS_ID
     , vCharlie顧客.MWS_パスワード          as MWS_パスワード
     , vCharlie顧客.MWS_パスワード読み      as MWS_パスワード読み
     , vCharlie顧客.MWS_申込種別            as MWS_申込種別
     , vCharlie顧客.MWS_申込書回収日        as MWS_申込書回収日
     , vCharlie顧客.MWS_販売種別            as MWS_販売種別
     , vCharlie顧客.MWS_使用許諾期限        as MWS_使用許諾期限
     , vUPG_USER.利用期限                   as UPG時利用期限
     , vUPG_USER.残月数                     as 残月数
     , vRESALER.顧客No                      as 販売店ID
     , vRESALER.顧客名１+vRESALER.顧客名２  as 販売店名称
     , vRESALER.販売店グループ              as 販売店グループコード
     , vRESALER.販売店グループ名            as 販売店グループ名称
     , vRESALER.販売店区分コード            as 販売店区分コード
     , vRESALER.販売店区分名称              as 販売店区分名称
     , vUSER基本.販売店担当者名             as 販売店担当者名
     , vUSER基本.更新日時                   as 更新日時
     , iif((vUSER基本.終了フラグ=0) and
           (vUSER基本.システム名>'000' and vUSER基本.システム名<'999') and
           (vUSER基本.改正時情報 not Like '%改正対応なし%'), 1 , 0)
                                            as 有効ユーザーフラグ
     , vUSER基本.同時接続クライアント数     as 同時接続クライアント数
from dbo.vMicユーザー基本2 vUSER基本
  inner join dbo.tMikPca得意先        t得意先       on (vUSER基本.顧客No=t得意先.fptCliMicID)
  left  join
     (select TMS.fpt得意先No    as 請求得意先No
            ,TMS.fpt得意先区分3 as 請求得意先区分
        --  ,TMS.fpt請求締日1   as 請求締日   2014/09/10  PCAで新請求締日グループ作成したがUSER検索.MDBの表示で誤解のないように対応
            ,(case TMS.fpt請求締日1
                when '11' then '10'
                when '19' then '20'
                when '29' then '31'
                when '32' then '31'
                else TMS.fpt請求締日1
              end)              as 請求締日
            ,TMS.fpt回収日1     as 請求回収日
            ,RTRIM(ems.ems_str) as 請求区分
            ,基本.fkj顧客区分   as 請求顧客区分
            ,iif((基本.fkj顧客区分=2 or 基本.fkj顧客区分=18) and (TMS.fpt得意先区分3=70), 1 ,0)
                                as 代引配送
      from dbo.tMikPca得意先 TMS
        left  join dbo.tMik基本情報      基本 on (基本.fkjCliMicID=TMS.fptCliMicID)
        left  join dbo.vMicPCA区分マスタ ems  on ((ems.ems_id=13) and (ems.ems_kbn=TMS.fpt得意先区分3))
     )                                q請求方法     on
        (q請求方法.請求得意先No=
         iif(isnull(vUSER基本.請求先コード,'')='',(vUSER基本.得意先No) ,(vUSER基本.請求先コード)))
  left  join dbo.tMikリース情報       tリース情報   on (vUSER基本.顧客No=tリース情報.fleCliMicID)
--  left  join dbo.tMik保守契約         t保守契約     on (vUSER基本.顧客No=t保守契約.fhsCliMicID)
  left  join dbo.tMik代行回収         t代行回収     on (vUSER基本.顧客No=t代行回収.fdaCliMicID)
  left  join dbo.tMik県番号           t県番号       on (LEFT(vUSER基本.住所１,3)=LEFT(t県番号.都道府県名,3))
  left  join dbo.tMikコードマスタ     t略名マスタ   on ((vUSER基本.システム名=t略名マスタ.fcmコード) and (t略名マスタ.fcmコード種別='91'))
  left  join dbo.vMic営業担当         v営業担当     on (vUSER基本.顧客No=v営業担当.顧客No)
  left  join dbo.vMicインスト担当     vインスト担当 on (vUSER基本.顧客No=vインスト担当.顧客No)
  left  join dbo.vMic_estore_Cust_mst veStoreCust   on (vUSER基本.顧客No=veStoreCust.顧客No)
  left  join dbo.vMic_MWS顧客状況     vCharlie顧客  on (vUSER基本.顧客No=vCharlie顧客.顧客ID)
  left join dbo.T_USE_PCCSUPPORT as PCS on vUSER基本.顧客No = PCS.fCustomerID
  left join dbo.vMic_MWSサービス一覧 as サービス一覧 on サービス一覧.サービスコード = PCS.fServiceId
  left  join
     (select U.fusCliMicID as 顧客No
            ,left(CONVERT(VARCHAR(10),dateadd(month,6*12-1,CONVERT(DATETIME,(U.fus納品月+'/01'))),111),7) as 利用期限
            ,datediff(month,Getdate(),dateadd(month,6*12  ,CONVERT(DATETIME,(U.fus納品月+'/01'))))        as 残月数
      from dbo.tMikユーザ U
      where ((U.fusシステム名 = '047' or U.fusシステム名 = '048') and (U.fus納品月>='2008/06'))
     )                                vUPG_USER     on (vUSER基本.顧客No=vUPG_USER.顧客No)
  left  join dbo.vMic全販売店         vRESALER      on (vUSER基本.販売店No=vRESALER.顧客No)





GO

