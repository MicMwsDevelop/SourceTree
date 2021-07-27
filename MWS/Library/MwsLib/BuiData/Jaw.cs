//
// enum Jaw - 上下顎の区分
//
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace MwsLib.BuiData
{
	/// <summary>
	/// 上下顎の区分
	/// </summary>
	[Flags]
    public enum Jaw : uint
    {
        /// <summary>未定義値</summary>
        None = 0,

        /// <summary>上顎</summary>
        UpperJaw = 0x00000001,
        /// <summary>下顎</summary>
        LowerJaw = 0x00000002
    }
}
