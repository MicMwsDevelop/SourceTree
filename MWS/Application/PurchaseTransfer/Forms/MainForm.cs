using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PurchaseTransfer.Settings;
using CommonLib.BaseFactory.PurchaseTransfer;
using System.IO;
using CommonLib.Common;
using CommonLib.DB.SqlServer.PurchaseTransfer;
using CommonLib.DB.SqlServer;
using System.Data.SqlClient;

namespace PurchaseTransfer.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		public static PurchaseTransferSettings gSettings;

		/// <summary>
		/// 集計月
		/// </summary>
		public YearMonth CollectMonth { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			// 先月
			CollectMonth = Date.Today.FirstDayOfLasMonth().ToYearMonth();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// 環境設定の読込
			gSettings = PurchaseTransferSettingsIF.GetSettings();

			// 1.在庫一覧テーブルリンク接続
			// 環境設定.在庫一覧表入力ファイル名：\\storage\公開データ\業務部公開用\経理共有\仕入振替\在庫一覧表\在庫一覧表GG年MM月末.txt
			string msg;
			List<在庫一覧表> zaikoList = ReadZaikoListCsvFile(gSettings.在庫一覧表入力ファイル名, out msg);
			if (0 == zaikoList.Count)
			{
				MessageBox.Show(msg, "在庫一覧表の読込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try
			{
				DatabaseAccess.DeleteDatabase("DELETE FROM TEST_在庫一覧表", gSettings.Connect.Charlie.ConnectionString);

				DataTable dt = new DataTable();
				在庫一覧表.SetDataColumn(dt);
				foreach (在庫一覧表 zaiko in zaikoList)
				{
					dt.Rows.Add(zaiko.GetDataRow(dt));
				}
				DatabaseAccess.BulkInsert(gSettings.Connect.Charlie.ConnectionString, "TEST_在庫一覧表", dt);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "在庫一覧表のレコード追加エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		/// <summary>
		/// START
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStart_Click(object sender, EventArgs e)
		{
			try
			{
				// 2.社内使用分出荷振替データ作成

				// (1)社内使用分出荷明細 選択クエリの実行：2-1 社内使用分出荷明細.sql
				List<PCA出荷明細> 社内使用分出荷明細_List = PurchaseTransferAccess.Select_社内使用分出荷明細(CollectMonth, gSettings.Connect.Junp.ConnectionString);

				// (2)在庫評価単価 選択クエリの実行：2-2 在庫評価単価.sql
				List<在庫評価単価> 在庫評価単価_List = PurchaseTransferAccess.Select_在庫評価単価(gSettings.Connect.Charlie.ConnectionString);

				// (3)当月仕入単価 選択クエリの実行：2-3 当月仕入単価.sql
				List<当月仕入単価> 当月仕入単価_List = PurchaseTransferAccess.Select_当月仕入単価(CollectMonth, gSettings.Connect.Junp.ConnectionString);

				// (4) 社内仕入振替データテーブルの全レコード削除
				// (5) 社内仕入振替データテーブルのレコード追加
				// (6)社内仕入振替データテーブルをエクセルファイルで出力
				// 環境設定.社内使用分振替出力ファイル名：\\storage\公開データ\業務部公開用\経理共有\仕入振替\仕入振替出力用ﾌｧｲﾙ.xlsx
				;

				// 3.貯蔵品社内使用分出荷振替データ作成

				// (1) 貯蔵品社内使用分出荷明細 選択クエリの実行：3-1 貯蔵品社内使用分出荷明細.sql
				List<PCA出荷明細貯蔵品> 貯蔵品社内使用分出荷明細_List = PurchaseTransferAccess.Select_貯蔵品社内使用分出荷明細(CollectMonth, gSettings.Connect.Junp.ConnectionString);

				// (2)在庫評価単価 選択クエリの実行：2-2 在庫評価単価.sql
				// (3)当月仕入単価貯蔵品 選択クエリの実行：3-2 当月仕入単価貯蔵品.sql
				List<当月仕入単価> 当月仕入単価貯蔵品_List = PurchaseTransferAccess.Select_当月仕入単価貯蔵品(CollectMonth, gSettings.Connect.Junp.ConnectionString);

				// (4)貯蔵品社内仕入振替データテーブルの全レコード削除
				// (5)貯蔵品社内仕入振替データテーブルレコード追加
				// (6)貯蔵品社内仕入振替データテーブルをエクセルファイルとして出力
				// 環境設定.貯蔵品社内使用分振替出力ファイル名：\\storage\公開データ\業務部公開用\経理共有\仕入振替\貯蔵品振替出力用ファイル.xlsx
				;

				// 4.社内仕入振替データ作成

				// (1)対象月社内仕入伝票 選択クエリの実行：4-1 対象月社内仕入伝票.sql
				List<対象月社内仕入伝票> 対象月社内仕入伝票_List = PurchaseTransferAccess.Select_対象月社内仕入伝票(CollectMonth, gSettings.Connect.Junp.ConnectionString);

				// (2)対象月社内仕入明細 選択クエリの実行：4-2 対象月社内仕入明細.sql
				List<PCA仕入明細> 対象月社内仕入れ明細_List = PurchaseTransferAccess.Select_対象月社内仕入明細(CollectMonth, gSettings.Connect.Junp.ConnectionString);

				// (3)在庫評価単価 選択クエリの実行：2-2 在庫評価単価.sql
				// (4)当月仕入単価 選択クエリの実行：2-3 当月仕入単価.sql
				// (6) \\SQLSV\PCADATAに振替仕入データファイルの新規作成
				// 環境設定.仕入振替出力ファイル名：振替仕入データ.txt
				// 対象月社内仕入れ明細の結果に対し、対象月社内仕入れ明細の結果でフィルタリングした結果をファイルに出力
				// (7)プラス分振替データ出力
				// (8)マイナス分振替データ出力
				;

				// 5.りすとん振替データ作成




			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "クエリ実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		/// <summary>
		///在庫一覧表GG年MM月.CSVの読み込み
		/// </summary>
		/// <param name="pathname">パラメタファイルパス名</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>在庫一覧表リスト</returns>
		public List<在庫一覧表> ReadZaikoListCsvFile(string pathname, out string msg)
		{
			msg = string.Empty;

			List<在庫一覧表> dataList = new List<在庫一覧表>();
			if (File.Exists(pathname))
			{
				try
				{
					// テキストファイルの読み込み
					using (StreamReader textfile = new StreamReader(pathname, System.Text.Encoding.GetEncoding("Shift_JIS")))
					{
						string line = textfile.ReadLine();
						bool firstLine = true;
						while (null != line)
						{
							if (0 < line.Length)
							{
								if (!firstLine)
								{
									line = line.Trim(StringUtil.DefalutTrimCharSet);
									if (';' != line[0])
									{
										// コメント行以外
										List<string> split = SplitString.CSVSplitLine(line);
										在庫一覧表 data = new 在庫一覧表();
										data.SetCsvRecord(split);
										dataList.Add(data);
									}
								}
								else
								{
									// １行目はタイトル行なのでスキップ
									firstLine = false;
								}
							}
							line = textfile.ReadLine();
						}
					}
				}
				catch (Exception ex)
				{
					msg = ex.ToString();
					return null;
				}
			}
			else
			{
				msg = pathname + "が存在しません。";
				return null;
			}
			return dataList;
		}
	}
}
