using CommonLib.BaseFactory.Charlie.View;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.MwsServiceCancelTool
{
	/// <summary>
	/// カプラー申込情報
	/// </summary>
	public class EditCouplerApply : V_COUPLER_APPLY
	{
		/// <summary>
		/// サービス名称
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EditCouplerApply() : base()
		{
			ServiceName = string.Empty;
		}

		/// <summary>
		/// [EditCouplerApply]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>利用情報</returns>
		public static new List<EditCouplerApply> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<EditCouplerApply> result = new List<EditCouplerApply>();
				foreach (DataRow row in table.Rows)
				{
					EditCouplerApply data = new EditCouplerApply
					{
						apply_id = DataBaseValue.ConvObjectToInt(row["申込No"]),
						cp_id = row["MWSID"].ToString().Trim(),
						customer_id = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						service_id = DataBaseValue.ConvObjectToInt(row["サービスID"]),
						apply_date = DataBaseValue.ConvObjectToDateTimeNull(row["申込日時"]),
						apply_type = row["申込種別"].ToString().Trim(),
						system_flg = row["システム反映済フラグ"].ToString().Trim(),
						create_date = DataBaseValue.ConvObjectToDateTimeNull(row["作成日時"]),
						create_user = row["作成者"].ToString().Trim(),
						update_date = DataBaseValue.ConvObjectToDateTimeNull(row["更新日時"]),
						update_user = row["更新者"].ToString().Trim(),
						ServiceName = row["サービス名"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
