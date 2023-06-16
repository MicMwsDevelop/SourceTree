using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorningBatch
{
	static class Program
	{
		/// <summary>
		/// 接続文字列
		/// </summary>
		//public static string gConnectStr = "Server=SQLSV;Database=charlieDB;User ID=web;Password=02035612;Min Pool Size=1";
		public static string gConnectStr = "Server=TESTSV;Database=charlieDB;User ID=web;Password=02035612;Min Pool Size=1";

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
