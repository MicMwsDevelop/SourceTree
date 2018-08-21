//
// GengoInformation.cs
// 
// 元号情報定義クラスの定義
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;

namespace MwsLib.Common
{
	/// <summary>
	/// 元号情報定義クラス
	/// </summary>
	[Serializable]
    public class GengoInformation
    {
        /// <summary>元号識別子</summary>
        public GengoID ID { get; set; }
        /// <summary>元号名称(漢字)</summary>
        public string Name { get; set; }
        /// <summary>元号アルファベット表記</summary>
        public string AlphaName { get; set; }
        /// <summary>元号期間</summary>
        public Span Span { get; set; }

        /// <summary>
        /// 西暦年から、元号年に変換
        /// </summary>
        /// <param name="adYear">西暦年</param>
        /// <returns>元号年</returns>
        public int ToGengoYear(int adYear)
        {
            if (adYear < Span.Start.Year)
            {
                throw new ArgumentException(string.Format("元号開始年以前の西暦年[{0}]が指定された。", adYear));
            }
            else
            {
                return adYear - (Span.Start.Year - 1);
            }
        }

        /// <summary>
        /// 元号年から西暦年に変換
        /// </summary>
        /// <param name="gengoYear">元号年</param>
        /// <returns>西暦年</returns>
        public int ToADYear(int gengoYear)
        {
            return gengoYear + (Span.Start.Year - 1);
        }
    }
}
