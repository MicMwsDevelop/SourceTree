using MwsServiceStatus.Settings;
using System;
using System.Windows.Forms;

namespace MwsServiceStatus
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "MWSサービスステータス出力";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static MwsServiceStatusSettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = MwsServiceStatusSettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}
	}
}
