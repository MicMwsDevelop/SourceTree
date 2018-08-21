//
// MIC WEB SERVICE見積書・注文書/注文請書印刷クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// 
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.MwsSimulation;
using MwsLib.Common;
using MwsLib.Print;
using MwsSimulation.Forms;
using MwsSimulation.Settings;
using System.Drawing;

namespace MwsSimulation.Print
{
	/// <summary>
	/// MIC WEB SERVICE見積書・注文書/注文請書印刷クラス
	/// </summary>
	public class PrintEstimate
	{
		/// <summary>
		/// 印刷パラメータリスト
		/// </summary>
		public PrintParaList ParameterList { private set; get; }

		/// <summary>
		/// 用紙種別
		/// </summary>
		public PrintEstimateDef.MwsPaperType PaperType { get; set; }

		/// <summary>
		/// 見積書印刷情報
		/// </summary>
		public PrintEstimateData PrintData { get; set; }

		/// <summary>
		/// 合計金額（税抜き）
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// 消費税率
		/// </summary>
		public int TaxRate { get; set; }

		/// <summary>
		/// 消費税
		/// </summary>
		public int Tax { get; set; }

		/// <summary>
		/// 合計金額の取得（税込）
		/// </summary>
		private int TotalPrice
		{
			get {
				return Price + Tax;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PrintEstimate()
		{
			ParameterList = new PrintParaList();
			PaperType = PrintEstimateDef.MwsPaperType.Estimate;
			PrintData = new PrintEstimateData();
			Price = 0;
			TaxRate = 0;
			Tax = 0;
		}

		/// <summary>
		/// MIC WEB SERVICE見積書・注文書/注文請書の印刷パラメタファイルの読込み
		/// </summary>
		/// <param name="type">用紙種別</param>
		/// <param name="msg">エラーメッセージ文字列</param>
		/// <returns>読込み行数</returns>
		public int ReadEstimateParameterFile(PrintEstimateDef.MwsPaperType type, out string msg)
		{
			string sectionName = string.Empty;
			switch (type)
			{
				case PrintEstimateDef.MwsPaperType.Estimate:
					sectionName = PrintEstimateDef.ESTIMATE_SECTION_NAME;
					break;
				case PrintEstimateDef.MwsPaperType.PurchaseOrder:
					sectionName = PrintEstimateDef.PURCHASE_ORDER_SECTION_NAME;
					break;
				case PrintEstimateDef.MwsPaperType.OrderConfirm:
					sectionName = PrintEstimateDef.OERDER_COMFIRM_SECTION_NAME;
					break;
			}
			if (false == ParameterList.ReadParameterFile(PrintEstimateDef.GetParameterFilePath(), PrintEstimateDef.PARAMETER_FILENAME, sectionName, out msg))
			{
				//パラメータ読み込みエラー
				return -1;
			}
			return ParameterList.ParaList.Count;
		}

		/// <summary>
		/// 見積ページ情報の設定
		/// </summary>
		/// <param name="type">用紙種別</param>
		/// <param name="est">見積書情報</param>
		/// <param name="taxRate">消費税率</param>
		/// <returns>見積ページ情報数</returns>
		public int SetData(PrintEstimateDef.MwsPaperType type, Estimate est, int taxRate)
		{
			PaperType = type;
			Price = est.GetPrice;
			TaxRate = taxRate;
			Tax = CalcTax.GetTaxPrice(TaxRate, CalcTax.RoundFraction.Round, Price);		// 四捨五入
			return PrintData.SetEstimateData(est);
		}

		/// <summary>
		/// 最大ページ数の取得
		/// </summary>
		/// <param name="est">見積書情報</param>
		/// <returns>最大ページ数</returns>
		public int GetMaxPage()
		{
			return PrintData.GetMaxPage();
		}

		/// <summary>
		/// MIC WEB SERVICEお見積書の印刷
		/// </summary>
		/// <param name="g">Graphics</param>
		/// <param name="offset">印刷オフセット</param>
		/// <param name="curPage">カレントページ</param>
		/// <param name="printRect">矩形印字の有無</param>
		public void PrintEstimateData(Graphics g, Point offset, int curPage, bool printRect)
		{
			Color formColor = Color.Black;
			int maxPage = this.GetMaxPage();

			BranchSettings branch;
			if (MainForm.gSettings.CurrentBranchIndex < MainForm.gSettings.BranchList.Count)
			{
				branch = MainForm.gSettings.BranchList[MainForm.gSettings.CurrentBranchIndex];
			}
			else
			{
				branch = new BranchSettings();
			}
			string staff = string.Empty;
			if (MainForm.gSettings.CurrentStaffIndex < MainForm.gSettings.StaffList.Count)
			{
				staff = MainForm.gSettings.StaffList[MainForm.gSettings.CurrentStaffIndex];
			}
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
					para.Entry = StringUtil.Left(para.Entry, 2);
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
					para.Entry = StringUtil.Left(para.Entry, 2);
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
						if ("<PAGE>" == para.Entry)
						{
							para.PrintString(g, offset, string.Format("{0}/{1}", curPage, maxPage));
						}
						else if ("<発行西暦>" == para.Entry)
						{
							para.PrintString(g, offset, PrintData.PrintDate.Year.ToString());
						}
						else if ("<発行月>" == para.Entry)
						{
							para.PrintString(g, offset, PrintData.PrintDate.Month.ToString());
						}
						else if ("<発行日>" == para.Entry)
						{
							para.PrintString(g, offset, PrintData.PrintDate.Day.ToString());
						}
						else if ("<宛先>" == para.Entry)
						{
							para.PrintString(g, offset, PrintData.Destination);
						}
						else if ("<合計金額>" == para.Entry)
						{
							para.PrintString(g, offset, StringUtil.CommaEdit(this.TotalPrice));
						}
						else if ("<内消費税タイトル>" == para.Entry)
						{
							para.PrintString(g, offset, string.Format("内消費税（{0}%）", TaxRate));
						}
						else if ("<内消費税>" == para.Entry)
						{
							para.PrintString(g, offset, StringUtil.CommaEdit(this.Tax));
						}
						else if ("<拠点名>" == para.Entry)
						{
							para.PrintString(g, offset, branch.Name);
						}
						else if ("<郵便番号>" == para.Entry)
						{
							para.PrintString(g, offset, string.Format("〒{0}", branch.Zipcode));
						}
						else if ("<住所1>" == para.Entry)
						{
							para.PrintString(g, offset, branch.Address1);
						}
						else if ("<住所2>" == para.Entry)
						{
							para.PrintString(g, offset, branch.Address2);
						}
						else if ("<TEL>" == para.Entry)
						{
							para.PrintString(g, offset, branch.Tel);
						}
						else if ("<FAX>" == para.Entry)
						{
							para.PrintString(g, offset, branch.Fax);
						}
						else if ("<担当者>" == para.Entry)
						{
							para.PrintString(g, offset, staff);
						}
						else if ("<品番" == StringUtil.Left(para.Entry, 3))
						{
							int line = PrintEstimateDef.ExtractionNumeral(para.Entry);
							EstimateData est = PrintData.GetEstimateData(curPage, line);
							if (null != est)
							{
								para.PrintString(g, offset, est.GoodsID);
							}
						}
						else if ("<品名" == StringUtil.Left(para.Entry, 3))
						{
							int line = PrintEstimateDef.ExtractionNumeral(para.Entry);
							EstimateData est = PrintData.GetEstimateData(curPage, line);
							if (null != est)
							{
								para.PrintString(g, offset, est.ServiceName);
							}
						}
						else if ("<数量" == StringUtil.Left(para.Entry, 3))
						{
							int line = PrintEstimateDef.ExtractionNumeral(para.Entry);
							EstimateData est = PrintData.GetEstimateData(curPage, line);
							if (null != est)
							{
								if (false == est.ChildServide)
								{
									// おまとめプラン or セット割サービスの子処置の数量は印字しない
									para.PrintString(g, offset, "1");
								}
							}
						}
						else if ("<金額" == StringUtil.Left(para.Entry, 3))
						{
							int line = PrintEstimateDef.ExtractionNumeral(para.Entry);
							EstimateData est = PrintData.GetEstimateData(curPage, line);
							if (null != est)
							{
								if (false == est.ChildServide)
								{
									// おまとめプラン or セット割サービスの子処置の金額は印字しない
									para.PrintString(g, offset, StringUtil.CommaEdit(est.Price));
								}
							}
						}
						else if ("<有効期限>" == para.Entry)
						{
							if (false == PrintData.AgreementSpan.IsNothing())
							{
								// 発行日の２週間後
								Date limitDate = PrintData.PrintDate + 13;
								para.PrintString(g, offset, limitDate.GetNormalString());
							}
						}
						else if ("<契約期間>" == para.Entry)
						{
							if (false == PrintData.AgreementSpan.IsNothing())
							{
								// 2018/01/01～2018/02/01
								para.PrintString(g, offset, PrintData.AgreementSpan.GetJapaneseANString("～", true, '0', true));
							}
						}
						else if ("<備考>" == para.Entry)
						{
							PrintPara workPara = para.CloneDeep();
							int height = para.Option.Rect.Height / PrintEstimateDef.REMARK_MAXLINE;
							workPara.Option.Rect = new Rectangle(para.Option.Rect.Left, para.Option.Rect.Top, para.Option.Rect.Width, height);
							for (int i = 0; i < PrintData.Remark.Count; i++)
							{
								workPara.PrintString(g, offset, PrintData.Remark[i]);
								workPara.Option.Rect = new Rectangle(workPara.Option.Rect.Left, workPara.Option.Rect.Top + height, workPara.Option.Rect.Width, height);
							}
						}
						else if ("<MICロゴ>" == para.Entry)
						{
							using (Image image = Properties.Resources.MicLogo)
							{
								para.PrintImage(g, offset, image);
							}
						}
						else if ("<MIC社印>" == para.Entry)
						{
							using (Image image = Properties.Resources.MicSeal)
							{
								para.PrintImage(g, offset, image);
							}
						}
						else
						{
							para.PrintString(g, offset, para.Entry);
						}
						break;
				}
			}
		}

	}
}
