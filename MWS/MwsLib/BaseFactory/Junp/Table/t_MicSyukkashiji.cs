//
// t_MicSyukkashiji.cs
//
// 出荷指示クラス
// [JunpDB].[dbo].[t_MicSyukkashiji]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using MwsLib.DB.SqlServer.Junp;
using System.Data.SqlClient;

namespace MwsLib.BaseFactory.Junp.Table
{
	/// <summary>
	/// 出荷指示
	/// </summary>
	public class t_MicSyukkashiji
	{
		int ID { get; set; }
		int? PCA受注No { get; set; }
		int? 出荷日 { get; set; }
		int? 受注先顧客No { get; set; }
		string 運送会社 { get; set; }
		int 出荷メール { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public t_MicSyukkashiji()
		{
			ID = 0;
			PCA受注No = null;
			出荷日 = null;
			受注先顧客No = null;
			運送会社 = string.Empty;
			出荷メール = 0;
		}

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5)", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.t_MicSyukkashiji]);
			}
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", PCA受注No.HasValue ? PCA受注No.Value : System.Data.SqlTypes.SqlInt32.Null),
				new SqlParameter("@2", 出荷日.HasValue ? 出荷日.Value : System.Data.SqlTypes.SqlInt32.Null),
				new SqlParameter("@3", 受注先顧客No.HasValue ? 受注先顧客No.Value : System.Data.SqlTypes.SqlInt32.Null),
				new SqlParameter("@4", 運送会社 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", 出荷メール)
			};
			return param;
		}
	}
}
