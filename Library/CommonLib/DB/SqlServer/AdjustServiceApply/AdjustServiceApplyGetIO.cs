//
// AdjustServiceApplyGetIO.cs
//
// サービス申込情報更新処理 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DB.SqlServer.AdjustServiceApply
{
	public static class AdjustServiceApplyGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// WW伝票参照ビュー抽出
		/// WW伝票view.受注承認日が締日の範囲内で、数量>0の、伝票番号が最小の伝票データの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetWonderWebSlip(string connectStr)
		{
			string strSQL = string.Format(@"SELECT Z.伝票No"
														+ ", Z.販売先顧客ID"
														+ ", Z.ユーザー顧客ID"
														+ ", Z.担当者ID"
														+ ", Z.担当者名"
														+ ", Z.担当支店ID"
														+ ", Z.担当支店名"
														+ ", Z.受注年月日"
														+ ", Z.受注承認日"
														+ ", Z.売上承認日"
														+ ", Z.商品コード"
														+ ", Z.商品名"
														+ ", Z.商品区分"
														+ ", Z.数量"
														+ ", Z.販売価格"
														+ ", Z.申込種別"
														+ ", Z.システム略称"
														+ ", Z.最終出力日時"
														+ " FROM {0} as Z"
														+ " INNER HASH JOIN"
														+ " ("
														+ " SELECT *"
														+ " FROM"
														+ " (SELECT"
														+ "  Y.ユーザー顧客ID"
														+ ", Y.商品コード"
														+ ", SUM(Y.数量) AS sumQUANTITY"
														+ ", COUNT(Y.数量) AS CNT"
														+ ", MIN(伝票No) AS minCHECK_NO"
														+ " FROM {0} as Y"
														+ " WHERE 受注承認日 is not null"
														+ " GROUP BY ユーザー顧客ID, 商品コード"
														+ ") as tblA"
														+ " WHERE sumQUANTITY > 0"
														+ ") as X ON X.minCHECK_NO = Z.伝票No AND X.ユーザー顧客ID = Z.ユーザー顧客ID AND X.商品コード = Z.商品コード"
														+ " WHERE Z.受注承認日 is not null"
														+ " ORDER BY 伝票No, ユーザー顧客ID, 商品コード"
														, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.WW伝票参照ビュー]);
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 販売店情報参照ビューから販売店コードを取得
		/// </summary>
		/// <param name="storeCode">販売店コード</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetStoreCode(int storeCode, string connectStr)
		{
			string strSQL = string.Format(@"SELECT SI.[販売店コード]"
														+ " FROM {0} as VR"
														+ " INNER JOIN {1} as SI ON VR.[区分コード] = CONVERT(int, SI.[販売店ランクコード]) AND VR.[ランク] is not null"
														+ " WHERE SI.[販売店コード] = {2}"
														, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.販売店区分参照ビュー]
														, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.販売店情報参照ビュー]
														, storeCode);
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 基本機能パック 商品コード、サービス種別ID、サービスIDの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetKihonPack(string connectStr)
		{
			string strSQL = string.Format(@"SELECT TOP 1 A.*"
												+ " FROM {0} as A"
												+ " INNER JOIN {1} as B on A.GOODS_ID = B.GOODS_ID AND B.[BRAND_CLASSIFICATION] = 200"
												+ " WHERE A.DELETE_FLG = '0' AND SET_SALE = '1'"
												+ " ORDER BY A.[GOODS_ID]"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_CODE]
												, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.V_PCA_GOODS]	);
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
	}
}
