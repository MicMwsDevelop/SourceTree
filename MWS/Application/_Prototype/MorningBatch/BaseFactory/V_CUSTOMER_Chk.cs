using CommonLib.DB;
using System.Data;

namespace MorningBatch.BaseFactory
{
	public class V_CUSTOMER_Chk
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int 顧客ID { get; set; }

		/// <summary>
		/// 顧客名1
		/// </summary>
		public string 顧客名1 { get; set; }

		/// <summary>
		/// 顧客名2
		/// </summary>
		public string 顧客名2 { get; set; }

		/// <summary>
		/// 登録カード回収日
		/// </summary>
		public string 登録カード回収日 { get; set; }

		/// <summary>
		/// 顧客名の取得
		/// </summary>
		public string 顧客名
		{
			get
			{
				return 顧客名1 + 顧客名2;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public V_CUSTOMER_Chk()
		{
			顧客ID = 0;
			顧客名1 = string.Empty;
			顧客名2 = string.Empty;
			登録カード回収日 = string.Empty;
		}

		/// <summary>
		/// 詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>V_CUSTOMER_Chk</returns>
		public static V_CUSTOMER_Chk DataTableToList(DataTable table)
		{
			if (null != table && 0 == table.Rows.Count)
			{
				DataRow row = table.Rows[0];
				V_CUSTOMER_Chk data = new V_CUSTOMER_Chk
				{
					顧客ID = DataBaseValue.ConvObjectToInt(row["顧客ID"]),
					顧客名1 = row["顧客名1"].ToString().Trim(),
					顧客名2 = row["顧客名2"].ToString().Trim(),
					登録カード回収日 = row["登録カード回収日"].ToString().Trim(),
				};
				return data;
			}
			return null;
		}
	}
}
