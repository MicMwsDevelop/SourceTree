using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipManage
{
	public class Hassousaki
	{
		/// <summary>
		/// 発送先顧客Ｎｏ．
		/// </summary>
		public string ClientNumber { get; set; }

		/// <summary>
		/// 発送先顧客名
		/// </summary>
		public string ClientName { get; set; }

		/// <summary>
		/// 発送先郵便番号
		/// </summary>
		public string ZipCode { get; set; }

		/// <summary>
		/// 発送先住所
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// 発送先電話番号
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// 発送先ＦＡＸ番号
		/// </summary>
		public string FAXNumber { get; set; }

		/// <summary>
		/// ＰＣＡ得意先情報
		/// </summary>
		public string PCA_Tokuisaki { get; set; }

		/// <summary>
		/// ＰＣＡ請求先情報
		/// </summary>
		public string PCA_Seikyuusaki { get; set; }

		/// <summary>
		/// 納期
		/// </summary>
		public string Nouki { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public Hassousaki()
		{
		}
	}
}
