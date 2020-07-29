using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.BaseFactory.Estore;
using MwsLib.DB.SqlServer.WebJucu;
using MwsLib.Common;
using MwsLib.Log;
using MwsLib.BaseFactory.Estore.Table;
using MwsLib.BaseFactory.Estore.View;
using System.Threading;

namespace WebJucuFileOutput
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// データベース接続先
		/// </summary>
		public const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// 出力先フォルダ
		/// </summary>
		private readonly string ExportFolder = @"c:\_aaa";

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// START
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStart_Click(object sender, EventArgs e)
		{
			//List<WebJucuTmp> tmpList = WebJucuAccess.GetWebJucu(Date.Today, DATABASE_ACCESS_CT);
			List<WebJucu> jucuList = WebJucuAccess.GetWebJucu(new Date(2020,6,12), DATABASE_ACCESS_CT);
			if (0 < jucuList.Count)
			{
				List<tMICestore_log> logList = WebJucuAccess.Get_tMicEstoreLog(DATABASE_ACCESS_CT);
				List<WebJucu> expList = (from tmp in jucuList
										  orderby tmp.受注No
											join log in logList on tmp.order_accept_id equals log.ID
											select tmp).ToList();
				if (0 < expList.Count)
				{
					// 送料追加
					List<vMic部門コード> bumonList = WebJucuAccess.Get_vMic部門コード(DATABASE_ACCESS_CT);

					var query1 = (from s in expList
								  orderby s.受注No
									join m in bumonList on new { a = s.PCA部門No, b = s.得意先No } equals new { a = m.PCA部門No, b = m.得意先コード }
									select new { s.受注No, s.受注日, s.得意先No, s.顧客名, m.PCA部門No, m.PCA主担当No }).Distinct();

					List<WebJucu> soryoList = new List<WebJucu>();
					foreach (var s in query1)
					{
						WebJucu soryo = WebJucu.SoryoData();
						soryo.受注No = s.受注No;
						soryo.受注日 = s.受注日;
						soryo.得意先No = s.得意先No;
						soryo.顧客名 = s.顧客名;
						soryo.PCA部門No = s.PCA部門No;
						soryo.PCA主担当No = s.PCA主担当No;
						soryoList.Add(soryo);
					}
					expList.AddRange(soryoList);

					// 着日指定追加
					var query2 = (from s in expList
								  orderby s.受注No
									where s.希望着日.HasValue == true
									join m in bumonList on new { a = s.PCA部門No, b = s.得意先No } equals new { a = m.PCA部門No, b = m.得意先コード }
									select new { s.受注No, s.受注日, s.納期, s.得意先No, s.顧客名, m.PCA部門No, m.PCA主担当No, 商品名 = s.GetChakubiString() }).Distinct();

					List<WebJucu> chakubiList = new List<WebJucu>();
					foreach (var c in query2)
					{
						WebJucu chakubi = WebJucu.ChakubiData();
						chakubi.受注No = c.受注No;
						chakubi.受注日 = c.受注日;
						chakubi.納期 = c.納期;
						chakubi.得意先No = c.得意先No;
						chakubi.顧客名 = c.顧客名;
						chakubi.PCA部門No = c.PCA部門No;
						chakubi.PCA主担当No = c.PCA主担当No;
						chakubi.商品名 = c.商品名;
						chakubiList.Add(chakubi);
					}
					expList.AddRange(chakubiList);

					// WEB受注ナンバーをPCA用受注ナンバーに振りなおし
					int pcaLastNo = WebJucuAccess.Get_vMic受注最大番号(DATABASE_ACCESS_CT);
					int initJucuNo = expList.Min(p => p.受注No);
					foreach (var exp in expList)
					{
						exp.受注No = (exp.受注No + pcaLastNo) - (initJucuNo - 1);
					}

					// orderby 受注No, マスター区分, goods_code
					expList.Sort((s, d) =>
					{
						if (s.受注No < d.受注No)
						{
							return -1;
						}
						else if (s.受注No > d.受注No)
						{
							return 1;
						}
						else
						{
							if (0 < string.Compare(s.マスター区分, d.マスター区分))
							{
								return -1;
							}
							else if (0 > string.Compare(s.マスター区分, d.マスター区分))
							{
								return 1;
							}
							else
							{
								if (0 < string.Compare(s.goods_code, d.goods_code))
								{
									return -1;
								}
								else if (0 > string.Compare(s.goods_code, d.goods_code))
								{
									return 1;
								}
							}
						}
						return 0;
					});

					string filename = WebJucu.OutputFilename;
					try
					{
						// WebJucu.txtの出力
						using (FileStream fileStream = new FileStream(Path.Combine(ExportFolder, filename), FileMode.Create, FileAccess.Write, FileShare.Read))
						{
							using (StreamWriter writer = new StreamWriter(fileStream, Encoding.GetEncoding("Shift_JIS")))
							{
								foreach (WebJucu exp in expList)
								{
									writer.WriteLine(exp.ToCsvString(10));
								}
								writer.Close();
							}
						}
					}
					catch (IOException ex)
					{
						MessageBox.Show(ex.Message, "ファイル書込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					// tMICestore_logの新規追加
					var query = from jucu in expList
								where jucu.order_accept_id != 0
								select jucu;
					List<tMICestore_log> addNewList = new List<tMICestore_log>();
					foreach (var jucu in query)
					{
						tMICestore_log log = new tMICestore_log();
						log.ID = jucu.order_accept_id;
						log.web受注No = jucu.変更前Web受注No;
						log.PCA受注No = jucu.受注No.ToString();
						log.作成日時 = DateTime.Now;
						addNewList.Add(log);
					}
					//WebJucuAccess.InsertInto_tMICestore_log(addNewList, DATABASE_ACCESS_CT);

					// 受注データ重複チェック
					var tokuisaki = from jucu in expList
									group jucu by new { jucu.受注No, jucu.得意先No } into X
									//where X.Count() > 1
									orderby X.Key.得意先No
									select new { X.Key.受注No, X.Key.得意先No };
					//var query3 = from t in tokuisaki



					MessageBox.Show("汎用データを出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
	}
}
