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
// Ver1.15(2023/01/13):19-経理部専用 オンライン資格確認等事業完了報告書 注文確認書の追加、領収証および書類送付状の削除
// Ver1.18(2023/04/06 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 送付先リストから受注日を取得
// Ver1.20(2023/06/09 勝呂):2-FAX送付状、3-種類送付状が販売店の時に出力できない
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
using VariousDocumentOut.BaseFactory;

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
				// オンライン資格確認等事業完了報告書を有効
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
				textBoxTokuisakiNo.Text = "010196";

				// オンライン資格確認等事業完了報告書を有効
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
			radioMWSIDパスワード.Enabled = enable;
			radioButton作業報告書.Enabled = enable;
			radioButtonFaxOrderSheet.Enabled = enable;
			radioButtonOnlineConfirm.Enabled = enable;

			// Ver1.20(2023/06/09 勝呂):2-FAX送付状、3-種類送付状が販売店の時に出力できない
			//radio書類送付状.Enabled = enable;
			//radioFAX送付状.Enabled = enable;
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			try
			{
				CustomerInfo cust = null;
				if (MwsDefine.TokuisakiNoLength == textBoxTokuisakiNo.Text.Length)
				{
					// 得意先番号で検索
					cust = VariousDocumentOutAccess.Select_CustomerInfoByTokuisakiNo(textBoxTokuisakiNo.Text, Program.gSettings.Connect.Junp.ConnectionString);
				}
				else if (MwsDefine.CustomerNoLength == textBoxCustomerNo.Text.Length)
				{
					// 顧客Noで検索
					cust = VariousDocumentOutAccess.Select_CustomerInfoByCustomerNo(textBoxCustomerNo.ToInt(), Program.gSettings.Connect.Junp.ConnectionString);
				}
				if (null != cust)
				{
					Common.顧客情報 = cust;
					textBoxCustomerName.Text = cust.顧客名;
					if (cust.Enable)
					{
						// 出力可能
						ReportOutEnable(true);
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
			catch (Exception ex)
			{
				MessageBox.Show(this, string.Format("サーバー通信エラー：{0}", ex.Message), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
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
			if (null == Common.顧客情報)
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

				// Ver1.20(2023/06/09 勝呂):2-FAX送付状、3-種類送付状が販売店の時に出力できない
				switch (DocType)
				{
					/// <summary>
					/// MWS IDパスワード
					/// </summary>
					case DocumentOut.DocumentType.MwsIDPassword:
						if (false == radioMWSIDパスワード.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutMwsIDPassword(Common, xlsPathname);
						break;
					/// <summary>
					/// FAX送付状
					/// </summary>
					case DocumentOut.DocumentType.FaxLetter:
						if (false == radioFAX送付状.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutFaxLetter(Common, xlsPathname);
						break;
					/// <summary>
					/// 書類送付状
					/// </summary>
					case DocumentOut.DocumentType.DocumentLetter:
						if (false == radio書類送付状.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutDocumentLetter(Common, xlsPathname);
						break;
					/// <summary>
					/// 光ディスク請求届出
					/// </summary>
					case DocumentOut.DocumentType.LightDisk:
						if (false == radio光ディスク請求届出.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutLightDisk(Common, xlsPathname);
						break;
					/// <summary>
					/// オンライン請求届出
					/// </summary>
					case DocumentOut.DocumentType.OnlineApply:
						if (false == radioオンライン請求届出.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutOnline(Common, xlsPathname, orgPpathname);
						break;
					/// <summary>
					/// 取引条件確認書
					/// </summary>
					case DocumentOut.DocumentType.Transaction:
						if (false == radio取引条件確認書.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutTransaction(Common, xlsPathname);
						break;
					/// <summary>
					/// 登録データ確認カード
					/// </summary>
					case DocumentOut.DocumentType.ConfirmCard:
						if (false == radio登録データ確認カード.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutConfirmCard(Common, xlsPathname);
						break;
					/// <summary>
					/// Microsoft365利用申請書
					/// </summary>
					case DocumentOut.DocumentType.Microsoft365:
						if (false == radioMicrosoft365利用申請書.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutMicrosoft365(Common, xlsPathname);
						break;
					/// <summary>
					/// 請求先変更届
					/// </summary>
					case DocumentOut.DocumentType.SeikyuChange:
						if (false == radio請求先変更届.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutSeikyuChange(Common, xlsPathname);
						break;
					/// <summary>
					/// 終了届
					/// </summary>
					case DocumentOut.DocumentType.UserFinished:
						if (false == radio終了届.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutUserFinished(Common, xlsPathname);
						break;
					/// <summary>
					/// 変更届
					/// </summary>
					case DocumentOut.DocumentType.UserChange:
						if (false == radio変更届.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutUserChange(Common, xlsPathname);
						break;
					/// <summary>
					/// 第一園芸注文書
					/// </summary>
					case DocumentOut.DocumentType.FirstEngei:
						if (false == radio第一園芸注文書.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutFirstEngei(Common, xlsPathname);
						break;
					/// <summary>
					/// 納品補助作業依頼書
					/// </summary>
					case DocumentOut.DocumentType.Delivery:
						if (false == radio納品補助作業依頼書.Enabled)
						{
							return;
						}
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
						if (false == radioPC安心サポート加入申込書.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutPcSupport(Common, xlsPathname);
						break;
					/// <summary>
					/// アプラス預金口座振替依頼書・自動払込利用申込書
					/// </summary>
					case DocumentOut.DocumentType.Aplus:
						if (false == radioアプラス預金口座振替依頼書.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutAplus(Common, xlsPathname);
						break;
					/// <summary>
					/// 作業報告書
					/// </summary>
					case DocumentOut.DocumentType.WorkReport:
						if (false == radioButton作業報告書.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutWorkReport(Common, xlsPathname);
						break;
					/// <summary>
					/// 消耗品FAXオーダーシート
					/// </summary>
					/// Ver1.03(2021/09/02):消耗品FAXオーダーシートの新規追加
					case DocumentOut.DocumentType.FaxOrderSheet:
						if (false == radioButtonFaxOrderSheet.Enabled)
						{
							return;
						}
						DocumentOut.ExcelOutFaxOrderSheet(Common, xlsPathname);
						break;
					/// <summary>
					/// オンライン資格確認等事業完了報告書(経理部専用)
					/// </summary>
					/// Ver1.07(2021/12/24):経理部専用 オンライン資格確認等事業完了報告書の対応
					case DocumentOut.DocumentType.OnlineConfirmKeiri:
						{
							if (false == radioButtonOnlineConfirm.Enabled)
							{
								return;
							}
							// Ver1.18(2023/04/06 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 送付先リストから受注日を取得
							string destinationPathname = string.Empty;
							using (SelectDestinationFileForm dlg = new SelectDestinationFileForm())
							{
								if (DialogResult.OK == dlg.ShowDialog())
								{
									destinationPathname = dlg.DestinationPathname;
								}
							}
							送付先リスト destination = null;
							if (0 < destinationPathname.Length)
							{
								if (false == File.Exists(destinationPathname))
								{
									MessageBox.Show(string.Format("{0}ファイルが見つかりません。", destinationPathname), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
									return;
								}
								// 送付先リストの読込
								List<送付先リスト>  destinationList = 送付先リスト.ReadExcel送付先リスト(destinationPathname);
								if (null != destinationList && 0 < destinationList.Count)
								{
									destination = destinationList.Find(p => p.得意先No == Common.顧客情報.得意先No);
								}
							}
							try
							{
								// 顧客Noに対するオンライン資格確認対象商品売上明細情報の取得
								List<オンライン資格確認対象商品売上明細> detailList = VariousDocumentOutAccess.Select_オンライン資格確認対象商品売上明細(Common.顧客情報.得意先No, Program.gSettings.Connect.Junp.ConnectionString);

								// Ver1.15(2023/01/13):19-経理部専用 オンライン資格確認等事業完了報告書 注文確認書の追加、領収証および書類送付状の削除
								// [JunpDB].[dbo].[vMicオンライン資格確認ソフト改修費]の取得
								string whereStr = string.Format("顧客No = {0}", Common.顧客情報.顧客No);
								vMicオンライン資格確認ソフト改修費 soft = null;
								List<vMicオンライン資格確認ソフト改修費> softList = JunpDatabaseAccess.Select_vMicオンライン資格確認ソフト改修費(whereStr, "受注番号 ASC", Program.gSettings.Connect.Junp.ConnectionString);
								if (null != softList && 0 < softList.Count)
								{
									soft = softList[0];
								}
								DocumentOut.ExcelOutOnlineConfirm(Common, xlsPathname, orgPpathname, detailList, soft, destination);
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
