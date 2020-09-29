using MwsLib.Common;
using System;
using System.IO;
using System.Windows.Forms;
using WebJucuOrderFile.Settings;

namespace WebJucuOrderFile.Forms
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
			textBoxFolder.Text = Program.gSettings.ExportDir;
			dateTimePickerOrderDate.Value = Date.Parse(Program.gSettings.OrderDate.Value).ToDateTime();
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			if (!Directory.Exists(textBoxFolder.Text))
			{
				MessageBox.Show("出力先が存在しません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Program.gSettings.ExportDir = textBoxFolder.Text;
			Program.gSettings.OrderDate = new Date(dateTimePickerOrderDate.Value).ToIntYMD();

			if (0 < Program.gSettings.Pathname.Length)
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// Web受注明細ファイルの出力
				string msg = Program.OutputCsvFile();

				// カーソルを元に戻す
				Cursor.Current = preCursor;
				if (0 == msg.Length)
				{
					MessageBox.Show(string.Format("{0}を出力しました。", Program.gSettings.Pathname), "出力成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show(msg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("出力先が設定されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// Form Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Directory.Exists(textBoxFolder.Text))
			{
				Program.gSettings.ExportDir = textBoxFolder.Text;
			}
			Program.gSettings.ExportDir = textBoxFolder.Text;
			Program.gSettings.OrderDate = new Date(dateTimePickerOrderDate.Value).ToIntYMD();
			WebJucuOrderFileSettingsIF.SetSettings(Program.gSettings);
		}
	}
}
