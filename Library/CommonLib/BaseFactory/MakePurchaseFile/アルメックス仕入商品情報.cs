//
// アルメックス仕入商品情報.cs
// 
// アルメックス保守サービス仕入商品情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/07 勝呂)
//
namespace CommonLib.BaseFactory.MakePurchaseFile
{
	/// <summary>
	/// アルメックス保守サービス仕入商品情報
	/// </summary>
	public class アルメックス仕入商品情報
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
        public アルメックス仕入商品情報()
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
