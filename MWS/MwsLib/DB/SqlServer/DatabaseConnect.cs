//
// DatabaseConnect.cs
// 
// SQL SERVER データベース接続情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/12/06 勝呂)
//
namespace MwsLib.DB.SqlServer
{
	/// <summary>
	/// データベース接続情報
	/// </summary>
	public class DatabaseConnect
	{
		// ＤＢインスタンス ///////////////////////////////////////

		/// <summary>
		/// SQLSVのインスタンス名（本番環境）
		/// </summary>
		private const string DB_INSTANCE_NAME_SQLSV = "SQLSV";

		/// <summary>
		/// SQLSV2のインスタンス名（CT環境）
		/// </summary>
		private const string DB_INSTANCE_NAME_SQLSV2 = "SQLSV2";

		/// <summary>
		/// CP-DB01のインスタンス名（本番環境）
		/// </summary>
		private const string DB_INSTANCE_NAME_CPDB01 = "CP-DB01";

		/// <summary>
		/// CPTest-DBのインスタンス名（CT環境）
		/// </summary>
		private const string DB_INSTANCE_NAME_CPTEST = "CPTest-DB";


		// ＤＢ名 ///////////////////////////////////////

		/// <summary>
		/// charlieのDB名
		/// </summary>
		private const string DB_NAME_CHARLIE = "charlieDB";

		/// <summary>
		/// junpのDB名
		/// </summary>
		private const string DB_NAME_JUNP = "JunpDB";

		/// <summary>
		/// estoreのDB名
		/// </summary>
		private const string DB_NAME_ESTORE = "estoreDB";

		/// <summary>
		/// COUPLERのDB名
		/// </summary>
		private const string DB_NAME_COUPLER = "COUPLER";

		/// <summary>
		/// PCAのDB名
		/// </summary>
		private const string DB_NAME_PCA = "P10V01C001KON0001";


		// ユーザー名 ///////////////////////////////////////

		/// <summary>
		/// SQLSV SA
		/// </summary>
		private const string USER_SQLSV_SA = "sa";

		/// <summary>
		/// SQLSV Web
		/// </summary>
		private const string USER_SQLSV_WEB = "web";

		/// <summary>
		/// CP-DB01
		/// </summary>
		private const string USER_COUPLER_CPDB01 = "cpuser";

		/// <summary>
		/// CPTest-DB
		/// </summary>
		private const string USER_COUPLER_CPTEST = "sa";


		// パスワード ///////////////////////////////////////

		/// <summary>
		/// SQLSV SA
		/// </summary>
		private const string PWD_SQLSV_SA = "07883510";

		/// <summary>
		/// SQLSV Web
		/// </summary>
		private const string PWD_SQLSV_WEB = "02035612";


		/// <summary>
		/// CP-DB01
		/// </summary>
		private const string PWD_COUPLER_CPDB01 = "Sqladmin#39";

		/// <summary>
		/// CPTest-DB
		/// </summary>
		private const string PWD_COUPLER_CPTEST = PWD_COUPLER_CPDB01;


		// 接続文字列 ///////////////////////////////////////

		/// <summary>
		/// DB接続文字列
		/// </summary>
		private const string DB_CONNECT_STRING = @"Server={0};Database={1};User ID={2};Password={3};Min Pool Size=1";


		// メンバ ///////////////////////////////////////

		/// <summary>
		/// インスタンス名
		/// </summary>
		private string InstanceName { get; set; }

		/// <summary>
		/// データベース名
		/// </summary>
		private string DatabaseName { get; set; }

		/// <summary>
		/// ユーザーID
		/// </summary>
		private string UserID { get; set; }

		/// <summary>
		/// パスワード
		/// </summary>
		private string Password { get; set; }

		/// <summary>
		/// 接続文字列の取得
		/// </summary>
		public string ConnectionString
		{
			get
			{
				return string.Format(DB_CONNECT_STRING, InstanceName, DatabaseName, UserID, Password);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DatabaseConnect()
		{
			InstanceName = string.Empty;
			DatabaseName = string.Empty;
			UserID = string.Empty;
			Password = string.Empty;
		}

		/// <summary>
		/// 接続文字列の取得
		/// インスタンス名：SQLSV or SQLSV2
		/// DB名：CharlieDB
		/// ユーザー名：sa
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>接続文字列</returns>
		public static string CharlieSAConnectionString(bool ct)
		{
			DatabaseConnect con = new DatabaseConnect();
			con.InstanceName = (ct) ? DB_INSTANCE_NAME_SQLSV2 : DB_INSTANCE_NAME_SQLSV;
			con.DatabaseName = DB_NAME_CHARLIE;
			con.UserID = USER_SQLSV_SA;
			con.Password = PWD_SQLSV_SA;
			return con.ConnectionString;
		}

		/// <summary>
		/// 接続文字列の取得
		/// インスタンス名：SQLSV or SQLSV2
		/// DB名：CharlieDB
		/// ユーザー名：web
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>接続文字列</returns>
		public static string CharlieWebConnectionString(bool ct)
		{
			DatabaseConnect con = new DatabaseConnect();
			con.InstanceName = (ct) ? DB_INSTANCE_NAME_SQLSV2 : DB_INSTANCE_NAME_SQLSV;
			con.DatabaseName = DB_NAME_CHARLIE;
			con.UserID = USER_SQLSV_WEB;
			con.Password = PWD_SQLSV_WEB;
			return con.ConnectionString;
		}

		/// <summary>
		/// 接続文字列の取得
		/// インスタンス名：SQLSV or SQLSV2
		/// DB名：JunpDB
		/// ユーザー名：sa
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>接続文字列</returns>
		public static string JunpSAConnectionString(bool ct)
		{
			DatabaseConnect con = new DatabaseConnect();
			con.InstanceName = (ct) ? DB_INSTANCE_NAME_SQLSV2 : DB_INSTANCE_NAME_SQLSV;
			con.DatabaseName = DB_NAME_JUNP;
			con.UserID = USER_SQLSV_SA;
			con.Password = PWD_SQLSV_SA;
			return con.ConnectionString;
		}

		/// <summary>
		/// 接続文字列の取得
		/// インスタンス名：SQLSV or SQLSV2
		/// DB名：JunpDB
		/// ユーザー名：web
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>接続文字列</returns>
		public static string JunpWebConnectionString(bool ct)
		{
			DatabaseConnect con = new DatabaseConnect();
			con.InstanceName = (ct) ? DB_INSTANCE_NAME_SQLSV2 : DB_INSTANCE_NAME_SQLSV;
			con.DatabaseName = DB_NAME_JUNP;
			con.UserID = USER_SQLSV_WEB;
			con.Password = PWD_SQLSV_WEB;
			return con.ConnectionString;
		}

		/// <summary>
		/// 接続文字列の取得
		/// インスタンス名：SQLSV or SQLSV2
		/// DB名：estoreDB
		/// ユーザー名：web
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>接続文字列</returns>
		public static string EstoreWebConnectionString(bool ct)
		{
			DatabaseConnect con = new DatabaseConnect();
			con.InstanceName = (ct) ? DB_INSTANCE_NAME_SQLSV2 : DB_INSTANCE_NAME_SQLSV;
			con.DatabaseName = DB_NAME_ESTORE;
			con.UserID = USER_SQLSV_WEB;
			con.Password = PWD_SQLSV_WEB;
			return con.ConnectionString;
		}

		/// <summary>
		/// 接続文字列の取得
		/// インスタンス名：SQLSV or SQLSV2
		/// DB名：P10V01C001KON0001
		/// ユーザー名：web
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>接続文字列</returns>
		public static string PcaWebConnectionString(bool ct)
		{
			DatabaseConnect con = new DatabaseConnect();
			con.InstanceName = (ct) ? DB_INSTANCE_NAME_SQLSV2 : DB_INSTANCE_NAME_SQLSV;
			con.DatabaseName = DB_NAME_PCA;
			con.UserID = USER_SQLSV_WEB;
			con.Password = PWD_SQLSV_WEB;
			return con.ConnectionString;
		}

		/// <summary>
		/// 接続文字列の取得
		/// インスタンス名：CPTest-DB
		/// DB名：COUPLER
		/// ユーザー名：sa
		/// </summary>
		/// <returns>接続文字列</returns>
		public static string CouplerConnectionString()
		{
			DatabaseConnect con = new DatabaseConnect();
			con.InstanceName = DB_INSTANCE_NAME_CPTEST;
			con.DatabaseName = DB_NAME_COUPLER;
			con.UserID = USER_COUPLER_CPTEST;
			con.Password = PWD_COUPLER_CPTEST;
			return con.ConnectionString;
		}
	}
}
