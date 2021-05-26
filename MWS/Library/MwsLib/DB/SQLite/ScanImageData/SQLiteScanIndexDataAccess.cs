//
// SQLiteScanImageDataAccess.cs
// 
// 文書インデックスファイル管理 SQLiteデータベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/05/16 勝呂)
//
using MwsLib.BaseFactory.ScanImageData;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MwsLib.DB.SQLite.ScanImageData
{
	public static class SQLiteScanImageDataAccess
	{
		/// <summary>
		/// インデックスファイル情報の取得
		/// </summary>
		/// <returns>インデックスファイル情報</returns>
		public static List<IndexFileInfo> GetIndexFileInfoList()
		{
			DataTable table = SQLiteScanImageDataGetIO.GetIndexFileInfo(Directory.GetCurrentDirectory());
			return SQLiteScanImageDataController.ConvertIndexFileInfo(table);
		}

		/// <summary>
		/// インデックスファイル情報の設定
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="list">インデックスファイル情報</param>
		/// <returns>判定</returns>
		public static int SetIndexFileInfo(string dbPath, List<IndexFileInfo> list)
		{
			SQLiteScanImageDataSetIO.DeleteIndexFileInfo(dbPath);
			return SQLiteScanImageDataSetIO.InsertIntoIndexFileInfo(dbPath, list);
		}

		/// <summary>
		/// インデックスファイル情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>判定</returns>
		public static int DeleteIndexFileInfo(string dbPath)
		{
			return SQLiteScanImageDataSetIO.DeleteIndexFileInfo(dbPath);
		}
	}
}
