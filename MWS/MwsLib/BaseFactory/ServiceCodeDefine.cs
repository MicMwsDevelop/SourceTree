namespace MwsLib.BaseFactory
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
			RecieptStandard = 1010100,

			/////////////////////////
			// 16 自費診療

			/// <summary>
			/// 自費見積書発行
			/// </summary>
			JihiEstimate = 1016180,

			/// <summary>
			/// インプラント管理
			/// </summary>
			JihiImplant = 1016200,

			/// <summary>
			/// 治療費等請求書
			/// </summary>
			JihiSeikyusho = 1018280,

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
			// 24 各種帳票

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
			ReportPatient = 1024320,

			/// <summary>
			/// 保険証確認患者一覧表
			/// </summary>
			ReportComfirmInsurance = 1024340,

			/// <summary>
			/// 点数一覧表
			/// </summary>
			ReportPoint = 1024360,

			/// <summary>
			/// 薬価一覧表
			/// </summary>
			ReportYakka = 1024380,

			/// <summary>
			/// 算定ルール一覧表
			/// </summary>
			ReportRule = 1024400,

			/// <summary>
			/// 保険請求額概算集計表
			/// </summary>
			ReportHokenSeikyu = 1024420,

			/////////////////////////
			// 28 分析

			/// <summary>
			/// 地図分析
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
			/// </summary>
			LinkThreeDentMovie = 1030120,

			/// <summary>
			/// 患者情報出力
			/// </summary>
			LinkPatientOut = 1030140,

			/////////////////////////
			// 32 訪問診療

			/// <summary>
			/// 訪問診療標準サービス
			/// </summary>
			HomonStadard = 1032100,

			/////////////////////////
			// 36 その他拡張

			/// <summary>
			/// 保険証ＯＣＲ
			/// </summary>
			ExHokenshoOcr = 1036100,

			/// <summary>
			/// 補綴状況管理(ｺﾝﾃﾞｨｼｮﾝﾋﾞｭｰ)
			/// </summary>
			ExConditionView = 1036140,

			/// <summary>
			/// 電子レセプトカルテビューワ
			/// </summary>
			ExRezeptComputeChartViewer = 1036200,

			/// <summary>
			/// paletteアカウント
			/// </summary>
			ExPaletteAccount = 1036220,

			/////////////////////////
			// 40 口腔ケア

			/// <summary>
			/// 口腔ケアノート
			/// </summary>
			MouthCareNote = 1040100,

			/////////////////////////
			// 42 電子カルテ

			/// <summary>
			/// 電子カルテ標準サービス
			/// </summary>
			ElectricChart = 1042100,

			/////////////////////////
			// 1000 シカハコ

			// 1510100 シカハコ
			Shikahako = 1510100,

			/////////////////////////
			// 2000 りすとん

			// 2010100 りすとん
			Liston = 2010100,

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
			// 100000 ユーザー

			/// <summary>
			/// palette ES
			/// </summary>
			PaletteES = 9910100,
		}
	}
}
