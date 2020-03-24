using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BaseFactory.StockGoodsMaster
{
	/// <summary>
	/// 仕入商品マスタ情報
	/// </summary>
	public class StockGoodsMasterData
	{
		/// <summary>
		/// 
		/// </summary>
		public string 商品コード { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 仕入商品コード { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int 仕入価格 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 仕入先 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 商品名 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool 仕入フラグ { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public StockGoodsMasterData()
		{
			商品コード = string.Empty;
			仕入商品コード = string.Empty;
			仕入価格 = 0;
			仕入先 = string.Empty;
			商品名 = string.Empty;
			仕入フラグ = false;
		}
	}
}
