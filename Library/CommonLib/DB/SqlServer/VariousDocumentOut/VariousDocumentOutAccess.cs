//
// VariousDocumentOutAccess.cs
// 
// 各種書類出力ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
// Ver1.18(2023/04/03 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 送付先リストから受注日を取得
//
using CommonLib.BaseFactory.VariousDocumentOut;
using CommonLib.DB.SqlServer.Charlie;
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
		public static CustomerInfo Select_CustomerInfoByTokuisakiNo(string tokuisakiNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT TOP 1 B.fkj顧客区分 AS 顧客区分, CL.fCliID AS 顧客No, B.fkj得意先情報 AS 得意先No, B.fkj郵便番号 AS 郵便番号"
											+ ", B.fkjファックス番号 AS FAX番号, B.fkj電話番号 AS 電話番号, CL.fCliName AS 顧客名1, B.fkj顧客名2 AS 顧客名2"
											+ ", B.fkj住所1 AS 住所1, B.fkj住所2 AS 住所2, U.fus院長名 AS 院長名, U.fus運用サポート情報 as 運用サポート情報"
											+ ", U.fus医保医療コード as 医保医療コード"
											+ ", CL.fCliYomi as フリガナ"
											+ ", B.fkj住所フリガナ as 住所フリガナ"
											+ ", U.fus院長名フリガナ as 院長名フリガナ"
											+ ", U.fus休診日 as 休診日"
											+ ", U.fus診療時間 as 診療時間"
											+ ", U.fusﾒｰﾙｱﾄﾞﾚｽ as メールアドレス"
											+ ", U.fus備考2 as 備考"
											+ ", U.fus単体 as 単体"
											+ ", U.fusサーバー as サーバー"
											+ ", U.fusクライアント as クライアント"
											+ ", U.fus納品月 as 納品月"
											+ ", P.fBshName3 as 支店名"
											+ ", P.fUsrName as 担当者名"
											+ ", S.営業担当者名 as 営業担当者名"
											+ ", AG.fdaAPLUSコード as 代行回収APLUSコード"
											+ ", AG.fda状態 as 代行回収状態"
											+ ", U.fus開設者 as 開設者名"
											+ ", PC.PRODUCT_ID as MWS_ID"
											+ ", PC.PASSWORD as MWS_パスワード"
											+ ", PC.PASSWORD_READING as MWS_パスワード読み"
											+ ", KN.県番号 as 県番号"
											+ ", KN.都道府県名 as 都道府県名"
											+ ", RE.顧客No as 販売店ID"
											+ ", RE.顧客名1 + RE.顧客名2 as 販売店名称"
											+ ", TK.fcm名称 as システム名称"
											+ ", TK2.fcm名称 as システム略称"
											+ " FROM {0} as CL"
											+ " INNER JOIN {1} as B ON CL.fCliID = B.fkjCliMicID"
											+ " LEFT JOIN {2} as U ON B.fkjCliMicID = U.fusCliMicID"
											+ " LEFT JOIN {3} as P ON CL.fCliFirstcMan = P.fUsrID"
											+ " LEFT JOIN {4} as S ON CL.fCliID=S.顧客No"
											+ " LEFT JOIN {5} as AG ON CL.fCliID = AG.fdaCliMicID"
											+ " LEFT JOIN {6} as PC ON CL.fCliID = PC.CUSTOMER_ID"
											+ " LEFT JOIN {7} as KN ON LEFT(B.fkj住所１, 3) = left(KN.都道府県名, 3)"
											+ " LEFT JOIN {8} as RE ON RE.顧客No = U.fus販売店No"
											+ " LEFT JOIN {9} as TK ON TK.fcmコード種別 = '01' and U.fusシステム名 = TK.fcmコード"
											+ " LEFT JOIN {9} as TK2 ON U.fusシステム名 = TK2.fcmコード AND TK2.fcmコード種別 = '91'"
											+ " WHERE B.fkj得意先情報 = '{10}'"
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ]
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMih担当者]
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic営業担当]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik代行回収]
											, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik県番号]
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全販売店]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikコードマスタ]
											, tokuisakiNo);
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return CustomerInfo.DataTableToData(table);
		}

		/// <summary>
		/// 顧客Noに対する顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static CustomerInfo Select_CustomerInfoByCustomerNo(int CustomerNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT TOP 1 B.fkj顧客区分 AS 顧客区分, CL.fCliID AS 顧客No, B.fkj得意先情報 AS 得意先No, B.fkj郵便番号 AS 郵便番号"
											+ ", B.fkjファックス番号 AS FAX番号, B.fkj電話番号 AS 電話番号, CL.fCliName AS 顧客名1, B.fkj顧客名2 AS 顧客名2"
											+ ", B.fkj住所1 AS 住所1, B.fkj住所2 AS 住所2, U.fus院長名 AS 院長名, U.fus運用サポート情報 as 運用サポート情報"
											+ ", U.fus医保医療コード as 医保医療コード"
											+ ", CL.fCliYomi as フリガナ"
											+ ", B.fkj住所フリガナ as 住所フリガナ"
											+ ", U.fus院長名フリガナ as 院長名フリガナ"
											+ ", U.fus休診日 as 休診日"
											+ ", U.fus診療時間 as 診療時間"
											+ ", U.fusﾒｰﾙｱﾄﾞﾚｽ as メールアドレス"
											+ ", U.fus備考2 as 備考"
											+ ", U.fus単体 as 単体"
											+ ", U.fusサーバー as サーバー"
											+ ", U.fusクライアント as クライアント"
											+ ", U.fus納品月 as 納品月"
											+ ", P.fBshName3 as 支店名"
											+ ", P.fUsrName as 担当者名"
											+ ", S.営業担当者名 as 営業担当者名"
											+ ", AG.fdaAPLUSコード as 代行回収APLUSコード"
											+ ", AG.fda状態 as 代行回収状態"
											+ ", U.fus開設者 as 開設者名"
											+ ", PC.PRODUCT_ID as MWS_ID"
											+ ", PC.PASSWORD as MWS_パスワード"
											+ ", PC.PASSWORD_READING as MWS_パスワード読み"
											+ ", KN.県番号 as 県番号"
											+ ", KN.都道府県名 as 都道府県名"
											+ ", RE.顧客No as 販売店ID"
											+ ", RE.顧客名1 + RE.顧客名2 as 販売店名称"
											+ ", TK.fcm名称 as システム名称"
											+ ", TK2.fcm名称 as システム略称"
											+ " FROM {0} as CL"
											+ " INNER JOIN {1} as B ON CL.fCliID = B.fkjCliMicID"
											+ " LEFT JOIN {2} as U ON B.fkjCliMicID = U.fusCliMicID"
											+ " LEFT JOIN {3} as P ON CL.fCliFirstcMan = P.fUsrID"
											+ " LEFT JOIN {4} as S ON CL.fCliID=S.顧客No"
											+ " LEFT JOIN {5} as AG ON CL.fCliID = AG.fdaCliMicID"
											+ " LEFT JOIN {6} as PC ON CL.fCliID = PC.CUSTOMER_ID"
											+ " LEFT JOIN {7} as KN ON LEFT(B.fkj住所１, 3) = left(KN.都道府県名, 3)"
											+ " LEFT JOIN {8} as RE ON RE.顧客No = U.fus販売店No"
											+ " LEFT JOIN {9} as TK ON TK.fcmコード種別 = '01' and U.fusシステム名 = TK.fcmコード"
											+ " LEFT JOIN {9} as TK2 ON U.fusシステム名 = TK2.fcmコード AND TK2.fcmコード種別 = '91'"
											+ " WHERE CL.fCliID = {10}"
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ]
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMih担当者]
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic営業担当]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik代行回収]
											, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik県番号]
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全販売店]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikコードマスタ]
											, CustomerNo);
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return CustomerInfo.DataTableToData(table);
		}

		/// <summary>
		/// 顧客Noに対するオンライン資格確認対象商品売上明細情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<オンライン資格確認対象商品売上明細> Select_オンライン資格確認対象商品売上明細(string tokuisakiNo, string connectStr)
		{
			// Ver1.18(2023/04/03 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 送付先リストから受注日を取得
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
										+ "SELECT TOP 1"
										+ " sykd_denno"
										+ ",sykd_uribi"
										+ ",sykd_tcd"
										+ " FROM {0}"
										+ " WHERE sykd_scd = '018021' AND sykd_tcd = {1}"
										+ " ORDER BY sykd_denno"
										+ ") as ソフト改修費 on ソフト改修費.sykd_denno = 明細.sykd_denno AND ソフト改修費.sykd_uribi = 明細.sykd_uribi AND ソフト改修費.sykd_tcd = 明細.sykd_tcd"
										+ " ORDER BY 明細.sykd_eda"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, tokuisakiNo);
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return オンライン資格確認対象商品売上明細.DataTableToList(table);
		}
	}
}
