//
// vMicPCA担当者マスタ.cs
//
// PCA担当者マスタ情報クラス
// [JunpDB].[dbo].[vMicPCA担当者マスタ]
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
    public class vMicPCA担当者マスタ
	{
        public string emst_kbn { get; set; }
        public string emst_str { get; set; }
        public string emst_bmn { get; set; }
        public int emst_kosin { get; set; }
        public DateTime? emst_upddate { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public vMicPCA担当者マスタ()
        {
            emst_kbn = string.Empty;
            emst_str = string.Empty;
            emst_bmn = string.Empty;
            emst_kosin = 0;
            emst_upddate = null;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<vMicPCA担当者マスタ> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<vMicPCA担当者マスタ> result = new List<vMicPCA担当者マスタ>();
                foreach (DataRow row in table.Rows)
                {
                    vMicPCA担当者マスタ data = new vMicPCA担当者マスタ
                    {
                        emst_kbn = row["emst_kbn"].ToString().Trim(),
                        emst_str = row["emst_str"].ToString().Trim(),
                        emst_bmn = row["emst_bmn"].ToString().Trim(),
                        emst_kosin = DataBaseValue.ConvObjectToInt(row["emst_kosin"]),
                        emst_upddate = DataBaseValue.ConvObjectToDateTimeNull(row["emst_upddate"]),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
    }
}
