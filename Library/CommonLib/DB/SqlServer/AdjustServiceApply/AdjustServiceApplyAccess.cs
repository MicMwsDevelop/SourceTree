//
// AdjustServiceApplyAccess.cs
//
// サービス申込情報更新処理 データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.BaseFactory.AlmexMainte;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.AlmexMainte;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.AdjustServiceApply
{
	public static class AdjustServiceApplyAccess
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// WW伝票参照ビュー抽出から受注承認日が締日の範囲内で、数量>0の、伝票番号が最小の伝票データの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>WW伝票参照ビューリスト</returns>
		public static List<WW伝票参照ビュー> GetWonderWebSlip(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetWonderWebSlip(connectStr);
			return WW伝票参照ビュー.DataTableToList(table);
		}

		/// <summary>
		/// 販売店情報参照ビューから販売店コードを取得
		/// </summary>
		/// <param name="storeCode">販売店コード</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>WW伝票参照ビューリスト</returns>
		public static List<int> GetStoreCode(int storeCode, string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetStoreCode(storeCode, connectStr);
			if (null != table && 0 < table.Rows.Count)
			{
				List<int> result = new List<int>();
				foreach (DataRow row in table.Rows)
				{
					result.Add(DataBaseValue.ConvObjectToInt(row["販売店コード"]));
				}
			}
			return null;
		}

		/// <summary>
		/// 基本機能パック 商品コード、サービス種別ID、サービスIDの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>MWSコードマスタ</returns>
		public static M_CODE GetKihonPack(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetKihonPack(connectStr);
			return M_CODE.DataTableToData(table);
		}
	}
}
