//
// 社員マスタ参照ビュー.cs
//
// [CharlieDB].[dbo].[社員マスタ参照ビュー]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.View
{
	public class 社員マスタ参照ビュー
	{
		public string 社員番号 { get; set; }
		public string ログインID { get; set; }
		public string 社員名 { get; set; }
		public string 権限フラグ { get; set; }
		public string 部署コード1 { get; set; }
		public string 部署名1 { get; set; }
		public string 部署コード2 { get; set; }
		public string 部署名2 { get; set; }
		public string 部署コード3 { get; set; }
		public string 部署名3 { get; set; }
		public int 営業区分 { get; set; }
		public string 社員名フリガナ { get; set; }
		public string メールアドレス { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 社員マスタ参照ビュー()
		{
			社員番号 = string.Empty;
			ログインID = string.Empty;
			社員名 = string.Empty;
			権限フラグ = string.Empty;
			部署コード1 = string.Empty;
			部署名1 = string.Empty;
			部署コード2 = string.Empty;
			部署名2 = string.Empty;
			部署コード3 = string.Empty;
			部署名3 = string.Empty;
			営業区分 = 0;
			社員名フリガナ = string.Empty;
			メールアドレス = string.Empty;
		}

		/// <summary>
		/// [charlieDB].[dbo].[社員マスタ参照ビュー]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>社員マスタ参照ビュー</returns>
		public static List<社員マスタ参照ビュー> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<社員マスタ参照ビュー> result = new List<社員マスタ参照ビュー>();
				foreach (DataRow row in table.Rows)
				{
					社員マスタ参照ビュー data = new 社員マスタ参照ビュー
					{
						社員番号 = row["社員番号"].ToString().Trim(),
						ログインID = row["ログインID"].ToString().Trim(),
						社員名 = row["社員名"].ToString().Trim(),
						権限フラグ = row["権限フラグ"].ToString().Trim(),
						部署コード1 = row["部署コード1"].ToString().Trim(),
						部署名1 = row["部署名1"].ToString().Trim(),
						部署コード2 = row["部署コード2"].ToString().Trim(),
						部署名2 = row["部署名2"].ToString().Trim(),
						部署コード3 = row["部署コード3"].ToString().Trim(),
						部署名3 = row["部署名3"].ToString().Trim(),
						営業区分 = DataBaseValue.ConvObjectToInt(row["営業区分"]),
						社員名フリガナ = row["社員名フリガナ"].ToString().Trim(),
						メールアドレス = row["メールアドレス"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
