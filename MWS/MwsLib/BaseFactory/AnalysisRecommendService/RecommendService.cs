using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.DB;

namespace MwsLib.BaseFactory.AnalysisRecommendService
{
	/// <summary>
	/// おまとめプランサービス契約情報
	/// </summary>
	public class RecommendService
	{
		public int CustomerID { get; set; }
		public string CustomerName { get; set; }
		public List<Tuple<int, string>> ServiceList { get; set; }

		public int RecommendCount { get; set; }

		public RecommendService()
		{
			CustomerID = 0;
			CustomerName = string.Empty;
			ServiceList = new List<Tuple<int, string>>();
			RecommendCount = 0;
		}

		/// <summary>
		/// DataTable → データ変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static RecommendService DataTableToData(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				RecommendService data = new RecommendService();
				foreach (DataRow row in table.Rows)
				{
					if (0 == data.CustomerID)
					{
						data.CustomerID = DataBaseValue.ConvObjectToInt(row["fCustomerID"]);
					}
					int serviceID = DataBaseValue.ConvObjectToInt(row["fSERVICE_ID"]);
					string serviceName = row["fSERVICE_NAME"].ToString().Trim();
					data.ServiceList.Add(new Tuple<int, string>(serviceID, serviceName));
				}
				return data;
			}
			return null;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<RecommendService> DataTableToList(DataTable table)
		{
			List<RecommendService> result = new List<RecommendService>();
			if (null != table && 0 < table.Rows.Count)
			{
				RecommendService data = null;
				foreach (DataRow row in table.Rows)
				{
					int id = DataBaseValue.ConvObjectToInt(row["fCustomerID"]);
					if (null == data)
					{
						data = new RecommendService();
						data.CustomerID = id;
					}
					else if (data.CustomerID != id)
					{
						result.Add(data);
						data = new RecommendService();
						data.CustomerID = id;
					}
					int serviceID = DataBaseValue.ConvObjectToInt(row["fSERVICE_ID"]);
					string serviceName = row["fSERVICE_NAME"].ToString().Trim();
					data.ServiceList.Add(new Tuple<int, string>(serviceID, serviceName));
				}
				result.Add(data);
			}
			return result;
		}

		public static void SetRecommndCount(RecommendService target, List<RecommendService> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].CustomerID != target.CustomerID)
				{
					int recommndCount = 0;
					for (int j = 0; j < target.ServiceList.Count; j++)
					{
						if (-1 != list[i].ServiceList.FindIndex(p => p.Item1 == target.ServiceList[j].Item1))
						{
							recommndCount++;
						}
					}
					list[i].RecommendCount = recommndCount;
				}
			}
		}
	}
}
