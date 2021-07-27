using MwsLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.Junp.Table
{
	public class tMic社内データ管理利用部署情報
	{
		public int ID { get; set; }
		public string 利用部署名 { get; set; }
		public string 保存フォルダ { get; set; }
		public string Busho1 { get; set; }
		public string Busho2 { get; set; }
		public string Busho3 { get; set; }
		public string メモタイプ略号 { get; set; }
		public string 定型句保存フォルダ { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMic社内データ管理利用部署情報()
		{
			ID = 0;
			利用部署名 = string.Empty;
			保存フォルダ = string.Empty;
			Busho1 = string.Empty;
			Busho2 = string.Empty;
			Busho3 = string.Empty;
			メモタイプ略号 = string.Empty;
			定型句保存フォルダ = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tMic社内データ管理利用部署情報> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMic社内データ管理利用部署情報> result = new List<tMic社内データ管理利用部署情報>();
				foreach (DataRow row in table.Rows)
				{
					tMic社内データ管理利用部署情報 data = new tMic社内データ管理利用部署情報
					{
						ID = DataBaseValue.ConvObjectToInt(row["ID"]),
						利用部署名 = row["利用部署名"].ToString().Trim(),
						保存フォルダ = row["保存フォルダ"].ToString().Trim(),
						Busho1 = row["Busho1"].ToString().Trim(),
						Busho2 = row["Busho2"].ToString().Trim(),
						Busho3 = row["Busho3"].ToString().Trim(),
						メモタイプ略号 = row["メモタイプ略号"].ToString().Trim(),
						定型句保存フォルダ = row["定型句保存フォルダ"].ToString().Trim()
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
