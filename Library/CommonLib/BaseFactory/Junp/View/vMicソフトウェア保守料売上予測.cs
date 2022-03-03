//
// vMicソフトウェア保守料売上予測.cs
//
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/02/14 勝呂)
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.View
{
	/// <summary>
	/// vMicソフトウェア保守料売上予測
	/// </summary>
	public class vMicソフトウェア保守料売上予測
	{
		public string 部門コード { get; set; }
		public string 営業部名 { get; set; }
		public string 拠点コード { get; set; }
		public string 拠点名 { get; set; }
		public int 顧客No { get; set; }
		public string 顧客名 { get; set; }
		public int 受注番号 { get; set; }
		public DateTime 受注承認日 { get; set; }
		public DateTime 売上承認日 { get; set; }
		public string 納期 { get; set; }
		public int 売上金額 { get; set; }
		public string 計上月 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vMicソフトウェア保守料売上予測()
		{
			部門コード = string.Empty;
			営業部名 = string.Empty;
			拠点コード = string.Empty;
			拠点名 = string.Empty;
			顧客No = 0;
			顧客名 = string.Empty;
			受注番号 = 0;
			納期 = string.Empty;
			売上金額 = 0;
			計上月 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMicソフトウェア保守料売上予測> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<vMicソフトウェア保守料売上予測> result = new List<vMicソフトウェア保守料売上予測>();
				foreach (DataRow row in table.Rows)
				{
					vMicソフトウェア保守料売上予測 data = new vMicソフトウェア保守料売上予測
					{
						部門コード = row["部門コード"].ToString().Trim(),
						営業部名 = row["営業部名"].ToString().Trim(),
						拠点コード = row["拠点コード"].ToString().Trim(),
						拠点名 = row["拠点名"].ToString().Trim(),
						顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						顧客名 = row["顧客名"].ToString().Trim(),
						受注番号 = DataBaseValue.ConvObjectToInt(row["受注番号"]),
						受注承認日 = DataBaseValue.ConvObjectToDateTime(row["受注承認日"]),
						売上承認日 = DataBaseValue.ConvObjectToDateTime(row["売上承認日"]),
						納期 = row["納期"].ToString().Trim(),
						売上金額 = DataBaseValue.ConvObjectToInt(row["売上金額"]),
						計上月 = row["計上月"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
