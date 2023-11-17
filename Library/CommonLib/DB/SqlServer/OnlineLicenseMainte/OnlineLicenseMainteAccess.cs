//
// OnlineLicenseMainteAccess.cs
//
// オンライン資格確認保守サービス ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/10/04 勝呂):新規作成
// 
using CommonLib.BaseFactory.OnlineLicenseMainte;
using CommonLib.Common;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.OnlineLicenseMainte
{
	static public class OnlineLicenseMainteAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// アプリケーション情報からオンライン資格確認保守サービスの更新対象医院の取得
		/// </summary>
		/// <param name="mainteYM">保守終了年月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>オンライン資格確認保守サービス売上情報リスト</returns>
		public static List<OnlineLicenseMainteEarningsOut> GetOnlineLicenseMainteEarningsOut(YearMonth mainteYM, string connectStr)
		{
			DataTable dt = OnlineLicenseMainteGetIO.GetOnlineLicenseMainteEarningsOut(mainteYM, connectStr);
			return OnlineLicenseMainteEarningsOut.DataTableToList(dt);
		}
	}
}
