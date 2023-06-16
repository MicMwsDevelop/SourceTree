//
// MainForm.cs
// 
// オン資補助金申請書類顧客情報抽出 メイン画面フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/04/05 勝呂):新規作成
// Ver1.01(2023/04/13 勝呂):開設者が未設定の場合には院長名を出力する
//
using CommonLib.BaseFactory.Junp.View;
using CommonLib.DB.SqlServer.Junp;
using OnlineLicenseSubsidyCustomerList.BaseFactory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OnlineLicenseSubsidyCustomerList.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Text = string.Format("{0} ({1})", Program.ProgramName, Program.ProgramVersion);
		}

		/// <summary>
		/// オン資補助金申請書類顧客情報ファイルの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSelectCustomer_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "送付先リストを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxCusomerPathname.Text = dlg.FileName;
				}
			}
		}

		/// <summary>
		/// 送付先リストファイルの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSelectDestination_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "送付先リストを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxDestinationPathname.Text = dlg.FileName;
				}
			}
		}

		/// <summary>
		/// オン資補助金申請書類顧客情報ファイルの出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			if (0 == textBoxCusomerPathname.Text.Length)
			{
				MessageBox.Show("オン資補助金申請書類顧客情報を設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == File.Exists(textBoxCusomerPathname.Text))
			{
				MessageBox.Show(string.Format("{0}ファイルが見つかりません。", textBoxCusomerPathname.Text), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				List< 送付先リスト> destinationList = null;
				if (0 == textBoxDestinationPathname.Text.Length)
				{
					if (DialogResult.No == MessageBox.Show("送付先リストが指定されていません。よろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						return;
					}
				}
				else
				{
					if (false == File.Exists(textBoxDestinationPathname.Text))
					{
						MessageBox.Show(string.Format("{0}ファイルが見つかりません。", textBoxDestinationPathname.Text), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 送付先リストの読込
					destinationList = 送付先リスト.ReadExcel送付先リスト(textBoxDestinationPathname.Text);
				}
				List<オン資補助金申請書類顧客情報> customerList = オン資補助金申請書類顧客情報.ReadExcelオン資補助金申請書類顧客情報(textBoxCusomerPathname.Text);
				if (null != customerList && 0 < customerList.Count)
				{
					string numStr = オン資補助金申請書類顧客情報.GetNumberString(customerList);
					List<vMic全ユーザー3> userList = JunpDatabaseAccess.Select_vMic全ユーザー3(string.Format("[得意先No] in ({0})", numStr), "得意先No ASC", Program.gSettings.Junp.ConnectionString);
					if (null != userList && 0 < userList.Count)
					{
						// [JunpDB].[dbo].[vMicオンライン資格確認ソフト改修費]の取得
						List<vMicオンライン資格確認ソフト改修費> softList = JunpDatabaseAccess.Select_vMicオンライン資格確認ソフト改修費("", "顧客No ASC, 受注番号 ASC", Program.gSettings.Junp.ConnectionString);

						foreach (オン資補助金申請書類顧客情報 cust in customerList)
						{
							vMic全ユーザー3 user = userList.Find(p => p.得意先No == cust.得意先No);
							if (null != user)
							{
								cust.顧客名 = user.顧客名;
								cust.医療機関コード = user.NumericClinicCode;

								// Ver1.01(2023/04/13 勝呂):開設者が未設定の場合には院長名を出力する
								cust.開設者 = user.開設者名;
								if (0 == cust.開設者.Length)
								{
									cust.開設者 = user.院長名;
								}
								cust.郵便番号 = user.郵便番号;
								cust.住所 = user.住所;
								cust.電話番号 = user.電話番号;

								vMicオンライン資格確認ソフト改修費 soft = null;
								if (null != softList && 0 < softList.Count)
								{
									soft = softList.Find(p => p.顧客No == user.顧客No);
								}
								// 要件定義
								// (1) 受注日は送付先リストから取得する。送付先リストに該当する医院がない場合には、WWの受注伝票から受注日を取得する
								// (2) 金額はWWの受注伝票から金額を取得する。受注伝票が存在しない場合には、送付先リストから取得する
								if (null != soft)
								{
									cust.受注日 = soft.受注日;
									cust.金額 = (int)soft.受注金額税込;
								}
								if (null != destinationList && 0 < destinationList.Count)
								{
									送付先リスト destination = destinationList.Find(p => p.得意先No == cust.得意先No);
									if (null != destination)
									{
										cust.受注日 = destination.受注日;
										if (0 == cust.金額)
										{
											cust.金額 = destination.金額;
										}
									}
								}
							}
						}
						// オン資補助金申請書類顧客情報.xlsxの出力
						オン資補助金申請書類顧客情報.WriteExcelオン資補助金申請書類顧客情報(customerList, textBoxCusomerPathname.Text);

						// カーソルを元に戻す
						Cursor.Current = preCursor;

						MessageBox.Show(this, string.Format("{0} を出力しました。", textBoxCusomerPathname.Text), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);

						// Excelの起動
						using (Process process = new Process())
						{
							process.StartInfo.FileName = textBoxCusomerPathname.Text;
							process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
							process.Start();
						}
					}
					else
					{
						MessageBox.Show("顧客情報の取得に失敗しました。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
				else
				{
					MessageBox.Show("得意先Noが登録されていません。オン資補助金申請書類顧客情報をご確認ください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
