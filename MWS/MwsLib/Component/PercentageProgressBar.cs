//
// PercentageProgressBar.cs
// 
// パーセント表示プログレスバー
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace MwsLib.Component
{
	/// <summary>
	/// パーセント表示プログレスバー
	/// </summary>
	public partial class PercentageProgressBar : ProgressBar
    {
        private int WM_PAINT = 0x000F;

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                // 表示する文字列を決定する
                double percent = (double)(this.Value - this.Minimum) / (double)(this.Maximum - this.Minimum);
                string displayText = string.Format("{0}%", (int)(percent * 100.0));
                // 文字列を描画する
                TextFormatFlags tff = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
                Graphics g = this.CreateGraphics();
                TextRenderer.DrawText(g, displayText, this.Font, this.ClientRectangle, SystemColors.ControlText, tff);
                g.Dispose();
            }
        }
    }
}
