﻿using MwsLib.BaseFactory.WebJucuOrderFile;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Estore;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.WebJucuOrderFile
{
	public static class WebJucuOrderFileAccess
	{
		/// <summary>
		/// WebJucuの取得
		/// </summary>
		/// <param name="date">指定日</param>
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static List<WebJucu> Select_WebJucu(Date date, bool ct)
		{
			string sqlStr = string.Format("SELECT OA.* FROM {0} as OA"
											+ " LEFT JOIN {1} as LG on OA.order_accept_id = LG.ID"
											+ " WHERE OA.order_accept_id >= 1 AND convert(int, convert(nvarchar, OA.order_dt, 112)) >= {2} AND LG.web受注No is null"
											+ " ORDER BY OA.order_no, OA.order_accept_id"
											, EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMicOrder_accept]
											, EstoreDatabaseDefine.TableName[EstoreDatabaseDefine.TableType.tMICestore_log]
											, date.ToIntYMD());
			DataTable table = EstoreDatabaseAccess.EstoreDatabaseDataAdpter(sqlStr, ct);
			return WebJucu.DataTableToList(table);
		}
/*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="date">指定日</param>
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static List<BaseFactory.Estore.WebJucu> GetWebJucu(Date date, bool ct)
		{
			string sqlStr = string.Format("SELECT"
								+ " OA.order_accept_id as order_accept_id"
								+ ", OA.order_no as 受注No"
								+ ", convert(nvarchar, [order_dt], 112) as 受注日"
								+ ", '0' as 納期"
								+ ", CM.得意先No as 得意先No"
								+ ", CM.顧客名 as 顧客名"
								+ ", '' as 直送先コード"
								+ ", '' as 先方担当者"
								+ ", BC.PCA部門No as PCA部門No"
								+ ", BC.PCA主担当No as PCA主担当No"
								+ ", '031' as 摘要コード"
								+ ", 'Web受注分' as 摘要名"
								+ ", OA.goods_code as goods_code"
								+ ", '0' as マスター区分"
								+ ", GM.商品名 as 商品名"
								+ ", '0' as 倉庫コード"
								+ ", '0' as 入数"
								+ ", '0' as 箱数"
								+ ", convert(nvarchar, OA.order_num) as 数量"
								+ ", '' as 単位"
								+ ", convert(nvarchar, OA.web_price) as 単価"
								+ ", convert(nvarchar, OA.order_num * OA.web_price) as 受注金額"
								+ ", convert(nvarchar, convert(int, PCA.sms_gen)) as 原単価"
								+ ", convert(nvarchar, convert(int, PCA.sms_gen * OA.order_num)) as 原価額"
								+ ", '0' as 粗利益"
								+ ", '0' as 外税額"
								+ ", '0' as 内税額"
								+ ", '2' as 税区分"
								+ ", '0' as 税込区分"
								+ ", '' as 備考"
								+ ", '0' as 標準価格"
								+ ", '0' as 自動発注区分"
								+ ", '0' as 売単価"
								+ ", '0' as 売価金額"
								+ ", '' as 規格型番"
								+ ", '' as 色"
								+ ", '' as サイズ"
								+ ", '0' as 計算式コード"
								+ ", '0' as 商品項目1"
								+ ", '0' as 商品項目2"
								+ ", '0' as 商品項目3"
								+ ", '0' as 売上項目1"
								+ ", '0' as 売上項目2"
								+ ", '0' as 売上項目3"
								+ ", OA.pref_arrival_date as 希望着日"
								+ " FROM ((([estoreDB].[dbo].[vMicOrder_accept] as OA"
								+ " INNER JOIN [estoreDB].[dbo].[vMic顧客マスタ] as CM ON OA.customer_no = CM.顧客No)"
								+ " INNER JOIN [estoreDB].[dbo].[vMic部門コード] as BC ON CM.顧客No = BC.顧客Ｎｏ)"
								+ " INNER JOIN [estoreDB].[dbo].[vMic商品マスタ] as GM ON OA.goods_code = GM.商品コード)"
								+ " INNER JOIN [JunpDB].[dbo].[vMicPCA商品マスタ] as PCA ON GM.商品コード = PCA.sms_scd"
								+ " WHERE OA.order_accept_id >= 1 and convert(int, convert(nvarchar, OA.order_dt, 112)) >= {0}"
								+ " ORDER BY OA.order_accept_id"
								, date.ToIntYMD());
			DataTable table = EstoreDatabaseAccess.EstoreDatabaseDataAdpter(sqlStr, ct);
			return BaseFactory.Estore.WebJucu.DataTableToList(table);
		}
*/
	}
}