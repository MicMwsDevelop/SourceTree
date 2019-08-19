using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.BaseFactory.WonderWebMemo;
using System.Data;
using System.Data.SQLite;
using System.IO;


namespace MwsLib.DB.SQLite.WonderWebMemo
{
	public static class SQLiteWonderWebMemoSetIO
	{
		/// <summary>
		/// 銀行振込用請求書メモの追加
		/// </summary>
		/// <param name="dbPath">データベースパス</param>
		/// <param name="list">銀行振込用請求書リスト</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoBankTransfer(string dbPath, List<MemoBankTransfer> list)
		{
			int rowCount = -1;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, WonderWebMemoDef.WONDER_WEB_MEMO_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2)", WonderWebMemoDef.BANK_TRANSFER_TABLE_NAME);
							foreach (MemoBankTransfer info in list)
							{
								SQLiteParameter[] param = { new SQLiteParameter("@1", info.TokuisakiNo),		// 得意先No
															new SQLiteParameter("@2", info.BillingAmount) };	// 請求額

								// 実行
								rowCount = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertIntoBankTransfer() Error!");
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
