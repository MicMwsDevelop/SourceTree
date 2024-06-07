//
// サービスコード定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2019/02/08 勝呂)
// Ver1.01 Web予約受付対応(2021/09/07 勝呂)
//
namespace CommonLib.BaseFactory
{
	public static class ServiceCodeDefine
	{
		/// <summary>
		/// サービス種別
		/// </summary>
		public enum ServiceType
		{
			/// <summary>
			/// 標準機能
			/// </summary>
			Standard = 1,

			/// <summary>
			/// レセプト
			/// </summary>
			Rezept = 10,

			/// <summary>
			/// １号カルテ
			/// </summary>
			Chart1 = 12,

			/// <summary>
			/// ２号カルテ
			/// </summary>
			Chart2 = 14,

			/// <summary>
			/// 自費診療
			/// </summary>
			Jihi = 16,

			/// <summary>
			/// 会計
			/// </summary>
			Account = 18,

			/// <summary>
			/// 予約管理
			/// </summary>
			Appoint = 20,

			/// <summary>
			/// 各種文書
			/// </summary>
			Document = 22,

			/// <summary>
			/// 各種帳票
			/// </summary>
			Report = 24,

			/// <summary>
			/// 集患
			/// </summary>
			Collect = 26,

			/// <summary>
			/// 分析
			/// </summary>
			Analyze = 28,

			/// <summary>
			/// 他社リンク
			/// </summary>
			Link = 30,

			/// <summary>
			/// 訪問診療
			/// </summary>
			Homon = 32,

			/// <summary>
			/// カルテ拡張
			/// </summary>
			ChartEx = 34,

			/// <summary>
			/// その他拡張
			/// </summary>
			Extra = 36,

			/// <summary>
			/// リモート
			/// </summary>
			Remote = 38,

			/// <summary>
			/// 口腔ケア
			/// </summary>
			MouthCare = 40,

			/// <summary>
			/// 電子カルテ
			/// </summary>
			ElectricChart = 42,

			/// <summary>
			/// TABLET拡張
			/// </summary>
			TabletEx = 44,

			/// <summary>
			/// オンライン資格確認連携
			/// </summary>
			OnlineLicense = 46,

			/// <summary>
			/// 電子処方箋
			/// </summary>
			ElectricPrescription = 48,

			/// <summary>
			/// シカハコ
			/// </summary>
			Shikahako = 1000,

			/// <summary>
			/// りすとん
			/// </summary>
			Liston = 2000,

			/// <summary>
			/// 達人プラスVersion5
			/// </summary>
			TatsujinPlusVer5 = 3000,

			/// <summary>
			/// Microsoft365(SoftBank)
			/// </summary>
			Microsoft365SB = 4000,

			/// <summary>
			/// Microsoft365
			/// </summary>
			Microsoft365 = 4001,

			/// <summary>
			/// オンライン予約システムアポデント
			/// </summary>
			ApoDent = 5000,

			/// <summary>
			/// Ecoレセプトビューワ
			/// </summary>
			EcoReceiptViewer = 6000,

			/// <summary>
			/// ﾌﾟﾛｾｼｱVersion2Web版
			/// </summary>
			Processia = 7000,

			/// <summary>
			/// 問心伝
			/// </summary>
			Monshinden = 8000,

			/// <summary>
			/// DtooL
			/// </summary>
			Dtool = 9000,

			/// <summary>
			/// 達人プラス
			/// </summary>
			TatsujinPlus = 10000,

			/// <summary>
			/// スマート変換サービス
			/// </summary>
			SmartConvert = 11000,

			/// <summary>
			/// Curline
			/// </summary>
			Curline = 12000,

			/// <summary>
			/// 介護伝送
			/// </summary>
			KaigoDenso = 13000,

			/// <summary>
			/// ESET
			/// </summary>
			Eset = 14000,

			/// <summary>
			/// ナビック
			/// </summary>
			Navic = 15000,

			/// <summary>
			/// PC 安心サポート
			/// </summary>
			PcSupport = 16000,

			/// <summary>
			/// PC 安心サポートPlus
			/// </summary>
			PcSupportPlus = 17000,

			/// <summary>
			/// Web予約受付
			/// Ver1.01 Web予約受付対応(2021/09/07 勝呂)
			/// </summary>
			WebAppoint = 17100,

			/// <summary>
			/// Web問診票
			/// </summary>
			WebMonshin = 17200,

			/// <summary>
			/// デンタルパス
			/// </summary>
			DentalPass = 18000,

			/// <summary>
			/// ユーザー
			/// </summary>
			User = 100000,
		}

		/// <summary>
		/// サービスコード
		/// </summary>
		public enum ServiceCode
		{
			/// <summary>
			/// ＭＩＣ ＷＥＢ ＳＥＲＶＩＣＥ 標準機能
			/// </summary>
			StandardPalette = 1001,

			/// <summary>
			/// palette 非ユーザー - 標準サービス
			/// </summary>
			StandardNonPalette = 1099,

			/////////////////////////
			// 10 レセプト

			/// <summary>
			/// レセプト標準サービス
			/// </summary>
			RezeptStandard = 1010100,

			/////////////////////////
			// 12 １号カルテ

			/// <summary>
			/// １号カルテ標準サービス
			/// </summary>
			Chart1Standard = 1012100,

			/// <summary>
			/// １号カルテ傷病名欄発行
			/// </summary>
			Chart1Byomeiran = 1012120,

			/////////////////////////
			// 14 ２号カルテ

			/// <summary>
			/// ２号カルテ標準サービス
			/// </summary>
			Chart2Standard = 1014100,

			/// <summary>
			/// ２号カルテ月締め行発行
			/// </summary>
			CharttTsukishime = 1014120,

			/////////////////////////
			// 16 自費診療

			/// <summary>
			/// 自費カルテ発行
			/// </summary>
			JihiChart = 1016100,

			/// <summary>
			/// 自費処置集計表
			/// </summary>
			JihiShukei = 1016120,

			/// <summary>
			/// 自費処置頻度表
			/// </summary>
			JihiHindo = 1016140,

			/// <summary>
			/// 自費見積書発行
			/// </summary>
			JihiEstimate = 1016180,

			/// <summary>
			/// インプラント管理
			/// </summary>
			JihiImplant = 1016200,

			/////////////////////////
			// 18 会計

			/// <summary>
			/// 領収証・明細書発行
			/// </summary>
			AccountReceipt = 1018100,

			/// <summary>
			/// 未収金・預り金一覧表
			/// </summary>
			AccountMishukin = 1018120,

			/// <summary>
			/// 調整金一括処理
			/// </summary>
			AccountChoseikin = 1018140,

			/// <summary>
			/// 調整金一覧表
			/// </summary>
			AccountChoseikinList = 1018160,

			/// <summary>
			/// 物品販売集計表
			/// </summary>
			AccountBuppinShukei = 1018180,

			/// <summary>
			/// 物品販売頻度表
			/// </summary>
			AccountBuppinHindo = 1018200,

			/// <summary>
			/// 物品販売一覧表
			/// </summary>
			AccountBuppinList = 1018220,

			/// <summary>
			/// レジ金管理
			/// </summary>
			AccountRegisterManagement = 1018240,

			/// <summary>
			/// 治療費見積
			/// </summary>
			AccountEstimate = 1018260,

			/// <summary>
			/// 治療費等請求書
			/// </summary>
			AccountSeikyusho = 1018280,

			/// <summary>
			/// 自動釣銭機連携
			/// </summary>
			AccountAutoChange = 1018300,

			/// <summary>
			/// カスタマーディスプレイ
			/// </summary>
			AccountCustomerDisplay = 1018320,

			/////////////////////////
			// 20 予約管理

			/// <summary>
			/// 予約管理標準サービス
			/// </summary>
			AppointStandard = 1020100,

			/// <summary>
			/// 予約キャンセル管理
			/// </summary>
			AppointCancel = 1020120,

			/////////////////////////
			// 22 各種文書

			/// <summary>
			/// 処方箋発行
			/// </summary>
			DocumentShohosen = 1022100,

			/// <summary>
			/// 服用説明書発行
			/// </summary>
			DocumentFukuyo = 1022120,

			/// <summary>
			/// 服用説明書発行（定型）
			/// </summary>
			DocumentFukuyoForm = 1022140,

			/// <summary>
			/// 問診票発行
			/// </summary>
			DocumentMonshin = 1022160,

			/// <summary>
			/// 情報提供用文書発行
			/// </summary>
			DocumentInfomation = 1022180,

			/// <summary>
			/// 紹介状発行
			/// </summary>
			DocumentIntroduce = 1022200,

			/////////////////////////
			// 24 各種帳票

			/// <summary>
			/// 日計表
			/// </summary>
			ReportDaily = 1024100,

			/// <summary>
			/// 月計表
			/// </summary>
			ReportMonthly = 1024120,

			/// <summary>
			/// 年計表
			/// </summary>
			ReportYearly = 1024140,

			/// <summary>
			/// ドクター別患者集計表
			/// </summary>
			ReportDoctorShukei = 1024160,

			/// <summary>
			/// 保険種別集計表
			/// </summary>
			ReportHokenKindShukei = 1024180,

			/// <summary>
			/// 点数集計表
			/// </summary>
			ReportPointShukei = 1024200,

			/// <summary>
			/// 保険処置頻度表
			/// </summary>
			ReportHindoHoken = 1024220,

			/// <summary>
			/// 摘要欄文頻度表
			/// </summary>
			ReportHindoTekiyo = 1024240,

			/// <summary>
			/// 病名頻度表
			/// </summary>
			ReportHindoByomei = 1024260,

			/// <summary>
			/// 薬価頻度表
			/// </summary>
			ReportHindoYakka = 1024280,

			/// <summary>
			/// カルテ文頻度表
			/// </summary>
			ReportHindoChart = 1024300,

			/// <summary>
			/// 登録患者一覧表
			/// </summary>
			ReportPatientList = 1024320,

			/// <summary>
			/// 保険証確認患者一覧表
			/// </summary>
			ReportComfirmInsuranceList = 1024340,

			/// <summary>
			/// 点数一覧表
			/// </summary>
			ReportPointList = 1024360,

			/// <summary>
			/// 薬価一覧表
			/// </summary>
			ReportYakkaList = 1024380,

			/// <summary>
			/// 算定ルール一覧表
			/// </summary>
			ReportRuleList = 1024400,

			/// <summary>
			/// 保険請求額概算集計表
			/// </summary>
			ReportHokenSeikyu = 1024420,

			/// <summary>
			/// キャッシュレス対応帳票
			/// </summary>
			CashlessList = 1024440,

			/////////////////////////
			// 26 集患

			/// <summary>
			/// リコール
			/// </summary>
			CollectRecall = 1026100,

			/// <summary>
			/// 患者カード
			/// </summary>
			CollectPatientCard = 1026120,

			/////////////////////////
			// 28 分析

			/// <summary>
			/// 分析標準サービス
			/// </summary>
			AnalyzeStandard = 1028100,

			/// <summary>
			/// 地図分析
			/// ※廃止サービス
			/// </summary>
			AnalyzeMap = 1028120,

			/////////////////////////
			// 30 他社リンク

			/// <summary>
			/// ＰＬＭ
			/// </summary>
			LinkPlm = 1030100,

			/// <summary>
			/// ３ＤｅｎｔＭＯＶＩＥ
			/// ※廃止サービス
			/// </summary>
			LinkThreeDentMovie = 1030120,

			/// <summary>
			/// 患者情報出力
			/// </summary>
			LinkPatientOut = 1030140,

			/// <summary>
			/// 介護連携
			/// </summary>
			LinkKaigo = 1030160,

			/// <summary>
			/// デンタルカルチャー
			/// </summary>
			LinkDentalCulture = 1030180,

			/// <summary>
			/// 予約連携
			/// </summary>
			LinkAppoint = 1030200,

			/////////////////////////
			// 32 訪問診療

			/// <summary>
			/// 訪問診療標準サービス
			/// </summary>
			HomonStadard = 1032100,

			/// <summary>
			/// 介護保険請求
			/// </summary>
			HomonKaigo = 1032120,

			/////////////////////////
			// 34 カルテ拡張

			/// <summary>
			/// シェーマ
			/// </summary>
			ChartExShema = 1034100,

			/// <summary>
			/// 検査表発行
			/// </summary>
			ChartExKensa = 1014140,

			/////////////////////////
			// 36 その他拡張

			/// <summary>
			/// 保険証ＯＣＲ
			/// </summary>
			ExHokenshoOcr = 1036100,

			/// <summary>
			/// 当月累計
			/// </summary>
			ExMonthlyRuikei = 1036120,

			/// <summary>
			/// 補綴状況管理(ｺﾝﾃﾞｨｼｮﾝﾋﾞｭｰ)
			/// </summary>
			ExConditionView = 1036140,

			/// <summary>
			/// スマホDE診察券
			/// </summary>
			ExSmartPhoneCard = 1036180,

			/// <summary>
			/// 電子レセプトカルテビューワ
			/// </summary>
			ExRezeptComputeChartViewer = 1036200,

			/// <summary>
			/// paletteアカウント
			/// </summary>
			ExPaletteAccount = 1036220,

			/// <summary>
			/// クラウドバックアップ
			/// </summary>
			ExCloudBackup = 1036260,

			/// <summary>
			/// クラウドバックアップ（PC安心サポートPlus）
			/// </summary>
			ExCloudBackupPcSupportPlus = 1036280,

			/////////////////////////
			// 38 リモート

			/// <summary>
			/// リモートサービス
			/// </summary>
			RemoteService = 1038100,

			/////////////////////////
			// 40 口腔ケア

			/// <summary>
			/// スマート検査表
			/// </summary>
			SmartKesahyo = 1036160,

			/// <summary>
			/// 口腔ケアノート
			/// </summary>
			MouthCareNote = 1040100,

			/////////////////////////
			// 42 電子カルテ

			/// <summary>
			/// 電子カルテ標準サービス
			/// </summary>
			ElectricChartStandard = 1042100,

			/////////////////////////
			// 44 TABLET拡張

			/// <summary>
			/// TABLET ビューワ
			/// </summary>
			TabletViewer = 1036240,

			/// <summary>
			/// TABLET 口腔内所見
			/// </summary>
			TabletMouthShoken = 1044100,

			/// <summary>
			/// TABLET 検査表
			/// </summary>
			TabletKensahyo = 1044120,

			/////////////////////////
			// 46 オンライン資格確認連携

			/// <summary>
			/// オンライン資格確認連携
			/// </summary>
			OnlineLicenseConfirm = 1046100,

			/// <summary>
			/// 資格情報一括照会
			/// </summary>
			LicenseInquiry = 1046120,

			/// <summary>
			/// 特定健診情報閲覧
			/// </summary>
			HealthCheckInfo = 1046140,

			/// <summary>
			/// 薬剤情報等閲覧
			/// </summary>
			DrugInfo = 1046160,

			/// <summary>
			/// オンライン資格確認医療扶助連携
			/// </summary>
			OnlineLicenseSeiho = 1046180,

			/// <summary>
			/// オンライン資格確認訪問診療連携
			/// </summary>
			OnlineLicenseHomon = 1046200,

			/////////////////////////
			// 48 電子処方箋

			/// <summary>
			/// 電子処方箋管理サービス（院外処方）
			/// </summary>
			ElectricPrescriptionOutside = 1048100,

			/// <summary>
			/// 電子処方箋管理サービス（院内処方）
			/// </summary>
			ElectricPrescriptionInside = 1048120,

			/////////////////////////
			// 1000 シカハコ

			// 1510100 シカハコ
			Shikahako = 1510100,

			/////////////////////////
			// 2000 りすとん

			// 2010100 りすとん
			Liston = 2010100,

			/////////////////////////
			// 3000 達人プラスVersion5

			TatsujinPlus5Monthly = 3510100,	// 達人プラスVersion5 月額版

			/////////////////////////
			// 4000 SmaBiz! Office365

			Office365_SmallBusinessPremium = 2510100,
			Office365_SmallBusinessPremium2L = 2510101,
			SmaBizOffice365ProPLus = 2510120,
			Office365_Business = 2510140,

			/////////////////////////
			// 4001 Office 365

			Office365_BusinessPremium3L = 2510103,
			Office365_SmallBizPremium = 2520100,
			Office365_BusinessN = 2520120,
			Office365_Business2L = 2520140,
			Office365_BusinessPremium2L = 2520160,
			Office365_BusinessPremium3LN = 2520180,

			/////////////////////////
			// 5000 ｵﾝﾗｲﾝ予約ｼｽﾃﾑｱﾎﾟﾃﾞﾝﾄ

			Apodent = 3010100,				// ｵﾝﾗｲﾝ予約ｼｽﾃﾑアポデント
			ApoDentSMS = 3010120,			// ApoDent SMS
			ApoDentVoiceMessage = 3010140,	// ApoDent 自動ボイスメッセージ
			ApoDentLINE = 3010160,          // ApoDent LINEサービス

			/////////////////////////
			// 6000 Ecoレセプトビューワ

			EcoRezeptViewer = 4010100,

			/////////////////////////
			// 7000 ﾌﾟﾛｾｼｱVersion2Web版

			ProceciaVersion2 = 4510100,     // プロセシアVersion2Web版

			/////////////////////////
			// 10000 達人プラスVersion6

			TatsujinPlus6 = 5410100,			// 達人プラス Version６
			CloudBackupService10GB = 5410120,   // クラウドバックアップサービス 10GB
			CloudBackupService20GB = 5410140,   // クラウドバックアップサービス 20GB

			/////////////////////////
			// 11000 スマート変換サービス

			DudaMobileSoftbankCS = 5610100,     // DudaMobile SoftbankC&S

			/////////////////////////
			// 12000 Curline登録医

			/// <summary>
			/// Curline登録医
			/// </summary>
			CurlineDoctor = 9010100,


			/////////////////////////
			// 13000 介護伝送

			KaigoDenso = 5810100,   // 介護伝送

			/////////////////////////
			// 14000 ESET

			EsetMonthly1L = 6010100,	// ESET月額版(1ライセンス)
			EsetMonthly3L = 6010120,	// ESET月額版(3ライセンス)
			EsetMonthlt5L = 6010140,    // ESET月額版(5ライセンス)

			/////////////////////////
			// 15000 ナビック

			Navic = 6210100,	// ナビック

			/////////////////////////
			// 16000 PC安心サポート

			/// <summary>
			/// PC安心サポート３年契約
			/// </summary>
			PcSafetySupport3 = 6410100,

			/// <summary>
			/// PC安心サポート１年契約
			/// </summary>
			PcSafetySupport1 = 6410120,

			/// <summary>
			/// PC安心サポート１年契約(更新用)
			/// </summary>
			PcSafetySupportContinue = 6410160,

			/////////////////////////
			// 17000 PC安心サポートPlus

			/// <summary>
			/// PC安心サポート３年契約Plus
			/// </summary>
			PcSafetySupportPlus3 = 6610120,

			/// <summary>
			/// PC安心サポート１年契約Plus
			/// </summary>
			PcSafetySupportPlus1 = 6610100,

			/// <summary>
			/// PC安心サポート１年契約(更新用)Plus
			/// </summary>
			PcSafetySupportPlusContinue = 6610140,

			/////////////////////////
			// 17100 Web予約受付

			/// <summary>
			/// Web予約受付
			/// Ver1.01 Web予約受付対応(2021/09/07 勝呂)
			/// </summary>
			WebAppoint = 6810100,

			/////////////////////////
			// 17200 Web問診票

			/// <summary>
			/// Web問診票
			/// </summary>
			WebMonshin = 7010100,

			/////////////////////////
			// 18000 デンタルパス

			/// <summary>
			/// デンタルパス
			/// </summary>
			DentalPass = 3210100,

			/// <summary>
			/// デンタルパス SMS
			/// </summary>
			DentalPassSms = 3210120,

			/////////////////////////
			// 100000 ユーザー

			/// <summary>
			/// palette ES
			/// </summary>
			PaletteES = 9910100,

			/// <summary>
			/// palette ES ソフトウェア保守 6年
			/// </summary>
			SoftwareMainte6 = 9910120,

			/// <summary>
			/// palette ES ソフトウェア保守 1年
			/// </summary>
			SoftwareMainte1 = 9910140,
		}
	}
}
