//
// 進捗管理表_作業情報.cs
//
// 進捗管理表_作業情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.11 NTT現調プランに対応(2022/08/31 勝呂)
// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
// 
using CommonLib.DB;
using CommonLib.DB.SqlServer.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Sales.Table
{
	/// <summary>
	/// 進捗管理表_作業情報
	/// </summary>
	public class 進捗管理表_作業情報
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int 顧客No { get; set; }

		/// <summary>
		/// 受付通番
		/// </summary>
		/// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
		public string 受付通番 { get; set; }

		/// <summary>
		/// 進捗管理表ファイル名
		/// </summary>
		public string 進捗管理表ファイル名 { get; set; }

		/// <summary>
		/// 現地調査確定日
		/// </summary>
		public DateTime? 現地調査確定日 { get; set; }

		/// <summary>
		/// 現地調査確定日格納日時
		/// </summary>
		public DateTime? 現地調査確定日格納日時 { get; set; }

		/// <summary>
		/// 現地調査結果
		/// </summary>
		public string 現地調査結果 { get; set; }

		/// <summary>
		/// 現地調査結果格納日時
		/// </summary>
		public DateTime? 現地調査結果格納日時 { get; set; }

		/// <summary>
		/// 工事確定日
		/// </summary>
		public DateTime? 工事確定日 { get; set; }

		/// <summary>
		/// 工事確定日格納日時
		/// </summary>
		public DateTime? 工事確定日格納日時 { get; set; }

		/// <summary>
		/// 工事結果
		/// </summary>
		/// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
		public string 工事結果 { get; set; }

		/// <summary>
		/// 工事結果格納日時
		/// </summary>
		/// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
		public DateTime? 工事結果格納日時 { get; set; }

		/// <summary>
		/// 現調が未設定かどうか？
		/// </summary>
		public bool Is現調未設定
		{
			get
			{
				if (現地調査確定日 is null && 現地調査確定日格納日時 is null && 0 == 現地調査結果.Length && 現地調査結果格納日時 is null)
				{
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// 工事が未設定かどうか？
		/// </summary>
		public bool Is工事未設定
		{
			get
			{
				if (工事確定日 is null && 工事確定日格納日時 is null)
				{
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 進捗管理表_作業情報()
		{
			顧客No = 0;
			受付通番 = string.Empty;
			進捗管理表ファイル名 = string.Empty;
			現地調査確定日 = null;
			現地調査確定日格納日時 = null;
			現地調査結果 = string.Empty;
			現地調査結果格納日時 = null;
			工事確定日 = null;
			工事確定日格納日時 = null;
			工事結果 = string.Empty;
			工事結果格納日時 = null;
		}

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)", SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.進捗管理表_作業情報]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET 受付通番 = @1, 進捗管理表ファイル名 = @2, 現地調査確定日 = @3, 現地調査確定日格納日時 = @4, 現地調査結果 = @5, 現地調査結果格納日時 = @6, 工事確定日 = @7, 工事確定日格納日時 = @8, 工事結果 = @9, 工事結果格納日時 = @10 WHERE 顧客No = {1}", SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.進捗管理表_作業情報], 顧客No);
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
					data.受付通番 = row["受付通番"].ToString().Trim();
					data.進捗管理表ファイル名 = row["進捗管理表ファイル名"].ToString().Trim();
					data.現地調査確定日 = DataBaseValue.ConvObjectToDateTimeNull(row["現地調査確定日"]);
					data.現地調査確定日格納日時 = DataBaseValue.ConvObjectToDateTimeNull(row["現地調査確定日格納日時"]);
					data.現地調査結果 = row["現地調査結果"].ToString().Trim();
					data.現地調査結果格納日時 = DataBaseValue.ConvObjectToDateTimeNull(row["現地調査結果格納日時"]);
					data.工事確定日 = DataBaseValue.ConvObjectToDateTimeNull(row["工事確定日"]);
					data.工事確定日格納日時 = DataBaseValue.ConvObjectToDateTimeNull(row["工事確定日格納日時"]);
					data.工事結果 = row["工事結果"].ToString().Trim();
					data.工事結果格納日時 = DataBaseValue.ConvObjectToDateTimeNull(row["工事結果格納日時"]);
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
				new SqlParameter("@2", 受付通番),
				new SqlParameter("@3", 進捗管理表ファイル名),
				new SqlParameter("@4", 現地調査確定日.HasValue ? 現地調査確定日.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", 現地調査確定日格納日時.HasValue ? 現地調査確定日格納日時.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@6", 現地調査結果),
				new SqlParameter("@7", 現地調査結果格納日時.HasValue ? 現地調査結果格納日時.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", 工事確定日.HasValue ? 工事確定日.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", 工事確定日格納日時.HasValue ? 工事確定日格納日時.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", 工事結果),
				new SqlParameter("@11", 工事結果格納日時.HasValue ? 工事結果格納日時.Value.ToString() : System.Data.SqlTypes.SqlString.Null)
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
				new SqlParameter("@1", 受付通番),
				new SqlParameter("@2", 進捗管理表ファイル名),
				new SqlParameter("@3", 現地調査確定日.HasValue ? 現地調査確定日.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", 現地調査確定日格納日時.HasValue ? 現地調査確定日格納日時.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", 現地調査結果),
				new SqlParameter("@6", 現地調査結果格納日時.HasValue ? 現地調査結果格納日時.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", 工事確定日.HasValue ? 工事確定日.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", 工事確定日格納日時.HasValue ? 工事確定日格納日時.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", 工事結果),
				new SqlParameter("@10", 工事結果格納日時.HasValue ? 工事結果格納日時.Value.ToString() : System.Data.SqlTypes.SqlString.Null)
			};
			return param;
		}

		/// <summary>
		/// 進捗管理表_作業情報DBの更新
		/// </summary>
		/// <param name="data">進捗管理表_作業情報</param>
		/// <param name="modify">更新</param>
		/// <param name="connectStr">SQL接続文字列</param>
		public static void WriteProgressDatabase(進捗管理表_作業情報 data, bool modify, string connectStr)
		{
#if DEBUG == false
			try
			{
				if (modify)
				{
					SalesDatabaseAccess.UpdateSet_進捗管理表_作業情報(data, connectStr);
				}
				else
				{
					SalesDatabaseAccess.InsertInto_進捗管理表_作業情報(data, connectStr);
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("{0}(顧客No:{1})", ex.Message, data.顧客No));
			}
#endif
		}
	}
}
