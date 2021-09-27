//
// ToothAttributeHelper - enum ToothAttributeに関するヘルパーメソッド・拡張メソッドを定義するクラス
//
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace CommonLib.BuiData
{
	/// <summary>
	/// enum ToothAttributeに関するヘルパーメソッド・拡張メソッドを定義するクラス
	/// 　近心→遠心かつ右上→左上→右下→左下の順での順次列挙処理やセクション区分等
	/// </summary>
	public static class ToothAttributeHelper
    {
        public static readonly ToothAttribute[] All = 
        {
            ToothAttribute.NormalNumber,
            ToothAttribute.Shidaishi,
            ToothAttribute.KenzenShidaishi,
            ToothAttribute.Geki,
            ToothAttribute.Kajoshi,
            ToothAttribute.EtcKessonshi,
            ToothAttribute.EtcBunkatsushi,
            ToothAttribute.EtcZoshi,
            ToothAttribute.EtcKohason,
            ToothAttribute.EtcDummyOnZankon
        };

        /// <summary>
        /// 指定されたToothAttribute値が単独のToothAttribute値を示しているかどうかをチェックする
        /// </summary>
        /// <returns>true:単独の値 false:None、存在しない値、または、複数の値の組み合わせ。</returns>
        public static bool IsSingle(this ToothAttribute attribute)
        {
            // $パフォーマンス改善$
            return ((uint)attribute).BitCount() == 1 && attribute >= ToothAttribute.NormalNumber && attribute <= ToothAttribute.EtcDummyOnZankon;
        }

        /// <summary>
        /// 最初のToothAttribute
        /// </summary>
        public static ToothAttribute First
        {
            get
            {
                return ToothAttribute.NormalNumber;
            }
        }

        /// <summary>
        /// 最後のToothAttribute
        /// </summary>
        public static ToothAttribute Last
        {
            get
            {
                return ToothAttribute.EtcDummyOnZankon;
            }
        }

        /// <summary>
        /// 次のToothAttribute
        /// </summary>
        /// <exception cref="ArgumentException">ToothAttribute.Noneが指定されると発生する</exception>
        public static ToothAttribute Next(this ToothAttribute attribute)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!attribute.IsSingle())
            {
                throw new ArgumentException("単独のToothAttribute値ではないので演算できない");
            }

#endif
            if (attribute == Last)
            {
                return ToothAttribute.None;
            }
            else
            {
                return (ToothAttribute)((uint)attribute << 1);
            }
        }

        /// <summary>
        /// 前のToothAttribute
        /// </summary>
        /// <exception cref="ArgumentException">ToothAttribute.Noneが指定されると発生する</exception>
        public static ToothAttribute Prev(this ToothAttribute attribute)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!attribute.IsSingle())
            {
                throw new ArgumentException("単独のToothAttribute値ではないので演算できない");
            }

#endif
            if (attribute == First)
            {
                return ToothAttribute.None;
            }
            else
            {
                return (ToothAttribute)((uint)attribute >> 1);
            }
        }

        /// <summary>
        /// 指定したToothAttribute値から、序数を求める。
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static int ToSerialNumber(this ToothAttribute attribute)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!attribute.IsSingle())
            {
                throw new ArgumentException("単独のToothAttribute値ではないので演算できない");
            }

#endif
            // 2を何乗したらpositionの値になるかを求める事でビット位置を表す序数を求める
            // (2のゼロ乗は1, 2の１乗は2, 2の２乗は4, 2の31乗は0x80000000)
            return (int)System.Math.Round(System.Math.Log((double)attribute, 2.0));
        }
    }
}
