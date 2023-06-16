//
// SelectDestinationFileForm.cs
// 
// 送付先リスト設定画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.18(2023/04/06 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 送付先リストから受注日を取得
//
using System;
using System.IO;
using System.Windows.Forms;

namespace VariousDocumentOut.Forms
{
	/// <summary>
	/// 拠点選択フォーム
	/// </summary>
	public partial class SelectDestinationFileForm : Form
	{
		/// <summary>
		/// 送付先リストファイルパス名
		/// </summary>
		public string DestinationPathname { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SelectDestinationFileForm()
		{
			InitializeComponent();

			DestinationPathname = string.Empty;
		}

		/// <summary>
		/// 送付先リストの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSelectFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "送付先リストを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxPathname.Text = dlg.FileName;
				}
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			DestinationPathname = textBoxPathname.Text;
			this.Close();
		}
	}
}
