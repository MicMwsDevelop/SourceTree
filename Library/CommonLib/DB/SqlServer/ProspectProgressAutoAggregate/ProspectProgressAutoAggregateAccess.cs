//
// ProspectProgressAutoAggregateAccess.cs
//
// 見込進捗自動集計 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/08/04 勝呂)
// 
using CommonLib.BaseFactory.ProspectProgressAutoAggregate;
using CommonLib.Common;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.ProspectProgressAutoAggregate
{
	public static class ProspectProgressAutoAggregateAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 売上予想の取得
		/// </summary>
		/// <param name="today">当日</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>売上予想リスト</returns>
		public static List<売上予想> Select_売上予想(Date today, string connectStr)
		{
			DataTable dt = ProspectProgressAutoAggregateGetIO.Select_売上予想(today, connectStr);
			return 売上予想.DataTableToList(dt, today);
		}

		/// <summary>
		/// ES売上予想の取得
		/// </summary>
		/// <param name="start">計上開始月</param>
		/// <param name="end">計上終了月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>売上予想リスト</returns>
		public static List<ES売上予想> Select_ES売上予想(Date start, Date end, string connectStr)
		{
			DataTable dt = ProspectProgressAutoAggregateGetIO.Select_ES売上予想(start, end, connectStr);
			return ES売上予想.DataTableToList(dt);
		}

		/// <summary>
		/// 予測連絡用ESの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>売上予想リスト</returns>
		public static List<予測連絡用ES> Select_予測連絡用ES(string connectStr)
		{
			DataTable dt = ProspectProgressAutoAggregateGetIO.Select_予測連絡用ES(connectStr);
			return 予測連絡用ES.DataTableToList(dt);
		}

		/// <summary>
		/// 予測連絡用まとめの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>売上予想リスト</returns>
		public static List<予測連絡用まとめ> Select_予測連絡用まとめ(string connectStr)
		{
			DataTable dt = ProspectProgressAutoAggregateGetIO.Select_予測連絡用まとめ_WW(connectStr);
			List<予測連絡用まとめ> wwList = 予測連絡用まとめ.DataTableToList(dt);
			dt = ProspectProgressAutoAggregateGetIO.Select_予測連絡用まとめ_契約情報(connectStr);
			List<予測連絡用まとめ> agreeList = 予測連絡用まとめ.DataTableToList(dt);
			foreach (予測連絡用まとめ agree in agreeList)
			{
				if (-1 == wwList.FindIndex(p => p.顧客No == agree.顧客No && p.課金開始日 == agree.課金開始日 && p.課金終了日 == agree.課金終了日))
				{
					wwList.Add(agree);
				}
			}
			return wwList;
		}
	}
}
