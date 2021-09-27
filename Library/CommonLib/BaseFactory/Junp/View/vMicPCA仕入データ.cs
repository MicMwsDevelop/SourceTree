//
// vMicPCA仕入データ.cs
//
// PCA仕入データ情報クラス
// [JunpDB].[dbo].[vMicPCA仕入データ]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.View
{
	public class vMicPCA仕入データ
	{
		public short nykd_flid { get; set; }
		public string nykd_denku { get; set; }
		public string nykd_tcd { get; set; }
		public int nykd_uribi { get; set; }
		public int nykd_seibi { get; set; }
		public int nykd_denno { get; set; }
		public short nykd_hoho { get; set; }
		public int nykd_tehai { get; set; }
		public string nykd_ocd { get; set; }
		public string nykd_jbmn { get; set; }
		public string nykd_jtan { get; set; }
		public string nykd_tekcd { get; set; }
		public string nykd_tekmei { get; set; }
		public short nykd_eda { get; set; }
		public string nykd_scd { get; set; }
		public short nykd_mkbn { get; set; }
		public short nykd_tax { get; set; }
		public short nykd_komi { get; set; }
		public short nykd_tketa { get; set; }
		public short nykd_sketa { get; set; }
		public string nykd_mei { get; set; }
		public short nykd_ku { get; set; }
		public decimal nykd_iri { get; set; }
		public decimal nykd_hako { get; set; }
		public decimal nykd_suryo { get; set; }
		public string nykd_tani { get; set; }
		public decimal nykd_tanka { get; set; }
		public decimal nykd_kingaku { get; set; }
		public string nykd_biko { get; set; }
		public decimal nykd_hyo { get; set; }
		public int nykd_seq { get; set; }
		public short nykd_nkbn { get; set; }
		public string nykd_souko { get; set; }
		public decimal nykd_zei { get; set; }
		public decimal nykd_uchi { get; set; }
		public int nykd_hid { get; set; }
		public decimal nykd_rate { get; set; }

		public vMicPCA仕入データ()
		{
			nykd_flid = 0;
			nykd_denku = string.Empty;
			nykd_tcd = string.Empty;
			nykd_uribi = 0;
			nykd_seibi = 0;
			nykd_denno = 0;
			nykd_hoho = 0;
			nykd_tehai = 0;
			nykd_ocd = string.Empty;
			nykd_jbmn = string.Empty;
			nykd_jtan = string.Empty;
			nykd_tekcd = string.Empty;
			nykd_tekmei = string.Empty;
			nykd_eda = 0;
			nykd_scd = string.Empty;
			nykd_mkbn = 0;
			nykd_tax = 0;
			nykd_komi = 0;
			nykd_tketa = 0;
			nykd_sketa = 0;
			nykd_mei = string.Empty;
			nykd_ku = 0;
			nykd_iri = 0;
			nykd_hako = 0;
			nykd_suryo = 0;
			nykd_tani = string.Empty;
			nykd_tanka = 0;
			nykd_kingaku = 0;
			nykd_biko = string.Empty;
			nykd_hyo = 0;
			nykd_seq = 0;
			nykd_nkbn = 0;
			nykd_souko = string.Empty;
			nykd_zei = 0;
			nykd_uchi = 0;
			nykd_hid = 0;
			nykd_rate = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMicPCA仕入データ> DataTableToList(DataTable table)
		{
			List<vMicPCA仕入データ> result = new List<vMicPCA仕入データ>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					vMicPCA仕入データ data = new vMicPCA仕入データ
					{
						nykd_flid = DataBaseValue.ConvObjectToShort(row["nykd_flid"]),
						nykd_denku = row["nykd_denku"].ToString().Trim(),
						nykd_tcd = row["nykd_tcd"].ToString().Trim(),
						nykd_uribi = DataBaseValue.ConvObjectToInt(row["nykd_uribi"]),
						nykd_seibi = DataBaseValue.ConvObjectToInt(row["nykd_seibi"]),
						nykd_denno = DataBaseValue.ConvObjectToInt(row["nykd_denno"]),
						nykd_hoho = DataBaseValue.ConvObjectToShort(row["nykd_hoho"]),
						nykd_tehai = DataBaseValue.ConvObjectToInt(row["nykd_tehai"]),
						nykd_ocd = row["nykd_ocd"].ToString().Trim(),
						nykd_jbmn = row["nykd_jbmn"].ToString().Trim(),
						nykd_jtan = row["nykd_jtan"].ToString().Trim(),
						nykd_tekcd = row["nykd_tekcd"].ToString().Trim(),
						nykd_tekmei = row["nykd_tekmei"].ToString().Trim(),
						nykd_eda = DataBaseValue.ConvObjectToShort(row["nykd_eda"]),
						nykd_scd = row["nykd_scd"].ToString().Trim(),
						nykd_mkbn = DataBaseValue.ConvObjectToShort(row["nykd_mkbn"]),
						nykd_tax = DataBaseValue.ConvObjectToShort(row["nykd_tax"]),
						nykd_komi = DataBaseValue.ConvObjectToShort(row["nykd_komi"]),
						nykd_tketa = DataBaseValue.ConvObjectToShort(row["nykd_tketa"]),
						nykd_sketa = DataBaseValue.ConvObjectToShort(row["nykd_sketa"]),
						nykd_mei = row["nykd_mei"].ToString().Trim(),
						nykd_ku = DataBaseValue.ConvObjectToShort(row["nykd_ku"]),
						nykd_iri = DataBaseValue.ConvObjectToDecimal(row["nykd_iri"]),
						nykd_hako = DataBaseValue.ConvObjectToDecimal(row["nykd_hako"]),
						nykd_suryo = DataBaseValue.ConvObjectToDecimal(row["nykd_suryo"]),
						nykd_tani = row["nykd_tani"].ToString().Trim(),
						nykd_tanka = DataBaseValue.ConvObjectToDecimal(row["nykd_tanka"]),
						nykd_kingaku = DataBaseValue.ConvObjectToDecimal(row["nykd_kingaku"]),
						nykd_biko = row["nykd_biko"].ToString().Trim(),
						nykd_hyo = DataBaseValue.ConvObjectToDecimal(row["nykd_hyo"]),
						nykd_seq = DataBaseValue.ConvObjectToInt(row["nykd_seq"]),
						nykd_nkbn = DataBaseValue.ConvObjectToShort(row["nykd_nkbn"]),
						nykd_souko = row["nykd_souko"].ToString().Trim(),
						nykd_zei = DataBaseValue.ConvObjectToDecimal(row["nykd_zei"]),
						nykd_uchi = DataBaseValue.ConvObjectToDecimal(row["nykd_uchi"]),
						nykd_hid = DataBaseValue.ConvObjectToInt(row["nykd_hid"]),
						nykd_rate = DataBaseValue.ConvObjectToDecimal(row["nykd_rate"]),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
