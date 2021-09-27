//
// enum HalfJaw - 上顎右側、上顎左側、下顎右側、下顎左側の区分
//
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace CommonLib.BuiData
{
	/// <summary>上顎右側、上顎左側、下顎右側、下顎左側の区分</summary>
	[Flags]
    public enum HalfJaw : uint
    {
        /// <summary>未定義値</summary>
        None = 0,

        /// <summary>上顎右側</summary>
        UpperRight = 0x00000001,
        /// <summary>上顎左側</summary>
        UpperLeft = 0x00000002,
        /// <summary>下顎右側</summary>
        LowerRight = 0x00000004,
        /// <summary>下顎左側</summary>
        LowerLeft = 0x00000008,

        All = UpperRight | UpperLeft | LowerRight | LowerLeft
    }
}
