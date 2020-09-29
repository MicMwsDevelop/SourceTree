USE [JunpDB]
GO

/****** Object:  View [dbo].[vMic先月売上実績]    Script Date: 2020/03/02 17:00:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vMic先月売上実績]
--ALTER VIEW [dbo].[vMic先月売上実績]
as
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
from
(
select
           2                      as 区分No
          ,rtrim(D.sykd_jbmn)     as 部門コード
          ,sum(D.sykd_kingaku)    as 金額
          ,M.sms_skbn2            as 商品区分２
    from         vMicPCA売上明細   D
      inner join vMicPCA商品マスタ M on M.sms_scd  =D.sykd_scd
    where D.sykd_kingaku <> 0
      and D.sykd_uribi >= convert(int, convert(nvarchar, DATEADD(dd, 1, EOMONTH(getdate(), -2)), 112))
      and D.sykd_uribi <= convert(int, convert(nvarchar, EOMONTH(getdate(), -1), 112))
		group by D.sykd_jbmn,M.sms_skbn2
) SL left join vMicPCA部門マスタ BM on BM.emsb_kbn=SL.部門コード
     left join vMicPCA区分マスタ KM on KM.ems_kbn =SL.商品区分２ and KM.ems_id=22

GO

