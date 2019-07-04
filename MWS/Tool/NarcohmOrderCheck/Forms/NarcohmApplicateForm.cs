using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MwsLib.BaseFactory;
using MwsLib.DB.SqlServer.NarcohmOrderCheck;
using MwsLib.BaseFactory.NarcohmOrderCheck;
using MwsLib.Common;
using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.DB.SqlServer.Charlie;

namespace NarcohmOrderCheck.Forms
{
    public partial class NarcohmApplicateForm : Form
    {
		private bool ModifyFlag;

		private DataTable GoodsDataTable;
		private DataTable DetailGoodsDataTable;

		public T_NARCOHM_APPLICATE_HEADER ApplicateInfo;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public NarcohmApplicateForm()
        {
            InitializeComponent();

			GoodsDataTable = new DataTable();
			DetailGoodsDataTable = new DataTable();
			ApplicateInfo = null;
			ModifyFlag = false;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NarcohmApplicateForm_Load(object sender, EventArgs e)
        {
			// 商品名
			GoodsDataTable.Columns.Add("Display");
			GoodsDataTable.Columns.Add("Value");

			DataRow row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.TatsujinPlusMonthly];
			row["Value"] = NarcohmDefine.TatsujinPlusMonthlyCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.TatsujinPlus1Pack];
			row["Value"] = NarcohmDefine.TatsujinPlus1PackCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.TatsujinPlus3Pack];
			row["Value"] = NarcohmDefine.TatsujinPlus3PackCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.TatsujinPlus6Pack];
			row["Value"] = NarcohmDefine.TatsujinPlus6PackCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.TatsujinPlus1PackUpdate];
			row["Value"] = NarcohmDefine.TatsujinPlus1PackUpdateCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.TatsujinPlus3PackUpdate];
			row["Value"] = NarcohmDefine.TatsujinPlus3PackUpdateCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.TatsujinPlus6PackUpdate];
			row["Value"] = NarcohmDefine.TatsujinPlus6PackUpdateCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.ApoDentMonthly];
			row["Value"] = NarcohmDefine.ApoDentMonthlyCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.ApoDentLineMonthly];
			row["Value"] = NarcohmDefine.ApoDentLineMonthlyCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.ApoDentSmsMonthly];
			row["Value"] = NarcohmDefine.ApoDentSmsMonthlyCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.NavicMonthly];
			row["Value"] = NarcohmDefine.NavicMonthlyCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.HomePage];
			row["Value"] = NarcohmDefine.HomePageCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.HomePageSmartPhone];
			row["Value"] = NarcohmDefine.HomePageSmartPhoneCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.HomePageSSL];
			row["Value"] = NarcohmDefine.HomePageSSLCode;
			GoodsDataTable.Rows.Add(row);

			row = GoodsDataTable.NewRow();
			row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.Processia];
			row["Value"] = NarcohmDefine.ProcessiaCode;
			GoodsDataTable.Rows.Add(row);

			comboBoxNarcohm.DisplayMember = "Display";
			comboBoxNarcohm.ValueMember = "Value";

			// 販売種別
			comboBoxSaleType.Items.Add("月額");
			comboBoxSaleType.Items.Add("その他");


			DetailGoodsDataTable.Columns.Add("Display");
			DetailGoodsDataTable.Columns.Add("Value");

			if (null == ApplicateInfo)
			{
				// 新規入力
				ApplicateInfo = new T_NARCOHM_APPLICATE_HEADER();
			}
			else
			{
				// 変更
				ModifyFlag = true;

				textBoxTel.Text = ApplicateInfo.Telephone;
				textBoxCustomerNo.Text = ApplicateInfo.CustomerNo.ToString();
				textBoxToluisakiNo.Text = ApplicateInfo.TokuisakiNo;
				textBoxClinicName.Text = ApplicateInfo.ClinicName;
				textBoxBranch.Text = ApplicateInfo.BranchName;
				textBoxSalesman.Text = ApplicateInfo.SalesmanName;
				textBoxSubject.Text = ApplicateInfo.Subject;
				comboBoxNarcohm.Enabled = true;
				comboBoxNarcohm.DataSource = GoodsDataTable;
				comboBoxNarcohm.SelectedValue = ApplicateInfo.SectionCode;
				if (ApplicateInfo.ServiceStartDate.HasValue)
				{
					dateTimePickerServiceStartDate.Enabled = true;
					dateTimePickerServiceStartDate.Value = ApplicateInfo.ServiceStartDate.Value.ToDateTime();
				}
				string goodCode = string.Empty;
				foreach (T_NARCOHM_APPLICATE_DETAIL detail in ApplicateInfo.DetailList)
				{
					ListViewItem lvItem = new ListViewItem(detail.GetListViewData());
					lvItem.Tag = detail;
					listViewApplicate.Items.Add(lvItem);
					if (0 == goodCode.Length)
					{
						goodCode = detail.GoodsCode;
					}
				}
				if (0 < goodCode.Length)
				{
					comboBoxNarcohm.SelectedValue = goodCode;
				}
				if (ApplicateInfo.KakinStartYM.HasValue)
				{
					dateTimePickerKakinStartYM.Enabled = true;
					dateTimePickerKakinStartYM.Value = ApplicateInfo.KakinStartYM.Value.ToDate(1).ToDateTime();
				}
				if (MwsDefine.ApplyType.Monthly == ApplicateInfo.SaleType)
				{
					comboBoxSaleType.SelectedIndex = 0;
				}
				else
				{
					comboBoxSaleType.SelectedIndex = 1;
				}
			}
		}

		/// <summary>
		/// 電話番号による医院検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
        {
			if (0 < textBoxTel.Text.Length)
			{
				comboBoxNarcohm.SelectedIndex = -1;

				CustomerInfo customer = NarcohmOrderCheckAccess.GetCustomerInfo(textBoxTel.Text, false);
				if (null != customer)
				{
					ApplicateInfo.SetCustomerData(customer);
					textBoxCustomerNo.Text = ApplicateInfo.CustomerNo.ToString();
					textBoxToluisakiNo.Text = ApplicateInfo.TokuisakiNo;
					textBoxClinicName.Text = ApplicateInfo.ClinicName;
					textBoxBranch.Text = ApplicateInfo.BranchName;
					textBoxSalesman.Text = ApplicateInfo.SalesmanName;

					comboBoxNarcohm.Enabled = true;
					comboBoxNarcohm.DataSource = GoodsDataTable;
					comboBoxNarcohm.SelectedIndex = -1;
				}
				else
				{
					MessageBox.Show("該当する顧客は存在しません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

					textBoxCustomerNo.Text = string.Empty;
					textBoxToluisakiNo.Text = string.Empty;
					textBoxClinicName.Text = string.Empty;
					textBoxBranch.Text = string.Empty;
					textBoxSalesman.Text = string.Empty;

					comboBoxNarcohm.Enabled = false;
					comboBoxNarcohm.DataSource = null;
					comboBoxNarcohm.SelectedIndex = -1;
				}
			}
		}

		/// <summary>
		/// ナルコーム製品選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxNarcohm_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (-1 != comboBoxNarcohm.SelectedIndex)
			{
				dateTimePickerServiceStartDate.Enabled = true;
				comboBoxSaleType.Enabled = true;
				comboBoxSaleType.SelectedIndex = 0;
				textBoxSubject.Enabled = true;
				buttonLoadOrder.Enabled = true;
				buttonAdd.Enabled = true;
				buttonModify.Enabled = true;
				buttonRemove.Enabled = true;

				DetailGoodsDataTable.Rows.Clear();

				if (NarcohmDefine.HomePageCode == (string)comboBoxNarcohm.SelectedValue
					|| NarcohmDefine.HomePageSmartPhoneCode == (string)comboBoxNarcohm.SelectedValue
					|| NarcohmDefine.HomePageSSLCode == (string)comboBoxNarcohm.SelectedValue)
				{
					DataRow row = DetailGoodsDataTable.NewRow();
					row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.HomePage];
					row["Value"] = NarcohmDefine.HomePageCode;
					DetailGoodsDataTable.Rows.Add(row);

					row = DetailGoodsDataTable.NewRow();
					row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.HomePageSmartPhone];
					row["Value"] = NarcohmDefine.HomePageSmartPhoneCode;
					DetailGoodsDataTable.Rows.Add(row);

					row = DetailGoodsDataTable.NewRow();
					row["Display"] = NarcohmDefine.NarcohmProductTypeString[NarcohmDefine.NarcohmProductType.HomePageSSL];
					row["Value"] = NarcohmDefine.HomePageSSLCode;
					DetailGoodsDataTable.Rows.Add(row);
				}
				else
				{
					DataRow row = DetailGoodsDataTable.NewRow();
					row["Display"] = comboBoxNarcohm.Text;
					row["Value"] = comboBoxNarcohm.SelectedValue;
					DetailGoodsDataTable.Rows.Add(row);
				}
			}
			else
			{
				dateTimePickerServiceStartDate.Enabled = false;
				comboBoxSaleType.Enabled = false;
				comboBoxSaleType.SelectedIndex = -1;
				textBoxSubject.Enabled = false;
				buttonLoadOrder.Enabled = false;
				buttonAdd.Enabled = false;
				buttonModify.Enabled = false;
				buttonRemove.Enabled = false;
			}
		}

		/// <summary>
		/// 受注伝票からの読込
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonLoadOrder_Click(object sender, EventArgs e)
		{
			if (-1 != comboBoxNarcohm.SelectedIndex)
			{
				List<NarcohmOrderInfo> list = NarcohmOrderCheckAccess.GetNarcohmOrderInfo(ApplicateInfo.CustomerNo, (string)comboBoxNarcohm.SelectedValue, false);
				if (0 < list.Count)
				{
					using (SelectOrderInfoForm form = new SelectOrderInfoForm())
					{
						form.OrderInfoList = list;
						if (DialogResult.OK == form.ShowDialog())
						{
							listViewApplicate.Items.Clear();
							foreach (NarcohmOrderInfo order in form.SelectOrderInfoList)
							{
								T_NARCOHM_APPLICATE_DETAIL detail = new T_NARCOHM_APPLICATE_DETAIL();
								detail.SetNarcohmOrderInfo(order);
								ListViewItem lvItem = new ListViewItem(detail.GetListViewData());
								lvItem.Tag = detail;
								listViewApplicate.Items.Add(lvItem);
								if (0 == textBoxSubject.Text.Length)
								{
									textBoxSubject.Text = order.Subject;
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show("該当する受注伝票は存在しません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		/// <summary>
		/// 申込情報の追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			using (InputApplicateDetailForm form = new InputApplicateDetailForm())
			{
				form.ApplicateDetail = new T_NARCOHM_APPLICATE_DETAIL();
				form.DetailGoodsDataTable = DetailGoodsDataTable;
				if (DialogResult.OK == form.ShowDialog())
				{
					ListViewItem lvItem = new ListViewItem(form.ApplicateDetail.GetListViewData());
					lvItem.Tag = form.ApplicateDetail;
					listViewApplicate.Items.Add(lvItem);
					listViewApplicate.Items[listViewApplicate.Items.Count - 1].Selected = true;
				}
			}
		}

		/// <summary>
		/// 申込情報の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonModify_Click(object sender, EventArgs e)
		{
			if (0 < listViewApplicate.SelectedIndices.Count)
			{
				ListViewItem lvItem = listViewApplicate.SelectedItems[0];
				using (InputApplicateDetailForm form = new InputApplicateDetailForm())
				{
					form.ApplicateDetail = lvItem.Tag as T_NARCOHM_APPLICATE_DETAIL;
					form.DetailGoodsDataTable = DetailGoodsDataTable;
					form.ModifyFlag = true;
					if (DialogResult.OK == form.ShowDialog())
					{
						lvItem.Tag = form.ApplicateDetail;
						string[] data = form.ApplicateDetail.GetListViewData();
						lvItem.Text = data[0];
						lvItem.SubItems[1].Text = data[1];
						lvItem.SubItems[2].Text = data[2];
						lvItem.SubItems[3].Text = data[3];
						lvItem.SubItems[4].Text = data[4];
						lvItem.SubItems[5].Text = data[5];
						lvItem.SubItems[6].Text = data[6];
						lvItem.SubItems[7].Text = data[7];
						lvItem.SubItems[8].Text = data[8];
					}
				}
			}
		}

		/// <summary>
		/// 申込情報の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewApplicate_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			buttonModify.PerformClick();
		}

		/// <summary>
		/// 申込情報の削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonRemove_Click(object sender, EventArgs e)
		{
			if (0 < listViewApplicate.SelectedIndices.Count)
			{
				listViewApplicate.Items.Remove(listViewApplicate.SelectedItems[0]);
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 == ApplicateInfo.CustomerNo)
			{
				MessageBox.Show("顧客Noが設定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == dateTimePickerServiceStartDate.Checked)
			{
				MessageBox.Show("サービス開始日が設定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == dateTimePickerKakinStartYM.Checked)
			{
				MessageBox.Show("課金開始月が設定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 == listViewApplicate.Items.Count)
			{
				MessageBox.Show("申込詳細情報が設定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// サービス開始日
			ApplicateInfo.ServiceStartDate = new Date(dateTimePickerServiceStartDate.Value);

			// 課金開始月
			ApplicateInfo.KakinStartYM = new YearMonth(dateTimePickerKakinStartYM.Value.Year, dateTimePickerKakinStartYM.Value.Month);

			// 販売種別
			if (0 == comboBoxSaleType.SelectedIndex)
			{
				ApplicateInfo.SaleType = MwsDefine.ApplyType.Monthly;
			}
			else
			{
				ApplicateInfo.SaleType = MwsDefine.ApplyType.Etc;
			}
			// 件名
			ApplicateInfo.Subject = textBoxSubject.Text.Trim();

			// 申込詳細情報
			ApplicateInfo.DetailList.Clear();
			foreach (ListViewItem lvItem in listViewApplicate.Items)
			{
				T_NARCOHM_APPLICATE_DETAIL detail = lvItem.Tag as T_NARCOHM_APPLICATE_DETAIL;
				ApplicateInfo.DetailList.Add(detail);
			}
			if (ModifyFlag)
			{
				// 変更
				ApplicateInfo.UpdateDate = DateTime.Now;
				ApplicateInfo.UpdatePerson = Program.ProductName;

				// 申込情報の変更
				CharlieDatabaseAccess.UpdateSet_T_NARCOHM_APPLICATE_HEADER(ApplicateInfo, true);
			}
			else
			{
				// 新規作成
				ApplicateInfo.CreateDate = DateTime.Now;
				ApplicateInfo.CreatePerson = Program.ProductName;

				// 申込情報の新規追加
				CharlieDatabaseAccess.InsertInto_T_NARCOHM_APPLICATE_HEADER(ApplicateInfo, true);
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
