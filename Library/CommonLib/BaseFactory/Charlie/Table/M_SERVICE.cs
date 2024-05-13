//
// M_SERVICE.cs
//
// MWSサービスマスタクラス
// [charlieDB].[dbo].[M_SERVICE]
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
	public class M_SERVICE
	{
		/// <summary>
		/// サービスID
		/// </summary>
		public int SERVICE_ID { get; set; }

		/// <summary>
		/// サービス種別ID
		/// </summary>
		public int SERVICE_TYPE_ID { get; set; }

		/// <summary>
		/// 親サービスID
		/// </summary>
		public int PARENTS_SERVICE_ID { get; set; }

		/// <summary>
		/// サービス名
		/// </summary>
		public string SERVICE_NAME { get; set; }

		/// <summary>
		/// 有効無効フラグ 0:有効 1:無効
		/// </summary>
		public string UMU_FLG { get; set; }

		/// <summary>
		/// ソートキー
		/// </summary>
		public int SORT_KEY { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public string DELETE_FLG { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? CREATE_DATE { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CREATE_PERSON { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime? UPDATE_DATE { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UPDATE_PERSON { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public M_SERVICE()
        {
			SERVICE_ID = 0;
			SERVICE_TYPE_ID = 0;
			PARENTS_SERVICE_ID = 0;
			SERVICE_NAME = string.Empty;
			UMU_FLG = string.Empty;
			SORT_KEY = 0;
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
        public static List<M_SERVICE> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<M_SERVICE> result = new List<M_SERVICE>();
                foreach (DataRow row in table.Rows)
                {
					M_SERVICE data = new M_SERVICE
					{
						SERVICE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]),
						SERVICE_TYPE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_TYPE_ID"]),
						PARENTS_SERVICE_ID = DataBaseValue.ConvObjectToInt(row["PARENTS_SERVICE_ID"]),
						SERVICE_NAME = row["SERVICE_NAME"].ToString().Trim(),
						UMU_FLG = row["UMU_FLG"].ToString().Trim(),
						SORT_KEY = DataBaseValue.ConvObjectToInt(row["SORT_KEY"]),
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
