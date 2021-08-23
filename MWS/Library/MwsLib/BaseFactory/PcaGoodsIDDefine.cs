//
// PCA商品マスター定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/02/08 勝呂)
// Ver1.001 PC安心サポートPlus対応(2020/10/16 勝呂)
//
namespace MwsLib.BaseFactory
{
	public static class PcaGoodsIDDefine
	{
		/// <summary>
		/// 記事レコード
		/// </summary>
		public const string ArticleCode = "000014";

		/// <summary>
		/// 送料 PCA商品コード
		/// </summary>
		public const string ShippingCode = "000600";

		/// <summary>
		/// 着日指定 PCA商品コード
		/// </summary>
		public const string ArrivalDateCode = "000020";

		/// <summary>
		/// PC安心サポート３年契約
		/// </summary>
		public const string PcSupport3 = "001871";

		/// <summary>
		/// PC安心サポート１年契約
		/// </summary>
		public const string PcSupport1 = "001872";

		/// <summary>
		/// PC安心サポート１年契約（更新用）
		/// </summary>
		public const string PcSupport1Continue = "001874";

		/// <summary>
		/// PC安心サポートPlus３年契約
		/// </summary>
		public const string PcSupportPlus3 = "101871";

		/// <summary>
		/// PC安心サポートPlus１年契約
		/// </summary>
		public const string PcSupportPlus1 = "101872";

		/// <summary>
		/// PC安心サポートPlus１年契約（更新用）
		/// </summary>
		public const string PcSupportPlus1Continue = "101874";

		/// <summary>
		/// クラウドバックアップ（PC安心サポートPlus）
		/// </summary>
		public const string CloudBackupPcSupport = "800084";

		/// <summary>
		/// MWS palette ES 2019版
		/// </summary>
		public const string PaletteES_2019 = "800121";

		/// <summary>
		/// MWS palette ES（月額）
		/// </summary>
		public const string PaletteES_Monthly = "800122";

		/// <summary>
		/// palette ES ｿﾌﾄｳｪｱ保守料72ケ月
		/// </summary>
		public const string PaletteES_Mainte72 = "800161";

		/// <summary>
		/// palette ES ｿﾌﾄｳｪｱ保守料12ケ月
		/// </summary>
		public const string PaletteES_Mainte12 = "800162";

		/// <summary>
		/// おまとめプラン12ケ月
		/// </summary>
		public const string Matome12 = "800155";

		/// <summary>
		/// おまとめプラン24ケ月
		/// </summary>
		public const string Matome24 = "800156";

		/// <summary>
		/// おまとめプラン36ケ月
		/// </summary>
		public const string Matome36 = "800157";

		/// <summary>
		/// おまとめプラン48ケ月
		/// </summary>
		public const string Matome48 = "800158";

		/// <summary>
		/// おまとめプラン60ケ月
		/// </summary>
		public const string Matome60 = "800159";

		/// <summary>
		/// MIC WEB SERVICE(ﾌﾟﾗｯﾄﾌｫｰﾑ利用 月額)
		/// </summary>
		public const string MwsPlatform = "800001";

		/// <summary>
		/// MWS Curline ｸﾗｳﾄﾞ利用料(月額)                          
		/// </summary>
		public const string MwsCurlineCloud = "802001";

		/// <summary>
		/// はなはなし
		/// </summary>
		public const string Hanahanashi = "001401";

		/// <summary>
		/// PC安心サポートかどうか？
		/// </summary>
		/// <param name="code">商品コード</param>
		/// <returns>判定</returns>
		public static bool IsPcSupport(string code)
		{
			switch (code)
			{
				case PcSupport3:
				case PcSupport1:
				case PcSupport1Continue:
				case PcSupportPlus3:
				case PcSupportPlus1:
				case PcSupportPlus1Continue:
					return true;
			}
			return false;
		}

		/// <summary>
		/// おまとめプランの商品かどうか？
		/// </summary>
		/// <param name="code">商品コード</param>
		/// <returns>判定</returns>
		public static bool IsMatome(string code)
		{
			switch (code)
			{
				case Matome12:
				case Matome24:
				case Matome36:
				case Matome48:
				case Matome60:
					return true;
			}
			return false;
		}

		/// <summary>
		/// paletteS商品かどうか？
		/// </summary>
		/// <param name="code">商品コード</param>
		/// <returns>判定</returns>
		public static bool IsPaletteES(string code)
		{
			if (code == PaletteES_2019)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// ソフトウェア保守料かどうか？
		/// </summary>
		/// <param name="code">商品コード</param>
		/// <returns>判定</returns>
		public static bool IsPaletteES_Mainte(string code)
		{
			switch (code)
			{
				case PaletteES_Mainte72:
				case PaletteES_Mainte12:
					return true;
			}
			return false;
		}
	}
}
