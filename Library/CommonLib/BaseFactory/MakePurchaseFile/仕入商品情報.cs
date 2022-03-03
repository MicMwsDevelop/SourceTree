//
// 仕入商品情報.cs
// 
// 仕入商品情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/07 勝呂)
//
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.MakePurchaseFile
{
	public class 仕入商品情報 : ICloneable, IEquatable<仕入商品情報>
	{
		public string Palette商品コード { get; set; }
		public string 商品コード { get; set; }
		public string 仕入商品コード { get; set; }
		public int 仕入価格 { get; set; }
		public string 仕入先 { get; set; }

		/// <summary>
		/// TMP_Curline本体アプリ商品 INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString_Curline本体アプリ商品
		{
			get
			{
				return @"INSERT INTO TMP_Curline本体アプリ商品 (Palette商品コード, 商品コード, 仕入商品コード, 仕入価格, 仕入先) VALUES (@1, @2, @3, @4, @5)";
			}
		}

		/// <summary>
		/// TMP_Curline本体アプリ商品 CREATE TABLE SQL文字列の取得
		/// </summary>
		public static string CreateTableString_Curline本体アプリ商品
		{
			get
			{
				return "CREATE TABLE [dbo].[TMP_Curline本体アプリ商品]([Palette商品コード][nvarchar](50) NULL, [商品コード] [nvarchar](50) NULL, [仕入商品コード] [nvarchar](50) NULL, [仕入価格] [int] NULL, [仕入先] [nvarchar](50) NULL) ON [PRIMARY]";
			}
		}

		/// <summary>
		/// TMP_Curline本体アプリ商品 DROP TABLE SQL文字列の取得
		/// </summary>
		public static string DropTableString_Curline本体アプリ商品
		{
			get
			{
				return "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TMP_Curline本体アプリ商品]') AND type in (N'U')) DROP TABLE [dbo].[TMP_Curline本体アプリ商品]";
			}
		}

		/// <summary>
		/// TMP_Microsoft365商品 INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString_Microsoft365商品
		{
			get
			{
				return @"INSERT INTO TMP_Microsoft365商品 (Palette商品コード, 商品コード, 仕入商品コード, 仕入価格, 仕入先) VALUES (@1, @2, @3, @4, @5)";
			}
		}

		/// <summary>
		/// TMP_Microsoft365商品 CREATE TABLE SQL文字列の取得
		/// </summary>
		public static string CreateTableString_Microsoft365商品
		{
			get
			{
				return "CREATE TABLE [dbo].[TMP_Microsoft365商品]([Palette商品コード][nvarchar](50) NULL, [商品コード] [nvarchar](50) NULL, [仕入商品コード] [nvarchar](50) NULL, [仕入価格] [int] NULL, [仕入先] [nvarchar](50) NULL) ON [PRIMARY]";
			}
		}

		/// <summary>
		/// TMP_Microsoft365商品 DROP TABLE SQL文字列の取得
		/// </summary>
		public static string DropTableString_Microsoft365商品
		{
			get
			{
				return "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TMP_Microsoft365商品]') AND type in (N'U')) DROP TABLE [dbo].[TMP_Microsoft365商品]";
			}
		}

		/// <summary>
		/// TMP_りすとん月額商品 INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString_りすとん月額商品
		{
			get
			{
				return @"INSERT INTO TMP_りすとん月額商品 (Palette商品コード, 商品コード, 仕入商品コード, 仕入価格, 仕入先) VALUES (@1, @2, @3, @4, @5)";
			}
		}

		/// <summary>
		/// TMP_りすとん月額商品 CREATE TABLE SQL文字列の取得
		/// </summary>
		public static string CreateTableString_りすとん月額商品
		{
			get
			{
				return "CREATE TABLE [dbo].[TMP_りすとん月額商品]([Palette商品コード][nvarchar](50) NULL, [商品コード] [nvarchar](50) NULL, [仕入商品コード] [nvarchar](50) NULL, [仕入価格] [int] NULL, [仕入先] [nvarchar](50) NULL) ON [PRIMARY]";
			}
		}

		/// <summary>
		/// TMP_りすとん月額商品 DROP TABLE SQL文字列の取得
		/// </summary>
		public static string DropTableString_りすとん月額商品
		{
			get
			{
				return "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TMP_りすとん月額商品]') AND type in (N'U')) DROP TABLE [dbo].[TMP_りすとん月額商品]";
			}
		}

		/// <summary>
		/// TMP_問心伝月額商品 INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString_問心伝月額商品
		{
			get
			{
				return @"INSERT INTO TMP_問心伝月額商品 (Palette商品コード, 商品コード, 仕入商品コード, 仕入価格, 仕入先) VALUES (@1, @2, @3, @4, @5)";
			}
		}

		/// <summary>
		/// TMP_問心伝月額商品 CREATE TABLE SQL文字列の取得
		/// </summary>
		public static string CreateTableString_問心伝月額商品
		{
			get
			{
				return "CREATE TABLE [dbo].[TMP_問心伝月額商品]([Palette商品コード][nvarchar](50) NULL, [商品コード] [nvarchar](50) NULL, [仕入商品コード] [nvarchar](50) NULL, [仕入価格] [int] NULL, [仕入先] [nvarchar](50) NULL) ON [PRIMARY]";
			}
		}

		/// <summary>
		/// TMP_問心伝月額商品 DROP TABLE SQL文字列の取得
		/// </summary>
		public static string DropTableString_問心伝月額商品
		{
			get
			{
				return "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TMP_問心伝月額商品]') AND type in (N'U')) DROP TABLE [dbo].[TMP_問心伝月額商品]";
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 仕入商品情報()
		{
			Palette商品コード = string.Empty;
			商品コード = string.Empty;
			仕入商品コード = string.Empty;
			仕入価格 = 0;
			仕入先 = string.Empty;
		}

		/// <summary>
		/// メンバーのクローンを作成する
		/// （ICloneableの実装）
		/// </summary>
		/// <returns>クローンオブジェクト</returns>
		public Object Clone()
		{
			return MemberwiseClone();
		}

		/// <summary>
		/// このインスタンスと、指定した環境設定クラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(仕入商品情報 other)
		{
			if (other != null)
			{
				if (Palette商品コード == other.Palette商品コード
					&& 商品コード == other.商品コード
					&& 仕入商品コード == other.仕入商品コード
					&& 仕入価格 == other.仕入価格
					&& 仕入先 == other.仕入先)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is 仕入商品情報)
			{
				return this.Equals((仕入商品情報)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// ハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			List<SqlParameter> param = new List<SqlParameter>();
			param.Add(new SqlParameter("@1", Palette商品コード));
			param.Add(new SqlParameter("@2", 商品コード));
			param.Add(new SqlParameter("@3", 仕入商品コード));
			param.Add(new SqlParameter("@4", 仕入価格.ToString()));
			param.Add(new SqlParameter("@5", 仕入先));
			return param.ToArray();
		}
	}
}
