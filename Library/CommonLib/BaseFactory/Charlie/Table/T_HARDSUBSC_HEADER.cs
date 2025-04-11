//
// T_HARDSUBSC_HEADER.cs
//
// ハードサブスク情報管理 契約情報クラス
// [CharlieDB].[dbo].[T_HARDSUBSC_HEADER]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.DB;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// ハードサブスク情報管理 契約情報
	/// </summary>
	public class T_HARDSUBSC_HEADER : ICloneable, IEquatable<T_HARDSUBSC_HEADER>
	{
		/// <summary>
		/// 貸出番号
		/// </summary>
		public int RentalNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerID { get; set; }

		/// <summary>
		/// 受付日時
		/// </summary>
		public DateTime? ApplyDate { get; set; }

		/// <summary>
		/// 契約月数
		/// </summary>
		public short Months { get; set; }

		/// <summary>
		/// 金額
		/// </summary>
		public int TotalAmount { get; set; }

		/// <summary>
		/// 月額
		/// </summary>
		public int MonthlyAmount { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public DateTime? ContractStartDate { get; set; }

		/// <summary>
		/// 利用終了日
		/// </summary>
		public DateTime? ContractEndDate { get; set; }

		/// <summary>
		/// 課金開始日
		/// </summary>
		public DateTime? BillingStartDate { get; set; }

		/// <summary>
		/// 課金終了日
		/// </summary>
		public DateTime? BillingEndDate { get; set; }

		/// <summary>
		/// 解約日
		/// </summary>
		public DateTime? CancelDate { get; set; }

		/// <summary>
		/// 解約受付日時
		/// </summary>
		public DateTime? CancelApplyDate { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? CreateDate { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreatePerson { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime? UpdateDate { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UpdatePerson { get; set; }

		/// <summary>
		/// 削除可能かどうか？
		/// </summary>
		public bool IsDelete
		{
			get
			{
				return BillingStartDate.HasValue;
			}
		}

		/// <summary>
		/// 解約日が設定可能か？
		/// </summary>
		public bool IsEnableCancelDate
		{
			get
			{
				return ContractStartDate.HasValue;
			}
		}

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15) SELECT SCOPE_IDENTITY()", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARDSUBSC_HEADER]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET CustomerID = @1, ApplyDate = @2, Months = @3, TotalAmount = @4, MonthlyAmount = @5, ContractStartDate = @6, ContractEndDate = @7, BillingStartDate = @8,"
									+ " BillingEndDate = @9, CancelDate = @10, CancelApplyDate = @11, UpdateDate = @12, UpdatePerson = @13"
									+ " WHERE RentalNo = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARDSUBSC_HEADER], RentalNo);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_HARDSUBSC_HEADER()
		{
			RentalNo = 0;
			CustomerID = 0;
			ApplyDate = null;
			Months = 0;
			TotalAmount = 0;
			MonthlyAmount = 0;
			ContractStartDate = null;
			ContractEndDate = null;
			BillingStartDate = null;
			BillingEndDate = null;
			CancelDate = null;
			CancelApplyDate = null;
			CreateDate = null;
			CreatePerson = string.Empty;
			UpdateDate = null;
			UpdatePerson = string.Empty;
		}

		/// <summary>
		/// メンバーのクローンを作成する
		/// （ICloneableの実装）
		/// </summary>
		/// <returns>クローンオブジェクト</returns>
		public Object Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>
		/// Deep Copy
		/// </summary>
		/// <returns>オブジェクト</returns>
		public T_HARDSUBSC_HEADER DeepCopy()
		{
			T_HARDSUBSC_HEADER ret = new T_HARDSUBSC_HEADER();
			ret.RentalNo = this.RentalNo;
			ret.CustomerID = this.CustomerID;
			ret.ApplyDate = this.ApplyDate;
			ret.Months = this.Months;
			ret.TotalAmount = this.TotalAmount;
			ret.MonthlyAmount = this.MonthlyAmount;
			ret.ContractStartDate = this.ContractStartDate;
			ret.ContractEndDate = this.ContractEndDate;
			ret.BillingStartDate = this.BillingStartDate;
			ret.BillingEndDate = this.BillingEndDate;
			ret.CancelDate = this.CancelDate;
			ret.CancelApplyDate = this.CancelApplyDate;
			ret.CreateDate = this.CreateDate;
			ret.CreatePerson = this.CreatePerson;
			ret.UpdateDate = this.UpdateDate;
			ret.UpdatePerson = this.UpdatePerson;
			return ret;
		}

		/// <summary>
		/// このインスタンスと、指定した環境設定クラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(T_HARDSUBSC_HEADER other)
		{
			if (other != null)
			{
				if (RentalNo == other.RentalNo
					&& CustomerID == other.CustomerID
					&& ApplyDate.Equals(other.ApplyDate)
					&& Months == other.Months
					&& TotalAmount == other.TotalAmount
					&& MonthlyAmount == other.MonthlyAmount
					&& ContractStartDate.Equals(other.ContractStartDate)
					&& ContractEndDate.Equals(other.ContractEndDate)
					&& BillingStartDate.Equals(other.BillingStartDate)
					&& BillingEndDate.Equals(other.BillingEndDate)
					&& CancelDate.Equals(other.CancelDate)
					&& CancelApplyDate.Equals(other.CancelApplyDate)
					&& CreateDate.Equals(other.CreateDate)
					&& CreatePerson == other.CreatePerson
					&& UpdateDate.Equals(other.UpdateDate)
					&& UpdatePerson == other.UpdatePerson)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is T_HARDSUBSC_HEADER)
			{
				return this.Equals((T_HARDSUBSC_HEADER)obj);
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
		/// [charlieDB].[dbo].[T_HARDSUBSC_HEADER]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_HARDSUBSC_HEADER</returns>
		public static List<T_HARDSUBSC_HEADER> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_HARDSUBSC_HEADER> result = new List<T_HARDSUBSC_HEADER>();
				foreach (DataRow row in table.Rows)
				{
					T_HARDSUBSC_HEADER data = new T_HARDSUBSC_HEADER
					{
						RentalNo = DataBaseValue.ConvObjectToInt(row["RentalNo"]),
						CustomerID = DataBaseValue.ConvObjectToInt(row["CustomerID"]),
						ApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["ApplyDate"]),
						Months = DataBaseValue.ConvObjectToShort(row["Months"]),
						TotalAmount = DataBaseValue.ConvObjectToInt(row["TotalAmount"]),
						MonthlyAmount = DataBaseValue.ConvObjectToInt(row["MonthlyAmount"]),
						ContractStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["ContractStartDate"]),
						ContractEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["ContractEndDate"]),
						BillingStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["BillingStartDate"]),
						BillingEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["BillingEndDate"]),
						CancelDate = DataBaseValue.ConvObjectToDateTimeNull(row["CancelDate"]),
						CancelApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["CancelApplyDate"]),
						CreateDate = DataBaseValue.ConvObjectToDateTimeNull(row["CreateDate"]),
						CreatePerson = row["CreatePerson"].ToString().Trim(),
						UpdateDate = DataBaseValue.ConvObjectToDateTimeNull(row["UpdateDate"]),
						UpdatePerson = row["UpdatePerson"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <param name="person">作成者</param>
		/// <returns>パラメタ</returns>
		public SqlParameter[] GetInsertIntoParameters(string person)
		{
			SqlParameter[] param = {
				new SqlParameter("@1", CustomerID),
				new SqlParameter("@2", ApplyDate.HasValue ? ApplyDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", Months),
				new SqlParameter("@4", TotalAmount),
				new SqlParameter("@5", MonthlyAmount),
				new SqlParameter("@6", ContractStartDate.HasValue ? ContractStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", ContractEndDate.HasValue ? ContractEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", BillingStartDate.HasValue ? BillingStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", BillingEndDate.HasValue ? BillingEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", CancelDate.HasValue ? CancelDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", CancelApplyDate.HasValue ? CancelApplyDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@12", DateTime.Now),
				new SqlParameter("@13", person),
				new SqlParameter("@14", System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@15", System.Data.SqlTypes.SqlString.Null),
			};
			return param;
		}

		/// <summary>
		/// UPDATE SETパラメタの取得
		/// </summary>
		/// <param name="person">更新者</param>
		/// <returns>パラメタ</returns>
		public SqlParameter[] GetUpdateSetParameters(string person)
		{
			SqlParameter[] param = {
				new SqlParameter("@1", CustomerID),
				new SqlParameter("@2", ApplyDate.HasValue ? ApplyDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", Months),
				new SqlParameter("@4", TotalAmount),
				new SqlParameter("@5", MonthlyAmount),
				new SqlParameter("@6", ContractStartDate.HasValue ? ContractStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", ContractEndDate.HasValue ? ContractEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", BillingStartDate.HasValue ? BillingStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", BillingEndDate.HasValue ? BillingEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", CancelDate.HasValue ? CancelDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", CancelApplyDate.HasValue ? CancelApplyDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@12", DateTime.Now),
				new SqlParameter("@13", person)
			};
			return param;
		}
	}
}
