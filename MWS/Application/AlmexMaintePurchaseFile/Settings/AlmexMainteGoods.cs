//
// AlmexMainteGoods.cs
// 
// アルメックス保守サービス商品情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/01/06 勝呂)
//
namespace AlmexMaintePurchaseFile.Settings
{
	/// <summary>
	/// アルメックス保守サービス商品情報
	/// </summary>
	public class AlmexMainteGoods
	{
        public string 商品コード { get; set; }
        public string 商品名 { get; set; }
        public string 仕入商品コード { get; set; }
        public int 仕入価格 { get; set; }
        public int 仕入数 { get; set; }
        public string 仕入先 { get; set; }
        public short 仕入フラグ { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public AlmexMainteGoods()
        {
            商品コード = string.Empty;
            商品名 = string.Empty;
            仕入商品コード = string.Empty;
            仕入価格 = 0;
            仕入数 = -1;
            仕入先 = string.Empty;
            仕入フラグ = 0;
        }
    }
}
