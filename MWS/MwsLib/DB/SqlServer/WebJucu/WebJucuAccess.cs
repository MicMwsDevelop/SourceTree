using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.BaseFactory.Estore;
using MwsLib.BaseFactory.Estore.Table;
using MwsLib.BaseFactory.Estore.View;
using MwsLib.Common;
using System.Data;
using MwsLib.DB.SqlServer.Estore;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.WebJucu
{
	public static class WebJucuAccess
	{
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

		/// <summary>
		/// estoreログの取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static List<tMICestore_log> Get_tMicEstoreLog(bool ct)
		{
			//string sqlStr = string.Format("SELECT * FROM {0} WHERE web受注No is null ORDER BY ID", EstoreDatabaseDefine.TableName[EstoreDatabaseDefine.TableType.tMICestore_log]);
			string sqlStr = string.Format("SELECT * FROM {0} ORDER BY ID", EstoreDatabaseDefine.TableName[EstoreDatabaseDefine.TableType.tMICestore_log]);
			DataTable table = EstoreDatabaseAccess.EstoreDatabaseDataAdpter(sqlStr, ct);
			return tMICestore_log.DataTableToList(table);
		}

		/// <summary>
		/// vMic部門コードの取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static List<vMic部門コード> Get_vMic部門コード(bool ct)
		{
			string sqlStr = string.Format("SELECT * FROM {0}", EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMic部門コード]);
			DataTable table = EstoreDatabaseAccess.EstoreDatabaseDataAdpter(sqlStr, ct);
			return vMic部門コード.DataTableToList(table);
		}

		/// <summary>
		/// vMic受注最大番号の取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static int Get_vMic受注最大番号(bool ct)
		{
			string sqlStr = string.Format("SELECT * FROM {0}", EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMic受注最大番号]);
			DataTable table = EstoreDatabaseAccess.EstoreDatabaseDataAdpter(sqlStr, ct);
			if (0 < table.Rows.Count)
			{
				DataRow row = table.Rows[0];
				return DataBaseValue.ConvObjectToInt(row["j_max"]);
			}
			return 0;
		}

		/// <summary>
		/// tMICestore_logリストの新規追加
		/// </summary>
		/// <param name="list"></param>
		/// <param name="ct">CT環境</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_tMICestore_log(List<tMICestore_log> list, bool ct)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SqlTransaction tran = con.BeginTransaction())
					{
						try
						{
							foreach (tMICestore_log log in list)
							{
								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, tMICestore_log.InsertIntoSqlString, log.GetInsertIntoParameters());
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertInto_tMICestore_log() Error!");
								}
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}
	}
}
