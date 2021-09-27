//
// ひとつの歯番(ToothPosition+EikyushiNyushi)によって位置が特定される部位の基本クラス)
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace CommonLib.BuiData
{
	/// <summary>
	/// 歯番によって口腔内での位置を表される全ての部位クラスの抽象基本クラス
	/// </summary>
	[Serializable]
    public abstract class Tooth : Bui
    {
        /// <summary>
        /// 歯番(歯の位置)
        /// </summary>
        protected ToothNumber m_Number = ToothNumber.None;

        public override bool IsLeaf
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 永久歯・乳歯の区別(EikyushiNyushiType)を持つ型かどうか
        /// </summary>
        public override bool HasEikyushiNyushi
        {
            get
            {
                return true;
            }
        }


        /// <summary>
        /// 歯番(ToothPosition)を持つ型かどうか
        /// </summary>
        public override bool HasToothPosition
        {
            get
            {
                return true;
            }
        }
        
        /// <summary>
        /// 歯番
        /// </summary>
        public override ToothPosition Position
        {
            get
            {
                return m_Number.GetPosition();
            }
        }

        /// <summary>
        /// 永久歯／乳歯区分
        /// </summary>
        public override EikyushiNyushiType EikyushiNyushi
        {
            get
            {
                return m_Number.GetEikyushiNyushi();
            }
        }

        public override ToothNumber Number
        {
            get
            {
                return m_Number;
            }
        }

        public override bool IsContainer
        {
            get {
                return false;
            }
        }

        public override bool HasToothAttribute
        {
            get {
                return false;
            }
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        /// <remarks>デフォルトコンストラクタは使用しない</remarks>
        protected Tooth()
        {
        }

        /// <summary>
        /// 歯番・永久歯／乳歯を指定して「歯」のインスタンスを作成
        /// </summary>
        /// <param name="position">歯番</param>
        /// <param name="eikyushiNyushi">永久歯・乳歯の区別</param>
        /// <remarks>抽象クラスなので派生クラスコンストラクタからの呼び出しのみ</remarks>
        protected Tooth(ToothPosition position, EikyushiNyushiType eikyushiNyushi)
        {
            if(!position.IsSingle())
            {
                // 例外処理
                throw new ArgumentException("positionが有効な単独のToothPosition値ではない");
            }

            // 乳歯の場合、positionは1～5でなければならない
            if (eikyushiNyushi == EikyushiNyushiType.Nyushi &&
                ! (
                           (ToothPosition.UpperRight1 <= position && position <= ToothPosition.UpperRight5)
                        || (ToothPosition.UpperLeft1 <= position && position <= ToothPosition.UpperLeft5)
                        || (ToothPosition.LowerRight1 <= position && position <= ToothPosition.LowerRight5)
                        || (ToothPosition.LowerLeft1 <= position && position <= ToothPosition.LowerLeft5)
                )
            )
            {
                // 乳歯に指定できないToothPositionが指定された
                throw new ArgumentException("乳歯に指定できないToothPosition値が指定された");
            }

            m_Number = ToothNumberHelper.GetNumber(position, eikyushiNyushi);
        }
        /// <summary>
        /// 歯番・永久歯／乳歯を指定して「歯」のインスタンスを作成
        /// </summary>
        /// <param name="position">歯番</param>
        /// <param name="eikyushiNyushi">永久歯・乳歯の区別</param>
        /// <remarks>抽象クラスなので派生クラスコンストラクタからの呼び出しのみ</remarks>
        protected Tooth(ToothNumber number)
        {
            if (!number.IsSingle())
            {
                // 例外処理
                throw new ArgumentException("numberが有効な単独のToothNumber値ではない");
            }
            m_Number = number;
        }

    // 歯番の属性判定

        /// <summary>
        /// 永久歯かどうか
        /// </summary>
        /// <returns>永久歯:true  永久歯以外:false</returns>
        public bool IsEikyushi
        {
            get
            {
                return EikyushiNyushi == EikyushiNyushiType.Eikyushi;
            }
        }

        /// <summary>
        /// 乳歯かどうか
        /// </summary>
        /// <returns>乳歯:true  乳歯以外:false</returns>
        public bool IsNyushi
        {
            get
            {
                return EikyushiNyushi == EikyushiNyushiType.Nyushi;
            }
        }

        /// <summary>
        /// 上顎かどうか
        /// </summary>
        public bool IsUpper
        {
            get
            {
                return Number.IsUpper();
            }
        }

        /// <summary>
        /// 下顎かどうか
        /// </summary>
        public bool IsLower
        {
            get
            {
                return Number.IsLower();
            }
        }

        /// <summary>
        /// 右側かどうか
        /// </summary>
        public bool IsRight
        {
            get
            {
                return Number.IsRight();
            }
        }

        /// <summary>
        /// 左側かどうか
        /// </summary>
        public bool IsLeft
        {
            get
            {
                return Number.IsLeft();
            }
        }

        /// <summary>
        /// 上顎右側かどうか
        /// </summary>
        public bool IsUpperRight
        {
            get
            {
                return Number.IsUpperRight();
            }
        }

        /// <summary>
        /// 上顎左側かどうか
        /// </summary>
        public bool IsUpperLeft
        {
            get
            {
                return Number.IsUpperLeft();
            }
        }

        /// <summary>
        /// 下顎右側かどうか
        /// </summary>
        public bool IsLowerRight
        {
            get
            {
                return Number.IsLowerRight();
            }
        }

        /// <summary>
        /// 下顎左側かどうか
        /// </summary>
        public bool IsLowerLeft
        {
            get
            {
                return Number.IsLowerLeft();
            }
        }

        /// <summary>
        /// 上顎右側臼歯かどうか
        /// </summary>
        public bool IsUpperRightMolar
        {
            get
            {
                return Number.IsUpperRightMolar();
            }
        }

        /// <summary>
        /// 上顎前歯かどうか
        /// </summary>
        public bool IsUpperFrontTooth
        {
            get
            {
                return Number.IsUpperFrontTooth();
            }
        }

        /// <summary>
        /// 上顎左側臼歯かどうか
        /// </summary>
        public bool IsUpperLeftMolar
        {
            get
            {
                return Number.IsUpperLeftMolar();
            }
        }

        /// <summary>
        /// 下顎右側臼歯かどうか
        /// </summary>
        public bool IsLowerRightMolar
        {
            get
            {
                return Number.IsLowerRightMolar();
            }
        }

        /// <summary>
        /// 下顎前歯かどうか
        /// </summary>
        public bool IsLowerFrontTooth
        {
            get
            {
                return Number.IsLowerFrontTooth();
            }
        }

        /// <summary>
        /// 下顎左側臼歯かどうか
        /// </summary>
        public bool IsLowerLeftMolar
        {
            get
            {
                return Number.IsLowerLeftMolar();
            }
        }

        /// <summary>
        /// 上顎右側小臼歯かどうか
        /// </summary>
        public bool IsUpperRightPremolar
        {
            get
            {
                return Number.IsUpperRightPremolar();
            }
        }

        /// <summary>
        /// 上顎左側小臼歯かどうか
        /// </summary>
        public bool IsUpperLeftPremolar
        {
            get
            {
                return Number.IsUpperLeftPremolar();
            }
        }

        /// <summary>
        /// 下顎右側小臼歯かどうか
        /// </summary>
        public bool IsLowerRightPremolar
        {
            get
            {
                return Number.IsLowerRightPremolar();
            }
        }

        /// <summary>
        /// 下顎左側小臼歯かどうか
        /// </summary>
        public bool IsLowerLeftPremolar
        {
            get
            {
                return Number.IsLowerLeftPremolar();
            }
        }

        /// <summary>
        /// 上顎右側大臼歯かどうか
        /// </summary>
        public bool IsUpperRightTrueMolar
        {
            get
            {
                return Number.IsUpperRightTrueMolar();
            }
        }

        /// <summary>
        /// 上顎左側大臼歯かどうか
        /// </summary>
        public bool IsUpperLeftTrueMolar
        {
            get
            {
                return Number.IsUpperLeftTrueMolar();
            }
        }

        /// <summary>
        /// 下顎右側大臼歯かどうか
        /// </summary>
        public bool IsLowerRightTrueMolar
        {
            get
            {
                return Number.IsLowerRightTrueMolar();
            }
        }

        /// <summary>
        /// 下顎左側大臼歯かどうか
        /// </summary>
        public bool IsLowerLeftTrueMolar
        {
            get
            {
                return Number.IsLowerLeftTrueMolar();
            }
        }

        /// <summary>
        /// 顎の位置
        /// </summary>
        public Jaw Jaw
        {
            get
            {
                return Number.GetJaw();
            }
        }

        /// <summary>
        /// 1/3額単位の位置
        /// </summary>
        public OneThirdJaw OneThirdJaw
        {
            get
            {
                return Number.GetOneThirdJaw();
            }
        }

        /// <summary>
        /// 上下顎右左側区分の判定
        /// </summary>
        public HalfJaw HalfJaw
        {
            get
            {
                return Number.GetHalfJaw();
            }
        }


        // 永久歯 ///////////////////////////////////////////////

        /// <summary>
        /// 永久歯歯番文字群
        /// </summary>
        protected static readonly char[] EIKYUSHI_CHARS = { '1', '2', '3', '4', '5', '6', '7', '8' };

        /// <summary>
        /// 永久歯用の変換テーブル
        /// </summary>
        internal static readonly dynamic[] EIKYUSHI_TABLE =
        {
            new { Position = ToothPosition.UpperRight1, HalfJaw = HalfJaw.UpperRight, Str = EIKYUSHI_CHARS[0] },
            new { Position = ToothPosition.UpperRight2, HalfJaw = HalfJaw.UpperRight, Str = EIKYUSHI_CHARS[1] },
            new { Position = ToothPosition.UpperRight3, HalfJaw = HalfJaw.UpperRight, Str = EIKYUSHI_CHARS[2] },
            new { Position = ToothPosition.UpperRight4, HalfJaw = HalfJaw.UpperRight, Str = EIKYUSHI_CHARS[3] },
            new { Position = ToothPosition.UpperRight5, HalfJaw = HalfJaw.UpperRight, Str = EIKYUSHI_CHARS[4] },
            new { Position = ToothPosition.UpperRight6, HalfJaw = HalfJaw.UpperRight, Str = EIKYUSHI_CHARS[5] },
            new { Position = ToothPosition.UpperRight7, HalfJaw = HalfJaw.UpperRight, Str = EIKYUSHI_CHARS[6] },
            new { Position = ToothPosition.UpperRight8, HalfJaw = HalfJaw.UpperRight, Str = EIKYUSHI_CHARS[7] },
            new { Position = ToothPosition.UpperLeft1,  HalfJaw = HalfJaw.UpperLeft,  Str = EIKYUSHI_CHARS[0] },
            new { Position = ToothPosition.UpperLeft2,  HalfJaw = HalfJaw.UpperLeft,  Str = EIKYUSHI_CHARS[1] },
            new { Position = ToothPosition.UpperLeft3,  HalfJaw = HalfJaw.UpperLeft,  Str = EIKYUSHI_CHARS[2] },
            new { Position = ToothPosition.UpperLeft4,  HalfJaw = HalfJaw.UpperLeft,  Str = EIKYUSHI_CHARS[3] },
            new { Position = ToothPosition.UpperLeft5,  HalfJaw = HalfJaw.UpperLeft,  Str = EIKYUSHI_CHARS[4] },
            new { Position = ToothPosition.UpperLeft6,  HalfJaw = HalfJaw.UpperLeft,  Str = EIKYUSHI_CHARS[5] },
            new { Position = ToothPosition.UpperLeft7,  HalfJaw = HalfJaw.UpperLeft,  Str = EIKYUSHI_CHARS[6] },
            new { Position = ToothPosition.UpperLeft8,  HalfJaw = HalfJaw.UpperLeft,  Str = EIKYUSHI_CHARS[7] },
            new { Position = ToothPosition.LowerRight1, HalfJaw = HalfJaw.LowerRight, Str = EIKYUSHI_CHARS[0] },
            new { Position = ToothPosition.LowerRight2, HalfJaw = HalfJaw.LowerRight, Str = EIKYUSHI_CHARS[1] },
            new { Position = ToothPosition.LowerRight3, HalfJaw = HalfJaw.LowerRight, Str = EIKYUSHI_CHARS[2] },
            new { Position = ToothPosition.LowerRight4, HalfJaw = HalfJaw.LowerRight, Str = EIKYUSHI_CHARS[3] },
            new { Position = ToothPosition.LowerRight5, HalfJaw = HalfJaw.LowerRight, Str = EIKYUSHI_CHARS[4] },
            new { Position = ToothPosition.LowerRight6, HalfJaw = HalfJaw.LowerRight, Str = EIKYUSHI_CHARS[5] },
            new { Position = ToothPosition.LowerRight7, HalfJaw = HalfJaw.LowerRight, Str = EIKYUSHI_CHARS[6] },
            new { Position = ToothPosition.LowerRight8, HalfJaw = HalfJaw.LowerRight, Str = EIKYUSHI_CHARS[7] },
            new { Position = ToothPosition.LowerLeft1,  HalfJaw = HalfJaw.LowerLeft,  Str = EIKYUSHI_CHARS[0] },
            new { Position = ToothPosition.LowerLeft2,  HalfJaw = HalfJaw.LowerLeft,  Str = EIKYUSHI_CHARS[1] },
            new { Position = ToothPosition.LowerLeft3,  HalfJaw = HalfJaw.LowerLeft,  Str = EIKYUSHI_CHARS[2] },
            new { Position = ToothPosition.LowerLeft4,  HalfJaw = HalfJaw.LowerLeft,  Str = EIKYUSHI_CHARS[3] },
            new { Position = ToothPosition.LowerLeft5,  HalfJaw = HalfJaw.LowerLeft,  Str = EIKYUSHI_CHARS[4] },
            new { Position = ToothPosition.LowerLeft6,  HalfJaw = HalfJaw.LowerLeft,  Str = EIKYUSHI_CHARS[5] },
            new { Position = ToothPosition.LowerLeft7,  HalfJaw = HalfJaw.LowerLeft,  Str = EIKYUSHI_CHARS[6] },
            new { Position = ToothPosition.LowerLeft8,  HalfJaw = HalfJaw.LowerLeft,  Str = EIKYUSHI_CHARS[7] },
        };

        // 乳歯 ///////////////////////////////////////////////

        /// <summary>
        /// 乳歯歯番文字群
        /// </summary>
        protected static readonly char[] NYUSHI_CHARS = { 'A', 'B', 'C', 'D', 'E' };


        /// <summary>
        /// 乳歯用の変換テーブル
        /// </summary>
        internal static readonly dynamic[] NYUSHI_TABLE =
        {
            new { Position = ToothPosition.UpperRight1, HalfJaw = HalfJaw.UpperRight, Str = NYUSHI_CHARS[0] },
            new { Position = ToothPosition.UpperRight2, HalfJaw = HalfJaw.UpperRight, Str = NYUSHI_CHARS[1] },
            new { Position = ToothPosition.UpperRight3, HalfJaw = HalfJaw.UpperRight, Str = NYUSHI_CHARS[2] },
            new { Position = ToothPosition.UpperRight4, HalfJaw = HalfJaw.UpperRight, Str = NYUSHI_CHARS[3] },
            new { Position = ToothPosition.UpperRight5, HalfJaw = HalfJaw.UpperRight, Str = NYUSHI_CHARS[4] },
            new { Position = ToothPosition.UpperLeft1,  HalfJaw = HalfJaw.UpperLeft,  Str = NYUSHI_CHARS[0] },
            new { Position = ToothPosition.UpperLeft2,  HalfJaw = HalfJaw.UpperLeft,  Str = NYUSHI_CHARS[1] },
            new { Position = ToothPosition.UpperLeft3,  HalfJaw = HalfJaw.UpperLeft,  Str = NYUSHI_CHARS[2] },
            new { Position = ToothPosition.UpperLeft4,  HalfJaw = HalfJaw.UpperLeft,  Str = NYUSHI_CHARS[3] },
            new { Position = ToothPosition.UpperLeft5,  HalfJaw = HalfJaw.UpperLeft,  Str = NYUSHI_CHARS[4] },
            new { Position = ToothPosition.LowerRight1, HalfJaw = HalfJaw.LowerRight, Str = NYUSHI_CHARS[0] },
            new { Position = ToothPosition.LowerRight2, HalfJaw = HalfJaw.LowerRight, Str = NYUSHI_CHARS[1] },
            new { Position = ToothPosition.LowerRight3, HalfJaw = HalfJaw.LowerRight, Str = NYUSHI_CHARS[2] },
            new { Position = ToothPosition.LowerRight4, HalfJaw = HalfJaw.LowerRight, Str = NYUSHI_CHARS[3] },
            new { Position = ToothPosition.LowerRight5, HalfJaw = HalfJaw.LowerRight, Str = NYUSHI_CHARS[4] },
            new { Position = ToothPosition.LowerLeft1,  HalfJaw = HalfJaw.LowerLeft,  Str = NYUSHI_CHARS[0] },
            new { Position = ToothPosition.LowerLeft2,  HalfJaw = HalfJaw.LowerLeft,  Str = NYUSHI_CHARS[1] },
            new { Position = ToothPosition.LowerLeft3,  HalfJaw = HalfJaw.LowerLeft,  Str = NYUSHI_CHARS[2] },
            new { Position = ToothPosition.LowerLeft4,  HalfJaw = HalfJaw.LowerLeft,  Str = NYUSHI_CHARS[3] },
            new { Position = ToothPosition.LowerLeft5,  HalfJaw = HalfJaw.LowerLeft,  Str = NYUSHI_CHARS[4] },
        };

        // 上下額左右区分 ///////////////////////////////////////////////

        /// <summary>
        /// 上下額左右区分文字群
        /// </summary>
        protected static readonly char[] HALF_JAW_CHARS = { 'R', 'L', 'r', 'l' };


        /// <summary>
        /// 上下顎左右区分の変換テーブル
        /// </summary>
        protected static readonly dynamic[] HALF_JAW_TABLE =
        {
            new { HalfJaw = HalfJaw.UpperRight, Str = HALF_JAW_CHARS[0] },
            new { HalfJaw = HalfJaw.UpperLeft, Str = HALF_JAW_CHARS[1] },
            new { HalfJaw = HalfJaw.LowerRight, Str = HALF_JAW_CHARS[2] },
            new { HalfJaw = HalfJaw.LowerLeft, Str = HALF_JAW_CHARS[3] },
        };

        /// <summary>
        /// 歯番を表す文字を取得(1～8 or A～E)
        /// </summary>
        /// <returns></returns>
        public char GetNumberChar()
        {
            if (IsEikyushi)
            {
                int serialNumber = Number.ToSerialNumber();
                int index;
                switch (Number.GetHalfJaw())
                {
                    case BuiData.HalfJaw.UpperRight:
                        index = serialNumber - ToothNumber.UpperRight1.ToSerialNumber();
                        break;
                    case BuiData.HalfJaw.UpperLeft:
                        index = serialNumber - ToothNumber.UpperLeft1.ToSerialNumber();
                        break;
                    case BuiData.HalfJaw.LowerRight:
                        index = serialNumber - ToothNumber.LowerRight1.ToSerialNumber();
                        break;
                    case BuiData.HalfJaw.LowerLeft:
                        index = serialNumber - ToothNumber.LowerLeft1.ToSerialNumber();
                        break;
                    default:
                        throw new NotSupportedException();
                }
                return EIKYUSHI_CHARS[index];
            }
            else
            {
                int serialNumber = Number.ToSerialNumber();
                int index;
                switch (Number.GetHalfJaw())
                {
                    case BuiData.HalfJaw.UpperRight:
                        index = serialNumber - ToothNumber.UpperRightA.ToSerialNumber();
                        break;
                    case BuiData.HalfJaw.UpperLeft:
                        index = serialNumber - ToothNumber.UpperLeftA.ToSerialNumber();
                        break;
                    case BuiData.HalfJaw.LowerRight:
                        index = serialNumber - ToothNumber.LowerRightA.ToSerialNumber();
                        break;
                    case BuiData.HalfJaw.LowerLeft:
                        index = serialNumber - ToothNumber.LowerLeftA.ToSerialNumber();
                        break;
                    default:
                        throw new NotSupportedException();
                }
                return NYUSHI_CHARS[index];
            }
        }


    }
}
