//
// 売上実績.cs
//
// 売上実績クラス
// [charlieDB].[dbo].[売上実績]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.Table
{
	public class 売上実績
	{
        public int 実績日{get;set;}
        public string 営業部コード { get; set; }
        public short 予算VP { get; set; }
        public short 予算ES { get; set; }
        public short 予算まとめ { get; set; }
        public int 予算売上 { get; set; }
        public int 予算営業損益 { get; set; }
        public short 予測VP { get; set; }
        public short 予測ES { get; set; }
        public short 予測まとめ { get; set; }
        public int 予測売上 { get; set; }
        public int 予測営業損益 { get; set; }
        public short 実績VP { get; set; }
        public short 実績ES { get; set; }
        public short 実績まとめ { get; set; }
        public int 実績売上 { get; set; }
        public int 実績営業損益 { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public 売上実績()
        {
            実績日 = 0;
            営業部コード = string.Empty;
            予算VP = 0;
            予算ES = 0;
            予算まとめ = 0;
            予算売上 = 0;
            予算営業損益 = 0;
            予測VP = 0;
            予測ES = 0;
            予測まとめ = 0;
            予測売上 = 0;
            予測営業損益 = 0;
            実績VP = 0;
            実績ES = 0;
            実績まとめ = 0;
            実績売上 = 0;
            実績営業損益 = 0;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<売上実績> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<売上実績> result = new List<売上実績>();
                foreach (DataRow row in table.Rows)
                {
                    売上実績 data = new 売上実績
                    {
                        実績日 = DataBaseValue.ConvObjectToInt(row["実績日"]),
                        営業部コード = row["営業部コード"].ToString().Trim(),
                        予算VP = DataBaseValue.ConvObjectToShort(row["予算VP"]),
                        予算ES = DataBaseValue.ConvObjectToShort(row["予算ES"]),
                        予算まとめ = DataBaseValue.ConvObjectToShort(row["予算まとめ"]),
                        予算売上 = DataBaseValue.ConvObjectToInt(row["予算売上"]),
                        予算営業損益 = DataBaseValue.ConvObjectToInt(row["予算営業損益"]),
                        予測VP = DataBaseValue.ConvObjectToShort(row["予測VP"]),
                        予測ES = DataBaseValue.ConvObjectToShort(row["予測ES"]),
                        予測まとめ = DataBaseValue.ConvObjectToShort(row["予測まとめ"]),
                        予測売上 = DataBaseValue.ConvObjectToInt(row["予測売上"]),
                        予測営業損益 = DataBaseValue.ConvObjectToInt(row["予測営業損益"]),
                        実績VP = DataBaseValue.ConvObjectToShort(row["実績VP"]),
                        実績ES = DataBaseValue.ConvObjectToShort(row["実績ES"]),
                        実績まとめ = DataBaseValue.ConvObjectToShort(row["実績まとめ"]),
                        実績売上 = DataBaseValue.ConvObjectToInt(row["実績売上"]),
                        実績営業損益 = DataBaseValue.ConvObjectToInt(row["実績営業損益"]),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
    }
}
