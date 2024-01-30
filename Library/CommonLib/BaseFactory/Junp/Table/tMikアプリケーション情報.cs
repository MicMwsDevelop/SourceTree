//
// tMikアプリケーション情報.cs
//
// 基本情報クラス
// [JunpDB].[dbo].[tMikアプリケーション情報]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2021/01/14 勝呂)
//
using System;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;
using CommonLib.DB.SqlServer.Junp;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Junp.Table
{
	public class tMikアプリケーション情報
	{
		public int faiCliMicID { get; set; }
		public int faiアプリケーションNo { get; set; }
		public string faiアプリケーション名 { get; set; }
		public string faiLicensedKey { get; set; }
		public string faiVersion情報 { get; set; }
		public string faiオプション1 { get; set; }
		public string faiオプション2 { get; set; }
		public string faiオプション3 { get; set; }
		public string fai登録ｶｰﾄﾞ回収日 { get; set; }
		public bool fai保守 { get; set; }
		public string fai契約書回収年月 { get; set; }
		public int? fai保守料金 { get; set; }
		public string fai保守契約開始 { get; set; }
		public string fai保守契約終了 { get; set; }
		public string fai保守契約備考 { get; set; }
		public bool fai終了フラグ { get; set; }
		public DateTime? fai更新日 { get; set; }
		public string fai更新者 { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18)", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報]);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMikアプリケーション情報()
		{
			faiCliMicID = 0;
			faiアプリケーションNo = 0;
			faiアプリケーション名 = string.Empty;
			faiLicensedKey = string.Empty;
			faiVersion情報 = string.Empty;
			faiオプション1 = string.Empty;
			faiオプション2 = string.Empty;
			faiオプション3 = string.Empty;
			fai登録ｶｰﾄﾞ回収日 = string.Empty;
			fai保守 = false;
			fai契約書回収年月 = string.Empty;
			fai保守料金 = null;
			fai保守契約開始 = string.Empty;
			fai保守契約終了 = string.Empty;
			fai保守契約備考 = string.Empty;
			fai終了フラグ = false;
			fai更新日 = null;
			fai更新者 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tMikアプリケーション情報> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMikアプリケーション情報> result = new List<tMikアプリケーション情報>();
				foreach (DataRow row in table.Rows)
				{
					tMikアプリケーション情報 data = new tMikアプリケーション情報
					{
						faiCliMicID = DataBaseValue.ConvObjectToInt(row["faiCliMicID"]),
						faiアプリケーションNo = DataBaseValue.ConvObjectToInt(row["faiアプリケーションNo"]),
						faiアプリケーション名 = row["faiアプリケーション名"].ToString().Trim(),
						faiLicensedKey = row["faiLicensedKey"].ToString().Trim(),
						faiVersion情報 = row["faiVersion情報"].ToString().Trim(),
						faiオプション1 = row["faiオプション1"].ToString().Trim(),
						faiオプション2 = row["faiオプション2"].ToString().Trim(),
						faiオプション3 = row["faiオプション3"].ToString().Trim(),
						fai登録ｶｰﾄﾞ回収日 = row["fai登録ｶｰﾄﾞ回収日"].ToString().Trim(),
						fai保守 = (row["fai保守"].ToString().Trim() == "0") ? false : true,
						fai契約書回収年月 = row["fai契約書回収年月"].ToString().Trim(),
						fai保守料金 = DataBaseValue.ConvObjectToIntNull(row["fai保守料金"]),
						fai保守契約開始 = row["fai保守契約開始"].ToString().Trim(),
						fai保守契約終了 = row["fai保守契約終了"].ToString().Trim(),
						fai保守契約備考 = row["fai保守契約備考"].ToString().Trim(),
						fai終了フラグ = (row["fai終了フラグ"].ToString().Trim() == "0") ? false : true,
						fai更新日 = DataBaseValue.ConvObjectToDateTimeNull(row["fai更新日"]),
						fai更新者 = row["fai更新者"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", faiCliMicID),
				new SqlParameter("@2", faiアプリケーションNo),
				new SqlParameter("@3", faiアプリケーション名 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", faiLicensedKey ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", faiVersion情報 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@6", faiオプション1 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", faiオプション2 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", faiオプション3 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", fai登録ｶｰﾄﾞ回収日 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", (fai保守) ? '1' : '0'),
				new SqlParameter("@11", fai契約書回収年月 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@12", (fai保守料金.HasValue) ? fai保守料金.Value : System.Data.SqlTypes.SqlInt32.Null),
				new SqlParameter("@13", fai保守契約開始 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@14", fai保守契約終了 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@15", fai保守契約備考 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@16", (fai終了フラグ) ? '1' : '0'),
				new SqlParameter("@17", DateTime.Now),
				new SqlParameter("@18", fai更新者 ?? System.Data.SqlTypes.SqlString.Null)
			};
			return param;
		}
	}
}
