//
// HalfJawHelper - enum HalfJawに関するヘルパーメソッド・拡張メソッドを定義するクラス
//
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace CommonLib.BuiData
{
	/// <summary>
	/// enum HalfJawに関するヘルパーメソッド・拡張メソッドを定義するクラス
	/// 　近心→遠心かつ右上→左上→右下→左下の順での順次列挙処理やセクション区分等
	/// </summary>
	public static class HalfJawHelper
    {
        public static readonly HalfJaw[] All = 
        {
             HalfJaw.UpperRight,
             HalfJaw.UpperLeft,
             HalfJaw.LowerRight,
             HalfJaw.LowerLeft
        };

        /// <summary>
        /// HalfJaw値が適正な範囲内にあるかどうかをチェック
        /// </summary>
        /// <returns>true:適正 false:存在しない値</returns>
        private static bool _CheckValue(HalfJaw pos)
        {
            return pos >= HalfJaw.None && pos <= HalfJaw.LowerLeft;
        }

        /// <summary>
        /// 最初のHalfJaw
        /// </summary>
        public static HalfJaw First
        {
            get
            {
                return HalfJaw.UpperRight;
            }
        }

        /// <summary>
        /// 最後のHalfJaw
        /// </summary>
        public static HalfJaw Last
        {
            get
            {
                return HalfJaw.LowerLeft;
            }
        }

        /// <summary>
        /// 次のHalfJaw
        /// </summary>
        /// <exception cref="ArgumentException">HalfJaw.Noneが指定されると発生する</exception>
        public static HalfJaw Next(this HalfJaw pos)
        {
            if (!_CheckValue(pos))
            {
                throw new ArgumentException("不正なHalfJaw値");
            }
            else if (pos == HalfJaw.None)
            {
                throw new ArgumentException("HalfJawとしてNoneが指定された");
            }
            else if (pos == Last)
            {
                return HalfJaw.None;
            }
            else
            {
                return (HalfJaw)((uint)pos << 1);
            }
        }

        /// <summary>
        /// 前のHalfJaw
        /// </summary>
        /// <exception cref="ArgumentException">HalfJaw.Noneが指定されると発生する</exception>
        public static HalfJaw Prev(this HalfJaw pos)
        {
            if (!_CheckValue(pos))
            {
                throw new ArgumentException("不正なHalfJaw値");
            }
            else if (pos == HalfJaw.None)
            {
                throw new ArgumentException("HalfJawとしてNoneが指定された");
            }
            else if (pos == First)
            {
                return HalfJaw.None;
            }
            else
            {
                return (HalfJaw)((uint)pos >> 1);
            }
        }

        /// <summary>
        /// 上顎かどうかどうかを返す
        /// </summary>
        /// <param name="position">位置</param>
        /// <returns>上顎の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsUpper(this HalfJaw position)
        {
            return HalfJaw.UpperRight == position || position == HalfJaw.UpperLeft;
        }

        /// <summary>
        /// 下顎かどうかどうかを返す
        /// </summary>
        /// <param name="position">判定対象の歯番</param>
        /// <returns>下顎の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsLower(this HalfJaw position)
        {
            return HalfJaw.LowerRight == position || position == HalfJaw.LowerLeft;
        }

        /// <summary>
        /// 右側かどうかどうかを返す
        /// </summary>
        /// <param name="position">位置</param>
        /// <returns>右側の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsRight(this HalfJaw position)
        {
            return HalfJaw.UpperRight == position || position == HalfJaw.LowerRight;
        }

        /// <summary>
        /// 左側かどうかどうかを返す
        /// </summary>
        /// <param name="position">位置</param>
        /// <returns>左側の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsLeft(this HalfJaw position)
        {
            return HalfJaw.UpperLeft == position || position == HalfJaw.LowerLeft;
        }


        /// <summary>
        /// 上下額区分を取得
        /// </summary>
        /// <param name="position">位置</param>
        /// <returns>上下額区分</returns>
        /// <exception cref="ArgumentException">HalfJaw.Noneが指定されると発生する</exception>
        public static Jaw GetJaw(this HalfJaw position)
        {
            if (position == HalfJaw.None)
            {
                throw new ArgumentException("HalfJawとしてNoneが指定された");
            }
            // 上顎
            else if (position.IsUpper())
            {
                return Jaw.UpperJaw;
            }
            // 下顎
            else if (position.IsLower())
            {
                return Jaw.LowerJaw;
            }
            else
            {
                throw new ApplicationException("上下顎の判定に失敗");
            }
        }

        /// <summary>
        /// 指定したHalfJaw値から、序数を求める。
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int ToSerialNumber(this HalfJaw position)
        {
            if (typeof(HalfJaw).IsEnumDefined(position))
            {
                // 2を何乗したらpositionのビット値になるかを求める事でビット位置を表す序数を求める
                // (2のゼロ乗は1, 2の１乗は2, 2の２乗は4, 2の31乗は0x80000000)
                return (int)System.Math.Round(System.Math.Log((double)position, 2.0));
            }
            else
            {
                throw new ArgumentException(string.Format("HalfJawに存在しない値[{0}]が指定された。", (uint)position));
            }
        }
    }
}
