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
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.BaseFactory.Charlie.Table
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
				fGoodsID = PcaGoodsIDDefine.PcSafetySupport3;
			}
			else
			{
				// PC安心ｻﾎﾟｰﾄ(1年契約)
				fServiceId = (int)ServiceCodeDefine.ServiceCode.PcSafetySupport1;
				fGoodsID = PcaGoodsIDDefine.PcSafetySupport1;
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
	}
}
