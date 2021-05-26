using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;
using MwsLib.BaseFactory.Junp.Table;

namespace MwsLib.BaseFactory.WonderWebMemo
{
	/// <summary>
	/// 銀行振込請求書発行先メモ情報
	/// </summary>
	public class MemoBankTransfer
	{
		/// <summary>
		/// 得意先No
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 請求額
		/// </summary>
		public int BillingAmount { get; set; }

		/// <summary>
		/// メモ更新者の取得
		/// </summary>
		public static string GetMemoUpdateMan
		{
			get
			{
				return "営業管理部";
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MemoBankTransfer()
		{
			TokuisakiNo = string.Empty;
			CustomerNo = 0;
			BillingAmount = 0;
		}

		/// <summary>
		/// メモタイプの取得
		/// </summary>
		/// <returns></returns>
		public static string GetMemoType()
		{
			return string.Format("{0} {1}", DateTime.Now.ToString(), MemoBankTransfer.GetMemoUpdateMan);
		}

		/// <summary>
		/// メモの取得
		/// </summary>
		/// <param name="date">締日</param>
		/// <returns></returns>
		public string GetMemo(Date date)
		{
			return string.Format(@"【銀行振込請求書発行】 {0}締\r\n請求額 \{1}", date.ToString(), BillingAmount);
		}

		/// <summary>
		/// メモ情報を取得
		/// </summary>
		/// <param name="date">締日</param>
		/// <returns></returns>
		public tMemo TotMemo(Date date)
		{
			tMemo memo = new tMemo();
			memo.fMemKey = CustomerNo;
			memo.fMemTable = "tClient";
			memo.fMemType = MemoBankTransfer.GetMemoType();
			memo.fMemMemo = GetMemo(date);
			memo.fMemUpdate = DateTime.Now;
			memo.fMemUpdateMan = MemoBankTransfer.GetMemoUpdateMan;
			return memo;
		}
	}
}
