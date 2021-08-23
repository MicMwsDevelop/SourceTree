//
// 売上予想.cs
//
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/08/04 勝呂)
//
using MwsLib.Common;
using MwsLib.DB;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.ProspectProgressAutoAggregate
{
	public class 売上予想
	{
        public YearMonth 集計月 { get; set; }
        public string 売上区分 { get; set; }
        public string 部門コード { get; set; }
        public string 部門名 { get; set; }
        public string 商品区分コード { get; set; }
        public string 商品区分名 { get; set; }
        public int 金額 { get; set; }

        /// <summary>
        /// 金額千円単位
        /// </summary>
        public int 金額千円単位
        {
            get
            {
                return string.Format("{0:#,}", 金額).ToInt();
            }
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public 売上予想()
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
        public static List<売上予想> DataTableToList(DataTable table, Date today)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<売上予想> result = new List<売上予想>();
                foreach (DataRow row in table.Rows)
                {
                    売上予想 data = new 売上予想
                    {
                        集計月 = today.ToYearMonth(),
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
