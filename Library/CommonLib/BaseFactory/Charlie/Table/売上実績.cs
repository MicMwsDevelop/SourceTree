//
// 売上実績.cs
//
// 売上実績クラス
// [charlieDB].[dbo].[売上実績]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;
using CommonLib.DB.SqlServer.Charlie;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Charlie.Table
{
	public class 売上実績
	{
        public int 実績日{get;set;}
        public string 営業部コード { get; set; }
        public short 予算VP { get; set; }
        public short 予算ES { get; set; }
        public short 予算まとめ { get; set; }
        public int 予算売上 { get; set; }
        public int 予算営業損益 { get; set; }
        public short 予測VP { get; set; }
        public short 予測ES { get; set; }
        public short 予測まとめ { get; set; }
        public int 予測売上 { get; set; }
        public int 予測営業損益 { get; set; }
        public short 実績VP { get; set; }
        public short 実績ES { get; set; }
        public short 実績まとめ { get; set; }
        public int 実績売上 { get; set; }
        public int 実績営業損益 { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17)"
								, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.売上実績]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET 予算ES = @1, 予算まとめ = @2, 予算売上 = @3, 予算営業損益 = @4, 予測ES = @5, 予測まとめ = @6"
									+ ", 予測売上 = @7, 予測営業損益 = @8, 実績ES = @9, 実績まとめ = @10, 実績売上 = @11, 実績営業損益 = @12"
									+ " WHERE 実績日 = {1} AND 営業部コード = '{2}'"
								, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.売上実績]
								, 実績日
								, 営業部コード);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 売上実績()
        {
            実績日 = 0;
            営業部コード = string.Empty;
            予算VP = 0;
            予算ES = 0;
            予算まとめ = 0;
            予算売上 = 0;
            予算営業損益 = 0;
            予測VP = 0;
            予測ES = 0;
            予測まとめ = 0;
            予測売上 = 0;
            予測営業損益 = 0;
            実績VP = 0;
            実績ES = 0;
            実績まとめ = 0;
            実績売上 = 0;
            実績営業損益 = 0;
        }

        /// <summary>
        /// DataTable → リスト変換
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<売上実績> DataTableToList(DataTable table)
        {
            if (null != table && 0 < table.Rows.Count)
            {
                List<売上実績> result = new List<売上実績>();
                foreach (DataRow row in table.Rows)
                {
                    売上実績 data = new 売上実績
                    {
                        実績日 = DataBaseValue.ConvObjectToInt(row["実績日"]),
                        営業部コード = row["営業部コード"].ToString().Trim(),
                        予算VP = DataBaseValue.ConvObjectToShort(row["予算VP"]),
                        予算ES = DataBaseValue.ConvObjectToShort(row["予算ES"]),
                        予算まとめ = DataBaseValue.ConvObjectToShort(row["予算まとめ"]),
                        予算売上 = DataBaseValue.ConvObjectToInt(row["予算売上"]),
                        予算営業損益 = DataBaseValue.ConvObjectToInt(row["予算営業損益"]),
                        予測VP = DataBaseValue.ConvObjectToShort(row["予測VP"]),
                        予測ES = DataBaseValue.ConvObjectToShort(row["予測ES"]),
                        予測まとめ = DataBaseValue.ConvObjectToShort(row["予測まとめ"]),
                        予測売上 = DataBaseValue.ConvObjectToInt(row["予測売上"]),
                        予測営業損益 = DataBaseValue.ConvObjectToInt(row["予測営業損益"]),
                        実績VP = DataBaseValue.ConvObjectToShort(row["実績VP"]),
                        実績ES = DataBaseValue.ConvObjectToShort(row["実績ES"]),
                        実績まとめ = DataBaseValue.ConvObjectToShort(row["実績まとめ"]),
                        実績売上 = DataBaseValue.ConvObjectToInt(row["実績売上"]),
                        実績営業損益 = DataBaseValue.ConvObjectToInt(row["実績営業損益"]),
                    };
                    result.Add(data);
                }
                return result;
            }
            return null;
        }

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", 実績日),
				new SqlParameter("@2", 営業部コード ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", 予算VP),
				new SqlParameter("@4", 予算ES),
				new SqlParameter("@5", 予算まとめ),
				new SqlParameter("@6", 予算売上),
				new SqlParameter("@7", 予算営業損益),
				new SqlParameter("@8", 予測VP),
				new SqlParameter("@9", 予測ES),
				new SqlParameter("@10", 予測まとめ),
				new SqlParameter("@11", 予測売上),
				new SqlParameter("@12", 予測営業損益),
				new SqlParameter("@13", 実績VP),
				new SqlParameter("@14", 実績ES),
				new SqlParameter("@15", 実績まとめ),
				new SqlParameter("@16", 実績売上),
				new SqlParameter("@17", 実績営業損益),
			};
			return param;
		}

		/// <summary>
		/// UPDATE SETパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetUpdateSetParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", 予算ES),
				new SqlParameter("@2", 予算まとめ),
				new SqlParameter("@3", 予算売上),
				new SqlParameter("@4", 予算営業損益),
				new SqlParameter("@5", 予測ES),
				new SqlParameter("@6", 予測まとめ),
				new SqlParameter("@7", 予測売上),
				new SqlParameter("@8", 予測営業損益),
				new SqlParameter("@9", 実績ES),
				new SqlParameter("@10", 実績まとめ),
				new SqlParameter("@11", 実績売上),
				new SqlParameter("@12", 実績営業損益),
			};
			return param;
		}
	}
}
