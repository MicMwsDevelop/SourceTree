using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BaseFactory.DecylAnalysis
{
	public class GoodsMaster
	{
		public string ServiceID { get; set; }

		public string GoodsID { get; set; }

		public string GoodsName { get; set; }

		public int GoodsPrice { get; set; }

		public GoodsMaster()
		{
			ServiceID = string.Empty;
			GoodsID = string.Empty;
			GoodsName = string.Empty;
			GoodsPrice = 0;
		}
	}
}
