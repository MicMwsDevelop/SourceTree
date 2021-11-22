//
// tMikコードマスタ.cs
//
// tMikコードマスタクラス
// [JunpDB].[dbo].[tMikコードマスタ]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2019/06/28 勝呂)
// Ver1.02 002189 アルメックス FIT-A 保守(ｸﾚｼﾞｯﾄ仕様)1ヶ月 削除の対応(2021/01/20 勝呂)
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.Table
{
	/// <summary>
	/// [JunpDB].[dbo].[tMikコードマスタ]
	/// </summary>
	public class tMikコードマスタ
	{
		/// <summary>
		/// fcmコード種別 18:アプリケーション名
		/// </summary>
		public const string fcmコード種別_ApplicationName = "18";

		/// <summary>
		/// fcmコード 031:ｱﾙﾒｯｸｽ TEX-30 保守(現金仕様)1ヶ月
		/// </summary>
		public const string fcmコード_AlmexMainteTex30_Cash = "031";

		/// <summary>
		/// fcmコード 032:ｱﾙﾒｯｸｽ TEX-30 保守(ｸﾚｼﾞｯﾄ仕様)1ヶ月
		/// </summary>
		public const string fcmコード_AlmexMainteTex30_Credit = "032";

		/// <summary>
		/// fcmコード 033:ｱﾙﾒｯｸｽ FIT-A 保守(現金仕様)1ヶ月
		/// </summary>
		public const string fcmコード_AlmexMainteFitA_Cash = "033";

		/// <summary>
		/// fcmコード 034:ｱﾙﾒｯｸｽ FIT-A保守(ｸﾚ仕様/取端無)1ヶ月
		/// </summary>
		public const string fcmコード_AlmexMainteFitA_Credit = "034";

		/// <summary>
		/// fcmコード 035:ｱﾙﾒｯｸｽ FIT-A 保守(QRｸﾚｼﾞｯﾄ仕様)1ヶ月
		/// </summary>
		public const string fcmコード_AlmexMainteFitA_QRCredit = "035";

		/// <summary>
		/// fcmID
		/// </summary>
		public int fcmID { get; set; }

		/// <summary>
		/// fcmコード種別
		/// </summary>
		public string fcmコード種別 { get; set; }

		/// <summary>
		/// fcmコード
		/// </summary>
		public string fcmコード { get; set; }

		/// <summary>
		/// fcm名称
		/// </summary>
		public string fcm名称 { get; set; }

		/// <summary>
		/// fcm更新日
		/// </summary>
		public DateTime? fcm更新日 { get; set; }

		/// <summary>
		/// fcm更新者
		/// </summary>
		public string fcm更新者 { get; set; }

		/// <summary>
		/// fcmサブコード
		/// </summary>
		public string fcmサブコード { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMikコードマスタ()
		{
			fcmID = 0;
			fcmコード種別 = null;
			fcmコード = null;
			fcm名称 = null;
			fcm更新日 = null;
			fcm更新者 = null;
			fcmサブコード = null;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tMikコードマスタ> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMikコードマスタ> result = new List<tMikコードマスタ>();
				foreach (DataRow row in table.Rows)
				{
					tMikコードマスタ data = new tMikコードマスタ
					{
						fcmID = DataBaseValue.ConvObjectToInt(row["fcmID"]),
						fcmコード種別 = row["fcmコード種別"].ToString().Trim(),
						fcmコード = row["fcmコード"].ToString().Trim(),
						fcm名称 = row["fcm名称"].ToString().Trim(),
						fcm更新日 = DataBaseValue.ConvObjectToDateTimeNull(row["fcm更新日"]),
						fcm更新者 = row["fcm更新者"].ToString().Trim(),
						fcmサブコード = row["fcmサブコード"].ToString().Trim()
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}