using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;

namespace MwsLib.BaseFactory.SoftwareMainteSaleData
{
    /// <summary>
    /// ソフトウェア保守料受注伝票情報
    /// </summary>
	public class OrderSlipSoftwareMainte
	{
        /// <summary>
        /// 
        /// </summary>
        public int f受注番号 { get; set; }
        public Date? f受注日 { get; set; }
        public Date? f受注承認日 { get; set; }
        public Date? f売上承認日 { get; set; }
        public int? f販売種別 { get; set; }
        public int? f販売先コード { get; set; }
        public string f販売先 { get; set; }
        public int? fユーザーコード { get; set; }
        public string fユーザー { get; set; }
        public YearMonth? fSV利用開始年月 { get; set; }
        public YearMonth? fSV利用終了年月 { get; set; }
        public string fBshCode3 { get; set; }
        public string f担当支店名 { get; set; }
        public string f件名 { get; set; }
        public string f商品コード { get; set; }
        public string f商品名 { get; set; }
        public int? f数量 { get; set; }
        public int? f標準価格 { get; set; }
        public int? f金額 { get; set; }
        public int? f提供価格 { get; set; }
        public string f税区分 { get; set; }
        public int? f税率 { get; set; }
        public string f税込区分 { get; set; }
        public int? f売上原価 { get; set; }
        public short? fPca部門コード { get; set; }
        public string fPca担当者コード { get; set; }
        public short? fPca倉庫コード { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public OrderSlipSoftwareMainte()
        {
            f受注番号 = 0;
            f受注日 = null;
            f受注承認日 = null;
            f売上承認日 = null;
            f販売種別 = null;
            f販売先コード = null;
            f販売先 = string.Empty;
            fユーザーコード = null;
            fユーザー = string.Empty;
            fSV利用開始年月 = null;
            fSV利用終了年月 = null;
            fBshCode3 = string.Empty;
            f担当支店名 = string.Empty;
            f件名 = string.Empty;
            f商品コード = string.Empty;
            f商品名 = string.Empty;
            f数量 = null;
            f標準価格 = null;
            f金額 = null;
            f提供価格 = null;
            f税区分 = string.Empty;
            f税率 = null;
            f税込区分 = string.Empty;
            f売上原価 = null;
            fPca部門コード = null;
            fPca担当者コード = string.Empty;
            fPca倉庫コード = null;
        }

        /// <summary>
        /// ソフトウェア保守料売上データ作成
        /// </summary>
        /// <param name="no">伝票No</param>
        /// <param name="taxRate">伝票No</param>
        /// <param name="saleDate">売上日 </param>
        /// <returns>CSV文字列</returns>
        public string ToSale(int no, int taxRate, Date saleDate)
        {
            string[] ret = new string[53];
            ret[0] = "0";                                   // 1:伝区
            ret[1] = saleDate.ToIntYMD().ToString();        // 2:売上年月日
            ret[2] = saleDate.ToIntYMD().ToString();        // 3:請求年月日
            ret[3] = no.ToString();                         // 4:伝票番号
            ret[4] = f販売先コード.Value.ToString();            // 5:得意先コード
            ret[5] = f販売先; // 6:得意先名
            ret[6] = "";    // 7:直送先コード
            ret[7] = "";    // 8:先方担当者名
            ret[8] = fPca部門コード.Value.ToString();    // 9:部門コード
            ret[9] = fPca担当者コード;  // 10:担当者コード
            ret[10] = "0"; // 11:摘要コード
            ret[11] = fユーザー;  // 12:摘要名
            ret[12] = "";   //  13:分類コード
            ret[13] = "";  // 14:伝票区分コード
            ret[14] = f商品コード;              // 15:商品コード
            ret[15] = "0";          //  16:マスタ区分  (0:一般商品)
            ret[16] = f商品名;                                              // 17:品名
            ret[17] = "0";  // 18:区
            ret[18] = fPca倉庫コード.Value.ToString();  // 19:倉庫コード
            ret[19] = "0";  // 20:入数
            ret[20] = "0"; // 21:箱数
            ret[21] = "1";                               // 22:数量
            ret[22] = "";              // 23:単位
            ret[23] = "0";       // 24:単価
            ret[24] = f提供価格.Value.ToString();  // 25:売上金額
            ret[25] = f売上原価.Value.ToString();  // 26:原単価
            ret[26] = f売上原価.Value.ToString();  // 27:原価額
            ret[27] = "0";  // 28:粗利益
            ret[28] = "0";  // 29:外税額
            ret[29] = "0";   // 30:内税額
            ret[30] = f税区分;   // 31:税区分
            ret[31] = f税込区分;   // 32:税込区分
            ret[32] = "";  // 33:備考    ""
            ret[33] = f標準価格.Value.ToString();  // 34:標準価格
            ret[34] = "0";  // 35:同時入荷区分
            ret[35] = "0";  // 36:売単価コード
            ret[36] = "0";  // 37:売価金額コード
            ret[37] = "";  // 38:規格・型番
            ret[38] = "";  // 39:色
            ret[39] = "";              // 40:サイズ
            ret[40] = "0";  // 41:計算式コード
            ret[41] = "0";   // 42:商品項目1
            ret[42] = "0";   // 43:商品項目2
            ret[43] = "0";  // 44:商品項目3
            ret[44] = "0";   // 45:売上項目1
            ret[45] = "0";   // 46:売上項目2
            ret[46] = "0";   // 47:売上項目3
            ret[47] = taxRate.ToString();   // 48:税率
            ret[48] = "0";   // 49:伝票消費税（外税）
            ret[49] = "";   // 50:プロジェクトコード
            ret[50] = "";   // 51:伝票No２
            ret[51] = "0";   // 52:データ区分
            ret[52] = "";   // 53:商品名２
            return string.Join(",", ret);
        }
    }
}
