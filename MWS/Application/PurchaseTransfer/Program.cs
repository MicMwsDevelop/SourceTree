using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PurchaseTransfer
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "仕入振替";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver0.00(2021/12/09)";

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Forms.MainForm());
		}
	}
}
