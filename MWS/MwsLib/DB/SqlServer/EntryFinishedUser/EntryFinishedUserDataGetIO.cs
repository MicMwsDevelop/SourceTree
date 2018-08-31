using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserDataGetIO
	{
		/// <summary>
		/// 顧客情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <param name="tokuisakiID">得意先No</param>
		/// <returns>DataTable</returns>
		public static DataTable GetEntryFinishedUserDataList(bool sqlsv2, string tokuisakiID = "")
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT vMic全ユーザー３.顧客No, vMic全ユーザー３.得意先No, 顧客名１ + 顧客名２ AS 顧客名"
									+ ", vMic全ユーザー３.レセコン名称, vMic全ユーザー３.拠点コード, vMic全ユーザー３.拠点名"
									+ ", vMic全ユーザー３.都道府県名, tMic終了ユーザーリスト.終了事由, tMic終了ユーザーリスト.リプレース"
									+ ", tMic終了ユーザーリスト.理由, '' AS コメント, vMic全ユーザー３.有効ユーザーフラグ"
									+ ", '' AS 除外, vMic全ユーザー３.販売店ID, vMic全ユーザー３.販売店名称, tMic終了ユーザーリスト.終了月, tMic終了ユーザーリスト.終了届受領日"
									+ " FROM tMic終了ユーザーリスト INNER JOIN vMic全ユーザー３ ON tMic終了ユーザーリスト.得意先No = vMic全ユーザー３.得意先No";
					if (0 < tokuisakiID.Length)
					{
						strSQL += string.Format(" WHERE vMic全ユーザー３.得意先No = '{0}'", tokuisakiID);
					}
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
		/// リプレース先メーカーリストの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>リプレース先メーカーリスト</returns>
		public static DataTable GetReplaceMakerList(bool sqlsv2)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT [tMikコードマスタ].fcm名称"
									+ " FROM [tMikコードマスタ]"
									+ " WHERE [tMikコードマスタ].fcm名称 Not Like '%不可%' AND [tMikコードマスタ].fcmコード <> '001' AND [tMikコードマスタ].fcmコード種別 = '30'"
									+ " ORDER BY [tMikコードマスタ].fcmコード ASC";
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
	}
}
