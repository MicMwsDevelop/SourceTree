//
// VariousDocumentOutAccess.cs
// 
// 各種書類出力ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using MwsLib.BaseFactory.VariousDocumentOut;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.Settings.SqlServer;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.VariousDocumentOut
{
	public static class VariousDocumentOutAccess
	{
		/// <summary>
		/// 営業部所属住所情報の取得
		/// </summary>
		/// <param name="userName">ログイン名</param>
		/// <param name="settings">SQL Server接続情報</param>
		/// <returns>結果</returns>
		public static List<SatelliteOffice> Select_SatelliteOfficeInfo(string userName, string connectStr)
		{
			string sqlStr = string.Format("SELECT 部署.fBshName2 as 部署名, 支店.f支店名 as 拠点名, 支店.f住所１ as 住所1, 支店.f住所２ as 住所2, 支店.f電話番号 as 電話番号"
											+ ", 支店.fファックス番号 as FAX番号, 支店.f郵便番号 as 郵便番号, 担当者.fUsrLoginID as ログイン名"
											+ " FROM ({0} as 担当者"
											+ " INNER JOIN {1} as 支店 ON 担当者.fBshCode3 = 支店.fBshCode3 AND 担当者.fBshCode2 = 支店.fBshCode2 AND 担当者.fBshCode1 = 支店.fBshCode1)"
											+ " INNER JOIN {2} as 部署 ON 支店.fBshCode3 = 部署.fBshCode3 AND 支店.fBshCode2 = 部署.fBshCode2 AND 支店.fBshCode1 = 部署.fBshCode1"
											+ " WHERE 担当者.fUsrLoginID = '{3}'"
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic担当者]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tBusho]
											, userName);
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(sqlStr, connectStr);
			return SatelliteOffice.DataTableToList(table);
		}

		/// <summary>
		/// 本社所属部署住所情報の取得
		/// </summary>
		/// <param name="userName">ログイン名</param>
		/// <param name="settings">SQL Server接続情報</param>
		/// <returns>結果</returns>
		public static List<SatelliteOffice> Select_HeadOfficeInfo(string userName, SqlServerConnectSettings settings)
		{
			string sqlStr = string.Format("SELECT '本社' as 部署名, 支店.f支店名 as 拠点名, 支店.f住所１ as 住所1, 支店.f住所２ as 住所2, 支店.f電話番号 as 電話番号, 支店.fファックス番号 as FAX番号"
											+ ", 支店.f郵便番号 as 郵便番号, 担当者.fUsrLoginID as ログイン名"
											+ " FROM {0} as 担当者"
											+ " INNER JOIN {1} as 支店 ON 担当者.fBshCode2 = 支店.fBshCode2 AND 担当者.fBshCode1 = 支店.fBshCode1"
											+ " WHERE 支店.fBshCode3 = '99' AND 担当者.fUsrLoginID = '{2}'"
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic担当者]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
											, userName);
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(sqlStr, settings);
			return SatelliteOffice.DataTableToList(table);
		}

		/// <summary>
		/// 得意先番号に対する顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static List<CustomerInfo> Select_CustomerInfoByTokuisakiNo(string tokuisakiNo, bool ct)
		{
			string sqlStr = string.Format("SELECT 基本.fkj顧客区分 AS 顧客区分, CL.fCliID AS 顧客No, 基本.fkj得意先情報 AS 得意先No, 基本.fkj郵便番号 AS 郵便番号"
											+ ", 基本.fkjファックス番号 AS FAX番号, 基本.fkj電話番号 AS 電話番号, CL.fCliName AS 顧客名1, 基本.fkj顧客名2 AS 顧客名2"
											+ ", 基本.fkj住所1 AS 住所1, 基本.fkj住所2 AS 住所2, ユーザ.fus院長名 AS 院長名, ユーザ.fus運用サポート情報 as 運用サポート情報"
											+ " FROM ({0} as CL"
											+ " INNER JOIN {1} as 基本 ON CL.fCliID = 基本.fkjCliMicID)"
											+ " LEFT JOIN {2} as ユーザ ON 基本.fkjCliMicID = ユーザ.fusCliMicID"
											+ " WHERE 基本.fkj得意先情報 = '{3}'"
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ]
											, tokuisakiNo);
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(sqlStr, ct);
			return CustomerInfo.DataTableToList(table);
		}

		/// <summary>
		/// 得意先番号に対する顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <param name="settings">SQL Server接続情報</param>
		/// <returns>結果</returns>
		public static List<CustomerInfo> Select_CustomerInfoByTokuisakiNo(string tokuisakiNo, SqlServerConnectSettings settings)
		{
			string sqlStr = string.Format("SELECT 基本.fkj顧客区分 AS 顧客区分, CL.fCliID AS 顧客No, 基本.fkj得意先情報 AS 得意先No, 基本.fkj郵便番号 AS 郵便番号"
											+ ", 基本.fkjファックス番号 AS FAX番号, 基本.fkj電話番号 AS 電話番号, CL.fCliName AS 顧客名1, 基本.fkj顧客名2 AS 顧客名2"
											+ ", 基本.fkj住所1 AS 住所1, 基本.fkj住所2 AS 住所2, ユーザ.fus院長名 AS 院長名, ユーザ.fus運用サポート情報 as 運用サポート情報"
											+ " FROM ({0} as CL"
											+ " INNER JOIN {1} as 基本 ON CL.fCliID = 基本.fkjCliMicID)"
											+ " LEFT JOIN {2} as ユーザ ON 基本.fkjCliMicID = ユーザ.fusCliMicID"
											+ " WHERE 基本.fkj得意先情報 = '{3}'"
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ]
											, tokuisakiNo);
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(sqlStr, settings);
			return CustomerInfo.DataTableToList(table);
		}

		/// <summary>
		/// 顧客Noに対する顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static List<CustomerInfo> Select_CustomerInfoByCustomerNo(int CustomerNo, bool ct)
		{
			string sqlStr = string.Format("SELECT 基本.fkj顧客区分 AS 顧客区分, CL.fCliID AS 顧客No, 基本.fkj得意先情報 AS 得意先No, 基本.fkj郵便番号 AS 郵便番号"
											+ ", 基本.fkjファックス番号 AS FAX番号, 基本.fkj電話番号 AS 電話番号, CL.fCliName AS 顧客名1, 基本.fkj顧客名2 AS 顧客名2"
											+ ", 基本.fkj住所1 AS 住所1, 基本.fkj住所2 AS 住所2, ユーザ.fus院長名 AS 院長名, ユーザ.fus運用サポート情報 as 運用サポート情報"
											+ " FROM ({0} as CL"
											+ " INNER JOIN {1} as 基本 ON CL.fCliID = 基本.fkjCliMicID)"
											+ " LEFT JOIN {2} as ユーザ ON 基本.fkjCliMicID = ユーザ.fusCliMicID"
											+ " WHERE CL.fCliID = {3}"
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ]
											, CustomerNo);
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(sqlStr, ct);
			return CustomerInfo.DataTableToList(table);
		}

		/// <summary>
		/// 顧客Noに対する顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <param name="settings">SQL Server接続情報</param>
		/// <returns>結果</returns>
		public static List<CustomerInfo> Select_CustomerInfoByCustomerNo(int CustomerNo, SqlServerConnectSettings settings)
		{
			string sqlStr = string.Format("SELECT 基本.fkj顧客区分 AS 顧客区分, CL.fCliID AS 顧客No, 基本.fkj得意先情報 AS 得意先No, 基本.fkj郵便番号 AS 郵便番号"
											+ ", 基本.fkjファックス番号 AS FAX番号, 基本.fkj電話番号 AS 電話番号, CL.fCliName AS 顧客名1, 基本.fkj顧客名2 AS 顧客名2"
											+ ", 基本.fkj住所1 AS 住所1, 基本.fkj住所2 AS 住所2, ユーザ.fus院長名 AS 院長名, ユーザ.fus運用サポート情報 as 運用サポート情報"
											+ " FROM ({0} as CL"
											+ " INNER JOIN {1} as 基本 ON CL.fCliID = 基本.fkjCliMicID)"
											+ " LEFT JOIN {2} as ユーザ ON 基本.fkjCliMicID = ユーザ.fusCliMicID"
											+ " WHERE CL.fCliID = {3}"
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ]
											, CustomerNo);
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(sqlStr, settings);
			return CustomerInfo.DataTableToList(table);
		}
	}
}
