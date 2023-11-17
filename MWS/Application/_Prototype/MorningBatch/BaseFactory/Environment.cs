using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorningBatch.BaseFactory
{
	public class Environment
	{
		/// <summary>
		/// システム日付
		/// </summary>
		public DateTime? SysDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? InsDate { get; set; }

		/// <summary>
		/// 開始日
		/// </summary>
		public DateTime? sSTART_DATE { get; set; }

		/// <summary>
		/// 終了日
		/// </summary>
		public DateTime? sEND_DATE { get; set; }

		/// <summary>
		/// 顧客管理基本へ追加した顧客IDのリスト
		/// </summary>
		public List<int> lstInsCustomerId { get; set; }

		/// <summary>
		/// システム日時の月初日
		/// </summary>
		public DateTime? SysDate_1st	 { get; set; }

		/// <summary>
		/// 申込データのチェック範囲日付作成
		/// </summary>
		public DateTime? sAPPCHK_ST_DATE { get; set; }

		/// <summary>
		/// 一番若い番号を取得
		/// </summary>
		public string Sel_CouplerID_Top { get; set; }

		/// <summary>
		/// ライセンス発行可能フラグ
		/// 0:登録カード未回収 1:登録カード回収済
		/// </summary>
		public bool LICENSE_FLG { get; set; }

		/// <summary>
		/// 営業担当者ID
		/// </summary>
		public string MARKETING_SPECIALIST_ID { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CUSTOMER_ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CUSTOMER_MSG { get; set; }

		/// <summary>
		/// 販売種別
		/// 1:直接 2:販売店
		/// </summary>
		public char SALE_TYPE { get; set; }

		/// <summary>
		/// 申込種別
		/// 0:その他 1:VP 2:UG 3:月額 4:まとめ
		/// </summary>
		public char APPLY_TYPE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string MARKETING_SPECIALIST_MSG { get; set; }

		/// <summary>
		/// サービス名
		/// </summary>
		public string SERVICE_NAME { get; set; }



		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public Environment()
		{
			SysDate = null;
			sSTART_DATE = null;
			sEND_DATE = null;
			lstInsCustomerId = new List<int>();
			SysDate_1st = null;
			Sel_CouplerID_Top = string.Empty;
			LICENSE_FLG = false;
			MARKETING_SPECIALIST_ID = string.Empty;
			CUSTOMER_ID = 0;
			CUSTOMER_MSG = string.Empty;
			SALE_TYPE = '';
			APPLY_TYPE = '';
			MARKETING_SPECIALIST_MSG = string.Empty;
			SERVICE_NAME = string.Empty;
		}

	}
}
