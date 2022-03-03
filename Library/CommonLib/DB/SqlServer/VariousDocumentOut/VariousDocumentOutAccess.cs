//
// VariousDocumentOutAccess.cs
// 
// 各種書類出力ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using CommonLib.BaseFactory.VariousDocumentOut;
using CommonLib.DB.SqlServer.Junp;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.VariousDocumentOut
{
	public static class VariousDocumentOutAccess
	{
		/// <summary>
		/// 営業部所属住所情報の取得
		/// </summary>
		/// <param name="userName">ログイン名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<SatelliteOffice> Select_SatelliteOfficeInfo(string userName, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
										+ " 部署.fBshCode2 as 営業部コード"
										+ ", 部署.fBshName2 as 部署名"
										+ ", 部署.fBshCode3 as 拠点コード"
										+ ", 部署.fBshName3 as 拠点名"
										+ ", 支店.f住所１ as 住所1"
										+ ", 支店.f住所２ as 住所2"
										+ ", 支店.f電話番号 as 電話番号"
										+ ", 支店.fファックス番号 as FAX番号"
										+ ", 支店.f郵便番号 as 郵便番号"
										+ ", 担当者.fUsrLoginID as ログイン名"
										+ " FROM ({0} as 担当者"
										+ " INNER JOIN {1} as 支店 ON 担当者.fBshCode3 = 支店.fBshCode3 AND 担当者.fBshCode2 = 支店.fBshCode2 AND 担当者.fBshCode1 = 支店.fBshCode1)"
										+ " INNER JOIN {2} as 部署 ON 支店.fBshCode3 = 部署.fBshCode3 AND 支店.fBshCode2 = 部署.fBshCode2 AND 支店.fBshCode1 = 部署.fBshCode1"
										+ " WHERE 担当者.fUsrLoginID = '{3}'"
										+ " ORDER BY 営業部コード, 拠点コード"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic担当者]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tBusho]
										, userName);

			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return SatelliteOffice.DataTableToList(table);
		}

		/// <summary>
		/// 本社所属部署住所情報の取得
		/// </summary>
		/// <param name="userName">ログイン名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<SatelliteOffice> Select_HeadOfficeInfo(string userName, string connectStr)
		{
			string sqlStr = string.Format("SELECT '本社' as 部署名, 支店.f支店名 as 拠点名, 支店.f住所１ as 住所1, 支店.f住所２ as 住所2, 支店.f電話番号 as 電話番号, 支店.fファックス番号 as FAX番号"
											+ ", 支店.f郵便番号 as 郵便番号, 担当者.fUsrLoginID as ログイン名"
											+ " FROM {0} as 担当者"
											+ " INNER JOIN {1} as 支店 ON 担当者.fBshCode2 = 支店.fBshCode2 AND 担当者.fBshCode1 = 支店.fBshCode1"
											+ " WHERE 支店.fBshCode3 = '99' AND 担当者.fUsrLoginID = '{2}'"
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic担当者]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
											, userName);

			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return SatelliteOffice.DataTableToList(table);
		}

		/// <summary>
		/// 得意先番号に対する顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<CustomerInfo> Select_CustomerInfoByTokuisakiNo(string tokuisakiNo, string connectStr)
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

			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return CustomerInfo.DataTableToList(table);
		}

		/// <summary>
		/// 顧客Noに対する顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<CustomerInfo> Select_CustomerInfoByCustomerNo(int CustomerNo, string connectStr)
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

			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return CustomerInfo.DataTableToList(table);
		}

		/// <summary>
		/// 顧客Noに対するオンライン資格確認対象商品売上明細情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<オンライン資格確認対象商品売上明細> Select_オンライン資格確認対象商品売上明細(string tokuisakiNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
										+ " 明細.sykd_tcd as 得意先コード"
										+ ",明細.sykd_uribi as 売上日"
										+ ",明細.sykd_denno as 伝票No"
										+ ",sykd_ocd"
										+ ",sykd_jbmn as 部門コード"
										+ ",sykd_tekmei as 摘要"
										+ ",sykd_eda as 枝番"
										+ ",明細.sykd_scd as 商品コード"
										+ ",sykd_mei as 商品名"
										+ ",convert(int, sykd_hyo) as 標準価格"
										+ ",convert(int, sykd_suryo) as 数量"
										+ ",convert(int, sykd_kingaku) as 金額"
										+ ",convert(int, sykd_zei) as 税額"
										+ ",convert(int, sykd_kingaku + sykd_zei) as 補助対象金額"
										+ " FROM {0} as 明細"
										+ " INNER JOIN"
										+ " ("
										+ " SELECT"
										+ " sykd_denno"
										+ ",sykd_uribi"
										+ ",sykd_scd"
										+ " FROM {0}"
										+ " WHERE sykd_scd = '018021'"
										+ ") as ソフト改修費 on ソフト改修費.sykd_uribi = 明細.sykd_uribi AND ソフト改修費.sykd_denno = 明細.sykd_denno"
										+ " WHERE 明細.sykd_tcd = '{1}'"
										+ " ORDER BY 明細.sykd_uribi, 明細.sykd_tcd, 明細.[sykd_eda]"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, tokuisakiNo);
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return オンライン資格確認対象商品売上明細.DataTableToList(table);
		}
	}
}
