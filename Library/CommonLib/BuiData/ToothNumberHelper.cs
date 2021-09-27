//
// enum ToothNumberHelper - 歯番の定義(１番と乳歯Ａを区別する)
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.BuiData
{
	/// <summary>
	/// enum ToothPositionに関するヘルパーメソッド・拡張メソッドを定義するクラス
	/// 　近心→遠心かつ右上→左上→右下→左下の順での順次列挙処理やセクション区分等
	/// </summary>
	public static class ToothNumberHelper
    {
        // $パフォーマンス改善$
        public static ToothNumber[] AllValue;

        /// <summary>
        /// 全ToothNumberの昇順配列
        /// </summary>
        public static ToothNumber[] All
        {
            get
            {
                if (AllValue == null)
                {
                    AllValue = new ToothNumber[] 
                    {
                        ToothNumber.UpperRight1,
                        ToothNumber.UpperRight2,
                        ToothNumber.UpperRight3,        
                        ToothNumber.UpperRight4,
                        ToothNumber.UpperRight5,
                        ToothNumber.UpperRight6,
                        ToothNumber.UpperRight7,
                        ToothNumber.UpperRight8,

                        ToothNumber.UpperRightA,
                        ToothNumber.UpperRightB,
                        ToothNumber.UpperRightC,
                        ToothNumber.UpperRightD,
                        ToothNumber.UpperRightE,

                        ToothNumber.UpperLeft1,
                        ToothNumber.UpperLeft2,
                        ToothNumber.UpperLeft3,
                        ToothNumber.UpperLeft4,
                        ToothNumber.UpperLeft5,
                        ToothNumber.UpperLeft6,
                        ToothNumber.UpperLeft7,
                        ToothNumber.UpperLeft8,

                        ToothNumber.UpperLeftA,
                        ToothNumber.UpperLeftB,
                        ToothNumber.UpperLeftC,
                        ToothNumber.UpperLeftD,
                        ToothNumber.UpperLeftE,

                        ToothNumber.LowerRight1,
                        ToothNumber.LowerRight2,
                        ToothNumber.LowerRight3,
                        ToothNumber.LowerRight4,
                        ToothNumber.LowerRight5,
                        ToothNumber.LowerRight6,
                        ToothNumber.LowerRight7,
                        ToothNumber.LowerRight8,

                        ToothNumber.LowerRightA,
                        ToothNumber.LowerRightB,
                        ToothNumber.LowerRightC,
                        ToothNumber.LowerRightD,
                        ToothNumber.LowerRightE,

                        ToothNumber.LowerLeft1,
                        ToothNumber.LowerLeft2,
                        ToothNumber.LowerLeft3,
                        ToothNumber.LowerLeft4,
                        ToothNumber.LowerLeft5,
                        ToothNumber.LowerLeft6,
                        ToothNumber.LowerLeft7,
                        ToothNumber.LowerLeft8,

                        ToothNumber.LowerLeftA,
                        ToothNumber.LowerLeftB,
                        ToothNumber.LowerLeftC,
                        ToothNumber.LowerLeftD,
                        ToothNumber.LowerLeftE,
                    };
                }
                return AllValue;
            }
        }

        public class ToothPositionAndEikyushiNyushi : IEquatable<ToothPositionAndEikyushiNyushi>
        {
            public ToothPosition Position { get; set; }
            public EikyushiNyushiType EikyushiNyushi { get; set; }

            public bool Equals(ToothPositionAndEikyushiNyushi other)
            {
                return Position == other.Position && EikyushiNyushi == other.EikyushiNyushi;
            }

            public override bool Equals(object other)
            {
                var val = other as ToothPositionAndEikyushiNyushi;
                if (val != null)
                {
                    return Equals(val);
                }
                else
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                return Position.GetHashCode() ^ EikyushiNyushi.GetHashCode();
            }


        }

        // $パフォーマンス改善$
        private static KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>[] ToothNumerToPositionTableValue;

        private static KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>[] ToothNumerToPositionTable
        {
            get
            {
                if (ToothNumerToPositionTableValue == null)
                {
                    ToothNumerToPositionTableValue = new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>[]
                    {
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRight1, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight1, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRight2, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight2, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRight3, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight3, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRight4, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight4, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRight5, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight5, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRight6, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight6, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRight7, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight7, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRight8, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight8, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),

                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRightA, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight1, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRightB, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight2, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRightC, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight3, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRightD, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight4, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperRightE, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperRight5, EikyushiNyushi = EikyushiNyushiType.Nyushi}),

                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeft1, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft1, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeft2, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft2, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeft3, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft3, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeft4, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft4, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeft5, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft5, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeft6, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft6, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeft7, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft7, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeft8, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft8, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),

                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeftA, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft1, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeftB, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft2, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeftC, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft3, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeftD, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft4, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.UpperLeftE, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.UpperLeft5, EikyushiNyushi = EikyushiNyushiType.Nyushi}),

                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRight1, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight1, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRight2, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight2, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRight3, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight3, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRight4, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight4, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRight5, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight5, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRight6, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight6, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRight7, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight7, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRight8, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight8, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),

                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRightA, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight1, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRightB, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight2, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRightC, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight3, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRightD, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight4, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerRightE, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerRight5, EikyushiNyushi = EikyushiNyushiType.Nyushi}),

                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeft1, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft1, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeft2, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft2, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeft3, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft3, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeft4, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft4, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeft5, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft5, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeft6, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft6, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeft7, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft7, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeft8, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft8, EikyushiNyushi = EikyushiNyushiType.Eikyushi}),

                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeftA, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft1, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeftB, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft2, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeftC, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft3, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeftD, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft4, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                        new KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>(ToothNumber.LowerLeftE, new ToothPositionAndEikyushiNyushi{ Position = ToothPosition.LowerLeft5, EikyushiNyushi = EikyushiNyushiType.Nyushi}),
                    };
                }
                return ToothNumerToPositionTableValue;
            }
        }

        // $パフォーマンス改善$
        private static Dictionary<ToothNumber, ToothPositionAndEikyushiNyushi> ToothNumberToPositionDictionaryValue;

        private static Dictionary<ToothNumber, ToothPositionAndEikyushiNyushi> ToothNumberToPositionDictionary
        {
            get
            {
                if (ToothNumberToPositionDictionaryValue == null)
                {
                    ToothNumberToPositionDictionaryValue = ToothNumerToPositionTable.ToDictionary(
                        new Func<KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>, ToothNumber>(x => x.Key),
                        new Func<KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>, ToothPositionAndEikyushiNyushi>(x => x.Value)
                        );
                }
                return ToothNumberToPositionDictionaryValue;
            }
        }

        // $パフォーマンス改善$
        private static Dictionary<ToothPositionAndEikyushiNyushi, ToothNumber> PositionToToothNumberDictionaryValue;

        private static Dictionary<ToothPositionAndEikyushiNyushi, ToothNumber> PositionToToothNumberDictionary
        {
            get
            {
                if (PositionToToothNumberDictionaryValue == null)
                {
                    PositionToToothNumberDictionaryValue = ToothNumerToPositionTable.ToDictionary(
                        new Func<KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>, ToothPositionAndEikyushiNyushi>(x => x.Value),
                        new Func<KeyValuePair<ToothNumber, ToothPositionAndEikyushiNyushi>, ToothNumber>(x => x.Key));
                }
                return PositionToToothNumberDictionaryValue;
            }
        }


        // $パフォーマンス改善$
        private static Dictionary<long, ToothPosition> ToothNumerToPositionListValue;

        /// <summary>
        /// ToothNumberとToothPositionの関連リスト
        /// 速度向上のため、ToothNumberToPositionDictionaryとは別にキーを数値で保持
        /// </summary>
        private static Dictionary<long, ToothPosition> ToothNumerToPositionList
        {
            get
            {
                if (ToothNumerToPositionListValue == null)
                {
                    ToothNumerToPositionListValue = new Dictionary<long, ToothPosition>(52)
                    {
                        {(long)ToothNumber.UpperRight1, ToothPosition.UpperRight1},
                        {(long)ToothNumber.UpperRight2, ToothPosition.UpperRight2},
                        {(long)ToothNumber.UpperRight3, ToothPosition.UpperRight3},
                        {(long)ToothNumber.UpperRight4, ToothPosition.UpperRight4},
                        {(long)ToothNumber.UpperRight5, ToothPosition.UpperRight5},
                        {(long)ToothNumber.UpperRight6, ToothPosition.UpperRight6},
                        {(long)ToothNumber.UpperRight7, ToothPosition.UpperRight7},
                        {(long)ToothNumber.UpperRight8, ToothPosition.UpperRight8},

                        {(long)ToothNumber.UpperRightA, ToothPosition.UpperRight1},
                        {(long)ToothNumber.UpperRightB, ToothPosition.UpperRight2},
                        {(long)ToothNumber.UpperRightC, ToothPosition.UpperRight3},
                        {(long)ToothNumber.UpperRightD, ToothPosition.UpperRight4},
                        {(long)ToothNumber.UpperRightE, ToothPosition.UpperRight5},

                        {(long)ToothNumber.UpperLeft1, ToothPosition.UpperLeft1},
                        {(long)ToothNumber.UpperLeft2, ToothPosition.UpperLeft2},
                        {(long)ToothNumber.UpperLeft3, ToothPosition.UpperLeft3},
                        {(long)ToothNumber.UpperLeft4, ToothPosition.UpperLeft4},
                        {(long)ToothNumber.UpperLeft5, ToothPosition.UpperLeft5},
                        {(long)ToothNumber.UpperLeft6, ToothPosition.UpperLeft6},
                        {(long)ToothNumber.UpperLeft7, ToothPosition.UpperLeft7},
                        {(long)ToothNumber.UpperLeft8, ToothPosition.UpperLeft8},

                        {(long)ToothNumber.UpperLeftA, ToothPosition.UpperLeft1},
                        {(long)ToothNumber.UpperLeftB, ToothPosition.UpperLeft2},
                        {(long)ToothNumber.UpperLeftC, ToothPosition.UpperLeft3},
                        {(long)ToothNumber.UpperLeftD, ToothPosition.UpperLeft4},
                        {(long)ToothNumber.UpperLeftE, ToothPosition.UpperLeft5},

                        {(long)ToothNumber.LowerRight1, ToothPosition.LowerRight1},
                        {(long)ToothNumber.LowerRight2, ToothPosition.LowerRight2},
                        {(long)ToothNumber.LowerRight3, ToothPosition.LowerRight3},
                        {(long)ToothNumber.LowerRight4, ToothPosition.LowerRight4},
                        {(long)ToothNumber.LowerRight5, ToothPosition.LowerRight5},
                        {(long)ToothNumber.LowerRight6, ToothPosition.LowerRight6},
                        {(long)ToothNumber.LowerRight7, ToothPosition.LowerRight7},
                        {(long)ToothNumber.LowerRight8, ToothPosition.LowerRight8},

                        {(long)ToothNumber.LowerRightA, ToothPosition.LowerRight1},
                        {(long)ToothNumber.LowerRightB, ToothPosition.LowerRight2},
                        {(long)ToothNumber.LowerRightC, ToothPosition.LowerRight3},
                        {(long)ToothNumber.LowerRightD, ToothPosition.LowerRight4},
                        {(long)ToothNumber.LowerRightE, ToothPosition.LowerRight5},

                        {(long)ToothNumber.LowerLeft1, ToothPosition.LowerLeft1},
                        {(long)ToothNumber.LowerLeft2, ToothPosition.LowerLeft2},
                        {(long)ToothNumber.LowerLeft3, ToothPosition.LowerLeft3},
                        {(long)ToothNumber.LowerLeft4, ToothPosition.LowerLeft4},
                        {(long)ToothNumber.LowerLeft5, ToothPosition.LowerLeft5},
                        {(long)ToothNumber.LowerLeft6, ToothPosition.LowerLeft6},
                        {(long)ToothNumber.LowerLeft7, ToothPosition.LowerLeft7},
                        {(long)ToothNumber.LowerLeft8, ToothPosition.LowerLeft8},

                        {(long)ToothNumber.LowerLeftA, ToothPosition.LowerLeft1},
                        {(long)ToothNumber.LowerLeftB, ToothPosition.LowerLeft2},
                        {(long)ToothNumber.LowerLeftC, ToothPosition.LowerLeft3},
                        {(long)ToothNumber.LowerLeftD, ToothPosition.LowerLeft4},
                        {(long)ToothNumber.LowerLeftE, ToothPosition.LowerLeft5},
                    };
                }
                return ToothNumerToPositionListValue;
            }
        }


        // $パフォーマンス改善$
        /// <summary>
        /// 指定されたToothNumber値が単独のToothNumber値を示しているかどうかをチェックする
        /// </summary>
        /// <returns>true:単独の値 false:None、存在しない値、または、複数の値の組み合わせ。</returns>
        public static bool IsSingle(this ToothNumber number)
        {
            return ((ulong)number).BitCount() == 1 && number >= ToothNumber.UpperRight1 && number <= ToothNumber.LowerLeftE;
        }

        /// <summary>
        /// 歯の位置と永久歯・乳歯区分から、ToothNumberを取得する。
        /// </summary>
        public static ToothNumber GetNumber(ToothPosition position, EikyushiNyushiType eikyushiNyushi)
        {
            var key = new ToothPositionAndEikyushiNyushi { Position = position, EikyushiNyushi = eikyushiNyushi };
            return PositionToToothNumberDictionary[key];
        }

        /// <summary>
        /// 上下左右区分、永久歯・乳歯区分、区分内での位置から、ToothNumber値を取得する。
        /// </summary>
        public static ToothNumber GetNumber(HalfJaw halfJaw, EikyushiNyushiType en, int n)
        {
            if (n < 0 || n >= 8)
            {
                throw new ArgumentException();
            }

            ToothNumber org;
            if (halfJaw == HalfJaw.UpperRight)
            {
                org = ToothNumber.UpperRight1;
            }
            else if (halfJaw == HalfJaw.UpperLeft)
            {
                org = ToothNumber.UpperLeft1;
            }
            else if (halfJaw == HalfJaw.LowerRight)
            {
                org = ToothNumber.LowerRight1;
            }
            else if (halfJaw == HalfJaw.LowerLeft)
            {
                org = ToothNumber.LowerLeft1;
            }
            else
            {
                throw new ArgumentException();
            }

            if (en == EikyushiNyushiType.Nyushi)
            {
                org = (ToothNumber)(((ulong)org) << 8);
                if (n >= 5)
                {
                    throw new ArgumentException();
                }
            }

            return (ToothNumber)((ulong)org << n);
        }

        /// <summary>
        /// 歯番名称の取得<br/>
        /// （例）右上1番
        /// </summary>
        /// <param name="number">歯番</param>
        /// <returns>歯番名称</returns>
        public static string GetToothNumberName(this ToothNumber number)
        {
            StringBuilder builder = new StringBuilder();

            // 左右文字
            if (number.IsRight())
            {
                // 右
                builder.Append("右");
            }
            else
            {
                // 左
                builder.Append("左");
            }

            // 上下顎文字
            if (number.IsUpper())
            {
                // 上顎
                builder.Append("上");
            }
            else
            {
                // 下顎
                builder.Append("下");
            }

            // 番号
            if (number.IsEikyushi())
            {
                // 永久歯
                var query = from x in Tooth.EIKYUSHI_TABLE where x.Position == number.GetPosition() select x.Str;
                builder.Append(query.Single());
            }

            else
            {
                // 乳歯
                var query = from x in Tooth.NYUSHI_TABLE where x.Position == number.GetPosition() select x.Str;
                builder.Append(query.Single());
            }
            builder.Append("番");

            return builder.ToString();
        }

        /// <summary>
        /// ToothNumberから歯の位置を取得する
        /// </summary>
        public static ToothPosition GetPosition(this ToothNumber number)
        {
            // 速度向上のため、キーを数値にしたリストから検索
            return ToothNumerToPositionList[(long)number];
        }

        /// <summary>
        /// ToothNumberから永久歯・乳歯区分を取得する
        /// </summary>
        public static EikyushiNyushiType GetEikyushiNyushi(this ToothNumber number)
        {
            return ToothNumberToPositionDictionary[number].EikyushiNyushi;
        }

        public static bool IsEikyushi(this ToothNumber number)
        {
            return GetEikyushiNyushi(number) == EikyushiNyushiType.Eikyushi;
        }

        public static bool IsNyushi(this ToothNumber number)
        {
            return GetEikyushiNyushi(number) == EikyushiNyushiType.Nyushi;
        }

        /// <summary>
        /// ToothNumberに含まれるフラグ(ToothNumber)の列挙を取得する
        /// </summary>
        public static IEnumerable<ToothNumber> GetFlags(this ToothNumber numbers)
        {
            foreach (var number in All)
            {
                if (numbers.HasFlag(number))
                {
                    yield return number;
                }
            }
        }

        /// <summary>
        /// 最初のToothNumber
        /// </summary>
        public static ToothNumber First
        {
            get
            {
                return ToothNumber.UpperRight1;
            }
        }

        /// <summary>
        /// 最後のToothNumber
        /// </summary>
        public static ToothNumber Last
        {
            get
            {
                return ToothNumber.LowerLeftE;
            }
        }

        /// <summary>
        /// 次のToothNumberを取得
        /// </summary>
        /// <param name="number">単独のToothNumber値を指定</param>
        /// <returns>次のToothNumber値。次のToothNumber値が存在しない場合はToothNumber.None</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static ToothNumber Next(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            if (number == Last)
            {
                return ToothNumber.None;
            }
            else
            {
                return (ToothNumber)((ulong)number << 1);
            }
        }

        /// <summary>
        /// 前のToothNumberを取得
        /// </summary>
        /// <param name="number">単独のToothNumber値を指定</param>
        /// <returns>前のToothNumber値。前のToothNumber値が存在しない場合はToothNumber.None</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static ToothNumber Prev(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            if (number == First)
            {
                return ToothNumber.None;
            }
            else
            {
                return (ToothNumber)((ulong)number >> 1);
            }
        }

        /// <summary>
        /// 上顎かどうかどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値を指定</param>
        /// <returns>上顎の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsUpper(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsUpper();
        }

        /// <summary>
        /// 下顎かどうかどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値を指定</param>
        /// <returns>下顎の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsLower(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsLower();
        }

        /// 右側かどうかどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値を指定</param>
        /// <returns>右側の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsRight(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsRight();
        }

        /// 左側かどうかどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値を指定</param>
        /// <returns>左側の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsLeft(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsLeft();
        }

        /// 上顎右側かどうかどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上顎右側の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsUpperRight(this ToothNumber number)
        {
            return number.IsUpper() && number.IsRight();
        }

        /// 上顎左側かどうかどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上顎左側の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsUpperLeft(this ToothNumber number)
        {
            return number.IsUpper() && number.IsLeft();
        }

        /// 下顎右側かどうかどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>下顎右側の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsLowerRight(this ToothNumber number)
        {
            return number.IsLower() && number.IsRight();
        }

        /// 下顎左側かどうかどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>下顎左側の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsLowerLeft(this ToothNumber number)
        {
            return number.IsLower() && number.IsLeft();
        }

        /// 上顎右側臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上顎右側臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsUpperRightMolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsUpperRightMolar();
        }

        /// 上顎前歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上顎前歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsUpperFrontTooth(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsUpperFrontTooth();
        }

        /// 上顎左側臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上顎左側臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsUpperLeftMolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsUpperLeftMolar();
        }

        /// 下顎右側臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>下顎右側臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsLowerRightMolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsLowerRightMolar();
        }

        /// 下顎前歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>下顎前歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsLowerFrontTooth(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsLowerFrontTooth();
        }

        /// 下顎左側臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>下顎左側臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsLowerLeftMolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsLowerLeftMolar();
        }

        /// 上顎右側小臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上顎右側小臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsUpperRightPremolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsUpperRightPremolar();
        }

        /// 上顎左側小臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上顎左側小臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsUpperLeftPremolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsUpperLeftPremolar();
        }

        /// 下顎右側小臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>下顎右側小臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsLowerRightPremolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsLowerRightPremolar();
        }

        /// 下顎左側小臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>下顎左側小臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsLowerLeftPremolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsLowerLeftPremolar();
        }

        /// 上顎右側大臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上顎右側大臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsUpperRightTrueMolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsUpperRightTrueMolar();
        }

        /// 上顎左側大臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上顎左側大臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsUpperLeftTrueMolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsUpperLeftTrueMolar();
        }

        /// 下顎右側大臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>下顎右側大臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsLowerRightTrueMolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsLowerRightTrueMolar();
        }

        /// 下顎左側大臼歯かどうかを返す
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>下顎左側大臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        public static bool IsLowerLeftTrueMolar(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().IsLowerLeftTrueMolar();
        }

        /// 顎の位置を取得
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上下額区分</returns>
        /// <exception cref="ArgumentException">単独のToothNumber値ではないので演算できない</exception>
        /// <exception cref="ApplicationException">上下顎の判定に失敗</exception>
        public static Jaw GetJaw(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().GetJaw();
        }

        /// 1/3額単位の位置を取得
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>1／3顎単位の区分</returns>
        /// <exception cref="ApplicationException">1／3顎の判定に失敗</exception>
        public static OneThirdJaw GetOneThirdJaw(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().GetOneThirdJaw();
        }

        /// 上下顎右左側区分の判定
        /// </summary>
        /// <param name="number">単独のToothNumber値</param>
        /// <returns>上下顎それぞれの右側と左側の区分</returns>
        /// <exception cref="ApplicationException">上下顎右左側区分の判定に失敗</exception>
        public static HalfJaw GetHalfJaw(this ToothNumber number)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(number))
            {
                throw new ArgumentException("単独のToothNumber値ではないので演算できない");
            }
#endif
            return number.GetPosition().GetHalfJaw();
        }

        /// <summary>
        /// 指定したToothNumber値から、序数を求める。
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int ToSerialNumber(this ToothNumber number)
        {
            if (typeof(ToothNumber).IsEnumDefined(number))
            {
                // 2を何乗したらpositionのビット値になるかを求める事でビット位置を表す序数を求める
                // ToothNumber.(2のゼロ乗は1, ToothNumber.2の１乗は2, ToothNumber.2の２乗は4, 2の31乗は0x80000000)
                // (数値演算メソッドを使用する事で、プロセッサ１ステップで結果が求められる事を期待)
                return (int)System.Math.Round(System.Math.Log((double)number, 2.0));
            }
            else
            {
                throw new ArgumentException(string.Format("ToothNumberに存在しない値[{0}]が指定された。", (uint)number));
            }
        }

        /// <summary>
        /// 指定した歯番の遠心側の歯番を取得
        /// </summary>
        /// <returns>遠心側の歯番。遠心側に歯番が無い場合はToothNumber.None</returns>
        public static ToothNumber GetOutsideNeighbor(this ToothNumber number)
        {
            if (number == ToothNumber.UpperRight8 || number == ToothNumber.UpperLeft8 || number == ToothNumber.LowerRight8 || number == ToothNumber.LowerLeft8
                || number == ToothNumber.UpperRightE || number == ToothNumber.UpperLeftE || number == ToothNumber.LowerRightE || number == ToothNumber.LowerLeftE)
            {
                return ToothNumber.None;
            }
            else
            {
                return number.Next();
            }
        }

        /// <summary>
        /// 指定した歯番の近心側の歯番を取得
        /// </summary>
        /// <returns>近心側の歯番</returns>
        public static ToothNumber GetInsideNeighbor(this ToothNumber number)
        {
            if (number == ToothNumber.UpperRight1)
            {
                return ToothNumber.UpperLeft1;
            }
            else if (number == ToothNumber.UpperLeft1)
            {
                return ToothNumber.UpperRight1;
            }
            else if (number == ToothNumber.LowerRight1)
            {
                return ToothNumber.LowerLeft1;
            }
            else if (number == ToothNumber.LowerLeft1)
            {
                return ToothNumber.LowerRight1;
            }
            else if (number == ToothNumber.UpperRightA)
            {
                return ToothNumber.UpperLeftA;
            }
            else if (number == ToothNumber.UpperLeftA)
            {
                return ToothNumber.UpperRightA;
            }
            else if (number == ToothNumber.LowerRightA)
            {
                return ToothNumber.LowerLeftA;
            }
            else if (number == ToothNumber.LowerLeftA)
            {
                return ToothNumber.LowerRightA;
            }
            else
            {
                return number.Prev();
            }
        }

        /// <summary>
        /// ToothNumber
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static ToothNumber ToFlags(this IEnumerable<ToothNumber> numbers)
        {
            ToothNumber result = ToothNumber.None;
            foreach (var number in numbers)
            {
                result |= number;
            }
            return result;
        }

        // ブロック内での0～7のインデックスを取得
        public static int GetInBlockIndex(this ToothNumber number)
        {
            return number.GetPosition().GetInBlockIndex();
        }

        /// <summary>
        /// flagsから個々のToothPositionを列挙
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static IEnumerable<ToothNumber> Separate(this ToothNumber flags)
        {
            for (ToothNumber p = ToothNumber.UpperRight1; p != ToothNumber.None; p = p.Next())
            {
                if (flags.HasFlag(p))
                {
                    yield return p;
                }
            }
        }

        public static int ToFDI(this ToothNumber number)
        {
            HalfJaw block = number.GetHalfJaw();
            int inBlockIndex = number.GetInBlockIndex();
            if (number.IsEikyushi())
            {
                switch (block)
                {
                    case HalfJaw.UpperRight:
                        return 11 + inBlockIndex;
                    case HalfJaw.UpperLeft:
                        return 21 + inBlockIndex;
                    case HalfJaw.LowerRight:
                        return 41 + inBlockIndex;
                    case HalfJaw.LowerLeft:
                        return 31 + inBlockIndex;
                    default:
                        throw new NotSupportedException();
                }
            }
            else if (number.IsNyushi())
            {
                switch (block)
                {
                    case HalfJaw.UpperRight:
                        return 51 + inBlockIndex;
                    case HalfJaw.UpperLeft:
                        return 61 + inBlockIndex;
                    case HalfJaw.LowerRight:
                        return 81 + inBlockIndex;
                    case HalfJaw.LowerLeft:
                        return 71 + inBlockIndex;
                    default:
                        throw new NotSupportedException();
                }
            }else{
                throw new NotSupportedException();
            }
        }

        public static ToothNumber FDIToToothNumber(this int fdi)
        {
            int n1 = fdi / 10;
            int n2 = fdi % 10;
            if ((n1 >= 1 && n1 <= 8 && n2 >= 1 && n2 <= 8))
            {
                ToothNumber sp;
                switch (n1)
                {
                    case 1:
                        sp = ToothNumber.UpperRight1;
                        break;
                    case 2:
                        sp = ToothNumber.UpperLeft1;
                        break;
                    case 3:
                        sp = ToothNumber.LowerLeft1;
                        break;
                    case 4:
                        sp = ToothNumber.LowerRight1;
                        break;
                    case 5:
                        sp = ToothNumber.UpperRightA;
                        break;
                    case 6:
                        sp = ToothNumber.UpperLeftA;
                        break;
                    case 7:
                        sp = ToothNumber.LowerLeftA;
                        break;
                    case 8:
                        sp = ToothNumber.LowerRightA;
                        break;
                    default:
                        throw new NotSupportedException();
                }

                return (ToothNumber)((Int64)sp << (n2 - 1));
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("指定された値({0})はFDI値の範囲外", fdi));
            }
        }

        public static ToothNumber AllEikyushi
        {
            get
            {
                return
                       ToothNumber.UpperRight1 | ToothNumber.UpperRight2 | ToothNumber.UpperRight3
                    | ToothNumber.UpperRight4 | ToothNumber.UpperRight5 | ToothNumber.UpperRight6 | ToothNumber.UpperRight7 | ToothNumber.UpperRight8
                    | ToothNumber.UpperLeft1 | ToothNumber.UpperLeft2 | ToothNumber.UpperLeft3
                    | ToothNumber.UpperLeft4 | ToothNumber.UpperLeft5 | ToothNumber.UpperLeft6 | ToothNumber.UpperLeft7 | ToothNumber.UpperLeft8
                    | ToothNumber.LowerRight1 | ToothNumber.LowerRight2 | ToothNumber.LowerRight3
                    | ToothNumber.LowerRight4 | ToothNumber.LowerRight5 | ToothNumber.LowerRight6 | ToothNumber.LowerRight7 | ToothNumber.LowerRight8
                    | ToothNumber.LowerLeft1 | ToothNumber.LowerLeft2 | ToothNumber.LowerLeft3
                    | ToothNumber.LowerLeft4 | ToothNumber.LowerLeft5 | ToothNumber.LowerLeft6 | ToothNumber.LowerLeft7 | ToothNumber.LowerLeft8;
            }
        }

        public static ToothNumber NyushiFlags
        {
            get
            {
                return
                      ToothNumber.UpperRightA | ToothNumber.UpperRightB | ToothNumber.UpperRightC
                    | ToothNumber.UpperRightD | ToothNumber.UpperRightE
                    | ToothNumber.UpperLeftA | ToothNumber.UpperLeftB | ToothNumber.UpperLeftC
                    | ToothNumber.UpperLeftD | ToothNumber.UpperLeftE
                    | ToothNumber.LowerRightA | ToothNumber.LowerRightB | ToothNumber.LowerRightC
                    | ToothNumber.LowerRightD | ToothNumber.LowerRightE
                    | ToothNumber.LowerLeftA | ToothNumber.LowerLeftB | ToothNumber.LowerLeftC
                    | ToothNumber.LowerLeftD | ToothNumber.LowerLeftE;
            }
        }

        public static ToothNumber FrontTeethFlags
        {
            get
            {
                return
                       ToothNumber.UpperRight1 | ToothNumber.UpperRight2 | ToothNumber.UpperRight3
                    | ToothNumber.UpperRightA | ToothNumber.UpperRightB | ToothNumber.UpperRightC
                    | ToothNumber.UpperLeft1 | ToothNumber.UpperLeft2 | ToothNumber.UpperLeft3
                    | ToothNumber.UpperLeftA | ToothNumber.UpperLeftB | ToothNumber.UpperLeftC
                    | ToothNumber.LowerRight1 | ToothNumber.LowerRight2 | ToothNumber.LowerRight3
                    | ToothNumber.LowerRightA | ToothNumber.LowerRightB | ToothNumber.LowerRightC
                    | ToothNumber.LowerLeft1 | ToothNumber.LowerLeft2 | ToothNumber.LowerLeft3
                    | ToothNumber.LowerLeftA | ToothNumber.LowerLeftB | ToothNumber.LowerLeftC;
            }
        }

        public static ToothNumber PreMolarFlags
        {
            get
            {
                return
                       ToothNumber.UpperRight4 | ToothNumber.UpperRight5
                     | ToothNumber.UpperRightD | ToothNumber.UpperRightE
                     | ToothNumber.UpperLeft4 | ToothNumber.UpperLeft5
                     | ToothNumber.UpperLeftD | ToothNumber.UpperLeftE
                     | ToothNumber.LowerRight4 | ToothNumber.LowerRight5
                     | ToothNumber.LowerRightD | ToothNumber.LowerRightE
                     | ToothNumber.LowerLeft4 | ToothNumber.LowerLeft5
                     | ToothNumber.LowerLeftD | ToothNumber.LowerLeftE;
            }
        }
        public static ToothNumber MolarFlags
        {
            get
            {
                return
                      ToothNumber.UpperRight6 | ToothNumber.UpperRight7 | ToothNumber.UpperRight8
                    | ToothNumber.UpperLeft6 | ToothNumber.UpperLeft7 | ToothNumber.UpperLeft8
                    | ToothNumber.LowerRight7 | ToothNumber.LowerRight8
                    | ToothNumber.LowerLeft6 | ToothNumber.LowerLeft7 | ToothNumber.LowerLeft8
                    | ToothNumber.LowerLeftD | ToothNumber.LowerLeftE;
            }
        }

    }
}
