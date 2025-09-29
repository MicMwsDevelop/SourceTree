//
// MainForm.cs
// 
// 部署間利益付け替え仕入データ作成 メイン画面フォームファイル
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/09/22 越田)
//
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Pca;
using CommonLib.BaseFactory.ProfitTransferPurchaseFile;
using CommonLib.Common;
using CommonLib.DB.SqlServer.ProfitTransferPurchaseFile;
using ProfitTransferPurchaseFile.Properties;
using ProfitTransferPurchaseFile.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProfitTransferPurchaseFile
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
		public ProfitTransferPurchaseFileSettings Settings { get; set; }


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
			Settings = ProfitTransferPurchaseFileSettingsIF.GetSettings();

			// 先月初日を設定
			//   ※毎月月初に前月売上の売上伝票を抽出する運用のため
			dateTimePickerTarget.Value = Date.Today.FirstDayOfLasMonth().ToDateTime();

			// 環境設定内容の表示 - 出力先フォルダ
			textBoxOutputFolder.Text = Settings.出力先フォルダ;

			// 環境設定内容の表示 - 部署間利益付け替え仕入データファイル名
			textBoxProfitTransferPurchaseFilename.Text = Settings.部署間利益付け替え仕入データファイル名;

			// 環境設定内容の表示 - PCAバージョン番号
			//   ※PCA仕入明細汎用データファイル出力形式判定用
			textBoxPcaVersion.Text = Settings.PcaVersion.ToString();
		}

		/// <summary>
		/// 
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
		/// [START]ボタン - クリック時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStart_Click(object sender, EventArgs e)
		{
			// 画面から対象年月を取得
			YearMonth collectMonth = new YearMonth(dateTimePickerTarget.Value.Year, dateTimePickerTarget.Value.Month);

			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;


				var outputList = new List<汎用データレイアウト仕入明細データ>();
				int denNo = Settings.部署間利益付け替え開始伝票番号;


				// PRO営業部→CS事業部 利益付け替え仕入データ作成
				PRO営業部_CS事業部利益付け替え仕入データ作成(outputList, collectMonth, ref denNo);

				// SOL営業部→CS事業部 利益付け替え仕入データ作成
				denNo++;    // 次の空き伝票Noに設定
				SOL営業部_CS事業部利益付け替え仕入データ作成(outputList, collectMonth, ref denNo);

				// 仕入明細汎用データCSVファイルに出力
				// TODO: 出力ファイル名に日時文字列を付加して前回出力ファイルが上書きされて消えないようにするか要検討(→本アプリの場合、何度でも同じ結果を出力できるみから一旦そこまで対応しない方針とした) by KOSHITA
				// TODO: 出力ファイルをネットワーク先に直接書き込む処理だと、ネットワーク環境で処理に失敗するリスクを加味して、ローカルに書き込んだファイルをを配布先にコピーするようにするか要検討(→本アプリの場合、何度でも同じ結果を出力できるみから一旦そこまで対応しない方針とした) by KOSHITA
				using (var sw = new StreamWriter(Settings.部署間利益付け替え仕入データパス名, false, System.Text.Encoding.GetEncoding("shift_jis")))
				{
					foreach (汎用データレイアウト仕入明細データ pca in outputList)
					{
						string record = pca.ToCsvString(Settings.PcaVersion);
						sw.WriteLine(record);
					}
				}

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
		/// [終了]ボタン - クリック時
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// PRO営業部→CS事業部 利益付け替え仕入データ作成
		/// </summary>
		/// <param name="outputList">格納用 汎用データレイアウト仕入明細データ(追記)</param>
		/// <param name="collectMonth">対象年月</param>
		/// <param name="denNo">次回発番仕入伝票No(本メソッド終了時に本メソッド内で発番した最終Noを保持)</param>
		private void PRO営業部_CS事業部利益付け替え仕入データ作成(List<汎用データレイアウト仕入明細データ> outputList, YearMonth collectMonth, ref int denNo)
		{
			// 
			List<営業部_CS事業部_利益付け替え仕入集計> PRO営業部_CS事業部_利益付け替え仕入集計s = ProfitTransferPurchaseFileAccess.Select_PRO営業部_CS事業部_利益付け替え仕入集計(Settings.GetPRO営業部ListonGoods(), Settings.委託元_PRO営業部_部門コード, collectMonth, Settings.Connect.Junp.ConnectionString);


			// 委託元仕入データ作成
			string prev委託先部門コード = string.Empty;
			foreach (営業部_CS事業部_利益付け替え仕入集計 meisai in PRO営業部_CS事業部_利益付け替え仕入集計s)
			{
				if (!string.IsNullOrEmpty(prev委託先部門コード) && prev委託先部門コード != meisai.委託先_部門コード)
				{
					denNo++;
				}

				// 仕入データ(委託元)
				//   ※下記でpcaに未指定の値は、初期値が正しい(数値型ならば0、文字列型ならば空文字列)ことを前提としている
				var pca = new 汎用データレイアウト仕入明細データ();
				pca.仕入日 = int.Parse(DateTime.ParseExact(meisai.売上日.ToString(), "yyyyMMdd", null).EndOfMonth().ToString("yyyyMMdd"));        // 売上月の末日
				pca.精算日 = int.Parse(DateTime.ParseExact(meisai.売上日.ToString(), "yyyyMMdd", null).EndOfMonth().ToString("yyyyMMdd"));        // 売上月の末日
				pca.伝票No = denNo;
				pca.仕入先コード = Settings.仕入先コード;
				pca.仕入先名 = string.Empty;                    // 仕入先名はPCA汎用データ受入れ時に不要なため空文字列とする(Settings.仕入先名で設定は可能)
				pca.部門コード = Settings.委託元_PRO営業部_部門コード;
				pca.担当者コード = Settings.委託元_PRO営業部_担当者コード;
				pca.摘要コード = Settings.Get委託先摘要コード(meisai.委託先_部門コード);
				pca.摘要名 = Settings.Get委託先摘要名(meisai.委託先_部門コード);
				pca.商品コード = meisai.商品コード;
				pca.商品名 = meisai.商品名;
				pca.倉庫コード = "0000";                        // 対象商品が役務で在庫管理不要のため倉庫コードの設定なし(0000を設定)
				pca.数量 = meisai.数量;							// 委託元はマイナスの利益とするためプラスの仕入とする
				//pca.単価 = meisai.原単価;						// ※WW受注伝票→PCA売上伝票作成の場合、原単価は設定されるが、単価は常に0のためSOL営業部の集計に合わせて単価は設定しない
				pca.金額 = meisai.原価金額;                     // PRO営業部の場合は原価が利益付け替え対象金額。委託元はマイナスの利益とするためプラスの仕入とする
				pca.税区分 = meisai.税区分;
				pca.税込区分 = meisai.税込区分;
				pca.備考 = MultiByteStrings.CutByMultiByteLength(meisai.売上伝票No + " " + meisai.顧客名, 汎用データレイアウト仕入明細データ.備考_MAX_BYTE_LENGTH);
				pca.税率 = meisai.税率;

				outputList.Add(pca);

				// 
				prev委託先部門コード = meisai.委託先_部門コード;
			}

			// 委託先仕入データ作成
			denNo++;
			prev委託先部門コード = string.Empty;
			foreach (営業部_CS事業部_利益付け替え仕入集計 meisai in PRO営業部_CS事業部_利益付け替え仕入集計s)
			{
				if (!string.IsNullOrEmpty(prev委託先部門コード) && prev委託先部門コード != meisai.委託先_部門コード)
				{
					denNo++;
				}

				// 仕入データ(委託元)
				//   ※下記でpcaに未指定の値は、初期値が正しい(数値型ならば0、文字列型ならば空文字列)ことを前提としている
				var pca = new 汎用データレイアウト仕入明細データ();
				pca.仕入日 = int.Parse(DateTime.ParseExact(meisai.売上日.ToString(), "yyyyMMdd", null).EndOfMonth().ToString("yyyyMMdd"));        // 売上月の末日
				pca.精算日 = int.Parse(DateTime.ParseExact(meisai.売上日.ToString(), "yyyyMMdd", null).EndOfMonth().ToString("yyyyMMdd"));        // 売上月の末日
				pca.伝票No = denNo;
				pca.仕入先コード = Settings.仕入先コード;
				pca.仕入先名 = string.Empty;                    // 仕入先名はPCA汎用データ受入れ時に不要なため空文字列とする(Settings.仕入先名で設定は可能)
				pca.部門コード = meisai.委託先_部門コード;
				pca.担当者コード = meisai.委託先_担当者コード;
				pca.摘要コード = Settings.Get委託元摘要コード(Settings.委託元_PRO営業部_部門コード);
				pca.摘要名 = Settings.Get委託元摘要名(Settings.委託元_PRO営業部_部門コード);
				pca.商品コード = meisai.商品コード;
				pca.商品名 = meisai.商品名;
				pca.倉庫コード = "0000";						// 対象商品が役務で在庫管理不要のため倉庫コードの設定なし(0000を設定)
				pca.数量 = -meisai.数量;                        // 委託先はプラスの利益とするためマイナスの仕入とする
				//pca.単価 = meisai.原単価;						// ※WW受注伝票→PCA売上伝票作成の場合、原単価は設定されるが、単価は常に0のためSOL営業部の集計に合わせて単価は設定しない
				pca.金額 = -meisai.原価金額;                    // PRO営業部の場合は原価が利益付け替え対象金額。委託先はプラスの利益とするためマイナスの仕入とする
				pca.税区分 = meisai.税区分;
				pca.税込区分 = meisai.税込区分;
				pca.備考 = MultiByteStrings.CutByMultiByteLength(meisai.売上伝票No + " " + meisai.顧客名, 汎用データレイアウト仕入明細データ.備考_MAX_BYTE_LENGTH);
				pca.税率 = meisai.税率;

				outputList.Add(pca);

				// 
				prev委託先部門コード = meisai.委託先_部門コード;
			}
		}

		/// <summary>
		/// SOL営業部→CS事業部 利益付け替え仕入データ作成
		/// </summary>
		/// <param name="outputList">格納用 汎用データレイアウト仕入明細データ(追記)</param>
		/// <param name="collectMonth">対象年月</param>
		/// <param name="denNo">次回発番仕入伝票No(本メソッド終了時に本メソッド内で発番した最終Noを保持)</param>
		private void SOL営業部_CS事業部利益付け替え仕入データ作成(List<汎用データレイアウト仕入明細データ> outputList, YearMonth collectMonth, ref int denNo)
		{
			// 
			List<営業部_CS事業部_利益付け替え仕入集計> SOL営業部_CS事業部_利益付け替え仕入集計s = ProfitTransferPurchaseFileAccess.Select_SOL営業部_CS事業部_利益付け替え仕入集計(Settings.GetSOL営業部ListonGoods(), Settings.委託元_SOL営業部_部門コード, collectMonth, Settings.Connect.Junp.ConnectionString);


			// 委託元仕入データ作成
			string prev委託先部門コード = string.Empty;
			foreach (営業部_CS事業部_利益付け替え仕入集計 meisai in SOL営業部_CS事業部_利益付け替え仕入集計s)
			{
				if (!string.IsNullOrEmpty(prev委託先部門コード) && prev委託先部門コード != meisai.委託先_部門コード)
				{
					denNo++;
				}

				// 仕入データ(委託元)
				//   ※下記でpcaに未指定の値は、初期値が正しい(数値型ならば0、文字列型ならば空文字列)ことを前提としている
				var pca = new 汎用データレイアウト仕入明細データ();
				pca.仕入日 = int.Parse(DateTime.ParseExact(meisai.売上日.ToString(), "yyyyMMdd", null).EndOfMonth().ToString("yyyyMMdd"));        // 売上月の末日
				pca.精算日 = int.Parse(DateTime.ParseExact(meisai.売上日.ToString(), "yyyyMMdd", null).EndOfMonth().ToString("yyyyMMdd"));        // 売上月の末日
				pca.伝票No = denNo;
				pca.仕入先コード = Settings.仕入先コード;
				pca.仕入先名 = string.Empty;					// 仕入先名はPCA汎用データ受入れ時に不要なため空文字列とする(Settings.仕入先名で設定は可能)
				pca.部門コード = Settings.委託元_SOL営業部_部門コード;
				pca.担当者コード = Settings.委託元_SOL営業部_担当者コード;
				pca.摘要コード = Settings.Get委託先摘要コード(meisai.委託先_部門コード);
				pca.摘要名 = Settings.Get委託先摘要名(meisai.委託先_部門コード);
				pca.商品コード = meisai.商品コード;
				pca.商品名 = meisai.商品名;
				pca.倉庫コード = "0000";						// 対象商品が役務で在庫管理不要のため倉庫コードの設定なし(0000を設定)
				pca.数量 = meisai.数量;                         // 委託元はマイナスの利益とするためプラスの仕入とする
				//pca.単価 = meisai.単価;						// ※WW受注伝票→PCA売上伝票作成の場合、単価は常に0のため設定しない
				pca.金額 = meisai.売上金額;						// SOL営業部の場合は売上価格(PCA売上伝票画面上は[金額]欄)が利益付け替え対象。委託元はマイナスの利益とするためプラスの仕入とする
				pca.税区分 = meisai.税区分;
				pca.税込区分 = meisai.税込区分;
				pca.備考 = MultiByteStrings.CutByMultiByteLength(meisai.売上伝票No + " " + meisai.顧客名, 汎用データレイアウト仕入明細データ.備考_MAX_BYTE_LENGTH);
				pca.税率 = meisai.税率;

				outputList.Add(pca);

				// 
				prev委託先部門コード = meisai.委託先_部門コード;
			}

			// 委託先仕入データ作成
			denNo++;
			prev委託先部門コード = string.Empty;
			foreach (営業部_CS事業部_利益付け替え仕入集計 meisai in SOL営業部_CS事業部_利益付け替え仕入集計s)
			{
				if (!string.IsNullOrEmpty(prev委託先部門コード) && prev委託先部門コード != meisai.委託先_部門コード)
				{
					denNo++;
				}

				// 仕入データ(委託元)
				//   ※下記でpcaに未指定の値は、初期値が正しい(数値型ならば0、文字列型ならば空文字列)ことを前提としている
				var pca = new 汎用データレイアウト仕入明細データ();
				pca.仕入日 = int.Parse(DateTime.ParseExact(meisai.売上日.ToString(), "yyyyMMdd", null).EndOfMonth().ToString("yyyyMMdd"));        // 売上月の末日
				pca.精算日 = int.Parse(DateTime.ParseExact(meisai.売上日.ToString(), "yyyyMMdd", null).EndOfMonth().ToString("yyyyMMdd"));        // 売上月の末日
				pca.伝票No = denNo;
				pca.仕入先コード = Settings.仕入先コード;
				pca.仕入先名 = string.Empty;                   // 仕入先名は空白		Settings.仕入先名
				pca.部門コード = meisai.委託先_部門コード;
				pca.担当者コード = meisai.委託先_担当者コード;
				pca.摘要コード = Settings.Get委託元摘要コード(Settings.委託元_SOL営業部_部門コード);
				pca.摘要名 = Settings.Get委託元摘要名(Settings.委託元_SOL営業部_部門コード);
				pca.商品コード = meisai.商品コード;
				pca.商品名 = meisai.商品名;
				pca.倉庫コード = "0000";						// 対象商品が役務で在庫管理不要のため倉庫コードの設定なし(0000を設定)
				pca.数量 = -meisai.数量;						// 委託先はプラスの利益とするためマイナスの仕入とする
				//pca.単価 = meisai.単価;						// ※WW受注伝票→PCA売上伝票作成の場合、単価は常に0のため設定しない
				pca.金額 = -meisai.売上金額;					// SOL営業部の場合は売上価格(PCA売上伝票画面上は[金額]欄)が利益付け替え対象。委託先はプラスの利益とするためマイナスの仕入とする
				pca.税区分 = meisai.税区分;
				pca.税込区分 = meisai.税込区分;
				pca.備考 = MultiByteStrings.CutByMultiByteLength(meisai.売上伝票No + " " + meisai.顧客名, 汎用データレイアウト仕入明細データ.備考_MAX_BYTE_LENGTH);
				pca.税率 = meisai.税率;

				outputList.Add(pca);

				// 
				prev委託先部門コード = meisai.委託先_部門コード;
			}
		}
	}
}
