using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBackupPurchaseFile.Settings
{
    /// <summary>
    /// クラウドバックアップ商品情報
    /// </summary>
	public class CloudBackupGoods
	{
        public string 商品コード { get; set; }
        public string 仕入商品コード { get; set; }
        public int 仕入価格 { get; set; }
        public string 仕入先 { get; set; }
        public string 商品名 { get; set; }
        public short 仕入フラグ { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public CloudBackupGoods()
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
