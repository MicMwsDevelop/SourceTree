//
// M_CODE.cs
//
// MWSコードマスタ管理クラス
// [charlieDB].[dbo].[M_CODE]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/02/20 勝呂):新規作成
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.Table
{
	public class M_CODE
	{
		public string GOODS_ID { get; set; }
		public int SERVICE_TYPE_ID { get; set; }
		public int SERVICE_ID { get; set; }
		public string SET_SALE { get; set; }
		public string REMARKS { get; set; }
		public string DELETE_FLG { get; set; }
		public DateTime? CREATE_DATE { get; set; }
		public string CREATE_PERSON { get; set; }
		public DateTime? UPDATE_DATE { get; set; }
		public string UPDATE_PERSON { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public M_CODE()
        {
			GOODS_ID = string.Empty;
			SERVICE_TYPE_ID = 0;
			SERVICE_ID = 0;
			SET_SALE = string.Empty;
			REMARKS = string.Empty;
			DELETE_FLG = string.Empty;
			CREATE_DATE = null;
			CREATE_PERSON = string.Empty;
			UPDATE_DATE = null;
			UPDATE_PERSON = string.Empty;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<M_CODE> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<M_CODE> result = new List<M_CODE>();
                foreach (DataRow row in table.Rows)
                {
					M_CODE data = new M_CODE
					{
						GOODS_ID = row["GOODS_ID"].ToString().Trim(),
						SERVICE_TYPE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_TYPE_ID"]),
						SERVICE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]),
						SET_SALE = row["SET_SALE"].ToString().Trim(),
						REMARKS = row["REMARKS"].ToString().Trim(),
						DELETE_FLG = row["DELETE_FLG"].ToString().Trim(),
						CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]),
						CREATE_PERSON = row["CREATE_PERSON"].ToString().Trim(),
						UPDATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]),
						UPDATE_PERSON = row["UPDATE_PERSON"].ToString().Trim(),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
	}
}
