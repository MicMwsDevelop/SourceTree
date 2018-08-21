//
// パーセント表示プログレスバー
// 
// Copyright (C) MIC All Rights Reserved.
// 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace MwsLib.Components
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
