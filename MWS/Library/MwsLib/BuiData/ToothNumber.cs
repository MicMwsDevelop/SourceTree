//
// enum ToothNumber -- 歯番の定義
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BuiData
{
    [Flags]
    public enum ToothNumber : ulong
    {
        None = 0,

        /// <summary>歯番右上1</summary>
        UpperRight1 = 0x00000000000000001ul,
        /// <summary>歯番右上2</summary>
        UpperRight2 = 0x0000000000000002ul,
        /// <summary>歯番右上3</summary>
        UpperRight3 = 0x0000000000000004ul,
        /// <summary>歯番右上4</summary>
        UpperRight4 = 0x0000000000000008ul,
        /// <summary>歯番右上5</summary>
        UpperRight5 = 0x0000000000000010ul,
        /// <summary>歯番右上<summary>
        UpperRight6 = 0x0000000000000020ul,
        /// <summary>歯番右上<summary>
        UpperRight7 = 0x0000000000000040ul,
        /// <summary>歯番右上<summary>
        UpperRight8 = 0x0000000000000080ul,
        /// <summary>歯番右上乳歯A</summary>
        UpperRightA = 0x0000000000000100ul,
        /// <summary>歯番右上乳歯B</summary>
        UpperRightB = 0x0000000000000200ul,
        /// <summary>歯番右上乳歯C</summary>
        UpperRightC = 0x0000000000000400ul,
        /// <summary>歯番右上乳歯D</summary>
        UpperRightD = 0x0000000000000800ul,
        /// <summary>歯番右上乳歯E</summary>
        UpperRightE = 0x0000000000001000ul,

        /// <summary>歯番左上1</summary>
        UpperLeft1 = 0x0000000000002000ul,
        /// <summary>歯番左上2</summary>
        UpperLeft2 = 0x0000000000004000ul,
        /// <summary>歯番左上3</summary>
        UpperLeft3 = 0x0000000000008000ul,
        /// <summary>歯番左上4</summary>
        UpperLeft4 = 0x0000000000010000ul,
        /// <summary>歯番左上5</summary>
        UpperLeft5 = 0x0000000000020000ul,
        /// <summary>歯番左上<summary>
        UpperLeft6 = 0x0000000000040000ul,
        /// <summary>歯番左上<summary>
        UpperLeft7 = 0x0000000000080000ul,
        /// <summary>歯番左上<summary>
        UpperLeft8 = 0x0000000000100000ul,
        /// <summary>歯番左上乳歯A</summary>
        UpperLeftA = 0x0000000000200000ul,
        /// <summary>歯番左上乳歯B</summary>
        UpperLeftB = 0x0000000000400000ul,
        /// <summary>歯番左上乳歯C</summary>
        UpperLeftC = 0x0000000000800000ul,
        /// <summary>歯番左上乳歯D</summary>
        UpperLeftD = 0x0000000001000000ul,
        /// <summary>歯番左上乳歯E</summary>
        UpperLeftE = 0x0000000002000000ul,
        
        /// <summary>歯番右下1</summary>
        LowerRight1 = 0x0000000004000000ul,
        /// <summary>歯番右下2</summary>
        LowerRight2 = 0x0000000008000000ul,
        /// <summary>歯番右下3</summary>
        LowerRight3 = 0x0000000010000000ul,
        /// <summary>歯番右下4</summary>
        LowerRight4 = 0x0000000020000000ul,
        /// <summary>歯番右下5</summary>
        LowerRight5 = 0x0000000040000000ul,
        /// <summary>歯番右下<summary>
        LowerRight6 = 0x0000000080000000ul,
        /// <summary>歯番右下<summary>
        LowerRight7 = 0x0000000100000000ul,
        /// <summary>歯番右下<summary>
        LowerRight8 = 0x0000000200000000ul,
        /// <summary>歯番右下乳歯A</summary>
        LowerRightA = 0x0000000400000000ul,
        /// <summary>歯番右下乳歯B</summary>
        LowerRightB = 0x0000000800000000ul,
        /// <summary>歯番右下乳歯C</summary>
        LowerRightC = 0x0000001000000000ul,
        /// <summary>歯番右下乳歯D</summary>
        LowerRightD = 0x0000002000000000ul,
        /// <summary>歯番右下乳歯E</summary>
        LowerRightE = 0x0000004000000000ul,
        
        /// <summary>歯番左下1</summary>
        LowerLeft1 = 0x0000008000000000ul,
        /// <summary>歯番左下2</summary>
        LowerLeft2 = 0x0000010000000000ul,
        /// <summary>歯番左下3</summary>
        LowerLeft3 = 0x0000020000000000ul,
        /// <summary>歯番左下4</summary>
        LowerLeft4 = 0x0000040000000000ul,
        /// <summary>歯番左下5</summary>
        LowerLeft5 = 0x0000080000000000ul,
        /// <summary>歯番左下<summary>
        LowerLeft6 = 0x0000100000000000ul,
        /// <summary>歯番左下<summary>
        LowerLeft7 = 0x0000200000000000ul,
        /// <summary>歯番左下<summary>
        LowerLeft8 = 0x0000400000000000ul,
        /// <summary>歯番左下乳歯A</summary>
        LowerLeftA = 0x0000800000000000ul,
        /// <summary>歯番左下乳歯B</summary>
        LowerLeftB = 0x0001000000000000ul,
        /// <summary>歯番左下乳歯C</summary>
        LowerLeftC = 0x0002000000000000ul,
        /// <summary>歯番左下乳歯D</summary>
        LowerLeftD = 0x0004000000000000ul,
        /// <summary>歯番左下乳歯E</summary>
        LowerLeftE = 0x0008000000000000ul,

        All = 0x000FFFFFFFFFFFFul,
    }
}
