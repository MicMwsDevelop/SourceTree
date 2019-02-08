//
// NumericTextBox.cs
// 
// 数値入力専用テキストボックスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/01/31 勝呂)
//
using System.Windows.Forms;

namespace MwsLib.Component
{
	public partial class NumericTextBox : TextBox
	{
		/// <summary>
		/// Windowsメッセージ処理のための定数
		/// </summary>
		private const int WM_PASTE = 0x302;
		private const int WM_CHAR = 0x102;

		/// <summary>
		/// 全角入力禁止のために、デフォルトIMEモードは無効にする
		/// </summary>
		protected override ImeMode DefaultImeMode
		{
			get { return ImeMode.Disable; }
		}

		/// <summary>
		/// 数値以外禁止
		/// </summary>
		/// <param name="m"></param>
		[System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_PASTE:
					//数値以外の貼り付けを禁止する
					IDataObject iData = Clipboard.GetDataObject();
					if (iData != null && iData.GetDataPresent(DataFormats.Text))
					{
						string sClipStr = (string)iData.GetData(DataFormats.Text);
						//クリップボードの文字列が数字か調べる
						if (!System.Text.RegularExpressions.Regex.IsMatch(
							sClipStr,
							@"^[0-9]+$"))
							return;
					}
					break;
				case WM_CHAR:
					// 数値以外の入力を禁止する
					char sKeyChar = (char)(m.WParam.ToInt32());

					// 制御文字の時は処理しない
					if (!char.IsControl(sKeyChar))
					{
						// 数値(0-9)以外は、メッセージを破棄する
						if (!('0' <= sKeyChar & sKeyChar <= '9'))
						{
							return;
						}
					}
					break;

			}
			base.WndProc(ref m);
		}
	}
}
