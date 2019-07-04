using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;
using System.Data;
using MwsLib.DB;
using MwsLib.DB.SqlServer.Charlie;
using System.Data.SqlClient;
using MwsLib.BaseFactory.NarcohmOrderCheck;

namespace MwsLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// ナルコーム製品申込詳細情報
	/// [CharlieDB].[dbo].[T_NARCOHM_APPLICATE_DETAIL]
	/// </summary>
	public class T_NARCOHM_APPLICATE_DETAIL
	{
		/// <summary>
		/// 申込詳細番号(オートナンバー)
		/// </summary>
		public int ApplicateDetailID { get; set; }

		/// <summary>
		/// 申込番号
		/// </summary>
		public int ApplicateID { get; set; }

		/// <summary>
		/// 受注番号
		/// </summary>
		public int? OrderNo { get; set; }

		/// <summary>
		/// 受注日
		/// </summary>
		public Date? OrderDate { get; set; }

		/// <summary>
		/// 商品コード
		/// </summary>
		public string GoodsCode { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 金額
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public int Count { get; set; }

		/// <summary>
		/// 合計
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public Date? UseStartDate { get; set; }

		/// <summary>
		/// 利用終了日
		/// </summary>
		public Date? UseEndDate { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_NARCOHM_APPLICATE_DETAIL]);
			}
		}

		/// <summary>
		/// DELETE SQL文字列の取得
		/// </summary>
		/// <param name="order">受注情報</param>
		/// <returns>SQL文字列</returns>
		public static string DeleteSqlString(int applicateID)
		{
			return string.Format(@"DELETE FROM {0} WHERE ApplicateID = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_NARCOHM_APPLICATE_DETAIL], applicateID);
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_NARCOHM_APPLICATE_DETAIL()
		{
			ApplicateDetailID = 0;
			ApplicateID = 0;
			OrderNo = null;
			OrderDate = null;
			GoodsCode = string.Empty;
			GoodsName = string.Empty;
			Price = 0;
			Count = 0;
			Total = 0;
			UseStartDate = null;
			UseEndDate = null;
		}

		/// <summary>
		/// 受注情報の格納
		/// </summary>
		/// <param name="order">受注情報</param>
		public void SetNarcohmOrderInfo(NarcohmOrderInfo order)
		{
			OrderNo = order.OrderNo;
			OrderDate = order.OrderDate;
			GoodsCode = order.GoodsCode;
			GoodsName = order.GoodsName;
			Price = order.Price;
			Count = order.Count;
			Total = order.Total;
		}

		/// <summary>
		/// リストビュー表示情報の取得 
		/// </summary>
		/// <returns>表示情報</returns>
		public string[] GetListViewData()
		{
			string[] array = new string[9];
			array[0] = OrderNo.ToString();
			array[1] = (OrderDate.HasValue) ? OrderDate.ToString() : "";
			array[2] = GoodsCode;
			array[3] = GoodsName;
			array[4] = "\\" + StringUtil.CommaEdit(Price);
			array[5] = Count.ToString();
			array[6] = "\\" + StringUtil.CommaEdit(Total);
			array[7] = (UseStartDate.HasValue) ? UseStartDate.Value.ToYearMonth().ToString() : "";
			array[8] = (UseEndDate.HasValue) ? UseEndDate.Value.ToYearMonth().ToString() : "";
			return array;
		}

		/// <summary>
		/// ナルコーム製品申込詳細情報の詰め替え
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_DETAIL]
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ナルコーム製品申込詳細情報リスト</returns>
		public static List<T_NARCOHM_APPLICATE_DETAIL> DataTableToList(DataTable table)
		{
			List<T_NARCOHM_APPLICATE_DETAIL> result = null;
			if (null != table)
			{
				result = new List<T_NARCOHM_APPLICATE_DETAIL>();
				foreach (DataRow row in table.Rows)
				{
					T_NARCOHM_APPLICATE_DETAIL data = new T_NARCOHM_APPLICATE_DETAIL
					{
						ApplicateDetailID = DataBaseValue.ConvObjectToInt(row["ApplicateDetailID"]),
						ApplicateID = DataBaseValue.ConvObjectToInt(row["ApplicateID"]),
						OrderNo = DataBaseValue.ConvObjectToIntNull(row["OrderNo"]),
						OrderDate = DataBaseValue.ConvObjectToDateNullByDate(row["OrderDate"]),
						GoodsCode = row["GoodsCode"].ToString().Trim(),
						GoodsName = row["GoodsName"].ToString().Trim(),
						Count = DataBaseValue.ConvObjectToInt(row["Count"]),
						Price = DataBaseValue.ConvObjectToInt(row["Price"]),
						Total = DataBaseValue.ConvObjectToInt(row["Total"]),
						UseStartDate = DataBaseValue.ConvObjectToDateNullByDate(row["UseStartDate"]),
						UseEndDate = DataBaseValue.ConvObjectToDateNullByDate(row["UseEndDate"])
					};
					result.Add(data);
				}
			}
			return result;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters(int seqNo)
		{
			SqlParameter[] param = {
				new SqlParameter("@1", ApplicateID),	// [ApplicateID]
				new SqlParameter("@2", seqNo),			// [SeqNo]
				new SqlParameter("@3", (OrderNo.HasValue) ? OrderNo.Value : System.Data.SqlTypes.SqlInt32.Null),	// [OrderNo] 
				new SqlParameter("@4", (OrderDate.HasValue) ? OrderDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	    // [OrderDate] 
				new SqlParameter("@5", GoodsCode),		// [GoodsCode] 
				new SqlParameter("@6", GoodsName),		// [GoodsName] 
				new SqlParameter("@7", Price),			// [Price] 
				new SqlParameter("@8", Count),			// [Count] 
				new SqlParameter("@9", Total),			// [Total] 
				new SqlParameter("@10", (UseStartDate.HasValue) ? UseStartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// [UseStartDate] 
				new SqlParameter("@11", (UseEndDate.HasValue) ? UseEndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null)		// [UseEndtDate] 
			};
			return param;
		}
	}
}
