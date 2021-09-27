//
// 文字列表現部位クラス
// 
// 部位を文字列で表現する（旧歯番外部位）
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;

namespace CommonLib.BuiData
{
	/// <summary>
	/// 文字列表現部位（旧歯番外部位）クラス
	/// </summary>
	[Serializable]
    public class StringBui : Bui, IComparable<StringBui>
    {
        /// <summary>
        /// 文字列表現部位の先頭文字
        /// </summary>
        public const char HEADER_CHAR = '!';
        
        /// <summary>
        /// 文字列表現
        /// </summary>
        public string Expression { get; private set; }
        
        /// <summary>
        /// Buiの子要素を持たない(持てない)型(ひとつの歯番、ひとつの歯番外部位など)かどうか
        /// </summary>
        public override bool IsLeaf
        {
            get
            {
                return true;
            }
        }

        public override bool IsContainer
        {
            get
            {
                return false;
            }
        }

        public override bool HasEikyushiNyushi
        {
            get {
                return false;
            }
        }


        public override bool HasToothPosition
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

        // デフォルトコンストラクタは使用不可とする
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        protected StringBui()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="expression"></param>
        public StringBui(string expression)
        {
            Expression = expression;
        }

        /// <summary>
        /// 文字列表現部位を返す<br/>
        /// (Object.ToStringをオーバーライドする)
        /// </summary>
        /// <returns>文字列表現部位</returns>
        public override string ToString()
        {
            return HEADER_CHAR + Expression;
        }

        /// <summary>
        /// 文字列表現部位からStringBuiクラスに変換する
        /// </summary>
        /// <param name="str">文字列表現部位</param>
        /// <returns>StringBuiクラス</returns>
        public static StringBui Parse(string expression)
        {
            return new StringBui(expression.Substring(1));
        }

        /// <summary>
        /// メンバーのクローンを作成する
        /// （ICloneableの実装）
        /// </summary>
        /// <returns>クローンオブジェクト</returns>
        public StringBui Clone()
        {
            return (StringBui)MemberwiseClone();
        }

        /// <summary>
        /// このインスタンスと、指定した部位の値が同一かどうかを判断する
        /// </summary>
        /// <param name="other">比較する部位</param>
        /// <returns>文字列表現部位が同じ場合はtrue、それ以外の場合はfalseを返す</returns>
        public override bool Equals(Bui other)
        {
            if (other is StringBui)
            {
                StringBui target = (StringBui)other;

                return Expression == target.Expression;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// このインスタンスと、指定した文字列表現部位の値が同一かどうかを判断する
        /// </summary>
        /// <param name="other">比較するStringBui</param>
        /// <returns>文字列表現部位が同じ場合はtrue、それ以外の場合はfalseを返す</returns>
        public override bool Equals(object other)
        {
            if (other is StringBui)
            {
                return Equals(other as StringBui);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Expression.GetHashCode();
        }

        /// <summary>
        /// 文字列表現部位の大小比較
        /// </summary>
        /// <param name="other">比較対象の部位</param>
        /// <returns>
        /// this < other  : 負の値
        /// this == other : 0
        /// this > other  : 正の値
        /// </returns>
        public override int CompareTo(Bui other)
        {
            if (other is StringBui)
            {
                // 文字列表現部位同士の比較
                return CompareTo(other as StringBui);
            }
            else if (other is SimpleTooth)
            {
                //歯番は文字列表現部位はより大きい（昇順なら後）ものとする
                return -1;
            }
            else
            {
                // 歯番、文字列部位以外とStringBuiの比較は未定義
                throw new NotImplementedException();
            }
        }

        public int CompareTo(StringBui other)
        {
            // 文字列表現部位同士の比較
            return Expression.CompareTo(other.Expression);
        }

    }
}
