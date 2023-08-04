using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using ClosedXML.Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace MwsServiceStatus.Forms
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutput_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			List<T_MWS_APPLY> applyList = CharlieDatabaseAccess.Select_T_MWS_APPLY("[system_flg] = '0' AND LEFT([cp_id], 3) = 'MWS'", "[customer_id], [service_id]", Program.gSettings.ConnectCharlie.ConnectionString);
			List<V_SERVICE> serviceList = CharlieDatabaseAccess.Select_V_SERVICE("LEFT([cp_id], 3) = 'MWS'", "[customer_id], [service_id]", Program.gSettings.ConnectCharlie.ConnectionString);
			List<M_SERVICE> masterList = CharlieDatabaseAccess.Select_M_SERVICE("", "", Program.gSettings.ConnectCharlie.ConnectionString);

			try
			{
				using (XLWorkbook wb = new XLWorkbook())
				{
					IXLWorksheet ws = wb.AddWorksheet("サービス利用情報");

					ws.Cell(1, 1).Value = "No";
					ws.Cell(1, 2).Value = "顧客No";
					ws.Cell(1, 3).Value = "MWSID";
					ws.Cell(1, 4).Value = "サービスID";
					ws.Cell(1, 5).Value = "サービス名";
					ws.Cell(1, 6).Value = "MWSステータス";
					ws.Cell(1, 7).Value = "利用開始日";
					ws.Cell(1, 8).Value = "利用終了日";

					int row = 2;
					foreach (V_SERVICE service in serviceList)
					{
						T_MWS_APPLY apply = applyList.FindLast(p => p.cp_id == service.cp_id && p.service_id == service.service_id);
						V_SERVICE.ServiceStatus status = V_SERVICE.GetServiceStatus(service, apply, DateTime.Today);
						M_SERVICE master = masterList.Find(p =>p.SERVICE_ID == service.service_id);
						ws.Cell(row, 1).Value = row - 1;
						ws.Cell(row, 2).Value = service.customer_id;
						ws.Cell(row, 3).Value = service.cp_id;
						ws.Cell(row, 4).Value = service.service_id;
						ws.Cell(row, 5).Value = master.SERVICE_NAME;
						ws.Cell(row, 6).Value = status.ToString();
						ws.Cell(row, 7).Value = service.start_date.ToString();
						ws.Cell(row, 8).Value = service.end_date.ToString();
						row++;
					}
					wb.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), "サービス利用情報出力.xlsx"));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;

			MessageBox.Show("サービス利用情報出力を出力しました。");
		}
	}
}
