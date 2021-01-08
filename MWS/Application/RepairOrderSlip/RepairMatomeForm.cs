using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.DB.SqlServer.Charlie;
using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.BaseFactory.Charlie.View;

namespace RepairOrderSlip
{
	public partial class RepairMatomeForm : Form
	{
		public RepairMatomeForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			int id = numericTextBoxCustomerNo.ToInt();
			if (0 < id)
			{
				dataGridViewContractHeader.Rows.Clear();
				dataGridViewContractDetail.Rows.Clear();
				listViewApply.Items.Clear();

				string whereStr1 = string.Format("fCustomerID = {0} and fContractType = 'まとめ'", id);
				List<T_USE_CONTRACT_HEADER> header = CharlieDatabaseAccess.Select_T_USE_CONTRACT_HEADER(whereStr1, "fApplyDate DESC", Program.DATABASE_ACCESS_CT);
				if (1 == header.Count)
				{
					dataGridViewContractHeader.Rows.Add();
					dataGridViewContractHeader.Rows[0].Cells[0].Value = header[0].fContractID;
					dataGridViewContractHeader.Rows[0].Cells[1].Value = header[0].fContractType;
					dataGridViewContractHeader.Rows[0].Cells[2].Value = header[0].fMonths;
					dataGridViewContractHeader.Rows[0].Cells[3].Value = header[0].fGoodsID;
					dataGridViewContractHeader.Rows[0].Cells[4].Value = header[0].fApplyDate;
					dataGridViewContractHeader.Rows[0].Cells[5].Value = header[0].fContractStartDate;
					dataGridViewContractHeader.Rows[0].Cells[6].Value = header[0].fContractEndDate;
					dataGridViewContractHeader.Rows[0].Cells[7].Value = header[0].fBillingStartDate;
					dataGridViewContractHeader.Rows[0].Cells[8].Value = header[0].fBillingEndDate;
					dataGridViewContractHeader.Rows[0].Cells[9].Value = header[0].fCreateDate;
					dataGridViewContractHeader.Rows[0].Cells[10].Value = header[0].fCreatePerson;
					dataGridViewContractHeader.Rows[0].Cells[11].Value = header[0].fUpdateDate;
					dataGridViewContractHeader.Rows[0].Cells[12].Value = header[0].fUpdatePerson;

					string whereStr2 = string.Format("fContractID = {0}", header[0].fContractID);
					List<T_USE_CONTRACT_DETAIL> details = CharlieDatabaseAccess.Select_T_USE_CONTRACT_DETAIL(whereStr2, "fContractDetailID ASC", Program.DATABASE_ACCESS_CT);
					if (null != details)
					{
						for (int i = 0; i < details.Count; i++)
						{
							dataGridViewContractDetail.Rows.Add();
							dataGridViewContractDetail.Rows[i].Cells[0].Value = details[i].fContractID;
							dataGridViewContractDetail.Rows[i].Cells[1].Value = details[i].fSERVICE_ID;
							dataGridViewContractDetail.Rows[i].Cells[2].Value = details[i].fSERVICE_NAME;
						}
						string whereStr3 = string.Format("customer_id = {0} and convert(nvarchar, apply_date, 111) = '{1}'", id, header[0].fApplyDate.Value.ToShortDateString());
						List<V_COUPLER_APPLY> applys = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr3, "apply_id ASC", Program.DATABASE_ACCESS_CT);
						if (null != applys)
						{
							for (int i = 0; i < applys.Count; i++)
							{
								string[] fields = new string[6];
								fields[0] = applys[i].cp_id;
								fields[1] = applys[i].customer_id.ToString();
								fields[2] = applys[i].service_id.ToString();
								fields[3] = applys[i].apply_date.ToString();
								fields[4] = applys[i].apply_type;
								fields[5] = applys[i].system_flg;
								listViewApply.Items.Add(new ListViewItem(fields));
							}
						}
					}
				}
			}
		}
	}
}
