//
// PurchaseTransferAccess.cs
//
// 仕入振替 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/05/27 勝呂)
// 
using MwsLib.BaseFactory.PurchaseTransfer;
using MwsLib.BaseFactory.Junp.View;
using System;
using System.Collections.Generic;
using System.Data;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.Common;
using System.Linq;
using MwsLib.BaseFactory.Pca;

namespace MwsLib.DB.SqlServer.PurchaseTransfer
{
	/// <summary>
	/// 仕入振替 ファイルアクセスクラス
	/// </summary>
	public static class PurchaseTransferAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 対象月全仕入れ明細の取得
		/// </summary>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境</param>
		/// <returns>PCA仕入明細リスト</returns>
		public static List<PCA仕入明細汎用データ> Get対象月全仕入れ明細(Span span, bool ct = false)
		{
			DataTable table = PurchaseTransferGetIO.GetIo対象月全仕入れ明細(span, ct);
			return PCA仕入明細汎用データ.DataTableToList(table);
		}

		/// <summary>
		/// 対象月社内仕入れ明細の取得
		/// </summary>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境</param>
		/// <returns>PCA仕入明細リスト</returns>
		public static List<PCA仕入明細汎用データ> Get対象月社内仕入れ明細(Span span, bool ct = false)
		{
			DataTable table = PurchaseTransferGetIO.GetIo対象月社内仕入れ明細(span, ct);
			return PCA仕入明細汎用データ.DataTableToList(table);
		}

		/// <summary>
		/// 社内使用分出荷明細リストの取得
		/// </summary>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境</param>
		/// <returns>社内使用分出荷明細リスト</returns>
		public static List<社内使用分出荷明細> Get社内使用分出荷明細(Span span, bool ct = false)
		{
			string whereStr = string.Format("urid_uribi Between {0} AND {1} AND urid_tcd Between '000387' And '000475'", span.Start.ToIntYMD(), span.End.ToIntYMD());
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA出荷データ], whereStr, "urid_uribi, urid_denno", ct);
			List<vMicPCA出荷データ> list = vMicPCA出荷データ.DataTableToList(table);
			List<社内使用分出荷明細> result = new List<社内使用分出荷明細>();
			foreach (vMicPCA出荷データ pca in list)
			{
				result.Add(new 社内使用分出荷明細(pca));
			}
			return result;
		}

		/// <summary>
		/// 貯蔵品社内使用分出荷明細リストの取得
		/// </summary>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境</param>
		/// <returns>貯蔵品社内使用分出荷明細リスト</returns>
		public static List<PCA出荷明細貯蔵品> Get貯蔵品社内使用分出荷明細(Span span, bool ct = false)
		{
			string whereStr = string.Format("urid_tcd Between '000387' AND '000475' AND (urid_souko = '0011' OR urid_souko = '0050') AND urid_uribi Between {0} AND {1} AND urid_scd Like 'A%' OR urid_scd Like 'B%' OR urid_scd Like 'C%' OR urid_scd Like 'D%' OR urid_scd Like 'E%'", span.Start.ToIntYMD(), span.End.ToIntYMD());
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA出荷データ], whereStr, "urid_uribi, urid_denno, urid_scd", ct);
			List<vMicPCA出荷データ> list = vMicPCA出荷データ.DataTableToList(table);
			List<PCA出荷明細貯蔵品> result = new List<PCA出荷明細貯蔵品>();
			foreach (vMicPCA出荷データ pca in list)
			{
				result.Add(new PCA出荷明細貯蔵品(pca));
			}
			return result;
		}

		/// <summary>
		/// 対象月仕入明細貯蔵品の取得
		/// </summary>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境</param>
		/// <returns>PCA仕入明細貯蔵品リスト</returns>
		public static List<PCA仕入明細貯蔵品> Get対象月仕入明細貯蔵品(Span span, bool ct = false)
		{
			DataTable table = PurchaseTransferGetIO.GetIo対象月仕入明細貯蔵品(span, ct);
			return PCA仕入明細貯蔵品.DataTableToList(table);
		}

		/// <summary>
		/// 売上明細月次の取得
		/// </summary>
		/// <param name="whereGoods">palette商品コード列</param>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境</param>
		/// <returns>売上明細月次リスト</returns>
		public static List<vMicPCA売上明細> Get売上明細月次(string whereGoods, Span span, bool ct = false)
		{
			DataTable table = PurchaseTransferGetIO.GetIo売上明細月次(whereGoods, span, ct);
			return vMicPCA売上明細.DataTableToList(table);
		}

		/// <summary>
		/// PCA商品マスタの取得
		/// </summary>
		/// <param name="scd">商品コード</param>
		/// <param name="ct">CT環境</param>
		/// <returns>PCA商品マスタリスト</returns>
		public static List<vMicPCA商品マスタ> GetPCA商品マスタ(string scd, bool ct = false)
		{
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ], string.Format("sms_scd = '{0}'", scd), "", ct);
			return vMicPCA商品マスタ.DataTableToList(table);
		}




		/*
				/// <summary>
				/// 拠店情報の取得
				/// </summary>
				/// <param name="sqlsv2">CT環境</param>
				/// <returns>拠店情報</returns>
				public static List<BranchEmployeeInfo> GetBranchEmployeeInfo(bool sqlsv2 = false)
				{
					DataTable dt = PcSupportManagerGetIO.GetBranchEmployeeInfo(sqlsv2);
					return PcSupportManagerController.ConvertBranchEmployeeInfo(dt);
				}

				/// <summary>
				/// PC安心サポート商品情報の取得
				/// </summary>
				/// <param name="sqlsv2">CT環境</param>
				/// <returns>PC安心サポート商品情報</returns>
				public static List<PcSupportGoodsInfo> GetPcSupportGoodsInfo(bool sqlsv2 = false)
				{
					DataTable dt = PcSupportManagerGetIO.GetPcSupportGoodsInfo(sqlsv2);
					return PcSupportManagerController.ConvertPcSupportGoodsInfo(dt);
				}

				/// <summary>
				/// 製品サポート情報ソフト保守情報の取得
				/// </summary>
				/// <param name="sqlsv2">CT環境</param>
				/// <returns>PC安心サポート商品情報</returns>
				public static List<SoftMaintenanceContract> GetSoftMaintenanceContract(bool sqlsv2 = false)
				{
					DataTable dt = PcSupportManagerGetIO.GetSoftMaintenanceContract(0, sqlsv2);
					return PcSupportManagerController.ConvertSoftMaintenanceContractList(dt);
				}

				/// <summary>
				/// 製品サポート情報ソフト保守情報の格納
				/// </summary>
				/// <param name="data">ソフト保守メンテナンス情報</param>
				/// <param name="sqlsv2">CT環境</param>
				/// <returns>レコード数</returns>
				public static int SetSoftMaintenanceContract(SoftMaintenanceContract data, bool sqlsv2 = false)
				{
					DataTable dt = PcSupportManagerGetIO.GetSoftMaintenanceContract(data.CustomerNo, sqlsv2);
					if (0 < dt.Rows.Count)
					{
						return PcSupportManagerSetIO.UpdateSoftMaintenanceContract(data, sqlsv2);
					}
					return PcSupportManagerSetIO.InsertIntoSoftMaintenanceContract(data, sqlsv2);
				}


				//////////////////////////////////////////////////////////////////
				/// CharlieDB
				//////////////////////////////////////////////////////////////////

				/// <summary>
				/// PC安心サポート管理情報の取得
				/// </summary>
				/// <param name="orderNo">受注No</param>
				/// <param name="sqlsv2">CT環境</param>
				/// <returns>PC安心サポート管理情報</returns>
				public static List<PcSupportControl> GetPcSupportControl(string orderNo = "", bool sqlsv2 = false)
				{
					DataTable dt = PcSupportManagerGetIO.GetPcSupportControl(orderNo, sqlsv2);
					return PcSupportManagerController.ConvertPcSupportControl(dt);
				}

				/// <summary>
				/// PC安心サポート管理情報の格納
				/// </summary>
				/// <param name="data">PC安心サポート管理情報</param>
				/// <param name="sqlsv2">CT環境</param>
				/// <returns>レコード数</returns>
				public static int SetPcSupportControl(PcSupportControl data, bool sqlsv2 = false)
				{
					DataTable dt = PcSupportManagerGetIO.GetPcSupportControl(data.OrderNo, sqlsv2);
					if (0 < dt.Rows.Count)
					{
						return PcSupportManagerSetIO.UpdatePcSupportControl(data, sqlsv2);
					}
					return PcSupportManagerSetIO.InsertIntoPcSupportControl(data, sqlsv2);
				}

				/// <summary>
				/// PC安心サポート管理情報リストの格納
				/// </summary>
				/// <param name="list">PC安心サポート管理情報リスト</param>
				/// <param name="sqlsv2">CT環境</param>
				/// <returns>レコード数</returns>
				public static int SetPcSupportControlList(List<PcSupportControl> list, bool sqlsv2 = false)
				{
					return PcSupportManagerSetIO.SetPcSupportControlList(list, sqlsv2);
				}

				/// <summary>
				/// 顧客メールアドレスの取得
				/// </summary>
				/// <param name="sqlsv2">CT環境</param>
				/// <returns>顧客メールアドレス</returns>
				public static List<Tuple<int, string>> GetCustomerMailAddress(bool sqlsv2 = false)
				{
					DataTable dt = PcSupportManagerGetIO.GetCustomerMailAddress(sqlsv2);
					return PcSupportManagerController.ConvertCustomerMailAddress(dt);
				}

				/// <summary>
				/// 拠店情報の取得
				/// </summary>
				/// <param name="sqlsv2">CT環境</param>
				/// <returns>拠店拠店情報</returns>
				public static List<BranchInfo> GetBranchInfo(bool sqlsv2 = false)
				{
					DataTable dt = PcSupportManagerGetIO.GetBranchInfo(sqlsv2);
					return PcSupportManagerController.ConvertBranchInfo(dt);
				}

				/// <summary>
				/// PC安心サポート送信メール情報リストの追加
				/// [Charlie].[dbo].[T_PC_SUPPORT_MAIL]
				/// </summary>
				/// <param name="list">PC安心サポート送信メール情報リスト</param>
				/// <param name="sqlsv2">CT環境かどうか？</param>
				/// <returns>影響行数</returns>
				public static int InsertIntoPcSupportMailList(List<PcSupportMail> list, bool sqlsv2 = false)
				{
					return PcSupportManagerSetIO.InsertIntoPcSupportMailList(list);
				}
		*/
	}
}
