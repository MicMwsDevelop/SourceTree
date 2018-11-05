//
// CalcBusinessConsignCommissionAccess.cs
// 
// PCA仕入データ業務委託手数料再計算ツール SQL SERVERデータベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/05 勝呂)
//
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.CalcBusinessConsignCommission
{
	/// <summary>
	/// PCA仕入データ業務委託手数料再計算ツール SQL SERVERデータベースアクセスクラス
	/// </summary>
	public static class CalcBusinessConsignCommissionAccess
	{
		/// <summary>
		/// 販売店情報の取得
		/// </summary>
		/// <returns>レコード数</returns>
		public static List<Tuple<string, int>> GetSalesOutletInfo()
		{
			DataTable dt = CalcBusinessConsignCommissionGetIO.GetSalesOutletInfo();
			return CalcBusinessConsignCommissionController.ConvertSalesOutletInfo(dt);
		}
	}
}
