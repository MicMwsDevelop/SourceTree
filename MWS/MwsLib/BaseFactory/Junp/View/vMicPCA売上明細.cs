//
// vMicPCA売上明細.cs
//
// PCA売上明細情報クラス
// [JunpDB].[dbo].[vMicPCA売上明細]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/05/29 勝呂)
//
using MwsLib.DB;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.Junp.View
{
	public class vMicPCA売上明細
	{
		public string sykd_denku { get; set; }
		public string sykd_tcd { get; set; }
		public int sykd_uribi { get; set; }
		public int sykd_seibi { get; set; }
		public int sykd_denno { get; set; }
		public string sykd_ocd { get; set; }
		public string sykd_jbmn { get; set; }
		public string sykd_jtan { get; set; }
		public string sykd_tekcd { get; set; }
		public string sykd_tekmei { get; set; }
		public short sykd_eda { get; set; }
		public string sykd_scd { get; set; }
		public short sykd_mkbn { get; set; }
		public short sykd_tax { get; set; }
		public short sykd_komi { get; set; }
		public short sykd_tketa { get; set; }
		public short sykd_sketa { get; set; }
		public string sykd_mei { get; set; }
		public short sykd_ku { get; set; }
		public decimal sykd_iri { get; set; }
		public decimal sykd_hako { get; set; }
		public decimal sykd_suryo { get; set; }
		public string sykd_tani { get; set; }
		public decimal sykd_tanka { get; set; }
		public decimal sykd_gentan { get; set; }
		public decimal sykd_kingaku { get; set; }
		public decimal sykd_genka { get; set; }
		public string sykd_biko { get; set; }
		public short sykd_nmark { get; set; }
		public decimal sykd_hyo { get; set; }
		public int sykd_hid { get; set; }
		public decimal sykd_zei { get; set; }
		public decimal sykd_rate { get; set; }
		public string sykd_souko { get; set; }
		
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vMicPCA売上明細()
		{
			sykd_denku = string.Empty;
			sykd_tcd = string.Empty;
			sykd_uribi = 0;
			sykd_seibi = 0;
			sykd_denno = 0;
			sykd_ocd = string.Empty;
			sykd_jbmn = string.Empty;
			sykd_jtan = string.Empty;
			sykd_tekcd = string.Empty;
			sykd_tekmei = string.Empty;
			sykd_eda = 0;
			sykd_scd = string.Empty;
			sykd_mkbn = 0;
			sykd_tax = 0;
			sykd_komi = 0;
			sykd_tketa = 0;
			sykd_sketa = 0;
			sykd_mei = string.Empty;
			sykd_ku = 0;
			sykd_iri = 0;
			sykd_hako = 0;
			sykd_suryo = 0;
			sykd_tani = string.Empty;
			sykd_tanka = 0;
			sykd_gentan = 0;
			sykd_kingaku = 0;
			sykd_genka = 0;
			sykd_biko = string.Empty;
			sykd_nmark = 0;
			sykd_hyo = 0;
			sykd_hid = 0;
			sykd_zei = 0;
			sykd_rate = 0;
			sykd_souko = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMicPCA売上明細> DataTableToList(DataTable table)
		{
			List<vMicPCA売上明細> result = new List<vMicPCA売上明細>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					vMicPCA売上明細 data = new vMicPCA売上明細
					{
						sykd_denku = row["sykd_denku"].ToString().Trim(),
						sykd_tcd = row["sykd_tcd"].ToString().Trim(),
						sykd_uribi = DataBaseValue.ConvObjectToInt(row["sykd_uribi"]),
						sykd_seibi = DataBaseValue.ConvObjectToInt(row["sykd_seibi"]),
						sykd_denno = DataBaseValue.ConvObjectToInt(row["sykd_denno"]),
						sykd_ocd = row["sykd_ocd"].ToString().Trim(),
						sykd_jbmn = row["sykd_jbmn"].ToString().Trim(),
						sykd_jtan = row["sykd_jtan"].ToString().Trim(),
						sykd_tekcd = row["sykd_tekcd"].ToString().Trim(),
						sykd_tekmei = row["sykd_tekmei"].ToString().Trim(),
						sykd_eda = DataBaseValue.ConvObjectToShort(row["sykd_eda"]),
						sykd_scd = row["sykd_scd"].ToString().Trim(),
						sykd_mkbn = DataBaseValue.ConvObjectToShort(row["sykd_mkbn"]),
						sykd_tax = DataBaseValue.ConvObjectToShort(row["sykd_tax"]),
						sykd_komi = DataBaseValue.ConvObjectToShort(row["sykd_komi"]),
						sykd_tketa = DataBaseValue.ConvObjectToShort(row["sykd_tketa"]),
						sykd_sketa = DataBaseValue.ConvObjectToShort(row["sykd_sketa"]),
						sykd_mei = row["sykd_mei"].ToString().Trim(),
						sykd_ku = DataBaseValue.ConvObjectToShort(row["sykd_ku"]),
						sykd_iri = DataBaseValue.ConvObjectToDecimal(row["sykd_iri"]),
						sykd_hako = DataBaseValue.ConvObjectToDecimal(row["sykd_hako"]),
						sykd_suryo = DataBaseValue.ConvObjectToDecimal(row["sykd_suryo"]),
						sykd_tani = row["sykd_tani"].ToString().Trim(),
						sykd_tanka = DataBaseValue.ConvObjectToDecimal(row["sykd_tanka"]),
						sykd_gentan = DataBaseValue.ConvObjectToDecimal(row["sykd_gentan"]),
						sykd_kingaku = DataBaseValue.ConvObjectToDecimal(row["sykd_kingaku"]),
						sykd_genka = DataBaseValue.ConvObjectToDecimal(row["sykd_genka"]),
						sykd_biko = row["sykd_biko"].ToString().Trim(),
						sykd_nmark = DataBaseValue.ConvObjectToShort(row["sykd_nmark"]),
						sykd_hyo = DataBaseValue.ConvObjectToDecimal(row["sykd_hyo"]),
						sykd_hid = DataBaseValue.ConvObjectToInt(row["sykd_hid"]),
						sykd_zei = DataBaseValue.ConvObjectToDecimal(row["sykd_zei"]),
						sykd_rate = DataBaseValue.ConvObjectToDecimal(row["sykd_rate"]),
						sykd_souko = row["sykd_souko"].ToString().Trim(),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}

