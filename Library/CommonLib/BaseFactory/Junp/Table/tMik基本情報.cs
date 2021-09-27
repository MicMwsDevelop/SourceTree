//
// tMik基本情報.cs
//
// 基本情報クラス
// [JunpDB].[dbo].[tMik基本情報]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using System;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.Table
{
	/// <summary>
	/// [JunpDB].[dbo].[tMik基本情報]
	/// </summary>
	public class tMik基本情報
	{
		/// <summary>
		/// fkjCliMicID
		/// </summary>
		public int fkjCliMicID { get; set; }

		/// <summary>
		/// fkj顧客区分
		/// </summary>
		public int fkj顧客区分 { get; set; }

		/// <summary>
		/// fkj顧客名２
		/// </summary>
		public string fkj顧客名２ { get; set; }

		/// <summary>
		/// fkj郵便番号
		/// </summary>
		public string fkj郵便番号 { get; set; }

		/// <summary>
		/// fkj住所１
		/// </summary>
		public string fkj住所１ { get; set; }

		/// <summary>
		/// fkj住所２
		/// </summary>
		public string fkj住所２ { get; set; }

		/// <summary>
		/// fkj住所フリガナ
		/// </summary>
		public string fkj住所フリガナ { get; set; }

		/// <summary>
		/// fkj電話番号
		/// </summary>
		public string fkj電話番号 { get; set; }

		/// <summary>
		/// fkjファックス番号
		/// </summary>
		public string fkjファックス番号 { get; set; }

		/// <summary>
		/// fkj削除フラグ
		/// </summary>
		public bool fkj削除フラグ { get; set; }

		/// <summary>
		/// fkj得意先情報
		/// </summary>
		public string fkj得意先情報 { get; set; }

		/// <summary>
		/// fkj仕入先情報
		/// </summary>
		public string fkj仕入先情報 { get; set; }

		/// <summary>
		/// fkj状態1
		/// </summary>
		public int fkj状態1 { get; set; }

		/// <summary>
		/// fkj状態2
		/// </summary>
		public int fkj状態2 { get; set; }

		/// <summary>
		/// fkj更新日
		/// </summary>
		public DateTime? fkj更新日 { get; set; }

		/// <summary>
		/// fkj更新者
		/// </summary>
		public string fkj更新者 { get; set; }

		/// <summary>
		/// 住所から県番号を取得
		/// </summary>
		public KenNumDef.KenNumber 県番号
		{
			get
			{
				if (0 < fkj住所１.Length)
				{
					KenNumDef.KenNumber[] kens = {	KenNumDef.KenNumber.Hokkaido,
													KenNumDef.KenNumber.Aomori,
													KenNumDef.KenNumber.Iwate,
													KenNumDef.KenNumber.Miyagi,
													KenNumDef.KenNumber.Akita,
													KenNumDef.KenNumber.Yamagata,
													KenNumDef.KenNumber.Fukushima,
													KenNumDef.KenNumber.Ibaraki,
													KenNumDef.KenNumber.Tochigi,
													KenNumDef.KenNumber.Gunma,
													KenNumDef.KenNumber.Saitama,
													KenNumDef.KenNumber.Chiba,
													KenNumDef.KenNumber.Tokyo,
													KenNumDef.KenNumber.Kanagawa,
													KenNumDef.KenNumber.Niigata,
													KenNumDef.KenNumber.Toyama,
													KenNumDef.KenNumber.Ishikawa,
													KenNumDef.KenNumber.Fukui,
													KenNumDef.KenNumber.Yamanashi,
													KenNumDef.KenNumber.Nagano,
													KenNumDef.KenNumber.Gifu,
													KenNumDef.KenNumber.Shizuoka,
													KenNumDef.KenNumber.Aichi,
													KenNumDef.KenNumber.Mie,
													KenNumDef.KenNumber.Shiga,
													KenNumDef.KenNumber.Kyoto,
													KenNumDef.KenNumber.Osaka,
													KenNumDef.KenNumber.Hyogo,
													KenNumDef.KenNumber.Nara,
													KenNumDef.KenNumber.Wakayama,
													KenNumDef.KenNumber.Tottori,
													KenNumDef.KenNumber.Shimane,
													KenNumDef.KenNumber.Okayama,
													KenNumDef.KenNumber.Hiroshima,
													KenNumDef.KenNumber.Yamaguchi,
													KenNumDef.KenNumber.Tokushima,
													KenNumDef.KenNumber.Kagawa,
													KenNumDef.KenNumber.Ehime,
													KenNumDef.KenNumber.Kochi,
													KenNumDef.KenNumber.Fukuoka,
													KenNumDef.KenNumber.Saga,
													KenNumDef.KenNumber.Nagasaki,
													KenNumDef.KenNumber.Kumamoto,
													KenNumDef.KenNumber.Oita,
													KenNumDef.KenNumber.Miyazaki,
													KenNumDef.KenNumber.Kagoshima,
													KenNumDef.KenNumber.Okinawa };
					for (int i = 0; i < kens.Length; i++)
					{
						if (-1 != fkj住所１.IndexOf(KenNumDef.KenString[kens[i]][0]))
						{
							return kens[i];
						}
					}
				}
				return KenNumDef.KenNumber.None;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMik基本情報()
		{
			fkjCliMicID = 0;
			fkj顧客区分 = 0;
			fkj顧客名２ = string.Empty;
			fkj郵便番号 = string.Empty;
			fkj住所１ = string.Empty;
			fkj住所２ = string.Empty;
			fkj住所フリガナ = string.Empty;
			fkj電話番号 = string.Empty;
			fkjファックス番号 = string.Empty;
			fkj削除フラグ = false;
			fkj得意先情報 = string.Empty;
			fkj仕入先情報 = string.Empty;
			fkj状態1 = 0;
			fkj状態2 = 0;
			fkj更新日 = null;
			fkj更新者 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tMik基本情報> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMik基本情報> result = new List<tMik基本情報>();
				foreach (DataRow row in table.Rows)
				{
					tMik基本情報 data = new tMik基本情報
					{
						fkjCliMicID = DataBaseValue.ConvObjectToInt(row["fkjCliMicID"]),
						fkj顧客区分 = DataBaseValue.ConvObjectToInt(row["fkj顧客区分"]),
						fkj顧客名２ = row["fkj顧客名２"].ToString().Trim(),
						fkj郵便番号 = row["fkj郵便番号"].ToString().Trim(),
						fkj住所１ = row["fkj住所１"].ToString().Trim(),
						fkj住所２ = row["fkj住所２"].ToString().Trim(),
						fkj住所フリガナ = row["fkj住所フリガナ"].ToString().Trim(),
						fkj電話番号 = row["fkj電話番号"].ToString().Trim(),
						fkjファックス番号 = row["fkjファックス番号"].ToString().Trim(),
						fkj削除フラグ = (row["fkj削除フラグ"].ToString().Trim() == "0") ? false : true,
						fkj得意先情報 = row["fkj得意先情報"].ToString().Trim(),
						fkj仕入先情報 = row["fkj仕入先情報"].ToString().Trim(),
						fkj状態1 = DataBaseValue.ConvObjectToInt(row["fkj状態1"]),
						fkj状態2 = DataBaseValue.ConvObjectToInt(row["fkj状態2"]),
						fkj更新日 = DataBaseValue.ConvObjectToDateTimeNull(row["fkj更新日"]),
						fkj更新者 = row["fkj更新者"].ToString().Trim()
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
