//
// 部位基本抽象クラス
//
//   部位表現(BuiExp)に含まれる個々の歯番、文字列表現部位等を表す。
// 
// Copyright (C) MIC All Rights Reserved.
// 
using System;
using System.Collections;
using System.Collections.Generic;

namespace CommonLib.BuiData
{
	/// <summary>
	/// 部位基本抽象クラス
	/// </summary>
	/// <remarks>
	/// 子要素(子要素もまたBuiである)の列挙とEquals関数による同値比較が可能<br/>
	/// 子要素の列挙の際、子要素の子要素(孫要素)の再帰的な列挙は行わない<br/>
	/// その他下記の情報を取得する共通のメソッド・プロパティを定義<br/>
	/// 　子要素を持てるクラスかどうか<br/>
	/// 　歯番を持つクラスかどうか<br/>
	/// 　Buiの子要素を持てないクラス(ひとつの歯番、ひとつの歯番外部位など)かどうか<br/>
	/// </remarks>
	[Serializable]
    public abstract class Bui : IEnumerable<Bui>, IEquatable<Bui>, IComparable<Bui>
    {
        /// <summary>
        /// Buiの子要素を持つ(持てる)型かどうか
        /// </summary>
        /// <remarks>子要素を持てるクラスの子要素の数が空か１個か複数なのかはCountプロパティで判別する事</remarks>
        public abstract bool IsContainer { get; }

        public virtual int Count 
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Buiの子要素を持てないクラス(ひとつの歯番、ひとつの歯番外部位など)かどうか
        /// </summary>
        /// <remarks>子要素を持てるクラスの子要素の数が空か１個か複数なのかはCountプロパティで判別する事</remarks>
        public abstract bool IsLeaf { get; }

        /// <summary>
        /// 歯番(ToothPosition)を持つクラスかどうか
        /// </summary>
        public abstract bool HasToothPosition { get; }


        public virtual ToothPosition Position
        {
            get{
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 永久歯・乳歯の区別(EikyushiNyushiType)を持つクラスかどうか
        /// </summary>
        public abstract bool HasEikyushiNyushi { get; }


        public virtual EikyushiNyushiType EikyushiNyushi
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public bool HasToothNumber 
        {
            get
            {
                return HasEikyushiNyushi && HasToothPosition;
            }
        }

        public virtual ToothNumber Number 
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// 歯の属性(ToothAttribute)を持つクラスかどうか
        /// </summary>
        public abstract bool HasToothAttribute { get; }

        /// <summary>
        /// 歯の属性を返す
        /// </summary>
        public virtual ToothAttribute Attribute
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        //--- public

        /// <summary>
        /// コレクションを反復処理する列挙子を返します。
        /// </summary>
        /// <returns>NullEnumerater列挙子</returns>
        public virtual IEnumerator<Bui> GetEnumerator()
        {
            return new NullEnumerater();
        }

        /// <summary>
        /// このインスタンスと、指定した部位情報の値が同一かどうかを判断する<br/>
        /// 抽象メソッドのため、継承クラスで実装する
        /// </summary>
        /// <param name="other">比較するBui</param>
        /// <returns>部位情報が同じ場合はtrue、それ以外の場合はfalseを返す</returns>
        public abstract bool Equals(Bui other);

        /// <summary>
        /// 標準比較処理
        /// </summary>
        /// <remarks>
        /// 標準ソート順を下記の通り定める
        /// 　想定していない歯番なしクラス < 文字列部位 < 歯の位置ありの順
        /// 　歯番ありの場合の優先項目→1歯の位置,2永久歯・乳歯区分(永久歯・乳歯区分無しが先),3属性(属性なしが先)
        /// </remarks>
        /// <param name="other"></param>
        /// <returns></returns>
        public abstract int CompareTo(Bui other);

        /// <summary>
        /// コレクションを反復処理する列挙子を返す<br/> 
        /// (IEnumerable から継承される)
        /// </summary>
        /// <returns>NullEnumerater列挙子</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new NullEnumerater();
        }

        /// <summary>
        /// 子要素を持つ事が出来ないサブクラスのデフォルト動作用のIEnumerator実装
        /// </summary>
        class NullEnumerater : IEnumerator<Bui>, IEnumerator
        {
            /// <summary>
            /// 列挙子の現在位置にあるコレクション内の要素を取得する
            /// </summary>
            /// <remarks>使用されることはないので、実行するとNotImplementedExceptionが返る</remarks>
            /// <exception cref="NotImplementedException">実行されると必ず発生する</exception>
            Bui IEnumerator<Bui>.Current
            {
                // ここへは来ないはず
                get
                {
                    throw new NotImplementedException();
                }
            }

            /// <summary>
            /// コレクション内の現在の要素を取得する
            /// </summary>
            /// <remarks>使用されることはないので、実行するとNotImplementedExceptionが返る</remarks>
            /// <exception cref="NotImplementedException">実行されると必ず発生する</exception>
            object IEnumerator.Current
            {
                // ここへは来ないはず
                get
                {
                    throw new NotImplementedException();
                }
            }

            /// <summary>
            /// デフォルトコンストラクタ
            /// </summary>
            public NullEnumerater()
            {
            }

            //--- public

            /// <summary>
            /// 解放処理
            /// </summary>
            public void Dispose()
            {
            }

            /// <summary>
            /// 列挙子をコレクションの次の要素に進める
            /// </summary>
            /// <returns>falseが帰る</returns>
            public bool MoveNext()
            {
                return false;
            }

            /// <summary>
            /// 列挙子を初期位置、つまりコレクションの最初の要素の前に設定する
            /// </summary>
            /// <remarks>使用されることはないので、実行するとNotImplementedExceptionが返る</remarks>
            /// <exception cref="NotImplementedException">実行されると必ず発生する</exception>
            public void Reset()
            {
                // Reset()は定義せず下記の例外をスローしてよい。Help参照。
                throw new NotImplementedException();
            }


        }

    }
}
