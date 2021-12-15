using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.BaseFactory.PurchaseTransfer
{
	/// <summary>
	/// 5-2 りすとん仕入振替月次合計行追加
	/// 5-3 りすとん仕入振替月次追加
	/// 6-2 問心伝仕入振替月次合計行追加
	/// 6-3 問心伝仕入振替月次追加
	/// </summary>
	public class 仕入振替月次追加
	{
		public string sykd_jbmn { get; set; }
		public string sykd_jtan { get; set; }
		public string sykd_scd { get; set; }
		public string sykd_mkbn { get; set; }
		public string sykd_mei { get; set; }
		public int 数量 { get; set; }
		public string sykd_tani { get; set; }
		public int 評価単価 { get; set; }
		public int sykd_rate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 仕入振替月次追加()
		{
			sykd_jbmn = string.Empty;
			sykd_jtan = string.Empty;
			sykd_scd = string.Empty;
			sykd_mkbn = string.Empty;
			sykd_mei = string.Empty;
			数量 = 0;
			sykd_tani = string.Empty;
			評価単価 = 0;
			sykd_rate = 0;
		}
	}
}
