//
// 進捗管理表_作業情報.cs
//
// 進捗管理表_作業情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2022/03/08 勝呂)
// 
using CommonLib.DB;
using CommonLib.DB.SqlServer.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Sales.Table
{
	public class 進捗管理表_作業情報
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int 顧客No { get; set; }

		/// <summary>
		/// 工事確定日
		/// </summary>
		public DateTime? 工事確定日 { get; set; }

		/// <summary>
		/// 進捗管理表ファイル名
		/// </summary>
		public string 進捗管理表ファイル名 { get; set; }

		/// <summary>
		/// 更新日
		/// </summary>
		public DateTime? 更新日 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 進捗管理表_作業情報()
		{
			顧客No = 0;
			工事確定日 = null;
			進捗管理表ファイル名 = string.Empty;
			更新日 = null;
		}

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4)", SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.進捗管理表_作業情報]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET 工事確定日 = @1, 進捗管理表ファイル名 = @2, 更新日 = @3 WHERE 顧客No = {1}", SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.進捗管理表_作業情報], 顧客No);
			}
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<進捗管理表_作業情報> DataTableToList(DataTable table)
		{
			List<進捗管理表_作業情報> result = new List<進捗管理表_作業情報>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					進捗管理表_作業情報 data = new 進捗管理表_作業情報();
					data.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.工事確定日 = DataBaseValue.ConvObjectToDateTimeNull(row["工事確定日"]);
					data.進捗管理表ファイル名 = row["進捗管理表ファイル名"].ToString().Trim();
					data.更新日 = DataBaseValue.ConvObjectToDateTimeNull(row["更新日"]);
					result.Add(data);
				}
			}
			return result;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", 顧客No.ToString()),
				new SqlParameter("@2", 工事確定日.HasValue ? 工事確定日.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", 進捗管理表ファイル名),
				new SqlParameter("@4", DateTime.Now.ToString())
			};
			return param;
		}

		/// <summary>
		/// UPDATE SETパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetUpdateSetParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", 工事確定日.HasValue ? 工事確定日.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@2", 進捗管理表ファイル名),
				new SqlParameter("@3", DateTime.Now.ToString())
			};
			return param;
		}
	}
}
