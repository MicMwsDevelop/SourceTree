using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.BaseFactory.Junp.View;

namespace OnlineLicenseSubsidy.BaseFactory
{
	/// <summary>
	/// 領収内訳情報
	/// </summary>
	public class 領収内訳情報
	{
		public string 項目 { get; set; }
		public string 内訳 { get; set; }
		public double 補助対象金額 { get; set; }
		public double 補助対象外金額 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 領収内訳情報()
		{
			項目 = string.Empty;
			内訳 = string.Empty;
			補助対象金額 = 0;
			補助対象外金額 = 0;
		}
	}

	/// <summary>
	/// 補助金申請情報
	/// </summary>
	public class 補助金申請情報
	{
		public vMicユーザーオン資用 顧客情報WW { get; set; }
		public string 受付通番 { get; set; }
		public string 顧客名 { get; set; }
		public string 郵便番号 { get; set; }
		public string 住所 { get; set; }
		public string 電話番号 { get; set; }
		public string 開設者 { get; set; }
		public string 医療機関コード { get; set; }
		public DateTime? 工事完了日 { get; set; }
		public List<領収内訳情報> 領収内訳情報List { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 補助金申請情報()
		{
			顧客情報WW = null;
			受付通番 = string.Empty;
			顧客名 = string.Empty;
			郵便番号 = string.Empty;
			住所 = string.Empty;
			電話番号 = string.Empty;
			開設者 = string.Empty;
			医療機関コード = string.Empty;
			工事完了日 = null;
			領収内訳情報List = null;
		}
	}
}
