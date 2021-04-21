//
// AlemxPurchase.cs
//
// アルメックス仕入先コード情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2021/01/14 勝呂)
//
namespace MwsLib.BaseFactory.AlmexMainte
{
	public static class AlemxPurchase
	{
		/// <summary>
		/// 住所から仕入先コードを決定する
		/// </summary>
		/// <param name="ken">県番号</param>
		/// <param name="address">住所</param>
		/// <returns>仕入先コード</returns>
		public static string 仕入先コード(KenNumDef.KenNumber ken, string address)
		{
			switch (ken)
			{
				// 札幌支店
				case KenNumDef.KenNumber.Hokkaido:
					return "010197";
				// 仙台支店
				case KenNumDef.KenNumber.Miyagi:
				case KenNumDef.KenNumber.Yamagata:
				case KenNumDef.KenNumber.Fukushima:
				case KenNumDef.KenNumber.Aomori:
				case KenNumDef.KenNumber.Iwate:
				case KenNumDef.KenNumber.Akita:
					return "010198";
				// 東東京支店
				case KenNumDef.KenNumber.Tochigi:
				case KenNumDef.KenNumber.Chiba:
				case KenNumDef.KenNumber.Saitama:
				case KenNumDef.KenNumber.Ibaraki:
					return "010199";
				// 西東京支店
				case KenNumDef.KenNumber.Tokyo:
				case KenNumDef.KenNumber.Kanagawa:
				case KenNumDef.KenNumber.Yamanashi:
				case KenNumDef.KenNumber.Niigata:
				case KenNumDef.KenNumber.Gunma:
					return "010200";
				// 名古屋支店
				case KenNumDef.KenNumber.Aichi:
				case KenNumDef.KenNumber.Gifu:
				case KenNumDef.KenNumber.Ishikawa:
				case KenNumDef.KenNumber.Fukui:
				case KenNumDef.KenNumber.Toyama:
					return "010201";
				// 大阪支店
				case KenNumDef.KenNumber.Osaka:
				case KenNumDef.KenNumber.Kyoto:
				case KenNumDef.KenNumber.Nara:
				case KenNumDef.KenNumber.Shiga:
				case KenNumDef.KenNumber.Hyogo:
				case KenNumDef.KenNumber.Wakayama:
					return "010202";
				// 広島支店
				case KenNumDef.KenNumber.Hiroshima:
				case KenNumDef.KenNumber.Tottori:
				case KenNumDef.KenNumber.Okayama:
				case KenNumDef.KenNumber.Shimane:
				case KenNumDef.KenNumber.Yamaguchi:
				case KenNumDef.KenNumber.Kagawa:
				case KenNumDef.KenNumber.Ehime:
				case KenNumDef.KenNumber.Tokushima:
					return "010203";
				// 福岡支店
				case KenNumDef.KenNumber.Fukuoka:
				case KenNumDef.KenNumber.Saga:
				case KenNumDef.KenNumber.Nagasaki:
				case KenNumDef.KenNumber.Oita:
				case KenNumDef.KenNumber.Kumamoto:
				case KenNumDef.KenNumber.Miyazaki:
				case KenNumDef.KenNumber.Kagoshima:
				case KenNumDef.KenNumber.Okinawa:
					return "010204";
			}
			if (KenNumDef.KenNumber.Shizuoka == ken)
			{
				// 西東京支店
				if (-1 != address.IndexOf("静岡市")
					|| -1 != address.IndexOf("浜松市")
					|| -1 != address.IndexOf("沼津市")
					|| -1 != address.IndexOf("熱海市")
					|| -1 != address.IndexOf("三島市")
					|| -1 != address.IndexOf("富士宮市")
					|| -1 != address.IndexOf("伊東市")
					|| -1 != address.IndexOf("島田市")
					|| -1 != address.IndexOf("富士市")
					|| -1 != address.IndexOf("磐田市")
					|| -1 != address.IndexOf("焼津市")
					|| -1 != address.IndexOf("掛川市")
					|| -1 != address.IndexOf("藤枝市")
					|| -1 != address.IndexOf("御殿場市")
					|| -1 != address.IndexOf("袋井市")
					|| -1 != address.IndexOf("下田市")
					|| -1 != address.IndexOf("裾野市")
					|| -1 != address.IndexOf("湖西市")
					|| -1 != address.IndexOf("伊豆市")
					|| -1 != address.IndexOf("御前崎市")
					|| -1 != address.IndexOf("菊川市")
					|| -1 != address.IndexOf("賀茂郡")
					|| -1 != address.IndexOf("田方郡")
					|| -1 != address.IndexOf("駿東郡")
					|| -1 != address.IndexOf("榛原郡")
					|| -1 != address.IndexOf("周智郡"))
				{
					return "010200";
				}
				// 名古屋支店
				return "010201";
			}
			return string.Empty;
		}
	}
}
