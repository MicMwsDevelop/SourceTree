//
// MakeIndexFileForm.cs
//
// インデックスファイル作成画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
// 
using MwsLib.BaseFactory.ScanImageManager;
using MwsLib.DB.SqlServer.ScanImageManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ScanImageManager.Forms
{
	public partial class MakeIndexFileForm : Form
	{
		/// <summary>
		/// スキャナーファイル
		/// </summary>
		private List<ScanImageFile> ScanFiles { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MakeIndexFileForm()
		{
			InitializeComponent();

			ScanFiles = new List<ScanImageFile>();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MakeIndexFileForm_Load(object sender, EventArgs e)
		{
			textBoxImagePath.Text = Program.gSettings.ImagePath;
		}

		/// <summary>
		/// 出力対象リストデータグリッドビュー設定
		/// </summary>
		private void SetDataGridViewIndex()
		{
			dataGridViewIndex.DataSource = null;
			dataGridViewIndex.Columns.Clear();
			dataGridViewIndex.Rows.Clear();

			dataGridViewIndex.DataSource = ScanFiles;
			dataGridViewIndex.Columns["FileName"].HeaderText = "ファイル名";
			dataGridViewIndex.Columns["FileName"].DisplayIndex = 0;
			dataGridViewIndex.Columns["FileName"].Width = 300;
			dataGridViewIndex.Columns["CustomerNo"].HeaderText = "顧客No";
			dataGridViewIndex.Columns["CustomerNo"].DisplayIndex = 1;
			dataGridViewIndex.Columns["CustomerNo"].Width = 50;
			dataGridViewIndex.Columns["TokuisakiNo"].HeaderText = "得意先No";
			dataGridViewIndex.Columns["TokuisakiNo"].DisplayIndex = 2;
			dataGridViewIndex.Columns["TokuisakiNo"].Width = 50;
			dataGridViewIndex.Columns["ClinicName"].HeaderText = "医院名";
			dataGridViewIndex.Columns["ClinicName"].DisplayIndex = 3;
			dataGridViewIndex.Columns["ClinicName"].Width = 300;
			dataGridViewIndex.Columns["FolderName"].Visible = false;
			dataGridViewIndex.Columns["FileDateTime"].Visible = false;
			dataGridViewIndex.Columns["Document"].Visible = false;
			dataGridViewIndex.Columns["Pathname"].Visible = false;
		}

		/// <summary>
		/// スキャンデータ登録パスの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputPath_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog form = new FolderBrowserDialog())
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				form.Description = "フォルダを指定してください。";
				form.RootFolder = Environment.SpecialFolder.Desktop;
				form.SelectedPath = textBoxImagePath.Text;
				form.ShowNewFolderButton = true;
				if (DialogResult.OK == form.ShowDialog(this))
				{
					textBoxImagePath.Text = textBoxImagePath.Text = form.SelectedPath;

					List<string> files = Directory.EnumerateFiles(textBoxImagePath.Text, "*.*", SearchOption.TopDirectoryOnly).ToList();
					files.Remove(Path.Combine(textBoxImagePath.Text, ScanImageManagerDef.TempThumbnailFile));
					files.Sort();

					dataGridViewIndex.DataSource = null;
					ScanFiles.Clear();
					if (0 == files.Count)
					{
						MessageBox.Show("画像ファイルが存在しません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					foreach (string file in files)
					{
						ScanImageFile scan = new ScanImageFile();
						scan.FileName = Path.GetFileName(file);
						scan.FolderName = textBoxImagePath.Text;
						scan.FileDateTime = File.GetLastWriteTime(scan.Pathname);
						ScanFiles.Add(scan);
					}
					// 出力対象リストデータグリッドビュー設定
					SetDataGridViewIndex();
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
		}

		/// <summary>
		/// 出力対象リスト変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewIndex_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (0 < ScanFiles.Count())
			{
				int index = dataGridViewIndex.SelectedRows[0].Index;
				ScanImageFile scan = dataGridViewIndex.SelectedRows[0].DataBoundItem as ScanImageFile;
				if (null != scan)
				{
					using (DisplayScanImageForm form = new DisplayScanImageForm())
					{
						form.ImageFile = scan;
						if (DialogResult.OK == form.ShowDialog(this))
						{
							ScanFiles[index] = form.ImageFile;

							// 出力対象リストデータグリッドビュー設定
							SetDataGridViewIndex();
						}
					}
				}
			}
		}

		/// <summary>
		/// 得意先番号推測
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonGuess_Click(object sender, EventArgs e)
		{
			if (0 == ScanFiles.Count())
			{
				MessageBox.Show("出力対象リストがありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (-1 == ScanFiles.FindIndex(p => 0 == p.TokuisakiNo.Length))
			{
				MessageBox.Show("得意先番号はすべて設定されています。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			bool success = false;
			foreach (ScanImageFile scan in ScanFiles)
			{
				if (0 == scan.TokuisakiNo.Length)
				{
					ScanImageFile work = ScanImageManagerAccess.GetCustomerInfo(scan.GetToluisakiNo(), Program.DATABACE_ACCEPT_CT);
					if (null != work)
					{
						scan.ClinicName = work.ClinicName;
						scan.CustomerNo = work.CustomerNo;
						scan.TokuisakiNo = work.TokuisakiNo;
						success = true;
					}
				}
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;

			if (success)
			{
				// 出力対象リストデータグリッドビュー設定
				SetDataGridViewIndex();
				MessageBox.Show("得意先番号から顧客情報を設定しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("ファイル名から得意先番号を推測できませんでした。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// 出力対象リスト初期化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClear_Click(object sender, EventArgs e)
		{
			dataGridViewIndex.DataSource = null;
			dataGridViewIndex.Columns.Clear();
			dataGridViewIndex.Rows.Clear();
			ScanFiles.Clear();
		}

		/// <summary>
		/// インデックスファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutput_Click(object sender, EventArgs e)
		{
			if (0 == ScanFiles.Count())
			{
				MessageBox.Show("出力対象がありません。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			List<string> output = new List<string>();
			output.Add("ＷＷ顧客Ｎｏ, 得意先No, 医院名");
			foreach (ScanImageFile scan in ScanFiles)
			{
				if (0 == scan.TokuisakiNo.Length)
				{
					MessageBox.Show("得意先番号が未確定のファイルがあります。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				output.Add(scan.Output());
			}
			if (1 < output.Count)
			{
				try
				{
					// 元のカーソルを保持
					Cursor preCursor = Cursor.Current;

					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;

					string filename = string.Format("{0}.txt", Path.GetFileName(textBoxImagePath.Text));
					using (var sw = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), filename), false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						foreach (string line in output)
						{
							sw.WriteLine(line);
						}
					}
					// カーソルを元に戻す
					Cursor.Current = preCursor;

					MessageBox.Show("インデックスファイルを出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
		}
	}
}
