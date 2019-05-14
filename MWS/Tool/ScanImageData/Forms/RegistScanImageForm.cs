//
// RegistScanImageForm.cs
// 
// スキャナーイメージ登録画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/13 勝呂)
//
using MwsLib.BaseFactory.ScanImageData;
using MwsLib.Component;
using System;
using System.IO;
using System.Windows.Forms;

namespace ScanImageData.Forms
{
	/// <summary>
	/// スキャナーイメージ登録画面
	/// </summary>
	public partial class RegistScanImageForm : Form
	{
		public string ScanDataImagePath { get; set; }

		private string ScanDataImageFolder { get; set; }


		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private RegistScanImageForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="path"></param>
		public RegistScanImageForm(string path)
		{
			InitializeComponent();

			ScanDataImagePath = path;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegistScanImageForm_Load(object sender, EventArgs e)
		{
			// 登録・変更を選択
			radioButtonUser.Checked = true;
		}

		/// <summary>
		/// スキャンデータファイルダブルクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void explorerListViewScanImage_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (null == explorerTreeViewScanImage.SelectedNode)
			{
				return;
			}
			ExplorerListView targetItem = (ExplorerListView)sender;
			if (0 < targetItem.SelectedItems.Count)
			{
				ListViewItem item = explorerListViewScanImage.SelectedItems[0];
				ShellItem si = item.Tag as ShellItem;
				string ext = Path.GetExtension(si.Path);
				if (0 <= ext.Length)
				{
					using (DisplayScanImageForm form = new DisplayScanImageForm(si.Path))
					{
						form.ShowDialog();
					}
				}
			}
		}

		/// <summary>
		/// 登録・変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonUser_CheckedChanged(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			ScanDataImageFolder = Path.Combine(ScanDataImagePath, ScanImageDataDef.FolderToroku);
			explorerTreeViewScanImage.UIInit(ScanDataImageFolder);
			explorerListViewScanImage.UIInit(ScanDataImageFolder);

			//explorerTreeViewScanImage.SelectedNode = explorerTreeViewScanImage.TopNode;

			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// 保守契約
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMainte_CheckedChanged(object sender, EventArgs e)
		{
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			ScanDataImageFolder = Path.Combine(ScanDataImagePath, ScanImageDataDef.FolderHoshu);
			explorerTreeViewScanImage.UIInit(ScanDataImageFolder);
			explorerListViewScanImage.UIInit(ScanDataImageFolder);

			//explorerTreeViewScanImage.SelectedNode = explorerTreeViewScanImage.TopNode;

			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// 口座振替
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonAccountTransfer_CheckedChanged(object sender, EventArgs e)
		{
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			ScanDataImageFolder = Path.Combine(ScanDataImagePath, ScanImageDataDef.FolderKofuri);
			explorerTreeViewScanImage.UIInit(ScanDataImageFolder);
			explorerListViewScanImage.UIInit(ScanDataImageFolder);

			//explorerTreeViewScanImage.SelectedNode = explorerTreeViewScanImage.TopNode;

			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// 取引条件確認書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonTransaction_CheckedChanged(object sender, EventArgs e)
		{
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			ScanDataImageFolder = Path.Combine(ScanDataImagePath, ScanImageDataDef.FolderTransaction);
			explorerTreeViewScanImage.UIInit(ScanDataImageFolder);
			explorerListViewScanImage.UIInit(ScanDataImageFolder);

			//explorerTreeViewScanImage.SelectedNode = explorerTreeViewScanImage.TopNode;

			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// リモートサービス利用規約同意書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonConsent_CheckedChanged(object sender, EventArgs e)
		{
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			ScanDataImageFolder = Path.Combine(ScanDataImagePath, ScanImageDataDef.FolderRemote);
			explorerTreeViewScanImage.UIInit(ScanDataImageFolder);
			explorerListViewScanImage.UIInit(ScanDataImageFolder);

			//explorerTreeViewScanImage.SelectedNode = explorerTreeViewScanImage.TopNode;

			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}
	}
}
