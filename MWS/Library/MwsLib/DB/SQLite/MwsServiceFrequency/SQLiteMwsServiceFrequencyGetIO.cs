using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MwsLib.Common;
using System.Data;
using System.Data.SQLite;

namespace MwsLib.DB.SQLite.MwsServiceFrequency
{
	public static class SQLiteMwsServiceFrequencyGetIO
	{
		public static bool IsExistUsedMonth(string dbPath, YearMonth ym)
		{
			bool result = false;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsServiceFrequencyDef.MWS_SERVICE_FREQUENCY_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT UsedMonth FROM {0} WHERE UsedMonth = {1}", SQLiteMwsServiceFrequencyDef.USED_SERIVE_TABLE_NAME, ym.ToIntYM());
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							DataTable table = new DataTable();
							da.Fill(table);
							if (0 < table.Rows.Count)
							{
								result = true;
							}
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
	}
}
