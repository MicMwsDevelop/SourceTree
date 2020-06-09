//
// vMicPCA受注明細.cs
//
// PCA受注明細情報クラス
// [JunpDB].[dbo].[vMicPCA受注明細]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using MwsLib.Common;
using MwsLib.DB;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace MwsLib.BaseFactory.Junp.View
{
    public class vMicPCA受注明細
    {
        /// <summary>
        /// 出荷済フラグ
        /// </summary>
        public short jucd_flg { get; set; }

        /// <summary>
        /// 受注日
        /// </summary>
        public int jucd_jucbi { get; set; }

        /// <summary>
        /// 納期
        /// </summary>
        public int jucd_noki { get; set; }

        /// <summary>
        /// 受注No
        /// </summary>
        public int jucd_jno { get; set; }

        /// <summary>
        /// 得意先コード
        /// </summary>
        public string jucd_tcd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jucd_mno { get; set; }

        /// <summary>
        /// 部門コード
        /// </summary>
        public string jucd_jbmn { get; set; }

        /// <summary>
        /// 担当者コード
        /// </summary>
        public string jucd_jtan { get; set; }

        /// <summary>
        /// 得意先部門コード
        /// </summary>
        public string jucd_tbmn { get; set; }

        /// <summary>
        /// 得意先担当者コード
        /// </summary>
        public string jucd_ttan { get; set; }

        /// <summary>
        /// 摘要コード
        /// </summary>
        public string jucd_tekcd { get; set; }

        /// <summary>
        /// 摘要名
        /// </summary>
        public string jucd_tekmei { get; set; }

        /// <summary>
        /// レコードシーケンス番号(0origin)
        /// </summary>
        public short jucd_eda { get; set; }

        /// <summary>
        /// 商品コード
        /// </summary>
        public string jucd_scd { get; set; }

        /// <summary>
        /// マスター区分
        /// 0:一般商品、1:雑商品、2:諸雑 費、3:値引、4:記事
        /// </summary>
        public short jucd_mkbn { get; set; }

        /// <summary>
        /// 税区分
        /// 0:非課税、1～9:会社基本情報の税 率ﾃｰﾌﾞﾙ
        /// </summary>
        public short jucd_tax { get; set; }

        /// <summary>
        ///  税込区分
        ///  0:税抜き、1:税込み 
        /// </summary>
        public short jucd_komi { get; set; }

        public short jucd_tketa { get; set; }
        public short jucd_sketa { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string jucd_mei { get; set; }

        /// <summary>
        /// 入数
        /// </summary>
        public int jucd_iri { get; set; }

        /// <summary>
        /// 箱数
        /// </summary>
        public int jucd_hako { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int jucd_suryo { get; set; }

        /// <summary>
        /// 単位
        /// </summary>
        public string jucd_tani { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        public int jucd_tanka { get; set; }

        /// <summary>
        /// 原単価
        /// </summary>
        public int jucd_gentan { get; set; }

        /// <summary>
        /// 受注金額
        /// </summary>
        public int jucd_kingaku { get; set; }

        /// <summary>
        /// 受注残金額
        /// </summary>
        public int jucd_zan { get; set; }

        /// <summary>
        /// 原価金額
        /// </summary>
        public int jucd_genka { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        public string jucd_biko { get; set; }
        public short jucd_nmark { get; set; }

        /// <summary>
        /// 標準価格
        /// </summary>
        public int jucd_hyo { get; set; }

        /// <summary>
        /// レコード番号(1origin)
        /// </summary>
        public int jucd_seq { get; set; }

        /// <summary>
        /// 出荷累計
        /// </summary>
        public int jucd_ruikei { get; set; }

        /// <summary>
        /// 直送先コード
        /// </summary>
        public string jucd_ncd { get; set; }

        /// <summary>
        /// 倉庫コード
        /// </summary>
        public int jucd_souko { get; set; }

        /// <summary>
        /// 売単価
        /// </summary>
        public int jucd_baitan { get; set; }

        /// <summary>
        /// 売価金額
        /// </summary>
        public int jucd_baika { get; set; }
        public int jucd_hid { get; set; }

        /// <summary>
        /// 受注残数の取得
        /// 受注残数 = 受注数 - 出荷済数
        /// </summary>
        public int 受注残数
        {
            get
            {
                return jucd_suryo - jucd_ruikei;
            }
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public vMicPCA受注明細()
        {
            this.Clear();
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public vMicPCA受注明細(vMicPCA受注明細 src)
        {
            this.Copy(src);
        }

        /// <summary>
        /// クリア
        /// </summary>
        public virtual void Clear()
        {
            jucd_flg = 0;
            jucd_jucbi = 0;
            jucd_noki = 0;
            jucd_jno = 0;
            jucd_tcd = string.Empty;
            jucd_mno = 0;
            jucd_jbmn = string.Empty;
            jucd_jtan = string.Empty;
            jucd_tbmn = string.Empty;
            jucd_ttan = string.Empty;
            jucd_tekcd = string.Empty;
            jucd_tekmei = string.Empty;
            jucd_eda = 0;
            jucd_scd = string.Empty;
            jucd_mkbn = 0;
            jucd_tax = 0;
            jucd_komi = 0;
            jucd_tketa = 0;
            jucd_sketa = 0;
            jucd_mei = string.Empty;
            jucd_iri = 0;
            jucd_hako = 0;
            jucd_suryo = 0;
            jucd_tani = string.Empty;
            jucd_tanka = 0;
            jucd_gentan = 0;
            jucd_kingaku = 0;
            jucd_zan = 0;
            jucd_genka = 0;
            jucd_biko = string.Empty;
            jucd_nmark = 0;
            jucd_hyo = 0;
            jucd_seq = 0;
            jucd_ruikei = 0;
            jucd_ncd = string.Empty;
            jucd_souko = 0;
            jucd_baitan = 0;
            jucd_baika = 0;
            jucd_hid = 0;
        }

        /// <summary>
        /// コピー関数
        /// </summary>
        /// <param name="other"></param>
        public virtual void Copy(vMicPCA受注明細 other)
        {
            jucd_flg = other.jucd_flg;
            jucd_jucbi = other.jucd_jucbi;
            jucd_noki = other.jucd_noki;
            jucd_jno = other.jucd_jno;
            jucd_tcd = other.jucd_tcd;
            jucd_mno = other.jucd_mno;
            jucd_jbmn = other.jucd_jbmn;
            jucd_jtan = other.jucd_jtan;
            jucd_tbmn = other.jucd_tbmn;
            jucd_ttan = other.jucd_ttan;
            jucd_tekcd = other.jucd_tekcd;
            jucd_tekmei = other.jucd_tekmei;
            jucd_eda = other.jucd_eda;
            jucd_scd = other.jucd_scd;
            jucd_mkbn = other.jucd_mkbn;
            jucd_tax = other.jucd_tax;
            jucd_komi = other.jucd_komi;
            jucd_tketa = other.jucd_tketa;
            jucd_sketa = other.jucd_sketa;
            jucd_mei = other.jucd_mei;
            jucd_iri = other.jucd_iri;
            jucd_hako = other.jucd_hako;
            jucd_suryo = other.jucd_suryo;
            jucd_tani = other.jucd_tani;
            jucd_tanka = other.jucd_tanka;
            jucd_gentan = other.jucd_gentan;
            jucd_kingaku = other.jucd_kingaku;
            jucd_zan = other.jucd_zan;
            jucd_genka = other.jucd_genka;
            jucd_biko = other.jucd_biko;
            jucd_nmark = other.jucd_nmark;
            jucd_hyo = other.jucd_hyo;
            jucd_seq = other.jucd_seq;
            jucd_ruikei = other.jucd_ruikei;
            jucd_ncd = other.jucd_ncd;
            jucd_souko = other.jucd_souko;
            jucd_baitan = other.jucd_baitan;
            jucd_baika = other.jucd_baika;
            jucd_hid = other.jucd_hid;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<vMicPCA受注明細> DataTableToList(DataTable table)
        {
            List<vMicPCA受注明細> result = new List<vMicPCA受注明細>();
            if (null != table && 0 < table.Rows.Count)
            {
                foreach (DataRow row in table.Rows)
                {
                    vMicPCA受注明細 data = new vMicPCA受注明細
                    {
                        jucd_jucbi = DataBaseValue.ConvObjectToInt(row["jucd_jucbi"]),
                        jucd_noki = DataBaseValue.ConvObjectToInt(row["jucd_noki"]),
                        jucd_jno = DataBaseValue.ConvObjectToInt(row["jucd_jno"]),
                        jucd_tcd = row["jucd_tcd"].ToString().Trim(),
                        jucd_mno = DataBaseValue.ConvObjectToInt(row["jucd_mno"]),
                        jucd_jbmn = row["jucd_jbmn"].ToString().Trim(),
                        jucd_jtan = row["jucd_jtan"].ToString().Trim(),
                        jucd_tbmn = row["jucd_tbmn"].ToString().Trim(),
                        jucd_ttan = row["jucd_ttan"].ToString().Trim(),
                        jucd_tekcd = row["jucd_tekcd"].ToString().Trim(),
                        jucd_tekmei = row["jucd_tekmei"].ToString().Trim(),
                        jucd_eda = DataBaseValue.ConvObjectToShort(row["jucd_eda"]),
                        jucd_scd = row["jucd_scd"].ToString().Trim(),
                        jucd_mkbn = DataBaseValue.ConvObjectToShort(row["jucd_mkbn"]),
                        jucd_tax = DataBaseValue.ConvObjectToShort(row["jucd_tax"]),
                        jucd_komi = DataBaseValue.ConvObjectToShort(row["jucd_komi"]),
                        jucd_tketa = DataBaseValue.ConvObjectToShort(row["jucd_tketa"]),
                        jucd_sketa = DataBaseValue.ConvObjectToShort(row["jucd_sketa"]),
                        jucd_mei = row["jucd_mei"].ToString().Trim(),
                        jucd_iri = DataBaseValue.ConvObjectToInt(row["jucd_iri"]),
                        jucd_hako = DataBaseValue.ConvObjectToInt(row["jucd_hako"]),
                        jucd_suryo = DataBaseValue.ConvObjectToInt(row["jucd_suryo"]),
                        jucd_tani = row["jucd_tani"].ToString().Trim(),
                        jucd_tanka = DataBaseValue.ConvObjectToInt(row["jucd_tanka"]),
                        jucd_gentan = DataBaseValue.ConvObjectToInt(row["jucd_gentan"]),
                        jucd_kingaku = DataBaseValue.ConvObjectToInt(row["jucd_kingaku"]),
                        jucd_zan = DataBaseValue.ConvObjectToInt(row["jucd_zan"]),
                        jucd_genka = DataBaseValue.ConvObjectToInt(row["jucd_genka"]),
                        jucd_biko = row["jucd_biko"].ToString().Trim(),
                        jucd_nmark = DataBaseValue.ConvObjectToShort(row["jucd_nmark"]),
                        jucd_hyo = DataBaseValue.ConvObjectToInt(row["jucd_hyo"]),
                        jucd_seq = DataBaseValue.ConvObjectToInt(row["jucd_seq"]),
                        jucd_ruikei = DataBaseValue.ConvObjectToInt(row["jucd_ruikei"]),
                        jucd_ncd = row["jucd_ncd"].ToString().Trim(),
                        jucd_souko = DataBaseValue.ConvObjectToInt(row["jucd_souko"]),
                        jucd_baitan = DataBaseValue.ConvObjectToInt(row["jucd_baitan"]),
                        jucd_baika = DataBaseValue.ConvObjectToInt(row["jucd_baika"]),
                        jucd_hid = DataBaseValue.ConvObjectToInt(row["jucd_hid"])
                    };
                    result.Add(data);
                }
            }
            return result;
        }

        /// <summary>
        /// DataTable → グループリスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<vMicPCA受注明細> DataTableToGroupList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<vMicPCA受注明細> result = new List<vMicPCA受注明細>();
                foreach (DataRow row in table.Rows)
                {
                    vMicPCA受注明細 data = new vMicPCA受注明細
                    {
                        jucd_jucbi = DataBaseValue.ConvObjectToInt(row["jucd_jucbi"]),
                        jucd_jno = DataBaseValue.ConvObjectToInt(row["jucd_jno"]),
                        jucd_tcd = row["jucd_tcd"].ToString().Trim(),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// 着日を取得
        /// </summary>
        /// <returns>着日</returns>
        public Date? ArrivalDate(Date today)
        {
            if (0 < jucd_mei.Length)
            {
                if (jucd_mei.Contains("着日指定："))
                {
                    string work = jucd_mei.Replace("着日指定：", "");
                    work = work.Replace("日", "月");
                    string[] array = Regex.Split(work, "月");
                    if (2 <= array.Length)
                    {
                        int mm = StringUtil.ToInt(array[0]);
                        int dd = StringUtil.ToInt(array[1]);
                        if (0 < mm && 0 < dd)
                        {
                            Date arraivalDate = new Date(today.Year, mm, dd);
                            if (arraivalDate < today)
                            {
                                return arraivalDate.PlusYears(1);
                            }
                            return arraivalDate;
                        }
                    }
                }
            }
            return null;
        }
    }
}
