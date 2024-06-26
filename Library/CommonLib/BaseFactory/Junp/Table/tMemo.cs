﻿//
// tMemo.cs
//
// メモ情報クラス
// [JunpDB].[dbo].[tMemo]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Junp.Table
{
	/// <summary>
	/// [JunpDB].[dbo].[tMemo]
	/// </summary>
	public class tMemo
	{
		/// <summary>
		/// fMemID
		/// </summary>
		public int fMemID{get;set;}

		/// <summary>
		/// fMemKey
		/// </summary>
		public int fMemKey { get; set; }

		/// <summary>
		/// fMemTable
		/// </summary>
		public string fMemTable { get; set; }

		/// <summary>
		/// fMemType
		/// </summary>
		public string fMemType { get; set; }

		/// <summary>
		/// fMemMemo
		/// </summary>
		public string fMemMemo { get; set; }

		/// <summary>
		/// fMemUpdate
		/// </summary>
		public DateTime? fMemUpdate { get; set; }

		/// <summary>
		/// fMemUpdateMan
		/// </summary>
		public string fMemUpdateMan { get; set; }

		/// <summary>
		/// fMemUrl
		/// </summary>
		public string fMemUrl { get; set; }

		/// <summary>
		/// fMemOriginalPath1
		/// </summary>
		public string fMemOriginalPath1 { get; set; }

		/// <summary>
		/// fMemOriginalPath2
		/// </summary>
		public string fMemOriginalPath2 { get; set; }

		/// <summary>
		/// fMemOriginalPath3
		/// </summary>
		public string fMemOriginalPath3 { get; set; }

		/// <summary>
		/// fMemWlfID1
		/// </summary>
		public int fMemWlfID1 { get; set; }

		/// <summary>
		/// fMemWlfID2
		/// </summary>
		public int fMemWlfID2 { get; set; }

		/// <summary>
		/// fMemWlfID3
		/// </summary>
		public int fMemWlfID3 { get; set; }

		/// <summary>
		/// fMemCatID1
		/// </summary>
		public int fMemCatID1 { get; set; }

		/// <summary>
		/// fMemCatID2
		/// </summary>
		public int fMemCatID2 { get; set; }

		/// <summary>
		/// fMemCatID3
		/// </summary>
		public int fMemCatID3 { get; set; }

		/// <summary>
		/// fMemKubun
		/// </summary>
		public string fMemKubun { get; set; }

		/// <summary>
		/// fMemComment
		/// </summary>
		public string fMemComment { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMemo()
		{
			fMemID = 0;
			fMemKey = 0;
			fMemTable = null;
			fMemType = null;
			fMemMemo = null;
			fMemUpdate = null;
			fMemUpdateMan = null;
			fMemUrl = null;
			fMemOriginalPath1 = null;
			fMemOriginalPath2 = null;
			fMemOriginalPath3 = null;
			fMemWlfID1 = 0;
			fMemWlfID2 = 0;
			fMemWlfID3 = 0;
			fMemCatID1 = 0;
			fMemCatID2 = 0;
			fMemCatID3 = 0;
			fMemKubun = null;
			fMemComment = null;
		}

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18)", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMemo]);
			}
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", fMemKey),
				new SqlParameter("@2", fMemTable ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", fMemType ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", fMemMemo ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", fMemUpdate.HasValue ? fMemUpdate.Value : System.Data.SqlTypes.SqlDateTime.Null),
				new SqlParameter("@6", fMemUpdateMan ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", fMemUrl ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", fMemOriginalPath1 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", fMemOriginalPath2 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", fMemOriginalPath3 ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", fMemWlfID1),
				new SqlParameter("@12", fMemWlfID2),
				new SqlParameter("@13", fMemWlfID3),
				new SqlParameter("@14", fMemCatID1),
				new SqlParameter("@15", fMemCatID2),
				new SqlParameter("@16", fMemCatID3),
				new SqlParameter("@17", fMemKubun ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@18", fMemComment ?? System.Data.SqlTypes.SqlString.Null)
			};
			return param;
		}
	}
}
