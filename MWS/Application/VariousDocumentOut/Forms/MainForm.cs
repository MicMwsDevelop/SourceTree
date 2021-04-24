//
// MainForm.cs
// 
// 各種書類出力メイン画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.BaseFactory.Junp.View;
using MwsLib.BaseFactory.VariousDocumentOut;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.DB.SqlServer.VariousDocumentOut;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VariousDocumentOut.Settings;

namespace VariousDocumentOut.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 各種書類出力 共通情報
		/// </summary>
		private DocumentCommon Common { get; set; }

		/// <summary>
		/// 各種書類出力種別
		/// </summary>
		private DocumentOut.DocumentType DocType { get; set;}

		/// <summary>
		/// 支店情報
		/// </summary>
		private List<tMih支店情報> BranchList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			Common = new DocumentCommon();
			BranchList = null;
		}

		/// <summary>
		/// Load Form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
#if DEBUG
				textBoxTokuisakiNo.Text = "010223";
#endif
				// 本社情報の読込
				Common.HeadOffice = HeadOfficeSettingsIF.GetSettings();

				DocType = DocumentOut.DocumentType.MwsIDPassword;
				//#if DEBUG
				//				List<SatelliteOffice> satelliteList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo("itagaki", Program.DATABASE_ACCESS_CT);
				//				List<SatelliteOffice> headOfficeList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo("itagaki", Program.DATABASE_ACCESS_CT);
				//#else
				//			List<SatelliteOffice> satelliteList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo(Environment.UserName, Program.DATABASE_ACCESS_CT);
				//			List<SatelliteOffice> headOfficeList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo(Environment.UserName, Program.DATABASE_ACCESS_CT);
				//#endif
				List<SatelliteOffice> satelliteList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo(Environment.UserName, Program.DATABASE_ACCESS_CT);
				List<SatelliteOffice> headOfficeList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo(Environment.UserName, Program.DATABASE_ACCESS_CT);
				if (null == satelliteList && null == headOfficeList)
				{
					// 本社所属
					;
				}
				else if (0 < satelliteList.Count && null == headOfficeList)
				{
					// 営業部所属
					Common.Satellite = satelliteList.First();
				}
				else
				{
					// 営業部以外所属
					Common.Satellite = headOfficeList.First();
				}
				// 支店情報の取得
				BranchList = JunpDatabaseAccess.Select_tMih支店情報("", "", Program.DATABASE_ACCESS_CT);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, string.Format("サーバー通信エラー：{0}", ex.Message), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 各種書類出力管理
		/// </summary>
		private void ReportOutEnable(bool enable)
		{
			radioアプラス預金口座振替依頼書.Enabled = enable;
			radioPC安心サポート加入申込書.Enabled = enable;
			radio二次キッティング依頼書.Enabled = enable;
			radio納品補助作業依頼書.Enabled = enable;
			radio第一園芸注文書.Enabled = enable;
			radio変更届.Enabled = enable;
			radio終了届.Enabled = enable;
			radio請求先変更届.Enabled = enable;
			radioMicrosoft365利用申請書.Enabled = enable;
			radio登録データ確認カード.Enabled = enable;
			radio取引条件確認書.Enabled = enable;
			radioオンライン請求届出.Enabled = enable;
			radio光ディスク請求届出.Enabled = enable;
			radio書類送付状.Enabled = enable;
			radioFAX送付状.Enabled = enable;
			radioMWSIDパスワード.Enabled = enable;
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			if (MwsDefine.TokuisakiNoLength == textBoxTokuisakiNo.Text.Length)
			{
				List<CustomerInfo> result = VariousDocumentOutAccess.Select_CustomerInfo(textBoxTokuisakiNo.Text, Program.DATABASE_ACCESS_CT);
				if (0 < result.Count)
				{
					CustomerInfo cust = result.First();
					textBoxCustomerName.Text = cust.顧客名;
					Common.運用サポート情報 = cust.運用サポート情報;

					if (cust.Enable)
					{
						// 出力可能
						ReportOutEnable(true);
						try
						{
							// 顧客詳細情報の読込
							string whereStr = string.Format("得意先No = '{0}'", textBoxTokuisakiNo.Text);
							List<vMic全ユーザー2> work = JunpDatabaseAccess.Select_vMic全ユーザー2(whereStr, "", Program.DATABASE_ACCESS_CT);
							if (null != work)
							{
								Common.Customer = work.First();
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(this, string.Format("サーバー通信エラー：{0}", ex.Message), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
					}
					else
					{
						// 出力不可
						ReportOutEnable(false);
					}
					return;
				}
				textBoxCustomerName.Text = string.Empty;
				Common.ClearCustomer();
			}
			MessageBox.Show(this, "該当顧客が見つかりません", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// クリア
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void buttonClear_Click(object sender, EventArgs e)
		{
			// 顧客情報クリア
			Common.ClearCustomer();
		}

		/// <summary>
		/// MWS IDパスワード
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioMWSIDパスワード_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.MwsIDPassword;
		}

		/// <summary>
		/// FAX送付状
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioFAX送付状_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.FaxLetter;
		}

		/// <summary>
		/// 書類送付状
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio書類送付状_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.DocumentLetter;
		}

		/// <summary>
		/// 光ディスク請求届出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio光ディスク請求届出_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.LightDisk;
		}

		/// <summary>
		/// オンライン請求届出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioオンライン請求届出_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.Online;
		}

		/// <summary>
		/// 取引条件確認書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio取引条件確認書_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.Transaction;
		}

		/// <summary>
		/// 登録データ確認カード
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio登録データ確認カード_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.ConfirmCard;
		}

		/// <summary>
		/// Microsoft365利用申請書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioMicrosoft365利用申請書_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.Microsoft365;
		}

		/// <summary>
		/// 請求先変更届
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio請求先変更届_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.SeikyuChange;
		}

		/// <summary>
		/// 終了届
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio終了届_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.UserFinished;
		}

		/// <summary>
		/// 変更届
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio変更届_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.UserChange;
		}

		/// <summary>
		/// 第一園芸注文書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio第一園芸注文書_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.FirstEngei;
		}

		/// <summary>
		/// 納品補助作業依頼書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio納品補助作業依頼書_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.Delivery;
		}

		/// <summary>
		/// 二次キッティング依頼書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radio二次キッティング依頼書_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.SecondKitting;
		}

		/// <summary>
		/// PC安心サポート加入申込書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioPC安心サポート加入申込書_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.PcSupport;
		}

		/// <summary>
		/// アプラス預金口座振替依頼書・自動払込利用申込書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioアプラス預金口座振替依頼書_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.Aplus;
		}

		/// <summary>
		/// EXCEL出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutputExcel_Click(object sender, EventArgs e)
		{
			if (null == Common.Customer)
			{
				MessageBox.Show(this, "得意先Noを指定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				string filename = string.Empty;
				string pathname = string.Empty;
				switch (DocType)
				{
					/// <summary>
					/// MWS IDパスワード
					/// </summary>
					case DocumentOut.DocumentType.MwsIDPassword:
						filename = "1-MWSIDパスワード.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutMwsIDPassword(pathname, Common);
						break;
					/// <summary>
					/// FAX送付状
					/// </summary>
					case DocumentOut.DocumentType.FaxLetter:
						filename = "2-FAX送付状.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutFaxLetter(pathname, Common);
						break;
					/// <summary>
					/// 書類送付状
					/// </summary>
					case DocumentOut.DocumentType.DocumentLetter:
						filename = "3-書類送付状.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutDocumentLetter(pathname, Common);
						break;
					/// <summary>
					/// 光ディスク請求届出
					/// </summary>
					case DocumentOut.DocumentType.LightDisk:
						filename = "4-光ディスク請求届出.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutLightDisk(pathname, Common);
						break;
					/// <summary>
					/// オンライン請求届出
					/// </summary>
					case DocumentOut.DocumentType.Online:
						filename = "5-オンライン請求届出.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutOnline(pathname, Common);
						break;
					/// <summary>
					/// 取引条件確認書
					/// </summary>
					case DocumentOut.DocumentType.Transaction:
						filename = "6-取引条件確認書.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutTransaction(pathname, Common);
						break;
					/// <summary>
					/// 登録データ確認カード
					/// </summary>
					case DocumentOut.DocumentType.ConfirmCard:
						filename = "7-登録データ確認カード.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutConfirmCard(pathname, Common);
						break;
					/// <summary>
					/// Microsoft365利用申請書
					/// </summary>
					case DocumentOut.DocumentType.Microsoft365:
						filename = "8-Microsoft365利用申請書.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutMicrosoft365(pathname, Common);
						break;
					/// <summary>
					/// 請求先変更届
					/// </summary>
					case DocumentOut.DocumentType.SeikyuChange:
						filename = "9-請求先変更届.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutSeikyuChange(pathname, Common);
						break;
					/// <summary>
					/// 終了届
					/// </summary>
					case DocumentOut.DocumentType.UserFinished:
						filename = "10-終了届.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutUserFinished(pathname, Common);
						break;
					/// <summary>
					/// 変更届
					/// </summary>
					case DocumentOut.DocumentType.UserChange:
						filename = "11-変更届.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutUserChange(pathname, Common);
						break;
					/// <summary>
					/// 第一園芸注文書
					/// </summary>
					case DocumentOut.DocumentType.FirstEngei:
						filename = "12-第一園芸注文書.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutFirstEngei(pathname, Common);
						break;
					/// <summary>
					/// 納品補助作業依頼書
					/// </summary>
					case DocumentOut.DocumentType.Delivery:
						filename = "13-納品補助作業依頼書.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutDelivery(pathname, Common);
						break;
					/// <summary>
					/// 二次キッティング依頼書
					/// </summary>
					case DocumentOut.DocumentType.SecondKitting:
						if (Common.IsHeadOffice)
						{
							// 本社
							filename = "14-2次キッティング依頼書.xlsx";
							pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
							DocumentOut.ExcelOutSecondKitting(pathname, Common, string.Empty, null);
						}
						else
						{
							// 営業部
							using (SelectSatelliteForm dlg = new SelectSatelliteForm())
							{
								dlg.SaleDepartment = Common.Satellite.SaleDepartment;
								dlg.Branch = Common.Satellite.Branch;
								if (DialogResult.Cancel == dlg.ShowDialog())
								{
									return;
								}
								tMih支店情報 branch = BranchList.Find(p => p.f支店名 == dlg.Branch);
								filename = "14-2次キッティング依頼書.xlsx";
								pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
								DocumentOut.ExcelOutSecondKitting(pathname, Common, dlg.SaleDepartment, branch);
							}
						}
						break;
					/// <summary>
					/// PC安心サポート加入申込書
					/// </summary>
					case DocumentOut.DocumentType.PcSupport:
						filename = "15-PC安心サポート加入申込書.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutPcSupport(pathname, Common);
						break;
					/// <summary>
					/// アプラス預金口座振替依頼書・自動払込利用申込書
					/// </summary>
					case DocumentOut.DocumentType.Aplus:
						filename = "16-アプラス預金口座振替依頼書・自動払込利用申込書.xlsx";
						pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
						DocumentOut.ExcelOutAplus(pathname, Common);
						break;
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				// Excelの起動
				ProcessStartInfo pInfo = new ProcessStartInfo();
				pInfo.FileName = pathname;
				Process.Start(pInfo);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
