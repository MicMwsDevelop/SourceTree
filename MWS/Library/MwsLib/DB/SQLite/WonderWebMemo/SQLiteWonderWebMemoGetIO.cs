using System.Data;
using System.Data.SQLite;
using System.IO;
using MwsLib.BaseFactory.WonderWebMemo;

namespace MwsLib.DB.SQLite.WonderWebMemo
{
	public static class SQLiteWonderWebMemoGetIO
	{
		/// <summary>
		/// 銀行振込請求書発行先メモ情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns></returns>
		public static DataTable GetMemoBankTransfer(string dbPath)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, WonderWebMemoDef.WONDER_WEB_MEMO_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} ORDER BY TOKUISAKI_NO ASC", WonderWebMemoDef.BANK_TRANSFER_TABLE_NAME);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
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
	}
}
