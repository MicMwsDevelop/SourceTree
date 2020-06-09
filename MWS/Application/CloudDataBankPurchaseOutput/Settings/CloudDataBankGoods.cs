using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudDataBankPurchaseOutput.Settings
{
    /// <summary>
    /// クラウドデータバンク商品情報
    /// </summary>
	public class CloudDataBankGoods
	{
        public string 商品コード;
        public string 仕入商品コード;
        public int 仕入価格;
        public string 仕入先;
        public string 商品名;
        public short 仕入フラグ;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public CloudDataBankGoods()
        {
            商品コード = string.Empty;
            仕入商品コード = string.Empty;
            仕入価格 = 0;
            仕入先 = string.Empty;
            商品名 = string.Empty;
            仕入フラグ = 0;
        }
    }
}
