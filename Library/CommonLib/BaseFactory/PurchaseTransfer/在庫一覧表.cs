using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CommonLib.BaseFactory.PurchaseTransfer
{
	public class 在庫一覧表
	{
		public string 倉庫コード { get; set; }

		public string 倉庫名 { get; set; }

		public string データ区分 { get; set; }

		public string 商品コード { get; set; }

		public string 商品名 { get; set; }

		public int 繰越在庫 { get; set; }

		public int 入荷数 { get; set; }

		public int 出荷数 { get; set; }

		public int 現品数 { get; set; }

		public int 在庫数 { get; set; }

		public int 評価単価 { get; set; }

		public int 在庫金額 { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return @"INSERT INTO TEST_在庫一覧表 (倉庫ｺｰﾄﾞ, 倉庫名, ﾃﾞｰﾀ区分, 商品ｺｰﾄﾞ, 品名, 繰越在庫, 入荷数, 出荷数, 現品数, 在庫数, 評価単価, 在庫金額) VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12)";
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 在庫一覧表()
		{
			倉庫コード = string.Empty;
			倉庫名 = string.Empty;
			データ区分 = string.Empty;
			商品コード = string.Empty;
			商品名 = string.Empty;
			繰越在庫 = 0;
			入荷数 = 0;
			出荷数 = 0;
			現品数 = 0;
			在庫数 = 0;
			評価単価 = 0;
			在庫金額 = 0;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			List<SqlParameter> param = new List<SqlParameter>();
			param.Add(new SqlParameter("@1", 倉庫コード));
			param.Add(new SqlParameter("@2", 倉庫名));
			param.Add(new SqlParameter("@3", データ区分));
			param.Add(new SqlParameter("@4", 商品コード));
			param.Add(new SqlParameter("@5", 商品名));
			param.Add(new SqlParameter("@6", 繰越在庫.ToString()));
			param.Add(new SqlParameter("@7", 入荷数.ToString()));
			param.Add(new SqlParameter("@8", 出荷数.ToString()));
			param.Add(new SqlParameter("@9", 現品数.ToString()));
			param.Add(new SqlParameter("@10", 在庫数.ToString()));
			param.Add(new SqlParameter("@11", 評価単価.ToString()));
			param.Add(new SqlParameter("@12", 在庫金額.ToString()));
			return param.ToArray();
		}

		/// <summary>
		/// CSVデータの格納
		/// </summary>
		/// <param name="split"></param>
		/// <returns></returns>
		public bool SetCsvRecord(List<string> split)
		{
			if (12 == split.Count)
			{
				try
				{
					倉庫コード = split[0];
					倉庫名 = split[1];
					データ区分 = split[2];
					商品コード = split[3];
					商品名 = split[4];
					繰越在庫 = int.Parse(split[5]);
					入荷数 = int.Parse(split[6]);
					出荷数 = int.Parse(split[7]);
					現品数 = int.Parse(split[8]);
					在庫数 = int.Parse(split[9]);
					if ("" != split[10] && null != split[10])
					{
						評価単価 = (int)double.Parse(split[10]);
					}
					在庫金額 = int.Parse(split[11]);
				}
				catch (Exception ex)
				{
					throw new ApplicationException(ex.Message);
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		public static void SetDataColumn(DataTable dt)
		{
			DataColumn dc = new DataColumn("倉庫ｺｰﾄﾞ", Type.GetType("System.String"));
			dt.Columns.Add(dc);
			dc = new DataColumn("倉庫名", Type.GetType("System.String"));
			dt.Columns.Add(dc);
			dc = new DataColumn("ﾃﾞｰﾀ区分", Type.GetType("System.Int32"));
			dt.Columns.Add(dc);
			dc = new DataColumn("商品ｺｰﾄﾞ", Type.GetType("System.String"));
			dt.Columns.Add(dc);
			dc = new DataColumn("品名", Type.GetType("System.String"));
			dt.Columns.Add(dc);
			dc = new DataColumn("繰越在庫", Type.GetType("System.Int32"));
			dt.Columns.Add(dc);
			dc = new DataColumn("入荷数", Type.GetType("System.Int32"));
			dt.Columns.Add(dc);
			dc = new DataColumn("出荷数", Type.GetType("System.Int32"));
			dt.Columns.Add(dc);
			dc = new DataColumn("現品数", Type.GetType("System.Int32"));
			dt.Columns.Add(dc);
			dc = new DataColumn("在庫数", Type.GetType("System.Int32"));
			dt.Columns.Add(dc);
			dc = new DataColumn("評価単価", Type.GetType("System.Int32"));
			dt.Columns.Add(dc);
			dc = new DataColumn("在庫金額", Type.GetType("System.Int32"));
			dt.Columns.Add(dc);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public DataRow GetDataRow(DataTable dt)
		{
			DataRow row = dt.NewRow();
			row.BeginEdit();
			row["倉庫ｺｰﾄﾞ"] = 倉庫コード;
			row["倉庫名"] = 倉庫名;
			row["ﾃﾞｰﾀ区分"] = データ区分;
			row["商品ｺｰﾄﾞ"] = 商品コード;
			row["品名"] = 商品名;
			row["繰越在庫"] = 繰越在庫;
			row["入荷数"] = 入荷数;
			row["出荷数"] = 出荷数;
			row["現品数"] = 現品数;
			row["在庫数"] = 在庫数;
			row["評価単価"] = 評価単価;
			row["在庫金額"] = 在庫金額;
			row.EndEdit();
			return row;
		}
	}
}
