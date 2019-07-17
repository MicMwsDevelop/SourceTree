//
// tMik保守契約.cs
//
// tMik保守契約クラス
// [JunpDB].[dbo].[tMik保守契約]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using MwsLib.Common;
using MwsLib.DB;
using MwsLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.BaseFactory.Junp.Table
{
	/// <summary>
	/// [JunpDB].[dbo].[tMik保守契約]
	/// 保守契約情報
	/// <summary>
	public class tMik保守契約
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int fhsCliMicID { get; set; }

		/// <summary>
		/// ソフト保守：保守の有無
		/// </summary>
		public bool fhsS保守 { get; set; }

		/// <summary>
		/// ソフト保守：契約書回収日付
		/// </summary>
		public Date? fhsS契約書回収年月 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public bool fhsS売計上 { get; set; }

		/// <summary>
		/// ソフト保守：契約年数
		/// </summary>
		public int fhsS契約年数 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public int fhsSメンテ料金 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public YearMonth? fhsSメンテ契約開始 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public YearMonth? fhsSメンテ契約終了 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public string fhsSメンテ契約備考1 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public string fhsSメンテ契約備考2 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public string fhsS契約名義 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public string fhsSメンテ請求先コード { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public string fhsSメンテ請求先名 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public bool fhsSメンテ区分 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public string fhsS卸BM先名 { get; set; }

		/// <summary>
		/// ソフト保守：
		/// </summary>
		public int fhsS金額 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool fhsH保守 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Date? fhsH契約書回収年月 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool fhsH売計上 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int fhsH契約年数 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int fhsHメンテ料金 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public YearMonth? fhsHメンテ契約開始 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public YearMonth? fhsHメンテ契約終了 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string fhsHメンテ契約備考1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string fhsHメンテ契約備考2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string fhsH契約名義 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string fhsHメンテ請求先コード { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string fhsHメンテ請求先名 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string fhsHメンテ区分 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string fhsH卸BM先名 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int fhsH金額 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? fhs更新日 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string fhs更新者 { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15"
										+ ", @16, @17, @18, @19, @20, @21, @22, @23, @24, @25, @26, @27, @28, @29, @30, @31, @32, @33)"
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik保守契約]);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMik保守契約()
		{
			fhsCliMicID = 0;
			fhsS保守 = false;
			fhsS契約書回収年月 = null;
			fhsS売計上 = false;
			fhsS契約年数 = 0;
			fhsSメンテ料金 = 0;
			fhsSメンテ契約開始 = null;
			fhsSメンテ契約終了 = null;
			fhsSメンテ契約備考1 = null;
			fhsSメンテ契約備考2 = null;
			fhsS契約名義 = null;
			fhsSメンテ請求先コード = null;
			fhsSメンテ請求先名 = null;
			fhsSメンテ区分 = false;
			fhsS卸BM先名 = null;
			fhsS金額 = 0;
			fhsH保守 = false;
			fhsH契約書回収年月 = null;
			fhsH売計上 = false;
			fhsH契約年数 = 0;
			fhsHメンテ料金 = 0;
			fhsHメンテ契約開始 = null;
			fhsHメンテ契約終了 = null;
			fhsHメンテ契約備考1 = null;
			fhsHメンテ契約備考2 = null;
			fhsH契約名義 = null;
			fhsHメンテ請求先コード = null;
			fhsHメンテ請求先名 = null;
			fhsHメンテ区分 = null;
			fhsH卸BM先名 = null;
			fhsH金額 = 0;
			fhs更新日 = null;
			fhs更新者 = null;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", fhsCliMicID.ToString()),
				new SqlParameter("@2", fhsS保守 ? "1" : "0"),
				new SqlParameter("@3", fhsS契約書回収年月.HasValue ? fhsS契約書回収年月.ToString() : ""),
				new SqlParameter("@4", fhsS売計上 ? "1" : "0"),
				new SqlParameter("@5", fhsS契約年数.ToString()),
				new SqlParameter("@6", fhsSメンテ料金.ToString()),
				new SqlParameter("@7", fhsSメンテ契約開始.HasValue ? fhsSメンテ契約開始.Value.ToString() : ""),
				new SqlParameter("@8", fhsSメンテ契約終了.HasValue ? fhsSメンテ契約終了.Value.ToString() : ""),
				new SqlParameter("@9", fhsSメンテ契約備考1 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", fhsSメンテ契約備考2 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", fhsS契約名義 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@12", fhsSメンテ請求先コード ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@13", fhsSメンテ請求先名 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@14", fhsSメンテ区分 ? "1": "0"),
				new SqlParameter("@15", fhsS卸BM先名 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@16", fhsS金額.ToString()),
				new SqlParameter("@17", fhsH保守 ? "1" : "0"),
				new SqlParameter("@18", fhsH契約書回収年月.HasValue ? fhsH契約書回収年月.Value.ToString() : ""),
				new SqlParameter("@19", fhsH売計上 ? "1" : "0"),
				new SqlParameter("@20", fhsH契約年数.ToString()),
				new SqlParameter("@21", fhsHメンテ料金.ToString()),
				new SqlParameter("@22", fhsHメンテ契約開始.HasValue ? fhsHメンテ契約開始.Value.ToString() : ""),
				new SqlParameter("@23", fhsHメンテ契約終了.HasValue ? fhsHメンテ契約終了.Value.ToString() : ""),
				new SqlParameter("@24", fhsHメンテ契約備考1 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@25", fhsHメンテ契約備考2 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@26", fhsH契約名義 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@27", fhsHメンテ請求先コード ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@28", fhsHメンテ請求先名 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@29", fhsHメンテ区分 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@30", fhsH卸BM先名 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@31", fhsH金額.ToString()),
				new SqlParameter("@32", fhs更新日.HasValue ? fhs更新日.Value : System.Data.SqlTypes.SqlDateTime.Null),
				new SqlParameter("@33", fhs更新者 ?? System.Data.SqlTypes.SqlString.Null),
			};
			return param;
		}

		/// <summary>
		/// [charlieDB].[dbo].[tMik保守契約]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>支店情報</returns>
		public static List<tMik保守契約> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMik保守契約> result = new List<tMik保守契約>();
				foreach (DataRow row in table.Rows)
				{
					tMik保守契約 data = new tMik保守契約
					{
						fhsCliMicID = DataBaseValue.ConvObjectToInt(row["fhsCliMicID"]),
						fhsS保守 = ("1" == row["fhsS保守"].ToString()) ? true : false,
						fhsS契約書回収年月 = null,
						fhsS売計上 = ("1" == row["fhsS売計上"].ToString()) ? true : false,
						fhsS契約年数 = DataBaseValue.ConvObjectToInt(row["fhsS契約年数"]),
						fhsSメンテ料金 = DataBaseValue.ConvObjectToInt(row["fhsSメンテ料金"]),
						fhsSメンテ契約開始 = null,
						fhsSメンテ契約終了 = null,
						fhsSメンテ契約備考1 = row["fhsSメンテ契約備考1"].ToString().Trim(),
						fhsSメンテ契約備考2 = row["fhsSメンテ契約備考2"].ToString().Trim(),
						fhsS契約名義 = row["fhsS契約名義"].ToString().Trim(),
						fhsSメンテ請求先コード = row["fhsSメンテ請求先コード"].ToString().Trim(),
						fhsSメンテ請求先名 = row["fhsSメンテ請求先名"].ToString().Trim(),
						fhsSメンテ区分 = ("1" == row["fhsSメンテ区分"].ToString()) ? true : false,
						fhsS卸BM先名 = row["fhsS卸BM先名"].ToString().Trim(),
						fhsS金額 = DataBaseValue.ConvObjectToInt(row["fhsS金額"]),
						fhsH保守 = ("1" == row["fhsH保守"].ToString()) ? true : false,
						fhsH契約書回収年月 = null,
						fhsH売計上 = ("1" == row["fhsH売計上"].ToString()) ? true : false,
						fhsH契約年数 = DataBaseValue.ConvObjectToInt(row["fhsH契約年数"]),
						fhsHメンテ料金 = DataBaseValue.ConvObjectToInt(row["fhsHメンテ料金"]),
						fhsHメンテ契約開始 = null,
						fhsHメンテ契約終了 = null,
						fhsHメンテ契約備考1 = row["fhsHメンテ契約備考1"].ToString().Trim(),
						fhsHメンテ契約備考2 = row["fhsHメンテ契約備考2"].ToString().Trim(),
						fhsH契約名義 = row["fhsH契約名義"].ToString().Trim(),
						fhsHメンテ請求先コード = row["fhsHメンテ請求先コード"].ToString().Trim(),
						fhsHメンテ請求先名 = row["fhsHメンテ請求先名"].ToString().Trim(),
						fhsHメンテ区分 = row["fhsHメンテ区分"].ToString().Trim(),
						fhsH卸BM先名 = row["fhsH卸BM先名"].ToString().Trim(),
						fhsH金額 = DataBaseValue.ConvObjectToInt(row["fhsH金額"]),
						fhs更新日 = DataBaseValue.ConvObjectToDateTimeNull(row["fhs更新日"]),
						fhs更新者 = row["fhs更新者"].ToString()
					};
					if (Date.TryParse(row["fhsS契約書回収年月"].ToString().Trim(), out Date ymds))
					{
						data.fhsS契約書回収年月 = ymds;
					}
					if (YearMonth.TryParse(row["fhsSメンテ契約開始"].ToString().Trim(), out YearMonth ymss))
					{
						data.fhsSメンテ契約開始 = ymss;
					}
					if (YearMonth.TryParse(row["fhsSメンテ契約終了"].ToString().Trim(), out YearMonth ymse))
					{
						data.fhsSメンテ契約終了 = ymse;
					}
					if (Date.TryParse(row["fhsH契約書回収年月"].ToString().Trim(), out Date ymdh))
					{
						data.fhsH契約書回収年月 = ymdh;
					}
					if (YearMonth.TryParse(row["fhsHメンテ契約開始"].ToString().Trim(), out YearMonth ymhs))
					{
						data.fhsHメンテ契約開始 = ymhs;
					}
					if (YearMonth.TryParse(row["fhsHメンテ契約終了"].ToString().Trim(), out YearMonth ymhe))
					{
						data.fhsHメンテ契約終了 = ymhe;
					}
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
