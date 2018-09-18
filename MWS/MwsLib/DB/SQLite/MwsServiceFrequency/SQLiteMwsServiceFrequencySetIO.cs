using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using MwsLib.BaseFactory.MwsServiceFrequency;
using System.IO;
using MwsLib.Common;

namespace MwsLib.DB.SQLite.MwsServiceFrequency
{
	public static class SQLiteMwsServiceFrequencySetIO
	{
		public static int InsertIntoMwsServiceFrequencyDataList(string dbPath, MwsServiceFrequencyDataList list)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsServiceFrequencyDef.MWS_SERVICE_FREQUENCY_DATABASE_NAME))))
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
							rowCount = 0;
							foreach (MwsServiceFrequencyData data in list)
							{
								rowCount += InsertIntoMwsServiceFrequencyData(con, tran, data);
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

		private static int InsertIntoMwsServiceFrequencyData(SQLiteConnection con, SQLiteTransaction tran, MwsServiceFrequencyData data)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7)", SQLiteMwsServiceFrequencyDef.USED_SERIVE_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", data.UsedMonth.ToIntYM()),
										new SQLiteParameter("@2", data.CostomerID),
										new SQLiteParameter("@3", data.ServiceCode),
										new SQLiteParameter("@4", data.ServiceName),
										new SQLiteParameter("@5", data.UsedCount),
										new SQLiteParameter("@6", data.Ken),
										new SQLiteParameter("@7", data.BranchCode) };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		public static int DeleteAllMwsServiceFrequencyData(string dbPath, YearMonth ym)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsServiceFrequencyDef.MWS_SERVICE_FREQUENCY_DATABASE_NAME))))
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
							rowCount = DeleteAllMwsServiceFrequencyData(con, tran, ym);

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

		private static int DeleteAllMwsServiceFrequencyData(SQLiteConnection con, SQLiteTransaction tran, YearMonth ym)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0} WHERE UsedMonth = {1}", SQLiteMwsServiceFrequencyDef.USED_SERIVE_TABLE_NAME, ym.ToIntYM());

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}
	}
}
