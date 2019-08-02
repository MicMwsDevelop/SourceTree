//
// Program.cs
//
// PC安心サポート契約情報作成コンソールアプリ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/07/04 勝呂)
// 
using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.DB.SqlServer.Charlie;
using MwsLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using MwsLib.Common;

namespace MakeUsePcSupportTable
{
	class Program
	{
		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		public static bool DATABACE_ACCEPT_CT = true;

		/// <summary>
		/// メイン処理
		/// </summary>
		/// <param name="args">プログラム引数</param>
		static void Main(string[] args)
		{
			string msg = "tMik保守契約からPC安心サポート契約情報を作成します。よろしいですか？";
			if (DATABACE_ACCEPT_CT)
			{
				msg += "（CT環境）";
			}
			Console.WriteLine(msg);
			Console.ReadKey();
			Console.WriteLine("実行中．．．");

			ICollection<tMik保守契約> list = JunpDatabaseAccess.Select_tMik保守契約("fhsS保守 = '1'", "fhsCliMicID ASC", DATABACE_ACCEPT_CT);
			if (0 < list.Count)
			{
				List<T_USE_PCCSUPPORT> pcList = new List<T_USE_PCCSUPPORT>();
				foreach (tMik保守契約 mnt in list)
				{
					T_USE_PCCSUPPORT pc = new T_USE_PCCSUPPORT();
					pc.Set_tMik保守契約(mnt);
					if (false == pc.fApplyDate.HasValue)
					{
						pc.fApplyDate = new Date(2019, 6, 1).ToDateTime();
					}
					//if (false == pc.fBillingStartDate.HasValue && true == pc.fContractStartDate.HasValue && pc.fContractStartDate.Value <= Date.Today)
					if (false == pc.fBillingStartDate.HasValue && true == pc.fContractStartDate.HasValue)
					{
						// 契約開始日が当日以前のデータには課金期間を格納
						pc.fBillingStartDate = pc.fContractStartDate;
						pc.fBillingEndDate = pc.fContractEndDate;
					}
					pcList.Add(pc);
				}
				try
				{
					// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の複数新規追加
					CharlieDatabaseAccess.InsertIntoList_T_USE_PCCSUPPORT(pcList, DATABACE_ACCEPT_CT);

					Console.WriteLine("正常に終了しました。");
				}
				catch (Exception ex)
				{
					Console.WriteLine(string.Format("エラー終了({0})", ex.Message));
				}
				Console.ReadKey();
			}
		}
	}
}
