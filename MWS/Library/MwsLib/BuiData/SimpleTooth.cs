//
// 歯番と属性で表現される部位クラス
// 
// UBOX準拠部位表現中の歯番を持つ要素(歯、欠損、隙、支台等。歯番と属性で表現される)
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;
using System.Linq;
using System.Text;

namespace MwsLib.BuiData
{
	/// <summary>
	/// UBOX準拠部位表現中の歯番を持つ要素(歯、欠損、隙、支台等。歯番と属性で表現される)<br/>
	/// このクラスのインスタンス内データの変更は不可(変更が可能なインスタンスメンバは作成しない)
	/// </summary>
	[Serializable]
    public class SimpleTooth : Tooth, IComparable<SimpleTooth>, IComparable<Bui>
    {

        // SimpleTooth定義済みインスタンス ///////////////////////////////////////////////

        /// <summary>歯番右上1</summary>
        public static readonly SimpleTooth UpperRight1NormalNumber = new SimpleTooth(ToothNumber.UpperRight1, ToothAttribute.NormalNumber);
        /// <summary>歯番右上2</summary>
        public static readonly SimpleTooth UpperRight2NormalNumber = new SimpleTooth(ToothNumber.UpperRight2, ToothAttribute.NormalNumber);
        /// <summary>歯番右上3</summary>
        public static readonly SimpleTooth UpperRight3NormalNumber = new SimpleTooth(ToothNumber.UpperRight3, ToothAttribute.NormalNumber);
        /// <summary>歯番右上4</summary>
        public static readonly SimpleTooth UpperRight4NormalNumber = new SimpleTooth(ToothNumber.UpperRight4, ToothAttribute.NormalNumber);
        /// <summary>歯番右上5</summary>
        public static readonly SimpleTooth UpperRight5NormalNumber = new SimpleTooth(ToothNumber.UpperRight5, ToothAttribute.NormalNumber);
        /// <summary>歯番右上6mary>
        public static readonly SimpleTooth UpperRight6NormalNumber = new SimpleTooth(ToothNumber.UpperRight6, ToothAttribute.NormalNumber);
        /// <summary>歯番右上7mary>
        public static readonly SimpleTooth UpperRight7NormalNumber = new SimpleTooth(ToothNumber.UpperRight7, ToothAttribute.NormalNumber);
        /// <summary>歯番右上8mary>
        public static readonly SimpleTooth UpperRight8NormalNumber = new SimpleTooth(ToothNumber.UpperRight8, ToothAttribute.NormalNumber);

        /// <summary>歯番左上1</summary>
        public static readonly SimpleTooth UpperLeft1NormalNumber = new SimpleTooth(ToothNumber.UpperLeft1, ToothAttribute.NormalNumber);
        /// <summary>歯番左上2</summary>
        public static readonly SimpleTooth UpperLeft2NormalNumber = new SimpleTooth(ToothNumber.UpperLeft2, ToothAttribute.NormalNumber);
        /// <summary>歯番左上3</summary>
        public static readonly SimpleTooth UpperLeft3NormalNumber = new SimpleTooth(ToothNumber.UpperLeft3, ToothAttribute.NormalNumber);
        /// <summary>歯番左上4</summary>
        public static readonly SimpleTooth UpperLeft4NormalNumber = new SimpleTooth(ToothNumber.UpperLeft4, ToothAttribute.NormalNumber);
        /// <summary>歯番左上5</summary>
        public static readonly SimpleTooth UpperLeft5NormalNumber = new SimpleTooth(ToothNumber.UpperLeft5, ToothAttribute.NormalNumber);
        /// <summary>歯番左上6mary>
        public static readonly SimpleTooth UpperLeft6NormalNumber = new SimpleTooth(ToothNumber.UpperLeft6, ToothAttribute.NormalNumber);
        /// <summary>歯番左上7mary>
        public static readonly SimpleTooth UpperLeft7NormalNumber = new SimpleTooth(ToothNumber.UpperLeft7, ToothAttribute.NormalNumber);
        /// <summary>歯番左上8mary>
        public static readonly SimpleTooth UpperLeft8NormalNumber = new SimpleTooth(ToothNumber.UpperLeft8, ToothAttribute.NormalNumber);

        /// <summary>歯番右下1</summary>
        public static readonly SimpleTooth LowerRight1NormalNumber = new SimpleTooth(ToothNumber.LowerRight1, ToothAttribute.NormalNumber);
        /// <summary>歯番右下2</summary>
        public static readonly SimpleTooth LowerRight2NormalNumber = new SimpleTooth(ToothNumber.LowerRight2, ToothAttribute.NormalNumber);
        /// <summary>歯番右下3</summary>
        public static readonly SimpleTooth LowerRight3NormalNumber = new SimpleTooth(ToothNumber.LowerRight3, ToothAttribute.NormalNumber);
        /// <summary>歯番右下4</summary>
        public static readonly SimpleTooth LowerRight4NormalNumber = new SimpleTooth(ToothNumber.LowerRight4, ToothAttribute.NormalNumber);
        /// <summary>歯番右下5</summary>
        public static readonly SimpleTooth LowerRight5NormalNumber = new SimpleTooth(ToothNumber.LowerRight5, ToothAttribute.NormalNumber);
        /// <summary>歯番右下6mary>
        public static readonly SimpleTooth LowerRight6NormalNumber = new SimpleTooth(ToothNumber.LowerRight6, ToothAttribute.NormalNumber);
        /// <summary>歯番右下7mary>
        public static readonly SimpleTooth LowerRight7NormalNumber = new SimpleTooth(ToothNumber.LowerRight7, ToothAttribute.NormalNumber);
        /// <summary>歯番右下8mary>
        public static readonly SimpleTooth LowerRight8NormalNumber = new SimpleTooth(ToothNumber.LowerRight8, ToothAttribute.NormalNumber);

        /// <summary>歯番左下1</summary>
        public static readonly SimpleTooth LowerLeft1NormalNumber = new SimpleTooth(ToothNumber.LowerLeft1, ToothAttribute.NormalNumber);
        /// <summary>歯番左下2</summary>
        public static readonly SimpleTooth LowerLeft2NormalNumber = new SimpleTooth(ToothNumber.LowerLeft2, ToothAttribute.NormalNumber);
        /// <summary>歯番左下3</summary>
        public static readonly SimpleTooth LowerLeft3NormalNumber = new SimpleTooth(ToothNumber.LowerLeft3, ToothAttribute.NormalNumber);
        /// <summary>歯番左下4</summary>
        public static readonly SimpleTooth LowerLeft4NormalNumber = new SimpleTooth(ToothNumber.LowerLeft4, ToothAttribute.NormalNumber);
        /// <summary>歯番左下5</summary>
        public static readonly SimpleTooth LowerLeft5NormalNumber = new SimpleTooth(ToothNumber.LowerLeft5, ToothAttribute.NormalNumber);
        /// <summary>歯番左下6mary>
        public static readonly SimpleTooth LowerLeft6NormalNumber = new SimpleTooth(ToothNumber.LowerLeft6, ToothAttribute.NormalNumber);
        /// <summary>歯番左下7mary>
        public static readonly SimpleTooth LowerLeft7NormalNumber = new SimpleTooth(ToothNumber.LowerLeft7, ToothAttribute.NormalNumber);
        /// <summary>歯番左下8mary>
        public static readonly SimpleTooth LowerLeft8NormalNumber = new SimpleTooth(ToothNumber.LowerLeft8, ToothAttribute.NormalNumber);

        /// <summary>歯番右上乳歯A</summary>
        public static readonly SimpleTooth UpperRightANormalNumber = new SimpleTooth(ToothNumber.UpperRightA, ToothAttribute.NormalNumber);
        /// <summary>歯番右上乳歯B</summary>
        public static readonly SimpleTooth UpperRightBNormalNumber = new SimpleTooth(ToothNumber.UpperRightB, ToothAttribute.NormalNumber);
        /// <summary>歯番右上乳歯C</summary>
        public static readonly SimpleTooth UpperRightCNormalNumber = new SimpleTooth(ToothNumber.UpperRightC, ToothAttribute.NormalNumber);
        /// <summary>歯番右上乳歯D</summary>
        public static readonly SimpleTooth UpperRightDNormalNumber = new SimpleTooth(ToothNumber.UpperRightD, ToothAttribute.NormalNumber);
        /// <summary>歯番右上乳歯E</summary>
        public static readonly SimpleTooth UpperRightENormalNumber = new SimpleTooth(ToothNumber.UpperRightE, ToothAttribute.NormalNumber);

        /// <summary>歯番左上乳歯A</summary>
        public static readonly SimpleTooth UpperLeftANormalNumber = new SimpleTooth(ToothNumber.UpperLeftA, ToothAttribute.NormalNumber);
        /// <summary>歯番左上乳歯B</summary>
        public static readonly SimpleTooth UpperLeftBNormalNumber = new SimpleTooth(ToothNumber.UpperLeftB, ToothAttribute.NormalNumber);
        /// <summary>歯番左上乳歯C</summary>
        public static readonly SimpleTooth UpperLeftCNormalNumber = new SimpleTooth(ToothNumber.UpperLeftC, ToothAttribute.NormalNumber);
        /// <summary>歯番左上乳歯D</summary>
        public static readonly SimpleTooth UpperLeftDNormalNumber = new SimpleTooth(ToothNumber.UpperLeftD, ToothAttribute.NormalNumber);
        /// <summary>歯番左上乳歯E</summary>
        public static readonly SimpleTooth UpperLeftENormalNumber = new SimpleTooth(ToothNumber.UpperLeftE, ToothAttribute.NormalNumber);

        /// <summary>歯番右下乳歯A</summary>
        public static readonly SimpleTooth LowerRightANormalNumber = new SimpleTooth(ToothNumber.LowerRightA, ToothAttribute.NormalNumber);
        /// <summary>歯番右下乳歯B</summary>
        public static readonly SimpleTooth LowerRightBNormalNumber = new SimpleTooth(ToothNumber.LowerRightB, ToothAttribute.NormalNumber);
        /// <summary>歯番右下乳歯C</summary>
        public static readonly SimpleTooth LowerRightCNormalNumber = new SimpleTooth(ToothNumber.LowerRightC, ToothAttribute.NormalNumber);
        /// <summary>歯番右下乳歯D</summary>
        public static readonly SimpleTooth LowerRightDNormalNumber = new SimpleTooth(ToothNumber.LowerRightD, ToothAttribute.NormalNumber);
        /// <summary>歯番右下乳歯E</summary>
        public static readonly SimpleTooth LowerRightENormalNumber = new SimpleTooth(ToothNumber.LowerRightE, ToothAttribute.NormalNumber);

        /// <summary>歯番左下乳歯A</summary>
        public static readonly SimpleTooth LowerLeftANormalNumber = new SimpleTooth(ToothNumber.LowerLeftA, ToothAttribute.NormalNumber);
        /// <summary>歯番左下乳歯B</summary>
        public static readonly SimpleTooth LowerLeftBNormalNumber = new SimpleTooth(ToothNumber.LowerLeftB, ToothAttribute.NormalNumber);
        /// <summary>歯番左下乳歯C</summary>
        public static readonly SimpleTooth LowerLeftCNormalNumber = new SimpleTooth(ToothNumber.LowerLeftC, ToothAttribute.NormalNumber);
        /// <summary>歯番左下乳歯D</summary>
        public static readonly SimpleTooth LowerLeftDNormalNumber = new SimpleTooth(ToothNumber.LowerLeftD, ToothAttribute.NormalNumber);
        /// <summary>歯番左下乳歯E</summary>
        public static readonly SimpleTooth LowerLeftENormalNumber = new SimpleTooth(ToothNumber.LowerLeftE, ToothAttribute.NormalNumber);

        public static readonly SimpleTooth[] AllNormal = {
                                                                 UpperRight1NormalNumber
                                                                ,UpperRight2NormalNumber
                                                                ,UpperRight3NormalNumber
                                                                ,UpperRight4NormalNumber
                                                                ,UpperRight5NormalNumber
                                                                ,UpperRight6NormalNumber
                                                                ,UpperRight7NormalNumber
                                                                ,UpperRight8NormalNumber
                                                                ,UpperLeft1NormalNumber
                                                                ,UpperLeft2NormalNumber
                                                                ,UpperLeft3NormalNumber
                                                                ,UpperLeft4NormalNumber
                                                                ,UpperLeft5NormalNumber
                                                                ,UpperLeft6NormalNumber
                                                                ,UpperLeft7NormalNumber
                                                                ,UpperLeft8NormalNumber
                                                                ,LowerRight1NormalNumber
                                                                ,LowerRight2NormalNumber
                                                                ,LowerRight3NormalNumber
                                                                ,LowerRight4NormalNumber
                                                                ,LowerRight5NormalNumber
                                                                ,LowerRight6NormalNumber
                                                                ,LowerRight7NormalNumber
                                                                ,LowerRight8NormalNumber
                                                                ,LowerLeft1NormalNumber
                                                                ,LowerLeft2NormalNumber
                                                                ,LowerLeft3NormalNumber
                                                                ,LowerLeft4NormalNumber
                                                                ,LowerLeft5NormalNumber
                                                                ,LowerLeft6NormalNumber
                                                                ,LowerLeft7NormalNumber
                                                                ,LowerLeft8NormalNumber
                                                                ,UpperRightANormalNumber
                                                                ,UpperRightBNormalNumber
                                                                ,UpperRightCNormalNumber
                                                                ,UpperRightDNormalNumber
                                                                ,UpperRightENormalNumber
                                                                ,UpperLeftANormalNumber
                                                                ,UpperLeftBNormalNumber
                                                                ,UpperLeftCNormalNumber
                                                                ,UpperLeftDNormalNumber
                                                                ,UpperLeftENormalNumber
                                                                ,LowerRightANormalNumber
                                                                ,LowerRightBNormalNumber
                                                                ,LowerRightCNormalNumber
                                                                ,LowerRightDNormalNumber
                                                                ,LowerRightENormalNumber
                                                                ,LowerLeftANormalNumber
                                                                ,LowerLeftBNormalNumber
                                                                ,LowerLeftCNormalNumber
                                                                ,LowerLeftDNormalNumber
                                                                ,LowerLeftENormalNumber
                                                         };

        /// <summary>
        /// 歯の属性
        /// </summary>
        private ToothAttribute m_Attribute = ToothAttribute.None;

        public override bool IsLeaf
        {
            get
            {
                return true;
            }
        }

        public override bool IsContainer
        {
            get {
                return false;
            }
        }

        /// <summary>
        /// 歯の属性(ToothAttribute)を持つクラスかどうか
        /// </summary>
        public override bool HasToothAttribute
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 歯の属性(通常歯番、支台歯、過剰歯、隙等)
        /// </summary>
        public override ToothAttribute Attribute
        {
            get
            {
                return m_Attribute;
            }
        }

 

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        /// <remarks>デフォルトコンストラクタは使用不可とする</remarks>
        protected SimpleTooth()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">歯番</param>
        /// <param name="eikyushiNyushi">永久歯・乳歯の区別</param>
        /// <param name="attribute">単独のToothAttribute値</param>
        public SimpleTooth(ToothNumber number, ToothAttribute attribute)
            : base(number)
        {
            if (!number.IsSingle())
            {
                throw new ArgumentException("単独のToothNumber値ではない");
            }
            if (!attribute.IsSingle())
            {
                throw new ArgumentException("単独のToothAttribute値ではない");
            }
            m_Attribute = attribute;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">歯番</param>
        /// <param name="eikyushiNyushi">永久歯・乳歯の区別</param>
        /// <param name="attribute">単独のToothAttribute値</param>
        public SimpleTooth(ToothPosition position, EikyushiNyushiType eikyushiNyushi, ToothAttribute attribute)
            : this(ToothNumberHelper.GetNumber(position, eikyushiNyushi), attribute)
        {
        }

        /// <summary>
        /// 属性文字群
        /// </summary>
        protected static readonly char[] ATTRIBUTE_EXP_CHARS = { 'O', '@', '^', 'V', 'X', '\'', '.', '~', '_' };

        /// <summary>
        /// 属性の変換テーブル
        /// </summary>
        internal static readonly dynamic[] ATTRIBUTE_EXP_TABLE =
        {
            new { Attribute = ToothAttribute.Shidaishi,        Str = ATTRIBUTE_EXP_CHARS[0] },  // 'O'
            new { Attribute = ToothAttribute.KenzenShidaishi,  Str = ATTRIBUTE_EXP_CHARS[1] },  // '@'
            new { Attribute = ToothAttribute.Geki,             Str = ATTRIBUTE_EXP_CHARS[2] },  // '^'
            new { Attribute = ToothAttribute.Kajoshi,          Str = ATTRIBUTE_EXP_CHARS[3] },  // 'V'
            new { Attribute = ToothAttribute.EtcKessonshi,     Str = ATTRIBUTE_EXP_CHARS[4] },  // 'X'
            new { Attribute = ToothAttribute.EtcBunkatsushi,   Str = ATTRIBUTE_EXP_CHARS[5] },  // '\''
            new { Attribute = ToothAttribute.EtcZoshi,         Str = ATTRIBUTE_EXP_CHARS[6] },  // '.'
            new { Attribute = ToothAttribute.EtcKohason,       Str = ATTRIBUTE_EXP_CHARS[7] },  // '~'
            new { Attribute = ToothAttribute.EtcDummyOnZankon, Str = ATTRIBUTE_EXP_CHARS[8] },  // '_'
        };

        /// <summary>
        /// 部位文字列を返す
        /// </summary>
        /// <param name="position">歯番</param>
        /// <param name="eikyushiNyushi">永久歯・乳歯の区別</param>
        /// <param name="attribute">属性</param>
        /// <returns>部位文字列</returns>
        public static string ToString(ToothPosition position, EikyushiNyushiType eikyushiNyushi, ToothAttribute attribute)
        {
            // 未定義値を含む場合はエラーとする
            if (position == ToothPosition.None
                || eikyushiNyushi == EikyushiNyushiType.None
                || attribute == ToothAttribute.None)
            {
                // 例外処理
                throw new ApplicationException("歯番文字列化パラメーターエラー");
            }

            StringBuilder buiBuilder = new StringBuilder(5);

            // 上下額左右区分の文字設定
            HalfJaw halfJaw = position.GetHalfJaw();

            var queryHalfJaw = from x in HALF_JAW_TABLE where x.HalfJaw == halfJaw select x.Str;
            buiBuilder.Append(queryHalfJaw.Single());

            // 属性が通常歯番以外の場合は属性文字列を付加
            if (attribute != ToothAttribute.NormalNumber)
            {
                var query = from x in ATTRIBUTE_EXP_TABLE where x.Attribute == attribute select x.Str;
                buiBuilder.Append(query.Single());
            }

            // 永久歯
            if (eikyushiNyushi == EikyushiNyushiType.Eikyushi)
            {
                var query = from x in EIKYUSHI_TABLE where x.Position == position select x.Str;
                buiBuilder.Append(query.Single());
            }
            // 乳歯
            else
            {
                var query = from x in NYUSHI_TABLE where x.Position == position select x.Str;
                buiBuilder.Append(query.Single());
            }
            return buiBuilder.ToString();
        }

        /// <summary>
        /// 部位文字列を返す
        /// (Object.ToStringをオーバーライドする)
        /// </summary>
        /// <returns>部位文字列</returns>
        public override string ToString()
        {
            return ToString(Position, EikyushiNyushi, m_Attribute);
        }

        /// <summary>
        /// 部位文字列からSimpleToothクラスに変換する
        /// </summary>
        /// <param name="str">部位文字列</param>
        /// <returns>SimpleToothクラス</returns>
        public static SimpleTooth Parse(string str)
        {
            ToothPosition position = ToothPosition.None;
            EikyushiNyushiType eikyushiNyushi = EikyushiNyushiType.None;
            ToothAttribute attribute = ToothAttribute.None;
            if (str.Length == 0)
            {
                throw new ArgumentException("空の文字列が指定された");
            }
            // １文字目は"ULul"の内の１文字
            if (HALF_JAW_CHARS.Contains(str[0]))
            {
                char firstChar = str[0];    // 最初の文字

                // 上下額左右区分の取得
                var queryHalfJaw = from x in HALF_JAW_TABLE where x.Str == firstChar select x.HalfJaw;
                HalfJaw halfJaw = queryHalfJaw.Single();

                if (str.Length < 2)
                {
                    throw new ArgumentException("上下額左右区分の後に歯番または属性が無い");
                }

                // ２文字目は歯番 or 属性
                char nextChar = str[1];
                if (ATTRIBUTE_EXP_CHARS.Contains(nextChar))
                {
                    var query = from x in ATTRIBUTE_EXP_TABLE where x.Str == nextChar select x.Attribute;
                    attribute = query.Single();

                    if (str.Length < 3)
                    {
                        throw new ArgumentException("属性の後に歯番が無い");
                    }

                    nextChar = str[2];
                }
                else if(EIKYUSHI_CHARS.Contains(nextChar) || NYUSHI_CHARS.Contains(nextChar))
                {
                    // ２文字目が歯番ということは属性指定は省略→通常歯番
                    attribute = ToothAttribute.NormalNumber;
                }
                else
                {
                    throw new ApplicationException("ブロック指定の次の文字が属性でも歯番でも無い");
                }

                // 永久歯
                if (EIKYUSHI_CHARS.Contains(nextChar))
                {
                    eikyushiNyushi = EikyushiNyushiType.Eikyushi;

                    var query = from x in EIKYUSHI_TABLE where (x.HalfJaw == halfJaw && x.Str == nextChar) select x.Position;
                    position = query.Single();
                }
                // 乳歯
                else if (NYUSHI_CHARS.Contains(nextChar))
                {
                    eikyushiNyushi = EikyushiNyushiType.Nyushi;
                    
                    var query = from x in NYUSHI_TABLE where (x.HalfJaw == halfJaw && x.Str == nextChar) select x.Position;
                    position = query.Single();
                }
                // 永久歯、乳歯に該当しない
                else
                {
                    // 例外処理
                    throw new ArgumentException("歯番が1～8, A～Eのいずれでもない");
                }
            }
            else
            {
                // 例外処理
                throw new ArgumentException("上下左右顎指定が\'R\',\'L\',\r\',\'l\'のいずれでもない");
            }

            // Noneのままのパラメーターがあると、SimpleToothのコンストラクタ内で例外がスローされる
            return new SimpleTooth(position, eikyushiNyushi, attribute);
        }

        /// <summary>
        /// メンバーのクローンを作成する
        /// （ICloneableの実装）
        /// </summary>
        /// <returns>クローンオブジェクト</returns>
        public SimpleTooth Clone()
        {
            return (SimpleTooth)MemberwiseClone();
        }

        /// <summary>
        /// このインスタンスと、指定した部位情報の値が同一かどうかを判断する
        /// </summary>
        /// <param name="other">比較するSimpleTooth</param>
        /// <returns>部位情報が同じ場合はtrue、それ以外の場合はfalseを返す</returns>
        public override bool Equals(Bui other)
        {
            if (other is SimpleTooth)
            {
                SimpleTooth target = other as SimpleTooth;

                if (Number.Equals(target.Number))
                {
                    return m_Attribute == target.m_Attribute;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// このインスタンスと、指定した部位情報の値が同一かどうかを判断する
        /// </summary>
        /// <param name="other">比較するSimpleTooth</param>
        /// <returns> 部位情報が同じ場合はtrue、それ以外の場合はfalseを返す</returns>
        public override bool Equals(Object other)
        {
            // オブジェクトがBuiExp
            if (other is Bui)
            {
                return this.Equals((Bui)other);
            }
            // オブジェクトがSimpleTooth以外
            else
            {
                return base.Equals(other);
            }
        }




        /// <summary>
        /// このインスタンスと、指定した部位情報の大小関係を比較する
        /// </summary>
        /// <param name="other">比較対象の部位</param>
        /// <returns>
        /// this < other  : 負の値
        /// this == other : 0
        /// this > other  : 正の値
        /// </returns>
        public override int CompareTo(Bui other)
        {
            if(other.HasToothPosition){
                int result = Position.ToSerialNumber() - other.Position.ToSerialNumber();
                if (result != 0)
                {
                    return result;
                }
                else
                {
                    if (other.HasEikyushiNyushi)
                    {
                        result = (int)EikyushiNyushi - (int)other.EikyushiNyushi;
                        if (result != 0)
                        {
                            return result;
                        }
                        else
                        {
                            if (other.HasToothAttribute)
                            {
                                result = Attribute.ToSerialNumber() - other.Attribute.ToSerialNumber();
                                return result;
                            }
                            else
                            {
                                // アトリビュートの無い部位の方を先にする
                                return 1;
                            }
                        }
                    }
                    else
                    {
                        // 永久歯・乳歯の区別が無い方が先
                        return 1;
                    }
                }
            }
            else if(other is StringBui)
            {
                // 文字列部位は歯番より小さい（昇順なら先）ものとする
                return 1;
            }
            else
            {
                // さらに想定していない新たなクラスは差し当たり先にする
                return 1;
            }
        }

        public int CompareTo(SimpleTooth other)
        {
            return CompareTo(other as Bui);
        }

        /// <summary>
        /// 歯に属性をセットした新しいインスタンスを返す
        /// </summary>
        /// <param name="attribute">セットする属性</param>
        /// <returns>新しいSimpleToothインスタンス</returns>
        /// <remarks>付加と言っても属性は一個しか持てないので実際の動作は上書きとなる</remarks>
        public SimpleTooth SetAttribute(ToothAttribute attribute)
        {
            return new SimpleTooth(m_Number, attribute);
        }

        /// <summary>
        /// 部位情報の値が同一かどうかの比較
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>部位情報の値が同一の場合はtrue、それ以外の場合はfalse</returns>
        public static bool operator ==(SimpleTooth left, SimpleTooth right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            else if(!ReferenceEquals(left, null))
            {
                return left.Equals(right);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 部位情報の値が同一ではないかどうかの比較
        /// </summary>
        /// <param name="left">左辺値</param>
        /// <param name="right">右辺値</param>
        /// <returns>部位情報の値が同一でない場合はtrue、それ以外の場合はfalse</returns>
        public static bool operator !=(SimpleTooth left, SimpleTooth right)
        {
            return !(left == right);
        }

        /// <summary>
        ///  この部位のハッシュコードを返す
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode()
        {
            return EikyushiNyushi.GetHashCode() ^ Position.GetHashCode() ^ Attribute.GetHashCode();
        }
    }

    // 後で隣在歯の情報を持つSimpleToothのサブクラスを定義
    // 隙に使用予定
}
