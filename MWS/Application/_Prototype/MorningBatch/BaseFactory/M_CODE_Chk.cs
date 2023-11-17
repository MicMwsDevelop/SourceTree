using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace MorningBatch.BaseFactory
{
	public class M_CODE_Chk
	{
		public string GOODS_ID { get; set; }
		public int SERVICE_TYPE_ID { get; set; }
		public int SERVICE_ID { get; set; }
		public string SERVICE_NAME { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public M_CODE_Chk()
		{
			GOODS_ID = string.Empty;
			SERVICE_TYPE_ID = 0;
			SERVICE_ID = 0;
			SERVICE_NAME = string.Empty;
		}

		/// <summary>
		/// 詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>M_CODE_Chk</returns>
		public static M_CODE_Chk DataTableToList(DataTable table)
		{
			if (null != table && 0 == table.Rows.Count)
			{
				DataRow row = table.Rows[0];
				M_CODE_Chk data = new M_CODE_Chk
				{
					GOODS_ID = row["GOODS_ID"].ToString().Trim(),
					SERVICE_TYPE_ID = DataBaseValue.ConvObjectToInt(row["C.SERVICE_TYPE_ID"]),
					SERVICE_ID = DataBaseValue.ConvObjectToInt(row["C.SERVICE_ID"]),
					SERVICE_NAME = row["SERVICE_NAME"].ToString().Trim()
				};
				return data;
			}
			return null;
		}
	}
}
