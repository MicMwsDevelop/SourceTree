using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.CalcBusinessConsignCommission
{
	public static class CalcBusinessConsignCommissionGetIO
	{
		/// <summary>
		/// 販売店情報の取得
		/// </summary>
		/// <returns>レコード数</returns>
		public static DataTable GetSalesOutletInfo()
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(false)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT A.[仕入先コード], B.[手数料率]  FROM dbo.販売店情報参照ビュー AS A LEFT JOIN dbo.販売店区分参照ビュー AS B ON A.販売店ランクコード = B.区分コード  ORDER BY [仕入先コード] ASC";
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
