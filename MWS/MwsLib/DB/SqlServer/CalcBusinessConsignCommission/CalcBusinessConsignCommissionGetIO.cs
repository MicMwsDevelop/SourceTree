//
// CalcBusinessConsignCommissionGetIO.cs
// 
// PCA仕入データ業務委託手数料再計算ツール SQL SERVERデータベース読込みI/Oクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/05 勝呂)
//
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.CalcBusinessConsignCommission
{
	/// <summary>
	/// PCA仕入データ業務委託手数料再計算ツール SQL SERVERデータベース読込みI/Oクラス
	/// </summary>
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
