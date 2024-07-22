//
// T_USE_PCCSUPPORT.cs
//
// PC安心サポート契約情報クラス
// [CharlieDB].[dbo].[T_USE_PCCSUPPORT]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using CommonLib.BaseFactory.Junp.Table;
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
	/// [JunpDB].[dbo].[T_USE_PCCSUPPORT]
	/// PC安心サポート契約情報
	/// <summary>
	public class T_USE_PCCSUPPORT
	{
		/// <summary>
		/// 申込番号
		/// PK：オートID
		/// </summary>
		public int fApplyNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int fCustomerID { get; set; }

		/// <summary>
		/// サービスコード
		/// </summary>
		public int fServiceId { get; set; }

		/// <summary>
		/// 契約年数
		/// </summary>
		public int fYears { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public string fGoodsID { get; set; }

		/// <summary>
		/// 申込日付
		/// </summary>
		public DateTime? fApplyDate { get; set; }

		/// <summary>
		/// 契約開始日
		/// </summary>
		public Date? fContractStartDate { get; set; }

		/// <summary>
		/// 契約終了日
		/// </summary>
		public Date? fContractEndDate { get; set; }

		/// <summary>
		/// 課金開始日
		/// </summary>
		public Date? fBillingStartDate { get; set; }

		/// <summary>
		/// 課金終了日
		/// </summary>
		public Date? fBillingEndDate { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool fEndFlag { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool fDeleteFlag { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? fCreateDate { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string fCreatePerson { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime? fUpdateDate { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string fUpdatePerson { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15)", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET fCustomerID = @1, fServiceId = @2, fYears = @3, fGoodsID = @4, fApplyDate = @5, fContractStartDate = @6"
									+ ", fContractEndDate = @7, fBillingStartDate = @8, fBillingEndDate = @9, fEndFlag = @10, fDeleteFlag = @11"
									+ ", fCreateDate = @12, fCreatePerson = @13, fUpdateDate = @14, fUpdatePerson = @15"
									+ " WHERE fApplyNo = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT], fApplyNo);
			}
		}

		/// <summary>
		/// 利用申込が取消が可能かどうか？
		/// </summary>
		public bool IsEnableCancel
		{
			get
			{
				return (fBillingStartDate is null) && (fBillingEndDate is null) ? true : false;
			}
		}

		/// <summary>
		/// 商品コードが更新かどうか？
		/// PC安心サポート１年契約（更新用） or PC安心サポートPlus１年契約（更新用）
		/// </summary>
		public bool IsContinueGoods
		{
			get
			{
				return (PcaGoodsIDDefine.PcSupport1Continue == fGoodsID || PcaGoodsIDDefine.PcSupportPlus1Continue == fGoodsID) ? true : false;
			}
		}

		/// <summary>
		/// 商品コードがPC安心サポートPlusかどうか？
		/// </summary>
		public bool IsPcSupportPlusGoods
		{
			get
			{
				switch (fGoodsID)
				{
					case PcaGoodsIDDefine.PcSupportPlus1:
					case PcaGoodsIDDefine.PcSupportPlus3:
					case PcaGoodsIDDefine.PcSupportPlus1Continue:
						return true;
				}
				return false;
			}
		}

		/// <summary>
		/// サービスコードがPC安心サポート３年契約またはPC安心サポートPlus３年契約かどうか？
		/// </summary>
		public bool IsThreeYearService
		{
			get
			{
				return ((int)ServiceCodeDefine.ServiceCode.PcSafetySupport3 == fServiceId || (int)ServiceCodeDefine.ServiceCode.PcSafetySupportPlus3 == fServiceId) ? true : false;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_USE_PCCSUPPORT()
		{
			fApplyNo = 0;
			fCustomerID = 0;
			fServiceId = 0;
			fYears = 0;
			fGoodsID = null;
			fApplyDate = null;
			fContractStartDate = null;
			fContractEndDate = null;
			fBillingStartDate = null;
			fBillingEndDate = null;
			fEndFlag = false;
			fDeleteFlag = false;
			fCreateDate = null;
			fCreatePerson = null;
			fUpdateDate = null;
			fUpdatePerson = null;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			List<SqlParameter> param = new List<SqlParameter>();
			param.Add(new SqlParameter("@1", fCustomerID.ToString()));
			param.Add(new SqlParameter("@2", fServiceId.ToString()));
			param.Add(new SqlParameter("@3", fYears.ToString()));
			param.Add(new SqlParameter("@4", fGoodsID ?? System.Data.SqlTypes.SqlString.Null));
			param.Add(new SqlParameter("@5", fApplyDate.HasValue ? fApplyDate.Value : System.Data.SqlTypes.SqlDateTime.Null));
			if (fContractStartDate.HasValue)
			{
				param.Add(new SqlParameter { ParameterName = "@6", SqlDbType = SqlDbType.Date, Value = fContractStartDate.Value.ToDateTime() });
			}
			else
			{
				param.Add(new SqlParameter { ParameterName = "@6", SqlDbType = SqlDbType.NVarChar, Value = System.Data.SqlTypes.SqlString.Null });
			}
			if (fContractEndDate.HasValue)
			{
				param.Add(new SqlParameter { ParameterName = "@7", SqlDbType = SqlDbType.Date, Value = fContractEndDate.Value.ToDateTime() });
			}
			else
			{
				param.Add(new SqlParameter { ParameterName = "@7", SqlDbType = SqlDbType.NVarChar, Value = System.Data.SqlTypes.SqlString.Null });
			}
			if (fBillingStartDate.HasValue)
			{
				param.Add(new SqlParameter { ParameterName = "@8", SqlDbType = SqlDbType.Date, Value = fBillingStartDate.Value.ToDateTime() });
			}
			else
			{
				param.Add(new SqlParameter { ParameterName = "@8", SqlDbType = SqlDbType.NVarChar, Value = System.Data.SqlTypes.SqlString.Null });
			}
			if (fBillingEndDate.HasValue)
			{
				param.Add(new SqlParameter { ParameterName = "@9", SqlDbType = SqlDbType.Date, Value = fBillingEndDate.Value.ToDateTime() });
			}
			else
			{
				param.Add(new SqlParameter { ParameterName = "@9", SqlDbType = SqlDbType.NVarChar, Value = System.Data.SqlTypes.SqlString.Null });
			}
			param.Add(new SqlParameter("@10", fEndFlag ? "1" : "0"));
			param.Add(new SqlParameter("@11", fDeleteFlag ? "1" : "0"));
			param.Add(new SqlParameter("@12", fCreateDate.HasValue ? fCreateDate.Value : System.Data.SqlTypes.SqlDateTime.Null));
			param.Add(new SqlParameter("@13", fCreatePerson ?? System.Data.SqlTypes.SqlString.Null));
			param.Add(new SqlParameter("@14", fUpdateDate.HasValue ? fUpdateDate.Value : System.Data.SqlTypes.SqlDateTime.Null));
			param.Add(new SqlParameter("@15", fUpdatePerson ?? System.Data.SqlTypes.SqlString.Null));
			return param.ToArray();
		}

		/// <summary>
		/// UPDATE SETパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetUpdateSetParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", fCustomerID),
				new SqlParameter("@2", fServiceId),
				new SqlParameter("@3", fYears),
				new SqlParameter("@4", fGoodsID ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", fApplyDate.HasValue ? fApplyDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@6", fContractStartDate.HasValue ? fContractStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", fContractEndDate.HasValue ? fContractEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", fBillingStartDate.HasValue ? fBillingStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", fBillingEndDate.HasValue ? fBillingEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", fEndFlag ? "1" : "0"),
				new SqlParameter("@11", fDeleteFlag ? "1" : "0"),
				new SqlParameter("@12", fCreateDate.HasValue ? fCreateDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@13", fCreatePerson ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@14", fUpdateDate.HasValue ? fUpdateDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@15", fUpdatePerson ?? System.Data.SqlTypes.SqlString.Null)
			};
			return param;
		}

		/// <summary>
		/// tMik保守契約による格納
		/// </summary>
		/// <param name="data"></param>
		public void Set_tMik保守契約(tMik保守契約 data)
		{
			fApplyNo = 0;
			fCustomerID = data.fhsCliMicID;
			fYears = data.fhsS契約年数;
			if (3 == data.fhsS契約年数)
			{
				// PC安心ｻﾎﾟｰﾄ(3年契約)
				fServiceId = (int)ServiceCodeDefine.ServiceCode.PcSafetySupport3;
				fGoodsID = PcaGoodsIDDefine.PcSupport3;
			}
			else
			{
				// PC安心ｻﾎﾟｰﾄ(1年契約)
				fServiceId = (int)ServiceCodeDefine.ServiceCode.PcSafetySupport1;
				fGoodsID = PcaGoodsIDDefine.PcSupport1;
			}
			if (data.fhsS契約書回収年月.HasValue)
			{
				fApplyDate = data.fhsS契約書回収年月.Value.ToDateTime();
			}
			else
			{
				fApplyDate = null;
			}
			if (data.fhsSメンテ契約開始.HasValue)
			{
				fContractStartDate = data.fhsSメンテ契約開始.Value.ToDate(1);
			}
			else
			{
				fContractStartDate = null;
			}
			if (data.fhsSメンテ契約終了.HasValue)
			{
				fContractEndDate = data.fhsSメンテ契約終了.Value.ToDate(-1);
			}
			else
			{
				fContractEndDate = null;
			}
			fBillingStartDate = null;
			fBillingEndDate = null;
			fEndFlag = false;
			fDeleteFlag = false;
			fCreateDate = DateTime.Now;
			fCreatePerson = "suguro";
			fUpdateDate = null;
			fUpdatePerson = null;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ESETライセンス管理情報</returns>
		public static List<T_USE_PCCSUPPORT> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_USE_PCCSUPPORT> result = new List<T_USE_PCCSUPPORT>();
				foreach (DataRow row in table.Rows)
				{
					T_USE_PCCSUPPORT data = new T_USE_PCCSUPPORT
					{
						fApplyNo = DataBaseValue.ConvObjectToInt(row["fApplyNo"]),
						fCustomerID = DataBaseValue.ConvObjectToInt(row["fCustomerID"]),
						fServiceId = DataBaseValue.ConvObjectToInt(row["fServiceId"]),
						fYears = DataBaseValue.ConvObjectToInt(row["fYears"]),
						fGoodsID = row["fGoodsID"].ToString().Trim(),
						fApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["fApplyDate"]),
						fContractStartDate = DataBaseValue.ConvObjectToDateNullByDate(row["fContractStartDate"]),
						fContractEndDate = DataBaseValue.ConvObjectToDateNullByDate(row["fContractEndDate"]),
						fBillingStartDate = DataBaseValue.ConvObjectToDateNullByDate(row["fBillingStartDate"]),
						fBillingEndDate = DataBaseValue.ConvObjectToDateNullByDate(row["fBillingEndDate"]),
						fEndFlag = ("0" == row["fEndFlag"].ToString()) ? false : true,
						fDeleteFlag = ("0" == row["fDeleteFlag"].ToString()) ? false : true,
						fCreateDate = DataBaseValue.ConvObjectToDateTimeNull(row["fCreateDate"]),
						fCreatePerson = row["fCreatePerson"].ToString().Trim(),
						fUpdateDate = DataBaseValue.ConvObjectToDateTimeNull(row["fUpdateDate"]),
						fUpdatePerson = row["fUpdatePerson"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
