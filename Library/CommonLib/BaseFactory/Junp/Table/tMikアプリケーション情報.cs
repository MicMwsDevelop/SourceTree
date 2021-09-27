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
		public int fai保守料金 { get; set; }
		public string fai保守契約開始 { get; set; }
		public string fai保守契約終了 { get; set; }
		public string fai保守契約備考 { get; set; }
		public bool fai終了フラグ { get; set; }
		public DateTime? fai更新日 { get; set; }
		public string fai更新者 { get; set; }

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
			fai保守料金 = 0;
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
						fai保守料金 = DataBaseValue.ConvObjectToInt(row["fai保守料金"]),
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
	}
}
