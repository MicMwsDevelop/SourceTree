USE [charlieDB]
GO

/****** Object:  View [dbo].[view_MWS顧客状況]    Script Date: 2019/11/25 15:32:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






--CREATE view [dbo].[view_MWS顧客状況]
ALTER view [dbo].[view_MWS顧客状況]
as
/*
   view_MWS顧客状況

   CharlieDB 用 JunpDB用_顧客状況参照ビュー
       Ver.1.00 2012/08/31 初版
       Ver.2.00 2012/10/15 ビューの名称変更
                           vMicCHARLIE顧客状況 → view_MWS顧客状況
       Ver.2.10 2013/01/16 [MWS_申込種別],[MWS_申込書回収日],[MWS_販売種別]項目追加
       Ver.2.20 2013/04/05 [MWS_使用許諾期限]項目追加
       Ver.2.30 2013/08/07 [MWS_販売店コード]項目追加
       Ver.2.40 2015/03/31 DtooLの使用期限を'2999/12/31'に設定したのでMWS_使用許諾期限にはそれを無視するよう変更
       Ver.2.50 2017/04/28 MWS_使用許諾期限に達人プラスV6など他社製アプリの使用期限を参照しないよう、
                           商品区分３=200～203のＭＩＣ製アプリのみを対象とした（課金対象外サービス含む）
       Ver.2.60 2017/06/05 hash で高速化
       Ver.2.61 2017/06/05 各種書類出力.xlsm がエラーになるので hash 高速化をとりやめ
       Ver.2.70 2017/06/26 非paletteユーザーも対象に追加するため USER_CLASSIFICATION = 1 も参照対象に追加
       Ver.2.80 2018/07/23 MWS_使用許諾期限 を 2018年7月に新設した契約情報ヘッダーから取得に切り替える準備
       Ver.3.00 2018/08/08 まとめ契約本番開始のため、使用許諾期限 を契約情報ヘッダーから取得に変更
       Ver.3.10 2019/11/26 MWS_申込種別にpaletteESの5を追加 by 勝呂

*/
select PRODUCT.CUSTOMER_ID          as 顧客ID
     , PRODUCT.PRODUCT_ID           as MWS_ID
     , PRODUCT.PASSWORD             as MWS_パスワード
     , PRODUCT.PASSWORD_READING     as MWS_パスワード読み
     , PRODUCT.TRIAL_FLG            as MWS_体験版フラグ
     , PRODUCT.END_FLG              as MWS_休止終了ステータス
     , PRODUCT.TRIAL_START_DATE     as MWS_利用開始日
     , PRODUCT.TRIAL_END_DATE       as MWS_利用終了日
     , PRODUCT.COMMISSION_PLACE     as MWS_委託先
     , PRODUCT.REMARKS              as MWS_備考
     , PRODUCT.REASON_CANCELLATION  as MWS_解約事由
     , PRODUCT.CANCELLATION_DATE    as MWS_解約日
     , PRODUCT.PAUSE_END_STATUS     as MWS_課金対象終了フラグ
     --Ver.3.10 2019/11/26 MWS_申込種別にpaletteESの5を追加 by 勝呂
     --, CUSTOMER.APPLY_TYPE          as MWS_申込種別
	 ,iif (CONTRACT.GoodsID = '800121', 5, CUSTOMER.APPLY_TYPE) as MWS_申込種別
     , CUSTOMER.APPLY_RECOVERY_DAY  as MWS_申込書回収日
     , CUSTOMER.SALE_TYPE           as MWS_販売種別
 --  , サービス状況.使用許諾期限    as MWS_使用許諾期限      -- 利用情報から利用終了日の最終値→契約情報が有効になったら下行に切り替え
     , CONTRACT.ContractEndDate     as MWS_使用許諾期限      -- 契約情報テーブルから取得
     , CUSTOMER.STORE_CODE          as MWS_販売店コード
from           T_PRODUCT_CONTROL      PRODUCT
    inner join T_CUSTOMER_FOUNDATIONS CUSTOMER on (PRODUCT.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID)
/*
    inner join             -- 利用情報から利用終了日の最終値を取得 → 契約情報が有効になったら削除
       (select USE_INFOMATION.CUSTOMER_ID       as CUSTOMER_ID
             , max(USE_INFOMATION.USE_END_DATE) as 使用許諾期限
        from  dbo.T_CUSSTOMER_USE_INFOMATION USE_INFOMATION
         inner join dbo.view_MWSサービス一覧 SERVICE_LIST
          on (SERVICE_LIST.商品区分 between 200 and 203) and USE_INFOMATION.SERVICE_ID=SERVICE_LIST.サービスコード
        where USE_INFOMATION.USE_END_DATE<'2999/12/31'
               and USE_INFOMATION.DELETE_FLG=0
           --  and USE_INFOMATION.PAUSE_END_STATUS=0
        group by USE_INFOMATION.CUSTOMER_ID) サービス状況 on (サービス状況.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID)
    --  契約情報ヘッダーから契約終了日を取得
    --  以下に２パターン作成しておくのでどちらかを使用してください
*/

       -- （１）顧客No ごとの長期契約終了日の最終値を取り出す
    left  join
      (select fCustomerID as CustomerID, max(fContractEndDate) as ContractEndDate, fGoodsID as GoodsID
        from T_USE_CONTRACT_HEADER
        where fContractFinalized=1 and fEndFlag=0 and fDeleteFlag=0 and
              (fContractType='ＶＰ' or fContractType='ＱＰ' or fContractType='ＵＧ' or fContractType='まとめ')
        group by fCustomerID, fGoodsID) CONTRACT on PRODUCT.CUSTOMER_ID = CONTRACT.CustomerID
    /*
       -- （２）顧客No ごとの有効な長期契約の終了日を取り出す
       -- 長期契約の契約満了で fEndFlag=1 への更新が保証されれば、
       -- 有効な長期契約レコードは単一になるので、
       -- 顧客No group 化で 契約終了日の最終値を取り出す必要はない 
    left  join
       (select fCustomerID as CustomerID, fContractEndDate as ContractEndDate
        from T_USE_CONTRACT_HEADER
        where fContractFinalized=1 and fEndFlag=0 and fDeleteFlag=0 and
              (fContractType='ＶＰ' or fContractType='ＱＰ' or fContractType='ＵＧ' or fContractType='まとめ')
       ) CONTRACT on PRODUCT.CUSTOMER_ID = CONTRACT.CustomerID
    */
where  ((PRODUCT.USER_CLASSIFICATION = N'0') or (PRODUCT.USER_CLASSIFICATION = N'1'))
    and (PRODUCT.CUSTOMER_ID <> 0)


GO

