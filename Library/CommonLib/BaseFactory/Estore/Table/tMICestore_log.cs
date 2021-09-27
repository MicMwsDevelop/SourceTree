using CommonLib.DB;
using CommonLib.DB.SqlServer.Estore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonLib.BaseFactory.Estore.View;

namespace CommonLib.BaseFactory.Estore.Table
{
	public class tMICestore_log
	{
		/// <summary>
		/// ID
		/// </summary>
        public int ID { get; set; }

		/// <summary>
		/// web受注No
		/// </summary>
		public int web受注No { get; set; }

		/// <summary>
		/// PCA受注No
		/// </summary>
		public string PCA受注No { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? 作成日時 { get; set; }

		/// <summary>
		/// 出荷日
		/// </summary>
		public DateTime? 出荷日 { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4)", EstoreDatabaseDefine.TableName[EstoreDatabaseDefine.TableType.tMICestore_log]);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMICestore_log()
        {
            ID = 0;
            web受注No = 0;
            PCA受注No = string.Empty;
            作成日時 = null;
            出荷日 = null;
        }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="order"></param>
		public tMICestore_log(vMicOrder_accept order)
		{
			ID = order.order_accept_id;
			web受注No = order.order_no;
			PCA受注No = string.Empty;
			作成日時 = null;
			出荷日 = null;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tMICestore_log> DataTableToList(DataTable table)
		{
			List<tMICestore_log> result = new List<tMICestore_log>();
			foreach (DataRow row in table.Rows)
			{
				tMICestore_log data = new tMICestore_log
				{
					ID = DataBaseValue.ConvObjectToInt(row["ID"]),
					web受注No = DataBaseValue.ConvObjectToInt(row["web受注No"]),
					PCA受注No = row["PCA受注No"].ToString().Trim(),
					作成日時 = DataBaseValue.ConvObjectToDateTimeNull(row["作成日時"]),
					出荷日 = DataBaseValue.ConvObjectToDateTimeNull(row["出荷日"]),
				};
				result.Add(data);
			}
			return result;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			List<SqlParameter> param = new List<SqlParameter>();
			param.Add(new SqlParameter("@1", web受注No.ToString()));
			param.Add(new SqlParameter("@2", PCA受注No));
			param.Add(new SqlParameter("@3", 作成日時.HasValue ? 作成日時.Value : System.Data.SqlTypes.SqlDateTime.Null));
			param.Add(new SqlParameter("@4", 出荷日.HasValue ? 出荷日.Value : System.Data.SqlTypes.SqlDateTime.Null));
			return param.ToArray();
		}
	}
}
