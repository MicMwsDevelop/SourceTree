//
// NouhinDataList.cs
// 
// 配送センター納品書情報リストクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/10/25 勝呂)
//
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace DeliveryCenterPrintNouhin.Print
{
	/// <summary>
	/// 配送センター納品書情報リストクラス
	/// </summary>
	public class NouhinDataList : List<NouhinData>
	{
		/// <summary>
		/// 印刷枚数の取得
		/// </summary>
		/// <returns>印刷枚数</returns>
		public int GetMaxPage()
		{
			if (0 < this.Count)
			{
				int page = 0;
				foreach (NouhinData data in this)
				{
					page += data.GetMaxPage();
				}
				return page;
			}
			return 0;
		}

		/// <summary>
		/// 印刷開始位置を取得
		/// </summary>
		/// <param name="page">現在のページ</param>
		/// <returns></returns>
		public Tuple<int, int> GetStartIndex(int page)
		{
			if (0 < this.Count)
			{
				int curpage = 0;
				for (int i = 0; i < this.Count; i++)
				{
					int maxPage = this[i].GetMaxPage();
					if (curpage + maxPage < page)
					{
						curpage += maxPage;
					}
					else if (curpage + 1 == page)
					{
						return new Tuple<int, int>(i, 0);
					}
					else
					{
						curpage++;
						int curline = 1;
						for (int j = 0; j < this[i].GoodsList.Count; j++)
						{
							if (curline == PrintNouhinControl.PRINT_GOODS_MAX)
							{
								if (curpage + 1 == page)
								{
									return new Tuple<int, int>(i, PrintNouhinControl.PRINT_GOODS_MAX * (this[i].GoodsList.Count / PrintNouhinControl.PRINT_GOODS_MAX));
								}
								curpage++;
								curline = 1;
							}
							else
							{
								curline++;
							}
						}
					}
				}


				//int curpage = 1;
				//for (int i = 0; i < this.Count; i++)
				//{
				//	if (curpage == page)
				//	{
				//		return new Tuple<int, int>(i, 0);
				//	}
				//	int curline = 1;
				//	for (int j = 0; j < this[i].GoodsList.Count; j++)
				//	{
				//		if (curline == PrintNouhinControl.PRINT_GOODS_MAX)
				//		{
				//			if (curpage + 1 == page)
				//			{
				//				return new Tuple<int, int>(i, PrintNouhinControl.PRINT_GOODS_MAX * curpage);
				//			}
				//			curpage++;
				//			curline = 1;
				//		}
				//		else
				//		{
				//			curline++;
				//		}
				//	}
				//	curpage++;
				//}
			}
			return null;
		}

		/// <summary>
		/// その伝票の最終ページかどうか？
		/// </summary>
		/// <param name="index">ページに対する先頭レコード</param>
		/// <returns>判定</returns>
		public bool IsLastPage(Tuple<int, int> index)
		{
			if (this[index.Item1].GoodsList.Count - index.Item2 <= PrintNouhinControl.PRINT_GOODS_MAX)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// 商品書CSVを読み込み納品書データリストに保存する
		/// </summary>
		/// <param name="pathname">パラメタファイルパス名</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>納品書データリスト</returns>
		public static NouhinDataList ReadNohinCsvFile(string pathname, out string msg)
		{
			msg = string.Empty;

			NouhinDataList dataList = new NouhinDataList();
			if (File.Exists(pathname))
			{
				try
				{
					// テキストファイルの読み込み
					using (StreamReader textfile = new StreamReader(pathname, System.Text.Encoding.GetEncoding("Shift_JIS")))
					{
						string line = textfile.ReadLine();
						bool firstLine = true;
						NouhinData data = null;
						while (null != line)
						{
							if (0 < line.Length)
							{
								if (!firstLine)
								{
									line = line.Trim(StringUtil.DefalutTrimCharSet);

									if (';' != line[0])
									{
										// コメント行以外
										List<string> split = SplitString.CSVSplitLine(line);
										if ('1' == line[0])
										{
											// 先頭レコードの設定
											data = new NouhinData();
											data.SetTopRecord(split);
											dataList.Add(data);
										}
										else
										{
											// 納品物情報の設定
											data.SetGoodsRecord(split);
										}
									}
								}
								else
								{
									// １行目はタイトル行なのでスキップ
									firstLine = false;
								}
							}
							line = textfile.ReadLine();
						}
					}
				}
				catch (Exception ex)
				{
					msg = ex.ToString();
					return null;
				}
			}
			else
			{
				msg = pathname + "が存在しません。";
				return null;
			}
			return dataList;
		}

		/// <summary>
		/// テスト印刷
		/// </summary>
		/// <returns></returns>
		public static NouhinDataList GetTestData()
		{
			NouhinDataList test = new NouhinDataList();
			for (int i = 0; i < PrintNouhinControl.PRINT_GOODS_MAX; i++)
			{
				test.Add(NouhinData.GetTestData());
			}
			return test;
		}
	}
}
