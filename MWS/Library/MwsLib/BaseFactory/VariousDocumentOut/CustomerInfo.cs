//
// CustomerInfo.cs
// 
// 顧客情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using System.Collections.Generic;
using System.Data;
using MwsLib.DB;

namespace MwsLib.BaseFactory.VariousDocumentOut
{
	/// <summary>
	/// 顧客情報
	/// </summary>
	public class CustomerInfo
	{
		public int 顧客区分 { get; set; }
		public int 顧客No { get; set; }
		public string 得意先No { get; set; }
		public string 顧客名1 { get; set; }
		public string 顧客名2 { get; set; }
		public string 住所1 { get; set; }
		public string 住所2 { get; set; }
		public string 郵便番号 { get; set; }
		public string FAX番号 { get; set; }
		public string 電話番号 { get; set; }
		public string 院長名 { get; set; }
		public string 運用サポート情報 { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string 顧客名
		{
			get
			{
				return 顧客名1 + 顧客名2;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool Enable
		{
			get
			{
				return (2 == 顧客区分 || 18 == 顧客区分) ? true : false;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CustomerInfo()
		{
			this.Empty();
		}

		/// <summary>
		/// クリア
		/// </summary>
		public void Empty()
		{
			顧客区分 = 0;
			顧客No = 0;
			得意先No = string.Empty;
			顧客名1 = string.Empty;
			顧客名2 = string.Empty;
			住所1 = string.Empty;
			住所2 = string.Empty;
			郵便番号 = string.Empty;
			FAX番号 = string.Empty;
			電話番号 = string.Empty;
			院長名 = string.Empty;
			運用サポート情報 = string.Empty;
		}

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<CustomerInfo> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<CustomerInfo> result = new List<CustomerInfo>();
                foreach (DataRow row in table.Rows)
                {
                    CustomerInfo data = new CustomerInfo
                    {
						顧客区分 = DataBaseValue.ConvObjectToInt(row["顧客区分"]),
						顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						得意先No = row["得意先No"].ToString().Trim(),
						顧客名1 = row["顧客名1"].ToString().Trim(),
						顧客名2 = row["顧客名2"].ToString().Trim(),
						住所1 = row["住所1"].ToString().Trim(),
						住所2 = row["住所2"].ToString().Trim(),
						郵便番号 = row["郵便番号"].ToString().Trim(),
						FAX番号 = row["FAX番号"].ToString().Trim(),
						電話番号 = row["電話番号"].ToString().Trim(),
						院長名 = row["院長名"].ToString().Trim(),
						運用サポート情報 = row["運用サポート情報"].ToString().Trim(),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
	}
}
