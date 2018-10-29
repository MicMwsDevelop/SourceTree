using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.Common;

namespace PcSafetySupport
{
	static class Program
	{
		public static Date SystemDate;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			SystemDate = Date.Today;

			Application.Run(new Forms.MainForm());
		}
	}
}
