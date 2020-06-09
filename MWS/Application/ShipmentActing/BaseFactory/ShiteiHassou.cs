using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentActing.BaseFactory
{
	public class ShiteiHassou
	{
        public string 顧客名 { get; set; }
        public int 伝票番号 { get; set; }
        public string 品名 { get; set; }
        public string 得意先No { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public ShiteiHassou()
        {
            顧客名 = string.Empty;
            伝票番号 = 0;
            品名 = string.Empty;
            得意先No = string.Empty;
        }
    }
}
