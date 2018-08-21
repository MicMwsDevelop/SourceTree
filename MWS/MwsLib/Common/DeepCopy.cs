//
// DeepCopy.cs
// 
// DeepCopyクラス - コピー処理
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MwsLib.Common
{
	/// <summary>
	/// DeepCopyクラス
	/// </summary>
	public static class DeepCopy
    {
        /// <summary>
        /// ディープコピーを作成する。
        /// クローンするクラスには、SerializableAttribute属性[Serializable]、
        /// 不要なフィールドは、NonSerializedAttribute属性[NonSerialized]をつけること。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T CloneDeep<T>(this T target)
        {
            object clone = null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, target);
                stream.Position = 0;
                clone = formatter.Deserialize(stream);
            }
            return (T)clone;
        }
    }
}
