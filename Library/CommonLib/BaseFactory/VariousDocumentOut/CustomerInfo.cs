//
// CustomerInfo.cs
// 
// 顧客情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2022/07/06 勝呂):新規作成
//
using CommonLib.Common;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.VariousDocumentOut
{
	/// <summary>
	/// 顧客情報
	/// </summary>
	public class CustomerInfo
	{
		/// <summary>
		/// tMik基本情報.fkj顧客区分
		/// </summary>
		public int 顧客区分 { get; set; }

		/// <summary>
		/// tClient.fCliID
		/// </summary>
		public int 顧客No { get; set; }

		/// <summary>
		/// tMik基本情報.fkj得意先情報
		/// </summary>
		public string 得意先No { get; set; }

		/// <summary>
		/// tClient.顧客名1
		/// </summary>
		public string 顧客名1 { get; set; }

		/// <summary>
		/// tMik基本情報.fkj顧客名2
		/// </summary>
		public string 顧客名2 { get; set; }

		/// <summary>
		/// tMik基本情報.fkj住所1
		/// </summary>
		public string 住所1 { get; set; }

		/// <summary>
		/// tMik基本情報.fkj住所2
		/// </summary>
		public string 住所2 { get; set; }

		/// <summary>
		/// tMik基本情報.fkj郵便番号
		/// </summary>
		public string 郵便番号 { get; set; }

		/// <summary>
		/// tMik基本情報.fkjファックス番号
		/// </summary>
		public string FAX番号 { get; set; }

		/// <summary>
		/// tMik基本情報.fkj電話番号
		/// </summary>
		public string 電話番号 { get; set; }

		/// <summary>
		/// tMikユーザ.fus院長名
		/// </summary>
		public string 院長名 { get; set; }

		/// <summary>
		/// tMikユーザ.fus運用サポート情報
		/// </summary>
		public string 運用サポート情報 { get; set; }

		/// <summary>
		/// tMikユーザ.fus医保医療コード
		/// </summary>
		public string 医保医療コード { get; set; }

		/// <summary>
		/// tClient.fCliYomi
		/// </summary>
		public string フリガナ { get; set; }

		/// <summary>
		/// tMik基本情報.fkj住所フリガナ
		/// </summary>
		public string 住所フリガナ { get; set; }

		/// <summary>
		/// tMikユーザ.fus院長名フリガナ
		/// </summary>
		public string 院長名フリガナ { get; set; }

		/// <summary>
		/// tMikユーザ.fus休診日
		/// </summary>
		public string 休診日 { get; set; }

		/// <summary>
		/// tMikユーザ.fus診療時間
		/// </summary>
		public string 診療時間 { get; set; }

		/// <summary>
		/// tMikユーザ.fusﾒｰﾙｱﾄﾞﾚｽ
		/// </summary>
		public string メールアドレス { get; set; }

		/// <summary>
		/// tMikユーザ.fus備考2
		/// </summary>
		public string 備考 { get; set; }

		/// <summary>
		/// tMikユーザ.fus単体
		/// </summary>
		public int 単体 { get; set; }

		/// <summary>
		/// tMikユーザ.fusサーバー
		/// </summary>
		public int サーバー { get; set; }

		/// <summary>
		/// tMikユーザ.fusクライアント
		/// </summary>
		public int クライアント { get; set; }

		/// <summary>
		/// tMikユーザ.fus納品月
		/// </summary>
		public string 納品月 { get; set; }

		/// <summary>
		/// vMih担当者.fBshName3
		/// </summary>
		public string 支店名 { get; set; }

		/// <summary>
		/// vMih担当者.fUsrName
		/// </summary>
		public string 担当者名 { get; set; }

		/// <summary>
		/// vMic営業担当.営業担当者名
		/// </summary>
		public string 営業担当者名 { get; set; }

		/// <summary>
		/// ftMik代行回収.daAPLUSコード
		/// </summary>
		public string 代行回収APLUSコード { get; set; }

		/// <summary>
		/// ftMik代行回収.fda状態
		/// </summary>
		public string 代行回収状態 { get; set; }

		/// <summary>
		/// tMikユーザ.fus開設者
		/// </summary>
		public string 開設者名 { get; set; }

		/// <summary>
		/// T_PRODUCT_CONTROL.PRODUCT_ID
		/// </summary>
		public string MWS_ID { get; set; }

		/// <summary>
		/// T_PRODUCT_CONTROL.PASSWORD
		/// </summary>
		public string MWS_パスワード { get; set; }

		/// <summary>
		/// T_PRODUCT_CONTROL.PASSWORD_READING
		/// </summary>
		public string MWS_パスワード読み { get; set; }

		/// <summary>
		/// tMik県番号.県番号
		/// </summary>
		public string 県番号 { get; set; }

		/// <summary>
		/// tMik県番号.都道府県名
		/// </summary>
		public string 都道府県名 { get; set; }

		/// <summary>
		/// vMic全販売店.顧客No
		/// </summary>
		public int 販売店ID { get; set; }

		/// <summary>
		/// vMic全販売店.顧客名1 + vMic全販売店.顧客名2
		/// </summary>
		public string 販売店名称 { get; set; }

		/// <summary>
		/// tMikコードマスタ.fcm名称（fcmコード種別 = '01'）
		/// </summary>
		public string システム名称 { get; set; }

		/// <summary>
		/// tMikコードマスタ.fcm名称（fcmコード種別 = '91'）
		/// </summary>
		public string システム略称 { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string 顧客名
		{
			get
			{
				string name = 顧客名1;
				if (0 < 顧客名2.Length)
				{
					name += 顧客名2;
				}
				return name;
			}
		}

		/// <summary>
		/// 住所
		/// </summary>
		public string 住所
		{
			get
			{
				string add = 住所1;
				if (0 < 住所2.Length)
				{
					add += " " + 住所2;
				}
				return add;
			}
		}

		/// <summary>
		/// 医療機関コード(数字のみ)
		/// </summary>
		public string NumericClinicCode
		{
			get
			{
				return StringUtil.DigitOnlyString(医保医療コード);
			}
		}

		/// <summary>
		/// 郵便番号(数字のみ)
		/// </summary>
		public string NumericZipcode
		{
			get
			{
				return StringUtil.DigitOnlyString(郵便番号);
			}
		}

		/// <summary>
		/// 有効な顧客情報かどうか？
		/// </summary>
		public bool Enable
		{
			get
			{
				return (2 == 顧客区分 || 18 == 顧客区分) ? true : false;
			}
		}

		/// <summary>
		/// 支部名の取得
		/// Ver1.01(2023/01/20):2022/10 社保組織変更 支部→審査委員会事務局
		///   4-光ディスク請求届出「光ディスク請求届出-社保用」、「光ディスク請求確認試験依頼書-社保用」
		///   5-オンライン請求届出「電子証明書発行等依頼書」、「オンライン請求届出-社保用」
		/// https://www.ssk.or.jp/shibu/index.html
		/// </summary>
		public string 支部名
		{
			get
			{
				string ken = 都道府県名;
				if ("北海道" != ken)
				{
					ken = ken.Replace("県", "").Replace("都", "").Replace("府", "");
				}
				//return string.Format("{0}支部", ken);
				return string.Format("{0}審査委員会事務局", ken);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CustomerInfo()
		{
			this.Empty();
		}

		/// <summary>
		/// クリア
		/// </summary>
		public void Empty()
		{
			顧客区分 = 0;
			顧客No = 0;
			得意先No = string.Empty;
			顧客名1 = string.Empty;
			顧客名2 = string.Empty;
			住所1 = string.Empty;
			住所2 = string.Empty;
			郵便番号 = string.Empty;
			FAX番号 = string.Empty;
			電話番号 = string.Empty;
			院長名 = string.Empty;
			運用サポート情報 = string.Empty;
			医保医療コード = string.Empty;
			フリガナ = string.Empty;
			住所フリガナ = string.Empty;
			院長名フリガナ = string.Empty;
			休診日 = string.Empty;
			診療時間 = string.Empty;
			メールアドレス = string.Empty;
			備考 = string.Empty;
			単体 = 0;
			サーバー = 0;
			クライアント = 0;
			納品月 = string.Empty;
			支店名 = string.Empty;
			担当者名 = string.Empty;
			営業担当者名 = string.Empty;
			代行回収APLUSコード = string.Empty;
			代行回収状態 = string.Empty;
			開設者名 = string.Empty;
			MWS_ID = string.Empty;
			MWS_パスワード = string.Empty;
			MWS_パスワード読み = string.Empty;
			県番号 = string.Empty;
			都道府県名 = string.Empty;
			販売店ID = 0;
			販売店名称 = string.Empty;
			システム名称 = string.Empty;
			システム略称 = string.Empty;
		}

	/// <summary>
	/// DataTable → リスト変換
	/// </summary>
	/// <param name="table"></param>
	/// <returns></returns>
	public static List<CustomerInfo> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<CustomerInfo> result = new List<CustomerInfo>();
                foreach (DataRow row in table.Rows)
                {
                    CustomerInfo data = new CustomerInfo
                    {
						顧客区分 = DataBaseValue.ConvObjectToInt(row["顧客区分"]),
						顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						得意先No = row["得意先No"].ToString().Trim(),
						顧客名1 = row["顧客名1"].ToString().Trim(),
						顧客名2 = row["顧客名2"].ToString().Trim(),
						住所1 = row["住所1"].ToString().Trim(),
						住所2 = row["住所2"].ToString().Trim(),
						郵便番号 = row["郵便番号"].ToString().Trim(),
						FAX番号 = row["FAX番号"].ToString().Trim(),
						電話番号 = row["電話番号"].ToString().Trim(),
						院長名 = row["院長名"].ToString().Trim(),
						運用サポート情報 = row["運用サポート情報"].ToString().Trim(),
						医保医療コード = row["医保医療コード"].ToString().Trim(),
						フリガナ = row["フリガナ"].ToString().Trim(),
						住所フリガナ = row["住所フリガナ"].ToString().Trim(),
						院長名フリガナ = row["院長名フリガナ"].ToString().Trim(),
						休診日 = row["休診日"].ToString().Trim(),
						診療時間 = row["診療時間"].ToString().Trim(),
						メールアドレス = row["メールアドレス"].ToString().Trim(),
						備考 = row["備考"].ToString().Trim(),
						単体 = DataBaseValue.ConvObjectToInt(row["単体"]),
						サーバー = DataBaseValue.ConvObjectToInt(row["サーバー"]),
						クライアント = DataBaseValue.ConvObjectToInt(row["クライアント"]),
						納品月 = row["納品月"].ToString().Trim(),
						支店名 = row["支店名"].ToString().Trim(),
						担当者名 = row["担当者名"].ToString().Trim(),
						営業担当者名 = row["営業担当者名"].ToString().Trim(),
						代行回収APLUSコード = row["代行回収APLUSコード"].ToString().Trim(),
						代行回収状態 = row["代行回収状態"].ToString().Trim(),
						開設者名 = row["開設者名"].ToString().Trim(),
						MWS_ID = row["MWS_ID"].ToString().Trim(),
						MWS_パスワード = row["MWS_パスワード"].ToString().Trim(),
						MWS_パスワード読み = row["MWS_パスワード読み"].ToString().Trim(),
						県番号 = row["県番号"].ToString().Trim(),
						都道府県名 = row["都道府県名"].ToString().Trim(),
						販売店ID = DataBaseValue.ConvObjectToInt(row["販売店ID"]),
						販売店名称 = row["販売店名称"].ToString().Trim(),
						システム名称 = row["システム名称"].ToString().Trim(),
						システム略称 = row["システム略称"].ToString().Trim(),
					};
                    result.Add(data);
                }
                return result;
            }
            return null;
        }

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns>CustomerInfo</returns>
		public static CustomerInfo DataTableToData(DataTable table)
		{
			if (null != table && 1 == table.Rows.Count)
			{
				DataRow row = table.Rows[0];
				CustomerInfo result = new CustomerInfo();
				result.顧客区分 = DataBaseValue.ConvObjectToInt(row["顧客区分"]);
				result.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
				result.得意先No = row["得意先No"].ToString().Trim();
				result.顧客名1 = row["顧客名1"].ToString().Trim();
				result.顧客名2 = row["顧客名2"].ToString().Trim();
				result.住所1 = row["住所1"].ToString().Trim();
				result.住所2 = row["住所2"].ToString().Trim();
				result.郵便番号 = row["郵便番号"].ToString().Trim();
				result.FAX番号 = row["FAX番号"].ToString().Trim();
				result.電話番号 = row["電話番号"].ToString().Trim();
				result.院長名 = row["院長名"].ToString().Trim();
				result.運用サポート情報 = row["運用サポート情報"].ToString().Trim();
				result.医保医療コード = row["医保医療コード"].ToString().Trim();
				result.フリガナ = row["フリガナ"].ToString().Trim();
				result.住所フリガナ = row["住所フリガナ"].ToString().Trim();
				result.院長名フリガナ = row["院長名フリガナ"].ToString().Trim();
				result.休診日 = row["休診日"].ToString().Trim();
				result.診療時間 = row["診療時間"].ToString().Trim();
				result.メールアドレス = row["メールアドレス"].ToString().Trim();
				result.備考 = row["備考"].ToString().Trim();
				result.単体 = DataBaseValue.ConvObjectToInt(row["単体"]);
				result.サーバー = DataBaseValue.ConvObjectToInt(row["サーバー"]);
				result.クライアント = DataBaseValue.ConvObjectToInt(row["クライアント"]);
				result.納品月 = row["納品月"].ToString().Trim();
				result.支店名 = row["支店名"].ToString().Trim();
				result.担当者名 = row["担当者名"].ToString().Trim();
				result.営業担当者名 = row["営業担当者名"].ToString().Trim();
				result.代行回収APLUSコード = row["代行回収APLUSコード"].ToString().Trim();
				result.代行回収状態 = row["代行回収状態"].ToString().Trim();
				result.開設者名 = row["開設者名"].ToString().Trim();
				result.MWS_ID = row["MWS_ID"].ToString().Trim();
				result.MWS_パスワード = row["MWS_パスワード"].ToString().Trim();
				result.MWS_パスワード読み = row["MWS_パスワード読み"].ToString().Trim();
				result.県番号 = row["県番号"].ToString().Trim();
				result.都道府県名 = row["都道府県名"].ToString().Trim();
				result.販売店ID = DataBaseValue.ConvObjectToInt(row["販売店ID"]);
				result.販売店名称 = row["販売店名称"].ToString().Trim();
				result.システム名称 = row["システム名称"].ToString().Trim();
				result.システム略称 = row["システム略称"].ToString().Trim();
				return result;
			}
			return null;
		}
	}
}
