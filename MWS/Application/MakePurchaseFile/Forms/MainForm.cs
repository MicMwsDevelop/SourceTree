//
// MainForm.cs
// 
// 仕入データ作成 メイン画面フォームファイル
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/07 勝呂)
// Ver1.02 汎用データレイアウト 仕入明細データ Version 9(Rev3.00)に対応(2022/05/25 勝呂)
// Ver1.03 経理部の要請により、Microsoft365仕入データを部門毎の集計を止めて、得意先に関する記事データを追加(2023/02/10 勝呂)
// Ver1.05(2023/07/10 勝呂):Curline本体アプリ仕入データファイル出力廃止対応
//
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.MakePurchaseFile;
using CommonLib.BaseFactory.Pca;
using CommonLib.Common;
using CommonLib.DB.SqlServer;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.MakePurchaseFile;
using MakePurchaseFile.Settings;
using MwsLib.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MakePurchaseFile.Forms
{
	public partial class MainForm : Form
	{
		// pinvoke:
		private const int DTM_GETMONTHCAL = 0x1000 + 8;
		private const int MCM_SETCURRENTVIEW = 0x1000 + 32;

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
		
		/// <summary>
		/// 環境設定
		/// </summary>
		public MakePurchaseFileSettings Settings { get; set; }

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
			// バージョン情報設定
			labelversion.Text = Program.gVersionStr;

			// 環境設定の読込
			Settings = PurchaseTransferSettingsIF.GetSettings();

			// 仕入商品情報テーブルの削除
			if (!DropTable())
			{
				return;
			}
			// 仕入商品情報テーブルの作成
			if (!CreateTable())
			{
				return;
			}
			// 仕入商品情報の登録
			if (!InsertIntoGoods())
			{
				return;
			}
			// 先月初日を設定
			dateTimePickerTarget.Value = Date.Today.FirstDayOfLasMonth().ToDateTime();

			// 環境設定内容の表示
			textBoxOutputFolder.Text = Settings.出力先フォルダ;
			textBoxListonFilename.Text = Settings.りすとん月額仕入データファイル名;
			textBoxMicrosoft365Filename.Text = Settings.Microsoft365仕入データファイル名;
			textBoxMonshindenFilename.Text = Settings.問心伝月額仕入データファイル名;

			// Ver1.05(2023/07/10 勝呂):Curline本体アプリ仕入データファイル出力廃止対応
			//textBoxCurlineFilename.Text = Settings.Curline本体アプリ仕入データファイル名;

			textBoxNarcohmFilename.Text = Settings.ナルコーム仕入データファイル名;
			textBoxCloudBackupFilename.Text = Settings.クラウドバックアップ仕入データファイル名;
			textBoxAlmexFilename.Text = Settings.アルメックス保守仕入データファイル名;

			// Ver1.02 汎用データレイアウト 仕入明細データ Version 9(Rev3.00)に対応(2022/05/25 勝呂)
			textBoxPcaVersion.Text = Settings.PcaVersion.ToString();

#if DEBUG
			buttonOutputListon.Visible = true;
			buttonOutputMicrosoft365.Visible = true;
			buttonOutputMonshinden.Visible = true;

			// Ver1.05(2023/07/10 勝呂):Curline本体アプリ仕入データファイル出力廃止対応
			//buttonOutputCurline.Visible = true;

			buttonOutputNarcohm.Visible = true;
			buttonPutputCloudBackup.Visible = true;
			buttonOutputAlmex.Visible = true;
#endif
		}

		/// <summary>
		/// Frop Down
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateTimePickerTarget_DropDown(object sender, EventArgs e)
		{
			DateTimePicker myDt = (DateTimePicker)sender;

			IntPtr cal = SendMessage(dateTimePickerTarget.Handle, DTM_GETMONTHCAL, IntPtr.Zero, IntPtr.Zero);
			SendMessage(cal, MCM_SETCURRENTVIEW, IntPtr.Zero, (IntPtr)1);
		}

		/// <summary>
		/// 仕入明細データファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStart_Click(object sender, EventArgs e)
		{
			// 対象年月
			YearMonth collectMonth = new YearMonth(dateTimePickerTarget.Value.Year, dateTimePickerTarget.Value.Month);

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				/////////////////////////////////////
				// 7. りすとん月額仕入データ作成

				りすとん月額仕入データファイル出力(collectMonth);

				/////////////////////////////////////
				// 8. Microsoft365仕入データ作成

				Microsoft365仕入データファイル出力(collectMonth);

				/////////////////////////////////////
				// 9. 問心伝月額仕入データ作成

				問心伝月額仕入データファイル出力(collectMonth);

				/////////////////////////////////////
				// 11. Curline本体アプリ仕入データ作成

				// Ver1.05(2023/07/10 勝呂):Curline本体アプリ仕入データファイル出力廃止対応
				//Curline本体アプリ仕入データファイル出力(collectMonth);

				/////////////////////////////////////
				// 12. ナルコーム仕入データ作成

				ナルコーム仕入データファイル出力(collectMonth);

				/////////////////////////////////////
				// 13. クラウドバックアップ仕入データ作成

				クラウドバックアップ仕入データファイル出力(collectMonth);

				/////////////////////////////////////
				// 14. アルメックス保守仕入データ作成

				YearMonth thisYM = collectMonth + 1;    // 対象月を当月初日に変更
				string msg = アルメックス保守仕入データファイル出力(thisYM);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				if (0 == msg.Length)
				{
					MessageBox.Show("仕入データを出力しました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show(string.Format("{0}\nERROR.LOGを確認してください。", msg), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "仕入データ出力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Form Closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
//#if !DEBUG
			DropTable();
//#endif
		}

		/// <summary>
		/// 仕入商品情報テーブルの削除
		/// </summary>
		public bool DropTable()
		{
			try
			{
				// TMP_Curline本体アプリ商品
				// Ver1.05(2023/07/10 勝呂):Curline本体アプリ仕入データファイル出力廃止対応
				//DatabaseAccess.DropTable(仕入商品情報.DropTableString_Curline本体アプリ商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_Microsoft365商品
				DatabaseAccess.DropTable(仕入商品情報.DropTableString_Microsoft365商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_りすとん月額商品
				DatabaseAccess.DropTable(仕入商品情報.DropTableString_りすとん月額商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_問心伝月額商品
				DatabaseAccess.DropTable(仕入商品情報.DropTableString_問心伝月額商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_ナルコーム商品
				DatabaseAccess.DropTable(ナルコーム仕入商品情報.DropTableString, Settings.Connect.Charlie.ConnectionString);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "DropTable実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 仕入商品情報テーブルの新規作成
		/// </summary>
		public bool CreateTable()
		{
			try
			{
				// TMP_Curline本体アプリ商品
				// Ver1.05(2023/07/10 勝呂):Curline本体アプリ仕入データファイル出力廃止対応
				//DatabaseAccess.CreateTable(仕入商品情報.CreateTableString_Curline本体アプリ商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_Microsoft365商品
				DatabaseAccess.CreateTable(仕入商品情報.CreateTableString_Microsoft365商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_りすとん月額商品
				DatabaseAccess.CreateTable(仕入商品情報.CreateTableString_りすとん月額商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_問心伝月額商品
				DatabaseAccess.CreateTable(仕入商品情報.CreateTableString_問心伝月額商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_ナルコーム商品
				DatabaseAccess.CreateTable(ナルコーム仕入商品情報.CreateTableString, Settings.Connect.Charlie.ConnectionString);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "CreateTable実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 商品登録
		/// </summary>
		/// <returns></returns>
		public bool InsertIntoGoods()
		{
			try
			{
				// TMP_Curline本体アプリ商品
				// Ver1.05(2023/07/10 勝呂):Curline本体アプリ仕入データファイル出力廃止対応
				//MakePurchaseFileAccess.InsertIntoList_Curline本体アプリ商品(Settings.Curline本体アプリ商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_Microsoft365商品
				MakePurchaseFileAccess.InsertIntoList_Microsoft365商品(Settings.Microsoft365商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_りすとん月額商品
				MakePurchaseFileAccess.InsertIntoList_りすとん月額商品(Settings.りすとん月額商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_問心伝月額商品
				MakePurchaseFileAccess.InsertIntoList_問心伝月額商品(Settings.問心伝月額商品, Settings.Connect.Charlie.ConnectionString);

				// TMP_ナルコーム商品
				MakePurchaseFileAccess.InsertIntoList_ナルコーム商品(Settings.ナルコーム商品, Settings.Connect.Charlie.ConnectionString);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "InsertIntoGoods実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 7.りすとん月額仕入データ作成
		/// </summary>
		/// <param name="collectMonth">対象年月</param>
		public void りすとん月額仕入データファイル出力(YearMonth collectMonth)
		{
			// (1)りすとん月額仕入振替月次 選択クエリの実行：7 りすとん月額仕入振替月次.sql
			List<仕入集計> りすとん月額仕入集計 = MakePurchaseFileAccess.Select_りすとん月額仕入集計(collectMonth, Settings.Connect.Junp.ConnectionString);

			// (2)プラス分振替データ出力
			List<汎用データレイアウト仕入明細データ> outputList = new List<汎用データレイアウト仕入明細データ>();

			int denNo = Settings.りすとん月額開始伝票番号;
			string bumonCode = string.Empty;
			foreach (仕入集計 data in りすとん月額仕入集計)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				汎用データレイアウト仕入明細データ pca = new 汎用データレイアウト仕入明細データ();
				pca.入荷方法 = 0;   // 0:通常仕入
				pca.仕入日 = collectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = pca.仕入日;
				pca.伝票No = denNo;
				pca.仕入先コード = data.仕入先;
				pca.仕入先名 = string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = "0";
				pca.摘要コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 0;  // 仕入
				pca.倉庫コード = "0";
				pca.数量 = data.数量;
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				pca.備考 = "0";
				pca.規格型番 = string.Empty;
				pca.色 = string.Empty;
				pca.サイズ = string.Empty;
				pca.税率 = data.sykd_rate;
				pca.ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
				pca.伝票No2 = string.Empty;
				pca.商品名2 = string.Empty;
				outputList.Add(pca);
			}
			// (3)\\SQLSV\PCADATAにりすとん月額振替仕入データファイルの新規作成
			// 環境設定.りすとん月額振替出力パス名：\\SQLSV\PCADATA\りすとん月額振替仕入データ.txt
			using (var sw = new StreamWriter(Settings.りすとん月額仕入データパス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (汎用データレイアウト仕入明細データ pca in outputList)
				{
					string record = pca.ToCsvString(Settings.PcaVersion);
					sw.WriteLine(record);
				}
			}
		}

		/// <summary>
		/// 8.Microsoft365仕入データ作成
		/// </summary>
		/// <param name="collectMonth">対象年月</param>
		public void Microsoft365仕入データファイル出力(YearMonth collectMonth)
		{
			int denNo = Settings.Microsoft365開始伝票番号;
			List<汎用データレイアウト仕入明細データ> outputList = new List<汎用データレイアウト仕入明細データ>();

			// Ver1.03 経理部の要請により、Microsoft365仕入データを部門毎の集計を止めて、得意先に関する記事データを追加(2023/02/10 勝呂)
			List<売上明細> Microsoft365売上明細 = MakePurchaseFileAccess.Select_Microsoft365売上明細(collectMonth, Settings.Connect.Junp.ConnectionString);
			foreach (売上明細 meisai in Microsoft365売上明細)
			{
				// 仕入データ
				汎用データレイアウト仕入明細データ pca = new 汎用データレイアウト仕入明細データ();
				pca.入荷方法 = 0;   // 0:通常仕入
				pca.仕入日 = collectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = collectMonth.First.FirstDayOfNextMonth().ToIntYMD(); // 対象月翌月初日
				pca.伝票No = denNo;
				pca.仕入先コード = meisai.仕入先;
				pca.仕入先名 = string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = meisai.sykd_jbmn.Substring(1);
				pca.担当者コード = "0";
				pca.摘要コード = "0";
				pca.摘要名 = meisai.摘要;
				pca.マスター区分 = 0; // 0:一般商品、1:雑商品、2:諸雑 費、3:値引、4:記事
				pca.商品コード = meisai.商品コード;
				pca.商品名 = meisai.商品名;
				pca.区 = 0;  // 仕入
				pca.倉庫コード = "0";
				pca.数量 = meisai.数量;
				pca.単位 = string.Empty;

				// Ver1.04(2023/03/30 勝呂):Microsoft365仕入データの単価が仕入価格でなく、標準価格となっている障害
				//pca.単価 = meisai.単価;
				pca.単価 = meisai.仕入価格;

				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				pca.備考 = "0";
				pca.規格型番 = string.Empty;
				pca.色 = string.Empty;
				pca.サイズ = string.Empty;
				pca.税率 = meisai.消費税率;
				pca.ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
				pca.伝票No2 = string.Empty;
				pca.商品名2 = string.Empty;
				outputList.Add(pca);

				string tokuisakiNo = meisai.得意先番号;
				string tokuisakiName = meisai.GetCustmerName();
				売上明細記事データ 記事データ = MakePurchaseFileAccess.Select_Microsoft365売上明細記事データ(meisai.伝票No, meisai.売上日, Settings.Connect.Junp.ConnectionString);
				if (null != 記事データ)
				{
					// 請求先と得意先が違う
					tokuisakiNo = 記事データ.得意先番号;
					tokuisakiName = 記事データ.GetCustmerName();
				}
				// 記事データ（得意先番号）
				汎用データレイアウト仕入明細データ article1 = new 汎用データレイアウト仕入明細データ();
				article1.入荷方法 = pca.入荷方法;
				article1.仕入日 = pca.仕入日;
				article1.精算日 = pca.精算日;
				article1.伝票No = pca.伝票No;
				article1.仕入先コード = pca.仕入先コード;
				article1.仕入先名 = pca.仕入先名;
				article1.先方担当者名 = pca.先方担当者名;
				article1.部門コード = pca.部門コード;
				article1.担当者コード = pca.担当者コード;
				article1.摘要コード = pca.摘要コード;
				article1.摘要名 = pca.摘要名;
				article1.商品コード = PcaGoodsIDDefine.ArticleCode;	// 000014
				article1.マスター区分 = 4; // 0:一般商品、1:雑商品、2:諸雑 費、3:値引、4:記事
				article1.商品名 = string.Format("得意先No.{0}", tokuisakiNo);
				article1.区 = pca.区;
				article1.倉庫コード = pca.倉庫コード;
				article1.数量 = 0;
				article1.単位 = string.Empty;
				article1.単価 = 0;
				article1.金額 = 0;
				article1.税区分 = 0;
				article1.備考 = "0";
				article1.規格型番 = string.Empty;
				article1.色 = string.Empty;
				article1.サイズ = string.Empty;
				article1.税率 = 0;
				article1.ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
				article1.伝票No2 = string.Empty;
				article1.商品名2 = string.Empty;
				outputList.Add(article1);

				// 記事データ（得意先名）
				汎用データレイアウト仕入明細データ article2 = new 汎用データレイアウト仕入明細データ();
				article2.入荷方法 = pca.入荷方法;
				article2.仕入日 = pca.仕入日;
				article2.精算日 = pca.精算日;
				article2.伝票No = pca.伝票No;
				article2.仕入先コード = pca.仕入先コード;
				article2.仕入先名 = pca.仕入先名;
				article2.先方担当者名 = pca.先方担当者名;
				article2.部門コード = pca.部門コード;
				article2.担当者コード = pca.担当者コード;
				article2.摘要コード = pca.摘要コード;
				article2.摘要名 = pca.摘要名;
				article2.商品コード = PcaGoodsIDDefine.ArticleCode;	// 000014
				article2.マスター区分 = 4; // 0:一般商品、1:雑商品、2:諸雑 費、3:値引、4:記事
				article2.商品名 = tokuisakiName;
				article2.区 = pca.区;
				article2.倉庫コード = pca.倉庫コード;
				article2.数量 = 0;
				article2.単位 = string.Empty;
				article2.単価 = 0;
				article2.金額 = 0;
				article2.税区分 = 0;
				article2.備考 = "0";
				article2.規格型番 = string.Empty;
				article2.色 = string.Empty;
				article2.サイズ = string.Empty;
				article2.税率 = 0;
				article2.ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
				article2.伝票No2 = string.Empty;
				article2.商品名2 = string.Empty;
				outputList.Add(article2);

				denNo++;
			}

			/*
			List<仕入集計> Microsoft365仕入集計 = MakePurchaseFileAccess.Select_Microsoft365仕入集計(collectMonth, Settings.Connect.Junp.ConnectionString);
			string bumonCode = string.Empty;
			foreach (仕入集計 data in Microsoft365仕入集計)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				汎用データレイアウト仕入明細データ pca = new 汎用データレイアウト仕入明細データ();
				pca.入荷方法 = 0;   // 0:通常仕入
				pca.仕入日 = collectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = collectMonth.First.FirstDayOfNextMonth().ToIntYMD(); // 対象月翌月初日
				pca.伝票No = denNo;
				pca.仕入先コード = data.仕入先;
				pca.仕入先名 = string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = "0";
				pca.摘要コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 0;  // 仕入
				pca.倉庫コード = "0";
				pca.数量 = data.数量;
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				pca.備考 = "0";
				pca.規格型番 = string.Empty;
				pca.色 = string.Empty;
				pca.サイズ = string.Empty;
				pca.税率 = data.sykd_rate;
				pca.ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
				pca.伝票No2 = string.Empty;
				pca.商品名2 = string.Empty;
				outputList.Add(pca);
			}
			*/
			using (var sw = new StreamWriter(Settings.Microsoft365仕入データパス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (汎用データレイアウト仕入明細データ pca in outputList)
				{
					string record = pca.ToCsvString(Settings.PcaVersion);
					sw.WriteLine(record);
				}
			}
		}

		/// <summary>
		/// 9.問心伝月額仕入データ作成
		/// </summary>
		/// <param name="collectMonth">対象年月</param>
		public void 問心伝月額仕入データファイル出力(YearMonth collectMonth)
		{
			List<仕入集計> 問心伝月額仕入集計 = MakePurchaseFileAccess.Select_問心伝月額仕入集計(collectMonth, Settings.Connect.Junp.ConnectionString);

			List<汎用データレイアウト仕入明細データ> outputList = new List<汎用データレイアウト仕入明細データ>();

			int denNo = Settings.問心伝月額開始伝票番号;
			string bumonCode = string.Empty;
			foreach (仕入集計 data in 問心伝月額仕入集計)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				汎用データレイアウト仕入明細データ pca = new 汎用データレイアウト仕入明細データ();
				pca.入荷方法 = 0;   // 0:通常仕入
				pca.仕入日 = collectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = pca.仕入日;
				pca.伝票No = denNo;
				pca.仕入先コード = data.仕入先;
				pca.仕入先名 = string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = "0";
				pca.摘要コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 0;  // 仕入
				pca.倉庫コード = "0";
				pca.数量 = data.数量;
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				pca.備考 = "0";
				pca.規格型番 = string.Empty;
				pca.色 = string.Empty;
				pca.サイズ = string.Empty;
				pca.税率 = data.sykd_rate;
				pca.ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
				pca.伝票No2 = string.Empty;
				pca.商品名2 = string.Empty;
				outputList.Add(pca);
			}
			using (var sw = new StreamWriter(Settings.問心伝月額仕入データパス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (汎用データレイアウト仕入明細データ pca in outputList)
				{
					string record = pca.ToCsvString(Settings.PcaVersion);
					sw.WriteLine(record);
				}
			}
		}

		/// <summary>
		/// 11.Curline本体アプリ仕入データ作成
		/// </summary>
		/// <param name="collectMonth">対象年月</param>
		// Ver1.05(2023/07/10 勝呂):Curline本体アプリ仕入データファイル出力廃止対応
/*
		public void Curline本体アプリ仕入データファイル出力(YearMonth collectMonth)
		{
			List<仕入集計> Curline本体アプリ仕入集計 = MakePurchaseFileAccess.Select_Curline本体アプリ仕入集計(collectMonth, Settings.Connect.Junp.ConnectionString);

			List<汎用データレイアウト仕入明細データ> outputList = new List<汎用データレイアウト仕入明細データ>();

			int denNo = Settings.Curline本体アプリ開始伝票番号;
			string bumonCode = string.Empty;
			foreach (仕入集計 data in Curline本体アプリ仕入集計)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				汎用データレイアウト仕入明細データ pca = new 汎用データレイアウト仕入明細データ();
				pca.入荷方法 = 0;   // 0:通常仕入
				pca.仕入日 = collectMonth.Last.ToIntYMD(); // 対象月末日
				pca.精算日 = pca.仕入日;
				pca.伝票No = denNo;
				pca.仕入先コード = data.仕入先;
				pca.仕入先名 = string.Empty;   // 仕入先名は空白
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);
				pca.担当者コード = "0";
				pca.摘要コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 0;  // 仕入
				pca.倉庫コード = "0";
				pca.数量 = data.数量;
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				pca.備考 = "0";
				pca.規格型番 = string.Empty;
				pca.色 = string.Empty;
				pca.サイズ = string.Empty;
				pca.税率 = data.sykd_rate;
				pca.ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
				pca.伝票No2 = string.Empty;
				pca.商品名2 = string.Empty;
				outputList.Add(pca);
			}
			using (var sw = new StreamWriter(Settings.Curline本体アプリ仕入データパス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (汎用データレイアウト仕入明細データ pca in outputList)
				{
					string record = pca.ToCsvString(Settings.PcaVersion);
					sw.WriteLine(record);
				}
			}
		}
*/

		/// <summary>
		/// 12.ナルコーム仕入データ作成
		/// </summary>
		/// <param name="collectMonth">対象年月</param>
		public void ナルコーム仕入データファイル出力(YearMonth collectMonth)
		{
			List<ナルコーム仕入集計> ナルコーム仕入集計 = MakePurchaseFileAccess.Select_ナルコーム仕入集計(collectMonth, Settings.Connect.Junp.ConnectionString);

			List<汎用データレイアウト仕入明細データ> outputList = new List<汎用データレイアウト仕入明細データ>();

			int denNo = Settings.ナルコーム開始伝票番号;
			string bumonCode = string.Empty;
			foreach (ナルコーム仕入集計 data in ナルコーム仕入集計)
			{
				if (bumonCode != data.sykd_jbmn)
				{
					bumonCode = data.sykd_jbmn;
					denNo++;
				}
				汎用データレイアウト仕入明細データ pca = new 汎用データレイアウト仕入明細データ();
				pca.科目区分 = data.仕入フラグ;
				pca.仕入日 = data.sykd_uribi;
				pca.精算日 = data.sykd_uribi;
				pca.伝票No = denNo;
				pca.仕入先コード = data.仕入先;
				pca.仕入先名 = string.Empty;
				pca.先方担当者名 = string.Empty;
				pca.部門コード = data.sykd_jbmn.Substring(1);    // 081→81
				pca.担当者コード = data.sykd_jtan.Substring(2);   // 0081→81
				pca.摘要コード = "0";
				pca.摘要名 = string.Empty;
				pca.商品コード = data.sykd_scd;
				pca.商品名 = data.sykd_mei;
				pca.区 = 2;  // 単価訂正
				pca.倉庫コード = "0";
				pca.数量 = data.数量;
				pca.単位 = string.Empty;
				pca.単価 = data.評価単価;
				pca.金額 = pca.数量 * pca.単価;
				pca.税区分 = 2;
				pca.備考 = "0";
				pca.規格型番 = string.Empty;
				pca.色 = string.Empty;
				pca.サイズ = string.Empty;
				pca.税率 = data.sykd_rate;
				pca.ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
				pca.伝票No2 = string.Empty;
				pca.商品名2 = string.Empty;
				outputList.Add(pca);

				denNo++;
			}
			using (var sw = new StreamWriter(Settings.ナルコーム仕入データパス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				foreach (汎用データレイアウト仕入明細データ pca in outputList)
				{
					string record = pca.ToCsvString(Settings.PcaVersion);
					sw.WriteLine(record);
				}
			}
		}

		/// <summary>
		/// 13.クラウドバックアップ仕入データ作成
		/// </summary>
		/// <param name="collectMonth">対象年月</param>
		public void クラウドバックアップ仕入データファイル出力(YearMonth collectMonth)
		{
			using (var sw = new StreamWriter(Settings.クラウドバックアップ仕入データパス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				List<GroupMicPCA売上明細> pcaList = MakePurchaseFileAccess.Select_クラウドバックアップ仕入集計(Settings.GetCloudBackupGoods(), collectMonth, Settings.Connect.Junp.ConnectionString);
				if (0 < pcaList.Count)
				{
					//var query = from PCA売上明細 in pcaList
					//			orderby PCA売上明細.sykd_jbmn, PCA売上明細.sykd_uribi, PCA売上明細.sykd_scd
					//			group PCA売上明細 by new { PCA売上明細.sykd_jbmn, PCA売上明細.sykd_jtan, PCA売上明細.sykd_scd, PCA売上明細.sykd_mkbn, PCA売上明細.sykd_tani, PCA売上明細.sykd_uribi, PCA売上明細.sykd_rate } into X
					//			select new { X.Key.sykd_jbmn, X.Key.sykd_jtan, X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_uribi, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };
					List<MakePurchaseData> outputList = new List<MakePurchaseData>();
					foreach (GroupMicPCA売上明細 pca in pcaList)
					{
						if (0 != pca.数量)
						{
							クラウドバックアップ仕入商品情報 goods = Settings.クラウドバックアップ商品.Find(p => p.商品コード == pca.sykd_scd);
							if (null != goods)
							{
								MakePurchaseData stock = new MakePurchaseData();
								stock.f仕入先コード = goods.仕入先;
								stock.f部門コード = pca.sykd_jbmn;
								stock.f担当者コード = pca.sykd_jtan;
								stock.f仕入商品コード = goods.仕入商品コード;
								stock.f単位 = pca.sykd_tani;
								stock.f仕入価格 = goods.仕入価格;
								stock.f売上日 = pca.sykd_uribi;
								stock.f仕入フラグ = goods.仕入フラグ;
								stock.f消費税率 = (short)pca.消費税率;

								// PC安心サポート Plus3年契約 仕入数36
								// PC安心サポート Plus1年契約 仕入数12
								// PC安心サポート Plus1年更新 仕入数12
								// MWS ｸﾗｳﾄﾞﾊﾞｯｸｱｯﾌﾟ(PC安心ｻﾎﾟｰﾄ Plus) 仕入数1*数量
								// MWS ｸﾗｳﾄﾞﾊﾞｯｸｱｯﾌﾟ(月額) 仕入数1*数量
								stock.f数量 = goods.仕入数 * pca.数量;

								vMicPCA商品マスタ mst = JunpDatabaseAccess.Select_vMicPCA商品マスタ(goods.仕入商品コード, Settings.Connect.Junp.ConnectionString);
								if (null != mst)
								{
									stock.f商品名 = mst.sms_mei;
								}
								outputList.Add(stock);
							}
						}
					}
					int plusNo = Settings.クラウドバックアップ開始伝票番号; // '20500番台（りすとん=20 office365=40）
					foreach (MakePurchaseData output in outputList)
					{
						string record = output.ToPurchase(plusNo, Settings.PcaVersion);
						sw.WriteLine(record);
						plusNo++;
					}
				}
			}
		}

		/// <summary>
		/// 14.アルメックス保守仕入データ作成
		/// </summary>
		/// <param name="collectMonth">対象年月</param>
		/// <returns>エラーメッセージ</returns>
		public string アルメックス保守仕入データファイル出力(YearMonth collectMonth)
		{
			string msg = string.Empty;
			using (var sw = new StreamWriter(Settings.アルメックス保守仕入データパス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				List<vMicPCA売上明細> pcaList = MakePurchaseFileAccess.Select_アルメックス仕入集計(Settings.GetAlmexMainteGoods(), collectMonth, Settings.Connect.Junp.ConnectionString);
				if (0 < pcaList.Count)
				{
					List<MakePurchaseData> outputList = new List<MakePurchaseData>();

					// 商品マスタ
					List<Tuple<string, string>> goodsList = new List<Tuple<string, string>>();

					// 仕入先マスタ
					List<Tuple<string, string>> stockMasterList = new List<Tuple<string, string>>();
					foreach (vMicPCA売上明細 pca in pcaList)
					{
						if (0 != pca.数量)
						{
							アルメックス仕入商品情報 goods = Settings.アルメックス商品.Find(p => p.商品コード == pca.sykd_scd);
							if (null != goods)
							{
								MakePurchaseData stock = new MakePurchaseData();
								stock.f部門コード = pca.sykd_jbmn;
								stock.f担当者コード = pca.sykd_jtan;
								stock.f仕入商品コード = goods.仕入商品コード;
								stock.f単位 = pca.sykd_tani;
								stock.f仕入価格 = goods.仕入価格;
								stock.f売上日 = pca.sykd_uribi;
								stock.f仕入フラグ = goods.仕入フラグ;
								stock.f消費税率 = pca.消費税;
								stock.f数量 = pca.数量;

								Tuple<string, string> goodsName = goodsList.Find(p => p.Item1 == goods.仕入商品コード);
								if (null != goodsName)
								{
									stock.f商品名 = goodsName.Item2;
								}
								else
								{
									vMicPCA商品マスタ mst = JunpDatabaseAccess.Select_vMicPCA商品マスタ(goods.仕入商品コード, Settings.Connect.Junp.ConnectionString);
									if (null != mst)
									{
										stock.f商品名 = mst.sms_mei;
										goodsList.Add(new Tuple<string, string>(goods.仕入商品コード, mst.sms_mei));
									}
								}

								// アプリケーション情報のLicensedKeyから仕入先コードを取得する方法
								// 請求元と請求先が違うか確認
								string stockCode = pca.sykd_tcd;
								string whereStr = string.Format("sykd_uribi = {0} AND sykd_denno = {1} AND sykd_scd = '{2}' AND sykd_mei like '%得意先No.%'"
																	, pca.sykd_uribi, pca.sykd_denno, PcaGoodsIDDefine.ArticleCode);
								List<vMicPCA売上明細> list = JunpDatabaseAccess.Select_vMicPCA売上明細(whereStr, "", Settings.Connect.Junp.ConnectionString);
								if (null != list && 0 < list.Count)
								{
									// 請求先が違う場合
									stockCode = list[0].sykd_mei.Replace("得意先No.", "").Trim();
								}
								List<tMik基本情報> basic = JunpDatabaseAccess.Select_tMik基本情報(string.Format("[fkj得意先情報] = '{0}'", stockCode), "", Settings.Connect.Junp.ConnectionString);
								whereStr = string.Format("faiCliMicID = {0} AND (faiアプリケーション名 = '{1}' OR faiアプリケーション名 = '{2}' OR faiアプリケーション名 = '{3}' OR faiアプリケーション名 = '{4}' OR faiアプリケーション名 = '{5}')"
																	, basic[0].fkjCliMicID
																	, tMikコードマスタ.fcmコード_AlmexMainteTex30_Cash
																	, tMikコードマスタ.fcmコード_AlmexMainteTex30_Credit
																	, tMikコードマスタ.fcmコード_AlmexMainteFitA_Cash
																	, tMikコードマスタ.fcmコード_AlmexMainteFitA_Credit
																	, tMikコードマスタ.fcmコード_AlmexMainteFitA_QRCredit);
								List<tMikアプリケーション情報> apl = JunpDatabaseAccess.Select_tMikアプリケーション情報(whereStr, "faiアプリケーションNo, faiアプリケーション名", Settings.Connect.Junp.ConnectionString);
								if (null != apl && 0 < apl.Count)
								{
									stock.f仕入先コード = apl[0].faiLicensedKey;
								}
								if (6 != stock.f仕入先コード.Length && true == StringUtil.IsAllHankakuNumeral(stock.f仕入先コード))
								{
									// 仕入先コードが半角6桁でない
									msg = string.Format("アプリケーション情報 Licensed Key欄の仕入先コードが空欄もしくは正しくありません。(伝票No.{0})", pca.sykd_denno);
									ErrorLogger.Error(msg);
								}
								//// 住所から仕入先コードを取得する方法
								//List<tMik基本情報> basic = JunpDatabaseAccess.Select_tMik基本情報(string.Format("[fkj得意先情報] = '{0}'", pca.sykd_tcd), "", Settings.Connect.Junp.ConnectionString);
								//if (null != basic && 0 < basic.Count)
								//{
								//	stock.f仕入先コード = AlemxPurchase.仕入先コード(basic[0].県番号, basic[0].fkj住所１);
								//}
								if (0 < stock.f仕入先コード.Length)
								{
									Tuple<string, string> stockName = stockMasterList.Find(p => p.Item1 == stock.f仕入先コード);
									if (null != stockName)
									{
										stock.f仕入先名 = stockName.Item2;
									}
									else
									{
										whereStr = string.Format("rms_tcd = '{0}'", stock.f仕入先コード);
										List<vMicPCA仕入先マスタ> mst = JunpDatabaseAccess.Select_vMicPCA仕入先マスタ(whereStr, "", Settings.Connect.Junp.ConnectionString);
										if (null != mst && 0 < mst.Count)
										{
											stock.f仕入先名 = string.Format("{0} {1}", mst[0].rms_mei1.Trim(), mst[0].rms_mei2.Trim());
											stockMasterList.Add(new Tuple<string, string>(stock.f仕入先コード, stock.f仕入先名));
										}
									}
								}
								outputList.Add(stock);
							}
						}
					}
					int plusNo = Settings.アルメックス開始伝票番号;
					foreach (MakePurchaseData output in outputList)
					{
						string record = output.ToPurchase(plusNo, Settings.PcaVersion);
						sw.WriteLine(record);
						plusNo++;
					}
				}
			}
			return msg;
		}

		/// <summary>
		/// りすとん月額仕入データファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutputListon_Click(object sender, EventArgs e)
		{
			// 対象年月
			YearMonth collectMonth = new YearMonth(dateTimePickerTarget.Value.Year, dateTimePickerTarget.Value.Month);

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				りすとん月額仕入データファイル出力(collectMonth);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("仕入データを出力しました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "仕入データ出力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Microsoft365仕入データファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// Ver1.03 経理部の要請により、Microsoft365仕入データを部門毎の集計を止めて、得意先に関する記事データを追加(2023/02/02 勝呂)
		private void buttonOutputMicrosoft365_Click(object sender, EventArgs e)
		{
			// 対象年月
			YearMonth collectMonth = new YearMonth(dateTimePickerTarget.Value.Year, dateTimePickerTarget.Value.Month);

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				Microsoft365仕入データファイル出力(collectMonth);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("仕入データを出力しました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "仕入データ出力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 問心伝月額仕入データファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutputMonshinden_Click(object sender, EventArgs e)
		{
			// 対象年月
			YearMonth collectMonth = new YearMonth(dateTimePickerTarget.Value.Year, dateTimePickerTarget.Value.Month);

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				問心伝月額仕入データファイル出力(collectMonth);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("仕入データを出力しました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "仕入データ出力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Curline本体アプリ仕入データファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Ver1.05(2023/07/10 勝呂):Curline本体アプリ仕入データファイル出力廃止対応
		private void buttonOutputCurline_Click(object sender, EventArgs e)
		{
/*
			// 対象年月
			YearMonth collectMonth = new YearMonth(dateTimePickerTarget.Value.Year, dateTimePickerTarget.Value.Month);

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				Curline本体アプリ仕入データファイル出力(collectMonth);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("仕入データを出力しました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "仕入データ出力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
*/
		}

		/// <summary>
		/// ナルコーム仕入データファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutputNarcohm_Click(object sender, EventArgs e)
		{
			// 対象年月
			YearMonth collectMonth = new YearMonth(dateTimePickerTarget.Value.Year, dateTimePickerTarget.Value.Month);

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				ナルコーム仕入データファイル出力(collectMonth);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("仕入データを出力しました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "仕入データ出力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// クラウドバックアップ仕入データファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPutputCloudBackup_Click(object sender, EventArgs e)
		{
			// 対象年月
			YearMonth collectMonth = new YearMonth(dateTimePickerTarget.Value.Year, dateTimePickerTarget.Value.Month);

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				クラウドバックアップ仕入データファイル出力(collectMonth);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("仕入データを出力しました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "仕入データ出力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// アルメックス保守仕入データファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutputAlmex_Click(object sender, EventArgs e)
		{
			// 対象年月
			YearMonth collectMonth = new YearMonth(dateTimePickerTarget.Value.Year, dateTimePickerTarget.Value.Month);

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				YearMonth thisYM = collectMonth + 1;    // 対象月を当月初日に変更
				string msg = アルメックス保守仕入データファイル出力(thisYM);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				if (0 == msg.Length)
				{
					MessageBox.Show("仕入データを出力しました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show(string.Format("{0}\nERROR.LOGを確認してください。", msg), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "仕入データ出力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
