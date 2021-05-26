//
// 予測連絡用ES.cs
//
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2021/05/19 勝呂)
//
using MwsLib.DB;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.ProspectProgressAutoAggregate
{
	/// <summary>
	/// 
	/// </summary>
	public class 予測連絡用ES
	{
		public string 売上月 { get; set; }
		public int 受注番号 { get; set; }
		public string 受注日 { get; set; }
		public int 販売先コード { get; set; }
		public int ユーザーコード { get; set; }
		public string 販売先 { get; set; }
		public string ユーザー { get; set; }
		public int 受注金額 { get; set; }
		public string 件名 { get; set; }
		public string 納期 { get; set; }
		public short リプレース区分 { get; set; }
		public string リプレース { get; set; }
		public string 担当者コード { get; set; }
		public string 担当者名 { get; set; }
		public string BshCode2 { get; set; }
		public string BshCode3 { get; set; }
		public string 担当支店名 { get; set; }
		public string 受注承認日 { get; set; }
		public string 売上承認日 { get; set; }
		public string 請求区分 { get; set; }
		public int 販売店コード { get; set; }
		public string 販売店 { get; set; }
		public int 販売種別 { get; set; }
		public string 課金開始日 { get; set; }
		public string 課金終了日 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 予測連絡用ES()
		{
			売上月 = string.Empty;
			受注番号 = 0;
			受注日 = string.Empty;
			販売先コード = 0;
			ユーザーコード = 0;
			販売先 = string.Empty;
			ユーザー = string.Empty;
			受注金額 = 0;
			件名 = string.Empty;
			納期 = string.Empty;
			リプレース区分 = 0;
			リプレース = string.Empty;
			担当者コード = string.Empty;
			担当者名 = string.Empty;
			BshCode2 = string.Empty;
			BshCode3 = string.Empty;
			担当支店名 = string.Empty;
			受注承認日 = string.Empty;
			売上承認日 = string.Empty;
			請求区分 = string.Empty;
			販売店コード = 0;
			販売店 = string.Empty;
			販売種別 = 0;
			課金開始日 = string.Empty;
			課金終了日 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<予測連絡用ES> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<予測連絡用ES> result = new List<予測連絡用ES>();
				foreach (DataRow row in table.Rows)
				{
					予測連絡用ES data = new 予測連絡用ES
					{
						売上月 = row["売上月"].ToString().Trim(),
						受注番号 = DataBaseValue.ConvObjectToInt(row["受注番号"]),
						受注日 = row["受注日"].ToString().Trim(),
						販売先コード = DataBaseValue.ConvObjectToInt(row["販売先コード"]),
						ユーザーコード = DataBaseValue.ConvObjectToInt(row["ユーザーコード"]),
						販売先 = row["販売先"].ToString().Trim(),
						ユーザー = row["ユーザー"].ToString().Trim(),
						受注金額 = DataBaseValue.ConvObjectToInt(row["受注金額"]),
						件名 = row["件名"].ToString().Trim(),
						納期 = row["納期"].ToString().Trim(),
						リプレース区分 = DataBaseValue.ConvObjectToShort(row["リプレース区分"]),
						リプレース = row["リプレース"].ToString().Trim(),
						担当者コード = row["担当者コード"].ToString().Trim(),
						担当者名 = row["担当者名"].ToString().Trim(),
						BshCode2 = row["BshCode2"].ToString().Trim(),
						BshCode3 = row["BshCode3"].ToString().Trim(),
						担当支店名 = row["担当支店名"].ToString().Trim(),
						受注承認日 = row["受注承認日"].ToString().Trim(),
						売上承認日 = row["売上承認日"].ToString().Trim(),
						請求区分 = row["請求区分"].ToString().Trim(),
						販売店コード = DataBaseValue.ConvObjectToInt(row["販売店コード"]),
						販売店 = row["販売店"].ToString().Trim(),
						販売種別 = DataBaseValue.ConvObjectToInt(row["販売種別"]),
						課金開始日 = row["課金開始日"].ToString().Trim(),
						課金終了日 = row["課金終了日"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
