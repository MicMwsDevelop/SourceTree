//
// PrintParaList.cs
// 
// パラメタファイル印刷リストクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MwsLib.Print
{
	/// <summary>
	/// パラメタファイル印刷クラス
	/// </summary>
	public class PrintParaList : IEquatable<PrintParaList>
    {
        /// <summary>
        /// 印刷パラメータリスト
        /// </summary>
        public List<PrintPara> ParaList { private set; get; }

        /// <summary>
        /// パラメタファイル名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 用紙名称
        /// </summary>
        public string PaperName { get; set; }

        /// <summary>
        /// 用紙種類
        /// </summary>
        public PaperKind PaperKind { get; set; }

        /// <summary>
        /// C++互換 - 順次移行予定
        /// </summary>
        public bool Interchange { get; set; }

        /// <summary>
        /// 用紙の横幅
        /// </summary>
        private int paperWidth { get; set; }

        /// <summary>
        /// 用紙の縦幅
        /// </summary>
        private int paperHeight { get; set; }

        /// <summary>
        /// 用紙方向が水平方向かどうか？
        /// </summary>
        public bool Landscape { get; set; }

        /// <summary>
        /// デフォルトフォント名称
        /// </summary>
        public string DefaultFontName { get; set; }

        /// <summary>
        /// 用紙色
        /// </summary>
        public Color PaperColor { get; set; }

        /// <summary>
        /// 用紙の長さを取得(用紙サイズがユーザー定義時のみ)
        /// </summary>
        /// <returns>用紙の長さ</returns>
        public int PaperLength
        {
            get 
            {
                return (PaperKind.Custom == PaperKind) ? paperHeight : 0;
            }
        }

        /// <summary>
        /// 用紙の幅を取得(用紙サイズがユーザー定義時のみ)
        /// </summary>
        /// <returns>用紙の幅</returns>
        public int PaperWidth
        {
            get
            {
                return (PaperKind.Custom == PaperKind) ? paperWidth : 0;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PrintParaList(bool interchange = false)
        {
            Empty();
            this.Interchange = interchange;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Empty()
        {
            this.ParaList = new List<PrintPara>();

            this.FileName = string.Empty;
            this.PaperName = string.Empty;
            this.PaperKind = PaperKind.Custom;
            this.paperWidth = 0;
            this.paperHeight = 0;
            this.Landscape = false;
            this.DefaultFontName = "ＭＳ ゴシック";
            this.PaperColor = Color.Black;
            //this.Interchange = false;
        }

        /// <summary>
        /// 空データかどうかを返す
        /// </summary>
        /// <returns>true：空データ、flase：データあり</returns>
        public bool IsEmpty()
        {
            return this.Equals(new PrintParaList());
        }

        /// <summary>
        /// 内容一致確認
        /// </summary>
        /// <param name="other">比較情報</param>
        /// <returns>true：一致、false：不一致</returns>
        public bool Equals(PrintParaList other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.ParaList.SequenceEqual(other.ParaList)
                && this.FileName == other.FileName
                && this.PaperName == other.PaperName
                && this.PaperKind == other.PaperKind
                && this.paperWidth == other.paperWidth
                && this.paperHeight == other.paperHeight
                && this.Landscape == other.Landscape
                && this.DefaultFontName == other.DefaultFontName
                && this.PaperColor == other.PaperColor
                && this.Interchange == other.Interchange)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 一致確認
        /// </summary>
        /// <param name="obj">比較対象オブジェクト</param>
        /// <returns>true:一致, false:不一致</returns>
        public override bool Equals(object obj)
        {
            if (obj is PrintParaList)
            {
                return Equals((PrintParaList)obj);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode()
        {
            int code = ParaList.GetHashCode() ^ FileName.GetHashCode() ^ PaperName.GetHashCode()
                        ^ PaperKind.GetHashCode() ^ paperWidth.GetHashCode() ^ paperHeight.GetHashCode()
                        ^ Landscape.GetHashCode() ^ DefaultFontName.GetHashCode() ^ PaperColor.GetHashCode()
                        ^ Interchange.GetHashCode();
            return code;
        }


        /// <summary>
        /// コメント行か？
        /// </summary>
        /// <param name="str">印字定義文字列</param>
        /// <returns>判定</returns>
        public bool IsCommentLine(string str)
        {
            if (0 < str.Length)
            {
                if (';' == str[0] || "REM" == str.Substring(0, 3))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// セクション行か？
        /// </summary>
        /// <param name="str">印字定義文字列</param>
        /// <returns>判定</returns>
        public bool IsSectionLine(string str)
        {
            if (0 < str.Length)
            {
                if ('[' == str[0] && ']' == str[str.Length - 1])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// パラメタデータの追加
        /// </summary>
        /// <param name="str">印字定義文字列</param>
        public void SetParameterData(string str)
        {
            if (0 < str.Length)
            {
                int len = str.IndexOf("=");
                if (0 < len)
                {
                    string entry = str.Substring(0, len).Trim(StringUtil.DefalutTrimCharSet);
                    string data = str.Substring(len + 1).Trim(StringUtil.DefalutTrimCharSet);
                    if ("<PaperName>" == entry)
                    {
                        // 用紙名称
                        PaperName = data;
                    }
                    else if ("<PaperSize>" == entry)
                    {
                        // 用紙サイズ
                        PaperKind = ConvertPaperKind(data);
                    }
                    else if ("<PaperLength>" == entry)
                    {
                        // 用紙の長さ
                        paperHeight = int.Parse(data);
                    }
                    else if ("<PaperWidth>" == entry)
                    {
                        // 用紙の幅
                        paperWidth = int.Parse(data);
                    }
                    else if ("<PaperOrientation>" == entry)
                    {
                        // 用紙方向
                        Landscape = (1 == int.Parse(data)) ? false : true;
                    }
                    else if ("<DefaultFontName>" == entry)
                    {
                        // デフォルトフォント
                        DefaultFontName = data;
                    }
                    else if ("<PaperColor>" == entry)
                    {
                        // 用紙色
                        string[] split = data.Split(',');
                        if (3 == split.Count())
                        {
                            PaperColor = Color.FromArgb(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
                        }
                        else
                        {
                            PaperColor = Color.Black;
                        }
                    }
                    else
                    {
                        // その他
                        PrintPara para = new PrintPara(DefaultFontName, str, entry, data, this.Interchange);
                        this.ParaList.Add(para);
                    }
                }
            }
        }

        /// <summary>
        /// セクション名の取得
        /// </summary>
        /// <param name="str">印字定義文字列</param>
        /// <returns>セクション名</returns>
        public string GetSectionString(string str)
        {
            if (this.IsSectionLine(str))
            {
                return str.Substring(1, str.Length - 2);
            }
            return "";
        }

        /// <summary>
        /// 指定されたセクションのエントリ名とデータのリストを取得
        /// </summary>
        /// <param name="path">パラメタファイルパス</param>
        /// <param name="filename">パラメタファイル名称</param>
        /// <param name="section">セクション</param>
        /// <param name="list">エントリ名とデータのリスト</param>
        /// <returns>エントリ名とデータのリスト</returns>
        public int GetEntryDataList(string path, string filename, string section, out List<string> list)
        {
            list = new List<string>();
            FileName = string.Empty;
            string pathname = Path.Combine(path, filename);

            if (File.Exists(pathname))
            {
                try
                {
                    // テキストファイルの読み込み
                    using (StreamReader textfile = new StreamReader(pathname, Encoding.GetEncoding("Shift_JIS")))
                    {
                        FileName = filename;
                        string target_section = string.Empty;
                        string line = textfile.ReadLine();
                        while (null != line)
                        {
                            line = line.Trim(StringUtil.DefalutTrimCharSet);
                            if (!IsCommentLine(line))
                            {
                                // コメント行以外
                                if (0 == target_section.Length)
                                {
                                    if (IsSectionLine(line))
                                    {
                                        // カレントセクションの更新
                                        string work = line.Substring(1, line.Length - 2);
                                        if (section == work)
                                        {
                                            target_section = work;
                                        }
                                        line = textfile.ReadLine();
                                        continue;
                                    }
                                }
                                else
                                {
                                    if (IsSectionLine(line))
                                    {
                                        break;
                                    }
                                    list.Add(line);
                                }
                            }
                            line = textfile.ReadLine();
                        }
                        textfile.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
            else
            {
                MessageBox.Show(pathname + "が存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return list.Count;
        }

        /// <summary>
        /// パラメタファイルの読み込み
        /// セクション名が指定されているときには、指定されたセクションのパラメタのみ読み込む
        /// </summary>
        /// <param name="path">パラメタファイルパス</param>
        /// <param name="filename">パラメタファイル名称</param>
        /// <param name="section">セクション</param>
        /// <param name="msg">エラーメッセージ</param>
        /// <returns>判定</returns>
        public bool ReadParameterFile(string path, string filename, string section, out string msg)
        {
            msg = string.Empty;
            this.ParaList.Clear();

            FileName = string.Empty;
            string pathname = Path.Combine(path, filename);
            if (File.Exists(pathname))
            {
                try
                {
                    // テキストファイルの読み込み
                    using (StreamReader textfile = new StreamReader(pathname, System.Text.Encoding.GetEncoding("Shift_JIS")))
                    {
                        FileName = pathname;
                        string current_section = string.Empty;
                        string line = textfile.ReadLine();
                        while (null != line)
                        {
                            line = line.Trim(StringUtil.DefalutTrimCharSet);
                            if (!IsCommentLine(line))
                            {
                                // コメント行以外
                                if (0 < section.Length)
                                {
                                    if (IsSectionLine(line))
                                    {
                                        // カレントセクションの更新
                                        current_section = line.Substring(1, line.Length - 2);
                                    }
                                    else
                                    {
                                        if (section == current_section)
                                        {
                                            // 同じセクション
                                            SetParameterData(line);
                                        }
                                    }
                                }
                                else
                                {
                                    SetParameterData(line);
                                }
                            }
                            line = textfile.ReadLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.ToString();
                    return false;
                }
            }
            else
            {
                msg = pathname + "が存在しません。";
                return false;
            }
            return true;
        }
        
         /// <summary>
        /// 用紙名称を設定
        /// </summary>
        /// <param name="str">用紙サイズ文字列</param>
        /// <returns>用紙名称</returns>
        public static PaperKind ConvertPaperKind(string str)
        {
            if ("A4" == str)
            {
                return PaperKind.A4;		// A4 210 x 297 mm
            }
            if ("A5" == str)
            {
                return PaperKind.A5;		// A5 148 x 210 mm
            }
            if ("B4" == str)
            {
                return PaperKind.B4;		// B4 (JIS) 250 x 354
            }
            if ("B5" == str)
            {
                return PaperKind.B5;		// B5 (JIS) 182 x 257 mm
            }
            if ("B6" == str)
            {
                return PaperKind.B6Jis;	    // B6 (JIS) 128 x 182 mm
            }
            if ("USER" == str)
            {
                return PaperKind.Custom;	// ユーザー定義
            }
            return PaperKind.Custom;		// ユーザー定義
        }

        /// <summary>
        /// 指定されたセクションが存在するか？
        /// </summary>
        /// <param name="pathname">パラメタファイル名称</param>
        /// <param name="section">セクション</param>
        /// <returns>判定</returns>
        public bool IsExistSection(string pathname, string section)
        {
            if (File.Exists(pathname))
            {
                try
                {
                    // テキストファイルの読み込み
                    using (StreamReader textfile = new StreamReader(pathname, System.Text.Encoding.GetEncoding("Shift_JIS")))
                    {
                        string line = textfile.ReadLine();
                        while (null != line)
                        {
                            line = line.Trim(StringUtil.DefalutTrimCharSet);
                            if (!IsCommentLine(line))
                            {
                                // コメント行以外
                                if (section == GetSectionString(line))
                                {
                                    textfile.Close();
                                    return true;
                                }
                            }
                            line = textfile.ReadLine();
                        }
                        textfile.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show(pathname + "が存在しません。");
            }
            return false;
        }
    }
}
