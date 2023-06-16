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
		/// <returns>MWSコードマスタリスト</returns>
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

		/// <summary>
		/// DataTable → クラス
		/// </summary>
		/// <param name="table"></param>
		/// <returns>MWSコードマスタ</returns>
		public static M_CODE DataTableToData(DataTable table)
		{
			if (null != table && 1 == table.Rows.Count)
			{
				DataRow row = table.Rows[0];
				M_CODE result = new M_CODE();
				result.GOODS_ID = row["GOODS_ID"].ToString().Trim();
				result.SERVICE_TYPE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_TYPE_ID"]);
				result.SERVICE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]);
				result.SET_SALE = row["SET_SALE"].ToString().Trim();
				result.REMARKS = row["REMARKS"].ToString().Trim();
				result.DELETE_FLG = row["DELETE_FLG"].ToString().Trim();
				result.CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]);
				result.CREATE_PERSON = row["CREATE_PERSON"].ToString().Trim();
				result.UPDATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]);
				result.UPDATE_PERSON = row["UPDATE_PERSON"].ToString().Trim();
				return result;
			}
			return null;
		}
	}

	public class M_CODE_EX : M_CODE
	{
		public string SERVICE_TYPE_NAME { get; set; }
		public string SERVICE_NAME { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public M_CODE_EX() : base()
		{
			SERVICE_TYPE_NAME = string.Empty;
			SERVICE_NAME = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static new List<M_CODE_EX> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<M_CODE_EX> result = new List<M_CODE_EX>();
				foreach (DataRow row in table.Rows)
				{
					M_CODE_EX data = new M_CODE_EX
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
						SERVICE_TYPE_NAME = row["SET_SALE"].ToString().Trim(),
						SERVICE_NAME = row["SERVICE_NAME"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
