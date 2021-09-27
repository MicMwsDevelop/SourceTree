//
// tMih支店情報.cs
//
// 支店情報クラス
// [JunpDB].[dbo].[tMih支店情報]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/13 勝呂)
//
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.Table
{
    public class tMih支店情報
    {
        /// <summary>
        /// 
        /// </summary>
        public string fBshCode1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fBshCode2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fBshCode3 { get; set; }

        /// <summary>
        /// 支店名
        /// </summary>
        public string f支店名 { get; set; }

        /// <summary>
        /// 住所1
        /// </summary>
        public string f住所1 { get; set; }

        /// <summary>
        /// 住所2
        /// </summary>
        public string f住所2 { get; set; }

        /// <summary>
        /// TEL
        /// </summary>
        public string f電話番号 { get; set; }

        /// <summary>
        /// FAX
        /// </summary>
        public string fファックス番号 { get; set; }

        /// <summary>
        /// PCA部門コード
        /// </summary>
        public short fPca部門コード { get; set; }

        /// <summary>
        /// 担当者コード
        /// </summary>
        public string f担当者コード { get; set; }

        /// <summary>
        /// 郵便番号
        /// </summary>
        public string f郵便番号 { get; set; }

        /// <summary>
        /// PCA倉庫コード
        /// </summary>
        public short fPca倉庫コード { get; set; }

        /// <summary>
        /// 支店コード
        /// </summary>
        public string f支店コード { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        public string fメールアドレス { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        public short f表示順 { get; set; }

        /// <summary>
        /// 住所
        /// </summary>
        public string 住所
        {
            get
            {
                return f住所1 + f住所2;
            }
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public tMih支店情報()
        {
            fBshCode1 = string.Empty;
            fBshCode2 = string.Empty;
            fBshCode3 = string.Empty;
            f支店名 = string.Empty;
            f住所1 = string.Empty;
            f住所2 = string.Empty;
            f電話番号 = string.Empty;
            fファックス番号 = string.Empty;
            fPca部門コード = 0;
            f担当者コード = string.Empty;
            f郵便番号 = string.Empty;
            fPca倉庫コード = 0;
            f支店コード = string.Empty;
            fメールアドレス = string.Empty;
            f表示順 = 0;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<tMih支店情報> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<tMih支店情報> result = new List<tMih支店情報>();
                foreach (DataRow row in table.Rows)
                {
                    tMih支店情報 data = new tMih支店情報
                    {
                        fBshCode1 = row["fBshCode1"].ToString().Trim(),
                        fBshCode2 = row["fBshCode2"].ToString().Trim(),
                        fBshCode3 = row["fBshCode3"].ToString().Trim(),
                        f支店名 = row["f支店名"].ToString().Trim(),
                        f住所1 = row["f住所１"].ToString().Trim(),
                        f住所2 = row["f住所２"].ToString().Trim(),
                        f電話番号 = row["f電話番号"].ToString().Trim(),
                        fファックス番号 = row["fファックス番号"].ToString().Trim(),
                        fPca部門コード = (short)DataBaseValue.ConvObjectToInt(row["fPca部門コード"]),
                        f担当者コード = row["f担当者コード"].ToString().Trim(),
                        f郵便番号 = row["f郵便番号"].ToString().Trim(),
                        fPca倉庫コード = (short)DataBaseValue.ConvObjectToInt(row["fPca倉庫コード"]),
                        f支店コード = row["f支店コード"].ToString().Trim(),
                        fメールアドレス = row["fメールアドレス"].ToString().Trim(),
                        f表示順 = (short)DataBaseValue.ConvObjectToInt(row["f表示順"]),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }
    }
}
