using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CheckOrderSlip
{
	static class Program
	{
		/// <summary>
		/// データベース接続先
		/// </summary>
		public const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// テストメール送信
		/// </summary>
		public const bool TEST_MAIL_SEND = false;

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
