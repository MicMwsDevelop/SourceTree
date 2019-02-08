//
// EntryFinishedUserForm.cs
//
// 終了ユーザー登録画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/12 勝呂)
// 
using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.Common;
using MwsLib.DB.SqlServer.EntryFinishedUser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace EntryFinishedUser.Forms
{
	/// <summary>
	/// 終了ユーザー登録画面
	/// </summary>
	public partial class EntryFinishedUserForm : Form
	{
		/// <summary>
		/// 終了ユーザー情報
		/// </summary>
		private EntryFinishedUserData FinishedUser;

		/// <summary>
		/// 更新フラグ
		/// </summary>
		private bool ModifyFlag;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EntryFinishedUserForm()
		{
			InitializeComponent();

			FinishedUser = null;
			ModifyFlag = false;
		}

		///// <summary>
		///// コンストラクタ
		///// </summary>
		///// <param name="user">終了ユーザー情報</param>
		///// <param name="modify">更新フラグ</param>
		//public EntryFinishedUserForm(EntryFinishedUserData user, bool modify)
		//{
		//	InitializeComponent();

		//	FinishedUser = user;
		//	ModifyFlag = modify;
		//}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EntryFinishedUserForm_Load(object sender, EventArgs e)
		{
			// リプレース
			comboBoxReplace.Items.AddRange(Program.gReplaceList.ToArray());
/*
			// 得意先Noの設定
			textBoxTokuisakiID.Text = FinishedUser.TokuisakiNo;

			// 顧客名の設定
			textBoxUserName.Text = FinishedUser.UserName;

			// 終了月の設定
			if (FinishedUser.FinishedYearMonth.HasValue)
			{
				dateTimePickerFinishedYearMonth.Checked = true;
				dateTimePickerFinishedYearMonth.Value = FinishedUser.FinishedYearMonth.Value.First.ToDateTime();
			}
			// リプレースの設定
			comboBoxReplace.Text = FinishedUser.Replace;

			// 終了事由の設定
			comboBoxFinishedReason.Text = FinishedUser.FinishedReason;

			// 理由の設定
			textBoxReason.Text = FinishedUser.Reason;

			// 終了届受領日の設定
			if (FinishedUser.AcceptDate.HasValue)
			{
				dateTimePickerAcceptDate.Checked = true;
				dateTimePickerAcceptDate.Value = FinishedUser.AcceptDate.Value.ToDateTime();
			}
			// 非paletteユーザー
			checkBoxNonPaletteUser.Checked = FinishedUser.NonPaletteUser;
*/
		}

		///// <summary>
		///// 得意先No 入力制限
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void textBoxTokuisakiID_KeyPress(object sender, KeyPressEventArgs e)
		//{
		//	// 0～9と、バックスペース以外の時は、イベントをキャンセルする
		//	if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
		//	{
		//		e.Handled = true;
		//	}
		//}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			string tokuisakiNo = textBoxTokuisakiID.Text.Trim();
			if (6 == tokuisakiNo.Length)
			{
				try
				{
					FinishedUser = EntryFinishedUserAccess.GetCustomerInfo(tokuisakiNo, Program.DATABACE_ACCEPT_CT);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "顧客情報取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
				if (null != FinishedUser)
				{
					// 顧客名の設定
					textBoxUserName.Text = FinishedUser.UserName;

					// 終了月の設定
					if (FinishedUser.FinishedYearMonth.HasValue)
					{
						dateTimePickerFinishedYearMonth.Checked = true;
						dateTimePickerFinishedYearMonth.Value = FinishedUser.FinishedYearMonth.Value.First.ToDateTime();
						ModifyFlag = true;
					}
					else
					{
						dateTimePickerFinishedYearMonth.Checked = false;
						ModifyFlag = false;
					}
					// リプレースの設定
					comboBoxReplace.Text = FinishedUser.Replace;

					// 終了事由の設定
					comboBoxFinishedReason.Text = FinishedUser.FinishedReason;

					// 理由の設定
					textBoxReason.Text = FinishedUser.Reason;

					// 終了届受領日の設定
					if (FinishedUser.AcceptDate.HasValue)
					{
						dateTimePickerAcceptDate.Checked = true;
						dateTimePickerAcceptDate.Value = FinishedUser.AcceptDate.Value.ToDateTime();
					}
					else
					{
						dateTimePickerAcceptDate.Checked = false;
					}
					// 非paletteユーザー
					checkBoxNonPaletteUser.Checked = FinishedUser.NonPaletteUser;
				}
				else
				{
					MessageBox.Show("得意先Noに該当するユーザーが存在しません。", "検索エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

					buttonClear.PerformClick();
				}
			}
		}

		/// <summary>
		/// クリア
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClear_Click(object sender, EventArgs e)
		{
			// 顧客名の設定
			textBoxUserName.Text = string.Empty;

			// 終了月の設定
			dateTimePickerFinishedYearMonth.Checked = false;

			// リプレースの設定
			comboBoxReplace.Text = string.Empty;

			// 終了事由の設定
			comboBoxFinishedReason.Text = string.Empty;

			// 理由の設定
			textBoxReason.Text = string.Empty;

			// 終了届受領日の設定
			dateTimePickerAcceptDate.Checked = false;

			// 非paletteユーザー
			checkBoxNonPaletteUser.Checked = false;

			ModifyFlag = false;
		}

		/// <summary>
		/// 終了ユーザーリスト参照
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonShowList_Click(object sender, EventArgs e)
		{
			List<EntryFinishedUserData> list = EntryFinishedUserAccess.GetEntryFinishedUserDataList(Program.DATABACE_ACCEPT_CT);
			if (0 < list.Count)
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// Excelオブジェクトの初期化
				Excel.Application xlApp = null;
				Excel.Workbooks xlBooks = null;
				Excel.Workbook xlBook = null;
				Excel.Sheets xlSheets = null;
				Excel.Worksheet xlSheet = null;

				try
				{
					// Excelシートのインスタンスを作る
					xlApp = new Excel.Application();
					xlBooks = xlApp.Workbooks;
					xlBook = xlBooks.Add();
					xlSheets = xlBook.Sheets;
					xlSheet = xlSheets[1];
					xlSheet.Name = "終了ユーザーリスト";
					xlSheet.Select(Type.Missing);
					xlSheet.Cells.NumberFormat = "@";

					xlApp.Visible = false;

					// フィールド名を設定
					xlSheet.Cells[1, 1].Value2 = EntryFinishedUserData.FieldName[0];
					xlSheet.Cells[1, 2].Value2 = EntryFinishedUserData.FieldName[1];
					xlSheet.Cells[1, 3].Value2 = EntryFinishedUserData.FieldName[2];
					xlSheet.Cells[1, 4].Value2 = EntryFinishedUserData.FieldName[3];
					xlSheet.Cells[1, 5].Value2 = EntryFinishedUserData.FieldName[4];
					xlSheet.Cells[1, 6].Value2 = EntryFinishedUserData.FieldName[5];
					xlSheet.Cells[1, 7].Value2 = EntryFinishedUserData.FieldName[6];
					xlSheet.Cells[1, 8].Value2 = EntryFinishedUserData.FieldName[7];
					xlSheet.Cells[1, 9].Value2 = EntryFinishedUserData.FieldName[8];
					xlSheet.Cells[1, 10].Value2 = EntryFinishedUserData.FieldName[9];
					xlSheet.Cells[1, 11].Value2 = EntryFinishedUserData.FieldName[10];
					xlSheet.Cells[1, 12].Value2 = EntryFinishedUserData.FieldName[11];
					xlSheet.Cells[1, 13].Value2 = EntryFinishedUserData.FieldName[12];
					xlSheet.Cells[1, 14].Value2 = EntryFinishedUserData.FieldName[13];
					xlSheet.Cells[1, 15].Value2 = EntryFinishedUserData.FieldName[14];
					xlSheet.Cells[1, 16].Value2 = EntryFinishedUserData.FieldName[15];
					xlSheet.Cells[1, 17].Value2 = EntryFinishedUserData.FieldName[16];

					// エクセルファイルにデータをセットする
					for (int i = 0; i < list.Count; i++)
					{
						// Excelのcell指定
						EntryFinishedUserData data = list[i];
						xlSheet.Cells[i + 2, 1].Value2 = data.CustomerID;
						xlSheet.Cells[i + 2, 2].Value2 = data.TokuisakiNo;
						xlSheet.Cells[i + 2, 3].Value2 = data.UserName;
						xlSheet.Cells[i + 2, 4].Value2 = data.SystemName;
						xlSheet.Cells[i + 2, 5].Value2 = data.AreaCode;
						xlSheet.Cells[i + 2, 6].Value2 = data.AreaName;
						xlSheet.Cells[i + 2, 7].Value2 = data.KenName;
						xlSheet.Cells[i + 2, 8].Value2 = data.FinishedReason;
						xlSheet.Cells[i + 2, 9].Value2 = data.Replace;
						xlSheet.Cells[i + 2, 10].Value2 = data.Reason;
						xlSheet.Cells[i + 2, 11].Value2 = data.Comment;
						xlSheet.Cells[i + 2, 12].Value2 = data.EnableUserFlag;
						xlSheet.Cells[i + 2, 13].Value2 = data.Expcet;
						xlSheet.Cells[i + 2, 14].Value2 = data.HanbaitenID;
						xlSheet.Cells[i + 2, 15].Value2 = data.HanbaitenName;
						xlSheet.Cells[i + 2, 16].Value2 = data.FinishedYearMonth.ToString();
						xlSheet.Cells[i + 2, 17].Value2 = data.NonPaletteUser.ToString();
					}
					// Excelファイルの保存
					string xlsPathname = Path.Combine(Directory.GetCurrentDirectory(), @"終了ユーザーリスト.xlsx");
					xlBook.SaveAs(xlsPathname);
					//xlBook.Close(false);

					// Excel を表示する
					xlApp.Visible = true;

					// 1000 ミリ秒 (1秒) 待機する
					//System.Threading.Thread.Sleep(1000);

					//xlApp.Quit();

					// COM オブジェクトの参照カウントを解放する (正しくは COM オブジェクトの参照カウントを解放する を参照)
					//Marshal.ReleaseComObject(xlBooks);

					// カーソルを元に戻す
					Cursor.Current = preCursor;

					//MessageBox.Show(string.Format("エクセルファイルに出力しました。({0})", xlsPathname), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				finally
				{
					// Excelのオブジェクトを開放し忘れているとプロセスが落ちないため注意
					Marshal.ReleaseComObject(xlSheet);
					Marshal.ReleaseComObject(xlSheets);
					Marshal.ReleaseComObject(xlBook);
					Marshal.ReleaseComObject(xlBooks);
					Marshal.ReleaseComObject(xlApp);
					xlSheet = null;
					xlSheets = null;
					xlBook = null;
					xlBooks = null;
					xlApp = null;

					GC.Collect();
				}
			}
		}

		/// <summary>
		/// 登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (false == dateTimePickerFinishedYearMonth.Checked)
			{
				MessageBox.Show("終了月が入力されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == dateTimePickerAcceptDate.Checked)
			{
				MessageBox.Show("終了届受領日が入力されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 終了月の設定
			FinishedUser.FinishedYearMonth = new Date(dateTimePickerFinishedYearMonth.Value).ToYearMonth();

			// 終了届受領日の設定
			FinishedUser.AcceptDate = new Date(dateTimePickerAcceptDate.Value);

			// 終了事由の設定
			FinishedUser.FinishedReason = comboBoxFinishedReason.SelectedItem as string;

			// リプレースの設定
			FinishedUser.Replace = comboBoxReplace.Text;

			// 理由の設定
			FinishedUser.Reason = textBoxReason.Text;

			// 非paletteユーザー
			FinishedUser.NonPaletteUser = checkBoxNonPaletteUser.Checked;

			if (ModifyFlag)
			{
				// 更新
				try
				{
					EntryFinishedUserAccess.UpdateEntryFinishedUser(FinishedUser, Program.DATABACE_ACCEPT_CT);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "終了ユーザー情報更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
			else
			{
				// 新規追加
				try
				{
					EntryFinishedUserAccess.InsertIntoEntryFinishedUser(FinishedUser, Program.DATABACE_ACCEPT_CT);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "終了ユーザー情報追加エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
			MessageBox.Show("登録しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

			if (DialogResult.Yes == MessageBox.Show("メモ登録をします", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				try
				{
					EntryFinishedUserAccess.InsertIntoMemo(FinishedUser, Program.DATABACE_ACCEPT_CT);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "メモ登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
				MessageBox.Show("メモ登録をしました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("メモ登録をキャンセルしました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			base.DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
