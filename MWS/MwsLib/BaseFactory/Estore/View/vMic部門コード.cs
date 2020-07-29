using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.DB;

namespace MwsLib.BaseFactory.Estore.View
{
	public class vMic部門コード
	{
        public int 顧客No { get; set; }
        public string 得意先コード { get; set; }
        public string PCA主担当No { get; set; }
        public string PCA部門No { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public vMic部門コード()
        {
            顧客No = 0;
            得意先コード = string.Empty;
            PCA主担当No = string.Empty;
            PCA部門No = string.Empty;
        }

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMic部門コード> DataTableToList(DataTable table)
		{
			List<vMic部門コード> result = new List<vMic部門コード>();
			foreach (DataRow row in table.Rows)
			{
				vMic部門コード data = new vMic部門コード
				{
					顧客No = DataBaseValue.ConvObjectToInt(row["顧客Ｎｏ"]),
					得意先コード = row["得意先コード"].ToString().Trim(),
					PCA主担当No = row["PCA主担当No"].ToString().Trim(),
					PCA部門No = row["PCA部門No"].ToString().Trim(),
				};
				result.Add(data);
			}
			return result;
		}
	}
}
