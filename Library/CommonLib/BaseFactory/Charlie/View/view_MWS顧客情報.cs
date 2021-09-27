//
// view_MWS顧客情報.cs
//
// MWS顧客情報ビュークラス
// [CharlieDB].[dbo].[view_MWS顧客情報]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/02/05 勝呂)
// 
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.View
{
	public class view_MWS顧客情報
    {
        public string MWSID { get; set; }
        public int 顧客No { get; set; }
        public string 得意先No { get; set; }
        public string 顧客名 { get; set; }
        public string 県番号 { get; set; }
        public string 都道府県名 { get; set; }
        public string 住所 { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public view_MWS顧客情報()
        {
            MWSID = string.Empty;
            顧客No = 0;
            得意先No = string.Empty;
            顧客名 = string.Empty;
            県番号 = string.Empty;
            都道府県名 = string.Empty;
            住所 = string.Empty;
        }

        /// <summary>
        /// [charlieDB].[dbo].[view_MWS顧客情報]の詰め替え
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <returns>V_COUPLER_APPLY</returns>
        public static List<view_MWS顧客情報> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<view_MWS顧客情報> result = new List<view_MWS顧客情報>();
                foreach (DataRow row in table.Rows)
                {
                    view_MWS顧客情報 data = new view_MWS顧客情報
                    {
                        MWSID = row["MWSID"].ToString().Trim(),
                        顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
                        得意先No = row["得意先No"].ToString().Trim(),
                        顧客名 = row["顧客名"].ToString().Trim(),
                        県番号 = row["県番号"].ToString().Trim(),
                        都道府県名 = row["都道府県名"].ToString().Trim(),
                        住所 = row["住所"].ToString().Trim(),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
    }
}
