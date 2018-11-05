//
// Program.cs
//
// PCA仕入データ業務委託手数料再計算ツール
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/05 勝呂)
//
///////////////////////////////////////////////////////////////
// 処理内容
// CEC製課金データ作成で出力したPCA仕入データ.csvで月額課金の業務委託手数料に
// プラットフォーム利用料が対象金額に加算された金額に対して、業務委託手数料が
// 算出されてしまっているので、プラットフォーム利用料を引いた金額で算出し、新
// たにPCA仕入データ.csvを出力する
//
// 作成理由
// H30.10中に800001 MIC WEB SERVICE(ﾌﾟﾗｯﾄﾌｫｰﾑ利用 月額)の商品区分１の値を
// 掛率100%商品からソフトに変更したことにより、H30.11に出力した分とH30.9、H30.10
// の差分が発生したため
///////////////////////////////////////////////////////////////
// 
using System;
using System.Windows.Forms;

namespace CalcBusinessConsignCommission
{
	static class Program
	{
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
