using MwsLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.Estore.View
{
	/// <summary>
	/// estore 注文場情報
	/// </summary>
	public class vMicOrder_accept
	{
		/// <summary>
		/// 受付番号
		/// </summary>
		public int order_accept_id { get; set; }

		/// <summary>
		/// 注文番号
		/// </summary>
		public int order_no { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int customer_no { get; set; }

		/// <summary>
		/// 着日指定
		/// </summary>
		public DateTime? pref_arrival_date { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public string goods_code { get; set; }

		/// <summary>
		/// 金額
		/// </summary>
		public int web_price { get; set; }

		/// <summary>
		/// 注文順
		/// </summary>
		public int order_num { get; set; }

		/// <summary>
		/// 注文日時
		/// </summary>
		public DateTime order_dt { get; set; }

		/// <summary>
		/// 送信日時
		/// </summary>
		public DateTime send_dt { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vMicOrder_accept()
		{
			order_accept_id = 0;
			order_no = 0;
			customer_no = 0;
			pref_arrival_date = null;
			goods_code = string.Empty;
			web_price = 0;
			order_num = 0;
			order_dt = DateTime.MinValue;
			send_dt = DateTime.MinValue;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMicOrder_accept> DataTableToList(DataTable table)
		{
			List<vMicOrder_accept> result = new List<vMicOrder_accept>();
			foreach (DataRow row in table.Rows)
			{
				vMicOrder_accept data = new vMicOrder_accept
				{
					order_accept_id = DataBaseValue.ConvObjectToInt(row["order_accept_id"]),
					order_no = DataBaseValue.ConvObjectToInt(row["order_no"]),
					customer_no = DataBaseValue.ConvObjectToInt(row["customer_no"]),
					pref_arrival_date = DataBaseValue.ConvObjectToDateTimeNull(row["pref_arrival_date"]),
					goods_code = row["goods_code"].ToString().Trim(),
					web_price = DataBaseValue.ConvObjectToInt(row["web_price"]),
					order_num = DataBaseValue.ConvObjectToInt(row["order_num"]),
					order_dt = DataBaseValue.ConvObjectToDateTime(row["order_dt"]),
					send_dt = DataBaseValue.ConvObjectToDateTime(row["send_dt"]),
				};
				result.Add(data);
			}
			return result;
		}
	}
}
