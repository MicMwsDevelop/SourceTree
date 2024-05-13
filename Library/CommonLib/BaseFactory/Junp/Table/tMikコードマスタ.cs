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
		/// fcmコード 031:002170 ｱﾙﾒｯｸｽ TEX-30 保守(現金仕様)1ヶ月 \25,500
		/// </summary>
		public const string fcmコード_AlmexMainteTex30_Cash = "031";

		/// <summary>
		/// fcmコード 032:002171 ｱﾙﾒｯｸｽ TEX-30 保守(ｸﾚｼﾞｯﾄ仕様)1ヶ月 \29,000
		/// </summary>
		public const string fcmコード_AlmexMainteTex30_Credit = "032";

		/// <summary>
		/// fcmコード 033:002186 ｱﾙﾒｯｸｽ FIT-A 保守(現金仕様)1ヶ月 \26,500
		/// </summary>
		public const string fcmコード_AlmexMainteFitA_Cash = "033";

		/// <summary>
		/// fcmコード 034:002187 ｱﾙﾒｯｸｽ FIT-A保守(ｸﾚ仕様/取端無)1ヶ月 \27,000
		/// </summary>
		public const string fcmコード_AlmexMainteFitA_Credit = "034";

		/// <summary>
		/// fcmコード 035:002188 ｱﾙﾒｯｸｽ FIT-A 保守(QRｸﾚｼﾞｯﾄ仕様)1ヶ月 \28,000
		/// </summary>
		public const string fcmコード_AlmexMainteFitA_QRCredit = "035";

		/// <summary>
		/// fcmコード 036:002199 ｱﾙﾒｯｸｽ FIT-A 保守(現金2台仕様)1ヶ月 \51,500
		/// </summary>
		public const string fcmコード_AlmexMainteFitA_Cash2 = "036";

		/// <summary>
		/// fcmコード 040:018248 ﾘｺｰ ｵﾝ資用 閉域網利用料 1年 \36,000
		/// </summary>
		public const string fcmコード_Richo_LineUsageFee1 = "040";

		/// <summary>
		/// fcmコード 041:018249 ﾘｺｰ ｵﾝ資用 閉域網利用料 5年 \162,000
		/// </summary>
		public const string fcmコード_Richo_LineUsageFee5 = "041";

		/// <summary>
		/// fcmコード 042:018252 ﾘｺｰ ｵﾝ資用ﾉｰﾄPC ｵﾝｻｲﾄ保守 1年 \14,800
		/// </summary>
		public const string fcmコード_Richo_MainteUsageFeePC1 = "042";

		/// <summary>
		/// fcmコード 043:018253 ﾘｺｰ ｵﾝ資用ﾉｰﾄPC ｵﾝｻｲﾄ保守 5年 \71,500
		/// </summary>
		public const string fcmコード_Richo_MainteUsageFeePC5 = "043";

		/// <summary>
		/// fcmコード 044:018250 ﾘｺｰ ｵﾝ資用ﾙｰﾀｰ ｵﾝｻｲﾄ保守 1年 \8,800
		/// </summary>
		public const string fcmコード_Richo_MainteUsageFeeRT1 = "044";

		/// <summary>
		/// fcmコード 045:018251 ﾘｺｰ ｵﾝ資用ﾙｰﾀｰ ｵﾝｻｲﾄ保守 5年 \44,000
		/// </summary>
		public const string fcmコード_Richo_MainteUsageFeeRT5 = "045";

		/// <summary>
		/// fcmコード 046:018213 菱ｴﾚ ｵﾝ資格 ﾙｰﾀｰ年間接続保守料 \50,400
		/// </summary>
		public const string fcmコード_Ryoyo_LineUsageFee = "046";

		/// <summary>
		/// fcmコード 047:018214 菱ｴﾚ ｵﾝ資格 年間保守料平日9-17時 \24,000
		/// </summary>
		public const string fcmコード_Ryoyo_MainteUsageFee = "047";

		/// <summary>
		/// fcmコード 048:018502 SHINKO オン資・オンサイト保守(更新)　売価：\36,000(税抜)　仕入値：\24,000(税抜)
		/// </summary>
		public const string fcmコード_Shinko_MainteUsageFee1 = "048";

		/// <summary>
		/// fcmコード 049:018501 MICオンライン資格確認保守サービス(更新)　売価：\40,800(税抜)　仕入値：\34,800(税抜)
		/// </summary>
		public const string fcmコード_Mic_MainteUsageFee1 = "049";

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