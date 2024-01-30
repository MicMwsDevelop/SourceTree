//
// ApplicationInfo.cs
// 
// [JunpDB].[dbo].[tMikアプリケーション情報]設定クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/22 勝呂):新規作成
//
using CommonLib.Common;
using System.Collections.Generic;

namespace SetApplicationInfo.BaseFactory
{
	public class ApplicationInfo
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		/// アプリケーション名
		/// </summary>
		public string AplNo { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
		public string AplName { get; set; }

		/// <summary>
		/// 保守契約開始年月
		/// </summary>
		public YearMonth? StartYM { get; set; }

		/// <summary>
		/// 保守契約終了年月
		/// </summary>
		public YearMonth? EndYM { get; set; }

		/// <summary>
		/// 保守契約備考
		/// </summary>
		public string Marks { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ApplicationInfo()
		{
			CustomerNo = 0;
			CustomerName = string.Empty;
			AplNo = string.Empty;
			AplName = string.Empty;
			StartYM = null;
			EndYM = null;
			Marks = string.Empty;
		}

		/// <summary>
		/// データの格納
		/// </summary>
		/// <param name="line">ストリーム文字列</param>
		/// <returns>判定</returns>
		public bool SetData(string line)
		{
			List<string> split = SplitString.CSVSplitLine2(line);
			if (7 == split.Count)
			{
				CustomerNo = split[0].ToInt();
				CustomerName = split[1].Trim();
				AplNo = split[2].Trim();
				AplName = split[3].Trim();
				YearMonth ym;
				if (false == YearMonth.TryParse(split[4], out ym))
				{
					return false;
				}
				StartYM = ym;
				if (false == YearMonth.TryParse(split[5], out ym))
				{
					return false;
				}
				EndYM = ym;
				Marks = split[6].Trim();
				return true;
			}
			return false;
		}
	}
}
