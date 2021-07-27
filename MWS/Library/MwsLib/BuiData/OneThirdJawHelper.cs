//
// OneThirdJawHelper - enum OneThirdJawHelperに関するヘルパーメソッド・拡張メソッドを定義するクラス
//
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace MwsLib.BuiData
{
	/// <summary>
	/// enum OneThirdJawに関するヘルパーメソッド・拡張メソッドを定義するクラス
	/// 　近心→遠心かつ右上→左上→右下→左下の順での順次列挙処理やセクション区分等
	/// </summary>
	public static class OneThirdJawHelper
    {
        public static readonly OneThirdJaw[] All = 
        {
             OneThirdJaw.UpperRightMolars,
             OneThirdJaw.UpperFrontTeeth,
             OneThirdJaw.UpperLeftMolars,
             OneThirdJaw.LowerRightMolars,
             OneThirdJaw.LowerFrontTeeth,
             OneThirdJaw.LowerLeftMolars
        };

        /// <summary>
        /// OneThirdJaw値が適正な範囲内にあるかどうかをチェック
        /// </summary>
        /// <returns>true:適正 false:存在しない値</returns>
        private static bool _CheckValue(OneThirdJaw pos)
        {
            return pos >= OneThirdJaw.None && pos <= OneThirdJaw.LowerLeftMolars;
        }

        /// <summary>
        /// 最初のOneThirdJaw
        /// </summary>
        public static OneThirdJaw First
        {
            get
            {
                return OneThirdJaw.UpperRightMolars;
            }
        }

        /// <summary>
        /// 最後のOneThirdJaw
        /// </summary>
        public static OneThirdJaw Last
        {
            get
            {
                return OneThirdJaw.LowerLeftMolars;
            }
        }

        /// <summary>
        /// 次のOneThirdJaw
        /// </summary>
        /// <exception cref="ArgumentException">OneThirdJaw.Noneが指定されると発生する</exception>
        public static OneThirdJaw Next(this OneThirdJaw pos)
        {
            if (!_CheckValue(pos))
            {
                throw new ArgumentException("不正なOneThirdJaw値");
            }
            else if (pos == OneThirdJaw.None)
            {
                throw new ArgumentException("OneThirdJawとしてNoneが指定された");
            }
            else if (pos == Last)
            {
                return OneThirdJaw.None;
            }
            else
            {
                return (OneThirdJaw)((uint)pos << 1);
            }
        }

        /// <summary>
        /// 前のOneThirdJaw
        /// </summary>
        /// <exception cref="ArgumentException">OneThirdJaw.Noneが指定されると発生する</exception>
        public static OneThirdJaw Prev(this OneThirdJaw pos)
        {
            if (!_CheckValue(pos))
            {
                throw new ArgumentException("不正なOneThirdJaw値");
            }
            else if (pos == OneThirdJaw.None)
            {
                throw new ArgumentException("OneThirdJawとしてNoneが指定された");
            }
            else if (pos == First)
            {
                return OneThirdJaw.None;
            }
            else
            {
                return (OneThirdJaw)((uint)pos >> 1);
            }
        }

        /// <summary>
        /// 上顎かどうかどうかを返す
        /// </summary>
        /// <param name="position">位置</param>
        /// <returns>上顎の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">OneThirdJaw.Noneが指定されると発生する</exception>
        public static bool IsUpper(this OneThirdJaw position)
        {
            if (position == OneThirdJaw.None)
            {
                throw new ArgumentException("OneThirdJawとしてNoneが指定された");
            }
            if (OneThirdJaw.UpperRightMolars <= position && position <= OneThirdJaw.UpperLeftMolars)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 下顎かどうかどうかを返す
        /// </summary>
        /// <param name="position">判定対象の歯番</param>
        /// <returns>下顎の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">OneThirdJaw.Noneが指定されると発生する</exception>
        public static bool IsLower(this OneThirdJaw position)
        {
            if (position == OneThirdJaw.None)
            {
                throw new ArgumentException("OneThirdJawとしてNoneが指定された");
            }
            if (OneThirdJaw.LowerRightMolars <= position && position <= OneThirdJaw.LowerLeftMolars)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 顎の位置を取得
        /// </summary>
        /// <param name="position">位置</param>
        /// <returns>上下額区分</returns>
        /// <exception cref="ArgumentException">OneThirdJaw.Noneが指定されると発生する</exception>
        public static Jaw GetJaw(this OneThirdJaw position)
        {
            if (position == OneThirdJaw.None)
            {
                throw new ArgumentException("OneThirdJawとしてNoneが指定された");
            }
            // 上顎
            if (position.IsUpper())
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
        /// 指定したOneThirdJaw値から、序数を求める。
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int ToSerialNumber(this OneThirdJaw position)
        {
            if (typeof(OneThirdJaw).IsEnumDefined(position))
            {
                // 2を何乗したらpositionのビット値になるかを求める事でビット位置を表す序数を求める
                // (2のゼロ乗は1, 2の１乗は2, 2の２乗は4, 2の31乗は0x80000000)
                return (int)System.Math.Round(System.Math.Log((double)position, 2.0));
            }
            else
            {
                throw new ArgumentException(string.Format("OneThirdJawに存在しない値[{0}]が指定された。", (uint)position));
            }
        }
    }
}
