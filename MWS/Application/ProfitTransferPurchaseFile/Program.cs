//
// Program.cs
// 
// 部署間利益付け替え仕入データ作成プログラムファイル
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.00 新規作成(2025/09/22 越田)
/////////////////////////////////////////////////////////
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProfitTransferPurchaseFile
{
	internal static class Program
	{
		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string gVersionStr = "Ver1.00(2025/09/22)";

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
