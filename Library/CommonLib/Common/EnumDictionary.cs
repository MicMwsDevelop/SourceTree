//
// Enum専用のDictionaryクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CommonLib.Common
{
    /// <summary>
    /// Enum専用Dictionary(内部Enumをintに変換して管理)
    /// </summary>
    /// <typeparam name="K">キー(Enum)</typeparam>
    /// <typeparam name="V">対応する値</typeparam>
    [Serializable]
    public class EnumDictionary<K, V> : Dictionary<int, V>, IEnumerable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EnumDictionary()
            : base()
        {
        }

        /// <summary>
        /// コンストラクタ<br/>
        /// ※逆シリアライズで必要
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected EnumDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 指定したキーより対応する値を取得
        /// </summary>
        /// <param name="key">キー(Enum)</param>
        /// <returns>対応する値</returns>
        public V this[K key]
        {
            get
            {
                return this[Convert.ToInt32(key)];
            }
            set
            {
                this[Convert.ToInt32(key)] = value;
            }
        }

        /// <summary>
        /// 指定した値より対応するキーを取得
        /// </summary>
        /// <param name="Value">値</param>
        /// <returns>対応するキー(Enum) 無い場合には、キー定義のデフォルト</returns>
        public K GetKeyFirstOrDefault(V value)
        {
            K key = default(K);
            foreach (KeyValuePair<K, V> item in this)
            {
                if (item.Value.Equals(value))
                {
                    key = item.Key;
                    break;
                }
            }
            return key;
        }

        /// <summary>
        /// KeyCollection取得
        /// </summary>
        public new EnumDictionary<K, V>.KeyCollection Keys
        {
            get
            {
                return new EnumDictionary<K, V>.KeyCollection(this);
            }
        }

        /// <summary>
        /// 指定したキーが含まれているか確認
        /// </summary>
        /// <param name="key">キー(Enum)</param>
        /// <returns>true:有 false:無</returns>
        public bool ContainsKey(K key)
        {
            return base.Keys.Contains(Convert.ToInt32(key));
        }

        /// <summary>
        /// 指定したキーに対応する値を取得
        /// </summary>
        /// <param name="key">キー(Enum)</param>
        /// <param name="val">対応する値</param>
        /// <returns>true:有 false:無</returns>
        public bool TryGetValue(K key, out V value)
        {
            return base.TryGetValue(Convert.ToInt32(key), out value);
        }

        /// <summary>
        /// 追加処理
        /// </summary>
        /// <param name="key">キー(Enum)</param>
        /// <param name="value">対応する値</param>
        public void Add(K key, V value)
        {
            this.Add(Convert.ToInt32(key), value);
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="key">キー(Enum)</param>
        public bool Remove(K key)
        {
            return this.Remove(Convert.ToInt32(key));
        }

        /// <summary>
        /// Enumerator取得処理(foreach用)
        /// </summary>
        /// <returns>Enumerator</returns>
        public new IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return new EnumDictionary<K, V>.Enumerator(base.GetEnumerator());
        }

        /// <summary>
        /// オブジェクト取得<br/>
        /// ※逆シリアライズで必要
        /// </summary>
        /// <param name="si"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo si, StreamingContext context)
        {
            base.GetObjectData(si, context);
        }

        //////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 独自KeyCollectionクラス
        /// </summary>
        public new class KeyCollection : List<K>
        {
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="dictionary"></param>
            public KeyCollection(EnumDictionary<K, V> dictionary)
            {
                foreach (KeyValuePair<K, V> item in dictionary)
                {
                    // Keyは、Enumeratorで変換
                    this.Add(item.Key);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 独自Enumeratorクラス
        /// </summary>
        private new class Enumerator : IEnumerator<KeyValuePair<K, V>>, IDisposable
        {
            /// <summary>
            /// コレクション
            /// </summary>
            private List<KeyValuePair<K, V>> list = new List<KeyValuePair<K, V>>();

            /// <summary>
            /// カレント値
            /// </summary>
            private KeyValuePair<K, V> curItem;

            /// <summary>
            /// インデックス
            /// </summary>
            private int index;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="dictionary"></param>
            public Enumerator(Dictionary<int, V>.Enumerator dictionary)
            {
                while (dictionary.MoveNext())
                {
                    KeyValuePair<int, V> obj = dictionary.Current;

                    // Key値を変換する
                    K key = (K)Enum.ToObject(typeof(K), obj.Key);

                    // コレクション追加
                    list.Add(new KeyValuePair<K, V>(key, obj.Value));
                }

                index = -1;
                curItem = default(KeyValuePair<K, V>);
            }

            /// <summary>
            /// コレクション内の現在の要素を取得
            /// </summary>
            public KeyValuePair<K, V> Current
            {
                get { return curItem; }
            }

            /// <summary>
            /// コレクション内の現在の要素を取得
            /// </summary>
            object IEnumerator.Current
            {
                get { return Current; }
            }

            /// <summary>
            /// 列挙子をコレクションの次の要素に進める
            /// </summary>
            public bool MoveNext()
            {
                index = index + 1;

                if (list.Count <= index)
                {
                    return false;
                }
                curItem = list[index];
                return true;
            }

            /// <summary>
            /// 列挙子を初期位置に戻す
            /// </summary>
            public void Reset()
            {
                index = -1;
            }

            /// <summary>
            /// Dispose
            /// </summary>
            void IDisposable.Dispose() { }
        }
    }
}