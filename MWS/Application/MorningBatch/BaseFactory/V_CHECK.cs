using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MorningBatch.BaseFactory
{
	public class V_CHECK
	{
		public int 伝票No { get; set; }
		public int 販売先顧客ID { get; set; }
		public int ユーザー顧客ID { get; set; }
		public string 担当者ID { get; set; }
		public string 担当者名 { get; set; }
		public string 担当支店ID { get; set; }
		public string 担当支店名 { get; set; }
		public DateTime? 受注年月日 { get; set; }
		public DateTime? 受注承認日 { get; set; }
		public DateTime? 売上承認日 { get; set; }
		public string 商品コード { get; set; }
		public string 商品名 { get; set; }
		public short 商品区分 { get; set; }
		public int 数量 { get; set; }
		public int 販売価格 { get; set; }
		public int 申込種別 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public V_CHECK()
		{
			伝票No = 0;
			販売先顧客ID = 0;
			ユーザー顧客ID = 0;
			担当者ID = string.Empty;
			担当者名 = string.Empty;
			担当支店ID = string.Empty;
			担当支店名 = string.Empty;
			受注年月日 = null;
			受注承認日 = null;
			売上承認日 = null;
			商品コード = string.Empty;
			商品名 = string.Empty;
			商品区分 = 0;
			数量 = 0;
			販売価格 = 0;
			申込種別 = 0;
		}

		/// <summary>
		/// 詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>V_CHECK</returns>
		public static List<V_CHECK> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<V_CHECK> result = new List<V_CHECK>();
				foreach (DataRow row in table.Rows)
				{
					V_CHECK data = new V_CHECK
					{
						伝票No = DataBaseValue.ConvObjectToInt(row["伝票No"]),
						販売先顧客ID = DataBaseValue.ConvObjectToInt(row["販売先顧客ID"]),
						ユーザー顧客ID = DataBaseValue.ConvObjectToInt(row["ユーザー顧客ID"]),
						担当者ID = row["担当者ID"].ToString().Trim(),
						担当者名 = row["担当者名"].ToString().Trim(),
						担当支店ID = row["担当支店ID"].ToString().Trim(),
						担当支店名 = row["担当支店名"].ToString().Trim(),
						受注年月日 = DataBaseValue.ConvObjectToDateTimeNull(row["受注年月日"]),
						受注承認日 = DataBaseValue.ConvObjectToDateTimeNull(row["受注承認日"]),
						売上承認日 = DataBaseValue.ConvObjectToDateTimeNull(row["売上承認日"]),
						商品コード = row["商品コード"].ToString().Trim(),
						商品名 = row["商品名"].ToString().Trim(),
						商品区分 = DataBaseValue.ConvObjectToShort(row["商品区分"]),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						販売価格 = DataBaseValue.ConvObjectToInt(row["販売価格"]),
						申込種別 = DataBaseValue.ConvObjectToInt(row["申込種別"])
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
