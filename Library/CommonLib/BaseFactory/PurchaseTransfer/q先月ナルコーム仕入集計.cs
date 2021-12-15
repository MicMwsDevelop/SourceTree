using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.BaseFactory.PurchaseTransfer
{
	/// <summary>
	/// 1 q先月ナルコーム仕入集計
	/// </summary>
	public class q先月ナルコーム仕入集計
	{
		public string 仕入先 { get; set; }
		public string sykd_jbmn { get; set; }
		public string sykd_jtan { get; set; }
		public string sykd_scd { get; set; }
		public string sykd_mkbn { get; set; }
		public string sykd_mei { get; set; }
		public int 数量 { get; set; }
		public string sykd_tani { get; set; }
		public int 評価単価 { get; set; }
		public int sykd_uribi { get; set; }
		public int 仕入フラグ { get; set; }
		public int sykd_rate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public q先月ナルコーム仕入集計()
		{
			sykd_jbmn = string.Empty;
			sykd_jtan = string.Empty;
			sykd_scd = string.Empty;
			sykd_mkbn = string.Empty;
			sykd_mei = string.Empty;
			数量 = 0;
			sykd_tani = string.Empty;
			評価単価 = 0;
			sykd_uribi = 0;
			仕入フラグ = 0;
			sykd_rate = 0;
		}
	}
}
