using System;
using System.Windows.Forms;
using MwsLib.Common;
using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.DB.SqlServer.EntryFinishedUser;
using System.Collections.Generic;
using System.Linq;

namespace EntryFinishedUser
{
	static class Program
	{
		/// <summary>
		/// 起動引数
		/// </summary>
		private enum ProgramBootType
		{
			/// <summary>
			/// メイン画面起動
			/// </summary>
			Menu = 0,

			/// <summary>
			/// 終了ユーザーメール送信
			/// </summary>
			SendMail = 1,
		}

		/// <summary>
		/// 起動引数
		/// </summary>
		private static ProgramBootType BootType;

		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		public const bool DATABACE_ACCEPT_CT = true;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// コマンドライン引数を配列で取得する
			BootType = ProgramBootType.Menu;
			string[] cmds = Environment.GetCommandLineArgs();
			Date today = Date.Today;
			if (2 <= cmds.Length)
			{
				if ("1" == cmds[1])
				{
					BootType = ProgramBootType.SendMail;
				}
				if (3 == cmds.Length)
				{
					today = Date.Parse(int.Parse(cmds[2]));
				}
			}
			switch (BootType)
			{
				// メイン画面起動
				case ProgramBootType.Menu:
					Application.Run(new Forms.MainForm());
					break;
				// 終了ユーザーメール送信
				case ProgramBootType.SendMail:
					Program.SendMail(today);
					break;
			}
		}

		private static void SendMail(Date date)
		{
			List<EntryFinishedUserData> work = EntryFinishedUserAccess.GetEntryFinishedUserDataList(DATABACE_ACCEPT_CT);
			List<EntryFinishedUserData> list = work.Where(p => true == p.IsFinishedUser(date.ToYearMonth())).ToList();
			if (0 < list.Count)
			{
				

			}
		}
	}
}
