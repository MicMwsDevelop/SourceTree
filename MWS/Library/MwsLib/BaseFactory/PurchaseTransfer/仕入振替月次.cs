using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BaseFactory.PurchaseTransfer
{
	public class 仕入振替月次
	{
		/// <summary>
		/// 部門コード
		/// </summary>
		public string sykd_jbmn { get; set; }

		/// <summary>
		/// 担当者コード
		/// </summary>
		public string sykd_jtan { get; set; }

		/// <summary>
		/// 商品コード
		/// </summary>
		public string sykd_scd { get; set; }

		/// <summary>
		/// マスター区分
		/// </summary>
		public short sykd_mkbn { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string sykd_mei { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int 数量 { get; set; }

		/// <summary>
		/// 単位
		/// </summary>
		public string sykd_tani { get; set; }

		/// <summary>
		/// 評価単価
		/// </summary>
		public int 評価単価 { get; set; }

		/// <summary>
		/// 消費税率
		/// </summary>
		public int sykd_rate { get; set; }

		/// <summary>
		/// 仕入先コード
		/// </summary>
		public string 仕入先コード { get; set; }

		/// <summary>
		/// 仕入日
		/// </summary>
		public int 仕入日 { get; set; }

		/// <summary>
		/// 仕入フラグ
		/// </summary>
		public short 仕入フラグ { get; set; }


		/// <summary>
		/// 部門コードの取得
		/// </summary>
		public string 部門コード
		{
			get
			{
				return sykd_jbmn.Substring(sykd_jbmn.Length - 2, 2);
			}
		}

		/// <summary>
		/// 担当者コードの取得
		/// </summary>
		public string 担当者コード
		{
			get
			{
				return sykd_jtan.Substring(sykd_jtan.Length - 2, 2);
			}
		}

		/// <summary>
		/// 金額の取得
		/// </summary>
		public int 金額
		{
			get
			{
				return 評価単価 * 数量;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 仕入振替月次()
		{
			sykd_jbmn = string.Empty;
			sykd_jtan = string.Empty;
			sykd_scd = string.Empty;
			sykd_mkbn = 0;
			sykd_mei = string.Empty;
			数量 = 0;
			sykd_tani = string.Empty;
			評価単価 = 0;
			sykd_rate = 0;
			仕入先コード = string.Empty;
			仕入日 = 0;
			仕入フラグ = 0;
		}
	}
}
