using System.Data;
using System.Data.SQLite;
using System.IO;

namespace MwsLib.DB.SQLite.StockGoodsMaster
{
	public static class SQLiteStockGoodsMasterGetIO
	{
		/// <summary>
		/// AOSクラウドデータバンク仕入商品マスタの取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns></returns>
		public static DataTable GetStockGoodsMasterCloudDataBank(string dbPath)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteStockGoodsMasterAccess.DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} ORDER BY 商品コード ASC", SQLiteStockGoodsMasterAccess.AOS_CLOUDDATABANK_TABLE_NAME);
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
