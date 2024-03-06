//
// MainForm.cs
//
// MWSサービス異常データ検出 メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/29 勝呂):新規作成
// 
using ClosedXML.Excel;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.BaseFactory.CheckMwsServiceIllegalData;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.CheckMwsServiceIllegalData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CheckMwsServiceIllegalData.Forms
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
			this.Text = string.Format("{0} Ver{1} {2}", Program.gProcName, Program.gVersionStr, Program.gSettings.ConnectCharlie.InstanceName);
		}

		/// <summary>
		/// 検出開始
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// orgファイル→Excelファイルをコピー
			string orgPpathname = Path.Combine(Directory.GetCurrentDirectory(), CheckUseCustomerInfo.OrgFilename);
			string xlsPathname = Path.Combine(Directory.GetCurrentDirectory(), string.Format(CheckUseCustomerInfo.ExcelFilename, Date.Today.ToIntYMD()));
			File.Copy(orgPpathname, xlsPathname, true);

			try
			{
				// 出力データ
				List<string[]> outputData = new List<string[]>();
				List<int> idList = CheckMwsServiceIllegalDataAccess.GetCuiCustomerIdList(Program.gSettings.ConnectCharlie.ConnectionString);
				if (null != idList && 0 < idList.Count)
				{
					// CUI抽出条件：終了フラグ=0、CUI削除フラグ=0
					List<CheckUseCustomerInfo> allCuiList = CheckMwsServiceIllegalDataAccess.GetCheckUseCustomerInfo(Program.gSettings.ConnectCharlie.ConnectionString);

					// 申込情報抽出条件：MWSユーザー、システム反映済フラグ=0、地図分析と3DentMovieを除く
					List<V_COUPLER_APPLY> allApplyList = CharlieDatabaseAccess.Select_V_COUPLER_APPLY("system_flg = 0 AND LEFT(cp_id, 3) = 'MWS' AND service_id not in (1028120, 1030120)", "cp_id, service_id, apply_id desc", Program.gSettings.ConnectCharlie.ConnectionString);
					foreach (int id in idList)
					{
						List<CheckUseCustomerInfo> cuiList = allCuiList.FindAll(p => p.CustomerID == id);
						if (0 < cuiList.Count)
						{
							foreach (CheckUseCustomerInfo cui in cuiList) 
							{
								V_COUPLER_APPLY apply = allApplyList.Find(p => p.customer_id == cui.CustomerID && p.service_id == cui.ServiceID);
								if (cui.IsIllegalData(apply))
								{
									outputData.Add(cui.GetOutputData(apply));
								}
							}
						}
					}
				}
				// エクセルファイルへの出力
				ExcelOut(xlsPathname, outputData);

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
				MessageBox.Show(ex.Message, Program.gProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			// アプリケーションの終了
			this.Close();
		}

		/// <summary>
		/// EXCEL出力
		/// </summary>
		/// <param name="pathname">Excelファイルパス名</param>
		private void ExcelOut(string pathname, List<string[]> outputData)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname))
				{
					IXLWorksheet ws = wb.Worksheet(CheckUseCustomerInfo.ExcelSheetName);
					int row = 2;
					foreach (string[] rowData in outputData)
					{
						int column = 1;
						foreach (string data in rowData)
						{
							ws.Cell(row, column).SetValue(data);
							column++;
						}
						row++;
					}
					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.gProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
