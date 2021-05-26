//
// tMic離島.cs
//
// 離島情報クラス
// [JunpDB].[dbo].[tMic離島]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using MwsLib.DB;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.Junp.Table
{
    public class tMic離島
	{
		public int ID { get; set; }
		public string 離島住所 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMic離島()
		{
			ID = 0;
			離島住所 = string.Empty;
		}

		/// <summary>
		/// 佐川急便では離島のため代引き発送の取扱いが不能な地域の判定
		/// </summary>
		/// <param name="address">住所</param>
		/// <returns>判定</returns>
		public bool Is代引発送不可(string address)
		{
			if (離島住所 == address.Substring(離島住所.Length))
			{
				return true;
			}
			return false;
		}

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<tMic離島> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<tMic離島> result = new List<tMic離島>();
                foreach (DataRow row in table.Rows)
                {
                    tMic離島 data = new tMic離島
                    {
                        ID = DataBaseValue.ConvObjectToInt(row["ID"]),
                        離島住所 = row["離島住所"].ToString().Trim()
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<string> DataTableToStringList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<string> result = new List<string>();
                foreach (DataRow row in table.Rows)
                {
                    result.Add(row["離島住所"].ToString().Trim());
                }
                return result;
            }
            return null;
        }
    }
}
