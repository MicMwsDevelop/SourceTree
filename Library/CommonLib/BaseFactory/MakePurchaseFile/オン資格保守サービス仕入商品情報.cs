//
// オン資格保守サービス仕入商品情報.cs
// 
// オン資格保守サービス仕入商品情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
//

using System;

namespace CommonLib.BaseFactory.MakePurchaseFile
{
	public class オン資格保守サービス仕入商品情報 : ICloneable, IEquatable<オン資格保守サービス仕入商品情報>
	{
		public string 商品コード { get; set; }
		public string 商品名 { get; set; }
		public int 仕入価格 { get; set; }
		public string 仕入先 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public オン資格保守サービス仕入商品情報()
		{
			商品コード = string.Empty;
			商品名 = string.Empty;
			仕入価格 = 0;
			仕入先 = string.Empty;
		}

		/// <summary>
		/// メンバーのクローンを作成する
		/// （ICloneableの実装）
		/// </summary>
		/// <returns>クローンオブジェクト</returns>
		public Object Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>
		/// このインスタンスと、指定した環境設定クラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(オン資格保守サービス仕入商品情報 other)
		{
			if (other != null)
			{
				if (商品コード == other.商品コード
					&& 商品名 == other.商品名
					&& 仕入価格 == other.仕入価格
					&& 仕入先 == other.仕入先)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is オン資格保守サービス仕入商品情報)
			{
				return this.Equals((オン資格保守サービス仕入商品情報)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// ハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}
}
