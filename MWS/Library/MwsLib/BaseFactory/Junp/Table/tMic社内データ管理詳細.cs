using MwsLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.Junp.Table
{
	public class tMic社内データ管理詳細
	{
		public int AutoNo { get; set; }
		public int 作業No { get; set; }
		public int fLogNo { get; set; }
		public string fステータス { get; set; }
		public string f更新者コード { get; set; }
		public string f更新者名 { get; set; }
		public string f更新日時 { get; set; }
		public string fファイルパス { get; set; }
		public string f部署コード1 { get; set; }
		public string f部署コード2 { get; set; }
		public string f部署コード3 { get; set; }
		public string f備考 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMic社内データ管理詳細()
		{
			AutoNo = 0;
			作業No = 0;
			fLogNo = 0;
			fステータス = string.Empty;
			f更新者コード = string.Empty;
			f更新者名 = string.Empty;
			f更新日時 = string.Empty;
			fファイルパス = string.Empty;
			f部署コード1 = string.Empty;
			f部署コード2 = string.Empty;
			f部署コード3 = string.Empty;
			f備考 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tMic社内データ管理詳細> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMic社内データ管理詳細> result = new List<tMic社内データ管理詳細>();
				foreach (DataRow row in table.Rows)
				{
					tMic社内データ管理詳細 data = new tMic社内データ管理詳細
					{
						AutoNo = DataBaseValue.ConvObjectToInt(row["AutoNo"]),
						作業No = DataBaseValue.ConvObjectToInt(row["作業No"]),
						fLogNo = DataBaseValue.ConvObjectToInt(row["fLogNo"]),
						fステータス = row["fステータス"].ToString().Trim(),
						f更新者コード = row["f更新者コード"].ToString().Trim(),
						f更新者名 = row["f更新者名"].ToString().Trim(),
						f更新日時 = row["f更新日時"].ToString().Trim(),
						fファイルパス = row["fファイルパス"].ToString().Trim(),
						f部署コード1 = row["f部署コード1"].ToString().Trim(),
						f部署コード2 = row["f部署コード2"].ToString().Trim(),
						f部署コード3 = row["f部署コード3"].ToString().Trim(),
						f備考 = row["f備考"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
