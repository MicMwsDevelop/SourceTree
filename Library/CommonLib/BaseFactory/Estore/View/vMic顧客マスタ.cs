using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Estore.View
{
	public class vMic顧客マスタ
	{
        public int 顧客No { get; set; }
        public string 顧客名 { get; set; }
        public string システムコード { get; set; }
        public string システム名 { get; set; }
        public string お支払方法 { get; set; }
        public string 領収証コード { get; set; }
        public string 領収証用紙 { get; set; }
        public string カルテコード { get; set; }
        public string カルテ用紙 { get; set; }
        public string トナーコード { get; set; }
        public string トナー { get; set; }
        public string 県番号 { get; set; }
        public string 都道府県名 { get; set; }
        public string ライセンス発行 { get; set; }
        public string 得意先No { get; set; }
        public int ユーザー得意先区分3 { get; set; }
        public string 請求先コード { get; set; }
        public int 請求先得意先区分3 { get; set; }
        public int 請求先顧客区分 { get; set; }
        public string 改正時情報 { get; set; }
        public string ヘルスケア { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public vMic顧客マスタ()
        {
            顧客No = 0;
            顧客名 = string.Empty;
            システムコード = string.Empty;
            システム名 = string.Empty;
            お支払方法 = string.Empty;
            領収証コード = string.Empty;
            領収証用紙 = string.Empty;
            カルテコード = string.Empty;
            カルテ用紙 = string.Empty;
            トナーコード = string.Empty;
            トナー = string.Empty;
            県番号 = string.Empty;
            都道府県名 = string.Empty;
            ライセンス発行 = string.Empty;
            得意先No = string.Empty;
            ユーザー得意先区分3 = 0;
            請求先コード = string.Empty;
            請求先得意先区分3 = 0;
            請求先顧客区分 = 0;
            改正時情報 = string.Empty;
            ヘルスケア = string.Empty;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<vMic顧客マスタ> DataTableToList(DataTable table)
        {
            List<vMic顧客マスタ> result = new List<vMic顧客マスタ>();
            foreach (DataRow row in table.Rows)
            {
                vMic顧客マスタ data = new vMic顧客マスタ
                {
                    顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
                    顧客名 = row["顧客名"].ToString().Trim(),
                    システムコード = row["システムコード"].ToString().Trim(),
                    システム名 = row["システム名"].ToString().Trim(),
                    お支払方法 = row["お支払方法"].ToString().Trim(),
                    領収証コード = row["領収証コード"].ToString().Trim(),
                    領収証用紙 = row["領収証用紙"].ToString().Trim(),
                    カルテコード = row["カルテコード"].ToString().Trim(),
                    カルテ用紙 = row["カルテ用紙"].ToString().Trim(),
                    トナーコード = row["トナーコード"].ToString().Trim(),
                    トナー = row["トナー"].ToString().Trim(),
                    県番号 = row["県番号"].ToString().Trim(),
                    都道府県名 = row["都道府県名"].ToString().Trim(),
                    ライセンス発行 = row["ライセンス発行"].ToString().Trim(),
                    得意先No = row["得意先No"].ToString().Trim(),
                    ユーザー得意先区分3 = DataBaseValue.ConvObjectToInt(row["ユーザー得意先区分3"]),
                    請求先コード = row["請求先コード"].ToString().Trim(),
                    請求先得意先区分3 = DataBaseValue.ConvObjectToInt(row["請求先得意先区分3"]),
                    請求先顧客区分 = DataBaseValue.ConvObjectToInt(row["請求先顧客区分"]),
                    改正時情報 = row["改正時情報"].ToString().Trim(),
                    ヘルスケア = row["ヘルスケア"].ToString().Trim(),
                };
                result.Add(data);
            }
            return result;
        }
    }
}
