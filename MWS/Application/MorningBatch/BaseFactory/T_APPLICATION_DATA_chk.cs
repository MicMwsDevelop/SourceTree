using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorningBatch.BaseFactory
{
	public class T_APPLICATION_DATA_chk
	{
		public int CUSTOMER_ID { get; set; }
		public int SERVICE_TYPE_ID { get; set; }
		public int SERVICE_ID { get; set; }

		public T_APPLICATION_DATA_chk()
		{
			CUSTOMER_ID = 0;
			SERVICE_TYPE_ID = 0;
			SERVICE_ID = 0;
		}

		/// <summary>
		/// 詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>V_CHECK_Service</returns>
		public static List<T_APPLICATION_DATA_chk> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_APPLICATION_DATA_chk> result = new List<T_APPLICATION_DATA_chk>();
				foreach (DataRow row in table.Rows)
				{
					T_APPLICATION_DATA_chk data = new T_APPLICATION_DATA_chk
					{
						CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
						SERVICE_TYPE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_TYPE_ID"]),
						SERVICE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
