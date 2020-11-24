//
// PcSupportEarningsController.cs
//
// PC安心サポート継続売上データ作成 データテーブル詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/11/02 勝呂)
// 
using MwsLib.BaseFactory.PcSupportEarnings;
using System.Data;

namespace MwsLib.DB.SqlServer.PcSupportEarnings
{
	/// <summary>
	/// ソフトウェア保守料売上データ作成 データテーブル詰め替えクラス
	/// </summary>
	public static class PcSupportEarningsController
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 売上データ必須情報の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ソフト保守料情報</returns>
		public static PcSupportEarningsOut ConvertPcSupportEarningsOut(DataTable table)
		{
			if (null != table)
			{
				if (0 < table.Rows.Count)
				{
					DataRow row = table.Rows[0];
					PcSupportEarningsOut ret = new PcSupportEarningsOut
					{
						f顧客No = DataBaseValue.ConvObjectToInt(row["f顧客No"]),
						f顧客名 = row["f顧客名"].ToString().Trim(),
						f得意先コード = row["f得意先コード"].ToString().Trim(),
						f請求先コード = row["f請求先コード"].ToString().Trim(),
						fPca部門コード = (short)DataBaseValue.ConvObjectToIntNull(row["fPca部門コード"]),
						fPca担当者コード = row["fPca担当者コード"].ToString().Trim(),
						fPca倉庫コード = (short)DataBaseValue.ConvObjectToIntNull(row["fPca倉庫コード"]),
						f商品コード = row["f商品コード"].ToString().Trim(),
						f商品名 = row["f商品名"].ToString().Trim(),
						f標準価格 = DataBaseValue.ConvObjectToInt(row["f標準価格"]),
						f原単価 = DataBaseValue.ConvObjectToInt(row["f原単価"]),
						f単位 = row["f単位"].ToString().Trim(),
					};
					return ret;
				}
			}
			return null;
		}
	}
}
