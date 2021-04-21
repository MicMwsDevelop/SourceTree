using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VariousReportOut
{
	static class Program
	{
		/// <summary>
		/// データベース接続先
		/// </summary>
		public const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "各種書類出力";

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
