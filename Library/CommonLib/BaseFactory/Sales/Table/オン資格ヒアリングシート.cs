//
// オン資格ヒアリングシート.cs
//
// オン資格ヒアリングシート
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2022/03/04 勝呂)
// Ver1.10 NTT西日本進捗管理表新フォーム(20220822版)MIC連絡担当者社員番号対応(2022/08/19 勝呂)
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Sales.Table
{
	/// <summary>
	/// オン資格ヒアリングシート
	/// </summary>
	public class オン資格ヒアリングシート
	{
		public int 顧客番号 { get; set; }
		public DateTime? 登録日時 { get; set; }
		public DateTime? 更新日時 { get; set; }
		public string 登録者 { get; set; }
		public string 更新者 { get; set; }
		public string オンライン資格確認の開始希望 { get; set; }
		public bool ポータルサイト申込み状況 { get; set; }
		public string カードリーダー申込み状況 { get; set; }
		public string カードリーダー申込時期 { get; set; }
		public bool オンライン資格確認利用申請状況 { get; set; }
		public string 電子証明書発行申請状況 { get; set; }
		public string 資格確認端末の設置場所 { get; set; }
		public string 資格確認端末の運用形態 { get; set; }
		public string paletteとの連携 { get; set; }
		public string MIC連絡担当者 { get; set; }
		public string オンライン資格確認端末PC種 { get; set; }
		public string 新規回線導入工事 { get; set; }
		public string 工事予定希望日 { get; set; }
		public string 医院連絡可能日時間帯 { get; set; }
		public string 駐車場 { get; set; }
		public string 工事備考 { get; set; }
		public string オンライン請求利用状況 { get; set; }
		public string オンライン請求利用ブラウザ { get; set; }
		public string オンライン請求利用環境ーオペレーションシステム { get; set; }
		public string オンライン請求の利用回線 { get; set; }
		public string IPsecIKEサービス提供事業者 { get; set; }
		public string 回線サービス名 { get; set; }
		public string ISDNサービス名 { get; set; }
		public string ビジネスフォン導入社名と連絡先 { get; set; }
		public string 光回線_NTTのお客様ID_CAFCOD { get; set; }
		public string プロバイダ名 { get; set; }
		public string インターネットプロバイダIDとPASS { get; set; }
		public string paletteのシステム構成 { get; set; }
		public string IPアドレス構成_palette { get; set; }
		public string paletteのサブネットマスク { get; set; }
		public string サーバー機のメーカー名 { get; set; }
		public string サーバー機の製品名 { get; set; }
		public string サーバー機の設置場所 { get; set; }
		public string 受付機_連携設定のメーカー名 { get; set; }
		public string 受付機_連携設定の製品名 { get; set; }
		public string 受付機_連携設定の設置場所 { get; set; }
		public string 複数受付機設置場所 { get; set; }
		public string paletteルーター { get; set; }
		public string デジタルレントゲンのシステム名 { get; set; }
		public string デジタルレントゲンのメーカー名 { get; set; }
		public string レントゲンとの連携 { get; set; }
		public string IPアドレス構成_デジタルレントゲン { get; set; }
		public string サブネットマスク_デジタルレントゲン { get; set; }
		public string 備考_デジタルレントゲン { get; set; }
		public string メーカー名_他社システム1 { get; set; }
		public string 備考_他社システム1 { get; set; }
		public string メーカー名_他社システム2 { get; set; }
		public string 備考_他社システム2 { get; set; }
		public string メーカー名_他社システム3 { get; set; }
		public string 備考_他社システム3 { get; set; }
		public string その他_特記事項 { get; set; }
		public string ヒアリングシート完成日 { get; set; }
		public string 送信履歴 { get; set; }
		public string 現調送信履歴 { get; set; }

		// Ver1.10 NTT西日本進捗管理表新フォーム(20220822版)MIC連絡担当者社員番号対応(2022/08/19 勝呂)
		public string MIC連絡担当者社員番号 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public オン資格ヒアリングシート()
		{
			this.Clear();
		}

		/// <summary>
		/// クリア
		/// </summary>
		protected void Clear()
		{
			顧客番号 = 0;
			登録日時 = null;
			更新日時 = null;
			登録者 = string.Empty;
			更新者 = string.Empty;
			オンライン資格確認の開始希望 = string.Empty;
			ポータルサイト申込み状況 = false;
			カードリーダー申込み状況 = string.Empty;
			カードリーダー申込時期 = string.Empty;
			オンライン資格確認利用申請状況 = false;
			電子証明書発行申請状況 = string.Empty;
			資格確認端末の設置場所 = string.Empty;
			資格確認端末の運用形態 = string.Empty;
			paletteとの連携 = string.Empty;
			MIC連絡担当者 = string.Empty;
			オンライン資格確認端末PC種 = string.Empty;
			新規回線導入工事 = string.Empty;
			工事予定希望日 = string.Empty;
			医院連絡可能日時間帯 = string.Empty;
			駐車場 = string.Empty;
			工事備考 = string.Empty;
			オンライン請求利用状況 = string.Empty;
			オンライン請求利用ブラウザ = string.Empty;
			オンライン請求利用環境ーオペレーションシステム = string.Empty;
			オンライン請求の利用回線 = string.Empty;
			IPsecIKEサービス提供事業者 = string.Empty;
			回線サービス名 = string.Empty;
			ISDNサービス名 = string.Empty;
			ビジネスフォン導入社名と連絡先 = string.Empty;
			光回線_NTTのお客様ID_CAFCOD = string.Empty;
			プロバイダ名 = string.Empty;
			インターネットプロバイダIDとPASS = string.Empty;
			paletteのシステム構成 = string.Empty;
			IPアドレス構成_palette = string.Empty;
			paletteのサブネットマスク = string.Empty;
			サーバー機のメーカー名 = string.Empty;
			サーバー機の製品名 = string.Empty;
			サーバー機の設置場所 = string.Empty;
			受付機_連携設定のメーカー名 = string.Empty;
			受付機_連携設定の製品名 = string.Empty;
			受付機_連携設定の設置場所 = string.Empty;
			複数受付機設置場所 = string.Empty;
			paletteルーター = string.Empty;
			デジタルレントゲンのシステム名 = string.Empty;
			デジタルレントゲンのメーカー名 = string.Empty;
			レントゲンとの連携 = string.Empty;
			IPアドレス構成_デジタルレントゲン = string.Empty;
			サブネットマスク_デジタルレントゲン = string.Empty;
			備考_デジタルレントゲン = string.Empty;
			メーカー名_他社システム1 = string.Empty;
			備考_他社システム1 = string.Empty;
			メーカー名_他社システム2 = string.Empty;
			備考_他社システム2 = string.Empty;
			メーカー名_他社システム3 = string.Empty;
			備考_他社システム3 = string.Empty;
			その他_特記事項 = string.Empty;
			ヒアリングシート完成日 = string.Empty;
			送信履歴 = string.Empty;
			現調送信履歴 = string.Empty;

			// Ver1.10 NTT西日本進捗管理表新フォーム(20220822版)MIC連絡担当者社員番号対応(2022/08/19 勝呂)
			MIC連絡担当者社員番号 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<オン資格ヒアリングシート> DataTableToList(DataTable table)
		{
			List<オン資格ヒアリングシート> result = new List<オン資格ヒアリングシート>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					オン資格ヒアリングシート data = new オン資格ヒアリングシート();
					data.顧客番号 = DataBaseValue.ConvObjectToInt(row["顧客番号"]);
					data.登録日時 = DataBaseValue.ConvObjectToDateTimeNull(row["登録日時"]);
					data.更新日時 = DataBaseValue.ConvObjectToDateTimeNull(row["更新日時"]);
					data.登録者 = row["登録者"].ToString().Trim();
					data.更新者 = row["更新者"].ToString().Trim();
					data.オンライン資格確認の開始希望 = row["オンライン資格確認の開始希望"].ToString().Trim();
					data.ポータルサイト申込み状況 = DataBaseValue.ConvObjectToBool(row["ポータルサイト申込み状況"]);
					data.カードリーダー申込み状況 = row["カードリーダー申込み状況"].ToString().Trim();
					data.カードリーダー申込時期 = row["カードリーダー申込時期"].ToString().Trim();
					data.オンライン資格確認利用申請状況 = DataBaseValue.ConvObjectToBool(row["オンライン資格確認利用申請状況"]);
					data.電子証明書発行申請状況 = row["電子証明書発行申請状況"].ToString().Trim();
					data.資格確認端末の設置場所 = row["資格確認端末の設置場所"].ToString().Trim();
					data.資格確認端末の運用形態 = row["資格確認端末の運用形態"].ToString().Trim();
					data.paletteとの連携 = row["paletteとの連携"].ToString().Trim();
					data.MIC連絡担当者 = row["MIC連絡担当者"].ToString().Trim();
					data.オンライン資格確認端末PC種 = row["オンライン資格確認端末PC種"].ToString().Trim();
					data.新規回線導入工事 = row["新規回線導入工事"].ToString().Trim();
					data.工事予定希望日 = row["工事予定希望日"].ToString().Trim();
					data.医院連絡可能日時間帯 = row["医院連絡可能日時間帯"].ToString().Trim();
					data.駐車場 = row["駐車場"].ToString().Trim();
					data.工事備考 = row["工事備考"].ToString().Trim();
					data.オンライン請求利用状況 = row["オンライン請求利用状況"].ToString().Trim();
					data.オンライン請求利用ブラウザ = row["オンライン請求利用ブラウザ"].ToString().Trim();
					data.オンライン請求利用環境ーオペレーションシステム = row["オンライン請求利用環境ーオペレーションシステム"].ToString().Trim();
					data.オンライン請求の利用回線 = row["オンライン請求の利用回線"].ToString().Trim();
					data.IPsecIKEサービス提供事業者 = row["IPsec + IKE サービス提供事業者"].ToString().Trim();
					data.回線サービス名 = row["回線サービス名"].ToString().Trim();
					data.ISDNサービス名 = row["ISDNサービス名"].ToString().Trim();
					data.ビジネスフォン導入社名と連絡先 = row["ビジネスフォン導入社名と連絡先"].ToString().Trim();
					data.光回線_NTTのお客様ID_CAFCOD = row["光回線（NTT）のお客様ID（CAF/COD）"].ToString().Trim();
					data.プロバイダ名 = row["プロバイダ名"].ToString().Trim();
					data.インターネットプロバイダIDとPASS = row["インターネットプロバイダIDとPASS"].ToString().Trim();
					data.paletteのシステム構成 = row["paletteのシステム構成"].ToString().Trim();
					data.IPアドレス構成_palette = row["IPアドレス構成(palette)"].ToString().Trim();
					data.paletteのサブネットマスク = row["paletteのサブネットマスク"].ToString().Trim();
					data.サーバー機のメーカー名 = row["サーバー機のメーカー名"].ToString().Trim();
					data.サーバー機の製品名 = row["サーバー機の製品名"].ToString().Trim();
					data.サーバー機の設置場所 = row["サーバー機の設置場所"].ToString().Trim();
					data.受付機_連携設定のメーカー名 = row["受付機（連携設定）のメーカー名"].ToString().Trim();
					data.受付機_連携設定の製品名 = row["受付機（連携設定）の製品名"].ToString().Trim();
					data.受付機_連携設定の設置場所 = row["受付機（連携設定）の設置場所"].ToString().Trim();
					data.複数受付機設置場所 = row["複数受付機設置場所"].ToString().Trim();
					data.paletteルーター = row["paletteルーター"].ToString().Trim();
					data.デジタルレントゲンのシステム名 = row["デジタルレントゲンのシステム名"].ToString().Trim();
					data.デジタルレントゲンのメーカー名 = row["デジタルレントゲンのメーカー名"].ToString().Trim();
					data.レントゲンとの連携 = row["レントゲンとの連携"].ToString().Trim();
					data.IPアドレス構成_デジタルレントゲン = row["IPアドレス構成(デジタルレントゲン)"].ToString().Trim();
					data.サブネットマスク_デジタルレントゲン = row["サブネットマスク（デジタルレントゲン）"].ToString().Trim();
					data.備考_デジタルレントゲン = row["備考(デジタルレントゲン)"].ToString().Trim();
					data.メーカー名_他社システム1 = row["メーカー名(他社システム)1"].ToString().Trim();
					data.備考_他社システム1 = row["備考_他社システム1"].ToString().Trim();
					data.メーカー名_他社システム2 = row["メーカー名(他社システム)2"].ToString().Trim();
					data.備考_他社システム2 = row["備考_他社システム2"].ToString().Trim();
					data.メーカー名_他社システム3 = row["メーカー名(他社システム)3"].ToString().Trim();
					data.備考_他社システム3 = row["備考_他社システム3"].ToString().Trim();
					data.その他_特記事項 = row["その他_特記事項"].ToString().Trim();
					data.ヒアリングシート完成日 = row["ヒアリングシート完成日"].ToString().Trim();
					data.送信履歴 = row["送信履歴"].ToString().Trim();
					data.現調送信履歴 = row["現調送信履歴"].ToString().Trim();

					// Ver1.10 NTT西日本進捗管理表新フォーム(20220822版)MIC連絡担当者社員番号対応(2022/08/19 勝呂)
					data.MIC連絡担当者社員番号 = row["MIC連絡担当者社員番号"].ToString().Trim();

					result.Add(data);
				}
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string[] GetData()
		{
			List<string> ret = new List<string>();
			ret.Add(顧客番号.ToString());
			ret.Add((null == 登録日時) ? "" : 登録日時.ToString());
			ret.Add((null == 更新日時) ? "" : 更新日時.ToString());
			ret.Add(登録者);
			ret.Add(更新者);
			ret.Add(オンライン資格確認の開始希望);
			ret.Add(ポータルサイト申込み状況.ToString());
			ret.Add(カードリーダー申込み状況);
			ret.Add(カードリーダー申込時期);
			ret.Add(オンライン資格確認利用申請状況.ToString());
			ret.Add(電子証明書発行申請状況);
			ret.Add(資格確認端末の設置場所);
			ret.Add(資格確認端末の運用形態);
			ret.Add(paletteとの連携);
			ret.Add(MIC連絡担当者);
			ret.Add(オンライン資格確認端末PC種);
			ret.Add(新規回線導入工事);
			ret.Add(工事予定希望日);
			ret.Add(医院連絡可能日時間帯);
			ret.Add(駐車場);
			ret.Add(工事備考);
			ret.Add(オンライン請求利用状況);
			ret.Add(オンライン請求利用ブラウザ);
			ret.Add(オンライン請求利用環境ーオペレーションシステム);
			ret.Add(オンライン請求の利用回線);
			ret.Add(IPsecIKEサービス提供事業者);
			ret.Add(回線サービス名);
			ret.Add(ISDNサービス名);
			ret.Add(ビジネスフォン導入社名と連絡先);
			ret.Add(光回線_NTTのお客様ID_CAFCOD);
			ret.Add(プロバイダ名);
			ret.Add(インターネットプロバイダIDとPASS);
			ret.Add(paletteのシステム構成);
			ret.Add(IPアドレス構成_palette);
			ret.Add(paletteのサブネットマスク);
			ret.Add(サーバー機のメーカー名);
			ret.Add(サーバー機の製品名);
			ret.Add(サーバー機の設置場所);
			ret.Add(受付機_連携設定のメーカー名);
			ret.Add(受付機_連携設定の製品名);
			ret.Add(受付機_連携設定の設置場所);
			ret.Add(複数受付機設置場所);
			ret.Add(paletteルーター);
			ret.Add(デジタルレントゲンのシステム名);
			ret.Add(デジタルレントゲンのメーカー名);
			ret.Add(レントゲンとの連携);
			ret.Add(IPアドレス構成_デジタルレントゲン);
			ret.Add(サブネットマスク_デジタルレントゲン);
			ret.Add(備考_デジタルレントゲン);
			ret.Add(メーカー名_他社システム1);
			ret.Add(備考_他社システム1);
			ret.Add(メーカー名_他社システム2);
			ret.Add(備考_他社システム2);
			ret.Add(メーカー名_他社システム3);
			ret.Add(備考_他社システム3);
			ret.Add(その他_特記事項);
			ret.Add(ヒアリングシート完成日);
			ret.Add(送信履歴);
			ret.Add(現調送信履歴);

			// Ver1.10 NTT西日本進捗管理表新フォーム(20220822版)MIC連絡担当者社員番号対応(2022/08/19 勝呂)
			ret.Add(MIC連絡担当者社員番号);

			return ret.ToArray();
		}
	}
}
