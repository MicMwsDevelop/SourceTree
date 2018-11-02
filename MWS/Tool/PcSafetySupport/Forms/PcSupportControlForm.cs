using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.BaseFactory.PcSafetySupport;
using MwsLib.Common;
using MwsLib.DB.SqlServer.PcSafetySupport;

namespace PcSafetySupport.Forms
{
	/// <summary>
	/// 管理情報の登録
	/// </summary>
	public partial class PcSupportControlForm : Form
	{
		/// <summary>
		/// 顧客ID
		/// </summary>
		private string CustomerID { get; set; }

		/// <summary>
		/// PC安心サポート管理情報保存領域
		/// </summary>
		private PcSupportControl OrgPcSupport;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private PcSupportControlForm()
		{
			InitializeComponent();

			CustomerID = string.Empty;
			OrgPcSupport = null;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="customerID">顧客ID</param>
		public PcSupportControlForm(string customerID)
		{
			InitializeComponent();

			CustomerID = customerID;
			OrgPcSupport = null;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="pcSupport">PC安心サポート管理情報</param>
		public PcSupportControlForm(PcSupportControl pcSupport)
		{
			InitializeComponent();

			CustomerID = string.Empty;
			OrgPcSupport = pcSupport;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PcSupportControlForm_Load(object sender, EventArgs e)
		{
			// イベントハンドラ削除
			comboBoxGoods.SelectedIndexChanged -= new EventHandler(comboBoxGoods_SelectedIndexChanged);
			dateTimePickerStartDate.ValueChanged -= new EventHandler(dateTimePickerStartDate_ValueChanged);
			checkBoxPeriodEndDate.CheckedChanged -= new EventHandler(checkBoxPeriodEndDate_CheckedChanged);
			comboBoxBranch.SelectedIndexChanged -= new EventHandler(comboBoxBranch_SelectedIndexChanged);
			checkBoxPeriodEndDate.CheckedChanged -= new EventHandler(checkBoxPeriodEndDate_CheckedChanged);

			// 商品名コンボボックスの設定
			comboBoxGoods.DisplayMember = "GoodsName";
			comboBoxGoods.ValueMember = "GoodsID";
			comboBoxGoods.DataSource = MainForm.gPcSupportGoodsList;

			// 拠店コンボボックスの設定
			comboBoxBranch.DisplayMember = "BranchName3";
			comboBoxBranch.ValueMember = "BranchCode3";
			comboBoxBranch.DataSource = MainForm.gBranchList;

			// 営業担当員コンボボックスの設定
			comboBoxEmployee.DisplayMember = "UserName";
			comboBoxEmployee.ValueMember = "UserID";

			if (null != OrgPcSupport)
			{
				// 変更
				comboBoxEmployee.DataSource = MainForm.gBranchList[0].EmployeeList;

				// 顧客ID
				textBoxCustomerID.Text = OrgPcSupport.CustomerID;

				// 商品名
				comboBoxGoods.SelectedValue = OrgPcSupport.GoodsID;

				// 契約年数
				textBoxAgreeYear.Text = string.Format("{0}年", OrgPcSupport.AgreeYear);

				// 料金
				textBoxPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(OrgPcSupport.Price));

				// 拠店
				comboBoxBranch.SelectedValue = OrgPcSupport.BranchID;

				// 営業担当員
				BranchInfo branch = MainForm.gBranchList.Find(p => p.BranchCode3 == OrgPcSupport.BranchID);
				comboBoxEmployee.DataSource = branch.EmployeeList;
				comboBoxEmployee.SelectedValue = OrgPcSupport.SalesmanID;

				// 契約開始日
				if (OrgPcSupport.StartDate.HasValue)
				{
					dateTimePickerStartDate.Value = OrgPcSupport.StartDate.Value.ToDateTime();
				}
				// 契約終了日
				if (OrgPcSupport.EndDate.HasValue)
				{
					dateTimePickerEndDate.Value = OrgPcSupport.EndDate.Value.ToDateTime();
				}
				// 申込用紙有無
				checkBoxApplyReportAccept.Checked = OrgPcSupport.ApplyReportAccept;

				// メールアドレス
				textBoxMaleAdderss.Text = OrgPcSupport.MaleAddress;

				// 備考１
				textBoxRemark1.Text = OrgPcSupport.Remark1;

				// 備考２
				textBoxRemark2.Text = OrgPcSupport.Remark2;

				// 申込日付
				if (OrgPcSupport.ApplyDate.HasValue)
				{
					dateTimePickerAcceptDate.Value = OrgPcSupport.ApplyDate.Value.ToDateTime();
				}

				checkBoxPeriodEndDate.Enabled = true;
				if (OrgPcSupport.PeriodEndDate.HasValue)
				{
					// 利用期限日
					checkBoxPeriodEndDate.Checked = true;
					dateTimePickerPeriodEndDate.Enabled = true;
					dateTimePickerPeriodEndDate.Value = OrgPcSupport.PeriodEndDate.Value.ToDateTime();

					// 解約事由
					textBoxCancelReason.Enabled = true;
					textBoxCancelReason.Text = OrgPcSupport.CancelReason;

					// 解約届有無
					checkBoxCancelReportAccept.Enabled = true;
					checkBoxCancelReportAccept.Checked = OrgPcSupport.CancelReportAccept;
				}
			}
			else
			{
				// 新規
				comboBoxEmployee.DataSource = MainForm.gBranchList[0].EmployeeList;

				// 顧客ID
				textBoxCustomerID.Text = CustomerID;

				// 商品名
				comboBoxGoods.SelectedValue = MainForm.gPcSupportGoodsList[0].GoodsID;

				// 契約年数
				textBoxAgreeYear.Text = string.Format("{0}年", MainForm.gPcSupportGoodsList[0].AgreeYear);

				// 料金
				textBoxPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gPcSupportGoodsList[0].Price));

				// 契約開始日
				dateTimePickerStartDate.Value = MainForm.gSystemDate.ToDateTime();

				// 契約終了日
				dateTimePickerEndDate.Value = PcSupportControl.GetEndDate(MainForm.gSystemDate, MainForm.gPcSupportGoodsList[0].AgreeYear).ToDateTime();

				// 拠店
				comboBoxBranch.SelectedValue = MainForm.gBranchList[0].BranchCode3;

				// 営業担当員
				comboBoxEmployee.SelectedValue = MainForm.gBranchList[0].EmployeeList[0].UserID;

				// 申込日付
				dateTimePickerAcceptDate.Value = MainForm.gSystemDate.ToDateTime();
			}
			// イベントハンドラ追加
			comboBoxGoods.SelectedIndexChanged += new EventHandler(comboBoxGoods_SelectedIndexChanged);
			dateTimePickerStartDate.ValueChanged += new EventHandler(dateTimePickerStartDate_ValueChanged);
			checkBoxPeriodEndDate.CheckedChanged += new EventHandler(checkBoxPeriodEndDate_CheckedChanged);
			comboBoxBranch.SelectedIndexChanged += new EventHandler(comboBoxBranch_SelectedIndexChanged);
			checkBoxPeriodEndDate.CheckedChanged += new EventHandler(checkBoxPeriodEndDate_CheckedChanged);
		}

		/// <summary>
		/// 商品名の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxGoods_SelectedIndexChanged(object sender, EventArgs e)
		{
			string goodsID = comboBoxGoods.SelectedValue as string;
			if (0 < goodsID.Length)
			{
				PcSupportGoodsInfo goodsInfo = MainForm.gPcSupportGoodsList.Find(p => p.GoodsID == goodsID);

				// 契約年数テキストボックスの変更
				textBoxAgreeYear.Text = string.Format("{0}年", goodsInfo.AgreeYear);

				// 料金の変更
				textBoxPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(goodsInfo.Price));

				// 契約終了日の変更
				dateTimePickerEndDate.Value = PcSupportControl.GetEndDate(new Date(dateTimePickerStartDate.Value), goodsInfo.AgreeYear).ToDateTime();
			}
		}

		/// <summary>
		/// 契約開始日の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
		{
			string goodsID = comboBoxGoods.SelectedValue as string;
			if (0 < goodsID.Length)
			{
				// 契約終了日の変更
				PcSupportGoodsInfo goodsInfo = MainForm.gPcSupportGoodsList.Find(p => p.GoodsID == goodsID);
				dateTimePickerEndDate.Value = PcSupportControl.GetEndDate(new Date(dateTimePickerStartDate.Value), goodsInfo.AgreeYear).ToDateTime();
			}
		}

		/// <summary>
		/// 拠店IDの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxBranch_SelectedIndexChanged(object sender, EventArgs e)
		{
			string branchID = comboBoxBranch.SelectedValue as string;
			if (0 < branchID.Length)
			{
				// 営業担当員コンボボックスの設定
				BranchInfo branch = MainForm.gBranchList.Find(p => p.BranchCode3 == branchID);
				comboBoxEmployee.DataSource = branch.EmployeeList;
				comboBoxEmployee.SelectedValue = branch.EmployeeList[0].UserID;
			}
		}

		/// <summary>
		/// 利用期限日の有効/無効
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxPeriodEndDate_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxPeriodEndDate.Checked)
			{
				dateTimePickerPeriodEndDate.Enabled = true;
				textBoxCancelReason.Enabled = true;
				checkBoxCancelReportAccept.Enabled = true;
			}
			else
			{
				dateTimePickerPeriodEndDate.Enabled = false;
				textBoxCancelReason.Enabled = false;
				checkBoxCancelReportAccept.Enabled = false;
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			PcSupportControl pc = new PcSupportControl();
			if (null == OrgPcSupport)
			{
				// 新規

				// 顧客ID
				pc.CustomerID = CustomerID;

				// 商品ID
				pc.GoodsID = comboBoxGoods.SelectedValue as string;

				// 契約年数
				PcSupportGoodsInfo goods = MainForm.gPcSupportGoodsList.Find(p => p.GoodsID == pc.GoodsID);
				pc.AgreeYear = goods.AgreeYear;

				// 料金
				pc.Price = goods.Price;

				// 契約開始日
				pc.StartDate = new Date(dateTimePickerStartDate.Value);

				// 契約終了日
				pc.EndDate = new Date(dateTimePickerEndDate.Value);

				// 拠店
				pc.BranchID = comboBoxBranch.SelectedValue as string;

				// 営業担当員
				pc.SalesmanID = comboBoxEmployee.SelectedValue as string;

				// メールアドレス
				pc.MaleAddress = textBoxMaleAdderss.Text;

				// 備考１
				pc.Remark1 = textBoxRemark1.Text;

				// 備考２
				pc.Remark2 = textBoxRemark2.Text;

				// 申込用紙有無
				pc.ApplyReportAccept = checkBoxApplyReportAccept.Checked;

				// 申込日時
				pc.ApplyDate = new Date(dateTimePickerAcceptDate.Value);

				// 作成日時
				pc.CreateDateTime = DateTime.Now;

				// 作成者
				pc.CreatePerson = MainForm.PROGRAM_NAME;

				// WonderWeb更新フラグ
				pc.WonderWebRenewalFlag = true;

				string msg;
				if (pc.IsAcceptComputeData(out msg))
				{
					try
					{
						PcSafetySupportAccess.SetPcSupportControl(pc, true);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "PC安心サポート管理情報登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						base.DialogResult = DialogResult.Cancel;
						return;
					}
					base.DialogResult = DialogResult.OK;
				}
				else
				{
					MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				// 更新

				// 顧客ID
				pc.CustomerID = OrgPcSupport.CustomerID;

				// 商品ID
				pc.GoodsID = comboBoxGoods.SelectedValue as string;

				// 契約年数
				PcSupportGoodsInfo goods = MainForm.gPcSupportGoodsList.Find(p => p.GoodsID == pc.GoodsID);
				pc.AgreeYear = goods.AgreeYear;

				// 料金
				pc.Price = goods.Price;

				// 契約開始日
				pc.StartDate = new Date(dateTimePickerStartDate.Value);

				// 契約終了日
				pc.EndDate = new Date(dateTimePickerEndDate.Value);

				// 拠店
				pc.BranchID = comboBoxBranch.SelectedValue as string;

				// 営業担当員
				pc.SalesmanID = comboBoxEmployee.SelectedValue as string;

				// メールアドレス
				pc.MaleAddress = textBoxMaleAdderss.Text;

				// 備考１
				pc.Remark1 = textBoxRemark1.Text;

				// 備考２
				pc.Remark2 = textBoxRemark2.Text;

				// 申込用紙有無
				pc.ApplyReportAccept = checkBoxApplyReportAccept.Checked;

				// 解約情報
				if (checkBoxPeriodEndDate.Enabled && checkBoxPeriodEndDate.Checked)
				{
					// 利用期限日
					pc.PeriodEndDate = new Date(dateTimePickerPeriodEndDate.Value);

					// 解約日時
					pc.CancelDate = MainForm.gSystemDate;

					// 解約届有無
					pc.CancelReportAccept = checkBoxCancelReportAccept.Checked;

					// 解約事由
					pc.CancelReason = textBoxCancelReason.Text;
				}
				// 更新日時
				pc.UpdateDateTime = DateTime.Now;

				// 更新者
				pc.UpdatePerson = MainForm.PROGRAM_NAME;

				string msg;
				if (pc.IsAcceptComputeData(out msg))
				{
					// WonderWeb更新フラグ
					if (false == OrgPcSupport.WonderWebRenewalFlag)
					{
						pc.WonderWebRenewalFlag = pc.IsWonderWebRenewal(OrgPcSupport);
					}
					try
					{
						PcSafetySupportAccess.SetPcSupportControl(pc, true);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "PC安心サポート管理情報登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						base.DialogResult = DialogResult.Cancel;
						return;
					}
					base.DialogResult = DialogResult.OK;
				}
				else
				{
					MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}
	}
}
