using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.VariousReportOut
{
	/// <summary>
	/// 事業所情報
	/// </summary>
	public class OfficeInfo
	{
		/// <summary>
		/// 会社名
		/// </summary>
		public const string CompanyName = "株式会社ミック";

		/// <summary>
		/// 郵便番号
		/// </summary>
		public const string CompanyZipcode = "160-0022";

		/// <summary>
		/// 住所
		/// </summary>
		public const string CompanyAddress = "東京都新宿区新宿1-8-5";


		public string 部署名 { get; set; }
		public string 拠点名 { get; set; }
		public string 住所1 { get; set; }
		public string 住所2 { get; set; }
		public string 電話番号 { get; set; }
		public string FAX番号 { get; set; }
		public string 郵便番号 { get; set; }
		public string ログイン名 { get; set; }

		/// <summary>
		/// 本社かどうか？
		/// </summary>
		public bool HeadOffice{ get; set; }

		/// <summary>
		/// 送付先
		/// </summary>
		public string 送付先
		{
			get
			{
				if (false == HeadOffice)
				{
					return CompanyName + 拠点名;
				}
				return 拠点名;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OfficeInfo()
		{
			部署名 = string.Empty;
			拠点名 = string.Empty;
			住所1 = string.Empty;
			住所2 = string.Empty;
			電話番号 = string.Empty;
			FAX番号 = string.Empty;
			郵便番号 = string.Empty;
			ログイン名 = string.Empty;
			HeadOffice = false;
		}

		/// <summary>
		/// 本社情報の設定
		/// </summary>
		/// <param name="loginName">ログイン名</param>
		public void SetHeadOfficeInfo(string loginName)
		{
			部署名 = "本社";
			拠点名 = CompanyName;
			住所1 = CompanyAddress;
			住所2 = string.Empty;
			電話番号 = "03-3350-1661";
			FAX番号 = "03-3350-1870";
			郵便番号 = CompanyZipcode;
			ログイン名 = loginName;
			HeadOffice = true;
		}

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<OfficeInfo> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<OfficeInfo> result = new List<OfficeInfo>();
                foreach (DataRow row in table.Rows)
                {
                    OfficeInfo data = new OfficeInfo
                    {
                        部署名 = row["部署名"].ToString().Trim(),
                        拠点名 = row["拠点名"].ToString().Trim(),
                        住所1 = row["住所1"].ToString().Trim(),
                        住所2 = row["住所2"].ToString().Trim(),
                        電話番号 = row["電話番号"].ToString().Trim(),
                        FAX番号 = row["FAX番号"].ToString().Trim(),
                        郵便番号 = row["郵便番号"].ToString().Trim(),
                        ログイン名 = row["ログイン名"].ToString().Trim(),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
	}
}
