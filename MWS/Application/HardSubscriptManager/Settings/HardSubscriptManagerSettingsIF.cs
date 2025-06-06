﻿//
// HardSubscriptManagerSettingsIF.cs
// 
// 環境設定インターフェイス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
//
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace HardSubscriptManager.Settings
{
	/// <summary>
	/// 環境設定インターフェイス
	/// </summary>
	public static class HardSubscriptManagerSettingsIF
	{
		/// <summary>
		/// 環境設定ファイル名称
		/// </summary>
		public const string SETTINGS_FILENAME = "HardSubscriptManagerSettings.xml";

		/// <summary>
		/// 環境設定
		/// </summary>
		private static HardSubscriptManagerSettings Settings = null;

		/// <summary>
		/// 環境設定ファイル名
		/// </summary>
		private static string SettingsFileName = string.Empty;

		/// <summary>
		/// 環境設定ファイル名の設定
		/// </summary>
		private static void SetSettingsFileName()
		{
			if (string.IsNullOrEmpty(SettingsFileName))
			{
				SettingsFileName = Path.Combine(Directory.GetCurrentDirectory(), SETTINGS_FILENAME);
			}
		}

		/// <summary>
		/// 環境設定の読み込み(XMLファイルから取得)
		/// </summary>
		/// <returns>true:成功 false:失敗</returns>
		private static bool LoadSettings(bool reload = false)
		{
			bool result = true;

			if (reload || null == Settings)
			{
				// メモリ上の設定が保存されていない時、リロード要求時に取得
				if (true == File.Exists(SettingsFileName))
				{
					FileStream fileStream = null;
					try
					{
						fileStream = new FileStream(SettingsFileName, FileMode.Open);
						XmlSerializer serializer = new XmlSerializer(typeof(HardSubscriptManagerSettings));
						Settings = serializer.Deserialize(fileStream) as HardSubscriptManagerSettings;
					}
					catch (Exception)
					{
						result = false;
					}
					finally
					{
						if (fileStream != null)
						{
							fileStream.Close();
						}
					}
				}
				else
				{
					// 存在しない場合は初期値を設定
					Settings = new HardSubscriptManagerSettings();
				}
			}
			return result;
		}

		/// <summary>
		/// 環境設定の保存(XMLファイルに出力)
		/// </summary>
		/// <returns>true:成功 false:失敗</returns>
		private static bool SaveSettings()
		{
			bool result = true;

			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(SettingsFileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				StreamWriter stream = new StreamWriter(fileStream, Encoding.UTF8);   // Unicodeで書き込む
				XmlSerializer serializer = new XmlSerializer(typeof(HardSubscriptManagerSettings));
				serializer.Serialize(stream, Settings);
			}
			catch (Exception)
			{
				result = false;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		/// <summary>
		/// 環境設定の取得
		/// </summary>
		/// <param name="reload">環境設定を再読みするかどうか（デフォルト：false）</param>
		/// <returns>環境設定</returns>
		/// <exception cref="ApplicationException">環境設定の読み込みが出来なかった場合に発生</exception>
		public static HardSubscriptManagerSettings GetSettings(bool reload = false)
		{
			SetSettingsFileName();

			LoadSettings(reload);

			if (null == Settings)
			{
				throw new ApplicationException("環境設定の取得に失敗");
			}
			return Settings.Clone() as HardSubscriptManagerSettings;
		}

		/// <summary>
		/// 環境設定の設定
		/// </summary>
		/// <param name="settings">環境設定</param>
		public static void SetSettings(HardSubscriptManagerSettings settings)
		{
			SetSettingsFileName();

			Settings = settings;

			SaveSettings();
		}
	}
}
