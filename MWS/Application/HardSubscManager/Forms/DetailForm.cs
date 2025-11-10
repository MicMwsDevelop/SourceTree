//
// DetailForm.cs
//
// 貸出機器情報入力画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/11/10 勝呂)
// 
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Windows.Forms;

namespace HardSubscManager.Forms
{
	/// <summary>
	/// 貸出機器情報入力画面クラス
	/// </summary>
	public partial class DetailForm : Form
	{
		/// <summary>
		/// 内部契約番号
		/// </summary>
		public int InternalContractNo { get; set; }

		/// <summary>
		/// 機器情報（保存用）
		/// </summary>
		public T_HARD_SUBSC_DETAIL SaveDetail { get; set; }

		/// <summary>
		/// 機器情報
		/// </summary>
		public T_HARD_SUBSC_DETAIL Detail { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DetailForm()
		{
			InitializeComponent();

			InternalContractNo = 0;
			SaveDetail = null;
			Detail = null;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DetailForm_Load(object sender, EventArgs e)
		{
			if (null == SaveDetail)
			{
				// 新規入力
				numericTextBoxQuantity.Text = "1";
			}
			else
			{
				// 変更
				numericTextBoxGoodsCode.Text = SaveDetail.GoodsCode;
				labelGoodsName.Text = SaveDetail.GoodsName;
				labelCategory.Text = SaveDetail.CategoryName;
				numericTextBoxQuantity.Text = SaveDetail.Quantity.ToString();
				textBoxSerialNo.Text = SaveDetail.SerialNo;
				textBoxScanFilename.Text = SaveDetail.ScanFilename;
				textBoxAssetsCode.Text = SaveDetail.AssetsCode;
				if (SaveDetail.ExchangeDate.HasValue)
				{
					dateTimePickerExchangeDate.Checked = true;
					dateTimePickerExchangeDate.Value = SaveDetail.ExchangeDate.Value;
				}
				textBoxDstSerialNo.Text = SaveDetail.DstSerialNo;
				Detail = SaveDetail.DeepCopy();
				if (Program.CategoryPC == SaveDetail.CategoryName)
				{
					labelCategory.ForeColor = System.Drawing.Color.Red;
				}
			}
		}

		/// <summary>
		/// 商品の検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			if (PcaGoodsIDDefine.GoodsCodeLength == numericTextBoxGoodsCode.Text.Length)
			{
				vMicPCA商品マスタ pca = JunpDatabaseAccess.Select_vMicPCA商品マスタ(numericTextBoxGoodsCode.Text, Program.gSettings.ConnectJunp.ConnectionString);
				if (null != pca)
				{
					labelGoodsName.Text = pca.sms_mei;
					labelCategory.Text = Program.GetCategoryName(pca.sms_skbn3.Value);
					labelCategory.Tag = pca.sms_skbn3.Value;
					textBoxSerialNo.Text = string.Empty;
					textBoxScanFilename.Text = string.Empty;
					textBoxAssetsCode.Text = string.Empty;
					dateTimePickerExchangeDate.Checked = false;
					textBoxDstSerialNo.Text = string.Empty;
					if (Program.CategoryPC == labelCategory.Text)
					{
						labelCategory.ForeColor = System.Drawing.Color.Red;
					}
					else
					{
						labelCategory.ForeColor = System.Drawing.Color.Black;
					}
				}
				else
				{
					MessageBox.Show("指定された商品コードに該当する商品がありません。商品コードをお確かめください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					labelGoodsName.Text = string.Empty;
					labelCategory.Text = string.Empty;
					labelCategory.Tag = null;
					textBoxSerialNo.Text = string.Empty;
					textBoxScanFilename.Text = string.Empty;
					textBoxAssetsCode.Text = string.Empty;
					dateTimePickerExchangeDate.Checked = false;
					textBoxDstSerialNo.Text = string.Empty;
					labelCategory.ForeColor = System.Drawing.Color.Black;
				}
			}
			else
			{
				MessageBox.Show("商品コードを正しく入力してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				labelGoodsName.Text = string.Empty;
				labelCategory.Text = string.Empty;
				labelCategory.Tag = null;
				textBoxSerialNo.Text = string.Empty;
				textBoxScanFilename.Text = string.Empty;
				textBoxAssetsCode.Text = string.Empty;
				dateTimePickerExchangeDate.Checked = false;
				textBoxDstSerialNo.Text = string.Empty;
				labelCategory.ForeColor = System.Drawing.Color.Black;
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 == labelGoodsName.Text.Length)
			{
				MessageBox.Show("機器名が設定されていません。商品コードによる検索を行ってください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			short quantity = (short)numericTextBoxQuantity.ToInt();
			if (quantity < 1)
			{
				MessageBox.Show("数量を入力してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			string serialNo = textBoxSerialNo.Text.Trim();
			string assetsCode = textBoxAssetsCode.Text.Trim();
			if (1 < quantity && 0 < assetsCode.Length)
			{
				MessageBox.Show("資産管理する機器の数量は１固定です。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (null == SaveDetail)
			{
				// 新規入力
				Detail = new T_HARD_SUBSC_DETAIL();
				Detail.InternalContractNo = InternalContractNo;
				Detail.GoodsCode = numericTextBoxGoodsCode.Text;
				Detail.GoodsName = labelGoodsName.Text;
				Detail.CategoryName = labelCategory.Text;
				Detail.Quantity = quantity;
				Detail.SerialNo = serialNo;
				Detail.ScanFilename = textBoxScanFilename.Text;
				Detail.AssetsCode = assetsCode;
				if (dateTimePickerExchangeDate.Checked)
				{
					Detail.ExchangeDate = dateTimePickerExchangeDate.Value;
				}
				Detail.DstSerialNo = textBoxDstSerialNo.Text;
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				// 変更
				Detail.GoodsCode = numericTextBoxGoodsCode.Text;
				Detail.GoodsName = labelGoodsName.Text;
				Detail.CategoryName = labelCategory.Text;
				Detail.Quantity = quantity;
				Detail.SerialNo = serialNo;
				Detail.ScanFilename = textBoxScanFilename.Text;
				Detail.AssetsCode = assetsCode;
				if (dateTimePickerExchangeDate.Checked)
				{
					Detail.ExchangeDate = dateTimePickerExchangeDate.Value;
				}
				else
				{
					Detail.ExchangeDate = null;
				}
				Detail.DstSerialNo = textBoxDstSerialNo.Text;
				if (false == SaveDetail.Equals(Detail))
				{
					this.DialogResult = DialogResult.OK;
				}
				else
				{
					this.DialogResult = DialogResult.Cancel;
				}
			}
			this.Close();
		}

		/// <summary>
		/// キャンセル
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
