using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace MorningBatch.BaseFactory
{
	public class V_CHECK_Service
	{
		public string BRAND_CODE { get; set; }
		public int SERVICE_TYPE_ID { get; set; }
		public string SERVICE_TYPE_NAME { get; set; }
		public int SERVICE_ID { get; set; }
		public string SERVICE_NAME { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public V_CHECK_Service()
		{
			BRAND_CODE = string.Empty;
			SERVICE_TYPE_ID = 0;
			SERVICE_TYPE_NAME = string.Empty;
			SERVICE_ID = 0;
			SERVICE_NAME = string.Empty;
		}

		/// <summary>
		/// 詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>V_CHECK_Service</returns>
		public static List<V_CHECK_Service> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<V_CHECK_Service> result = new List<V_CHECK_Service>();
				foreach (DataRow row in table.Rows)
				{
					V_CHECK_Service data = new V_CHECK_Service
					{
						BRAND_CODE = row["BRAND_CODE"].ToString().Trim()
						SERVICE_TYPE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_TYPE_ID"]),
						SERVICE_TYPE_NAME = row["SERVICE_TYPE_NAME"].ToString().Trim()
						SERVICE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]),
						SERVICE_NAME = row["SERVICE_NAME"].ToString().Trim()
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
