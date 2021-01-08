using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepairOrderSlip
{
	static class Program
	{
		/// <summary>
		/// データベース接続先
		/// </summary>
		public const bool DATABASE_ACCESS_CT = true;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new RepairMatomeForm());
		}
	}
}
