//
// vMic全ユーザー3.cs
//
// 全ユーザー3情報クラス
// [JunpDB].[dbo].[vMic全ユーザー3]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2019/06/28):新規作成 勝呂
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.View
{
	public class vMic全ユーザー3
	{
		public string レセコンユーザー { get; set; }

		public string きっかけ商品ユーザー { get; set; }

		public string ヘルスケアユーザー { get; set; }

		public string 終了フラグ { get; set; }

		public int 有効ユーザーフラグ { get; set; }
		public DateTime? 更新日時 { get; set; }

		public string 県番号 { get; set; }

		public string 都道府県名 { get; set; }

		public string 営業部コード { get; set; }

		public string 営業部名 { get; set; }

		public string 拠点コード { get; set; }

		public string 拠点名 { get; set; }

		public string 担当者コード { get; set; }

		public string 担当者名 { get; set; }

		public string 営業担当者コード { get; set; }

		public string 営業担当者名 { get; set; }

		public string インスト担当者コード { get; set; }

		public string インスト担当者名 { get; set; }

		public string レセコンシステムコード { get; set; }

		public string レセコン名称 { get; set; }

		public string レセコン略称 { get; set; }

		public int 顧客No { get; set; }

		public string 得意先No { get; set; }

		public string 顧客名1 { get; set; }

		public string 顧客名2 { get; set; }

		public string フリガナ { get; set; }

		public string 郵便番号 { get; set; }

		public string 住所1 { get; set; }

		public string 住所2 { get; set; }

		public string 住所フリガナ { get; set; }

		public string 電話番号 { get; set; }

		public string FAX番号 { get; set; }

		public string 備考 { get; set; }

		public string 改正時情報 { get; set; }

		public string 医療機関コード { get; set; }

		public string 開設者名 { get; set; }

		public string 院長名 { get; set; }

		public string 院長名フリガナ { get; set; }

		public string 発送先名 { get; set; }

		public string 発送先郵便番号 { get; set; }

		public string 発送先住所 { get; set; }

		public string 発送先電話番号 { get; set; }

		public string 発送先備考 { get; set; }

		public string 請求先コード { get; set; }

		public string 請求先名 { get; set; }

		public string 請求先郵便番号 { get; set; }

		public string 請求先住所 { get; set; }

		public string 請求先電話番号 { get; set; }

		public string 請求先備考 { get; set; }

		public string 請求締日 { get; set; }

		public string 請求回収日 { get; set; }

		public string 請求区分No { get; set; }

		public string 請求区分 { get; set; }

		public string 代引配送 { get; set; }

		public string カルテ用紙 { get; set; }

		public string 領収書用紙 { get; set; }

		public string 領収書用紙2 { get; set; }

		public int 売伝No { get; set; }
		public string 販売形態 { get; set; }

		public string 納品月 { get; set; }

		public string 売上月 { get; set; }

		public string 前システムコード { get; set; }

		public string 前システム名称 { get; set; }

		public string 前システム終了 { get; set; }

		public string 休診日 { get; set; }

		public string 診療時間 { get; set; }

		public string メールアドレス { get; set; }

		public string OS { get; set; }

		public string 登録カード回収日 { get; set; }

		public int 単体 { get; set; }
		public int サーバー { get; set; }
		public int クライアント { get; set; }
		public int 同時接続クライアント数 { get; set; }

		public string レセ電算請求種別 { get; set; }

		public string レセ電算請求開始 { get; set; }

		public int eStore登録フラグ { get; set; }
		public int メルマガ購読フラグ { get; set; }

		public string eStore登録メールアドレス { get; set; }

		public string eStore_パスワード { get; set; }

		public string eStore_パスワード読み { get; set; }

		public string MWS_ID { get; set; }

		public string MWS_パスワード { get; set; }

		public string MWS_パスワード読み { get; set; }

		public string MWS_申込種別 { get; set; }
		public DateTime? MWS_申込書回収日 { get; set; }

		public string MWS_販売種別 { get; set; }
		public DateTime? MWS_使用許諾期限 { get; set; }
		public int 販売店ID { get; set; }

		public string 販売店名称 { get; set; }

		public string 販売店担当者名 { get; set; }

		public string 販売店グループコード { get; set; }
		public string 販売店グループ名称 { get; set; }

		public string 販売店区分コード { get; set; }

		public string 販売店区分名称 { get; set; }

		public string 代行回収APLUSコード { get; set; }

		public string 代行回収銀行名カナ { get; set; }

		public string 代行回収銀行コード { get; set; }

		public string 代行回収支店名カナ { get; set; }

		public string 代行回収支店コード { get; set; }

		public string 代行回収預金種別 { get; set; }

		public string 代行回収口座番号 { get; set; }

		public string 代行回収預金者名 { get; set; }

		public string 代行回収上限金額 { get; set; }

		public string 代行回収最終引落日 { get; set; }

		public string 代行回収状態 { get; set; }

		public string 代行回収備考 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vMic全ユーザー3()
		{
			レセコンユーザー = string.Empty;
			きっかけ商品ユーザー = string.Empty;
			ヘルスケアユーザー = string.Empty;
			終了フラグ = string.Empty;
			有効ユーザーフラグ = 0;
			更新日時 = null;
			県番号 = string.Empty;
			都道府県名 = string.Empty;
			営業部コード = string.Empty;
			営業部名 = string.Empty;
			拠点コード = string.Empty;
			拠点名 = string.Empty;
			担当者コード = string.Empty;
			担当者名 = string.Empty;
			営業担当者コード = string.Empty;
			営業担当者名 = string.Empty;
			インスト担当者コード = string.Empty;
			インスト担当者名 = string.Empty;
			レセコンシステムコード = string.Empty;
			レセコン名称 = string.Empty;
			レセコン略称 = string.Empty;
			顧客No = 0;
			得意先No = string.Empty;
			顧客名1 = string.Empty;
			顧客名2 = string.Empty;
			フリガナ = string.Empty;
			郵便番号 = string.Empty;
			住所1 = string.Empty;
			住所2 = string.Empty;
			住所フリガナ = string.Empty;
			電話番号 = string.Empty;
			FAX番号 = string.Empty;
			備考 = string.Empty;
			改正時情報 = string.Empty;
			医療機関コード = string.Empty;
			開設者名 = string.Empty;
			院長名 = string.Empty;
			院長名フリガナ = string.Empty;
			発送先名 = string.Empty;
			発送先郵便番号 = string.Empty;
			発送先住所 = string.Empty;
			発送先電話番号 = string.Empty;
			発送先備考 = string.Empty;
			請求先コード = string.Empty;
			請求先名 = string.Empty;
			請求先郵便番号 = string.Empty;
			請求先住所 = string.Empty;
			請求先電話番号 = string.Empty;
			請求先備考 = string.Empty;
			請求締日 = string.Empty;
			請求回収日 = string.Empty;
			請求区分No = string.Empty;
			請求区分 = string.Empty;
			代引配送 = string.Empty;
			カルテ用紙 = string.Empty;
			領収書用紙 = string.Empty;
			領収書用紙2 = string.Empty;
			売伝No = 0;
			販売形態 = string.Empty;
			納品月 = string.Empty;
			売上月 = string.Empty;
			前システムコード = string.Empty;
			前システム名称 = string.Empty;
			前システム終了 = string.Empty;
			休診日 = string.Empty;
			診療時間 = string.Empty;
			メールアドレス = string.Empty;
			OS = string.Empty;
			登録カード回収日 = string.Empty;
			単体 = 0;
			サーバー = 0;
			クライアント = 0;
			同時接続クライアント数 = 0;
			レセ電算請求種別 = string.Empty;
			レセ電算請求開始 = string.Empty;
			eStore登録フラグ = 0;
			メルマガ購読フラグ = 0;
			eStore登録メールアドレス = string.Empty;
			eStore_パスワード = string.Empty;
			eStore_パスワード読み = string.Empty;
			MWS_ID = string.Empty;
			MWS_パスワード = string.Empty;
			MWS_パスワード読み = string.Empty;
			MWS_申込種別 = string.Empty;
			MWS_申込書回収日 = null;
			MWS_販売種別 = string.Empty;
			MWS_使用許諾期限 = null;
			販売店ID = 0;
			販売店名称 = string.Empty;
			販売店担当者名 = string.Empty;
			販売店グループコード = string.Empty;
			販売店グループ名称 = string.Empty;
			販売店区分コード = string.Empty;
			販売店区分名称 = string.Empty;
			代行回収APLUSコード = string.Empty;
			代行回収銀行名カナ = string.Empty;
			代行回収銀行コード = string.Empty;
			代行回収支店名カナ = string.Empty;
			代行回収支店コード = string.Empty;
			代行回収預金種別 = string.Empty;
			代行回収口座番号 = string.Empty;
			代行回収預金者名 = string.Empty;
			代行回収上限金額 = string.Empty;
			代行回収最終引落日 = string.Empty;
			代行回収状態 = string.Empty;
			代行回収備考 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMic全ユーザー3> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<vMic全ユーザー3> result = new List<vMic全ユーザー3>();
				foreach (DataRow row in table.Rows)
				{
					vMic全ユーザー3 data = new vMic全ユーザー3();
					data.レセコンユーザー = row["レセコンユーザー"].ToString().Trim();
					data.きっかけ商品ユーザー = row["きっかけ商品ユーザー"].ToString().Trim();
					data.ヘルスケアユーザー = row["ヘルスケアユーザー"].ToString().Trim();
					data.終了フラグ = row["終了フラグ"].ToString().Trim();
					data.有効ユーザーフラグ = DataBaseValue.ConvObjectToInt(row["有効ユーザーフラグ"]);
					data.更新日時 = DataBaseValue.ConvObjectToDateTimeNull(row["更新日時"]);
					data.県番号 = row["県番号"].ToString().Trim();
					data.都道府県名 = row["都道府県名"].ToString().Trim();
					data.営業部コード = row["営業部コード"].ToString().Trim();
					data.営業部名 = row["営業部名"].ToString().Trim();
					data.拠点コード = row["拠点コード"].ToString().Trim();
					data.拠点名 = row["拠点名"].ToString().Trim();
					data.担当者コード = row["担当者コード"].ToString().Trim();
					data.担当者名 = row["担当者名"].ToString().Trim();
					data.営業担当者コード = row["営業担当者コード"].ToString().Trim();
					data.営業担当者名 = row["営業担当者名"].ToString().Trim();
					data.インスト担当者コード = row["インスト担当者コード"].ToString().Trim();
					data.インスト担当者名 = row["インスト担当者名"].ToString().Trim();
					data.レセコンシステムコード = row["レセコンシステムコード"].ToString().Trim();
					data.レセコン名称 = row["レセコン名称"].ToString().Trim();
					data.レセコン略称 = row["レセコン略称"].ToString().Trim();
					data.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.得意先No = row["得意先No"].ToString().Trim();
					data.顧客名1 = row["顧客名1"].ToString().Trim();
					data.顧客名2 = row["顧客名2"].ToString().Trim();
					data.フリガナ = row["フリガナ"].ToString().Trim();
					data.郵便番号 = row["郵便番号"].ToString().Trim();
					data.住所1 = row["住所1"].ToString().Trim();
					data.住所2 = row["住所2"].ToString().Trim();
					data.住所フリガナ = row["住所フリガナ"].ToString().Trim();
					data.電話番号 = row["電話番号"].ToString().Trim();
					data.FAX番号 = row["FAX番号"].ToString().Trim();
					data.備考 = row["備考"].ToString().Trim();
					data.改正時情報 = row["改正時情報"].ToString().Trim();
					data.医療機関コード = row["医療機関コード"].ToString().Trim();
					data.開設者名 = row["開設者名"].ToString().Trim();
					data.院長名 = row["院長名"].ToString().Trim();
					data.院長名フリガナ = row["院長名フリガナ"].ToString().Trim();
					data.発送先名 = row["発送先名"].ToString().Trim();
					data.発送先郵便番号 = row["発送先郵便番号"].ToString().Trim();
					data.発送先住所 = row["発送先住所"].ToString().Trim();
					data.発送先電話番号 = row["発送先電話番号"].ToString().Trim();
					data.発送先備考 = row["発送先備考"].ToString().Trim();
					data.請求先コード = row["請求先コード"].ToString().Trim();
					data.請求先名 = row["請求先名"].ToString().Trim();
					data.請求先郵便番号 = row["請求先郵便番号"].ToString().Trim();
					data.請求先住所 = row["担当者コード"].ToString().Trim();
					data.請求先電話番号 = row["請求先電話番号"].ToString().Trim();
					data.請求先備考 = row["請求先備考"].ToString().Trim();
					data.請求締日 = row["請求締日"].ToString().Trim();
					data.請求回収日 = row["請求回収日"].ToString().Trim();
					data.請求区分No = row["請求区分No"].ToString().Trim();
					data.請求区分 = row["請求区分"].ToString().Trim();
					data.代引配送 = row["代引配送"].ToString().Trim();
					data.カルテ用紙 = row["カルテ用紙"].ToString().Trim();
					data.領収書用紙 = row["領収書用紙"].ToString().Trim();
					data.領収書用紙2 = row["領収書用紙2"].ToString().Trim();
					data.売伝No = DataBaseValue.ConvObjectToInt(row["売伝No"]);
					data.販売形態 = row["販売形態"].ToString().Trim();
					data.納品月 = row["納品月"].ToString().Trim();
					data.売上月 = row["売上月"].ToString().Trim();
					data.前システムコード = row["前システムコード"].ToString().Trim();
					data.前システム名称 = row["前システム名称"].ToString().Trim();
					data.前システム終了 = row["前システム終了"].ToString().Trim();
					data.休診日 = row["休診日"].ToString().Trim();
					data.診療時間 = row["診療時間"].ToString().Trim();
					data.メールアドレス = row["メールアドレス"].ToString().Trim();
					data.OS = row["OS"].ToString().Trim();
					data.登録カード回収日 = row["登録カード回収日"].ToString().Trim();
					data.単体 = DataBaseValue.ConvObjectToInt(row["単体"]);
					data.サーバー = DataBaseValue.ConvObjectToInt(row["サーバー"]);
					data.クライアント = DataBaseValue.ConvObjectToInt(row["クライアント"]);
					data.同時接続クライアント数 = DataBaseValue.ConvObjectToInt(row["同時接続クライアント数"]);
					data.レセ電算請求種別 = row["レセ電算請求種別"].ToString().Trim();
					data.レセ電算請求開始 = row["レセ電算請求開始"].ToString().Trim();
					data.eStore登録フラグ = DataBaseValue.ConvObjectToInt(row["eStore登録フラグ"]);
					data.メルマガ購読フラグ = DataBaseValue.ConvObjectToInt(row["メルマガ購読フラグ"]);
					data.eStore登録メールアドレス = row["eStore登録メールアドレス"].ToString().Trim();
					data.eStore_パスワード = row["eStore_パスワード"].ToString().Trim();
					data.eStore_パスワード読み = row["eStore_パスワード読み"].ToString().Trim();
					data.MWS_ID = row["MWS_ID"].ToString().Trim();
					data.MWS_パスワード = row["MWS_パスワード"].ToString().Trim();
					data.MWS_パスワード読み = row["MWS_パスワード読み"].ToString().Trim();
					data.MWS_申込種別 = row["MWS_申込種別"].ToString().Trim();
					data.MWS_申込書回収日 = DataBaseValue.ConvObjectToDateTimeNull(row["MWS_申込書回収日"]);
					data.MWS_販売種別 = row["MWS_販売種別"].ToString().Trim();
					data.MWS_使用許諾期限 = DataBaseValue.ConvObjectToDateTimeNull(row["MWS_使用許諾期限"]);
					data.販売店ID = DataBaseValue.ConvObjectToInt(row["販売店ID"]);
					data.販売店名称 = row["販売店名称"].ToString().Trim();
					data.販売店担当者名 = row["販売店担当者名"].ToString().Trim();
					data.販売店グループコード = row["販売店グループコード"].ToString().Trim();
					data.販売店グループ名称 = row["販売店グループ名称"].ToString().Trim();
					data.販売店区分コード = row["販売店区分コード"].ToString().Trim();
					data.販売店区分名称 = row["販売店区分名称"].ToString().Trim();
					data.代行回収APLUSコード = row["代行回収APLUSコード"].ToString().Trim();
					data.代行回収銀行名カナ = row["代行回収銀行名カナ"].ToString().Trim();
					data.代行回収銀行コード = row["代行回収銀行コード"].ToString().Trim();
					data.代行回収支店名カナ = row["代行回収支店名カナ"].ToString().Trim();
					data.代行回収支店コード = row["代行回収支店コード"].ToString().Trim();
					data.代行回収預金種別 = row["代行回収預金種別"].ToString().Trim();
					data.代行回収口座番号 = row["代行回収口座番号"].ToString().Trim();
					data.代行回収預金者名 = row["代行回収預金者名"].ToString().Trim();
					data.代行回収上限金額 = row["代行回収上限金額"].ToString().Trim();
					data.代行回収最終引落日 = row["代行回収最終引落日"].ToString().Trim();
					data.代行回収状態 = row["代行回収状態"].ToString().Trim();
					data.代行回収備考 = row["代行回収備考"].ToString().Trim();
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
