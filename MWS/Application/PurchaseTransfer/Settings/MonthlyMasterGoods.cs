using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseTransfer.Settings
{
    /// <summary>
    /// 月次商品情報
    /// </summary>
	public class MonthlyMasterGoods
    {
        public string 商品コード;
        public string 仕入商品コード;
        public int 仕入価格;
        public string 仕入先;
        public string Palette商品コード;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public MonthlyMasterGoods()
        {
            商品コード = string.Empty;
            仕入商品コード = string.Empty;
            仕入価格 = 0;
            仕入先 = string.Empty;
            Palette商品コード = string.Empty;
        }
    }
}
