using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.Table
{
	public class tMic社内データ管理ヘッダ
	{
		public int f作業No { get; set; }
		public string fデータ名称 { get; set; }
		public string f作業報告書No { get; set; }
		public string fステータス { get; set; }
		public string f更新者コード { get; set; }
		public string f更新者名 { get; set; }
		public DateTime? f作業終了予定日 { get; set; }
		public string ｆ更新日時 { get; set; }
		public string f部署コード1 { get; set; }
		public string f部署コード2 { get; set; }
		public string f部署コード3 { get; set; }
		public string f備考 { get; set; }
		public string f登録先パス { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMic社内データ管理ヘッダ()
		{
			f作業No = 0;
			fデータ名称 = string.Empty;
			f作業報告書No = string.Empty;
			fステータス = string.Empty;
			f更新者コード = string.Empty;
			f更新者名 = string.Empty;
			f作業終了予定日 = null;
			ｆ更新日時 = string.Empty;
			f部署コード1 = string.Empty;
			f部署コード2 = string.Empty;
			f部署コード3 = string.Empty;
			f備考 = string.Empty;
			f登録先パス = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tMic社内データ管理ヘッダ> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMic社内データ管理ヘッダ> result = new List<tMic社内データ管理ヘッダ>();
				foreach (DataRow row in table.Rows)
				{
					tMic社内データ管理ヘッダ data = new tMic社内データ管理ヘッダ
					{
						f作業No = DataBaseValue.ConvObjectToInt(row["f作業No"]),
						fデータ名称 = row["fデータ名称"].ToString().Trim(),
						f作業報告書No = row["f作業報告書No"].ToString().Trim(),
						fステータス = row["fステータス"].ToString().Trim(),
						f更新者コード = row["f更新者コード"].ToString().Trim(),
						f更新者名 = row["f更新者名"].ToString().Trim(),
						f作業終了予定日 = DataBaseValue.ConvObjectToDateTimeNull(row["f作業終了予定日"]),
						ｆ更新日時 = row["ｆ更新日時"].ToString().Trim(),
						f部署コード1 = row["f部署コード1"].ToString().Trim(),
						f部署コード2 = row["f部署コード2"].ToString().Trim(),
						f部署コード3 = row["f部署コード3"].ToString().Trim(),
						f備考 = row["f備考"].ToString().Trim(),
						f登録先パス = row["f登録先パス"].ToString().Trim()
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}