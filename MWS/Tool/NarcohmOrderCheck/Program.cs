using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NarcohmOrderCheck
{
    static class Program
    {
		public const string ProductName = "ナルコーム製品申込解約管理";


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
