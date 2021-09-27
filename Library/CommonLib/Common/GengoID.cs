//
// GengoID.cs
// 
// 元号識別子の定義
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// Ver1.001 新元号令和対応(2019/05/01 勝呂)
//
namespace CommonLib.Common
{
	public enum GengoID
    {
        None = 0,
        /// <summary>明治</summary>
        Meiji,
        /// <summary>大正</summary>
        Taisho,
        /// <summary>昭和</summary>
        Showa,
        /// <summary>平成</summary>
        Heisei,

		// Ver1.001 新元号令和対応(2019/05/01 勝呂)
		/// <summary>令和</summary>
		Reiwa,
	};
}
