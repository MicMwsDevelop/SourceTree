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
		/// <param name="date"></param>
		/// <returns></returns>
		public string GetMemo(Date date)
		{
			return string.Format(@"【銀行振込請求書発行】 {0}締\r\n請求額 \{1}", date.ToString(), BillingAmount);
		}

		///// <summary>
		///// メモ情報を取得
		///// </summary>
		///// <returns></returns>
		//public tMemo TotMemo()
		//{
		//	tMemo memo = new tMemo();
		//	memo.
		//	return memo;
		//}
		//}

		/// <summary>
		/// fMemID
		/// </summary>
		public int fMemID { get; set; }

		/// <summary>
		/// fMemKey
		/// </summary>
		public int fMemKey { get; set; }

		/// <summary>
		/// fMemTable
		/// </summary>
		public string fMemTable { get; set; }

		/// <summary>
		/// fMemType
		/// </summary>
		public string fMemType { get; set; }

		/// <summary>
		/// fMemMemo
		/// </summary>
		public string fMemMemo { get; set; }

		/// <summary>
		/// fMemUpdate
		/// </summary>
		public DateTime? fMemUpdate { get; set; }

		/// <summary>
		/// fMemUpdateMan
		/// </summary>
		public string fMemUpdateMan { get; set; }

		/// <summary>
		/// fMemUrl
		/// </summary>
		public string fMemUrl { get; set; }

		/// <summary>
		/// fMemOriginalPath1
		/// </summary>
		public string fMemOriginalPath1 { get; set; }

		/// <summary>
		/// fMemOriginalPath2
		/// </summary>
		public string fMemOriginalPath2 { get; set; }

		/// <summary>
		/// fMemOriginalPath3
		/// </summary>
		public string fMemOriginalPath3 { get; set; }

		/// <summary>
		/// fMemWlfID1
		/// </summary>
		public int fMemWlfID1 { get; set; }

		/// <summary>
		/// fMemWlfID2
		/// </summary>
		public int fMemWlfID2 { get; set; }

		/// <summary>
		/// fMemWlfID3
		/// </summary>
		public int fMemWlfID3 { get; set; }

		/// <summary>
		/// fMemCatID1
		/// </summary>
		public int fMemCatID1 { get; set; }

		/// <summary>
		/// fMemCatID2
		/// </summary>
		public int fMemCatID2 { get; set; }

		/// <summary>
		/// fMemCatID3
		/// </summary>
		public int fMemCatID3 { get; set; }

		/// <summary>
		/// fMemKubun
		/// </summary>
		public string fMemKubun { get; set; }

		/// <summary>
		/// fMemComment
		/// </summary>
		public string fMemComment { get; set; }
	}
}
