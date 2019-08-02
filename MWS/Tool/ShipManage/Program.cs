using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MwsLib.Common;
using MwsLib.DB;
using MwsLib.DB.SqlServer.Junp;
using System.Data;
using System.Data.SqlClient;
using ShipManage.Settings;

namespace ShipManage
{
	static class Program
	{
		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		public static bool DATABACE_ACCEPT_CT = true;

		public static ShipManageSettings settings;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// 出荷日の取得
			Date? defSyukaDate = SetDefSyukaDate();


			// コマンドライン引数を配列で取得する
			bool bAutoBooted = false;
			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("/Auto" == cmds[1])
				{
					// 2010/11/19 自動起動時は平日のみ実行とした
					//if (平日)
					{
						// 平日
						bAutoBooted = true;
					}
				}
			}
			// 環境設定の読込み
			settings = ShipManageSettingsIF.GetShipManageSettings();

			if (bAutoBooted)
			{
				// 自動起動モード
				MakeDataAndSendMail(Date.Today, "");
			}
			else
			{
				// 手動起動モード
				Application.Run(new MainForm());
			}
		}

		/// <summary>
		/// 出荷日をプログラム実行日から導出
		/// 手動起動の場合は規定値として使用される
		/// </summary>
		/// <returns>出荷日</returns>
		private static Date? SetDefSyukaDate()
		{
			return Date.Today;
		}

		/// <summary>
		/// 出荷代行データ作成とメール送信
		/// </summary>
		/// <param name="defSyukaDate">出荷日</param>
		/// <param name="juchuNo">指定受注No</param>
		/// <returns>判定</returns>
		private static bool MakeDataAndSendMail(Date defSyukaDate, string juchuNo)
		{
			// 出荷代行データ作成
			if (MakeShippingData(defSyukaDate, juchuNo))
			{
			}

/*

	If MakeDataAndSendMail Then
	  If gnCntOutDen <> 0 Then '出荷対象伝票あり？

		ReDim HassouzumiList(0)
        If sDenNum = "" Then '通常発送？

		  'Call 在庫引当表作成(False) '在庫引当表エクセルファイル作成
		  Call 在庫引当表作成2(False) '在庫引当表エクセルファイル作成

		  If gbAutoBooted Then '自動起動モード

			Call MakeHassouzumiList() '緊急出荷済み商品リスト作成

		  End If

		Else '受注番号指定で緊急出荷？

		  Call SaveDenNum(dDate, sDenNum) '受注番号指定で緊急出荷の受注番号を記憶

		End If


		MakeDataAndSendMail = MakeArchiveFile(sDenNum) '出荷依頼用アーカイブファイル作成

		If MakeDataAndSendMail Then
		  If sDenNum <> "" Then '緊急発送？

			MakeDataAndSendMail = PrintHassouIraisho() '発送依頼書を印刷

		  End If

		  If MakeDataAndSendMail Then
			MakeDataAndSendMail = MakeAndSendMail(dDate, KinkyuuIraiKokyaku.ClientName) 'メール作成・送信

			If MakeDataAndSendMail Then
			  MakeDataAndSendMail = MoveHassouDataFiles(dDate) '出荷依頼用ファイルをフォルダ移動

			  If MakeDataAndSendMail Then
				MakeDataAndSendMail = MovePCAReadDataFiles() 'ＰＣＡ読込用データファイルをフォルダ移動

				If MakeDataAndSendMail Then
				  If sDenNum = "" Then '通常発送？

					MakeDataAndSendMail = PrintYouHacchuuList() '要発注品リストを印刷

				  End If

				End If

			  End If

			End If

		  End If

		End If

	  End If

*/
			return true;
		}

		/// <summary>
		/// 出荷代行データ作成
		/// </summary>
		/// <param name="defSyukaDate">出荷日</param>
		/// <param name="juchuNo">指定受注No</param>
		/// <returns>判定</returns>
		private static bool MakeShippingData(Date defSyukaDate, string juchuNo)
		{
			/*
						// To トランザクション エラー発生により追加 MOD: 2004 / 01 / 31:holy
						If gstrLinkServer = "1" Then 'リンクサーバの場合の前処理
							cmd.CommandText = "SET XACT_ABORT ON"
							dr = cmd.ExecuteReader
							dr.Close()
						End If
			*/

			// 仕入先名の取得
			string gaShiMei = GetShiireSakimei();

			// tMih送料商品コードから送料データの商品コードを取得
			string[] gaStrScd = GetSouryoScd();

			// ＰＣＡ商品マスタから代引き手数料の商品名と金額を取得
			GetDaibikiTesuryo(out string sDaibikiTesuryoName, out int lDaibikiTesuryoGaku);

			// tMic離島 から佐川急便の代引き発送の取扱い不能地域住所を取得
			//rc = GetRitoData()

			// tMihPca在庫引当表Jの引当在庫数をクリアする
			ClearHikiateZaikoCount();

			// 送料データの商品コードから条件文を作成
			string strWhereScd1 = string.Empty;
			string strWhereScd2 = string.Empty;
			for (int i = 0; i < gaStrScd.Length; i++)
			{
				if (0 < i)
				{
					strWhereScd1 += " AND ";
					strWhereScd2 += " OR ";
				}
				strWhereScd1 += string.Format("jucd_scd <> '{0}'", gaStrScd[i]);
				strWhereScd2 += string.Format("jucd_scd = '{0}'", gaStrScd[i]); ;
			}
			string sSql = "SELECT jucd_jucbi, jucd_jno, jucd_tcd FROM vMicPCA受注明細 WHERE jucd_flg = '0'";
			if (0 < juchuNo.Length)
			{
				sSql += string.Format(" AND jucd_jno = '{0}'", juchuNo);
			}
			sSql += " AND " + strWhereScd1 + " GROUP BY jucd_jucbi, jucd_jno, jucd_tcd ORDER BY jucd_jucbi, jucd_jno";







			string sql = "SELECT * FROM vMicPCA受注明細"
			+ " WHERE jucd_flg = 0 AND jucd_tcd = '010223' AND jucd_jucbi = 20160731 AND jucd_jno = '168024'"
			+ " AND jucd_scd = '000014' AND jucd_scd = '000020' AND jucd_scd = '000600' AND jucd_scd = '000605'"
			+ " ORDER BY jucd_seq";

			return true;
		}

		/// <summary>
		/// 000250 配送センター出荷分 仕入先名の取得
		/// </summary>
		/// <returns>仕入先名</returns>
		private static string GetShiireSakimei()
		{
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入先マスタ], "rms_tcd = '000250'", "", DATABACE_ACCEPT_CT);
			if (null != table && 1 == table.Rows.Count)
			{
				return table.Rows[0]["rms_mei1"].ToString().Trim();
				
			}
			return string.Empty;
		}

		/// <summary>
		/// tMih送料商品コードから送料データの商品コードを取得
		/// </summary>
		/// <returns></returns>
		private static string[] GetSouryoScd()
		{
			List<string> scd = new List<string>();

			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih送料商品コード], "", "f商品コード", DATABACE_ACCEPT_CT);
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					scd.Add(row["f商品コード"].ToString().Trim());
				}
			}
			return scd.ToArray();
		}

		/// <summary>
		/// ＰＣＡ商品マスタから代引き手数料の商品名と金額を取得
		/// </summary>
		/// <param name="name">商品名</param>
		/// <param name="price">金額</param>
		private static void GetDaibikiTesuryo(out string name, out int price)
		{
			name = string.Empty;
			price = 0;

			//# 代引き手数料の商品コード
			//DaibikiTesuryoCode = "000610"
			DataTable table = JunpDatabaseAccess.SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ], string.Format("sms_scd = '{0}'", settings.DaibikiTesuryoCode), "", DATABACE_ACCEPT_CT);
			if (null != table && 1 == table.Rows.Count)
			{
				name = table.Rows[0]["sms_mei"].ToString().Trim();
				price = DataBaseValue.ConvObjectToInt(table.Rows[0]["sms_hyo"]);
			}
		}

		/// <summary>
		/// tMihPca在庫引当表Jの引当在庫数をクリアする
		/// </summary>
		private static int ClearHikiateZaikoCount()
		{
			string sqlString = string.Format(@"UPDATE {0} SET f引当在庫数 = @1", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMihPca在庫引当表J]);
			SqlParameter[] param = { new SqlParameter("@1", "0") };
			return JunpDatabaseAccess.UpdateSetJunpDatabase(sqlString, param, DATABACE_ACCEPT_CT);
		}
	}
}
