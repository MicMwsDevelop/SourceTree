using System;
using System.Collections.Generic;
using System.Linq;

namespace MwsLib.BaseFactory.PcSafetySupport
{
	/// <summary>
	/// 拠店員情報
	/// </summary>
	[Serializable]
	public class BranchInfo : IEquatable<BranchInfo>
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
		public BranchInfo()
		{
			BranchCode2 = string.Empty;
			BranchName2 = string.Empty;
			BranchCode3 = string.Empty;
			BranchName3 = string.Empty;
			EmployeeList = new List<EmployeeInfo>();
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するBranchInfo</param>
		/// <returns>判定</returns>
		public bool Equals(BranchInfo other)
		{
			if (null != other)
			{
				if (BranchCode2 != other.BranchCode2)
					return false;
				if (BranchName2 != other.BranchName2)
					return false;
				if (BranchCode3 != other.BranchCode3)
					return false;
				if (BranchName3 != other.BranchName3)
					return false;
				if (false == EmployeeList.SequenceEqual(other.EmployeeList))
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するBranchInfoオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is BranchInfo)
			{
				return this.Equals((BranchInfo)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// ハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// <summary>
		/// 出力レコードの取得
		/// </summary>
		/// <returns>出力レコード</returns>
		public override string ToString()
		{
			return BranchCode2 + BranchName2 + BranchCode3 + BranchName3;
		}
	}

	/// <summary>
	/// 従業員情報
	/// </summary>

	[Serializable]
	public class EmployeeInfo : IEquatable<EmployeeInfo>
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

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するEmployeeInfo</param>
		/// <returns>判定</returns>
		public bool Equals(EmployeeInfo other)
		{
			if (null != other)
			{
				if (UserID != other.UserID)
					return false;
				if (UserName != other.UserName)
					return false;
				if (BranchCode3 != other.BranchCode3)
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するEmployeeInfoオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is EmployeeInfo)
			{
				return this.Equals((EmployeeInfo)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// ハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// <summary>
		/// 出力レコードの取得
		/// </summary>
		/// <returns>出力レコード</returns>
		public override string ToString()
		{
			return UserID + UserName + BranchCode3;
		}
	}
}
