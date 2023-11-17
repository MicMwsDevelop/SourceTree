//
// EarningsFileOutForm.cs
// 
// 電子処方箋管理サービス売上明細ファイル出力画面フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/02/14 勝呂):新規作成
//
using CommonLib.BaseFactory.PrescriptionManager;
using CommonLib.Common;
using CommonLib.DB.SqlServer.PrescriptionManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PrescriptionManager.Forms
{
	public partial class EarningsFileOutForm : Form
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EarningsFileOutForm()
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
			this.Text = string.Format("{0}  {1}", Program.PROC_NAME, Program.VersionStr);

			dateTimePickerMonth.Value = Program.gBootDate.ToDateTime();
			textBoxExportFolder.Text = Program.gSettings.ExportFolder;
			textBoxPcaVer.Text = Program.gSettings.PcaVersion.ToString();
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			if (!Directory.Exists(textBoxExportFolder.Text))
			{
				MessageBox.Show("出力先が存在しません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Program.gSettings.ExportFolder = textBoxExportFolder.Text;
			Program.gSettings.PcaVersion = textBoxPcaVer.ToInt();

			if (0 < Program.gSettings.ExportFilename.Length)
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 当月初日
				Date firstDate = new Date(dateTimePickerMonth.Value);

				try
				{
					// 電子処方箋契約情報から運用開始日が先月の医院を取得
					List<PrescriptionEarnings> pcaList = PrescriptionManagerAccess.GetPrescriptionEarnings(firstDate, Program.gSettings.ConnectCharlie.ConnectionString);

					// 売上明細ファイルの出力
					Program.ExportEarningsFile(firstDate, pcaList);

					MessageBox.Show(string.Format("{0}を出力しました。", Program.gSettings.FormalPathname(Program.gEarningsFilename)), "出力成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("売上明細ファイル名が設定されていません。\n環境設定ファイルをご確認ください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}
