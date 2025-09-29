//
// T_HARD_SUBSC_HEADER.cs
//
// ハードサブスク情報管理 契約情報クラス
// [CharlieDB].[dbo].[T_HARD_SUBSC_HEADER]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
//
using CommonLib.Common;
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
	public class T_HARD_SUBSC_HEADER : ICloneable, IEquatable<T_HARD_SUBSC_HEADER>
	{
		/// <summary>
		/// 最低利用月数
		/// </summary>
		public const int ContractMonthMin = 36;

		/// <summary>
		/// 最大利用月数
		/// </summary>
		public const int ContractMonthMax = 60;

		/// <summary>
		/// 内部契約番号
		/// </summary>
		public int InternalContractNo { get; set; }

		/// <summary>
		/// 契約番号
		/// </summary>
		public string ContractNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerID { get; set; }

		/// <summary>
		/// 受付日
		/// </summary>
		public DateTime? AcceptDate { get; set; }

		/// <summary>
		/// 利用月数
		/// </summary>
		public short Months { get; set; }

		/// <summary>
		/// 月額利用料
		/// </summary>
		public int MonthlyAmount { get; set; }

		/// <summary>
		/// 納品日
		/// </summary>
		public DateTime? DeliveryDate { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public DateTime? UseStartDate { get; set; }

		/// <summary>
		/// 利用終了日
		/// </summary>
		public DateTime? UseEndDate { get; set; }

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
		/// 機器回収日
		/// </summary>
		public DateTime? CollectDate { get; set; }

		/// <summary>
		/// 機器廃棄日
		/// </summary>
		public DateTime? DisposalDate { get; set; }

		/// <summary>
		/// サービス終了フラグ
		/// </summary>
		public bool ServiceEndFlag { get; set; }

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
		/// 契約情報が削除可能かどうか？
		/// 条件：課金終了日未設定 かつ サービス終了フラグ=OFF
		/// </summary>
		public bool IsDelete
		{
			get
			{
				return !BillingEndDate.HasValue && !ServiceEndFlag;
			}
		}

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18) SELECT SCOPE_IDENTITY()", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARD_SUBSC_HEADER]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET ContractNo = @1, CustomerID = @2, AcceptDate = @3, Months = @4, MonthlyAmount = @5,  DeliveryDate= @6, UseStartDate = @7, UseEndDate = @8, BillingStartDate = @9,"
									+ " BillingEndDate = @10, CancelDate = @11, CollectDate = @12, DisposalDate = @13, ServiceEndFlag = @14, UpdateDate = @15, UpdatePerson = @16"
									+ " WHERE [InternalContractNo] = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARD_SUBSC_HEADER], InternalContractNo);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_HARD_SUBSC_HEADER()
		{
			this.Clear();
		}

		/// <summary>
		/// クリア
		/// </summary>
		public void Clear()
		{
			InternalContractNo = 0;
			ContractNo = string.Empty;
			CustomerID = 0;
			AcceptDate = null;
			Months = 0;
			MonthlyAmount = 0;
			DeliveryDate = null;
			UseStartDate = null;
			UseEndDate = null;
			BillingStartDate = null;
			BillingEndDate = null;
			CancelDate = null;
			CollectDate = null;
			DisposalDate = null;
			ServiceEndFlag = false;
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
		public T_HARD_SUBSC_HEADER DeepCopy()
		{
			T_HARD_SUBSC_HEADER ret = new T_HARD_SUBSC_HEADER();
			ret.InternalContractNo = this.InternalContractNo;
			ret.ContractNo = this.ContractNo;
			ret.CustomerID = this.CustomerID;
			ret.AcceptDate = this.AcceptDate;
			ret.Months = this.Months;
			ret.MonthlyAmount = this.MonthlyAmount;
			ret.DeliveryDate = this.DeliveryDate;
			ret.UseStartDate = this.UseStartDate;
			ret.UseEndDate = this.UseEndDate;
			ret.BillingStartDate = this.BillingStartDate;
			ret.BillingEndDate = this.BillingEndDate;
			ret.CancelDate = this.CancelDate;
			ret.CollectDate = this.CollectDate;
			ret.DisposalDate = this.DisposalDate;
			ret.ServiceEndFlag = this.ServiceEndFlag;
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
		public bool Equals(T_HARD_SUBSC_HEADER other)
		{
			if (other != null)
			{
				if (InternalContractNo == other.InternalContractNo
					&& ContractNo == other.ContractNo
					&& CustomerID == other.CustomerID
					&& AcceptDate.Equals(other.AcceptDate)
					&& Months == other.Months
					&& MonthlyAmount == other.MonthlyAmount
					&& DeliveryDate.Equals(other.DeliveryDate)
					&& UseStartDate.Equals(other.UseStartDate)
					&& UseEndDate.Equals(other.UseEndDate)
					&& BillingStartDate.Equals(other.BillingStartDate)
					&& BillingEndDate.Equals(other.BillingEndDate)
					&& CancelDate.Equals(other.CancelDate)
					&& CollectDate.Equals(other.CollectDate)
					&& DisposalDate.Equals(other.DisposalDate)
					&& ServiceEndFlag == other.ServiceEndFlag
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
			if (obj is T_HARD_SUBSC_HEADER)
			{
				return this.Equals((T_HARD_SUBSC_HEADER)obj);
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
		/// [charlieDB].[dbo].[T_HARD_SUBSC_HEADER]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_HARD_SUBSC_HEADER</returns>
		public static List<T_HARD_SUBSC_HEADER> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_HARD_SUBSC_HEADER> result = new List<T_HARD_SUBSC_HEADER>();
				foreach (DataRow row in table.Rows)
				{
					T_HARD_SUBSC_HEADER data = new T_HARD_SUBSC_HEADER
					{
						InternalContractNo = DataBaseValue.ConvObjectToInt(row["InternalContractNo"]),
						ContractNo = row["ContractNo"].ToString().Trim(),
						CustomerID = DataBaseValue.ConvObjectToInt(row["CustomerID"]),
						AcceptDate = DataBaseValue.ConvObjectToDateTimeNull(row["AcceptDate"]),
						Months = DataBaseValue.ConvObjectToShort(row["Months"]),
						MonthlyAmount = DataBaseValue.ConvObjectToInt(row["MonthlyAmount"]),
						DeliveryDate = DataBaseValue.ConvObjectToDateTimeNull(row["DeliveryDate"]),
						UseStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["UseStartDate"]),
						UseEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["UseEndDate"]),
						BillingStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["BillingStartDate"]),
						BillingEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["BillingEndDate"]),
						CancelDate = DataBaseValue.ConvObjectToDateTimeNull(row["CancelDate"]),
						CollectDate = DataBaseValue.ConvObjectToDateTimeNull(row["CollectDate"]),
						DisposalDate = DataBaseValue.ConvObjectToDateTimeNull(row["DisposalDate"]),
						ServiceEndFlag = ("0" == row["ServiceEndFlag"].ToString()) ? false : true,
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
				new SqlParameter("@1", ContractNo),
				new SqlParameter("@2", CustomerID),
				new SqlParameter("@3", AcceptDate.HasValue ? AcceptDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", Months),
				new SqlParameter("@5", MonthlyAmount),
				new SqlParameter("@6", DeliveryDate.HasValue ? DeliveryDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", UseStartDate.HasValue ? UseStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", UseEndDate.HasValue ? UseEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", BillingStartDate.HasValue ? BillingStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", BillingEndDate.HasValue ? BillingEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", CancelDate.HasValue ? CancelDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@12", CollectDate.HasValue ? CollectDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@13", DisposalDate.HasValue ? DisposalDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@14", ServiceEndFlag ? "1" : "0"),
				new SqlParameter("@15", DateTime.Now),
				new SqlParameter("@16", person),
				new SqlParameter("@17", System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@18", System.Data.SqlTypes.SqlString.Null),
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
				new SqlParameter("@1", ContractNo),
				new SqlParameter("@2", CustomerID),
				new SqlParameter("@3", AcceptDate.HasValue ? AcceptDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", Months),
				new SqlParameter("@5", MonthlyAmount),
				new SqlParameter("@6", DeliveryDate.HasValue ? DeliveryDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", UseStartDate.HasValue ? UseStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", UseEndDate.HasValue ? UseEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", BillingStartDate.HasValue ? BillingStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", BillingEndDate.HasValue ? BillingEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", CancelDate.HasValue ? CancelDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@12", CollectDate.HasValue ? CollectDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@13", DisposalDate.HasValue ? DisposalDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@14", ServiceEndFlag ? "1" : "0"),
				new SqlParameter("@15", DateTime.Now),
				new SqlParameter("@16", person)
			};
			return param;
		}

		/// <summary>
		/// 利用月数は正しいか？
		/// </summary>
		/// <param name="months">利用月数</param>
		/// <returns>判定</returns>
		public static bool IsFormalMonths(short months)
		{
			return T_HARD_SUBSC_HEADER.ContractMonthMin <= months && months <= T_HARD_SUBSC_HEADER.ContractMonthMax;
		}

		/// <summary>
		/// 納品日から利用開始日を取得
		/// </summary>
		/// <param name="shippingDate">納品日</param>
		/// <returns>利用開始日</returns>
		public static DateTime? GetUseStartDate(DateTime? deliveryDate)
		{
			if (deliveryDate.HasValue)
			{
				// 納品日の翌月初日
				return deliveryDate.Value.ToDate().PlusMonths(1).FirstDayOfTheMonth().ToDateTime();
			}
			return null;
		}

		/// <summary>
		/// 利用開始日と利用月数から利用終了日の取得
		/// </summary>
		/// <param name="startDate">利用開始日</param>
		/// <param name="months">利用月数</param>
		/// <returns>利用終了日</returns>
		public static DateTime? GetUseEndDate(DateTime? startDate, int months)
		{
			if (startDate.HasValue && 0 < months)
			{
				// 末日
				return startDate.Value.ToDate().PlusMonths(months).LastDayOfTheMonth().ToDateTime();
			}
			return null;
		}
	}
}
