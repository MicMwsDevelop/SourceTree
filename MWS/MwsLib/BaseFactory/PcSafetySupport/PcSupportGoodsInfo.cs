using System;

namespace MwsLib.BaseFactory.PcSafetySupport
{
	/// <summary>
	/// PC安心サポート商品情報
	/// </summary>
	[Serializable]
	public class PcSupportGoodsInfo : IEquatable<PcSupportGoodsInfo>
	{
		/// <summary>
		/// PC安心ｻﾎﾟｰﾄ(3年契約)                                   
		/// </summary>
		public const string PC_SUPPORT3_GOODS_ID = "001871";

		/// <summary>
		/// PC安心ｻﾎﾟｰﾄ(1年契約)                                   
		/// </summary>
		public const string PC_SUPPORT1_GOODS_ID = "001872";

		/// <summary>
		/// 商品ID
		/// </summary>
		public string GoodsID { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 料金
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// 契約年数の取得
		/// </summary>
		public int AgreeYear
		{
			get
			{
				if (PC_SUPPORT3_GOODS_ID == GoodsID)
				{
					return 3;
				}
				return 1;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PcSupportGoodsInfo()
		{
			GoodsID = string.Empty;
			GoodsName = string.Empty;
			Price = 0;
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するPcSupportGoodsInfo</param>
		/// <returns>判定</returns>
		public bool Equals(PcSupportGoodsInfo other)
		{
			if (null != other)
			{
				if (GoodsID != other.GoodsID)
					return false;
				if (GoodsName != other.GoodsName)
					return false;
				if (Price != other.Price)
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するPcSupportGoodsInfoオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is PcSupportGoodsInfo)
			{
				return this.Equals((PcSupportGoodsInfo)obj);
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

		/// <summary>
		/// 出力レコードの取得
		/// </summary>
		/// <returns>出力レコード</returns>
		public override string ToString()
		{
			return GoodsID + GoodsName + Price.ToString();
		}

		/// <summary>
		/// 契約年数の取得
		/// </summary>
		/// <param name="goodsID">商品ID</param>
		/// <returns>契約年数</returns>
		public static int GetAgreeYear(string goodsID)
		{
			if (PC_SUPPORT3_GOODS_ID == goodsID)
			{
				return 3;
			}
			return 1;
		}
	}
}
