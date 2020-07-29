using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BaseFactory.Estore
{
	public class WebJucuTmp2 : WebJucu
	{
		public int 変更前Web受注No { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public WebJucuTmp2() : base()
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
	}
}
