//
// CalcTax.cs
// 
// 消費税計算クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
namespace MwsLib.BaseFactory
{
	/// <summary>
	/// 消費税計算クラス
	/// </summary>
	public static class CalcTax
	{
		/// <summary>
		/// 消費税額の端数処理 
		/// </summary>
		public enum RoundFraction
		{
			/// <summary>切り捨て</summary>
			Cut = 0,
			/// <summary>四捨五入</summary>
			Round = 1,
			/// <summary>切り上げ</summary>
			Raise = 2,
		}

		/// <summary>
		/// 価格から消費税を取得する
		/// </summary>
		/// <param name="tax_rate">消費税率</param>
		/// <param name="round">端数処理指定</param>
		/// <param name="price">価格</param>
		/// <returns>消費税</returns>
		public static int GetTaxPrice(int taxRate, RoundFraction round, int price)
		{
			if (0 < taxRate && 0 < price)
			{
				// 外税
				int correct = 0;
				switch (round)
				{
					case RoundFraction.Cut: // 切り捨て
						correct = 0;
						break;
					case RoundFraction.Round: // 四捨五入
						correct = 50;
						break;
					case RoundFraction.Raise: // 切り上げ
						correct = 99;
						break;
				}
				return (price * taxRate + correct) / 100;
			}
			return 0;
		}
	}
}
