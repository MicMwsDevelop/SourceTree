//
// enum ToothPosition - 歯の位置の定義(永久歯１番と永久歯２番は区別するが、１番と乳歯Ａは同じ位置)
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;
using System.Collections.Generic;

namespace MwsLib.BuiData
{
	/// <summary>
	/// enum ToothPositionに関するヘルパーメソッド・拡張メソッドを定義するクラス
	/// 　近心→遠心かつ右上→左上→右下→左下の順での順次列挙処理やセクション区分等
	/// </summary>
	public static class ToothPositionHelper
    {
        /// <summary>
        /// 全ToothPositionの昇順配列
        /// </summary>
        public static readonly ToothPosition[] All = 
        {
             ToothPosition.UpperRight1
            ,ToothPosition.UpperRight2
            ,ToothPosition.UpperRight3
            ,ToothPosition.UpperRight4
            ,ToothPosition.UpperRight5
            ,ToothPosition.UpperRight6
            ,ToothPosition.UpperRight7
            ,ToothPosition.UpperRight8
            ,ToothPosition.UpperLeft1
            ,ToothPosition.UpperLeft2
            ,ToothPosition.UpperLeft3
            ,ToothPosition.UpperLeft4
            ,ToothPosition.UpperLeft5
            ,ToothPosition.UpperLeft6
            ,ToothPosition.UpperLeft7
            ,ToothPosition.UpperLeft8
            ,ToothPosition.LowerRight1
            ,ToothPosition.LowerRight2
            ,ToothPosition.LowerRight3
            ,ToothPosition.LowerRight4
            ,ToothPosition.LowerRight5
            ,ToothPosition.LowerRight6
            ,ToothPosition.LowerRight7
            ,ToothPosition.LowerRight8
            ,ToothPosition.LowerLeft1
            ,ToothPosition.LowerLeft2
            ,ToothPosition.LowerLeft3
            ,ToothPosition.LowerLeft4
            ,ToothPosition.LowerLeft5
            ,ToothPosition.LowerLeft6
            ,ToothPosition.LowerLeft7
            ,ToothPosition.LowerLeft8
        };


        /// <summary>
        /// 指定されたToothAttribute値が単独のToothAttribute値を示しているかどうかをチェックする
        /// </summary>
        /// <returns>true:単独の値 false:None、存在しない値、または、複数の値の組み合わせ。</returns>
        public static bool IsSingle(this ToothPosition pos)
        {
            // $パフォーマンス改善$
            return ((uint)pos).BitCount() == 1 && pos >= ToothPosition.UpperRight1 && pos <= ToothPosition.LowerLeft8;
        }

        /// <summary>
        /// 最初のToothPosition
        /// </summary>
        public static ToothPosition First
        {
            get
            {
                return ToothPosition.UpperRight1;
            }
        }

        /// <summary>
        /// 最後のToothPosition
        /// </summary>
        public static ToothPosition Last
        {
            get
            {
                return ToothPosition.LowerLeft8;
            }
        }

        /// <summary>
        /// 次のToothPositionを取得
        /// </summary>
        /// <param name="pos">単独のToothPosition値を指定</param>
        /// <returns>次のToothPosition値。次のToothPosition値が存在しない場合はToothPosition.None</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static ToothPosition Next(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif

            if (pos == Last)
            {
                return ToothPosition.None;
            }
            else
            {
                return (ToothPosition)((uint)pos << 1);
            }
        }

        /// <summary>
        /// 前のToothPositionを取得
        /// </summary>
        /// <param name="pos">単独のToothPosition値を指定</param>
        /// <returns>前のToothPosition値。前のToothPosition値が存在しない場合はToothPosition.None</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static ToothPosition Prev(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            if (pos == First)
            {
                return ToothPosition.None;
            }
            else
            {
                return (ToothPosition)((uint)pos >> 1);
            }
        }

        /// <summary>
        /// flagsから個々のToothPositionを列挙
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static IEnumerable<ToothPosition> Separate(this ToothPosition flags)
        {
            for (ToothPosition p = ToothPosition.UpperRight1; p != ToothPosition.None; p = p.Next())
            {
                if (flags.HasFlag(p))
                {
                    yield return p;
                }
            }
        }

        /// <summary>
        /// 上顎かどうかどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothPosition値を指定</param>
        /// <returns>上顎の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsUpper(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.UpperRight1 <= pos && pos <= ToothPosition.UpperLeft8;
        }

        /// <summary>
        /// 下顎かどうかどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothPosition値を指定</param>
        /// <returns>下顎の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsLower(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.LowerRight1 <= pos && pos <= ToothPosition.LowerLeft8;
        }

        /// <summary>
        /// 右側かどうかどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothPosition値を指定</param>
        /// <returns>右側の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsRight(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return (ToothPosition.UpperRight1 <= pos && pos <= ToothPosition.UpperRight8)
                    || (ToothPosition.LowerRight1 <= pos && pos <= ToothPosition.LowerRight8);
        }

        /// <summary>
        /// 左側かどうかどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothPosition値を指定</param>
        /// <returns>左側の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsLeft(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return (ToothPosition.UpperLeft1 <= pos && pos <= ToothPosition.UpperLeft8)
                    || (ToothPosition.LowerLeft1 <= pos && pos <= ToothPosition.LowerLeft8);
        }

        /// <summary>
        /// 上顎右側かどうかどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上顎右側の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsUpperRight(this ToothPosition pos)
        {
            return pos.IsUpper() && pos.IsRight();
        }

        /// <summary>
        /// 上顎左側かどうかどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上顎左側の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsUpperLeft(this ToothPosition pos)
        {
            return pos.IsUpper() && pos.IsLeft();
        }

        /// <summary>
        /// 下顎右側かどうかどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>下顎右側の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsLowerRight(this ToothPosition position)
        {
            return position.IsLower() && position.IsRight();
        }

        /// <summary>
        /// 下顎左側かどうかどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>下顎左側の場合はtrue、それ以外の場合はfalseを返す</returns>
        public static bool IsLowerLeft(this ToothPosition position)
        {
            return position.IsLower() && position.IsLeft();
        }

        /// <summary>
        /// 上顎右側臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上顎右側臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsUpperRightMolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.UpperRight4 <= pos && pos <= ToothPosition.UpperRight8;
        }

        /// <summary>
        /// 上顎前歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上顎前歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsUpperFrontTooth(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return (ToothPosition.UpperRight1 <= pos && pos <= ToothPosition.UpperRight3)
                    || (ToothPosition.UpperLeft1 <= pos && pos <= ToothPosition.UpperLeft3);
        }

        /// <summary>
        /// 上顎左側臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上顎左側臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsUpperLeftMolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.UpperLeft4 <= pos && pos <= ToothPosition.UpperLeft8;
        }

        /// <summary>
        /// 下顎右側臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>下顎右側臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsLowerRightMolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.LowerRight4 <= pos && pos <= ToothPosition.LowerRight8;
        }

        /// <summary>
        /// 下顎前歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>下顎前歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsLowerFrontTooth(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return (ToothPosition.LowerRight1 <= pos && pos <= ToothPosition.LowerRight3)
                    || (ToothPosition.LowerLeft1 <= pos && pos <= ToothPosition.LowerLeft3);
        }

        /// <summary>
        /// 下顎左側臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>下顎左側臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsLowerLeftMolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.LowerLeft4 <= pos && pos <= ToothPosition.LowerLeft8;
        }

        /// <summary>
        /// 上顎右側小臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上顎右側小臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsUpperRightPremolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.UpperRight4 == pos || ToothPosition.UpperRight5 == pos;
        }

        /// <summary>
        /// 上顎左側小臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上顎左側小臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsUpperLeftPremolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.UpperLeft4 == pos || ToothPosition.UpperLeft5 == pos;
        }

        /// <summary>
        /// 下顎右側小臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>下顎右側小臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsLowerRightPremolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.LowerRight4 == pos || ToothPosition.LowerRight5 == pos;
        }

        /// <summary>
        /// 下顎左側小臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>下顎左側小臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsLowerLeftPremolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.LowerLeft4 == pos || ToothPosition.LowerLeft5 == pos;
        }

        /// <summary>
        /// 上顎右側大臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上顎右側大臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsUpperRightTrueMolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.UpperRight6 <= pos && pos <= ToothPosition.UpperRight8;
        }

        /// <summary>
        /// 上顎左側大臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上顎左側大臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsUpperLeftTrueMolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.UpperLeft6 <= pos && pos <= ToothPosition.UpperLeft8;
        }

        /// <summary>
        /// 下顎右側大臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>下顎右側大臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsLowerRightTrueMolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.LowerRight6 <= pos && pos <= ToothPosition.LowerRight8;
        }
        /// <summary>
        /// 下顎左側大臼歯かどうかを返す
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>下顎左側大臼歯の場合はtrue、それ以外の場合はfalseを返す</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        public static bool IsLowerLeftTrueMolar(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            return ToothPosition.LowerLeft6 <= pos && pos <= ToothPosition.LowerLeft8;
        }

        /// <summary>
        /// 顎の位置を取得
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上下額区分</returns>
        /// <exception cref="ArgumentException">単独のToothPosition値ではないので演算できない</exception>
        /// <exception cref="ApplicationException">上下顎の判定に失敗</exception>
        public static Jaw GetJaw(this ToothPosition pos)
        {
            // $パフォーマンス改善$
#if DEBUG
            if (!IsSingle(pos))
            {
                throw new ArgumentException("単独のToothPosition値ではないので演算できない");
            }
#endif
            if (pos.IsUpper())
            {
                // 上顎
                return Jaw.UpperJaw;
            }
            else if (pos.IsLower())
            {
                // 下顎
                return Jaw.LowerJaw;
            }
            else
            {
                throw new ApplicationException("上下顎の判定に失敗");
            }
        }

        /// <summary>
        /// 1/3額単位の位置を取得
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>1／3顎単位の区分</returns>
        /// <exception cref="ApplicationException">1／3顎の判定に失敗</exception>
        public static OneThirdJaw GetOneThirdJaw(this ToothPosition pos)
        {
            // 上顎右側臼歯
            if (pos.IsUpperRightMolar())
            {
                return OneThirdJaw.UpperRightMolars;
            }
            // 上顎前歯
            else if (pos.IsUpperFrontTooth())
            {
                return OneThirdJaw.UpperFrontTeeth;
            }
            // 上顎左側臼歯
            else if (pos.IsUpperLeftMolar())
            {
                return OneThirdJaw.UpperLeftMolars;
            }
            // 下顎右側臼歯
            else if (pos.IsLowerRightMolar())
            {
                return OneThirdJaw.LowerRightMolars;
            }
            // 下顎前歯
            else if (pos.IsLowerFrontTooth())
            {
                return OneThirdJaw.LowerFrontTeeth;
            }
            // 下顎左側臼歯
            else if (pos.IsLowerLeftMolar())
            {
                return OneThirdJaw.LowerLeftMolars;
            }
            else
            {
                throw new ApplicationException("1／3顎の判定に失敗");
            }
        }

        /// <summary>
        /// 上下顎右左側区分の判定
        /// </summary>
        /// <param name="pos">単独のToothNumber値</param>
        /// <returns>上下顎それぞれの右側と左側の区分</returns>
        /// <exception cref="ApplicationException">上下顎右左側区分の判定に失敗</exception>
        public static HalfJaw GetHalfJaw(this ToothPosition pos)
        {
            // 上顎右側
            if (pos.IsUpper() && pos.IsRight())
            {
                return HalfJaw.UpperRight;
            }
            // 上顎左側
            else if (pos.IsUpper() && pos.IsLeft())
            {
                return HalfJaw.UpperLeft;
            }
            // 下顎右側
            else if (pos.IsLower() && pos.IsRight())
            {
                return HalfJaw.LowerRight;
            }
            // 下顎左側
            else if (pos.IsLower() && pos.IsLeft())
            {
                return HalfJaw.LowerLeft;
            }
            else
            {
                throw new ApplicationException("上下顎右左側区分の判定に失敗");
            }
        }

        /// <summary>
        /// 指定したToothPosition値から、序数を求める。
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static int ToSerialNumber(this ToothPosition pos)
        {
            if (typeof(ToothPosition).IsEnumDefined(pos))
            {
                // 2を何乗したらpositionのビット値になるかを求める事でビット位置を表す序数を求める
                // (2のゼロ乗は1, 2の１乗は2, 2の２乗は4, 2の31乗は0x80000000)
                return (int)System.Math.Round(System.Math.Log((double)pos, 2.0));
            }
            else
            {
                throw new ArgumentException(string.Format("ToothPositionに存在しない値[{0}]が指定された。", (uint)pos));
            }
        }

        /// <summary>
        /// 指定したToothPosition値に対応する、各ブロック内での0～7のインデックスを取得
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int GetInBlockIndex(this ToothPosition position)
        {
            int serial = position.ToSerialNumber();
            if (position.IsUpperRight())
            {
                return serial - (int)ToothPosition.UpperRight1.ToSerialNumber();
            }
            else if (position.IsUpperLeft())
            {
                return serial - (int)ToothPosition.UpperLeft1.ToSerialNumber();
            }
            else if (position.IsLowerRight())
            {
                return serial - (int)ToothPosition.LowerRight1.ToSerialNumber();
            }
            else if (position.IsLowerLeft())
            {
                return serial - (int)ToothPosition.LowerLeft1.ToSerialNumber();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static int ToFDI(this ToothPosition position)
        {
            HalfJaw block = position.GetHalfJaw();
            int inBlockIndex = position.GetInBlockIndex();
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

        public static ToothPosition FDIToToothPosition(this int fdi)
        {
            int n1 = fdi / 10;
            int n2 = fdi % 10;
            if (n1 >= 1 && n1 <= 4 && n2 >= 1 && n2 <= 8)
            {
                ToothPosition sp;
                switch (n1)
                {
                    case 1:
                        sp = ToothPosition.UpperRight1;
                        break;
                    case 2:
                        sp = ToothPosition.UpperLeft1;
                        break;
                    case 3:
                        sp = ToothPosition.LowerLeft1;
                        break;
                    case 4:
                        sp = ToothPosition.LowerRight1;
                        break;
                    default:
                        throw new NotSupportedException();
                }

                return (ToothPosition)((int)sp << (n2 - 1));
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("指定された値({0})はFDI値の範囲外", fdi));
            }
        }

    }
}
