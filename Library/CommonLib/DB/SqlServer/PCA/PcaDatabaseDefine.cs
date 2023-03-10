//
// PCAデータベース定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/03/08 勝呂):新規作成
// Ver1.01(2023/03/08 勝呂):PCAX→PCADXへの入替により、データベース名が変更
//
using CommonLib.Common;

namespace CommonLib.DB.SqlServer.PCA
{
	public static class PcaDatabaseDefine
	{
		/// <summary>
		/// データベース名
		/// </summary>
		/// Ver1.01(2023/03/08 勝呂):PCAX→PCADXへの入替により、データベース名が変更
		static private string DatabaseName = "P10V01C001KON0001.dbo";
		//static private string DatabaseName = "P20V01C001KON0001.dbo";

		/// <summary>
		/// テーブル種別 
		/// </summary>
		public enum TableType
		{
			JUCD = 1,
		}

		/// <summary>
		/// テーブル種別/テーブル文字列
		/// </summary>
		public static readonly EnumDictionary<TableType, string> TableName = new EnumDictionary<TableType, string>()
		{
			{ TableType.JUCD, string.Format("{0}.JUCD", DatabaseName) },
		};
	}
}
