//
// Program.cs
//
// paletteES 起票不備連絡ツール プログラムクラス
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
		public const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// テストメール送信
		/// </summary>
		public const bool TEST_MAIL_SEND = false;

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
#if DEBUG
			Date findDate = new Date(2019, 10, 1);
#else
			Date findDate = Date.Today;
#endif
			List<OrderVoucher> list = CheckVoucherPaletteESAccess.GetPaletteESOrderVoucherList(findDate, DATABASE_ACCESS_CT);
			if (null != list)
			{
				// paletteESのみ抽出
				List<OrderVoucher> listES = OrderVoucher.GetAllPaletteES(list);
				if (null != listES)
				{
					List<OrderVoucher> listMail = new List<OrderVoucher>();
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
							es.ErrorList.AddRange(logList);
							listMail.Add(es);
						}
					}
					if (0 < listMail.Count)
					{
						// メール送信
						//SendMailControl.SendMail(listMail);
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
