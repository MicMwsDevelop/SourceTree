using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.BaseFactory.PcSupportTool;
using MwsLib.Common;
using MwsLib.DB.SqlServer.PcSupportTool;

namespace PcSupportTool.Forms
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
				BranchInfo branch = MainForm.gBranchList.Find(p => p.BranchCode3 == OrgPcSupport.BranchID);
				comboBoxEmployee.DataSource = branch.EmployeeList;
				comboBoxEmployee.SelectedValue = OrgPcSupport.SalesmanID;

				// 契約開始日
				if (OrgPcSupport.StartDate.HasValue)
				{
					dateTimePickerStartDate.Value = OrgPcSupport.StartDate.Value.ToDateTime();
				}
				// 申込用紙有無
				checkBoxOrderReportAccept.Checked = OrgPcSupport.OrderReportAccept;

				// 受注承認日
				if (OrgPcSupport.OrderApprovalDate.HasValue)
				{
					checkBoxOrderApprovalDate.Checked = true;
					dateTimePickerOrderApprovalDate.Enabled = true;
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

				// 受注No
				textBoxOrderNo.Text = OrderNo;

				// 商品名
				comboBoxGoods.SelectedValue = MainForm.gPcSupportGoodsList[0].GoodsID;

				// 契約年数
				textBoxAgreeYear.Text = string.Format("{0}年", MainForm.gPcSupportGoodsList[0].AgreeYear);

				// 料金
				textBoxPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gPcSupportGoodsList[0].Price));

				// 契約開始日
				dateTimePickerStartDate.Value = DateTime.Now;	//MainForm.gSystemDate.ToDateTime();

				// 拠店
				comboBoxBranch.SelectedValue = MainForm.gBranchList[0].BranchCode3;

				// 営業担当員
				comboBoxEmployee.SelectedValue = MainForm.gBranchList[0].EmployeeList[0].UserID;

				// 受注日
				dateTimePickerOrderDate.Value = DateTime.Now;
			}
			// イベントハンドラ追加
			comboBoxGoods.SelectedIndexChanged += new EventHandler(comboBoxGoods_SelectedIndexChanged);
			checkBoxPeriodEndDate.CheckedChanged += new EventHandler(checkBoxPeriodEndDate_CheckedChanged);
			comboBoxBranch.SelectedIndexChanged += new EventHandler(comboBoxBranch_SelectedIndexChanged);
			checkBoxPeriodEndDate.CheckedChanged += new EventHandler(checkBoxPeriodEndDate_CheckedChanged);

			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// 顧客Noの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxCustomerNo_TextChanged(object sender, EventArgs e)
		{
			//if (8 == textBoxCustomerNo.Text.Length)
			//{
			//	int customerNo;
			//	if (int.TryParse(textBoxCustomerNo.Text, out customerNo))
			//	{
			//		try
			//		{
			//			textBoxClinicName.Text = PcetySupportAccess.GetClinicName(customerNo, Program.SQLSV2);
			//		}
			//		catch (Exception ex)
			//		{
			//			MessageBox.Show(string.Format("PcSafetySupportAccess.GetClinicName() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			//		}
			//	}
			//	else
			//	{
			//		MessageBox.Show("顧客Noが正しく入力されていません。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//	}
			//}
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
				BranchInfo branch = MainForm.gBranchList.Find(p => p.BranchCode3 == branchID);
				comboBoxEmployee.DataSource = branch.EmployeeList;
				comboBoxEmployee.SelectedValue = branch.EmployeeList[0].UserID;
			}
		}

		/// <summary>
		/// 受注承認日
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxOrderApprovalDate_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxOrderApprovalDate.Checked)
			{
				dateTimePickerOrderApprovalDate.Enabled = true;
			}
			else
			{
				dateTimePickerOrderApprovalDate.Enabled = false;
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
				PcSupportGoodsInfo goodsInfo = MainForm.gPcSupportGoodsList.Find(p => p.GoodsID == pc.GoodsID);
				pc.GoodsID = goodsInfo.GoodsName;

				// 料金
				pc.Price = goodsInfo.Price;

				// 契約年数
				pc.AgreeYear = goodsInfo.AgreeYear;

				// 契約開始日
				pc.StartDate = new Date(dateTimePickerStartDate.Value);

				// 契約終了日
				pc.EndDate = PcSupportControl.GetEndDate(pc.StartDate.Value, goodsInfo.AgreeYear);

				// 拠店ID
				pc.BranchID = comboBoxBranch.SelectedValue as string;

				// 拠店名
				pc.BranchName = comboBoxBranch.SelectedItem as string;

				// 担当者ID
				pc.SalesmanID = comboBoxEmployee.SelectedValue as string;

				// 担当者名
				pc.SalesmanName = comboBoxEmployee.SelectedItem as string; ;

				// メールアドレス
				pc.MailAddress = textBoxMailAdderss.Text.Trim();

				// 備考
				pc.Remark = textBoxRemark.Text.Trim();

				// 受注日
				pc.OrderDate = new Date(dateTimePickerOrderDate.Value);

				// 申込用紙有無
				pc.OrderReportAccept = checkBoxOrderReportAccept.Checked;

				// 受注承認日
				if (checkBoxOrderApprovalDate.Checked)
				{
					pc.OrderApprovalDate = new Date(dateTimePickerOrderApprovalDate.Value);
				}
				// 作成日時
				pc.CreateDateTime = DateTime.Now;

				// 作成者
				pc.CreatePerson = MainForm.PROGRAM_NAME;

				// WonderWeb更新フラグ
				pc.WonderWebRenewalFlag = true;

				try
				{
					PcSupportToolAccess.SetPcSupportControl(pc);
					base.DialogResult = DialogResult.Cancel;
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("PcSupportToolAccess.SetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
			else
			{
				// 更新
				PcSupportControl pc = new PcSupportControl(OrgPcSupport);

				// 商品ID
				pc.GoodsID = comboBoxGoods.SelectedValue as string;

				// 商品名
				PcSupportGoodsInfo goodsInfo = MainForm.gPcSupportGoodsList.Find(p => p.GoodsID == pc.GoodsID);
				pc.GoodsName = goodsInfo.GoodsName;

				// 契約年数
				pc.AgreeYear = goodsInfo.AgreeYear;

				// 料金
				pc.Price = goodsInfo.Price;

				// 契約開始日
				pc.StartDate = new Date(dateTimePickerStartDate.Value);

				// 契約終了日
				pc.EndDate = PcSupportControl.GetEndDate(pc.StartDate.Value, goodsInfo.AgreeYear);

				// 拠店ID
				pc.BranchID = comboBoxBranch.SelectedValue as string;

				// 拠店名
				pc.BranchName = comboBoxBranch.SelectedItem as string;

				// 担当者ID
				pc.SalesmanID = comboBoxEmployee.SelectedValue as string;

				// 担当者名
				pc.SalesmanName = comboBoxEmployee.SelectedItem as string; ;

				// メールアドレス
				pc.MailAddress = textBoxMailAdderss.Text.Trim();

				// 備考
				pc.Remark = textBoxRemark.Text.Trim();

				// 受注日
				pc.OrderDate = new Date(dateTimePickerOrderDate.Value);

				// 申込用紙有無
				pc.OrderReportAccept = checkBoxOrderReportAccept.Checked;

				// 受注承認日
				if (checkBoxOrderApprovalDate.Checked)
				{
					pc.OrderApprovalDate = new Date(dateTimePickerOrderApprovalDate.Value);
				}
				else
				{
					pc.OrderApprovalDate = null;
				}
				// 解約情報
				if (checkBoxPeriodEndDate.Enabled && checkBoxPeriodEndDate.Checked)
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
					pc.CancelDate = null;

					// 解約届有無
					pc.CancelReportAccept = false;

					// 解約事由
					pc.CancelReason = string.Empty;
				}
				// 更新日時
				pc.UpdateDateTime = DateTime.Now;

				// 更新者
				pc.UpdatePerson = MainForm.PROGRAM_NAME;

				// 無効
				pc.DisableFlag = checkBoxDisable.Checked;

				// WonderWeb更新フラグ
				if (false == OrgPcSupport.WonderWebRenewalFlag)
				{
					pc.WonderWebRenewalFlag = pc.IsWonderWebRenewal(OrgPcSupport);
				}
				try
				{
					PcSupportToolAccess.SetPcSupportControl(pc);
					base.DialogResult = DialogResult.OK;
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("PcSupportToolAccess.SetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}
	}
}
