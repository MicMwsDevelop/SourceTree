using System;
using System.Windows.Forms;

namespace EntryFinishedUser
{
	static class Program
	{
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
			Application.Run(new MainForm());
		}
	}
}
