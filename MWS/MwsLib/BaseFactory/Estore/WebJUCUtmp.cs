using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BaseFactory.Estore
{
	public class WebJUCUtmp : WebGeneralData
	{
		public int order_accept_id { get; set; }

		public DateTime? 希望着日 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public WebJUCUtmp() : base()
		{
			this.Clear();
		}

		/// <summary>
		/// クリア
		/// </summary>
		public override void Clear()
		{
			base.Clear();
			order_accept_id = 0;
			希望着日 = null;
		}

		/// <summary>
		/// Create Table 定義文字列の取得
		/// </summary>
		/// <returns>定義文字列</returns>
		public static string CreateTableWebJUCUtmp()
		{
			return string.Format("order_accept_id int,{0},希望着日 datetime,", WebGeneralData.CreateTableWebGeneralData());
		}
	}
}
