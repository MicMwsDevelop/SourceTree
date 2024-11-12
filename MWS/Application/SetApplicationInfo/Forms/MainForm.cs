//
// MainForm.cs
// 
// メイン画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/22 勝呂):新規作成
// Ver1.01(2024/11/12 勝呂):ライセンスキー追加対応 MICオンライン資格確認保守サービス DX推進課依頼
//
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.DB.SqlServer.Junp;
using SetApplicationInfo.BaseFactory;
using SetApplicationInfo.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SetApplicationInfo.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		public SetApplicationInfoSettings Settings { get; set; }

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
			// 環境設定の読込
			Settings = SetApplicationInfoSettingsIF.GetSettings();

			// プログラムタイトル設定
			this.Text = string.Format("{0} ({1}) {2}", Program.ProcName, Program.VersionStr, Settings.ConnectJunp.InstanceName);
		}

		/// <summary>
		/// アプリケーション情報設定ファイルの指定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
				dlg.Title = "アプリケーション情報設定ファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxInputFile.Text = dlg.FileName;
				}
			}
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			if (0 == textBoxInputFile.Text.Length)
			{
				MessageBox.Show("アプリケーション情報設定ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == File.Exists(textBoxInputFile.Text))
			{
				MessageBox.Show("アプリケーション情報設定ファイルが存在しません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				using (var sr = new StreamReader(textBoxInputFile.Text, Encoding.GetEncoding("Shift_JIS")))
				{
					List<ApplicationInfo> infoList = new List<ApplicationInfo>();
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						if (';' != line[0])
						{
							// コメント行以外
							ApplicationInfo info = new ApplicationInfo();
							if (false == info.SetData(line))
							{
								MessageBox.Show("アプリケーション情報のファイル内容が正しくありません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							infoList.Add(info);
						}
					}
					if (0 == infoList.Count)
					{
						MessageBox.Show("アプリケーション情報のファイルに設定内容がありません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					try
					{
						foreach (ApplicationInfo info in infoList)
						{
							tMikアプリケーション情報 data = new tMikアプリケーション情報();
							data.faiCliMicID = info.CustomerNo;
							//data.faiアプリケーションNo = 0;
							data.faiアプリケーション名 = info.AplNo;

							// Ver1.01(2024/11/12 勝呂):ライセンスキー追加対応 MICオンライン資格確認保守サービス DX推進課依頼
							data.faiLicensedKey = info.LicensedKey;

							//data.faiVersion情報 = string.Empty;
							//data.faiオプション1 = string.Empty;
							//data.faiオプション2 = string.Empty;
							//data.faiオプション3 = string.Empty;
							//data.fai登録ｶｰﾄﾞ回収日 = string.Empty;
							data.fai保守 = true;
							//data.fai契約書回収年月 = string.Empty;
							//data.fai保守料金 = null;
							data.fai保守契約開始 = info.StartYM.Value.ToString();
							data.fai保守契約終了 = info.EndYM.Value.ToString();
							data.fai保守契約備考 = info.Marks;
							//data.fai終了フラグ = false;
							//data.fai更新日 = DateTime.Now;
							data.fai更新者 = Program.ProcName;
#if !DebugNoWrite
							JunpDatabaseAccess.InsertInto_tMikアプリケーション情報(data, Settings.ConnectJunp.ConnectionString);
#endif
						}
						MessageBox.Show("アプリケーション情報を設定しました。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex2)
					{
						MessageBox.Show(ex2.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			catch (Exception ex1)
			{
				MessageBox.Show(string.Format("ファイルオープンエラー({0})", ex1.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}
	}
}
