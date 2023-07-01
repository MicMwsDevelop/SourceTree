//
// T_FILE_CREATEDATE.cs
//
// [CharlieDB].[dbo].[T_FILE_CREATEDATE]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// 同期日時管理テーブル
	/// </summary>
	public class T_FILE_CREATEDATE
	{
		/// <summary>
		/// 同期日時
		/// </summary>
		public DateTime FILE_CREATEDATE { get; set; }

		/// <summary>
		/// 同期種別（1:顧客情報、2:サービス利用情報）
		/// </summary>
		public string FILE_TYPE { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? CREATE_DATE { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CREATE_PERSON { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4)", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_FILE_CREATEDATE]);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_FILE_CREATEDATE()
		{
			FILE_CREATEDATE = DateTime.Now;
			FILE_TYPE = string.Empty;
			CREATE_DATE = null;
			CREATE_PERSON = string.Empty;
		}

		/// <summary>
		/// DataTable → クラス
		/// </summary>
		/// <param name="table"></param>
		/// <returns>MWSコードマスタ</returns>
		public static T_FILE_CREATEDATE DataTableToData(DataTable table)
		{
			if (null != table && 1 == table.Rows.Count)
			{
				DataRow row = table.Rows[0];
				T_FILE_CREATEDATE result = new T_FILE_CREATEDATE();
				result.FILE_CREATEDATE = DataBaseValue.ConvObjectToDateTime(row["FILE_CREATEDATE"]);
				result.FILE_TYPE = row["FILE_TYPE"].ToString().Trim();
				result.CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]);
				result.CREATE_PERSON = row["CREATE_PERSON"].ToString().Trim();
				return result;
			}
			return null;
		}
	}
}
