﻿//
// OnlineLicenseMainteGetIO.cs
//
// オンライン資格保守サービス データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.00(2023/11/20 勝呂):新規作成
// Ver1.05(2024/07/19 勝呂):SHINKO ｵﾝ資･ｵﾝｻｲﾄ保守、MIC ｵﾝﾗｲﾝ資格確認保守ｻｰﾋﾞｽに対応
// Ver1.05(2024/07/19 勝呂):売上データの更新単位を月から空白に修正
// 
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using System.Data;

namespace CommonLib.DB.SqlServer.OnlineLicenseMainte
{
	static public class OnlineLicenseMainteGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// アプリケーション情報からオン資格保守サービス売上情報の取得
		/// </summary>
		/// <param name="ym">保守終了年月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>レコード数</returns>
		public static DataTable GetOnlineLicenseMainteEarningsOut(YearMonth ym, string connectStr)
		{
			// Ver1.05(2024/07/19 勝呂):SHINKO ｵﾝ資･ｵﾝｻｲﾄ保守、MIC ｵﾝﾗｲﾝ資格確認保守ｻｰﾋﾞｽに対応
			// Ver1.05(2024/07/19 勝呂):売上データの更新単位を月から空白に修正
			//string strSQL = string.Format(@"SELECT"
			//						+ " U.顧客No as f顧客No"
			//						+ ", U.顧客名１ + U.顧客名２ as f顧客名"
			//						+ ", U.得意先No as f得意先コード"
			//						+ ", U.請求先コード as f請求先コード"
			//						+ ", A.fai保守契約開始 as f保守開始月"
			//						+ ", A.fai保守契約終了 as f保守終了月"
			//						+ ", A.faiアプリケーションNo as fアプリケーションNo"
			//						+ ", A.faiアプリケーション名 as fアプリケーション名"
			//						+ ", C.fcm名称 as fcm名称"
			//						+ ", '月' as f更新単位"
			//						+ ", S.sms_scd as f商品コード"
			//						+ ", S.sms_mei as f商品名"
			//						+ ", convert(int, S.sms_hyo) as f標準価格"
			//						+ ", convert(int, S.sms_gen) as f原単価"
			//						+ ", S.sms_tani as f単位"
			//						+ ", B.fPca部門コード as fPCA部門コード"
			//						+ ", B.fPca倉庫コード as fPCA倉庫コード"
			//						+ ", B.f担当者コード as fPCA担当者コード"
			//						+ " FROM {0} as U"
			//						+ " INNER JOIN {1} as A on A.faiCliMicID = U.顧客No"
			//						+ " INNER JOIN {2} as C on C.fcmコード = A.faiアプリケーション名"
			//						+ " INNER JOIN {3} as S on S.sms_scd = C.fcmサブコード"
			//						+ " INNER JOIN {4} as B on B.fBshCode3 = U.支店コード"
			//						+ " WHERE U.終了フラグ = '0' AND A.fai終了フラグ = '0' AND A.fai保守契約終了 = '{5}' AND C.fcmコード種別 = '{6}' AND C.fcmコード IN ('{7}', '{8}', '{9}', '{10}', '{11}')"
			//						+ " ORDER BY U.顧客No, S.sms_scd"
			//						, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2]			// 0
			//						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報]	// 1
			//						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikコードマスタ]				// 2
			//						, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]			// 3
			//						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]				// 4
			//						, ym.GetNormalString()	// 5
			//						, tMikコードマスタ.fcmコード種別_ApplicationName	// 6
			//						, tMikコードマスタ.fcmコード_Richo_LineUsageFee1  // 7
			//						, tMikコードマスタ.fcmコード_Richo_MainteUsageFeePC1  // 8
			//						, tMikコードマスタ.fcmコード_Richo_MainteUsageFeeRT1      // 9
			//						, tMikコードマスタ.fcmコード_Ryoyo_LineUsageFee      // 10
			//						, tMikコードマスタ.fcmコード_Ryoyo_MainteUsageFee);  // 11
			string strSQL = string.Format(@"SELECT"
									+ " U.顧客No as f顧客No"
									+ ", U.顧客名１ + U.顧客名２ as f顧客名"
									+ ", U.得意先No as f得意先コード"
									+ ", U.請求先コード as f請求先コード"
									+ ", A.fai保守契約開始 as f保守開始月"
									+ ", A.fai保守契約終了 as f保守終了月"
									+ ", A.faiアプリケーションNo as fアプリケーションNo"
									+ ", A.faiアプリケーション名 as fアプリケーション名"
									+ ", C.fcm名称 as fcm名称"
									+ ", '' as f更新単位"
									+ ", S.sms_scd as f商品コード"
									+ ", S.sms_mei as f商品名"
									+ ", convert(int, S.sms_hyo) as f標準価格"
									+ ", convert(int, S.sms_gen) as f原単価"
									+ ", S.sms_tani as f単位"
									+ ", B.fPca部門コード as fPCA部門コード"
									+ ", B.fPca倉庫コード as fPCA倉庫コード"
									+ ", B.f担当者コード as fPCA担当者コード"
									+ " FROM {0} as U"
									+ " INNER JOIN {1} as A on A.faiCliMicID = U.顧客No"
									+ " INNER JOIN {2} as C on C.fcmコード = A.faiアプリケーション名"
									+ " INNER JOIN {3} as S on S.sms_scd = C.fcmサブコード"
									+ " INNER JOIN {4} as B on B.fBshCode3 = U.支店コード"
									+ " WHERE U.終了フラグ = '0' AND A.fai終了フラグ = '0' AND A.fai保守契約終了 = '{5}' AND C.fcmコード種別 = '{6}' AND C.fcmコード IN ('{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')"
									+ " ORDER BY U.顧客No, S.sms_scd"
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2]           // 0
									, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報] // 1
									, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikコードマスタ]             // 2
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]         // 3
									, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]               // 4
									, ym.GetNormalString()  // 5
									, tMikコードマスタ.fcmコード種別_ApplicationName   // 6
									, tMikコードマスタ.fcmコード_Richo_LineUsageFee1  // 7
									, tMikコードマスタ.fcmコード_Richo_MainteUsageFeePC1  // 8
									, tMikコードマスタ.fcmコード_Richo_MainteUsageFeeRT1      // 9
									, tMikコードマスタ.fcmコード_Ryoyo_LineUsageFee      // 10
									, tMikコードマスタ.fcmコード_Ryoyo_MainteUsageFee    // 11
									, tMikコードマスタ.fcmコード_Shinko_MainteUsageFee1  // 12
									, tMikコードマスタ.fcmコード_Mic_MainteUsageFee1);   // 13

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// アプリケーション情報からオン資格保守サービス売上情報の取得（緊急用）
		/// </summary>
		/// <param name="customerNoString">顧客No群</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>レコード数</returns>
		public static DataTable GetOnlineLicenseMainteEarningsOutEmergency(string customerNoString, string connectStr)
		{
			// Ver1.05(2024/07/19 勝呂):SHINKO ｵﾝ資･ｵﾝｻｲﾄ保守、MIC ｵﾝﾗｲﾝ資格確認保守ｻｰﾋﾞｽに対応
			// Ver1.05(2024/07/19 勝呂):売上データの更新単位を月から空白に修正
			//string strSQL = string.Format(@"SELECT"
			//						+ " U.顧客No as f顧客No"
			//						+ ", U.顧客名１ + U.顧客名２ as f顧客名"
			//						+ ", U.得意先No as f得意先コード"
			//						+ ", U.請求先コード as f請求先コード"
			//						+ ", A.fai保守契約開始 as f保守開始月"
			//						+ ", A.fai保守契約終了 as f保守終了月"
			//						+ ", A.faiアプリケーションNo as fアプリケーションNo"
			//						+ ", A.faiアプリケーション名 as fアプリケーション名"
			//						+ ", C.fcm名称 as fcm名称"
			//						+ ", '月' as f更新単位"
			//						+ ", S.sms_scd as f商品コード"
			//						+ ", S.sms_mei as f商品名"
			//						+ ", convert(int, S.sms_hyo) as f標準価格"
			//						+ ", convert(int, S.sms_gen) as f原単価"
			//						+ ", S.sms_tani as f単位"
			//						+ ", B.fPca部門コード as fPCA部門コード"
			//						+ ", B.fPca倉庫コード as fPCA倉庫コード"
			//						+ ", B.f担当者コード as fPCA担当者コード"
			//						+ " FROM {0} as U"
			//						+ " INNER JOIN {1} as A on A.faiCliMicID = U.顧客No"
			//						+ " INNER JOIN {2} as C on C.fcmコード = A.faiアプリケーション名"
			//						+ " INNER JOIN {3} as S on S.sms_scd = C.fcmサブコード"
			//						+ " INNER JOIN {4} as B on B.fBshCode3 = U.支店コード"
			//						+ " WHERE U.終了フラグ = '0' AND A.fai終了フラグ = '0' AND A.faiCliMicID in ({5}) AND C.fcmコード種別 = '{6}' AND C.fcmコード IN ('{7}', '{8}', '{9}', '{10}', '{11}')"
			//						+ " ORDER BY U.顧客No, S.sms_scd"
			//						, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2]           // 0
			//						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報] // 1
			//						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikコードマスタ]             // 2
			//						, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]         // 3
			//						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]               // 4
			//						, customerNoString  // 5
			//						, tMikコードマスタ.fcmコード種別_ApplicationName   // 6
			//						, tMikコードマスタ.fcmコード_Richo_LineUsageFee1  // 7
			//						, tMikコードマスタ.fcmコード_Richo_MainteUsageFeePC1  // 8
			//						, tMikコードマスタ.fcmコード_Richo_MainteUsageFeeRT1      // 9
			//						, tMikコードマスタ.fcmコード_Ryoyo_LineUsageFee      // 10
			//						, tMikコードマスタ.fcmコード_Ryoyo_MainteUsageFee);  // 11
			string strSQL = string.Format(@"SELECT"
									+ " U.顧客No as f顧客No"
									+ ", U.顧客名１ + U.顧客名２ as f顧客名"
									+ ", U.得意先No as f得意先コード"
									+ ", U.請求先コード as f請求先コード"
									+ ", A.fai保守契約開始 as f保守開始月"
									+ ", A.fai保守契約終了 as f保守終了月"
									+ ", A.faiアプリケーションNo as fアプリケーションNo"
									+ ", A.faiアプリケーション名 as fアプリケーション名"
									+ ", C.fcm名称 as fcm名称"
									+ ", '' as f更新単位"
									+ ", S.sms_scd as f商品コード"
									+ ", S.sms_mei as f商品名"
									+ ", convert(int, S.sms_hyo) as f標準価格"
									+ ", convert(int, S.sms_gen) as f原単価"
									+ ", S.sms_tani as f単位"
									+ ", B.fPca部門コード as fPCA部門コード"
									+ ", B.fPca倉庫コード as fPCA倉庫コード"
									+ ", B.f担当者コード as fPCA担当者コード"
									+ " FROM {0} as U"
									+ " INNER JOIN {1} as A on A.faiCliMicID = U.顧客No"
									+ " INNER JOIN {2} as C on C.fcmコード = A.faiアプリケーション名"
									+ " INNER JOIN {3} as S on S.sms_scd = C.fcmサブコード"
									+ " INNER JOIN {4} as B on B.fBshCode3 = U.支店コード"
									+ " WHERE U.終了フラグ = '0' AND A.fai終了フラグ = '0' AND A.faiCliMicID in ({5}) AND C.fcmコード種別 = '{6}' AND C.fcmコード IN ('{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')"
									+ " ORDER BY U.顧客No, S.sms_scd"
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2]           // 0
									, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報] // 1
									, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikコードマスタ]             // 2
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]         // 3
									, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]               // 4
									, customerNoString  // 5
									, tMikコードマスタ.fcmコード種別_ApplicationName   // 6
									, tMikコードマスタ.fcmコード_Richo_LineUsageFee1  // 7
									, tMikコードマスタ.fcmコード_Richo_MainteUsageFeePC1  // 8
									, tMikコードマスタ.fcmコード_Richo_MainteUsageFeeRT1      // 9
									, tMikコードマスタ.fcmコード_Ryoyo_LineUsageFee      // 10
									, tMikコードマスタ.fcmコード_Ryoyo_MainteUsageFee  // 11
									, tMikコードマスタ.fcmコード_Shinko_MainteUsageFee1  // 12
									, tMikコードマスタ.fcmコード_Mic_MainteUsageFee1);   // 13

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
	}
}
