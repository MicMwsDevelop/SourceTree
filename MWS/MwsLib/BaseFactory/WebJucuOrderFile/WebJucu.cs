using MwsLib.BaseFactory.Estore.Table;
using MwsLib.BaseFactory.Estore.View;
using MwsLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.WebJucuOrderFile
{
	public class WebJucu
	{
		/// <summary>
		/// estore注文情報
		/// </summary>
		public vMicOrder_accept Order { get; set; }

		/// <summary>
		/// PCA受注No
		/// </summary>
		public int PCA受注No { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public WebJucu()
		{
			Order = new vMicOrder_accept();
			PCA受注No = 0;
		}

		/// <summary>
		/// estoreログテーブルデータの出力
		/// </summary>
		/// <returns></returns>
		public tMICestore_log TotMICestore_log()
		{
			tMICestore_log log = new tMICestore_log(Order);
			log.PCA受注No = PCA受注No.ToString();
			log.作成日時 = DateTime.Now;
			return log;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<WebJucu> DataTableToList(DataTable table)
		{
			List<WebJucu> result = new List<WebJucu>();
			foreach (DataRow row in table.Rows)
			{
				WebJucu data = new WebJucu();
				data.Order.order_accept_id = DataBaseValue.ConvObjectToInt(row["order_accept_id"]);
				data.Order.order_no = DataBaseValue.ConvObjectToInt(row["order_no"]);
				data.Order.customer_no = DataBaseValue.ConvObjectToInt(row["customer_no"]);
				data.Order.pref_arrival_date = DataBaseValue.ConvObjectToDateTimeNull(row["pref_arrival_date"]);
				data.Order.goods_code = row["goods_code"].ToString().Trim();
				data.Order.web_price = DataBaseValue.ConvObjectToInt(row["web_price"]);
				data.Order.order_num = DataBaseValue.ConvObjectToInt(row["order_num"]);
				data.Order.order_dt = DataBaseValue.ConvObjectToDateTime(row["order_dt"]);
				data.Order.send_dt = DataBaseValue.ConvObjectToDateTime(row["send_dt"]);
				result.Add(data);
			}
			return result;
		}
	}
}
