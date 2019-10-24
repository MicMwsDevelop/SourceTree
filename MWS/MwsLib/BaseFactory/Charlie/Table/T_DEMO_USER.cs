//
// T_DEMO_USER.cs
//
// デモ用ID管理テーブル情報クラス
// [CharlieDB].[dbo].[T_DEMO_USER]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/18 勝呂)
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.DB;

namespace MwsLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// [charlieDB].[dbo].[T_DEMO_USER]：デモ用ID管理テーブル
	/// </summary>
	public class T_DEMO_USER
	{
		/// <summary>
		/// AutoID
		/// </summary>
		public int NO { get; set; }

		/// <summary>
		/// 顧客ID
		/// </summary>
		public int CUSTOMER_ID { get; set; }

		/// <summary>
		/// WWコード
		/// 社員コード or 販売店コード
		/// </summary>
		public string WW_CODE { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string NAME { get; set; }

		/// <summary>
		/// メールアドレス１
		/// </summary>
		public string MAILADDR1 { get; set; }

		/// <summary>
		/// メールアドレス２
		/// </summary>
		public string MAILADDR2 { get; set; }

		/// <summary>
		/// ライセンス数
		/// </summary>
		public int CLIENT_LICENSES { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool END_FLG { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		public string REMARKS { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DELETE_FLG { get; set; }

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
		/// 顧客差分フラグ
		/// </summary>
		public bool RENEWAL_FLG { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_DEMO_USER()
		{
			NO = 0;
			CUSTOMER_ID = 0;
			WW_CODE = string.Empty;
			NAME = string.Empty;
			MAILADDR1 = string.Empty;
			MAILADDR2 = string.Empty;
			CLIENT_LICENSES = 0;
			END_FLG = false;
			REMARKS = string.Empty;
			DELETE_FLG = false;
			CREATE_DATE = null;
			CREATE_PERSON = string.Empty;
			UPDATE_DATE = null;
			UPDATE_PERSON = string.Empty;
			RENEWAL_FLG = false;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_DEMO_USER]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>デモ用ID管理テーブル</returns>
		public static List<T_DEMO_USER> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_DEMO_USER> result = new List<T_DEMO_USER>();
				foreach (DataRow row in table.Rows)
				{
					T_DEMO_USER data = new T_DEMO_USER
					{
						NO = DataBaseValue.ConvObjectToInt(row["NO"]),
						CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
						WW_CODE = row["WW_CODE"].ToString().Trim(),
						NAME = row["NAME"].ToString().Trim(),
						MAILADDR1 = row["MAILADDR1"].ToString().Trim(),
						MAILADDR2 = row["MAILADDR2"].ToString().Trim(),
						CLIENT_LICENSES = DataBaseValue.ConvObjectToInt(row["CLIENT_LICENSES"]),
						END_FLG = ("0" == row["END_FLG"].ToString()) ? false : true,
						REMARKS = row["REMARKS"].ToString().Trim(),
						DELETE_FLG = ("0" == row["DELETE_FLG"].ToString()) ? false : true,
						CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]),
						CREATE_PERSON = row["CREATE_PERSON"].ToString().Trim(),
						UPDATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]),
						UPDATE_PERSON = row["UPDATE_PERSON"].ToString().Trim(),
						RENEWAL_FLG = ("0" == row["RENEWAL_FLG"].ToString()) ? false : true,
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
