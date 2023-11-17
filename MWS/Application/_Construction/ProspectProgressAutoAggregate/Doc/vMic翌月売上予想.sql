USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic翌月売上予想]    Script Date: 2021/05/10 12:20:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--ALTER VIEW [dbo].[実験用作成中ビュー]
CREATE VIEW [dbo].[vMic翌月売上予想]
as
/*
    vMic翌月売上予想

        翌月売上金額の予想値を算出する
        ２．ＰＣＡ商魂の当月売上（リース下取除く）
        ３．ＷＷ伝票のバリューパック伝票で納期が翌月日付の売上未計上分（リース下取除く）
        ４．ＷＷ伝票のバリューパック以外の伝票で納期が翌月日付の売上未計上分（リース下取除く）
        の金額（税抜き）を部門ごとに集計します


        Ver.1.00  2016/08/31  初版
        Ver.1.10  2016/11/11  コード整理
        Ver.2.00  2017/08/25  保守前受月額売上を廃止し→売上に移行
                              「保守・リース下取・ナルコーム・ＨＭＥを除く」を「リース下取のみ除く」に変更
        Ver.2.02  2018/02/01  商品区分２を追加 

/*
初版仕様
        当月売上金額の予想値を算出する
        １．保守前受収益から当月分売上（概算値）
        ２．ＰＣＡ商魂の当月売上（保守等除く）
        ３．ＷＷ伝票のバリューパック伝票で納期が今月日付の売上未計上分（保守等除く）
        ４．ＷＷ伝票のバリューパック以外の伝票で納期が今月日付の売上未計上分（保守等除く）
        の金額（税抜き）を部門ごとに集計します

        ※（保守等除く）は商品区分２の
            3: ソフト保守
            5: リース下取
            102: ﾅﾙｺｰﾑ関連
            103: HME関連
        を除外する
*/

*/
select choose(SL.区分No,
              '保守前受月額売上', 
              'ＰＣＡ－売上計上対象',
              'ＷＷ受注残－ＶＰ',
              'ＷＷ受注残－ＶＰ以外') as 売上区分
      ,SL.部門コード                  as 部門コード
      ,rtrim(BM.emsb_str)             as 部門名
      ,SL.商品区分２                  as 商品区分コード
      ,KM.ems_str                     as 商品区分名
      ,cast(SL.金額 as integer)       as 金額
from (
    /*  １．保守前受収益から当月分売上（概算値）  */
    /*      ユーザー情報の保守データを使用し当月計上分（概算値）を集計する  */
    /*      保守加入で有効ユーザー（終了ではない）                          */
    /*        →有効ユーザーの条件を外した                                  */
    /*      保守料をQURIAは55000円、U-BOXは90000円とし（販売店卸はその８掛）の1/12の金額を集計  */
/*
    select 1                                                                                    as 区分No
          ,right('00'+cast(B.fPca部門コード as varchar),3)                                      as 部門コード
          ,sum(((iif(U.システム略称='QURIA',55000,90000))*(iif(U.Sメンテ区分=2,(0.8),(1))))/12) as 金額
    from        vMic全ユーザー U
      left join tMih支店情報   B on B.f支店コード=U.支店コード
    where U.S保守=1 -- and U.有効ユーザーフラグ=1
    group by B.fPca部門コード

  union
*/
    /*  ２．ＰＣＡ商魂の当月売上（保守等除く）          */
    /*      ＰＣＡの当月売上計上を集計する              */
    /*      リース下取を除く  */
    select
           2                      as 区分No
     --    iif(H.f販売種別=1,1,2) as 区分No
          ,rtrim(D.sykd_jbmn)     as 部門コード
          ,sum(D.sykd_kingaku)    as 金額
          ,M.sms_skbn2            as 商品区分２
    from         vMicPCA売上明細   D
      inner join vMicPCA商品マスタ M on M.sms_scd  =D.sykd_scd
--    left  join tMih受注ヘッダ    H on H.f受注番号=D.sykd_denno
    where D.sykd_kingaku<>0
      and (D.sykd_uribi> cast(convert(nvarchar,eomonth(getdate(), 0),112) as integer))
      and (D.sykd_uribi<=cast(convert(nvarchar,eomonth(getdate(), 1),112) as integer))
 --   Ver.2.00  2017/08/25  「保守・リース下取・ナルコーム・ＨＭＥを除く」を「リース下取のみ除く」に変更
 --     and not(M.sms_skbn2=3 or M.sms_skbn2=5 or M.sms_skbn2=102 or M.sms_skbn2=103)
        and not(M.sms_skbn2=5)
    group by D.sykd_jbmn,M.sms_skbn2
 -- group by H.f販売種別,D.sykd_jbmn,M.sms_skbn2

  union
    /*  ３．ＷＷ伝票のバリューパック伝票で納期が今月日付の売上未計上分（保守等除く）      */
    /*  ４．ＷＷ伝票のバリューパック以外の伝票で納期が今月日付の売上未計上分（保守等除く）*/
    /*      ＷＷ伝票の受注残を集計する                 */
    /*      売上計上日がNULLのもの＝未計上             */
    /*      納期日付が当月の日付                       */
    /*      リース下取を除く                           */
    /*      バリューパック伝票とそれ以外を区分         */
    select iif(H.f販売種別=1,3,4)                          -- as 区分No
          ,right('00'+cast(B.fPca部門コード as varchar),3) -- as 部門コード
          ,sum(D.f提供価格)                                -- as 金額
          ,D.f商品区分2                                    -- as 商品区分２
    from         tMih受注ヘッダ H
      inner join tMih受注詳細   D on D.f受注番号  =H.f受注番号
      left  join tMih支店情報   B on B.f支店コード=H.fBshCode3
    where (H.f売上計上日 IS NULL)
      and (H.f納期> eomonth(getdate(), 0))
      and (H.f納期<=eomonth(getdate(), 1))
 --   Ver.2.00  2017/08/25  「保守・リース下取・ナルコーム・ＨＭＥを除く」を「リース下取のみ除く」に変更
 --   and not(D.f商品区分2=3 or D.f商品区分2=5 or D.f商品区分2=102 or D.f商品区分2=103)
      and not(D.f商品区分2=5)
    group by H.f販売種別,B.fPca部門コード,D.f商品区分2

) SL left join vMicPCA部門マスタ BM on BM.emsb_kbn=SL.部門コード
     left join vMicPCA区分マスタ KM on KM.ems_kbn =SL.商品区分２ and KM.ems_id=22

GO

