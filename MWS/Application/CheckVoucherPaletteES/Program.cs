//
// Program.cs
//
// paletteES 起票確認ツール プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using MwsLib.BaseFactory.CheckVoucherPaletteES;
using MwsLib.Common;
using MwsLib.DB.SqlServer.CheckVoucherPaletteES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CheckVoucherPaletteES
{
	static class Program
	{
		/// <summary>
		/// データベース接続先
		/// </summary>
		public const bool DATABASEACCESS_CT = false;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			//コマンドライン引数を配列で取得する
			string[] cmds = Environment.GetCommandLineArgs();
			if (1 < cmds.Count())
			{
				if ("Auto" == cmds[1])
				{
					// paletteES起票確認
					CheckOrderVoucher();
					return;
				}
			}
			Application.Run(new MainForm());
		}

		/// <summary>
		/// paletteES起票確認
		/// </summary>
		private static void CheckOrderVoucher()
		{
			CheckVoucherPaletteESLogger.LogStart();

			// paletteES 受注伝票情報リストの取得
			List<OrderVoucher> list = CheckVoucherPaletteESAccess.GetPaletteESOrderVoucherList(Date.Today, DATABASEACCESS_CT);
			if (null != list)
			{
				// paletteESのみ抽出
				List<OrderVoucher> listES = OrderVoucher.GetAllPaletteES(list);
				if (null != listES)
				{
					foreach (OrderVoucher es in listES)
					{
						List<string> logList = new List<string>();

						// paletteESの販売種別がＶＰかどうか？
						if (false == es.IsFormalApplyTypeByPaletteES())
						{
							logList.Add("paletteESの販売種別がＶＰでない");
						}
						// paletteESの利用期間が72ヵ月かどうか？
						if (false == es.IsFormalSpanByPaletteES())
						{
							logList.Add("paletteESの利用期間が72ヵ月でない");
						}
						// 同伝票にｿﾌﾄｳｪｱ保守料72ケ月が存在するか？
						OrderVoucher mainte72 = OrderVoucher.GetSameVoucherMaite72(list, es);
						if (null == mainte72)
						{
							// 別伝票にｿﾌﾄｳｪｱ保守料72ケ月が存在するか？
							mainte72 = OrderVoucher.GetAnotherVoucherMaite72(list, es);
							if (null != mainte72)
							{
								// ｿﾌﾄｳｪｱ保守料72ケ月の販売種別が月額課金かどうか？
								if (false == mainte72.IsFormalApplyTypeByMainte72())
								{
									logList.Add("ｿﾌﾄｳｪｱ保守料72ケ月の販売種別が月額課金でない");
								}
								// ｿﾌﾄｳｪｱ保守料72ケ月の利用期間が72ヵ月かどうか？
								if (false == mainte72.IsFormalSpanByMainte72())
								{
									logList.Add("ｿﾌﾄｳｪｱ保守料72ケ月の利用期間が72ヵ月でない");
								}
								if (es.利用期間.Start != mainte72.利用期間.Start)
								{
									logList.Add("paletteESの利用開始日とｿﾌﾄｳｪｱ保守料の利用開始日が違う");
								}
							}
							else
							{
								// 別伝票にｿﾌﾄｳｪｱ保守料12ケ月が存在するか？
								OrderVoucher mainte12 = OrderVoucher.GetAnotherVoucherMaite12(list, es);
								if (null != mainte12)
								{
									// ｿﾌﾄｳｪｱ保守料12ケ月の販売種別が月額課金かどうか？
									if (false == mainte12.IsFormalApplyTypeByMainte12())
									{
										logList.Add("ｿﾌﾄｳｪｱ保守料12ケ月の販売種別が月額課金でない");
									}
									// ｿﾌﾄｳｪｱ保守料12ケ月の利用期間が12ヵ月かどうか？
									if (false == mainte12.IsFormalSpanByMainte12())
									{
										logList.Add("ｿﾌﾄｳｪｱ保守料12ケ月の利用期間が12ヵ月でない");
									}
									if (es.利用期間.Start != mainte12.利用期間.Start)
									{
										logList.Add("paletteESの利用開始日とｿﾌﾄｳｪｱ保守料の利用開始日が違う");
									}
								}
								else
								{
									logList.Add("ｿﾌﾄｳｪｱ保守料の伝票が存在しない");
								}
							}
						}
						CheckVoucherPaletteESLogger.MainLine(es.GetLogString());
						if (0 < logList.Count)
						{
							CheckVoucherPaletteESLogger.SubLine(logList);
						}
					}
				}
			}
			else
			{
				// paletteESの伝票が存在しない
				;
			}
			CheckVoucherPaletteESLogger.LogEnd();
		}
	}
}
