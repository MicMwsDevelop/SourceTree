using System.Collections.Generic;

namespace MwsLib.BaseFactory.PcSupportManager
{
	/// <summary>
	/// 拠店従業員情報
	/// </summary>
	public class BranchEmployeeInfo
	{
		/// <summary>
		/// 拠店コード２
		/// </summary>
		public string BranchCode2 { get; set; }

		/// <summary>
		/// 拠店名２
		/// </summary>
		public string BranchName2 { get; set; }

		/// <summary>
		/// 拠店コード３
		/// </summary>
		public string BranchCode3 { get; set; }

		/// <summary>
		/// 拠店名３
		/// </summary>
		public string BranchName3 { get; set; }

		/// <summary>
		/// 従業員情報リスト
		/// </summary>
		public List<EmployeeInfo> EmployeeList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public BranchEmployeeInfo()
		{
			BranchCode2 = string.Empty;
			BranchName2 = string.Empty;
			BranchCode3 = string.Empty;
			BranchName3 = string.Empty;
			EmployeeList = new List<EmployeeInfo>();
		}
	}

	/// <summary>
	/// 従業員情報
	/// </summary>
	public class EmployeeInfo
	{
		/// <summary>
		/// 社員番号
		/// </summary>
		public string UserID { get; set; }

		/// <summary>
		/// 社員名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 拠店コード３
		/// </summary>
		public string BranchCode3 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EmployeeInfo()
		{
			UserID = string.Empty;
			UserName = string.Empty;
			BranchCode3 = string.Empty;
		}
	}

	/// <summary>
	/// 拠店情報
	/// </summary>
	public class BranchInfo
	{
		/// <summary>
		/// 拠店ID
		/// </summary>
		public string BranchID { get; set; }

		/// <summary>
		/// 拠店名
		/// </summary>
		public string BranchName { get; set; }

		/// <summary>
		/// 拠店メールアドレス
		/// </summary>
		public string MailAddress { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public BranchInfo()
		{
			BranchID = string.Empty;
			BranchName = string.Empty;
			MailAddress = string.Empty;
		}
	}
}
