using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.BaseFactory.Junp.View
{
	public class vMic顧客情報
	{
		public int 顧客No { get; set; }
		public string 得意先No { get; set; }
		public string 顧客名1 { get; set; }
		public string 顧客名2 { get; set; }
		public string フリガナ { get; set; }
		public string 郵便番号 { get; set; }
		public string 住所1 { get; set; }
		public string 住所2 { get; set; }
		public string 住所フリガナ { get; set; }
		public string 電話番号 { get; set; }
		public string FAX番号 { get; set; }
		public bool 終了フラグ { get; set; }

		/// <summary>
		/// 顧客名の取得
		/// </summary>
		public string 顧客名
		{
			get
			{
				string name = 顧客名1;
				if (0 < 顧客名2.Length)
				{
					name += " " + 顧客名2;
				}
				return name;
			}
		}

		/// <summary>
		/// 住所の取得
		/// </summary>
		public string 住所
		{
			get
			{
				string add = 住所1;
				if (0 < 住所2.Length)
				{
					add += " " + 住所2;
				}
				return add;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vMic顧客情報()
		{
			顧客No = 0;
			得意先No = string.Empty;
			顧客名1 = string.Empty;
			顧客名2 = string.Empty;
			フリガナ = string.Empty;
			郵便番号 = string.Empty;
			住所1 = string.Empty;
			住所2 = string.Empty;
			住所フリガナ = string.Empty;
			電話番号 = string.Empty;
			FAX番号 = string.Empty;
			終了フラグ = false;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMic顧客情報> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<vMic顧客情報> result = new List<vMic顧客情報>();
				foreach (DataRow row in table.Rows)
				{
					vMic顧客情報 data = new vMic顧客情報();
					data.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.得意先No = row["得意先No"].ToString().Trim();
					data.顧客名1 = row["顧客名1"].ToString().Trim();
					data.顧客名2 = row["顧客名2"].ToString().Trim();
					data.フリガナ = row["フリガナ"].ToString().Trim();
					data.郵便番号 = row["郵便番号"].ToString().Trim();
					data.住所1 = row["住所1"].ToString().Trim();
					data.住所2 = row["住所2"].ToString().Trim();
					data.住所フリガナ = row["住所フリガナ"].ToString().Trim();
					data.電話番号 = row["電話番号"].ToString().Trim();
					data.FAX番号 = row["FAX番号"].ToString().Trim();
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
