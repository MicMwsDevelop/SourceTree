//
// vMicPCA商品マスタ.cs
//
// PCA商品マスタ情報クラス
// [JunpDB].[dbo].[vMicPCA商品マスタ]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using MwsLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.Junp.View
{
    /// <summary>
    /// PCA商品マスタ
    /// </summary>
	public class vMicPCA商品マスタ
	{
        public string sms_scd { get; set; }
        public string sms_mei { get; set; }
        public short sms_syskbn { get; set; }
        public short sms_mkbn { get; set; }
        public short sms_zkbn { get; set; }
        public short sms_jsk { get; set; }
        public string sms_tani { get; set; }
        public int sms_iri { get; set; }
        public int? sms_skbn1 { get; set; }
        public int? sms_skbn2 { get; set; }
        public int? sms_skbn3 { get; set; }
        public int? sms_skbn4 { get; set; }
        public int? sms_skbn5 { get; set; }
        public short sms_tax { get; set; }
        public short sms_komi { get; set; }
        public short sms_tketa { get; set; }
        public short sms_sketa { get; set; }
        public int sms_hyo { get; set; }
        public int sms_gen { get; set; }
        public int sms_bai1 { get; set; }
        public int sms_bai2 { get; set; }
        public int sms_bai3 { get; set; }
        public int sms_bai4 { get; set; }
        public int sms_bai5 { get; set; }
        public DateTime sms_upddate { get; set; }
        public int sms_rcd { get; set; }
        public int sms_ztan { get; set; }
        public short sms_state { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public vMicPCA商品マスタ()
        {
            sms_scd = string.Empty;
            sms_mei = string.Empty;
            sms_syskbn = 0;
            sms_mkbn = 0;
            sms_zkbn = 0;
            sms_jsk = 0;
            sms_tani = string.Empty;
            sms_iri = 0;
            sms_skbn1 = null;
            sms_skbn2 = null;
            sms_skbn3 = null;
            sms_skbn4 = null;
            sms_skbn5 = null;
            sms_tax = 0;
            sms_komi = 0;
            sms_tketa = 0;
            sms_sketa = 0;
            sms_hyo = 0;
            sms_gen = 0;
            sms_bai1 = 0;
            sms_bai2 = 0;
            sms_bai3 = 0;
            sms_bai4 = 0;
            sms_bai5 = 0;
            sms_upddate = DateTime.MinValue;
            sms_rcd = 0;
            sms_ztan = 0;
            sms_state = 0;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<vMicPCA商品マスタ> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<vMicPCA商品マスタ> result = new List<vMicPCA商品マスタ>();
                foreach (DataRow row in table.Rows)
                {
                    vMicPCA商品マスタ data = new vMicPCA商品マスタ
                    {
                        sms_scd = row["sms_scd"].ToString().Trim(),
                        sms_mei = row["sms_mei"].ToString().Trim(),
                        sms_syskbn = DataBaseValue.ConvObjectToShort(row["sms_syskbn"]),
                        sms_mkbn = DataBaseValue.ConvObjectToShort(row["sms_mkbn"]),
                        sms_zkbn = DataBaseValue.ConvObjectToShort(row["sms_zkbn"]),
                        sms_jsk = DataBaseValue.ConvObjectToShort(row["sms_jsk"]),
                        sms_tani = row["sms_tani"].ToString().Trim(),
                        sms_iri = DataBaseValue.ConvObjectToInt(row["sms_iri"]),
                        sms_skbn1 = DataBaseValue.ConvObjectToIntNull(row["sms_skbn1"]),
                        sms_skbn2 = DataBaseValue.ConvObjectToIntNull(row["sms_skbn2"]),
                        sms_skbn3 = DataBaseValue.ConvObjectToIntNull(row["sms_skbn3"]),
                        sms_skbn4 = DataBaseValue.ConvObjectToIntNull(row["sms_skbn4"]),
                        sms_skbn5 = DataBaseValue.ConvObjectToIntNull(row["sms_skbn5"]),
                        sms_tax = DataBaseValue.ConvObjectToShort(row["sms_tax"]),
                        sms_komi = DataBaseValue.ConvObjectToShort(row["sms_komi"]),
                        sms_tketa = DataBaseValue.ConvObjectToShort(row["sms_tketa"]),
                        sms_sketa = DataBaseValue.ConvObjectToShort(row["sms_sketa"]),
                        sms_hyo = DataBaseValue.ConvObjectToInt(row["sms_hyo"]),
                        sms_gen = DataBaseValue.ConvObjectToInt(row["sms_gen"]),
                        sms_bai1 = DataBaseValue.ConvObjectToInt(row["sms_bai1"]),
                        sms_bai2 = DataBaseValue.ConvObjectToInt(row["sms_bai2"]),
                        sms_bai3 = DataBaseValue.ConvObjectToInt(row["sms_bai3"]),
                        sms_bai4 = DataBaseValue.ConvObjectToInt(row["sms_bai4"]),
                        sms_bai5 = DataBaseValue.ConvObjectToInt(row["sms_bai5"]),
                        sms_upddate = DataBaseValue.ConvObjectToDateTime(row["sms_upddate"]),
                        sms_rcd = DataBaseValue.ConvObjectToInt(row["sms_ztan"]),
                        sms_ztan = DataBaseValue.ConvObjectToInt(row["sms_ztan"]),
                        sms_state = DataBaseValue.ConvObjectToShort(row["sms_state"]),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
    }
}
