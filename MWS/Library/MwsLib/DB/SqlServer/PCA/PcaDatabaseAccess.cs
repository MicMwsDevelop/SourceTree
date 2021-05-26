//
// PcaDatabaseAccess.cs
//
// P10V01C001KON0001 データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/04/15 勝呂)
// 
using MwsLib.BaseFactory.Junp.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MwsLib.Common;

namespace MwsLib.DB.SqlServer.PCA
{
	public static class PcaDatabaseAccess
	{
		/// <summary>
		/// [P10V01C001KON0001] レコードの更新
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetPcaDatabase(string sqlStr, SqlParameter[] param, bool ct)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreatePcaWebConnectionString(ct)))
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
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlStr, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("UpdateSetPcaDatabase() Error!");
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
