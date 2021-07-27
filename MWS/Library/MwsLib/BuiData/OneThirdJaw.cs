//
// enum OneThirdJaw - >1／3顎単位の区分
//
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace MwsLib.BuiData
{
	/// <summary>1／3顎単位の区分</summary>
	[Flags]
    public enum OneThirdJaw : uint
    {
        /// <summary>未定義値</summary>
        None = 0,

        /// <summary>上顎右臼歯部(８－４┘)</summary>
        UpperRightMolars = 0x00000001,
        /// <summary>上顎前歯(３┴３)</summary>
        UpperFrontTeeth = 0x00000002,
        /// <summary>上顎左臼歯部(└４－８)</summary>
        UpperLeftMolars = 0x00000004,

        /// <summary>下顎右臼歯部(８－４┘)</summary>
        LowerRightMolars = 0x00000008,
        /// <summary>下顎前歯(３┬３)</summary>
        LowerFrontTeeth = 0x00000010,
        /// <summary>下顎左臼歯部(┌４－８)</summary>
        LowerLeftMolars = 0x00000020
    }
}
