using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.BaseFactory.AnalysisRecommendService;

namespace MwsLib.DB.SqlServer.AnalysisRecommendService
{
	public static class AnalysisRecommendServiceAccess
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		public static RecommendService GetRecommendService(int customerNo, bool ct)
		{
			DataTable table = AnalysisRecommendServiceGetIO.GetRecommendService(customerNo, ct);
			return RecommendService.DataTableToData(table);
		}

		/// <summary>
		/// 指定期間のクラウドバックアップPCA売上明細情報リストの取得
		/// </summary>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>PCA売上明細情報リスト</returns>
		public static List<RecommendService> GetRecommendServiceList(bool ct)
		{
			DataTable table = AnalysisRecommendServiceGetIO.GetRecommendServiceList(ct);
			return RecommendService.DataTableToList(table);
		}
	}
}
