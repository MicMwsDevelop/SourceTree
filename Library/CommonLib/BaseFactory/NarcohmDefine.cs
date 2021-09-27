using CommonLib.Common;

namespace CommonLib.BaseFactory
{
	public static class NarcohmDefine
    {
        /// <summary>
        /// ナルコーム製品種別
        /// </summary>
        public enum NarcohmProductType
        {
            /// <summary>
            /// 不明
            /// </summary>
            Nothing = 0,

            /// <summary>
            /// 800304 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽVersion6 月額版
            /// </summary>
            TatsujinPlusMonthly = 1,

            /// <summary>
            /// 014800 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 1年ﾊﾟｯｸ
            /// </summary>
            TatsujinPlus1Pack = 2,

            /// <summary>
            /// 014801 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 3年ﾊﾟｯｸ
            /// </summary>
            TatsujinPlus3Pack = 3,

            /// <summary>
            /// 014802 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 6年ﾊﾟｯｸ
            /// </summary>
            TatsujinPlus6Pack = 4,

            /// <summary>
            /// 017286 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 1年ﾊﾟｯｸ(更新)
            /// </summary>
            TatsujinPlus1PackUpdate = 5,

            /// <summary>
            /// 017287 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 3年ﾊﾟｯｸ(更新)
            /// </summary>
            TatsujinPlus3PackUpdate = 6,

            /// <summary>
            /// 017288 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 6年ﾊﾟｯｸ（更新)
            /// </summary>
            TatsujinPlus6PackUpdate = 7,

            /// <summary>
            /// 800301 ﾅﾙｺｰﾑ ｱﾎﾟﾃﾞﾝﾄ 月額費用
            /// </summary>
            ApoDentMonthly = 8,

            /// <summary>
            /// 800310 ﾅﾙｺｰﾑ ｱﾎﾟﾃﾞﾝﾄ LINEｻｰﾋﾞｽ(月額)
            /// </summary>
            ApoDentLineMonthly = 9,

            /// <summary>
            /// 017389 ﾅﾙｺｰﾑ ｱﾎﾟﾃﾞﾝﾄ SMS(月額)
            /// </summary>
            ApoDentSmsMonthly = 10,

            /// <summary>
            /// 800350 ﾅﾙｺｰﾑ ﾅﾋﾞｯｸ(月額)
            /// </summary>
            NavicMonthly = 11,

            /// <summary>
            /// 800522 ﾅﾙｺｰﾑ ﾎｰﾑﾍﾟｰｼﾞ保守ｻｰﾋﾞｽ(月契約)
            /// </summary>
            HomePage = 12,

            /// <summary>
            /// 800523 ﾅﾙｺｰﾑ ﾎｰﾑﾍﾟｰｼﾞ・ｽﾏﾎ保守ｻｰﾋﾞｽ(月契約)
            /// </summary>
            HomePageSmartPhone = 13,

            /// <summary>
            /// 800524 ﾅﾙｺｰﾑ ﾎｰﾑﾍﾟｰｼﾞ・SSL保守ｻｰﾋﾞｽ(月契約)
            /// </summary>
            HomePageSSL = 14,

            /// <summary>
            /// 800303 ﾅﾙｺｰﾑ ﾌﾟﾛｾｼｱ Version2 Web版
            /// </summary>
            Processia = 15,
        }

        /// <summary>
        /// ナルコーム製品文字列
        /// </summary>
        public static readonly EnumDictionary<NarcohmProductType, string> NarcohmProductTypeString = new EnumDictionary<NarcohmProductType, string>()
        {
            { NarcohmProductType.Nothing, "不明" },
            { NarcohmProductType.TatsujinPlusMonthly, "ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽVersion6 月額版" },
            { NarcohmProductType.TatsujinPlus1Pack, "ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 1年ﾊﾟｯｸ" },
            { NarcohmProductType.TatsujinPlus3Pack, "ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 3年ﾊﾟｯｸ" },
            { NarcohmProductType.TatsujinPlus6Pack, "ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 6年ﾊﾟｯｸ" },
            { NarcohmProductType.TatsujinPlus1PackUpdate, "ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 1年ﾊﾟｯｸ(更新)" },
            { NarcohmProductType.TatsujinPlus3PackUpdate, "ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 3年ﾊﾟｯｸ(更新)" },
            { NarcohmProductType.TatsujinPlus6PackUpdate, "ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 6年ﾊﾟｯｸ（更新)" },
            { NarcohmProductType.ApoDentMonthly, "ﾅﾙｺｰﾑ ｱﾎﾟﾃﾞﾝﾄ 月額費用" },
            { NarcohmProductType.ApoDentLineMonthly, "ﾅﾙｺｰﾑ ｱﾎﾟﾃﾞﾝﾄ LINEｻｰﾋﾞｽ(月額)" },
            { NarcohmProductType.ApoDentSmsMonthly, "ﾅﾙｺｰﾑ ｱﾎﾟﾃﾞﾝﾄ SMS(月額)" },
            { NarcohmProductType.NavicMonthly, "ﾅﾙｺｰﾑ ﾅﾋﾞｯｸ(月額)" },
            { NarcohmProductType.HomePage, "ﾅﾙｺｰﾑ ﾎｰﾑﾍﾟｰｼﾞ保守ｻｰﾋﾞｽ(月契約)" },
            { NarcohmProductType.HomePageSmartPhone, "ﾅﾙｺｰﾑ ﾎｰﾑﾍﾟｰｼﾞ・ｽﾏﾎ保守ｻｰﾋﾞｽ(月契約)" },
            { NarcohmProductType.HomePageSSL, "ﾅﾙｺｰﾑ ﾎｰﾑﾍﾟｰｼﾞ・SSL保守ｻｰﾋﾞｽ(月契約)" },
            { NarcohmProductType.Processia, "ﾅﾙｺｰﾑ ﾌﾟﾛｾｼｱ Version2 Web版" },
        };

        /// <summary>
        /// 800304 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽVersion6 月額版
        /// </summary>
        public const string TatsujinPlusMonthlyCode = "800304";

        /// <summary>
        /// 014800 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 1年ﾊﾟｯｸ
        /// </summary>
        public const string TatsujinPlus1PackCode = "014800";

        /// <summary>
        /// 014801 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 3年ﾊﾟｯｸ
        /// </summary>
        public const string TatsujinPlus3PackCode = "014801";

        /// <summary>
        /// 014802 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 6年ﾊﾟｯｸ
        /// </summary>
        public const string TatsujinPlus6PackCode = "014802";

        /// <summary>
        /// 017286 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 1年ﾊﾟｯｸ(更新)
        /// </summary>
        public const string TatsujinPlus1PackUpdateCode = "017286";

        /// <summary>
        /// 017287 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 3年ﾊﾟｯｸ(更新)
        /// </summary>
        public const string TatsujinPlus3PackUpdateCode = "017287";

        /// <summary>
        /// 017288 ﾅﾙｺｰﾑ 達人ﾌﾟﾗｽV.6 6年ﾊﾟｯｸ（更新)
        /// </summary>
        public const string TatsujinPlus6PackUpdateCode = "017288";

        /// <summary>
        /// 800301 ﾅﾙｺｰﾑ ｱﾎﾟﾃﾞﾝﾄ 月額費用
        /// </summary>
        public const string ApoDentMonthlyCode = "800301";

        /// <summary>
        /// 800310 ﾅﾙｺｰﾑ ｱﾎﾟﾃﾞﾝﾄ LINEｻｰﾋﾞｽ(月額)
        /// </summary>
        public const string ApoDentLineMonthlyCode = "800310";

        /// <summary>
        /// 017389 ﾅﾙｺｰﾑ ｱﾎﾟﾃﾞﾝﾄ SMS(月額)
        /// </summary>
        public const string ApoDentSmsMonthlyCode = "017389";

        /// <summary>
        /// 800350 ﾅﾙｺｰﾑ ﾅﾋﾞｯｸ(月額)
        /// </summary>
        public const string NavicMonthlyCode = "800350";

        /// <summary>
        /// 800522 ﾅﾙｺｰﾑ ﾎｰﾑﾍﾟｰｼﾞ保守ｻｰﾋﾞｽ(月契約)
        /// </summary>
        public const string HomePageCode = "800522";

        /// <summary>
        /// 800523 ﾅﾙｺｰﾑ ﾎｰﾑﾍﾟｰｼﾞ・ｽﾏﾎ保守ｻｰﾋﾞｽ(月契約)
        /// </summary>
        public const string HomePageSmartPhoneCode = "800523";

        /// <summary>
        /// 800524 ﾅﾙｺｰﾑ ﾎｰﾑﾍﾟｰｼﾞ・SSL保守ｻｰﾋﾞｽ(月契約)
        /// </summary>
        public const string HomePageSSLCode = "800524";

        /// <summary>
        /// 800303 ﾅﾙｺｰﾑ ﾌﾟﾛｾｼｱ Version2 Web版
        /// </summary>
        public const string ProcessiaCode = "800303";
    }
}