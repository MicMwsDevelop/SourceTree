//
// CheckOrderSlipGetIO.cs
//
// 受注伝票情報 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.CheckOrderSlip
{
	public static class CheckOrderSlipGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 受注伝票情報リストの取得
		/// </summary>
		/// <param name="whereStr">where文</param>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		private static DataTable GetSlipList(string whereStr, Date date, List<string> goods, bool sqlsv2)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string goodsWhere = string.Empty;
					for (int i = 0; i < goods.Count; i++)
					{
						if (0 == i)
						{
							goodsWhere = " AND (";
						}
						else
						{
							goodsWhere += " OR ";
						}
						goodsWhere += string.Format("D.[f商品コード] = '{0}'", goods[i]);
					}
					goodsWhere += ")";

					string strSQL = string.Format(@"SELECT D.[f受注番号] AS f受注番号"
					+ ", H.[f受注日] AS f受注日"
					+ ", H.[f受注承認日] AS f受注承認日"
					+ ", H.[f売上承認日] AS f売上承認日"
					+ ", H.[f納期] AS f納期"
					+ ", H.[f販売種別] AS f販売種別"
					+ ", H.[fユーザーコード] AS fユーザーコード"
					+ ", H.[fユーザー] AS fユーザー"
					+ ", D.[f商品コード] AS f商品コード"
					+ ", D.[f商品名] AS f商品名"
					+ ", H.[f受注金額] AS f受注金額"
					+ ", H.[fSV利用開始年月] AS fSV利用開始年月"
					+ ", H.[fSV利用終了年月] AS fSV利用終了年月"
					+ ", H.[f販売先コード] AS f販売先コード"
					+ ", H.[f販売先] AS f販売先"
					+ ", H.[fBshCode3] AS fBshCode3"
					+ ", H.[f担当支店名] AS f担当支店名"
					+ ", H.[f担当者コード] AS f担当者コード"
					+ ", H.[f担当者名] AS f担当者名"
					+ ", H.[f件名] AS f件名"
					+ ", H.[fリプレース] AS fリプレース"
					+ " FROM {0} AS D"
					+ " LEFT JOIN {1} AS H ON D.[f受注番号] = H.[f受注番号]"
					+ " WHERE D.[f数量] > 0 AND CONVERT(int, CONVERT(nvarchar, {2}, 112)) >= {3} {4}"
					+ " ORDER BY {2} ASC, H.[fユーザーコード] ASC, D.[f受注番号] ASC, D.[f表示順] ASC"
					, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注詳細]
					, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]
					, whereStr
					, date.ToIntYMD()
					, goodsWhere);
  
					using (SqlCommand cmd = new SqlCommand(strSQL, con))
					{
						using (SqlDataAdapter da = new SqlDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// 受注伝票情報リストの取得(受注日)
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		public static DataTable GetOrderSlipList(Date date, List<string> goods, bool ct)
		{
			return CheckOrderSlipGetIO.GetSlipList("H.[f受注日]", date, goods, ct);
		}

		/// <summary>
		/// 受注伝票情報リストの取得(受注承認日)
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		public static DataTable GetOrderAcceptSlipList(Date date, List<string> goods, bool ct)
		{
			return CheckOrderSlipGetIO.GetSlipList("H.[f受注承認日]", date, goods, ct);
		}

		/// <summary>
		/// 受注伝票情報リストの取得(売上承認日)
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		public static DataTable GetSaleSlipList(Date date, List<string> goods, bool ct)
		{
			return CheckOrderSlipGetIO.GetSlipList("H.[f売上承認日]", date, goods, ct);
		}
	}
}
