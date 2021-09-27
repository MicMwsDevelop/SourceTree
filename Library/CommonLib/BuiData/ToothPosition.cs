//
// enum ToothPosition - 歯の位置の定義(永久歯１番と永久歯２番は区別するが、１番と乳歯Ａは同じ位置)
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace CommonLib.BuiData
{
	/// <summary>
	/// 歯番(歯の位置)の定義<br/>
	/// 歯だけでなく、欠損(隙)・装置・歯周等の位置を表すのにも用いる
	/// </summary>
	/// <remarks>
	/// enum値は変わっても良いが近心→遠心の順、かつ右上、左上、右下、左下の各ブロック内では
	/// 連続している必要がある→テスト項目
	/// </remarks>
	/// <remarks>
	/// 永久歯1と乳歯A, 2とB, 3とC・・・はそれぞれ同じ位置にあるという前提なので、ToothPositionでは区別しない。<br/>
	/// </remarks>
	[Flags]
    public enum ToothPosition : uint
    {
        /// <summary>未定義値</summary>
        None = 0,

        /// <summary>歯番右上1(乳歯A)</summary>
        UpperRight1 = 0x00000001,
        /// <summary>歯番右上2(乳歯B)</summary>
        UpperRight2 = 0x00000002,
        /// <summary>歯番右上3(乳歯C)</summary>
        UpperRight3 = 0x00000004,
        /// <summary>歯番右上4(乳歯D)</summary>
        UpperRight4 = 0x00000008,
        /// <summary>歯番右上5(乳歯E)</summary>
        UpperRight5 = 0x00000010,
        /// <summary>歯番右上6</summary>
        UpperRight6 = 0x00000020,
        /// <summary>歯番右上7</summary>
        UpperRight7 = 0x00000040,
        /// <summary>歯番右上8</summary>
        UpperRight8 = 0x00000080,

        /// <summary>歯番左上1(乳歯A)</summary>
        UpperLeft1 = 0x00000100,
        /// <summary>歯番左上2(乳歯B)</summary>
        UpperLeft2 = 0x00000200,
        /// <summary>歯番左上3(乳歯C)</summary>
        UpperLeft3 = 0x00000400,
        /// <summary>歯番左上4(乳歯D)</summary>
        UpperLeft4 = 0x00000800,
        /// <summary>歯番左上5(乳歯E)</summary>
        UpperLeft5 = 0x00001000,
        /// <summary>歯番左上6</summary>
        UpperLeft6 = 0x00002000,
        /// <summary>歯番左上7</summary>
        UpperLeft7 = 0x00004000,
        /// <summary>歯番左上8</summary>
        UpperLeft8 = 0x00008000,

        /// <summary>歯番右下1(乳歯A)</summary>
        LowerRight1 = 0x00010000,
        /// <summary>歯番右下2(乳歯B)</summary>
        LowerRight2 = 0x00020000,
        /// <summary>歯番右下3(乳歯C)</summary>
        LowerRight3 = 0x00040000,
        /// <summary>歯番右下4(乳歯D)</summary>
        LowerRight4 = 0x00080000,
        /// <summary>歯番右下5(乳歯E)</summary>
        LowerRight5 = 0x00100000,
        /// <summary>歯番右下6</summary>
        LowerRight6 = 0x00200000,
        /// <summary>歯番右下7</summary>
        LowerRight7 = 0x00400000,
        /// <summary>歯番右下8</summary>
        LowerRight8 = 0x00800000,

        /// <summary>歯番左下1(乳歯A)</summary>
        LowerLeft1 = 0x01000000,
        /// <summary>歯番左下2(乳歯B)</summary>
        LowerLeft2 = 0x02000000,
        /// <summary>歯番左下3(乳歯C)</summary>
        LowerLeft3 = 0x04000000,
        /// <summary>歯番左下4(乳歯D)</summary>
        LowerLeft4 = 0x08000000,
        /// <summary>歯番左下5(乳歯E)</summary>
        LowerLeft5 = 0x10000000,
        /// <summary>歯番左下6</summary>
        LowerLeft6 = 0x20000000,
        /// <summary>歯番左下7</summary>
        LowerLeft7 = 0x40000000,
        /// <summary>歯番左下8</summary>
        LowerLeft8 = 0x80000000,

        All = 0xFFFFFFFF
    }
}
