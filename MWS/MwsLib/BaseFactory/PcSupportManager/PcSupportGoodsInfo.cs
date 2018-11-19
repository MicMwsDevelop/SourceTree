//
// PcSupportGoodsInfo.cs
//
// PC安心サポート商品情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
// 
namespace MwsLib.BaseFactory.PcSupportManager
{
	/// <summary>
	/// PC安心サポート商品情報
	/// </summary>
	public class PcSupportGoodsInfo
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
