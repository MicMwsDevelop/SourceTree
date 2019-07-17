//
// T_NARCOHM_APPLICATE_HEADER.cs
//
// ナルコーム製品申込ヘッダ情報クラス
// [CharlieDB].[dbo].[T_NARCOHM_APPLICATE_HEADER]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using MwsLib.BaseFactory.NarcohmOrderCheck;
using MwsLib.Common;
using MwsLib.DB;
using MwsLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// ナルコーム製品申込ヘッダ情報
	/// [CharlieDB].[dbo].[T_NARCOHM_APPLICATE_HEADER]
	/// </summary>
	public class T_NARCOHM_APPLICATE_HEADER
	{
		/// <summary>
		/// 申込番号(オートナンバー)
		/// </summary>
		public int ApplicateID { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 得意先No
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string ClinicName { get; set; }

		/// <summary>
		/// 電話番号
		/// </summary>
		public string Telephone { get; set; }

		/// <summary>
		/// 件名
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// 営業部コード
		/// </summary>
		public string SectionCode { get; set; }

		/// <summary>
		/// 営業部名
		/// </summary>
		public string SectionName { get; set; }

		/// <summary>
		/// 拠点コード
		/// </summary>
		public string BranchCode { get; set; }

		/// <summary>
		/// 拠点名
		/// </summary>
		public string BranchName { get; set; }

		/// <summary>
		/// 営業担当者コード
		/// </summary>
		public string SalesmanCode { get; set; }

		/// <summary>
		/// 営業担当者名
		/// </summary>
		public string SalesmanName { get; set; }

		/// <summary>
		/// サービス開始日
		/// </summary>
		public Date? ServiceStartDate { get; set; }

		/// <summary>
		/// 課金開始年月
		/// </summary>
		public YearMonth? KakinStartYM { get; set; }

		/// <summary>
		/// 販売種別
		/// </summary>
		public MwsDefine.ApplyType SaleType { get; set; }

		/// <summary>
		/// メール送信日時
		/// </summary>
		public DateTime? MailSendDate { get; set; }

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
		/// ナルコーム製品申込詳細情報リスト
		/// </summary>
		public List<T_NARCOHM_APPLICATE_DETAIL> DetailList { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19);"
									+ " SELECT SCOPE_IDENTITY()", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_NARCOHM_APPLICATE_HEADER]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET"
									+ " CustomerNo = @1"
									+ ", TokuisakiNo = @2"
									+ ", ClinicName = @3"
									+ ", Telephone = @4"
									+ ", Subject = @5"
									+ ", SectionCode = @6"
									+ ", SectionName = @7"
									+ ", BranchCode = @8"
									+ ", BranchName = @9"
									+ ", SalesmanCode = @10"
									+ ", SalesmanName = @11"
									+ ", ServiceStartDate = @12"
									+ ", KakinStartYM = @13"
									+ ", SaleType = @14"
									+ ", MailSendDate = @15"
									+ ", CreateDate = @16"
									+ ", CreatePerson = @17"
									+ ", UpdateDate = @18"
									+ ", UpdatePerson = @19"
									+ " WHERE ApplicateID = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_NARCOHM_APPLICATE_HEADER], ApplicateID);
			}
		}

		/// <summary>
		/// DELETE SQL文字列の取得
		/// </summary>
		public string DeleteSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET"
									+ " CustomerNo = @1"
									+ ", TokuisakiNo = @2"
									+ ", ClinicName = @3"
									+ ", Telephone = @4"
									+ ", Subject = @5"
									+ ", SectionCode = @6"
									+ ", SectionName = @7"
									+ ", BranchCode = @8"
									+ ", BranchName = @9"
									+ ", SalesmanCode = @10"
									+ ", SalesmanName = @11"
									+ ", ServiceStartDate = @12"
									+ ", KakinStartYM = @13"
									+ ", SaleType = @14"
									+ ", MailSendDate = @15"
									+ ", CreateDate = @16"
									+ ", CreatePerson = @17"
									+ ", UpdateDate = @18"
									+ ", UpdatePerson = @19"
									+ " WHERE ApplicateID = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_NARCOHM_APPLICATE_HEADER], ApplicateID);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_NARCOHM_APPLICATE_HEADER()
		{
			ApplicateID = 0;
			CustomerNo = 0;
			TokuisakiNo = null;
			ClinicName = null;
			Telephone = null;
			Subject = null;
			SectionCode = null;
			SectionName = null;
			BranchCode = null;
			BranchName = null;
			SalesmanCode = null;
			SalesmanName = null;
			ServiceStartDate = null;
			KakinStartYM = null;
			SaleType = MwsDefine.ApplyType.Etc;
			MailSendDate = null;
			CreateDate = null;
			CreatePerson = null;
			UpdateDate = null;
			UpdatePerson = null;
			DetailList = new List<T_NARCOHM_APPLICATE_DETAIL>();
		}

		/// <summary>
		/// 医院情報の設定
		/// </summary>
		/// <param name="customer">医院情報</param>
		public void SetCustomerData(CustomerInfo customer)
		{
			CustomerNo = customer.CustomerNo;
			TokuisakiNo = customer.TokuisakiNo;
			ClinicName = customer.ClinicName;
			Telephone = customer.Telephone;
			SectionCode = customer.SectionCode;
			SectionName = customer.SectionName;
			BranchCode = customer.BranchCode;
			BranchName = customer.BranchName;
			SalesmanCode = customer.SalesmanCode;
			SalesmanName = customer.SalesmanName;
		}

		/// <summary>
		/// リストビュー表示情報の取得 
		/// </summary>
		/// <returns>表示情報</returns>
		public string[] GetListViewData()
		{
			string[] array = new string[15];
			array[0] = ApplicateID.ToString();
			array[3] = CustomerNo.ToString();
			array[4] = ClinicName;
			array[5] = Subject;
			array[6] = BranchName;
			array[7] = SalesmanName;
			array[10] = (ServiceStartDate.HasValue) ? ServiceStartDate.Value.ToString() : "";
			array[11] = (KakinStartYM.HasValue) ? KakinStartYM.Value.ToString() : "";
			array[9] = MwsDefine.ApplyTypeString[SaleType];
			array[14] = (MailSendDate.HasValue) ? MailSendDate.Value.ToString() : "";
			if (0 < DetailList.Count)
			{
				T_NARCOHM_APPLICATE_DETAIL detail = DetailList[0];
				array[1] = (detail.OrderNo.HasValue) ? detail.OrderNo.Value.ToString() : "";
				array[2] = (detail.OrderDate.HasValue) ? detail.OrderDate.Value.ToString() : "";
				array[8] = detail.GoodsName;
				array[12] = detail.Price.ToString();
				array[13] = detail.Count.ToString();
			}
			return array;
		}

		/// <summary>
		/// ナルコーム製品申込ヘッダ情報の詰め替え
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_HEADER]
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ナルコーム製品申込情報リスト</returns>
		public static List<T_NARCOHM_APPLICATE_HEADER> DataTableToList(DataTable table)
		{
			List<T_NARCOHM_APPLICATE_HEADER> result = null;
			if (null != table)
			{
				result = new List<T_NARCOHM_APPLICATE_HEADER>();
				foreach (DataRow row in table.Rows)
				{
					Date? kakinStart = DataBaseValue.ConvObjectToDateNullByDate(row["KakinStartYM"]);
					YearMonth? kakinStartYM = (kakinStart.HasValue) ? kakinStart.Value.ToYearMonth() : (YearMonth?)null;
					T_NARCOHM_APPLICATE_HEADER data = new T_NARCOHM_APPLICATE_HEADER
					{
						ApplicateID = DataBaseValue.ConvObjectToInt(row["ApplicateID"]),
						CustomerNo = DataBaseValue.ConvObjectToInt(row["CustomerNo"]),
						TokuisakiNo = row["TokuisakiNo"].ToString().Trim(),
						ClinicName = row["ClinicName"].ToString().Trim(),
						Telephone = row["Telephone"].ToString().Trim(),
						Subject = row["Subject"].ToString().Trim(),
						SectionCode = row["SectionCode"].ToString().Trim(),
						SectionName = row["SectionName"].ToString().Trim(),
						BranchCode = row["BranchCode"].ToString().Trim(),
						BranchName = row["BranchName"].ToString().Trim(),
						SalesmanCode = row["SalesmanCode"].ToString().Trim(),
						SalesmanName = row["SalesmanName"].ToString().Trim(),
						ServiceStartDate = DataBaseValue.ConvObjectToDateNullByDate(row["ServiceStartDate"]),
						KakinStartYM = kakinStartYM,
						SaleType = (MwsDefine.ApplyType)DataBaseValue.ConvObjectToInt(row["SaleType"]),
						MailSendDate = DataBaseValue.ConvObjectToDateTimeNull(row["MailSendDate"]),
						CreateDate = DataBaseValue.ConvObjectToDateTimeNull(row["CreateDate"]),
						CreatePerson = row["CreatePerson"].ToString().Trim(),
						UpdateDate = DataBaseValue.ConvObjectToDateTimeNull(row["UpdateDate"]),
						UpdatePerson = row["UpdatePerson"].ToString().Trim()
					};
					result.Add(data);
				}
			}
			return result;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", CustomerNo),												// [CustomerNo]
				new SqlParameter("@2", TokuisakiNo ?? System.Data.SqlTypes.SqlString.Null),		// [TokuisakiNo] 
				new SqlParameter("@3", ClinicName ?? System.Data.SqlTypes.SqlString.Null),	    // [ClinicName]
				new SqlParameter("@4", Telephone ?? System.Data.SqlTypes.SqlString.Null),		// [Telephone]
				new SqlParameter("@5", Subject ?? System.Data.SqlTypes.SqlString.Null),			// [Subject] 
				new SqlParameter("@6", SectionCode ?? System.Data.SqlTypes.SqlString.Null),		// [SectionCode] 
				new SqlParameter("@7", SectionName ?? System.Data.SqlTypes.SqlString.Null),		// [SectionName] 
				new SqlParameter("@8", BranchCode ?? System.Data.SqlTypes.SqlString.Null),	    // [BranchCode] 
				new SqlParameter("@9", BranchName ?? System.Data.SqlTypes.SqlString.Null),	    // [BranchName] 
				new SqlParameter("@10", SalesmanCode ?? System.Data.SqlTypes.SqlString.Null),	// [SalesmanCode] 
				new SqlParameter("@11", SalesmanName ?? System.Data.SqlTypes.SqlString.Null),	// [SalesmanName] 
				new SqlParameter("@12", ServiceStartDate.HasValue ? ServiceStartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// [ServiceStartDate] 
				new SqlParameter("@13", KakinStartYM.HasValue ? KakinStartYM.Value.ToDate(1).ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// [KakinStartYM] 
				new SqlParameter("@14", SaleType),												// [SaleType] 
				new SqlParameter("@15", MailSendDate.HasValue ? MailSendDate.Value : System.Data.SqlTypes.SqlDateTime.Null),	// [MailSendDate] 
				new SqlParameter("@16", CreateDate.HasValue ? CreateDate.Value : System.Data.SqlTypes.SqlDateTime.Null),		// [CreateDate] 
				new SqlParameter("@17", CreatePerson ?? System.Data.SqlTypes.SqlString.Null),	// [CreatePerson]
				new SqlParameter("@18", UpdateDate.HasValue ? UpdateDate.Value : System.Data.SqlTypes.SqlDateTime.Null),		// [UpdateDate] 
				new SqlParameter("@19", UpdatePerson ?? System.Data.SqlTypes.SqlString.Null)	// [UpdatePerson] 
			};
			return param;
		}

		/// <summary>
		/// UPDATE SETパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetUpdateSetParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", CustomerNo),												// [CustomerNo]
				new SqlParameter("@2", TokuisakiNo ?? System.Data.SqlTypes.SqlString.Null),		// [TokuisakiNo] 
				new SqlParameter("@3", ClinicName ?? System.Data.SqlTypes.SqlString.Null),	    // [ClinicName]
				new SqlParameter("@4", Telephone ?? System.Data.SqlTypes.SqlString.Null),		// [Telephone]
				new SqlParameter("@5", Subject ?? System.Data.SqlTypes.SqlString.Null),			// [Subject] 
				new SqlParameter("@6", SectionCode ?? System.Data.SqlTypes.SqlString.Null),		// [SectionCode] 
				new SqlParameter("@7", SectionName ?? System.Data.SqlTypes.SqlString.Null),		// [SectionName] 
				new SqlParameter("@8", BranchCode ?? System.Data.SqlTypes.SqlString.Null),		// [BranchCode] 
				new SqlParameter("@9", BranchName ?? System.Data.SqlTypes.SqlString.Null),		// [BranchName] 
				new SqlParameter("@10", SalesmanCode ?? System.Data.SqlTypes.SqlString.Null),	// [SalesmanCode] 
				new SqlParameter("@11", SalesmanName ?? System.Data.SqlTypes.SqlString.Null),	// [SalesmanName] 
				new SqlParameter("@12", ServiceStartDate.HasValue ? ServiceStartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// [ServiceStartDate] 
				new SqlParameter("@13", KakinStartYM.HasValue ? KakinStartYM.Value.ToDate(1).ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// [KakinStartYM] 
				new SqlParameter("@14", SaleType),												// [SaleType] 
				new SqlParameter("@15", MailSendDate.HasValue ? MailSendDate.Value : System.Data.SqlTypes.SqlDateTime.Null),	// [MailSendDate] 
				new SqlParameter("@16", CreateDate.HasValue ? CreateDate.Value : System.Data.SqlTypes.SqlDateTime.Null),		// [CreateDate] 
				new SqlParameter("@17", CreatePerson ?? System.Data.SqlTypes.SqlString.Null),	// [CreatePerson]
				new SqlParameter("@18", UpdateDate.HasValue ? UpdateDate.Value : System.Data.SqlTypes.SqlDateTime.Null),		// [UpdateDate] 
				new SqlParameter("@19", UpdatePerson ?? System.Data.SqlTypes.SqlString.Null)	// [UpdatePerson] 
			};
			return param;
		}
	}
}
