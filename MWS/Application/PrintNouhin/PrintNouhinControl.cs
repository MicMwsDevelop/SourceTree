//
// MIC 納品書印刷制御クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/02/14 勝呂)
// 
using MwsLib.Common;
using MwsLib.Print;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PrintNouhin
{
	/// <summary>
	/// MIC 納品書印刷制御クラス
	/// </summary>
	public class PrintNouhinControl
	{
		/// <summary>
		/// 納品書パラメタファイル名称
		/// </summary>
		public const string PARAMETER_FILENAME = "NOUHIN_01.PRM";

		/// <summary>
		/// セクション名
		/// </summary>
		private const string SECTION_NAME = "納品書";

		/// <summary>
		/// 品名印刷可能数
		/// </summary>
		public const int PRINT_GOODS_MAX = 10;

		/// <summary>
		/// 印刷パラメータリスト
		/// </summary>
		public PrintParaList ParameterList { private set; get; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PrintNouhinControl()
		{
			ParameterList = new PrintParaList();
		}

		/// <summary>
		/// MIC 納品書の印刷パラメタファイルの読込み
		/// </summary>
		/// <param name="type">用紙種別</param>
		/// <param name="msg">エラーメッセージ文字列</param>
		/// <returns>読込み行数</returns>
		public int ReadParameterFile(string path, string filename, out string msg)
		{
			if (false == ParameterList.ReadParameterFile(path, filename, SECTION_NAME, out msg))
			{
				// パラメータ読み込みエラー
				return -1;
			}
			return ParameterList.ParaList.Count;
		}

		/// <summary>
		/// 文字列から数字のみ抽出
		/// </summary>
		/// <param name="str">文字列</param>
		/// <returns>数字</returns>
		private int ExtractionNumeral(string str)
		{
			string strDecimal = Regex.Replace(str, @"[^0-9]", "");
			return int.Parse(strDecimal);
		}

		/// <summary>
		/// カンマ数字文字列の取得（\付き）
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		private string CommaEditString(string str)
		{
			if (0 < str.Length)
			{
				return @"\" + StringUtil.CommaEdit(str);
			}
			return string.Empty;
		}

		/// <summary>
		/// MIC 納品書の印刷
		/// </summary>
		/// <param name="dataList">印刷データリスト</param>
		/// <param name="printIndex">印刷部</param>
		/// <param name="g">Graphics</param>
		/// <param name="offset">印刷オフセット</param>
		/// <param name="curPage">カレントページ</param>
		/// <param name="printRect">矩形印字の有無</param>
		public void PrintNouhinData(NouhinDataList dataList, int printIndex, Graphics g, Point offset, int curPage, bool printRect)
		{
			Color formColor = Color.Black;
			int maxPage = dataList.GetMaxPage();

			foreach (PrintPara para in ParameterList.ParaList)
			{
				//  最初のページのみ
				if ("PF" == StringUtil.Left(para.Entry, 2))
				{
					if (1 != curPage)
					{
						// １ページ目以外を印刷中？ -> 次の印字項目へ
						continue;
					}
					// ページ制御文字を除いた印字項目エントリー名
					para.Entry = StringUtil.Mid(para.Entry, 2);
				}
				//  最終ページのみ
				else if ("PL" == StringUtil.Left(para.Entry, 2))
				{
					if (curPage != maxPage)
					{
						// 最終ページ以外を印刷中？ ->  次の印字項目へ
						continue;
					}
					// ページ制御文字を除いた印字項目エントリー名
					para.Entry = StringUtil.Mid(para.Entry, 2);
				}

				switch (para.GetPrintParaType())
				{
					// 線
					case PrintParaDef.PrintParaType.Line:
						para.PrintLine(g, offset, formColor);
						break;
					// 破線
					case PrintParaDef.PrintParaType.DotLine:
						para.PrintDotLine(g, offset, formColor);
						break;
					// 円
					case PrintParaDef.PrintParaType.Ellipse:
						if (printRect)
						{
							// 矩形印刷
							para.PrintFrame(g, offset, Color.Red);
						}
						para.PrintEllipse(g, offset, formColor);
						break;
					// 枠
					case PrintParaDef.PrintParaType.Frame:
						para.PrintFrame(g, offset, formColor);
						break;
					// 丸枠
					case PrintParaDef.PrintParaType.RoundFrame:
						if (printRect)
						{
							// 矩形印刷
							para.PrintFrame(g, offset, Color.Red);
						}
						para.PrintRoundFrame(g, offset, new Point(20, 20), formColor);
						break;
					// 塗りつぶし
					case PrintParaDef.PrintParaType.FillBox:
						para.PrintFillBox(g, offset);
						break;
					// 短形指定塗りつぶし
					case PrintParaDef.PrintParaType.FillColorBox:
						para.PrintFillColorBox(g, offset);
						break;
					// 短形指定塗りつぶし
					case PrintParaDef.PrintParaType.FillColorRoundBox:
						para.PrintFillColorRoundFrame(g, offset, new Point(20, 20), formColor);
						break;
					// 文字列
					case PrintParaDef.PrintParaType.String:
						if (printRect)
						{
							// 矩形印刷
							para.PrintFrame(g, offset, Color.Red);
						}
						para.PrintString(g, offset, para.Entry, formColor);
						break;
					// 特殊エントリ
					case PrintParaDef.PrintParaType.Special:
						if (printRect)
						{
							// 矩形印刷
							para.PrintFrame(g, offset, Color.Red);
						}
						// 通常印刷
						switch (para.Entry)
						{
							case "<お客様コードNo>":
								para.PrintString(g, offset, dataList[0].TokuisakiNo);
								break;
							case "<受注顧客No>":
								para.PrintString(g, offset, string.Format("受注情報 No  {0}", dataList[0].TokuisakiNo));
								break;
							case "<No>":
								para.PrintString(g, offset, printIndex.ToString());
								break;
							case "<郵便番号>":
								para.PrintString(g, offset, dataList[0].Zipcode);
								break;
							case "<年>":
								para.PrintString(g, offset, dataList[0].Year);
								break;
							case "<月>":
								para.PrintString(g, offset, dataList[0].Month);
								break;
							case "<日>":
								para.PrintString(g, offset, dataList[0].Day);
								break;
							case "<住所>":
								para.PrintString(g, offset, dataList[0].Address);
								break;
							case "<医院名>":
								para.PrintString(g, offset, dataList[0].ClinicName);
								break;
							case "<電話番号>":
								para.PrintString(g, offset, dataList[0].Tel);
								break;
							case "<担当>":
								para.PrintString(g, offset, dataList[0].Tanto);
								break;
							case "<合計>":
								para.PrintString(g, offset, CommaEditString(dataList[0].Total));
								break;
							default:
								if ("<品名" == StringUtil.Left(para.Entry, 3))
								{
									int line = ExtractionNumeral(para.Entry);
									int start = dataList.GetStartIndex(curPage);
									if (start + line <= dataList.Count)
									{
										para.PrintString(g, offset, dataList[start + line - 1].GoodsName);
									}
								}
								else if ("<数量" == StringUtil.Left(para.Entry, 3))
								{
									int line = ExtractionNumeral(para.Entry);
									int start = dataList.GetStartIndex(curPage);
									if (start + line <= dataList.Count)
									{
										if (0 < dataList[start + line - 1].Count.Length && "0" != dataList[start + line - 1].Count)
										{
											para.PrintString(g, offset, dataList[start + line - 1].Count);
										}
									}
								}
								else if ("<単価" == StringUtil.Left(para.Entry, 3))
								{
									int line = ExtractionNumeral(para.Entry);
									int start = dataList.GetStartIndex(curPage);
									if (start + line <= dataList.Count)
									{
										if (0 < dataList[start + line - 1].Tanka.Length && "0" != dataList[start + line - 1].Tanka)
										{
											para.PrintString(g, offset, CommaEditString(dataList[start + line - 1].Tanka));
										}
									}
								}
								else if ("<金額" == StringUtil.Left(para.Entry, 3))
								{
									int line = ExtractionNumeral(para.Entry);
									int start = dataList.GetStartIndex(curPage);
									if (start + line <= dataList.Count)
									{
										para.PrintString(g, offset, CommaEditString(dataList[start + line - 1].Price));
									}
								}
								break;
						}
						break;
					default:
						para.PrintString(g, offset, para.Entry);
						break;
				}
			}
		}
	}
}
