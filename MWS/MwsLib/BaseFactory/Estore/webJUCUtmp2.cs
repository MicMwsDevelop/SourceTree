using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BaseFactory.Estore
{
	public class WebJUCUtmp2 : WebJUCUtmp
	{
		public int 変更前Web受注No { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public WebJUCUtmp2() : base()
		{
			this.Clear();
		}

		/// <summary>
		/// クリア
		/// </summary>
		public override void Clear()
		{
			base.Clear();
			変更前Web受注No = 0;
		}

		/// <summary>
		/// Create Table 定義文字列の取得
		/// </summary>
		/// <returns>定義文字列</returns>
		public static string CreateTableWebJUCUtmp2()
		{
			return string.Format("{0},変更前Web受注No int,", WebJUCUtmp.CreateTableWebJUCUtmp());
		}
	}
}
