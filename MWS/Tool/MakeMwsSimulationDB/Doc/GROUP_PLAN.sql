/****** SSMS の SelectTopNRows コマンドのスクリプト  ******/
SELECT [goods_id] AS GoodsID
      ,[plan_rank] AS SeqNo
      ,[goods_kbn]
      --,[sort_no]
      ,[plan_nm] AS GroupName
      --,[coment_1]
      --,[coment_2]
      --,[coment_3]
      --,[open_date]
      --,[close_date]
      --,[del_flg]
      --,[create_date]
      --,[create_user]
      --,[update_date]
      --,[update_user]
      ,[keiyaku_month_cnt] AS KeiyakuMonth
      ,[free_use_month] AS FreeMonth
      --,[set_id]
      ,[min_amount] AS MinAmmount
      ,[max_amount] AS MaxAmmount
  FROM [COUPLER].[dbo].[GROUP_PLAN]
  WHERE [del_flg] = 0 --AND [goods_kbn] = 204
  ORDER BY [goods_id] ASC, [plan_rank] ASC