//
// enum EikyushiNyushiType - 永久歯・乳歯の区別
//
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace CommonLib.BuiData
{
	/// <summary>
	/// 永久歯・乳歯の区別
	/// </summary>
	[Flags]
    public enum EikyushiNyushiType : uint
    {
        /// <summary>未定義値</summary>
        None = 0,

        /// <summary>永久歯(1～8)</summary>
        Eikyushi = 0x00000001,
        /// <summary>乳歯(A～E)</summary>
        Nyushi = 0x00000002,

        All = Eikyushi | Nyushi
    }
}
