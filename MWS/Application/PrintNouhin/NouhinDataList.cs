using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace PrintNouhin
{
	public class NouhinDataList : List<NouhinData>
	{
		public const string CSVFILENAME = "Nouhin.csv";

		/// <summary>
		/// 印刷回数を取得
		/// </summary>
		/// <returns>印刷回数</returns>
		public int GetPrintCount()
		{
			int ret = 0;
			foreach (NouhinData data in this)
			{
				if (data.IsTop)
				{
					// 先頭レコード
					ret++;
				}
			}
			return ret;
		}

		/// <summary>
		/// 指定された印刷部に対する印刷データリストの取得
		/// </summary>
		/// <param name="index">印刷部(1origin)</param>
		/// <returns>印刷データリスト</returns>
		public NouhinDataList GetPrintDataList(int index)
		{
			NouhinDataList ret = null;
			int cnt = 0;
			foreach (NouhinData data in this)
			{
				if (data.IsTop)
				{
					cnt++;
					if (cnt == index)
					{
						if (null == ret)
						{
							ret = new NouhinDataList();
						}
						ret.Add(data);
					}
				}
				else
				{
					if (null != ret)
					{
						if (data.JuchuBango == ret[0].JuchuBango)
						{
							ret.Add(data);
						}
						else
						{
							break;
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 印刷枚数の取得
		/// </summary>
		/// <returns></returns>
		public int GetMaxPage()
		{
			if (0 < this.Count)
			{
				int page = this.Count / PrintNouhinControl.PRINT_GOODS_MAX;
				int amari = this.Count % PrintNouhinControl.PRINT_GOODS_MAX;
				return (0 == amari) ? page : page + 1;
			}
			return 0;
		}

		/// <summary>
		/// ページ毎の印刷開始データ位置を取得
		/// </summary>
		/// <param name="page">現在のページ</param>
		/// <returns></returns>
		public int GetStartIndex(int page)
		{
			return PrintNouhinControl.PRINT_GOODS_MAX * (page - 1);
		}

		/// <summary>
		/// 商品書CSVを読み込み納品書データリストに保存する
		/// </summary>
		/// <param name="pathname"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
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
						string current_section = string.Empty;
						string line = textfile.ReadLine();
						bool firstLine = true;
						while (null != line)
						{
							if (!firstLine)
							{
								line = line.Trim(StringUtil.DefalutTrimCharSet);
								if (';' != line[0])
								{
									// コメント行以外
									NouhinData data = new NouhinData();
									data.SetCsvData(line);
									dataList.Add(data);
								}
							}
							else
							{
								// １行目はタイトル行なのでスキップ
								firstLine = false;
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
	}
}
