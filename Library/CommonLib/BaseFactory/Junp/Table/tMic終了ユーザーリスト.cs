//
// tMic終了ユーザーリスト.cs
//
// 終了ユーザーリストクラス
// [JunpDB].[dbo].[tMic終了ユーザーリスト]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using CommonLib.Common;
using CommonLib.DB;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Junp.Table
{
	/// <summary>
	/// [JunpDB].[dbo].[tMic終了ユーザーリスト]
	/// </summary>
	public class tMic終了ユーザーリスト
	{
		/// <summary>
		/// 得意先No
		/// </summary>
		public string 得意先No { get; set; }

		/// <summary>
		/// 終了月
		/// </summary>
		public YearMonth? 終了月 { get; set; }

		/// <summary>
		/// 終了届受領日
		/// </summary>
		public Date? 終了届受領日 { get; set; }

		/// <summary>
		/// 終了事由
		/// </summary>
		public string 終了事由 { get; set; }

		/// <summary>
		/// リプレース
		/// </summary>
		public string リプレース { get; set; }

		/// <summary>
		/// 理由
		/// </summary>
		public string 理由 { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime? 更新日時 { get; set; }

		/// <summary>
		/// 非paletteユーザー
		/// </summary>
		public bool 非paletteユーザー { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMic終了ユーザーリスト()
		{
			得意先No = null;
			終了月 = null;
			終了届受領日 = null;
			リプレース = null;
			終了事由 = null;
			理由 = null;
			更新日時 = null;
			非paletteユーザー = false;
		}

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8)", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic終了ユーザーリスト]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET 終了月 = @1, 終了届受領日 = @2, 終了事由 = @3, リプレース = @4, 理由 = @5, 更新日時 = @6, 非paletteユーザー = @7"
									+ " WHERE 得意先No = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic終了ユーザーリスト], 得意先No);
			}
		}

		/// <summary>
		/// DELETE SQL文字列の取得
		/// </summary>
		public string DeleteSqlString
		{
			get
			{
				return string.Format(@"DELETE FROM {0} WHERE 得意先No = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic終了ユーザーリスト], 得意先No);
			}
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", 得意先No ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@2", 終了月.HasValue ? 終了月.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", 終了届受領日.HasValue ? 終了届受領日.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", 終了事由 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", リプレース ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@6", 理由 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", DateTime.Now),
				new SqlParameter("@8", (非paletteユーザー) ? "1" : "0")
			};
			return param;
		}

		/// <summary>
		/// UPDATE SETパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetUpdateSetParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", 終了月.HasValue ? 終了月.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@2", 終了届受領日.HasValue ? 終了届受領日.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", 終了事由 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", リプレース ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", 理由 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@6", DateTime.Now),
				new SqlParameter("@7", (非paletteユーザー) ? "1" : "0")
			};
			return param;
		}

		/// <summary>
		/// [JnmpDB].[dbo].[tMic終了ユーザーリスト]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>支店情報</returns>
		public static List<tMic終了ユーザーリスト> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMic終了ユーザーリスト> result = new List<tMic終了ユーザーリスト>();
				foreach (DataRow row in table.Rows)
				{
					tMic終了ユーザーリスト data = new tMic終了ユーザーリスト();
					data.得意先No = row["得意先No"].ToString().Trim();
					string buf = row["終了月"].ToString().Trim();
					data.終了月 = YearMonth.Parse(buf);
					buf = row["終了届受領日"].ToString().Trim();
					data.終了届受領日 = Date.Parse(buf);
					data.終了事由 = row["終了事由"].ToString().Trim();
					data.リプレース = row["リプレース"].ToString().Trim();
					data.理由 = row["理由"].ToString().Trim();
					data.更新日時 = DataBaseValue.ConvObjectToDateTimeNull(row["更新日時"]);
					data.非paletteユーザー = ("0" == row["非paletteユーザー"].ToString().Trim()) ? false : true;
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
