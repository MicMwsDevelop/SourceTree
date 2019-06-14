using System.Data;
using System.Data.SQLite;
using System.IO;
using MwsLib.BaseFactory.ScanImageData;

namespace MwsLib.DB.SQLite.ScanImageData
{
	public static class SQLiteScanImageDataGetIO
	{
		/// <summary>
		/// インデックスファイル情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>バージョン情報</returns>
		public static DataTable GetIndexFileInfo(string dbPath)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, ScanImageDataDef.SCAN_IMAGE_DATA_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} ORDER BY TOKUISAKI_NO ASC", ScanImageDataDef.INDEX_FILE_TABLE_NAME);
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
