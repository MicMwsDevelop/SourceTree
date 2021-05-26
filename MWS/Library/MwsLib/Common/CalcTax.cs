//
// CalcTax.cs
// 
// 消費税計算クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2021/04/09 勝呂)
//
using System;

namespace MwsLib.Common
{
	/// <summary>
	/// 消費税計算クラス
	/// </summary>
	public static class CalcTax
	{
		/// <summary>
		/// 端数処理
		/// </summary>
		public enum RoundFraction
		{
			/// <summary>
			/// 切り捨て
			/// </summary>
			Cut = 0,
			/// <summary>
			/// 四捨五入
			/// </summary>
			Round = 1,
			/// <summary>
			/// 切り上げ
			/// </summary>
			Raise = 2,
		}

		/// <summary>
		/// 価格から消費税を取得する
		/// </summary>
		/// <param name="tax_rate">消費税率</param>
		/// <param name="round">端数処理</param>
		/// <param name="price">価格</param>
		/// <returns>消費税</returns>
		public static int GetTax(short tax_rate, RoundFraction round, int price)
		{
			if (0 < tax_rate && 0 < price)
			{
				switch (round)
				{
					// 切り捨て
					case RoundFraction.Cut:
						return Decimal.ToInt32(Math.Floor((decimal)(price * tax_rate)));
					// 四捨五入
					case RoundFraction.Round:
						return Decimal.ToInt32(Math.Round((decimal)(price * tax_rate)));
					// 切り上げ
					case RoundFraction.Raise:
						return Decimal.ToInt32(Math.Ceiling((decimal)(price * tax_rate)));
				}
			}
			return 0;
		}
	}
}
