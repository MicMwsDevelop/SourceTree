using System;
using System.IO;
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
		/// 仕入振替単価不明伝票
		/// </summary>
		private const string FUMEI_LIST_TXT = "仕入振替単価不明伝票.txt";

		/// <summary>
		/// 仕入振替単価不明伝票ファイルパス名の取得
		/// </summary>
		public static string FumeiListFilePathname
		{
			get
			{
				return Path.Combine(Directory.GetCurrentDirectory(), FUMEI_LIST_TXT);
			}
		}

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
