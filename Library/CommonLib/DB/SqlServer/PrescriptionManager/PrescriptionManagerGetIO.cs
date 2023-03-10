//
// PrescriptionManagerGetIO.cs
// 
// 電子処方箋契約情報 売上明細データ、仕入明細データ 出力情報取得I/Oクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/02/14 勝呂):新規作成
//
using CommonLib.BaseFactory.Junp.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System.Data;

namespace CommonLib.DB.SqlServer.PrescriptionManager
{
	public static class PrescriptionManagerGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 電子処方箋契約情報から運用開始日が先月の医院の取得
		/// </summary>
		/// <param name="today">当日</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>レコード数</returns>
		public static DataTable GetPrescriptionEarnings(Date today, string connectStr)
		{
			int prevFirstDate = today.FirstDayOfLasMonth().ToIntYMD();

			string strSQL = string.Format(@"SELECT"
											+ "H.fContractID as 申込No"
											+ ",H.[fCustomerID] as 顧客No"
											+ ",U.[顧客名１] + U.[顧客名２] as 顧客名"
											+ ",U.[得意先No] as 得意先コード"
											+ ",U.[請求先コード] as 請求先コード"
											+ ",U.[請求先名] as 請求先名"
											+ ",H.[fOperationDate] as 運用開始日"
											+ ",H.[fContractStartDate] as 契約開始日"
											+ ",H.[fContractEndDate] as 契約終了日"
											+ ",'月' as 更新単位"
											+ ",M.[sms_scd] as 商品コード"
											+ ",M.[sms_mei] as 商品名"
											+ ",CONVERT(int, M.[sms_hyo]) as 標準価格"
											+ ",CONVERT(int, M.[sms_gen]) as 原単価"
											+ ",M.[sms_tani] as 単位"
											+ ",B.[fPca部門コード] as 部門コード"
											+ ",B.[fPca倉庫コード] as 倉庫コード"
											+ ",B.[f担当者コード] as 担当者コード"
											+ " FROM {0} as H"
											+ " INNER JOIN {1} as U on H.fCustomerID = U.顧客No"
											+ " INNER JOIN {2}  as M on M.sms_scd = H.fGoodsID"
											+ " INNER JOIN {3} as B on B.fBshCode3 = U.支店コード"
											+ " WHERE H.[fEndFlag] = '0' AND H.[fDeleteFlag] = '0' AND H.[fOperationDate] is not null AND CONVERT(int, CONVERT(NVARCHAR, DATEADD(dd, 1, EOMONTH(H.[fOperationDate], -1)), 112)) = {4}"
											+ " ORDER BY 部門コード ASC, 得意先コード ASC"
											, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PRESCRIPTION_HEADER]
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicユーザー基本]
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
											, prevFirstDate);;
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 電子処方箋契約情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>レコード数</returns>
		public static DataTable GetUsePrescriptionList(string connectStr, string where)
		{
			string strSQL = string.Format(@"SELECT"
											+ " H.[fContractID] as 申込No"
											+ ",H.[fCustomerID] as 顧客No"
											+ ",U.[顧客名１] + U.[顧客名２] as 顧客名"
											+ ",H.[fGoodsID] as 商品ID"
											+ ",M.[sms_mei] as 商品名"
											+ ",H.[fOrderReserveID] as 注文ID"
											+ ",H.[fApplyDate] as 申込日時"
											+ ",H.[fOperationDate] as 運用開始日"
											+ ",H.[fContractStartDate] as 契約開始日"
											+ ",H.[fContractEndDate] as 契約終了日"
											+ ",H.[fBillingStartDate] as 課金開始日"
											+ ",H.[fBillingEndDate] as 課金終了日"
											+ ",H.[fEndFlag] as 終了フラグ"
											+ ",H.[fDeleteFlag] as 削除フラグ"
											+ ",H.[fCreateDate] as 作成日"
											+ ",H.[fCreatePerson] as 作成者"
											+ ",H.[fUpdateDate] as 更新日"
											+ ",H.[fUpdatePerson] as 更新者"
											+ " FROM {0} as H"
											+ " LEFT JOIN {1} as U on U.[顧客No] = H.[fCustomerID]"
											+ " LEFT JOIN {2}  as M on M.[sms_scd] = H.[fGoodsID]"
											+ " {3}"
											+ " ORDER BY 申込No ASC"
											, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PRESCRIPTION_HEADER]
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicユーザー基本]
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
											, where);
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
	}
}
