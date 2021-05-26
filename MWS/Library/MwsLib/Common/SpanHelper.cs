//
// SpanHelper.cs
// 
// Span(期間)クラスの拡張メソッドを定義
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.Collections.Generic;
using System.Linq;

namespace MwsLib.Common
{
	/// <summary>
	/// Span(期間)クラスの拡張メソッドを定義
	/// </summary>
	public static class SpanHelper
    {
        /// <summary>
        /// 期間の比較
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns></returns>
        public static bool EqualsEx(this IEnumerable<Span> left, IEnumerable<Span> right)
        {
            return left.SequenceEqual(right);
        }

        /// <summary>
        /// 全期間を取得　※期間順でソートしてあることが前提
        /// </summary>
        /// <param name="list">Span(期間)</param>
        /// <returns>全期間</returns>
        public static Span GetAllSpan(this List<Span> list)
        {
            if (0 < list.Count)
            {
                Span first = list[0];
                Span last = list[list.Count - 1];

                return new Span(first.Start, last.End);
            }
            return Span.Nothing;
        }

		/// <summary>
		/// 期間の月数を取得
		/// </summary>
		/// <param name="span">this</param>
		/// <returns>月数</returns>
		public static int GetMonthCount(this Span span)
		{
			return span.End.ToYearMonth() - span.Start.ToYearMonth() + 1;
		}
    }
}
