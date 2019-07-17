using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
using System.Data;

namespace ShipManage
{
	static class Program
	{

		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		public static bool DATABACE_ACCEPT_CT = true;

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
				if ("/ Auto" == cmds[1])
				{
					// 2010/11/19 自動起動時は平日のみ実行とした
					//if (平日)
					{
						// 平日
						bAutoBooted = true;
					}
				}
			}
			if (bAutoBooted)
			{
				// 自動起動モード
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


			return true;
		}

		/// <summary>
		/// 000250 配送センター出荷分 仕入先名の取得
		/// </summary>
		/// <returns>仕入先名</returns>
		private static string GetShiireSakimei()
		{
			DataTable table = JunpDatabaseAccess.Select_vMicPCA仕入先マスタ("ms_tcd = '000250'", "", DATABACE_ACCEPT_CT);
			if (null != table && 1 == table.Rows.Count)
			{
				return table.Rows[0]["rms_mei1"].ToString().Trim();
				
			}
			return string.Empty;
		}
	}
}
