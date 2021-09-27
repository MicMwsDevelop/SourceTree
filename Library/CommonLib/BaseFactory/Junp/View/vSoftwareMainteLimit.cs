using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.View
{
	public class vSoftwareMainteLimit
	{
        public string 拠点コード { get; set; }
		public string 拠点名 { get; set; }
		public int 顧客No { get; set; }
		public string 顧客名 { get; set; }
		public string 営業担当 { get; set; }
		public int サービスID { get; set; }
		public string サービス名 { get; set; }
		public string 利用開始 { get; set; }
		public string 利用終了 { get; set; }
		public string 終了 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vSoftwareMainteLimit()
		{
			拠点コード = string.Empty;
			拠点名 = string.Empty;
			顧客No = 0;
			顧客名 = string.Empty;
			営業担当 = string.Empty;
			サービスID = 0;
			サービス名 = string.Empty;
			利用開始 = string.Empty;
			利用終了 = string.Empty;
			終了 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vSoftwareMainteLimit> DataTableToList(DataTable table)
		{
			List<vSoftwareMainteLimit> result = new List<vSoftwareMainteLimit>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					vSoftwareMainteLimit data = new vSoftwareMainteLimit
					{
						拠点コード = row["拠点コード"].ToString().Trim(),
						拠点名 = row["拠点名"].ToString().Trim(),
						顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						顧客名 = row["顧客名"].ToString().Trim(),
						営業担当 = row["営業担当"].ToString().Trim(),
						サービスID = DataBaseValue.ConvObjectToInt(row["サービスID"]),
						サービス名 = row["サービス名"].ToString().Trim(),
						利用開始 = row["利用開始"].ToString().Trim(),
						利用終了 = row["利用終了"].ToString().Trim(),
						終了 = row["終了"].ToString().Trim(),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
