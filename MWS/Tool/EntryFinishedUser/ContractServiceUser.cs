using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;
using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.BaseFactory;

namespace EntryFinishedUser
{
	public class ContractServiceUser
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerID { get; set; }

		/// <summary>
		/// 得意先No
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 終了月
		/// </summary>
		public YearMonth? FinishedYearMonth { get; set; }

		/// <summary>
		/// 拠点コード
		/// </summary>
		public string AreaCode { get; set; }

		/// <summary>
		/// 拠点名
		/// </summary>
		public string AreaName { get; set; }

		/// <summary>
		/// サービスID/PCA商品マスタ
		/// </summary>
		public string ServiceID { get; set; }

		/// <summary>
		/// サービス名
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// 利用終了日
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool FinishedUser { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ContractServiceUser()
		{
			CustomerID = 0;
			TokuisakiNo = string.Empty;
			UserName = string.Empty;
			FinishedYearMonth = null;
			AreaCode = string.Empty;
			AreaName = string.Empty;
			ServiceID = string.Empty;
			ServiceName = string.Empty;
			StartDate = null;
			EndDate = null;
			FinishedUser = false;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="user">終了ユーザー情報</param>
		public ContractServiceUser(EntryFinishedUserData user)
		{
			CustomerID = user.CustomerID;
			TokuisakiNo = user.TokuisakiNo;
			UserName = user.UserName;
			FinishedYearMonth = user.FinishedYearMonth;
			AreaCode = user.AreaCode;
			AreaName = user.AreaName;
			ServiceID = string.Empty;
			ServiceName = string.Empty;
			StartDate = null;
			EndDate = null;
			FinishedUser = user.FinishedUser;
		}

		/// <summary>
		/// エクセル行出力用
		/// </summary>
		/// <returns></returns>
		public object[] GetExcelData()
		{
			return new object[] {
			CustomerID
			, TokuisakiNo
			, UserName
			, FinishedYearMonth.HasValue ? FinishedYearMonth.Value.ToString() : ""
			, AreaCode
			, AreaName
			, ServiceID
			, ServiceName
			, StartDate.HasValue ? StartDate.Value.ToShortDateString() : ""
			, EndDate.HasValue ? EndDate.Value.ToShortDateString() : ""
			, FinishedUser ? "非ユーザー" : "MICユーザー"
			};
		}

		/// <summary>
		/// エクセル行出力用
		/// はなはなし用
		/// </summary>
		/// <returns></returns>
		public object[] GetExcelData2()
		{
			return new object[] {
			CustomerID
			, TokuisakiNo
			, UserName
			, FinishedYearMonth.HasValue ? FinishedYearMonth.Value.ToString() : ""
			, AreaCode
			, AreaName
			, ServiceID
			, ServiceName
			, FinishedUser ? "非ユーザー" : "MICユーザー"
			};
		}

		/// <summary>
		/// ListView表示情報の取得 
		/// </summary>
		/// <returns>表示情報</returns>
		public string[] GetListViewData()
		{
			string[] array = new string[9];
			array[0] = TokuisakiNo;
			array[1] = UserName;
			array[2] = FinishedYearMonth.HasValue ? FinishedYearMonth.Value.ToString(): "";
			array[3] = AreaName;
			array[4] = ServiceID;
			array[5] = ServiceName;
			array[6] = StartDate.HasValue ? StartDate.Value.ToShortDateString(): "";
			array[7] = EndDate.HasValue ? EndDate.Value.ToShortDateString() : "";
			array[8] = FinishedUser ? "非" : "MIC";
			return array;
		}

		/// <summary>
		/// ListView表示情報の取得 
		/// Curlineクラウド利用、はなはなし用
		/// </summary>
		/// <returns>表示情報</returns>
		public string[] GetListViewData2()
		{
			string[] array = new string[7];
			array[0] = TokuisakiNo;
			array[1] = UserName;
			array[2] = FinishedYearMonth.HasValue ? FinishedYearMonth.Value.ToString() : "";
			array[3] = AreaName;
			array[4] = ServiceID;
			array[5] = ServiceName;
			array[6] = FinishedUser ? "非" : "MIC";
			return array;
		}

		/// <summary>
		/// PC安心サポート製品
		/// </summary>
		/// <returns></returns>
		public static int[] PcSupportSeriveID()
		{
			int[] array = new int[3];
			array[0] = (int)ServiceCodeDefine.ServiceCode.PcSafetySupport3;			// PC安心サポート3年契約
			array[1] = (int)ServiceCodeDefine.ServiceCode.PcSafetySupport1;			// PC安心サポート1年契約
			array[2] = (int)ServiceCodeDefine.ServiceCode.PcSafetySupportContinue;	// PC安心サポート1年更新
			return array;
		}

		/// <summary>
		/// ナルコーム製品
		/// </summary>
		/// <returns></returns>
		public static int[] NarcohmSeriveID()
		{
			int[] array = new int[10];
			array[0] = (int)ServiceCodeDefine.ServiceCode.Apodent;                // ｵﾝﾗｲﾝ予約ｼｽﾃﾑアポデント
			array[1] = (int)ServiceCodeDefine.ServiceCode.ApoDentSMS;             // ApoDent SMS
			array[2] = (int)ServiceCodeDefine.ServiceCode.ApoDentVoiceMessage;    // ApoDent 自動ボイスメッセージ
			array[3] = (int)ServiceCodeDefine.ServiceCode.ApoDentLINE;            // ApoDent LINEサービス
			array[4] = (int)ServiceCodeDefine.ServiceCode.ProceciaVersion2;       // プロセシアVersion2Web版
			array[5] = (int)ServiceCodeDefine.ServiceCode.TatsujinPlus6;          // 達人プラス Version６
			array[6] = (int)ServiceCodeDefine.ServiceCode.Navic;                  // ナビック
			array[7] = (int)ServiceCodeDefine.ServiceCode.TatsujinPlus5Monthly;   // 達人プラスVersion5 月額版
			array[8] = (int)ServiceCodeDefine.ServiceCode.CloudBackupService10GB; // クラウドバックアップサービス 10GB
			array[9] = (int)ServiceCodeDefine.ServiceCode.CloudBackupService20GB; // クラウドバックアップサービス 20GB
			return array;
		}

		/// <summary>
		/// Microsoft365製品
		/// </summary>
		/// <returns></returns>
		public static int[] Microsoft365SeriveID()
		{
			int[] array = new int[10];
			array[0] = (int)ServiceCodeDefine.ServiceCode.Office365_SmallBusinessPremium;   // Office365 Small Business Premium
			array[1] = (int)ServiceCodeDefine.ServiceCode.Office365_SmallBusinessPremium2L; // Office365 Small Business Premium 2ﾗｲｾﾝｽ
			array[2] = (int)ServiceCodeDefine.ServiceCode.SmaBizOffice365ProPLus;           // SmaBiz! Office365 ProPLus
			array[3] = (int)ServiceCodeDefine.ServiceCode.Office365_Business;				// Office365 Business
			array[4] = (int)ServiceCodeDefine.ServiceCode.Office365_BusinessPremium3L;      // Office365 Business Premium 3
			array[5] = (int)ServiceCodeDefine.ServiceCode.Office365_SmallBizPremium;        // Office365 Small Biz Premium
			array[6] = (int)ServiceCodeDefine.ServiceCode.Office365_BusinessN;              // Office365 Business
			array[7] = (int)ServiceCodeDefine.ServiceCode.Office365_Business2L;             // Office365 Business 2L
			array[8] = (int)ServiceCodeDefine.ServiceCode.Office365_BusinessPremium2L;      // Office365 Business Premium 2L
			array[9] = (int)ServiceCodeDefine.ServiceCode.Office365_BusinessPremium3LN;     // Office365 Business Premium 3
			return array;
		}
	}
}
