//
// 支店情報参照ビュー.cs
//
// [charlieDB].[dbo].[支店情報参照ビュー]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.View
{
	/// <summary>
	/// [charlieDB].[dbo].[支店情報参照ビュー]
	/// </summary>
	public class 支店情報参照ビュー
	{
		/// <summary>
		/// 支店ＩＤ
		/// </summary>
		public string 支店ＩＤ { get; set; }

		/// <summary>
		/// 支店名
		/// </summary>
		public string 支店名 { get; set; }

		/// <summary>
		/// PCA担当者コード
		/// </summary>
		public string PCA担当者コード { get; set; }

		/// <summary>
		/// PCA部門コード
		/// </summary>
		public short PCA部門コード { get; set; }

		/// <summary>
		/// PCA倉庫コード
		/// </summary>
		public short PCA倉庫コード { get; set; }

		/// <summary>
		/// 支店メールアドレス
		/// </summary>
		public string 支店メールアドレス { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 支店情報参照ビュー()
		{
			支店ＩＤ = string.Empty;
			支店名 = string.Empty;
			PCA担当者コード = string.Empty;
			PCA部門コード = 0;
			PCA倉庫コード = 0;
			支店メールアドレス = string.Empty;
		}

		/// <summary>
		/// [charlieDB].[dbo].[支店情報参照ビュー]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>支店情報</returns>
		public static List<支店情報参照ビュー> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<支店情報参照ビュー> result = new List<支店情報参照ビュー>();
				foreach (DataRow row in table.Rows)
				{
					支店情報参照ビュー data = new 支店情報参照ビュー
					{
						支店ＩＤ = row["支店ＩＤ"].ToString().Trim(),
						支店名 = row["支店名"].ToString().Trim(),
						PCA担当者コード = row["PCA担当者コード"].ToString().Trim(),
						PCA部門コード = (short)DataBaseValue.ConvObjectToInt(row["PCA部門コード"]),
						PCA倉庫コード = (short)DataBaseValue.ConvObjectToInt(row["PCA倉庫コード"]),
						支店メールアドレス = row["支店メールアドレス"].ToString().Trim()
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}