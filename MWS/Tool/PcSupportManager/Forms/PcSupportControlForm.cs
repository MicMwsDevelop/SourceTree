using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.Common;
using MwsLib.DB.SqlServer.PcSupportManager;

namespace PcSupportManager.Forms
{
	/// <summary>
	/// 管理情報の登録
	/// </summary>
	public partial class PcSupportControlForm : Form
	{
		/// <summary>
		/// 受注No
		/// </summary>
		private string OrderNo { get; set; }

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

			OrderNo = string.Empty;
			OrgPcSupport = null;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="orderNo">受注No</param>
		public PcSupportControlForm(string orderNo)
		{
			InitializeComponent();

			OrderNo = orderNo;
			OrgPcSupport = null;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="pcSupport">PC安心サポート管理情報</param>
		public PcSupportControlForm(PcSupportControl pcSupport)
		{
			InitializeComponent();

			OrderNo = string.Empty;
			OrgPcSupport = pcSupport;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PcSupportControlForm_Load(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// イベントハンドラ削除
			comboBoxGoods.SelectedIndexChanged -= new EventHandler(comboBoxGoods_SelectedIndexChanged);
			comboBoxBranch.SelectedIndexChanged -= new EventHandler(comboBoxBranch_SelectedIndexChanged);

			// 商品名コンボボックスの設定
			comboBoxGoods.DisplayMember = "GoodsName";
			comboBoxGoods.ValueMember = "GoodsID";
			comboBoxGoods.DataSource = MainForm.gPcSupportGoodsList;

			// 拠店コンボボックスの設定
			comboBoxBranch.DisplayMember = "BranchName3";
			comboBoxBranch.ValueMember = "BranchCode3";
			comboBoxBranch.DataSource = MainForm.gBranchEmployeeList;

			// 営業担当員コンボボックスの設定
			comboBoxEmployee.DisplayMember = "UserName";
			comboBoxEmployee.ValueMember = "UserID";

			if (null != OrgPcSupport)
			{
				// 変更
				comboBoxEmployee.DataSource = MainForm.gBranchEmployeeList[0].EmployeeList;

				// 無効フラグを有効
				checkBoxDisable.Enabled = true;

				// 医院名
				textBoxClinicName.Text = OrgPcSupport.ClinicName;

				// 受注No
				textBoxOrderNo.Text = OrgPcSupport.OrderNo;

				// 顧客No
				textBoxCustomerNo.Text = OrgPcSupport.CustomerNo.ToString();
				textBoxCustomerNo.ReadOnly = true;

				// 商品名
				comboBoxGoods.SelectedValue = OrgPcSupport.GoodsID;

				// 契約年数
				textBoxAgreeYear.Text = string.Format("{0}年", OrgPcSupport.AgreeYear);

				// 料金
				textBoxPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(OrgPcSupport.Price));

				// 拠店
				comboBoxBranch.SelectedValue = OrgPcSupport.BranchID;

				// 営業担当員
				BranchEmployeeInfo branch = MainForm.gBranchEmployeeList.Find(p => p.BranchCode3 == OrgPcSupport.BranchID);
				comboBoxEmployee.DataSource = branch.EmployeeList;
				comboBoxEmployee.SelectedValue = OrgPcSupport.SalesmanID;

				// 契約開始日
				if (OrgPcSupport.StartDate.HasValue)
				{
					dateTimePickerStartDate.Checked = true;
					dateTimePickerStartDate.Value = OrgPcSupport.StartDate.Value.ToDateTime();
				}
				// 申込用紙有無
				checkBoxOrderReportAccept.Checked = OrgPcSupport.OrderReportAccept;

				// 受注承認日
				if (OrgPcSupport.OrderApprovalDate.HasValue)
				{
					dateTimePickerOrderApprovalDate.Value = OrgPcSupport.OrderApprovalDate.Value.ToDateTime();
				}
				// メールアドレス
				textBoxMailAdderss.Text = OrgPcSupport.MailAddress;

				// 備考
				textBoxRemark.Text = OrgPcSupport.Remark;

				// 受注日
				if (OrgPcSupport.OrderDate.HasValue)
				{
					dateTimePickerOrderDate.Value = OrgPcSupport.OrderDate.Value.ToDateTime();
				}
				if (OrgPcSupport.PeriodEndDate.HasValue)
				{
					// 利用期限日
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
				comboBoxEmployee.DataSource = MainForm.gBranchEmployeeList[0].EmployeeList;

				// 受注No
				textBoxOrderNo.Text = OrderNo;

				// 商品名
				comboBoxGoods.SelectedValue = MainForm.gPcSupportGoodsList[0].GoodsID;

				// 契約年数
				textBoxAgreeYear.Text = string.Format("{0}年", MainForm.gPcSupportGoodsList[0].AgreeYear);

				// 料金
				textBoxPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gPcSupportGoodsList[0].Price));

				// 拠店
				comboBoxBranch.SelectedValue = MainForm.gBranchEmployeeList[0].BranchCode3;

				// 営業担当員
				comboBoxEmployee.SelectedValue = MainForm.gBranchEmployeeList[0].EmployeeList[0].UserID;
			}
			// イベントハンドラ追加
			comboBoxGoods.SelectedIndexChanged += new EventHandler(comboBoxGoods_SelectedIndexChanged);
			comboBoxBranch.SelectedIndexChanged += new EventHandler(comboBoxBranch_SelectedIndexChanged);

			// カーソルを元に戻す
			Cursor.Current = preCursor;
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
				BranchEmployeeInfo branch = MainForm.gBranchEmployeeList.Find(p => p.BranchCode3 == branchID);
				comboBoxEmployee.DataSource = branch.EmployeeList;
				comboBoxEmployee.SelectedValue = branch.EmployeeList[0].UserID;
			}
		}

		/// <summary>
		/// 利用期限日の有効/無効
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateTimePickerPeriodEndDate_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimePickerPeriodEndDate.Checked)
			{
				textBoxCancelReason.Enabled = true;
				checkBoxCancelReportAccept.Enabled = true;
			}
			else
			{
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
			if (0 == textBoxMailAdderss.Text.Length)
			{
				if (DialogResult.No == MessageBox.Show("メールアドレスが設定されていません。よろしいですか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					textBoxMailAdderss.Focus();
					return;
				}
			}
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			if (null == OrgPcSupport)
			{
				// 新規
				PcSupportControl pc = new PcSupportControl();

				// 受注No
				pc.OrderNo = OrderNo;

				// 顧客No
				pc.CustomerNo = int.Parse(textBoxCustomerNo.Text);

				// 医院名
				pc.ClinicName = textBoxClinicName.Text;

				// 商品ID
				pc.GoodsID = comboBoxGoods.SelectedValue as string;

				// 商品名
				PcSupportGoodsInfo goodsInfo = comboBoxGoods.SelectedItem as PcSupportGoodsInfo;
				pc.GoodsID = goodsInfo.GoodsName;

				// 料金
				pc.Price = goodsInfo.Price;

				// 契約年数
				pc.AgreeYear = goodsInfo.AgreeYear;

				if (dateTimePickerStartDate.Checked)
				{
					// 契約開始日
					pc.StartDate = new Date(dateTimePickerStartDate.Value);

					// 契約終了日
					pc.EndDate = PcSupportControl.GetEndDate(pc.StartDate.Value, goodsInfo.AgreeYear);
				}
				// 拠店ID
				pc.BranchID = comboBoxBranch.SelectedValue as string;

				// 拠店名
				BranchEmployeeInfo branch = comboBoxBranch.SelectedItem as BranchEmployeeInfo;
				pc.BranchName = branch.BranchName3;

				// 担当者ID
				pc.SalesmanID = comboBoxEmployee.SelectedValue as string;

				// 担当者名
				EmployeeInfo employee = comboBoxEmployee.SelectedItem as EmployeeInfo;
				pc.SalesmanName = employee.UserName;

				// メールアドレス
				pc.MailAddress = textBoxMailAdderss.Text.Trim();

				// 備考
				pc.Remark = textBoxRemark.Text.Trim();

				// 受注日
				pc.OrderDate = new Date(dateTimePickerOrderDate.Value);

				// 申込用紙有無
				pc.OrderReportAccept = checkBoxOrderReportAccept.Checked;

				// 受注承認日
				if (dateTimePickerOrderApprovalDate.Checked)
				{
					pc.OrderApprovalDate = new Date(dateTimePickerOrderApprovalDate.Value);
				}
				// 作成日時
				pc.CreateDateTime = Program.SystemDate.ToDateTime();

				// 作成者
				pc.CreatePerson = PcSupportControl.PERSON_NAME;

				// WonderWeb更新フラグ
				pc.WonderWebRenewalFlag = true;

				try
				{
					PcSupportManagerAccess.SetPcSupportControl(pc);
					base.DialogResult = DialogResult.Cancel;
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("PcSupportManagerAccess.SetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
			else
			{
				// 更新
				PcSupportControl pc = new PcSupportControl(OrgPcSupport);

				// 商品ID
				pc.GoodsID = comboBoxGoods.SelectedValue as string;

				// 商品名
				PcSupportGoodsInfo goodsInfo = comboBoxGoods.SelectedItem as PcSupportGoodsInfo;
				pc.GoodsName = goodsInfo.GoodsName;

				// 契約年数
				pc.AgreeYear = goodsInfo.AgreeYear;

				// 料金
				pc.Price = goodsInfo.Price;

				if (dateTimePickerStartDate.Checked)
				{
					// 契約開始日
					pc.StartDate = new Date(dateTimePickerStartDate.Value);

					// 契約終了日
					pc.EndDate = PcSupportControl.GetEndDate(pc.StartDate.Value, goodsInfo.AgreeYear);
				}
				else
				{
					// 契約開始日
					pc.StartDate = null;

					// 契約終了日
					pc.EndDate = null;
				}
				// 拠店ID
				pc.BranchID = comboBoxBranch.SelectedValue as string;

				// 拠店名
				BranchEmployeeInfo branch = comboBoxBranch.SelectedItem as BranchEmployeeInfo;
				pc.BranchName = branch.BranchName3;

				// 担当者ID
				pc.SalesmanID = comboBoxEmployee.SelectedValue as string;

				// 担当者名
				EmployeeInfo employee = comboBoxEmployee.SelectedItem as EmployeeInfo;
				pc.SalesmanName = employee.UserName;

				// メールアドレス
				pc.MailAddress = textBoxMailAdderss.Text.Trim();

				// 備考
				pc.Remark = textBoxRemark.Text.Trim();

				// 受注日
				pc.OrderDate = new Date(dateTimePickerOrderDate.Value);

				// 申込用紙有無
				pc.OrderReportAccept = checkBoxOrderReportAccept.Checked;

				// 受注承認日
				if (dateTimePickerOrderApprovalDate.Checked)
				{
					pc.OrderApprovalDate = new Date(dateTimePickerOrderApprovalDate.Value);
				}
				else
				{
					pc.OrderApprovalDate = null;
				}
				// 解約情報
				if (dateTimePickerPeriodEndDate.Checked)
				{
					// 利用期限日
					pc.PeriodEndDate = new Date(dateTimePickerPeriodEndDate.Value);

					// 解約日時
					pc.CancelDate = new Date(DateTime.Now);

					// 解約届有無
					pc.CancelReportAccept = checkBoxCancelReportAccept.Checked;

					// 解約事由
					pc.CancelReason = textBoxCancelReason.Text.Trim();
				}
				else
				{
					// 利用期限日
					pc.PeriodEndDate = null;

					// 解約日時
					//pc.CancelDate = null;

					// 解約届有無
					pc.CancelReportAccept = checkBoxCancelReportAccept.Checked;

					// 解約事由
					pc.CancelReason = textBoxCancelReason.Text.Trim();
				}
				// 更新日時
				pc.UpdateDateTime = DateTime.Now;

				// 更新者
				pc.UpdatePerson = PcSupportControl.PERSON_NAME;

				// 無効
				pc.DisableFlag = checkBoxDisable.Checked;

				// WonderWeb更新フラグ
				if (false == OrgPcSupport.WonderWebRenewalFlag)
				{
					pc.WonderWebRenewalFlag = pc.IsWonderWebRenewal(OrgPcSupport);
				}
				try
				{
					PcSupportManagerAccess.SetPcSupportControl(pc);
					base.DialogResult = DialogResult.OK;
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("PcSupportManagerAccess.SetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}
	}
}
