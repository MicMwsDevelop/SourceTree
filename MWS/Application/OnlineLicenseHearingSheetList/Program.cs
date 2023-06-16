using System;
using System.Windows.Forms;

namespace OnlineLicenseHearingSheetList
{
	internal static class Program
	{
		public const string ProgramName = "オンライン資格確認ヒアリングシートリスト作成";

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
