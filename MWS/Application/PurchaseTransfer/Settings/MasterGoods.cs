using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseTransfer.Settings
{
	/// <summary>
	/// 商品情報
	/// </summary>
	public class MasterGoods
	{
		public string Palette商品コード;
		public string 仕入商品コード;

		public MasterGoods()
		{
			Palette商品コード = string.Empty;
			仕入商品コード = string.Empty;
		}
	}
}
