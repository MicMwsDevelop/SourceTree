//
// MainForm.cs
// 
// 各種書類出力メイン画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2021/04/22):新規作成
// Ver1.05(2021/11/12):消耗品FAXオーダーシートの新規追加
// Ver1.07(2021/12/24):経理部専用 オンライン資格確認等事業完了報告書の対応
// Ver1.09(2022/02/10):二次キッティング依頼書 2022/02組織変更対応
// Ver1.11(2022/02/21):二次キッティング依頼書 使用廃止によりメニューから削除
//
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.VariousDocumentOut;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.VariousDocumentOut;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
		// Ver1.11(2022/02/21):二次キッティング依頼書 使用廃止によりメニューから削除
		//private List<tMih支店情報> BranchList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			Common = new DocumentCommon();
			//BranchList = null;
		}


		/// <summary>
		/// Ctrl+Shift+Alt 経理部専用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Ver1.07(2021/12/24):経理部専用 オンライン資格確認等事業完了報告書の対応
		private void textBoxTokuisakiNo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Shift && e.Control && e.Alt)
			{
				radioButtonOnlineConfirm.Visible = true;
			}
		}

		/// <summary>
		/// Load Form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// ウィンドウタイトルにバージョン情報を表示
			this.Text = string.Format("{0} {1}", Program.ProgramName, Program.VersionStr);

			try
			{
#if DEBUG
				textBoxTokuisakiNo.Text = "210798";
				radioButtonOnlineConfirm.Visible = true;
#endif
				DocType = DocumentOut.DocumentType.MwsIDPassword;

#if DEBUG
				List<SatelliteOffice> satelliteList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo("k-sugawara", Program.gSettings.Connect.Junp.ConnectionString);
				List<SatelliteOffice> headOfficeList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo("k-sugawara", Program.gSettings.Connect.Junp.ConnectionString);
#else
				List<SatelliteOffice> satelliteList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo(Environment.UserName, Program.gSettings.Connect.Junp.ConnectionString);
				List<SatelliteOffice> headOfficeList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo(Environment.UserName, Program.gSettings.Connect.Junp.ConnectionString);
#endif
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
				// Ver1.11(2022/02/21):二次キッティング依頼書 使用廃止によりメニューから削除
				//BranchList = JunpDatabaseAccess.Select_tMih支店情報("", "", Program.gSettings.Connect.Junp.ConnectionString);
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
			//radio二次キッティング依頼書.Enabled = enable;
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
			radioButtonOnlineConfirm.Enabled = enable;
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
				// 得意先番号で検索
				List<CustomerInfo> result = VariousDocumentOutAccess.Select_CustomerInfoByTokuisakiNo(textBoxTokuisakiNo.Text, Program.gSettings.Connect.Junp.ConnectionString);
				if (null != result && 0 < result.Count)
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
							List<vMic全ユーザー2> work2 = JunpDatabaseAccess.Select_vMic全ユーザー2(whereStr, "", Program.gSettings.Connect.Junp.ConnectionString);
							if (null != work2)
							{
								Common.Customer = work2.First();
							}
							List<vMic全ユーザー3> work3 = JunpDatabaseAccess.Select_vMic全ユーザー3(whereStr, "", Program.gSettings.Connect.Junp.ConnectionString);
							if (null != work3)
							{
								Common.Customer3 = work3.First();
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
				MessageBox.Show(this, "該当顧客が見つかりません", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textBoxCustomerNo.Text = string.Empty;
				textBoxTokuisakiNo.Text = string.Empty;
				textBoxCustomerName.Text = string.Empty;
				Common.ClearCustomer();
			}
			if (MwsDefine.CustomerNoLength == textBoxCustomerNo.Text.Length)
			{
				// 顧客Noで検索
				List<CustomerInfo> result = VariousDocumentOutAccess.Select_CustomerInfoByCustomerNo(textBoxCustomerNo.ToInt(), Program.gSettings.Connect.Junp.ConnectionString);
				if (null != result && 0 < result.Count)
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
							string whereStr = string.Format("顧客No = {0}", textBoxCustomerNo.ToInt());
							List<vMic全ユーザー2> work2 = JunpDatabaseAccess.Select_vMic全ユーザー2(whereStr, "", Program.gSettings.Connect.Junp.ConnectionString);
							if (null != work2)
							{
								Common.Customer = work2.First();
							}
							List<vMic全ユーザー3> work3 = JunpDatabaseAccess.Select_vMic全ユーザー3(whereStr, "", Program.gSettings.Connect.Junp.ConnectionString);
							if (null != work3)
							{
								Common.Customer3 = work3.First();
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
				MessageBox.Show(this, "該当顧客が見つかりません", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textBoxCustomerNo.Text = string.Empty;
				textBoxTokuisakiNo.Text = string.Empty;
				textBoxCustomerName.Text = string.Empty;
				Common.ClearCustomer();
			}
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
			textBoxTokuisakiNo.Text = string.Empty;
			textBoxCustomerNo.Text = string.Empty;
			textBoxCustomerName.Text = string.Empty;
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
			DocType = DocumentOut.DocumentType.OnlineApply;
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
			//DocType = DocumentOut.DocumentType.SecondKitting;
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
		/// 作業報告書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButton作業報告書_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.WorkReport;
		}

		/// <summary>
		/// 消耗品FAXオーダーシート
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// Ver1.03(2021/09/02):消耗品FAXオーダーシートの新規追加
		private void radioButtonFaxOrderSheet_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.FaxOrderSheet;
		}

		/// <summary>
		/// オンライン資格確認等事業完了報告書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// Ver1.07(2021/12/24):経理部専用 オンライン資格確認等事業完了報告書の対応
		private void radioButtonOnlineConfirm_CheckedChanged(object sender, EventArgs e)
		{
			DocType = DocumentOut.DocumentType.OnlineConfirmKeiri;
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
				MessageBox.Show(this, "得意先番号を指定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// orgファイル→Excelファイルをコピー
				string orgPpathname = Path.Combine(Directory.GetCurrentDirectory(), DocumentOut.OrgFileName[DocType]);
				string xlsPathname = Path.Combine(Directory.GetCurrentDirectory(), DocumentOut.ExcelFileName[DocType]);
				File.Copy(orgPpathname, xlsPathname, true);
				switch (DocType)
				{
					/// <summary>
					/// MWS IDパスワード
					/// </summary>
					case DocumentOut.DocumentType.MwsIDPassword:
						DocumentOut.ExcelOutMwsIDPassword(Common, xlsPathname);
						break;
					/// <summary>
					/// FAX送付状
					/// </summary>
					case DocumentOut.DocumentType.FaxLetter:
						DocumentOut.ExcelOutFaxLetter(Common, xlsPathname);
						break;
					/// <summary>
					/// 書類送付状
					/// </summary>
					case DocumentOut.DocumentType.DocumentLetter:
						DocumentOut.ExcelOutDocumentLetter(Common, xlsPathname);
						break;
					/// <summary>
					/// 光ディスク請求届出
					/// </summary>
					case DocumentOut.DocumentType.LightDisk:
						DocumentOut.ExcelOutLightDisk(Common, xlsPathname);
						break;
					/// <summary>
					/// オンライン請求届出
					/// </summary>
					case DocumentOut.DocumentType.OnlineApply:
						DocumentOut.ExcelOutOnline(Common, xlsPathname, orgPpathname);
						break;
					/// <summary>
					/// 取引条件確認書
					/// </summary>
					case DocumentOut.DocumentType.Transaction:
						DocumentOut.ExcelOutTransaction(Common, xlsPathname);
						break;
					/// <summary>
					/// 登録データ確認カード
					/// </summary>
					case DocumentOut.DocumentType.ConfirmCard:
						DocumentOut.ExcelOutConfirmCard(Common, xlsPathname);
						break;
					/// <summary>
					/// Microsoft365利用申請書
					/// </summary>
					case DocumentOut.DocumentType.Microsoft365:
						DocumentOut.ExcelOutMicrosoft365(Common, xlsPathname);
						break;
					/// <summary>
					/// 請求先変更届
					/// </summary>
					case DocumentOut.DocumentType.SeikyuChange:
						DocumentOut.ExcelOutSeikyuChange(Common, xlsPathname);
						break;
					/// <summary>
					/// 終了届
					/// </summary>
					case DocumentOut.DocumentType.UserFinished:
						DocumentOut.ExcelOutUserFinished(Common, xlsPathname);
						break;
					/// <summary>
					/// 変更届
					/// </summary>
					case DocumentOut.DocumentType.UserChange:
						DocumentOut.ExcelOutUserChange(Common, xlsPathname);
						break;
					/// <summary>
					/// 第一園芸注文書
					/// </summary>
					case DocumentOut.DocumentType.FirstEngei:
						DocumentOut.ExcelOutFirstEngei(Common, xlsPathname);
						break;
					/// <summary>
					/// 納品補助作業依頼書
					/// </summary>
					case DocumentOut.DocumentType.Delivery:
						DocumentOut.ExcelOutDelivery(Common, xlsPathname);
						break;
					///// <summary>
					///// 二次キッティング依頼書
					///// </summary>
					///// Ver1.09(2022/02/10):二次キッティング依頼書 2022/02組織変更対応
					//// Ver1.11(2022/02/21):二次キッティング依頼書 使用廃止によりメニューから削除
					//case DocumentOut.DocumentType.SecondKitting:
					//	if (Common.IsHeadOffice)
					//	{
					//		// 本社
					//		DocumentOut.ExcelOutSecondKitting(Common, xlsPathname, string.Empty, null);
					//	}
					//	else
					//	{
					//		// 営業部
					//		using (SelectSatelliteForm dlg = new SelectSatelliteForm())
					//		{
					//			dlg.Office = Common.Satellite;
					//			;
					//			if (DialogResult.Cancel == dlg.ShowDialog())
					//			{
					//				return;
					//			}
					//			tMih支店情報 branch = BranchList.Find(p => p.fBshCode2 == dlg.SelectBusho.fBshCode2 && p.fBshCode3 == dlg.SelectBusho.fBshCode3);
					//			DocumentOut.ExcelOutSecondKitting(Common, xlsPathname, dlg.SelectBusho.fBshName2, branch);
					//		}
					//	}
					//	break;
					/// <summary>
					/// PC安心サポート加入申込書
					/// </summary>
					case DocumentOut.DocumentType.PcSupport:
						DocumentOut.ExcelOutPcSupport(Common, xlsPathname);
						break;
					/// <summary>
					/// アプラス預金口座振替依頼書・自動払込利用申込書
					/// </summary>
					case DocumentOut.DocumentType.Aplus:
						DocumentOut.ExcelOutAplus(Common, xlsPathname);
						break;
					/// <summary>
					/// 作業報告書
					/// </summary>
					case DocumentOut.DocumentType.WorkReport:
						DocumentOut.ExcelOutWorkReport(Common, xlsPathname);
						break;
					/// <summary>
					/// 消耗品FAXオーダーシート
					/// </summary>
					/// Ver1.03(2021/09/02):消耗品FAXオーダーシートの新規追加
					case DocumentOut.DocumentType.FaxOrderSheet:
						DocumentOut.ExcelOutFaxOrderSheet(Common, xlsPathname);
						break;
					/// <summary>
					/// オンライン資格確認等事業完了報告書(経理部専用)
					/// </summary>
					/// Ver1.07(2021/12/24):経理部専用 オンライン資格確認等事業完了報告書の対応
					case DocumentOut.DocumentType.OnlineConfirmKeiri:
						{
							try
							{
								// 顧客詳細情報の読込
								string whereStr = string.Format("顧客No = {0}", textBoxCustomerNo.ToInt());
								List<オンライン資格確認対象商品売上明細> goodsList = VariousDocumentOutAccess.Select_オンライン資格確認対象商品売上明細(Common.Customer.得意先No, Program.gSettings.Connect.Junp.ConnectionString);
								if (null != goodsList)
								{
									DocumentOut.ExcelOutOnlineConfirm(Common, xlsPathname, orgPpathname, goodsList);
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show(this, string.Format("サーバー通信エラー：{0}", ex.Message), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
								return;
							}
						}
						break;
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				// Excelの起動
				using (Process process = new Process())
				{
					process.StartInfo.FileName = xlsPathname;
					process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
					process.Start();
				}
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
