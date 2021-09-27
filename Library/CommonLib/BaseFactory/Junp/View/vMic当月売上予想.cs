//
// vMic当月売上予想.cs
//
// vMic当月売上予想
// [JunpDB].[dbo].[vMic当月売上予想]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2021/05/07 勝呂)
//
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.View
{
	public class vMic当月売上予想
	{
        public string 売上区分{get;set;}
        public string 部門コード { get; set; }
        public string 部門名 { get; set; }
        public string 商品区分コード { get; set; }
        public string 商品区分名 { get; set; }
        public int 金額 { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public vMic当月売上予想()
        {
            売上区分 = string.Empty;
            部門コード = string.Empty;
            部門名 = string.Empty;
            商品区分コード = string.Empty;
            商品区分名 = string.Empty;
            金額 = 0;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<vMic当月売上予想> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<vMic当月売上予想> result = new List<vMic当月売上予想>();
                foreach (DataRow row in table.Rows)
                {
                    vMic当月売上予想 data = new vMic当月売上予想
                    {
                        売上区分 = row["売上区分"].ToString().Trim(),
                        部門コード = row["部門コード"].ToString().Trim(),
                        部門名 = row["部門名"].ToString().Trim(),
                        商品区分コード = row["商品区分コード"].ToString().Trim(),
                        商品区分名 = row["商品区分名"].ToString().Trim(),
                        金額 = DataBaseValue.ConvObjectToInt(row["金額"]),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
    }
}
