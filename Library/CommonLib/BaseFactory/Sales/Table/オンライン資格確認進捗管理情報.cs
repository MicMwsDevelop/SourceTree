//
// オンライン資格確認進捗管理情報.cs
// 
// オンライン資格確認進捗管理情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/08/29 勝呂)
// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
//
using CommonLib.DB;
using CommonLib.DB.SqlServer.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Sales.Table
{
	/// <summary>
	/// オンライン資格確認進捗管理情報
	/// </summary>
	public class オンライン資格確認進捗管理情報
	{
		public int 顧客No { get; set; }
		public string 拠点名 { get; set; }
		public string 顧客名 { get; set; }
		public string オン資担当 { get; set; }
		public string 導入意思 { get; set; }
		public string 工事種別 { get; set; }
		public string ステータス { get; set; }
		public DateTime? 現調完了月 { get; set; }
		public DateTime? 導入月 { get; set; }
		public string 都道府県 { get; set; }
		public string 部署 { get; set; }
		public string 価格帯 { get; set; }
		public DateTime? 更新日付 { get; set; }

		// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
		public DateTime? オン資運用開始日 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public オンライン資格確認進捗管理情報()
		{
			顧客No = 0;
			拠点名 = string.Empty;
			顧客名 = string.Empty;
			オン資担当 = string.Empty;
			導入意思 = string.Empty;
			工事種別 = string.Empty;
			ステータス = string.Empty;
			現調完了月 = null;
			導入月 = null;
			都道府県 = string.Empty;
			部署 = string.Empty;
			価格帯 = string.Empty;
			更新日付 = null;

			// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
			オン資運用開始日 = null;
		}

		/// <summary>
		/// 同値かどうか
		/// </summary>
		public bool Equals(オンライン資格確認進捗管理情報 dst)
		{
			if (顧客No != dst.顧客No) return false;
			if (拠点名 != dst.拠点名) return false;
			if (顧客名 != dst.顧客名) return false;
			if (オン資担当 != dst.オン資担当) return false;
			if (導入意思 != dst.導入意思) return false;
			if (工事種別 != dst.工事種別) return false;
			if (ステータス != dst.ステータス) return false;
			if (現調完了月 != dst.現調完了月) return false;
			if (導入月 != dst.導入月) return false;
			if (都道府県 != dst.都道府県) return false;
			if (部署 != dst.部署) return false;
			if (価格帯 != dst.価格帯) return false;
			//if (更新日付 != dst.更新日付) return false;

			// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
			if (オン資運用開始日 != dst.オン資運用開始日) return false;
			return true;
		}

		public override bool Equals(object o)
		{
			if (o is オンライン資格確認進捗管理情報)
			{
				return Equals(o as オンライン資格確認進捗管理情報);
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// ハッシュコード取得
		/// </summary>
		public override int GetHashCode()
		{
			int sh = 現調完了月.GetHashCode();
			int hash = (sh << 8) + (sh >> 24);
			return hash ^ 導入月.GetHashCode();
		}

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14)", SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.オンライン資格確認進捗管理情報]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
				return string.Format(@"UPDATE {0} SET 拠点名 = @1, 顧客名 = @2, 都道府県 = @3, オン資担当 = @4, 導入意思 = @5, 工事種別 = @6, ステータス = @7, 現調完了月 = @8, 導入月 = @9, 部署 = @10, 価格帯 = @11, 更新日付 = @12, オン資運用開始日 = @13, WHERE 顧客No = {1}"
										, SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.オンライン資格確認進捗管理情報], 顧客No);
			}
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<オンライン資格確認進捗管理情報> DataTableToList(DataTable table)
		{
			List<オンライン資格確認進捗管理情報> result = new List<オンライン資格確認進捗管理情報>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					オンライン資格確認進捗管理情報 data = new オンライン資格確認進捗管理情報();
					data.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.拠点名 = row["拠点名"].ToString().Trim();
					data.顧客名 = row["顧客名"].ToString().Trim();
					data.都道府県 = row["都道府県"].ToString().Trim();
					data.オン資担当 = row["オン資担当"].ToString().Trim();
					data.導入意思 = row["導入意思"].ToString().Trim();
					data.工事種別 = row["工事種別"].ToString().Trim();
					data.ステータス = row["ステータス"].ToString().Trim();
					data.現調完了月 = DataBaseValue.ConvObjectToDateTimeNull(row["現調完了月"]);
					data.導入月 = DataBaseValue.ConvObjectToDateTimeNull(row["導入月"]);
					data.部署 = row["部署"].ToString().Trim();
					data.価格帯 = row["価格帯"].ToString().Trim();
					data.更新日付 = DataBaseValue.ConvObjectToDateTimeNull(row["更新日付"]);

					// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
					data.オン資運用開始日 = DataBaseValue.ConvObjectToDateTimeNull(row["オン資運用開始日"]);

					result.Add(data);
				}
			}
			return result;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", 顧客No.ToString()),
				new SqlParameter("@2", 拠点名),
				new SqlParameter("@3", 顧客名),
				new SqlParameter("@4", 都道府県),
				new SqlParameter("@5", オン資担当),
				new SqlParameter("@6", 導入意思),
				new SqlParameter("@7", 工事種別),
				new SqlParameter("@8", ステータス),
				new SqlParameter("@9", 現調完了月.HasValue ? 現調完了月.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", 導入月.HasValue ? 導入月.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", 部署),
				new SqlParameter("@12", 価格帯),
				new SqlParameter("@13", DateTime.Now),

				// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
				new SqlParameter("@14", オン資運用開始日.HasValue ? オン資運用開始日.Value.ToString() : System.Data.SqlTypes.SqlString.Null)
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
				new SqlParameter("@1", 拠点名),
				new SqlParameter("@2", 顧客名),
				new SqlParameter("@3", 都道府県),
				new SqlParameter("@4", オン資担当),
				new SqlParameter("@5", 導入意思),
				new SqlParameter("@6", 工事種別),
				new SqlParameter("@7", ステータス),
				new SqlParameter("@8", 現調完了月.HasValue ? 現調完了月.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", 導入月.HasValue ? 導入月.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", 部署),
				new SqlParameter("@11", 価格帯),
				new SqlParameter("@12", DateTime.Now),

				// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
				new SqlParameter("@13", オン資運用開始日.HasValue ? オン資運用開始日.Value.ToString() : System.Data.SqlTypes.SqlString.Null)
			};
			return param;
		}
	}
}
