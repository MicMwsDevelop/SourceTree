//
// 配送センター納品書印刷制御クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/10/25 勝呂)
// 
using CommonLib.Common;
using CommonLib.Print;
using System;
using System.Drawing;

namespace DeliveryCenterPrintNouhin.Print
{
	/// <summary>
	/// 配送センター納品書印刷制御クラス
	/// </summary>
	public class PrintNouhinControl
	{
		/// <summary>
		/// セクション名
		/// </summary>
		private readonly string SECTION_NAME = "納品書";

		/// <summary>
		/// 品名印刷可能数
		/// </summary>
		public static readonly int PRINT_GOODS_MAX = 10;

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
		/// 配送センター納品書の印刷パラメタファイルの読込み
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
		/// 配送センター納品書の印刷
		/// </summary>
		/// <param name="g">Graphics</param>
		/// <param name="offset">印刷オフセット</param>
		/// <param name="dataList">印刷データリスト</param>
		/// <param name="curPage">カレントページ</param>
		/// <param name="printRect">矩形印字の有無</param>
		public void PrintNouhinData(Graphics g, Point offset, NouhinDataList dataList, int curPage, bool printRect)
		{
			// ページに対する先頭レコードのインデックス番号
		    Tuple<int, int> index = dataList.GetStartIndex(curPage);

			// インデックス番号に対して最終ページかどうか？
			bool lastPage = dataList.IsLastPage(index);

			NouhinData data = dataList[index.Item1];
			foreach (PrintPara para in ParameterList.ParaList)
			{
				string entry = para.Entry;

				//  最初のページのみ
				if ("PF" == StringUtil.Left(entry, 2))
				{
					if (1 != curPage)
					{
						// １ページ目以外を印刷中？ -> 次の印字項目へ
						continue;
					}
					// ページ制御文字を除いた印字項目エントリー名
					entry = StringUtil.Mid(entry, 2);
				}
				//  最終ページのみ
				else if ("PL" == StringUtil.Left(entry, 2))
				{
					if (false == lastPage)
					{
						// 最終ページ以外を印刷中？ ->  次の印字項目へ
						continue;
					}
					// ページ制御文字を除いた印字項目エントリー名
					entry = StringUtil.Mid(entry, 2);
				}
				switch (para.GetPrintParaType(entry))
				{
					// 線
					case PrintParaDef.PrintParaType.Line:
						para.PrintLine(g, offset);
						break;
					// 破線
					case PrintParaDef.PrintParaType.DotLine:
						para.PrintDotLine(g, offset);
						break;
					// 円
					case PrintParaDef.PrintParaType.Ellipse:
						if (printRect)
						{
							// 矩形印刷
							para.PrintFrame(g, offset, Color.Red);
						}
						para.PrintEllipse(g, offset);
						break;
					// 枠
					case PrintParaDef.PrintParaType.Frame:
						para.PrintFrame(g, offset);
						break;
					// 丸枠
					case PrintParaDef.PrintParaType.RoundFrame:
						if (printRect)
						{
							// 矩形印刷
							para.PrintFrame(g, offset, Color.Red);
						}
						para.PrintRoundFrame(g, offset, new Point(20, 20));
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
						para.PrintFillColorRoundFrame(g, offset, new Point(20, 20));
						break;
					// 文字列
					case PrintParaDef.PrintParaType.String:
						if (printRect)
						{
							// 矩形印刷
							para.PrintFrame(g, offset, Color.Red);
						}
						para.PrintString(g, offset, entry);
						break;
					// 特殊エントリ
					case PrintParaDef.PrintParaType.Special:
						if (printRect)
						{
							// 矩形印刷
							para.PrintFrame(g, offset, Color.Red);
						}
						// 通常印刷
						switch (entry)
						{
							case "<お客様コードNo>":
								para.PrintString(g, offset, data.TokuisakiNo);
								break;
							case "<受注顧客No>":
								para.PrintString(g, offset, data.CustomerNo);
								break;
							case "<No>":
								para.PrintString(g, offset, data.DenNo);
								break;
							case "<郵便番号>":
								para.PrintString(g, offset, data.Zipcode);
								break;
							case "<年>":
								para.PrintString(g, offset, data.Year);
								break;
							case "<月>":
								para.PrintString(g, offset, data.Month);
								break;
							case "<日>":
								para.PrintString(g, offset, data.Day);
								break;
							case "<住所>":
								para.PrintString(g, offset, data.Address);
								break;
							case "<医院名>":
								para.PrintString(g, offset, data.ClinicName);
								break;
							case "<電話番号>":
								para.PrintString(g, offset, data.Tel);
								break;
							case "<担当>":
								para.PrintString(g, offset, data.Tanto);
								break;
							case "<摘要>":
								para.PrintString(g, offset, data.Tekiyo);
								break;
							case "<合計>":
								para.PrintString(g, offset, PrintPara.CommaEditString(data.GoodsList[index.Item2].Total));
								break;
							default:
								if ("<品名" == StringUtil.Left(entry, 3))
								{
									int line = PrintPara.ExtractionNumeral(entry) - 1;
									if (index.Item2 + line < data.GoodsList.Count)
									{
										para.PrintString(g, offset, data.GoodsList[index.Item2 + line].GoodsName);
									}
								}
								else if ("<数量" == StringUtil.Left(entry, 3))
								{
									int line = PrintPara.ExtractionNumeral(entry) - 1;
									if (index.Item2 + line < data.GoodsList.Count)
									{
										if (0 < data.GoodsList[index.Item2 + line].Count.Length && "0" != data.GoodsList[index.Item2 + line].Count)
										{
											para.PrintString(g, offset, data.GoodsList[index.Item2 + line].Count);
										}
									}
								}
								else if ("<単価" == StringUtil.Left(entry, 3))
								{
									int line = PrintPara.ExtractionNumeral(entry) - 1;
									if (index.Item2 + line < data.GoodsList.Count)
									{
										if (0 < data.GoodsList[index.Item2 + line].Tanka.Length && "0" != data.GoodsList[index.Item2 + line].Tanka)
										{
											para.PrintString(g, offset, PrintPara.CommaEditString(data.GoodsList[index.Item2 + line].Tanka));
										}
									}
								}
								else if ("<単位" == StringUtil.Left(entry, 3))
								{
									int line = PrintPara.ExtractionNumeral(entry) - 1;
									if (index.Item2 + line < data.GoodsList.Count)
									{
										para.PrintString(g, offset, data.GoodsList[index.Item2 + line].Unit);
									}
								}
								else if ("<金額" == StringUtil.Left(entry, 3))
								{
									int line = PrintPara.ExtractionNumeral(entry) - 1;
									if (index.Item2 + line < data.GoodsList.Count)
									{
										para.PrintString(g, offset, PrintPara.CommaEditString(data.GoodsList[index.Item2 + line].Price));
									}
								}
								else if ("<備考" == StringUtil.Left(entry, 3))
								{
									int line = PrintPara.ExtractionNumeral(entry) - 1;
									if (index.Item2 + line < data.GoodsList.Count)
									{
										para.PrintString(g, offset, data.GoodsList[index.Item2 + line].Biko);
									}
								}
								else if ("<MICロゴ黒>" == entry)
								{
									using (Image image = Properties.Resources.mic_logo_Black)
									{
										para.PrintImage(g, offset, image);
									}
								}
								else if ("<MICロゴ赤>" == entry)
								{
									using (Image image = Properties.Resources.mic_logo_Red)
									{
										para.PrintImage(g, offset, image);
									}
								}
								else if ("<R70黒>" == entry)
								{
									using (Image image = Properties.Resources.R70_Black)
									{
										para.PrintImage(g, offset, image);
									}
								}
								else if ("<R70赤>" == entry)
								{
									using (Image image = Properties.Resources.R70_Red)
									{
										para.PrintImage(g, offset, image);
									}
								}
								break;
						}
						break;
					default:
						para.PrintString(g, offset, entry);
						break;
				}
			}
		}
	}
}
