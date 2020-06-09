//
// CloudDataBankAccess.cs
//
// クラウドデータバンクPCA売上明細情報 データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/03/06 勝呂)
// 
using MwsLib.BaseFactory.Junp.View;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.CloudDataBank
{
	public static class CloudDataBankAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 指定期間のクラウドデータバンクPCA売上明細情報リストの取得
		/// </summary>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>PCA売上明細情報リスト</returns>
		public static List<vMicPCA売上明細> GetCloudDataBankEarningsList(string goods, Span span, bool ct)
		{
			DataTable table = CloudDataBankGetIO.GetCloudDataBankEarningsList(goods, span, ct);
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
	}
}
