//
// CheckCuiUseDate.cs
//
// 利用情報利用日確認情報
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.AdjustServiceApply
{
	public class CheckCuiUseDate
	{
		/// <summary>
		/// 顧客ID
		/// </summary>
		public int customer_id { get; set; }

		/// <summary>
		/// MWS-ID
		/// </summary>
		public string cp_id { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string customer_name { get; set; }

		/// <summary>
		/// サービスID
		/// </summary>
		public int service_id { get; set; }

		/// <summary>
		/// サービス名
		/// </summary>
		public string service_name { get; set; }

		/// <summary>
		/// 課金対象外フラグ
		/// </summary>
		public bool pause_end_status { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public DateTime? use_start_date { get; set; }

		/// <summary>
		/// 利用終了日
		/// </summary>
		public DateTime? use_end_date { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool delete_flg { get; set; }

		/// <summary>
		/// セット販売機能
		/// 一括販売:0、月額課金用:1
		/// </summary>
		public bool set_sale { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CheckCuiUseDate()
		{
			customer_id = 0;
			cp_id = string.Empty;
			customer_name = string.Empty;
			service_id = 0;
			service_name = string.Empty;
			pause_end_status = false;
			use_start_date = null;
			use_end_date = null;
			delete_flg = false;
			set_sale = false;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<CheckCuiUseDate> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<CheckCuiUseDate> result = new List<CheckCuiUseDate>();
				foreach (DataRow row in table.Rows)
				{
					CheckCuiUseDate data = new CheckCuiUseDate
					{
						customer_id = DataBaseValue.ConvObjectToInt(row["customer_id"]),
						cp_id = row["cp_id"].ToString().Trim(),
						customer_name = row["customer_name"].ToString().Trim(),
						service_id = DataBaseValue.ConvObjectToInt(row["service_id"]),
						service_name = row["service_name"].ToString().Trim(),
						pause_end_status = ("0" == row["pause_end_status"].ToString()) ? false : true,
						use_start_date = DataBaseValue.ConvObjectToDateTimeNull(row["use_start_date"]),
						use_end_date = DataBaseValue.ConvObjectToDateTimeNull(row["use_end_date"]),
						delete_flg = ("0" == row["delete_flg"].ToString()) ? false : true,
						set_sale = ("0" == row["set_sale"].ToString()) ? false : true,
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// ログ出力文字列の取得
		/// </summary>
		/// <returns>ログ出力文字列</returns>
		public string GetLog()
		{
			return string.Format("CouplerID：{0} 顧客：{1} {2} サービス：{3} {4} 利用開始日または利用終了日が指定されていません。", cp_id, customer_id, customer_name, service_id, service_name);
		}
	}
}
