//
// MainForm.cs
// 
// MWS課金バッチツール メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/08/23 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.MwsKakinBatchTool;
using MwsKakinBatchTool.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MwsKakinBatchTool.Forms
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
			this.Text = Program.ProcName;
			dateTimePickerExecDate.Value = DateTime.Now;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			if (false == checkBoxUse.Checked && false == checkBoxCancel.Checked && false == checkBoxMonthly.Checked)
			{
				return;
			}
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				LogOut.SetLogFileName(Directory.GetCurrentDirectory());

				// 今月末日
				DateTime thisMonth = dateTimePickerExecDate.Value.EndOfMonth();

				// 翌月末日
				DateTime nextMonth = dateTimePickerExecDate.Value.EndOfNextMonth();

				// 利用申込み反映
				if (checkBoxUse.Checked)
				{
					List<view_前月申込データ> useApplyList = MwsKakinBatchToolAccess.Select_サービス申込情報(dateTimePickerExecDate.Value, false, Program.gSettings.ConnectCharlie.ConnectionString);
					if (null != useApplyList)
					{
						foreach (view_前月申込データ apply in useApplyList)
						{
							List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(string.Format("CUSTOMER_ID = {0} AND SERVICE_ID = {1}", apply.customer_id, apply.service_id), "", Program.gSettings.ConnectCharlie.ConnectionString);
							if (null != cuiList && 0 < cuiList.Count)
							{
								// 更新
								T_CUSSTOMER_USE_INFOMATION cui = cuiList[0];
								if (cui.USE_END_DATE <= thisMonth)
								{
									cui.USE_END_DATE = nextMonth;  // 翌月末日
									cui.PAUSE_END_STATUS = false;
									cui.CANCELLATION_DAY = null;
									cui.CANCELLATION_PROCESSING_DATE = null;
									cui.UPDATE_DATE = DateTime.Today;
									cui.UPDATE_PERSON = Program.SectionName;
									cui.PERIOD_END_DATE = null;
									cui.RENEWAL_FLG = true;

									//CharlieDatabaseAccess.UpdateSet_T_CUSSTOMER_USE_INFOMATION(cui, Program.gSettings.ConnectCharlie.ConnectionString);

									LogOut.Out(string.Format("利用申込：No.{0} {1} 更新 {2}", cui.CUSTOMER_ID, cui.SERVICE_ID, apply.apply_date.Value.ToString("yyyy-MM-dd")));
								}
								else
								{
									LogOut.Out(string.Format("Warning！ 利用申込：No.{0} {1} {2} 利用中のサービスが利用申込みされている", cui.CUSTOMER_ID, cui.SERVICE_ID, apply.apply_date.Value.ToString("yyyy-MM-dd")));
								}
							}
							else
							{
								// 新規追加
								T_CUSSTOMER_USE_INFOMATION cui = new T_CUSSTOMER_USE_INFOMATION();
								cui.CUSTOMER_ID = apply.customer_id;
								cui.SERVICE_TYPE_ID = apply.service_type_id;
								cui.SERVICE_ID = apply.service_id;
								cui.USE_START_DATE = apply.apply_date;
								cui.USE_END_DATE = nextMonth;  // 翌月末日
								cui.CREATE_DATE = DateTime.Today;
								cui.CREATE_PERSON = Program.SectionName;
								cui.RENEWAL_FLG = true;

								//CharlieDatabaseAccess.InsertInto_T_CUSSTOMER_USE_INFOMATION(cui, Program.gSettings.ConnectCharlie.ConnectionString);

								LogOut.Out(string.Format("利用申込：No.{0} {1} 新規追加 {2}", cui.CUSTOMER_ID, cui.SERVICE_ID, apply.apply_date.Value.ToString("yyyy-MM-dd")));
							}
						}
					}
				}
				// 解約申込み反映
				if (checkBoxCancel.Checked)
				{
					List<view_前月申込データ> cancelApplyList = MwsKakinBatchToolAccess.Select_サービス申込情報(dateTimePickerExecDate.Value, true, Program.gSettings.ConnectCharlie.ConnectionString);
					if (null != cancelApplyList)
					{
						foreach (view_前月申込データ apply in cancelApplyList)
						{
							List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(string.Format("CUSTOMER_ID = {0} AND SERVICE_ID = {1}", apply.customer_id, apply.service_id), "", Program.gSettings.ConnectCharlie.ConnectionString);
							if (null != cuiList && 0 < cuiList.Count)
							{
								// 更新
								T_CUSSTOMER_USE_INFOMATION cui = cuiList[0];
								if (false == cui.PAUSE_END_STATUS || false == cui.PERIOD_END_DATE.HasValue)
								{
									cui.CANCELLATION_DAY = apply.apply_date;
									if (apply.apply_date.HasValue)
									{
										cui.CANCELLATION_PROCESSING_DATE = apply.apply_date.Value.EndOfMonth(); // 解約月の末日
									}
									else
									{
										cui.CANCELLATION_PROCESSING_DATE = null;
									}
									cui.PAUSE_END_STATUS = true;
									cui.UPDATE_DATE = DateTime.Today;
									cui.UPDATE_PERSON = Program.SectionName;
									cui.PERIOD_END_DATE = cui.USE_END_DATE;
									cui.RENEWAL_FLG = true;

									//CharlieDatabaseAccess.UpdateSet_T_CUSSTOMER_USE_INFOMATION(cui, Program.gSettings.ConnectCharlie.ConnectionString);

									LogOut.Out(string.Format("解約申込：No.{0} {1} {2}", cui.CUSTOMER_ID, cui.SERVICE_ID, apply.apply_date.Value.ToString("yyyy-MM-dd")));
								}
								else
								{
									LogOut.Out(string.Format("Warning！ 解約申込：No.{0} {1} {2} 解約済のサービスが解約申込みされている", cui.CUSTOMER_ID, cui.SERVICE_ID, apply.apply_date.Value.ToString("yyyy-MM-dd")));
								}
							}
							else
							{
								// 本来、レコードがあるはず
								LogOut.Out(string.Format("Warning！ 解約申込：No.{0} {1} {2} 顧客管理利用情報にレコードが存在しない", apply.customer_id, apply.service_id, apply.apply_date.Value.ToString("yyyy-MM-dd")));
							}
						}
					}
				}
				// 月額利用更新
				if (checkBoxMonthly.Checked)
				{
					List<T_CUSSTOMER_USE_INFOMATION> cuiList = MwsKakinBatchToolAccess.Select_T_CUSSTOMER_USE_INFOMATION_月額利用(nextMonth, Program.gSettings.ConnectCharlie.ConnectionString);
					if (null != cuiList)
					{
						// 更新
						T_CUSSTOMER_USE_INFOMATION cui = cuiList[0];
						cui.USE_END_DATE = nextMonth;  // 翌月末日
						cui.UPDATE_DATE = DateTime.Today;
						cui.UPDATE_PERSON = Program.SectionName;
						cui.PAUSE_END_STATUS = false;
						cui.PERIOD_END_DATE = null;
						cui.RENEWAL_FLG = true;

						//CharlieDatabaseAccess.UpdateSet_T_CUSSTOMER_USE_INFOMATION(cui, Program.gSettings.ConnectCharlie.ConnectionString);

						LogOut.Out(string.Format("月額利用更新：No.{0} {1}", cui.CUSTOMER_ID, cui.SERVICE_ID));
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("処理が終了しました。");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);

				LogOut.Out(string.Format("エラーが発生しました。{0}", ex.Message));
			}
		}
	}
}
