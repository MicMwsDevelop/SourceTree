using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BaseFactory.PurchaseTransfer
{
	public class 対象月社内仕入れ伝票
	{
		public int 仕入日 { get; set; }
		public int 伝票No { get; set; }
		public string 仕入先コード { get; set; }

		public 対象月社内仕入れ伝票()
		{
			仕入日 = 0;
			伝票No = 0;
			仕入先コード = string.Empty;
		}
	}
}
