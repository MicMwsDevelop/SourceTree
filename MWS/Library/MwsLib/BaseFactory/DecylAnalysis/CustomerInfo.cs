using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;
using MwsLib.BaseFactory;

namespace MwsLib.BaseFactory.DecylAnalysis
{
	public class CustomerInfo
	{
		public string CustomerNo { get; set; }
		public string TokuisakiCode { get; set; }

		public string CustomerName { get; set; }

		public MwsDefine.ApplyType Apply { get; set; }

		public List<UseServiceInfo> UseServiceList { get; set; }

		public CustomerInfo()
		{
			CustomerNo = string.Empty;
			TokuisakiCode = string.Empty;
			CustomerName = string.Empty;
			Apply = MwsDefine.ApplyType.Etc;
			UseServiceList = new List<UseServiceInfo>();
		}
	}


	public class UseServiceInfo
	{
		public string ServiceID { get; set; }
		public string ServiceName { get; set; }
		public Span UseSpan { get; set; }

		public int Price { get; set; }

		public UseServiceInfo()
		{
			ServiceID = string.Empty;
			ServiceName = string.Empty;
			UseSpan = Span.Nothing;
			Price = 0;
		}
	}

}
