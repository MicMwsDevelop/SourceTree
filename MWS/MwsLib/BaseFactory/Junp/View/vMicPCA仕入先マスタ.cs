//
// vMicPCA仕入先マスタ.cs
//
// PCA仕入先マスタ情報クラス
// [JunpDB].[dbo].[vMicPCA仕入先マスタ]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/05/27 勝呂)
//
using MwsLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.Junp.View
{
	public class vMicPCA仕入先マスタ
	{
		public string rms_tcd { get; set; }
		public string rms_mei1 { get; set; }
		public string rms_mei2 { get; set; }
		public string rms_tanmei { get; set; }
		public string rms_mail { get; set; }
		public string rms_ad1 { get; set; }
		public string rms_ad2 { get; set; }
		public string rms_tel { get; set; }
		public string rms_fax { get; set; }
		public short rms_jsk { get; set; }
		public string rms_keisyo { get; set; }
		public int rms_tkbn1 { get; set; }
		public int rms_tkbn2 { get; set; }
		public int rms_tkbn3 { get; set; }
		public string rms_ttan { get; set; }
		public string rms_ocd { get; set; }
		public short rms_smb { get; set; }
		public short rms_hasu { get; set; }
		public short rms_tax { get; set; }
		public short rms_kai1 { get; set; }
		public decimal rms_kaik { get; set; }
		public short rms_kai2 { get; set; }
		public short rms_kaib { get; set; }
		public short rms_kaih { get; set; }
		public string rms_kanamei { get; set; }
		public short rms_kkbn { get; set; }
		public string rms_koza { get; set; }
		public short rms_futan { get; set; }
		public short rms_tskbn { get; set; }
		public decimal rms_tesu { get; set; }
		public decimal rms_zan1 { get; set; }
		public decimal rms_zan2 { get; set; }
		public decimal rms_sei { get; set; }
		public decimal rms_nyu { get; set; }
		public decimal rms_uri { get; set; }
		public decimal rms_gonyu { get; set; }
		public decimal rms_miuri { get; set; }
		public int rms_kaibi { get; set; }
		public int rms_sseiymd { get; set; }
		public int rms_eseiymd { get; set; }
		public decimal rms_sotozei { get; set; }
		public decimal rms_kafu { get; set; }
		public DateTime? rms_upddate { get; set; }
		public string rms_skamoku { get; set; }
		public string rms_hkamoku { get; set; }
		public int rms_kosin { get; set; }
		public string rms_mailad { get; set; }
		public int? rms_tbank { get; set; }
		public string rms_fbank { get; set; }

		public vMicPCA仕入先マスタ()
		{
			rms_tcd = string.Empty;
			rms_mei1 = string.Empty;
			rms_mei2 = string.Empty;
			rms_tanmei = string.Empty;
			rms_mail = string.Empty;
			rms_ad1 = string.Empty;
			rms_ad2 = string.Empty;
			rms_tel = string.Empty;
			rms_fax = string.Empty;
			rms_jsk = 0;
			rms_keisyo = string.Empty;
			rms_tkbn1 = 0;
			rms_tkbn2 = 0;
			rms_tkbn3 = 0;
			rms_ttan = string.Empty;
			rms_ocd = string.Empty;
			rms_smb = 0;
			rms_hasu = 0;
			rms_tax = 0;
			rms_kai1 = 0;
			rms_kaik = 0;
			rms_kai2 = 0;
			rms_kaib = 0;
			rms_kaih = 0;
			rms_kanamei = string.Empty;
			rms_kkbn = 0;
			rms_koza = string.Empty;
			rms_futan = 0;
			rms_tskbn = 0;
			rms_tesu = 0;
			rms_zan1 = 0;
			rms_zan2 = 0;
			rms_sei = 0;
			rms_nyu = 0;
			rms_uri = 0;
			rms_gonyu = 0;
			rms_miuri = 0;
			rms_kaibi = 0;
			rms_sseiymd = 0;
			rms_eseiymd = 0;
			rms_sotozei = 0;
			rms_kafu = 0;
			rms_upddate = null;
			rms_skamoku = string.Empty;
			rms_hkamoku = string.Empty;
			rms_kosin = 0;
			rms_mailad = string.Empty;
			rms_tbank = null;
			rms_fbank = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMicPCA仕入先マスタ> DataTableToList(DataTable table)
		{
			List<vMicPCA仕入先マスタ> result = new List<vMicPCA仕入先マスタ>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					vMicPCA仕入先マスタ data = new vMicPCA仕入先マスタ
					{
						rms_tcd = row["rms_tcd"].ToString().Trim(),
						rms_mei1 = row["rms_mei1"].ToString().Trim(),
						rms_mei2 = row["rms_mei2"].ToString().Trim(),
						rms_tanmei = row["rms_tanmei"].ToString().Trim(),
						rms_mail = row["rms_mail"].ToString().Trim(),
						rms_ad1 = row["rms_ad1"].ToString().Trim(),
						rms_ad2 = row["rms_ad2"].ToString().Trim(),
						rms_tel = row["rms_tel"].ToString().Trim(),
						rms_fax = row["rms_fax"].ToString().Trim(),
						rms_jsk = DataBaseValue.ConvObjectToShort(row["rms_jsk"]),
						rms_keisyo = row["rms_keisyo"].ToString().Trim(),
						rms_tkbn1 = DataBaseValue.ConvObjectToInt(row["rms_tkbn1"]),
						rms_tkbn2 = DataBaseValue.ConvObjectToInt(row["rms_tkbn2"]),
						rms_tkbn3 = DataBaseValue.ConvObjectToInt(row["rms_tkbn3"]),
						rms_ttan = row["rms_ttan"].ToString().Trim(),
						rms_ocd = row["rms_ocd"].ToString().Trim(),
						rms_smb = DataBaseValue.ConvObjectToShort(row["rms_smb"]),
						rms_hasu = DataBaseValue.ConvObjectToShort(row["rms_hasu"]),
						rms_tax = DataBaseValue.ConvObjectToShort(row["rms_tax"]),
						rms_kai1 = DataBaseValue.ConvObjectToShort(row["rms_kai1"]),
						rms_kaik = DataBaseValue.ConvObjectToDecimal(row["rms_kaik"]),
						rms_kai2 = DataBaseValue.ConvObjectToShort(row["rms_kai2"]),
						rms_kaib = DataBaseValue.ConvObjectToShort(row["rms_kaib"]),
						rms_kaih = DataBaseValue.ConvObjectToShort(row["rms_kaih"]),
						rms_kanamei = row["rms_kanamei"].ToString().Trim(),
						rms_kkbn = DataBaseValue.ConvObjectToShort(row["rms_kkbn"]),
						rms_koza = row["rms_koza"].ToString().Trim(),
						rms_futan = DataBaseValue.ConvObjectToShort(row["rms_futan"]),
						rms_tskbn = DataBaseValue.ConvObjectToShort(row["rms_tskbn"]),
						rms_tesu = DataBaseValue.ConvObjectToDecimal(row["rms_tesu"]),
						rms_zan1 = DataBaseValue.ConvObjectToDecimal(row["rms_zan1"]),
						rms_zan2 = DataBaseValue.ConvObjectToDecimal(row["rms_zan2"]),
						rms_sei = DataBaseValue.ConvObjectToDecimal(row["rms_sei"]),
						rms_nyu = DataBaseValue.ConvObjectToDecimal(row["rms_nyu"]),
						rms_uri = DataBaseValue.ConvObjectToDecimal(row["rms_uri"]),
						rms_gonyu = DataBaseValue.ConvObjectToDecimal(row["rms_gonyu"]),
						rms_miuri = DataBaseValue.ConvObjectToDecimal(row["rms_miuri"]),
						rms_kaibi = DataBaseValue.ConvObjectToInt(row["rms_kaibi"]),
						rms_sseiymd = DataBaseValue.ConvObjectToInt(row["rms_sseiymd"]),
						rms_eseiymd = DataBaseValue.ConvObjectToInt(row["rms_eseiymd"]),
						rms_sotozei = DataBaseValue.ConvObjectToDecimal(row["rms_sotozei"]),
						rms_kafu = DataBaseValue.ConvObjectToDecimal(row["rms_kafu"]),
						rms_upddate = DataBaseValue.ConvObjectToDateTimeNull(row["rms_upddate"]),
						rms_skamoku = row["rms_skamoku"].ToString().Trim(),
						rms_hkamoku = row["rms_hkamoku"].ToString().Trim(),
						rms_kosin = DataBaseValue.ConvObjectToInt(row["rms_kosin"]),
						rms_mailad = row["rms_mailad"].ToString().Trim(),
						rms_tbank = DataBaseValue.ConvObjectToIntNull(row["rms_tbank"]),
						rms_fbank = row["rms_fbank"].ToString().Trim(),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
