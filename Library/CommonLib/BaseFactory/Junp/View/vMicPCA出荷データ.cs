//
// vMicPCA出荷データ.cs
//
// PCA出荷データ情報クラス
// [JunpDB].[dbo].[vMicPCA出荷データ]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/05/26 勝呂)
//
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.View
{
	public class vMicPCA出荷データ
	{
		public short urid_dkbn { get; set; }
		public string urid_tcd { get; set; }
		public string urid_mei1 { get; set; }
		public int urid_uribi { get; set; }
		public int urid_denno { get; set; }
		public string urid_jbmn { get; set; }
		public string urid_jtan { get; set; }
		public short urid_eda { get; set; }
		public string urid_scd { get; set; }
		public short urid_mkbn { get; set; }
		public short urid_tax { get; set; }
		public short urid_komi { get; set; }
		public short urid_tketa { get; set; }
		public short urid_sketa { get; set; }
		public string urid_mei { get; set; }
		public decimal urid_iri { get; set; }
		public decimal urid_hako { get; set; }
		public decimal urid_suryo { get; set; }
		public string urid_tani { get; set; }
		public decimal urid_tanka { get; set; }
		public decimal urid_kingaku { get; set; }
		public string urid_biko { get; set; }
		public string urid_souko { get; set; }
		public int urid_hid { get; set; }
		public string URID_tanmei { get; set; }
		public decimal urid_rate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vMicPCA出荷データ()
		{
			urid_dkbn = 0;
			urid_tcd = string.Empty;
			urid_mei1 = string.Empty;
			urid_uribi = 0;
			urid_denno = 0;
			urid_jbmn = string.Empty;
			urid_jtan = string.Empty;
			urid_eda = 0;
			urid_scd = string.Empty;
			urid_mkbn = 0;
			urid_tax = 0;
			urid_komi = 0;
			urid_tketa = 0;
			urid_sketa = 0;
			urid_mei = string.Empty;
			urid_iri = 0;
			urid_hako = 0;
			urid_suryo = 0;
			urid_tani = string.Empty;
			urid_tanka = 0;
			urid_kingaku = 0;
			urid_biko = string.Empty;
			urid_souko = string.Empty;
			urid_hid = 0;
			URID_tanmei = string.Empty;
			urid_rate = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMicPCA出荷データ> DataTableToList(DataTable table)
		{
			List<vMicPCA出荷データ> result = new List<vMicPCA出荷データ>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					vMicPCA出荷データ data = new vMicPCA出荷データ
					{
						urid_dkbn = DataBaseValue.ConvObjectToShort(row["urid_dkbn"]),
						urid_tcd = row["urid_tcd"].ToString().Trim(),
						urid_mei1 = row["urid_mei1"].ToString().Trim(),
						urid_uribi = DataBaseValue.ConvObjectToInt(row["urid_uribi"]),
						urid_denno = DataBaseValue.ConvObjectToInt(row["urid_denno"]),
						urid_jbmn = row["urid_jbmn"].ToString().Trim(),
						urid_jtan = row["urid_jtan"].ToString().Trim(),
						urid_eda = DataBaseValue.ConvObjectToShort(row["urid_eda"]),
						urid_scd = row["urid_scd"].ToString().Trim(),
						urid_mkbn = DataBaseValue.ConvObjectToShort(row["urid_mkbn"]),
						urid_tax = DataBaseValue.ConvObjectToShort(row["urid_tax"]),
						urid_komi = DataBaseValue.ConvObjectToShort(row["urid_komi"]),
						urid_tketa = DataBaseValue.ConvObjectToShort(row["urid_tketa"]),
						urid_sketa = DataBaseValue.ConvObjectToShort(row["urid_sketa"]),
						urid_mei = row["urid_mei"].ToString().Trim(),
						urid_iri = DataBaseValue.ConvObjectToDecimal(row["urid_iri"]),
						urid_hako = DataBaseValue.ConvObjectToDecimal(row["urid_hako"]),
						urid_suryo = DataBaseValue.ConvObjectToDecimal(row["urid_suryo"]),
						urid_tani = row["urid_tani"].ToString().Trim(),
						urid_tanka = DataBaseValue.ConvObjectToDecimal(row["urid_tanka"]),
						urid_kingaku = DataBaseValue.ConvObjectToDecimal(row["urid_kingaku"]),
						urid_biko = row["urid_biko"].ToString().Trim(),
						urid_souko = row["urid_souko"].ToString().Trim(),
						urid_hid = DataBaseValue.ConvObjectToInt(row["urid_hid"]),
						URID_tanmei = row["URID_tanmei"].ToString().Trim(),
						urid_rate = DataBaseValue.ConvObjectToDecimal(row["urid_rate"]),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
