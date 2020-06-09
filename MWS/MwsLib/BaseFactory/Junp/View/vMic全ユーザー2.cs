//
// vMic全ユーザー2.cs
//
// 全ユーザー2情報クラス
// [JunpDB].[dbo].[vMic全ユーザー2]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using MwsLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.Junp.View
{
    public class vMic全ユーザー2
	{
        public string 担当者コード { get; set; }
        public string 担当者名 { get; set; }
        public string 支店コード { get; set; }
        public string 支店名 { get; set; }
        public string システム名称 { get; set; }
        public bool 終了フラグ { get; set; }
        public int 顧客No { get; set; }
        public string 得意先No { get; set; }
        public string 顧客名１ { get; set; }
        public string 顧客名２ { get; set; }
        public string フリガナ { get; set; }
        public string 郵便番号 { get; set; }
        public string 住所１ { get; set; }
        public string 住所２ { get; set; }
        public string 住所フリガナ { get; set; }
        public string 電話番号 { get; set; }
        public string FAX番号 { get; set; }
        public int? 売伝No { get; set; }
        public string 医保医療コード { get; set; }
        public string 国保医療コード { get; set; }
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
        public string システム名 { get; set; }
        public int? オプション1 { get; set; }
        public int? オプション2 { get; set; }
        public int? オプション3 { get; set; }
        public int? オプション4 { get; set; }
        public int? オプション5 { get; set; }
        public int? オプション6 { get; set; }
        public int? レセプト用紙 { get; set; }
        public int? 連単 { get; set; }
        public string カルテ用紙 { get; set; }
        public int? 処方箋用紙 { get; set; }
        public string 領収書用紙 { get; set; }
        public string 領収書用紙２ { get; set; }
        public int? メディア { get; set; }
        public int? ＦＤ種 { get; set; }
        public string 納品月 { get; set; }
        public string 売上月 { get; set; }
        public int? 単体 { get; set; }
        public int? サーバー { get; set; }
        public int? クライアント { get; set; }
        public int? 販売店名 { get; set; }
        public string LicensedKey { get; set; }
        public string バージョン情報 { get; set; }
        public bool? 販売形態 { get; set; }
        public bool? 代行回収 { get; set; }
        public bool? S保守契約 { get; set; }
        public int? H保守契約 { get; set; }
        public bool? ハード構成 { get; set; }
        public bool? リース情報 { get; set; }
        public bool? 登録カード回収 { get; set; }
        public bool? 保守契約書回収 { get; set; }
        public bool? 代行回収回収 { get; set; }
        public string 改正時情報 { get; set; }
        public string 休診日 { get; set; }
        public string 診療時間 { get; set; }
        public string メールアドレス { get; set; }
        public int? ClientLicense1 { get; set; }
        public int? ClientLicense2 { get; set; }
        public int? ClientLicense3 { get; set; }
        public int? ClientLicense4 { get; set; }
        public int? ClientLicense5 { get; set; }
        public int? ClientLicense6 { get; set; }
        public int? ClientLicense7 { get; set; }
        public int? ClientLicense8 { get; set; }
        public int? ClientLicense9 { get; set; }
        public int? ClientLicense10 { get; set; }
        public int? ClientLicense11 { get; set; }
        public int? ClientLicense12 { get; set; }
        public string ＯＳ { get; set; }
        public string 登録カード回収日 { get; set; }
        public int? ODeS加入 { get; set; }
        public string 前システム名 { get; set; }
        public bool? 前システム終了 { get; set; }
        public string 備考 { get; set; }
        public string 前システム名称 { get; set; }
        public string リース店名 { get; set; }
        public string 契約No { get; set; }
        public string 期間 { get; set; }
        public string リース開始 { get; set; }
        public string リース終了 { get; set; }
        public int? リース料 { get; set; }
        public int? 残回数 { get; set; }
        public int? 残金額 { get; set; }
        public string リース契約備考 { get; set; }
        public bool S保守 { get; set; }
        public string S契約書回収年月 { get; set; }
        public int? S売計上 { get; set; }
        public int? S契約年数 { get; set; }
        public int? Sメンテ料金 { get; set; }
        public string Sメンテ契約開始 { get; set; }
        public string Sメンテ契約終了 { get; set; }
        //,NULL AS Sメンテ契約備考1
        //,NULL AS Sメンテ契約備考2
        //,NULL AS S契約名義
        //,NULL AS Sメンテ請求先コード
        //,NULL AS Sメンテ請求先名
        //,NULL AS Sメンテ区分
        //,NULL AS S卸BM先名
        //,NULL AS S金額
        //, NULL                                 as H保守                  -- 2015/10/19 変更
        //, NULL                                 as H契約書回収年月
        //, NULL                                 as H売計上
        //, NULL                                 as H契約年数
        //, NULL                                 as Hメンテ料金
        //, NULL                                 as Hメンテ契約開始
        //, NULL                                 as Hメンテ契約終了
        //, NULL                                 as Hメンテ契約備考1
        //, NULL                                 as Hメンテ契約備考2
        //, NULL                                 as H契約名義
        //, NULL                                 as Hメンテ請求先コード
        //, NULL                                 as Hメンテ請求先名
        //, NULL                                 as Hメンテ区分
        //, NULL                                 as H卸BM先名
        //, NULL                                 as H金額
        public string 代行回収APLUSコード { get; set; }
        public string 代行回収銀行名カナ { get; set; }
        public string 代行回収銀行コード { get; set; }
        public string 代行回収支店名カナ { get; set; }
        public string 代行回収支店コード { get; set; }
        public bool? 代行回収預金種別 { get; set; }
        public string 代行回収口座番号 { get; set; }
        public string 代行回収預金者名 { get; set; }
        public string 代行回収上限金額 { get; set; }
        public string 代行回収最終引落日 { get; set; }
        public string 代行回収状態 { get; set; }
        public string 代行回収備考 { get; set; }
        public bool? 適用売価No { get; set; }
        public string 請求締日 { get; set; }
        public string 請求回収日 { get; set; }
        public string 請求区分No { get; set; }
        public string 請求区分 { get; set; }
        public int? 代引配送 { get; set; }
        public string 県番号 { get; set; }
        public string 都道府県名 { get; set; }
        public string システム略称 { get; set; }
        //, NULL                                 as オンライン             -- 2016/03/24 変更
        public string レセ電算請求種別 { get; set; }
        //, NULL                                 as レセ電算確認試験
        public string レセ電算請求開始 { get; set; }
        //, NULL                                 as レセ電算オンライン提出予定フラグ
        //, NULL                                 as レセ電算インターネット利用情報
        //, NULL                                 as レセ電算利用プロバイダ
        //, NULL                                 as レセ電算利用回線
        //, NULL                                 as レセ電算回線既設場所
        //, NULL                                 as レセ電算オンライン請求PC
        //, NULL                                 as レセ電算オンライン確認試験
        //, NULL                                 as レセ電算オンライン請求開始
        //, NULL                                 as レセ電算リンク作業状況
        //, NULL                                 as レセ電算予定年月
        //, NULL                                 as レセ電算エントリーチェック
        //, NULL                                 as レセ電算受付担当者
        //, NULL                                 as レセ電算受付日
        //, NULL                                 as レセ電算作業区分
        //, NULL                                 as レセ電算発送依頼者
        //, NULL                                 as レセ電算発送日
        //, NULL                                 as レセ電算作業担当者
        //, NULL                                 as レセ電算作業完了日
        //, NULL                                 as レセ電算作業完了フラグ
        //, NULL                                 as レセ電算備考
        //, NULL                                 as レセ電算媒体
        //, NULL                                 as レセ電算オンライン
        //, NULL                                 as レセ電算請求済
        public string 営業担当者コード { get; set; }
        public string 営業担当者名 { get; set; }
        public string インスト担当者コード { get; set; }
        public string インスト担当者名 { get; set; }
        public int eStore登録フラグ { get; set; }
        public int メルマガ購読フラグ { get; set; }
        public string eStore登録メールアドレス { get; set; }
        public string eStore_パスワード { get; set; }
        public string eStore_パスワード読み { get; set; }
        public string MWS_ID { get; set; }
        public string MWS_パスワード { get; set; }
        public string MWS_パスワード読み { get; set; }
        public string MWS_申込種別 { get; set; }
        public string MWS_申込書回収日 { get; set; }
        public string MWS_販売種別 { get; set; }
        public string MWS_使用許諾期限 { get; set; }
        public string UPG時利用期限 { get; set; }
        public int? 残月数 { get; set; }
        public int? 販売店ID { get; set; }
        public string 販売店名称 { get; set; }
        public string 販売店グループコード { get; set; }
        public string 販売店グループ名称 { get; set; }
        public string 販売店区分コード { get; set; }
        public string 販売店区分名称 { get; set; }
        public string 販売店担当者名 { get; set; }
        public DateTime? 更新日時 { get; set; }
        public int 有効ユーザーフラグ { get; set; }
        public int? 同時接続クライアント数 { get; set; }

        /// <summary>
        /// 請求先が別途存在するかどうか？
        /// </summary>
        public bool Is別途請求先
        {
            get
            {
                return 0 < 請求先コード.Length;
            }
        }

        /// <summary>
        /// 顧客名の取得
        /// </summary>
        public string 顧客名
        {
            get
            {
                string name = 顧客名１;
                if (0 < 顧客名２.Length)
                {
                    name += " " + 顧客名２;
                }
                return name;
            }
        }

        /// <summary>
        /// 住所の取得
        /// </summary>
        public string 住所
        {
            get
            {
                string add = 住所１;
                if (0 < 住所２.Length)
                {
                    add += " " + 住所２;
                }
                return add;
            }
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public vMic全ユーザー2()
        {
            this.Clear();
        }

        /// <summary>
        /// クリア
        /// </summary>
        public void Clear()
        {
            担当者コード = string.Empty;
            担当者名 = string.Empty;
            支店コード = string.Empty;
            支店名 = string.Empty;
            システム名称 = string.Empty;
            終了フラグ = false;
            顧客No = 0;
            得意先No = string.Empty;
            顧客名１ = string.Empty;
            顧客名２ = string.Empty;
            フリガナ = string.Empty;
            郵便番号 = string.Empty;
            住所１ = string.Empty;
            住所２ = string.Empty;
            住所フリガナ = string.Empty;
            電話番号 = string.Empty;
            FAX番号 = string.Empty;
            売伝No = null;
            医保医療コード = string.Empty;
            国保医療コード = string.Empty;
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
            システム名 = string.Empty;
            オプション1 = null;
            オプション2 = null;
            オプション3 = null;
            オプション4 = null;
            オプション5 = null;
            オプション6 = null;
            レセプト用紙 = null;
            連単 = null;
            カルテ用紙 = string.Empty;
            処方箋用紙 = null;
            領収書用紙 = string.Empty;
            領収書用紙２ = string.Empty;
            メディア = null;
            ＦＤ種 = null;
            納品月 = string.Empty;
            売上月 = string.Empty;
            単体 = null;
            サーバー = null;
            クライアント = null;
            販売店名 = null;
            LicensedKey = string.Empty;
            バージョン情報 = string.Empty;
            販売形態 = null;
            代行回収 = null;
            S保守契約 = null;
            H保守契約 = null;
            ハード構成 = null;
            リース情報 = null;
            登録カード回収 = null;
            保守契約書回収 = null;
            代行回収回収 = null;
            改正時情報 = string.Empty;
            休診日 = string.Empty;
            診療時間 = string.Empty;
            メールアドレス = string.Empty;
            ClientLicense1 = null;
            ClientLicense2 = null;
            ClientLicense3 = null;
            ClientLicense4 = null;
            ClientLicense5 = null;
            ClientLicense6 = null;
            ClientLicense7 = null;
            ClientLicense8 = null;
            ClientLicense9 = null;
            ClientLicense10 = null;
            ClientLicense11 = null;
            ClientLicense12 = null;
            ＯＳ = string.Empty;
            登録カード回収日 = string.Empty;
            ODeS加入 = null;
            前システム名 = string.Empty;
            前システム終了 = null;
            備考 = string.Empty;
            前システム名称 = string.Empty;
            リース店名 = string.Empty;
            契約No = string.Empty;
            期間 = string.Empty;
            リース開始 = string.Empty;
            リース終了 = string.Empty;
            リース料 = null;
            残回数 = null;
            残金額 = null;
            リース契約備考 = string.Empty;
            S保守 = false;
            S契約書回収年月 = string.Empty;
            S売計上 = null;
            S契約年数 = null;
            Sメンテ料金 = null;
            Sメンテ契約開始 = string.Empty;
            Sメンテ契約終了 = string.Empty;
            代行回収APLUSコード = string.Empty;
            代行回収銀行名カナ = string.Empty;
            代行回収銀行コード = string.Empty;
            代行回収支店名カナ = string.Empty;
            代行回収支店コード = string.Empty;
            代行回収預金種別 = null;
            代行回収口座番号 = string.Empty;
            代行回収預金者名 = string.Empty;
            代行回収上限金額 = string.Empty;
            代行回収最終引落日 = string.Empty;
            代行回収状態 = string.Empty;
            代行回収備考 = string.Empty;
            適用売価No = null;
            請求締日 = string.Empty;
            請求回収日 = string.Empty;
            請求区分No = string.Empty;
            請求区分 = string.Empty;
            代引配送 = null;
            県番号 = string.Empty;
            都道府県名 = string.Empty;
            システム略称 = string.Empty;
            レセ電算請求種別 = string.Empty;
            レセ電算請求開始 = string.Empty;
            営業担当者コード = string.Empty;
            営業担当者名 = string.Empty;
            インスト担当者コード = string.Empty;
            インスト担当者名 = string.Empty;
            eStore登録フラグ = 0;
            メルマガ購読フラグ = 0;
            eStore登録メールアドレス = string.Empty;
            eStore_パスワード = string.Empty;
            eStore_パスワード読み = string.Empty;
            MWS_ID = string.Empty;
            MWS_パスワード = string.Empty;
            MWS_パスワード読み = string.Empty;
            MWS_申込種別 = string.Empty;
            MWS_申込書回収日 = string.Empty;
            MWS_販売種別 = string.Empty;
            MWS_使用許諾期限 = string.Empty;
            UPG時利用期限 = string.Empty;
            残月数 = null;
            販売店ID = null;
            販売店名称 = string.Empty;
            販売店グループコード = string.Empty;
            販売店グループ名称 = string.Empty;
            販売店区分コード = string.Empty;
            販売店区分名称 = string.Empty;
            販売店担当者名 = string.Empty;
            更新日時 = null;
            有効ユーザーフラグ = 0;
            同時接続クライアント数 = null;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<vMic全ユーザー2> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<vMic全ユーザー2> result = new List<vMic全ユーザー2>();
                foreach (DataRow row in table.Rows)
                {
                    vMic全ユーザー2 data = new vMic全ユーザー2();
                    data.担当者コード = row["担当者コード"].ToString().Trim();
                    data.担当者名 = row["担当者名"].ToString().Trim();
                    data.支店コード = row["支店コード"].ToString().Trim();
                    data.支店名 = row["支店名"].ToString().Trim();
                    data.システム名称 = row["システム名称"].ToString().Trim();
                    data.終了フラグ = ("0" == row["終了フラグ"].ToString()) ? false : true;
                    data.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
                    data.得意先No = row["得意先No"].ToString().Trim();
                    data.顧客名１ = row["顧客名１"].ToString().Trim();
                    data.顧客名２ = row["顧客名２"].ToString().Trim();
                    data.フリガナ = row["フリガナ"].ToString().Trim();
                    data.郵便番号 = row["郵便番号"].ToString().Trim();
                    data.住所１ = row["住所１"].ToString().Trim();
                    data.住所２ = row["住所２"].ToString().Trim();
                    data.住所フリガナ = row["住所フリガナ"].ToString().Trim();
                    data.電話番号 = row["電話番号"].ToString().Trim();
                    data.FAX番号 = row["FAX番号"].ToString().Trim();
                    data.売伝No = DataBaseValue.ConvObjectToIntNull(row["売伝No"]);
                    data.医保医療コード = row["医保医療コード"].ToString().Trim();
                    data.国保医療コード = row["国保医療コード"].ToString().Trim();
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
                    data.請求先住所 = row["請求先住所"].ToString().Trim();
                    data.請求先電話番号 = row["請求先電話番号"].ToString().Trim();
                    data.請求先備考 = row["請求先備考"].ToString().Trim();
                    data.システム名 = row["システム名"].ToString().Trim();
                    data.オプション1 = DataBaseValue.ConvObjectToIntNull(row["オプション1"]);
                    data.オプション2 = DataBaseValue.ConvObjectToIntNull(row["オプション2"]);
                    data.オプション3 = DataBaseValue.ConvObjectToIntNull(row["オプション3"]);
                    data.オプション4 = DataBaseValue.ConvObjectToIntNull(row["オプション4"]);
                    data.オプション5 = DataBaseValue.ConvObjectToIntNull(row["オプション5"]);
                    data.オプション6 = DataBaseValue.ConvObjectToIntNull(row["オプション6"]);
                    data.レセプト用紙 = DataBaseValue.ConvObjectToIntNull(row["レセプト用紙"]);
                    data.連単 = DataBaseValue.ConvObjectToIntNull(row["連単"]);
                    data.カルテ用紙 = row["カルテ用紙"].ToString().Trim();
                    data.処方箋用紙 = DataBaseValue.ConvObjectToIntNull(row["処方箋用紙"]);
                    data.領収書用紙 = row["領収書用紙"].ToString().Trim();
                    data.領収書用紙２ = row["領収書用紙２"].ToString().Trim();
                    data.メディア = DataBaseValue.ConvObjectToIntNull(row["メディア"]);
                    data.ＦＤ種 = DataBaseValue.ConvObjectToIntNull(row["ＦＤ種"]);
                    data.納品月 = row["納品月"].ToString().Trim();
                    data.売上月 = row["売上月"].ToString().Trim();
                    data.単体 = DataBaseValue.ConvObjectToIntNull(row["単体"]);
                    data.サーバー = DataBaseValue.ConvObjectToIntNull(row["サーバー"]);
                    data.クライアント = DataBaseValue.ConvObjectToIntNull(row["クライアント"]);
                    data.販売店名 = DataBaseValue.ConvObjectToIntNull(row["販売店名"]);
                    data.LicensedKey = row["LicensedKey"].ToString().Trim();
                    data.バージョン情報 = row["バージョン情報"].ToString().Trim();
                    data.販売形態 = ("0" == row["販売形態"].ToString()) ? false : true;
                    data.代行回収 = ("0" == row["代行回収"].ToString()) ? false : true;
                    data.S保守契約 = ("0" == row["S保守契約"].ToString()) ? false : true;
                    data.H保守契約 = DataBaseValue.ConvObjectToIntNull(row["H保守契約"]);
                    data.ハード構成 = ("0" == row["ハード構成"].ToString()) ? false : true;
                    data.リース情報 = ("0" == row["リース情報"].ToString()) ? false : true;
                    data.登録カード回収 = ("0" == row["登録カード回収"].ToString()) ? false : true;
                    data.保守契約書回収 = ("0" == row["保守契約書回収"].ToString()) ? false : true;
                    data.代行回収回収 = ("0" == row["代行回収回収"].ToString()) ? false : true;
                    data.改正時情報 = row["改正時情報"].ToString().Trim();
                    data.休診日 = row["休診日"].ToString().Trim();
                    data.診療時間 = row["診療時間"].ToString().Trim();
                    data.メールアドレス = row["メールアドレス"].ToString().Trim();
                    data.ClientLicense1 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense1"]);
                    data.ClientLicense2 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense2"]);
                    data.ClientLicense3 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense3"]);
                    data.ClientLicense4 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense4"]);
                    data.ClientLicense5 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense5"]);
                    data.ClientLicense6 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense6"]);
                    data.ClientLicense7 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense7"]);
                    data.ClientLicense8 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense8"]);
                    data.ClientLicense9 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense9"]);
                    data.ClientLicense10 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense10"]);
                    data.ClientLicense11 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense11"]);
                    data.ClientLicense12 = DataBaseValue.ConvObjectToIntNull(row["ClientLicense12"]);
                    data.ＯＳ = row["ＯＳ"].ToString();
                    data.登録カード回収日 = row["登録カード回収日"].ToString().Trim();
                    data.ODeS加入 = DataBaseValue.ConvObjectToIntNull(row["ODeS加入"]);
                    data.前システム名 = row["前システム名"].ToString().Trim();
                    data.前システム終了 = ("0" == row["前システム終了"].ToString()) ? false : true;
                    data.備考 = row["備考"].ToString().Trim();
                    data.前システム名称 = row["前システム名称"].ToString().Trim();
                    data.リース店名 = row["リース店名"].ToString().Trim();
                    data.契約No = row["契約No"].ToString().Trim();
                    data.期間 = row["期間"].ToString().Trim();
                    data.リース開始 = row["リース開始"].ToString().Trim();
                    data.リース終了 = row["リース終了"].ToString().Trim();
                    data.リース料 = DataBaseValue.ConvObjectToIntNull(row["リース料"]);
                    data.残回数 = DataBaseValue.ConvObjectToIntNull(row["残回数"]);
                    data.残金額 = DataBaseValue.ConvObjectToIntNull(row["残金額"]);
                    data.リース契約備考 = row["リース契約備考"].ToString().Trim();
                    data.S保守 = ("0" == row["S保守"].ToString()) ? false : true;
                    data.S契約書回収年月 = row["S契約書回収年月"].ToString().Trim();
                    data.S売計上 = DataBaseValue.ConvObjectToIntNull(row["S売計上"]);
                    data.S契約年数 = DataBaseValue.ConvObjectToIntNull(row["S契約年数"]);
                    data.Sメンテ料金 = DataBaseValue.ConvObjectToIntNull(row["Sメンテ料金"]);
                    data.Sメンテ契約開始 = row["Sメンテ契約開始"].ToString().Trim();
                    data.Sメンテ契約終了 = row["Sメンテ契約終了"].ToString().Trim();
                    data.代行回収APLUSコード = row["代行回収APLUSコード"].ToString().Trim();
                    data.代行回収銀行名カナ = row["代行回収銀行名カナ"].ToString().Trim();
                    data.代行回収銀行コード = row["代行回収銀行コード"].ToString().Trim();
                    data.代行回収支店名カナ = row["代行回収支店名カナ"].ToString().Trim();
                    data.代行回収支店コード = row["代行回収支店コード"].ToString().Trim();
                    data.代行回収預金種別 = ("0" == row["代行回収預金種別"].ToString()) ? false : true;
                    data.代行回収口座番号 = row["代行回収口座番号"].ToString().Trim();
                    data.代行回収預金者名 = row["代行回収預金者名"].ToString().Trim();
                    data.代行回収上限金額 = row["代行回収上限金額"].ToString().Trim();
                    data.代行回収最終引落日 = row["代行回収最終引落日"].ToString().Trim();
                    data.代行回収状態 = row["代行回収状態"].ToString().Trim();
                    data.代行回収備考 = row["代行回収備考"].ToString().Trim();
                    data.適用売価No = ("0" == row["適用売価No"].ToString()) ? false : true;
                    data.請求締日 = row["請求締日"].ToString().Trim();
                    data.請求回収日 = row["請求回収日"].ToString().Trim();
                    data.請求区分No = row["請求区分No"].ToString().Trim();
                    data.請求区分 = row["請求区分"].ToString().Trim();
                    data.代引配送 = DataBaseValue.ConvObjectToIntNull(row["代引配送"]);
                    data.県番号 = row["県番号"].ToString().Trim();
                    data.都道府県名 = row["都道府県名"].ToString().Trim();
                    data.システム略称 = row["システム略称"].ToString().Trim();
                    data.レセ電算請求種別 = row["レセ電算請求種別"].ToString().Trim();
                    data.レセ電算請求開始 = row["レセ電算請求開始"].ToString().Trim();
                    data.営業担当者コード = row["営業担当者コード"].ToString().Trim();
                    data.営業担当者名 = row["営業担当者名"].ToString().Trim();
                    data.インスト担当者コード = row["インスト担当者コード"].ToString().Trim();
                    data.インスト担当者名 = row["インスト担当者名"].ToString().Trim();
                    data.eStore登録フラグ = DataBaseValue.ConvObjectToInt(row["eStore登録フラグ"]);
                    data.メルマガ購読フラグ = DataBaseValue.ConvObjectToInt(row["メルマガ購読フラグ"]);
                    data.eStore登録メールアドレス = row["eStore登録メールアドレス"].ToString().Trim();
                    data.eStore_パスワード = row["eStore_パスワード"].ToString().Trim();
                    data.eStore_パスワード読み = row["eStore_パスワード読み"].ToString().Trim();
                    data.MWS_ID = row["MWS_ID"].ToString().Trim();
                    data.MWS_パスワード = row["MWS_パスワード"].ToString().Trim();
                    data.MWS_パスワード読み = row["MWS_パスワード読み"].ToString().Trim();
                    data.MWS_申込種別 = row["MWS_申込種別"].ToString().Trim();
                    data.MWS_申込書回収日 = row["MWS_申込書回収日"].ToString().Trim();
                    data.MWS_販売種別 = row["MWS_販売種別"].ToString().Trim();
                    data.MWS_使用許諾期限 = row["MWS_使用許諾期限"].ToString().Trim();
                    data.UPG時利用期限 = row["UPG時利用期限"].ToString().Trim();
                    data.残月数 = DataBaseValue.ConvObjectToIntNull(row["残月数"]);
                    data.販売店ID = DataBaseValue.ConvObjectToIntNull(row["販売店ID"]);
                    data.販売店名称 = row["販売店名称"].ToString().Trim();
                    data.販売店グループコード = row["販売店グループコード"].ToString().Trim();
                    data.販売店グループ名称 = row["販売店グループ名称"].ToString().Trim();
                    data.販売店区分コード = row["販売店区分コード"].ToString().Trim();
                    data.販売店区分名称 = row["販売店区分名称"].ToString().Trim();
                    data.販売店担当者名 = row["販売店担当者名"].ToString().Trim();
                    data.更新日時 = DataBaseValue.ConvObjectToDateTimeNull(row["更新日時"]);
                    data.有効ユーザーフラグ = DataBaseValue.ConvObjectToInt(row["有効ユーザーフラグ"]);
                    data.同時接続クライアント数 = DataBaseValue.ConvObjectToIntNull(row["同時接続クライアント数"]);
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
    }
}
