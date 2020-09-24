using MwsLib.DB;
using System.Collections.Generic;
using System.Data;
using System;

namespace MwsLib.BaseFactory.Estore.View
{
	public class vMic商品マスタ
	{
        public int ID { get; set; }
        public string 商品コード { get; set; }
        public string 商品名 { get; set; }
        public int 通常提供価格 { get; set; }
        public int web提供価格 { get; set; }
        public int Q通常提供価格 { get; set; }
        public int Qweb提供価格 { get; set; }
        public int カテゴリNo { get; set; }
        public string 商品カテゴリ { get; set; }
        public string 商品説明 { get; set; }
        public int 削除 { get; set; }
        public DateTime? 表示開始日時 { get; set; }
        public DateTime? 表示終了日時 { get; set; }
        public int 付加情報 { get; set; }
        public string 商品詳細のリンク設定 { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public vMic商品マスタ()
        {
            ID = 0;
            商品コード = string.Empty;
            商品名 = string.Empty;
            通常提供価格 = 0;
            web提供価格 = 0;
            Q通常提供価格 = 0;
            Qweb提供価格 = 0;
            カテゴリNo = 0;
            商品カテゴリ = string.Empty;
            商品説明 = string.Empty;
            削除 = 0;
            表示開始日時 = null;
            表示終了日時 = null;
            付加情報 = 0;
            商品詳細のリンク設定 = string.Empty;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<vMic商品マスタ> DataTableToList(DataTable table)
        {
            List<vMic商品マスタ> result = new List<vMic商品マスタ>();
            foreach (DataRow row in table.Rows)
            {
                vMic商品マスタ data = new vMic商品マスタ
                {
                    ID = DataBaseValue.ConvObjectToInt(row["ID"]),
                    商品コード = row["商品コード"].ToString().Trim(),
                    商品名 = row["商品名"].ToString().Trim(),
                    通常提供価格 = DataBaseValue.ConvObjectToInt(row["通常提供価格"]),
                    web提供価格 = DataBaseValue.ConvObjectToInt(row["web提供価格"]),
                    Q通常提供価格 = DataBaseValue.ConvObjectToInt(row["Q通常提供価格"]),
                    Qweb提供価格 = DataBaseValue.ConvObjectToInt(row["Qweb提供価格"]),
                    カテゴリNo = DataBaseValue.ConvObjectToInt(row["カテゴリNo"]),
                    商品カテゴリ = row["商品カテゴリ"].ToString().Trim(),
                    商品説明 = row["商品説明"].ToString().Trim(),
                    削除 = DataBaseValue.ConvObjectToInt(row["削除"]),
                    表示開始日時 = DataBaseValue.ConvObjectToDateTimeNull(row["表示開始日時"]),
                    表示終了日時 = DataBaseValue.ConvObjectToDateTimeNull(row["表示終了日時"]),
                    付加情報 = DataBaseValue.ConvObjectToInt(row["付加情報"]),
                    商品詳細のリンク設定 = row["商品詳細のリンク設定"].ToString().Trim(),
                };
                result.Add(data);
            }
            return result;
        }
    }
}
