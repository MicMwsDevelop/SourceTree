namespace CommonDialog.PrintPreview
{
    partial class PrintPreviewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.hiddenToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.printFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.LeftPanel = new System.Windows.Forms.Panel();
			this.zoomGroupBox = new System.Windows.Forms.GroupBox();
			this.zoomAutoRadioButton = new System.Windows.Forms.RadioButton();
			this.zoomManualRadioButton = new System.Windows.Forms.RadioButton();
			this.zoom500RadioButton = new System.Windows.Forms.RadioButton();
			this.zoom200RadioButton = new System.Windows.Forms.RadioButton();
			this.zoom150RadioButton = new System.Windows.Forms.RadioButton();
			this.zoom100RadioButton = new System.Windows.Forms.RadioButton();
			this.zoom75RadioButton = new System.Windows.Forms.RadioButton();
			this.zoom50RadioButton = new System.Windows.Forms.RadioButton();
			this.zoom25RadioButton = new System.Windows.Forms.RadioButton();
			this.zoom10RadioButton = new System.Windows.Forms.RadioButton();
			this.printButton = new System.Windows.Forms.Button();
			this.dispGroupBox = new System.Windows.Forms.GroupBox();
			this.Disp6RadioButton = new System.Windows.Forms.RadioButton();
			this.Disp4RadioButton = new System.Windows.Forms.RadioButton();
			this.Disp3RadioButton = new System.Windows.Forms.RadioButton();
			this.Disp2RadioButton = new System.Windows.Forms.RadioButton();
			this.disp1RadioButton = new System.Windows.Forms.RadioButton();
			this.closeButton = new System.Windows.Forms.Button();
			this.rightLineLabel = new System.Windows.Forms.Label();
			this.BottomPanel = new System.Windows.Forms.Panel();
			this.zoomLabel = new System.Windows.Forms.Label();
			this.nextPageButton = new System.Windows.Forms.Button();
			this.zoomTrackBar = new System.Windows.Forms.TrackBar();
			this.maxPageLabel = new System.Windows.Forms.Label();
			this.currentPageTextBox = new System.Windows.Forms.TextBox();
			this.prevPageButton = new System.Windows.Forms.Button();
			this.printPreviewVScrollBar = new System.Windows.Forms.VScrollBar();
			this.printPreviewControl = new CommonDialog.PrintPreview.PrintPreviewControl();
			this.LeftPanel.SuspendLayout();
			this.zoomGroupBox.SuspendLayout();
			this.dispGroupBox.SuspendLayout();
			this.BottomPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.printPreviewControl)).BeginInit();
			this.SuspendLayout();
			// 
			// printFlowLayoutPanel
			// 
			this.printFlowLayoutPanel.AutoSize = true;
			this.printFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.printFlowLayoutPanel.BackColor = System.Drawing.Color.White;
			this.printFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.printFlowLayoutPanel.Location = new System.Drawing.Point(433, 0);
			this.printFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.printFlowLayoutPanel.Name = "printFlowLayoutPanel";
			this.printFlowLayoutPanel.Size = new System.Drawing.Size(0, 0);
			this.printFlowLayoutPanel.TabIndex = 36;
			// 
			// LeftPanel
			// 
			this.LeftPanel.BackColor = System.Drawing.Color.White;
			this.LeftPanel.Controls.Add(this.zoomGroupBox);
			this.LeftPanel.Controls.Add(this.printButton);
			this.LeftPanel.Controls.Add(this.dispGroupBox);
			this.LeftPanel.Controls.Add(this.closeButton);
			this.LeftPanel.Controls.Add(this.rightLineLabel);
			this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.LeftPanel.Location = new System.Drawing.Point(0, 0);
			this.LeftPanel.MinimumSize = new System.Drawing.Size(0, 100);
			this.LeftPanel.Name = "LeftPanel";
			this.LeftPanel.Size = new System.Drawing.Size(120, 668);
			this.LeftPanel.TabIndex = 37;
			this.LeftPanel.Resize += new System.EventHandler(this.LeftPanel_Resize);
			// 
			// zoomGroupBox
			// 
			this.zoomGroupBox.Controls.Add(this.zoomAutoRadioButton);
			this.zoomGroupBox.Controls.Add(this.zoomManualRadioButton);
			this.zoomGroupBox.Controls.Add(this.zoom500RadioButton);
			this.zoomGroupBox.Controls.Add(this.zoom200RadioButton);
			this.zoomGroupBox.Controls.Add(this.zoom150RadioButton);
			this.zoomGroupBox.Controls.Add(this.zoom100RadioButton);
			this.zoomGroupBox.Controls.Add(this.zoom75RadioButton);
			this.zoomGroupBox.Controls.Add(this.zoom50RadioButton);
			this.zoomGroupBox.Controls.Add(this.zoom25RadioButton);
			this.zoomGroupBox.Controls.Add(this.zoom10RadioButton);
			this.zoomGroupBox.Location = new System.Drawing.Point(15, 240);
			this.zoomGroupBox.Margin = new System.Windows.Forms.Padding(0);
			this.zoomGroupBox.Name = "zoomGroupBox";
			this.zoomGroupBox.Size = new System.Drawing.Size(87, 64);
			this.zoomGroupBox.TabIndex = 37;
			this.zoomGroupBox.TabStop = false;
			this.zoomGroupBox.Text = "ズーム";
			// 
			// zoomAutoRadioButton
			// 
			this.zoomAutoRadioButton.AutoSize = true;
			this.zoomAutoRadioButton.Checked = true;
			this.zoomAutoRadioButton.Location = new System.Drawing.Point(19, 16);
			this.zoomAutoRadioButton.Name = "zoomAutoRadioButton";
			this.zoomAutoRadioButton.Size = new System.Drawing.Size(47, 16);
			this.zoomAutoRadioButton.TabIndex = 0;
			this.zoomAutoRadioButton.TabStop = true;
			this.zoomAutoRadioButton.Text = "自動";
			this.zoomAutoRadioButton.UseVisualStyleBackColor = true;
			this.zoomAutoRadioButton.CheckedChanged += new System.EventHandler(this.zoomRadioButton_CheckedChanged);
			// 
			// zoomManualRadioButton
			// 
			this.zoomManualRadioButton.AutoSize = true;
			this.zoomManualRadioButton.Location = new System.Drawing.Point(19, 38);
			this.zoomManualRadioButton.Name = "zoomManualRadioButton";
			this.zoomManualRadioButton.Size = new System.Drawing.Size(47, 16);
			this.zoomManualRadioButton.TabIndex = 1;
			this.zoomManualRadioButton.TabStop = true;
			this.zoomManualRadioButton.Text = "手動";
			this.zoomManualRadioButton.UseVisualStyleBackColor = true;
			this.zoomManualRadioButton.CheckedChanged += new System.EventHandler(this.zoomRadioButton_CheckedChanged);
			// 
			// zoom500RadioButton
			// 
			this.zoom500RadioButton.AutoSize = true;
			this.zoom500RadioButton.Location = new System.Drawing.Point(21, 60);
			this.zoom500RadioButton.Name = "zoom500RadioButton";
			this.zoom500RadioButton.Size = new System.Drawing.Size(47, 16);
			this.zoom500RadioButton.TabIndex = 2;
			this.zoom500RadioButton.TabStop = true;
			this.zoom500RadioButton.Tag = "500";
			this.zoom500RadioButton.Text = "500%";
			this.zoom500RadioButton.UseVisualStyleBackColor = true;
			this.zoom500RadioButton.Visible = false;
			this.zoom500RadioButton.CheckedChanged += new System.EventHandler(this.zoomRadioButton_CheckedChanged);
			// 
			// zoom200RadioButton
			// 
			this.zoom200RadioButton.AutoSize = true;
			this.zoom200RadioButton.Location = new System.Drawing.Point(21, 80);
			this.zoom200RadioButton.Name = "zoom200RadioButton";
			this.zoom200RadioButton.Size = new System.Drawing.Size(47, 16);
			this.zoom200RadioButton.TabIndex = 3;
			this.zoom200RadioButton.TabStop = true;
			this.zoom200RadioButton.Tag = "200";
			this.zoom200RadioButton.Text = "200%";
			this.zoom200RadioButton.UseVisualStyleBackColor = true;
			this.zoom200RadioButton.Visible = false;
			this.zoom200RadioButton.CheckedChanged += new System.EventHandler(this.zoomRadioButton_CheckedChanged);
			// 
			// zoom150RadioButton
			// 
			this.zoom150RadioButton.AutoSize = true;
			this.zoom150RadioButton.Location = new System.Drawing.Point(21, 100);
			this.zoom150RadioButton.Name = "zoom150RadioButton";
			this.zoom150RadioButton.Size = new System.Drawing.Size(47, 16);
			this.zoom150RadioButton.TabIndex = 4;
			this.zoom150RadioButton.TabStop = true;
			this.zoom150RadioButton.Tag = "150";
			this.zoom150RadioButton.Text = "150%";
			this.zoom150RadioButton.UseVisualStyleBackColor = true;
			this.zoom150RadioButton.Visible = false;
			this.zoom150RadioButton.CheckedChanged += new System.EventHandler(this.zoomRadioButton_CheckedChanged);
			// 
			// zoom100RadioButton
			// 
			this.zoom100RadioButton.AutoSize = true;
			this.zoom100RadioButton.Location = new System.Drawing.Point(21, 120);
			this.zoom100RadioButton.Name = "zoom100RadioButton";
			this.zoom100RadioButton.Size = new System.Drawing.Size(47, 16);
			this.zoom100RadioButton.TabIndex = 5;
			this.zoom100RadioButton.TabStop = true;
			this.zoom100RadioButton.Tag = "100";
			this.zoom100RadioButton.Text = "100%";
			this.zoom100RadioButton.UseVisualStyleBackColor = true;
			this.zoom100RadioButton.Visible = false;
			this.zoom100RadioButton.CheckedChanged += new System.EventHandler(this.zoomRadioButton_CheckedChanged);
			// 
			// zoom75RadioButton
			// 
			this.zoom75RadioButton.AutoSize = true;
			this.zoom75RadioButton.Location = new System.Drawing.Point(21, 140);
			this.zoom75RadioButton.Name = "zoom75RadioButton";
			this.zoom75RadioButton.Size = new System.Drawing.Size(41, 16);
			this.zoom75RadioButton.TabIndex = 6;
			this.zoom75RadioButton.TabStop = true;
			this.zoom75RadioButton.Tag = "75";
			this.zoom75RadioButton.Text = "75%";
			this.zoom75RadioButton.UseVisualStyleBackColor = true;
			this.zoom75RadioButton.Visible = false;
			this.zoom75RadioButton.CheckedChanged += new System.EventHandler(this.zoomRadioButton_CheckedChanged);
			// 
			// zoom50RadioButton
			// 
			this.zoom50RadioButton.AutoSize = true;
			this.zoom50RadioButton.Location = new System.Drawing.Point(21, 160);
			this.zoom50RadioButton.Name = "zoom50RadioButton";
			this.zoom50RadioButton.Size = new System.Drawing.Size(41, 16);
			this.zoom50RadioButton.TabIndex = 7;
			this.zoom50RadioButton.TabStop = true;
			this.zoom50RadioButton.Tag = "50";
			this.zoom50RadioButton.Text = "50%";
			this.zoom50RadioButton.UseVisualStyleBackColor = true;
			this.zoom50RadioButton.Visible = false;
			this.zoom50RadioButton.CheckedChanged += new System.EventHandler(this.zoomRadioButton_CheckedChanged);
			// 
			// zoom25RadioButton
			// 
			this.zoom25RadioButton.AutoSize = true;
			this.zoom25RadioButton.Location = new System.Drawing.Point(21, 180);
			this.zoom25RadioButton.Name = "zoom25RadioButton";
			this.zoom25RadioButton.Size = new System.Drawing.Size(41, 16);
			this.zoom25RadioButton.TabIndex = 8;
			this.zoom25RadioButton.TabStop = true;
			this.zoom25RadioButton.Tag = "25";
			this.zoom25RadioButton.Text = "25%";
			this.zoom25RadioButton.UseVisualStyleBackColor = true;
			this.zoom25RadioButton.Visible = false;
			this.zoom25RadioButton.CheckedChanged += new System.EventHandler(this.zoomRadioButton_CheckedChanged);
			// 
			// zoom10RadioButton
			// 
			this.zoom10RadioButton.AutoSize = true;
			this.zoom10RadioButton.Location = new System.Drawing.Point(21, 200);
			this.zoom10RadioButton.Name = "zoom10RadioButton";
			this.zoom10RadioButton.Size = new System.Drawing.Size(41, 16);
			this.zoom10RadioButton.TabIndex = 9;
			this.zoom10RadioButton.TabStop = true;
			this.zoom10RadioButton.Tag = "10";
			this.zoom10RadioButton.Text = "10%";
			this.zoom10RadioButton.UseVisualStyleBackColor = true;
			this.zoom10RadioButton.Visible = false;
			this.zoom10RadioButton.CheckedChanged += new System.EventHandler(this.zoomRadioButton_CheckedChanged);
			// 
			// printButton
			// 
			this.printButton.BackColor = System.Drawing.Color.GhostWhite;
			this.printButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.printButton.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.printButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.printButton.Image = global::CommonDialog.PrintPreview.Properties.Resources.Print;
			this.printButton.Location = new System.Drawing.Point(15, 15);
			this.printButton.Margin = new System.Windows.Forms.Padding(4);
			this.printButton.Name = "printButton";
			this.printButton.Size = new System.Drawing.Size(87, 87);
			this.printButton.TabIndex = 33;
			this.printButton.Text = "印刷";
			this.printButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.printButton.UseVisualStyleBackColor = true;
			this.printButton.EnabledChanged += new System.EventHandler(this.printButton_EnabledChanged);
			this.printButton.Click += new System.EventHandler(this.printButton_Click);
			// 
			// dispGroupBox
			// 
			this.dispGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.dispGroupBox.Controls.Add(this.Disp6RadioButton);
			this.dispGroupBox.Controls.Add(this.Disp4RadioButton);
			this.dispGroupBox.Controls.Add(this.Disp3RadioButton);
			this.dispGroupBox.Controls.Add(this.Disp2RadioButton);
			this.dispGroupBox.Controls.Add(this.disp1RadioButton);
			this.dispGroupBox.Location = new System.Drawing.Point(15, 111);
			this.dispGroupBox.Margin = new System.Windows.Forms.Padding(4);
			this.dispGroupBox.Name = "dispGroupBox";
			this.dispGroupBox.Size = new System.Drawing.Size(87, 125);
			this.dispGroupBox.TabIndex = 35;
			this.dispGroupBox.TabStop = false;
			this.dispGroupBox.Text = "表示ページ数";
			// 
			// Disp6RadioButton
			// 
			this.Disp6RadioButton.AutoSize = true;
			this.Disp6RadioButton.Location = new System.Drawing.Point(16, 98);
			this.Disp6RadioButton.Name = "Disp6RadioButton";
			this.Disp6RadioButton.Size = new System.Drawing.Size(59, 16);
			this.Disp6RadioButton.TabIndex = 5;
			this.Disp6RadioButton.TabStop = true;
			this.Disp6RadioButton.Tag = "2,3";
			this.Disp6RadioButton.Text = "6ページ";
			this.Disp6RadioButton.UseVisualStyleBackColor = true;
			this.Disp6RadioButton.CheckedChanged += new System.EventHandler(this.dispRadioButton_CheckedChanged);
			// 
			// Disp4RadioButton
			// 
			this.Disp4RadioButton.AutoSize = true;
			this.Disp4RadioButton.Location = new System.Drawing.Point(16, 78);
			this.Disp4RadioButton.Name = "Disp4RadioButton";
			this.Disp4RadioButton.Size = new System.Drawing.Size(59, 16);
			this.Disp4RadioButton.TabIndex = 4;
			this.Disp4RadioButton.TabStop = true;
			this.Disp4RadioButton.Tag = "2,2";
			this.Disp4RadioButton.Text = "4ページ";
			this.Disp4RadioButton.UseVisualStyleBackColor = true;
			this.Disp4RadioButton.CheckedChanged += new System.EventHandler(this.dispRadioButton_CheckedChanged);
			// 
			// Disp3RadioButton
			// 
			this.Disp3RadioButton.AutoSize = true;
			this.Disp3RadioButton.Location = new System.Drawing.Point(16, 58);
			this.Disp3RadioButton.Name = "Disp3RadioButton";
			this.Disp3RadioButton.Size = new System.Drawing.Size(59, 16);
			this.Disp3RadioButton.TabIndex = 3;
			this.Disp3RadioButton.TabStop = true;
			this.Disp3RadioButton.Tag = "1,3";
			this.Disp3RadioButton.Text = "3ページ";
			this.Disp3RadioButton.UseVisualStyleBackColor = true;
			this.Disp3RadioButton.CheckedChanged += new System.EventHandler(this.dispRadioButton_CheckedChanged);
			// 
			// Disp2RadioButton
			// 
			this.Disp2RadioButton.AutoSize = true;
			this.Disp2RadioButton.Location = new System.Drawing.Point(16, 38);
			this.Disp2RadioButton.Name = "Disp2RadioButton";
			this.Disp2RadioButton.Size = new System.Drawing.Size(59, 16);
			this.Disp2RadioButton.TabIndex = 1;
			this.Disp2RadioButton.TabStop = true;
			this.Disp2RadioButton.Tag = "1,2";
			this.Disp2RadioButton.Text = "2ページ";
			this.Disp2RadioButton.UseVisualStyleBackColor = true;
			this.Disp2RadioButton.CheckedChanged += new System.EventHandler(this.dispRadioButton_CheckedChanged);
			// 
			// disp1RadioButton
			// 
			this.disp1RadioButton.AutoSize = true;
			this.disp1RadioButton.Checked = true;
			this.disp1RadioButton.Location = new System.Drawing.Point(16, 18);
			this.disp1RadioButton.Name = "disp1RadioButton";
			this.disp1RadioButton.Size = new System.Drawing.Size(59, 16);
			this.disp1RadioButton.TabIndex = 0;
			this.disp1RadioButton.TabStop = true;
			this.disp1RadioButton.Tag = "1,1";
			this.disp1RadioButton.Text = "1ページ";
			this.disp1RadioButton.UseVisualStyleBackColor = true;
			this.disp1RadioButton.CheckedChanged += new System.EventHandler(this.dispRadioButton_CheckedChanged);
			// 
			// closeButton
			// 
			this.closeButton.BackColor = System.Drawing.Color.GhostWhite;
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.Image = global::CommonDialog.PrintPreview.Properties.Resources.Close;
			this.closeButton.Location = new System.Drawing.Point(15, 316);
			this.closeButton.Margin = new System.Windows.Forms.Padding(4, 32, 4, 4);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(87, 41);
			this.closeButton.TabIndex = 36;
			this.closeButton.Text = "閉じる";
			this.closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.closeButton.UseVisualStyleBackColor = true;
			// 
			// rightLineLabel
			// 
			this.rightLineLabel.BackColor = System.Drawing.Color.LightGray;
			this.rightLineLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rightLineLabel.Location = new System.Drawing.Point(117, 0);
			this.rightLineLabel.Margin = new System.Windows.Forms.Padding(0);
			this.rightLineLabel.Name = "rightLineLabel";
			this.rightLineLabel.Size = new System.Drawing.Size(1, 668);
			this.rightLineLabel.TabIndex = 32;
			// 
			// BottomPanel
			// 
			this.BottomPanel.BackColor = System.Drawing.Color.White;
			this.BottomPanel.Controls.Add(this.zoomLabel);
			this.BottomPanel.Controls.Add(this.nextPageButton);
			this.BottomPanel.Controls.Add(this.zoomTrackBar);
			this.BottomPanel.Controls.Add(this.maxPageLabel);
			this.BottomPanel.Controls.Add(this.currentPageTextBox);
			this.BottomPanel.Controls.Add(this.prevPageButton);
			this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomPanel.Location = new System.Drawing.Point(120, 639);
			this.BottomPanel.Name = "BottomPanel";
			this.BottomPanel.Size = new System.Drawing.Size(840, 29);
			this.BottomPanel.TabIndex = 41;
			// 
			// zoomLabel
			// 
			this.zoomLabel.AutoSize = true;
			this.zoomLabel.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.zoomLabel.Location = new System.Drawing.Point(625, 6);
			this.zoomLabel.Margin = new System.Windows.Forms.Padding(0);
			this.zoomLabel.Name = "zoomLabel";
			this.zoomLabel.Size = new System.Drawing.Size(35, 14);
			this.zoomLabel.TabIndex = 43;
			this.zoomLabel.Text = "自動";
			// 
			// nextPageButton
			// 
			this.nextPageButton.BackColor = System.Drawing.Color.GhostWhite;
			this.nextPageButton.Location = new System.Drawing.Point(170, 4);
			this.nextPageButton.Name = "nextPageButton";
			this.nextPageButton.Size = new System.Drawing.Size(35, 23);
			this.nextPageButton.TabIndex = 34;
			this.nextPageButton.Text = ">>";
			this.nextPageButton.UseVisualStyleBackColor = false;
			this.nextPageButton.Click += new System.EventHandler(this.nextPageButton_Click);
			// 
			// zoomTrackBar
			// 
			this.zoomTrackBar.BackColor = System.Drawing.Color.White;
			this.zoomTrackBar.Location = new System.Drawing.Point(653, 0);
			this.zoomTrackBar.Margin = new System.Windows.Forms.Padding(0);
			this.zoomTrackBar.Maximum = 500;
			this.zoomTrackBar.Minimum = 10;
			this.zoomTrackBar.Name = "zoomTrackBar";
			this.zoomTrackBar.Size = new System.Drawing.Size(169, 45);
			this.zoomTrackBar.TabIndex = 42;
			this.zoomTrackBar.TickFrequency = 0;
			this.zoomTrackBar.Value = 75;
			this.zoomTrackBar.Scroll += new System.EventHandler(this.ZoomTrackBar_Scroll);
			// 
			// maxPageLabel
			// 
			this.maxPageLabel.AutoSize = true;
			this.maxPageLabel.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.maxPageLabel.Location = new System.Drawing.Point(103, 8);
			this.maxPageLabel.Name = "maxPageLabel";
			this.maxPageLabel.Size = new System.Drawing.Size(59, 14);
			this.maxPageLabel.TabIndex = 33;
			this.maxPageLabel.Text = "1/ ページ";
			// 
			// currentPageTextBox
			// 
			this.currentPageTextBox.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.currentPageTextBox.Location = new System.Drawing.Point(73, 5);
			this.currentPageTextBox.Name = "currentPageTextBox";
			this.currentPageTextBox.Size = new System.Drawing.Size(30, 21);
			this.currentPageTextBox.TabIndex = 32;
			this.currentPageTextBox.Text = "1";
			this.currentPageTextBox.TextChanged += new System.EventHandler(this.currentPageTextBox_TextChanged);
			this.currentPageTextBox.Leave += new System.EventHandler(this.currentPageTextBox_Leave);
			// 
			// prevPageButton
			// 
			this.prevPageButton.BackColor = System.Drawing.Color.GhostWhite;
			this.prevPageButton.Enabled = false;
			this.prevPageButton.Location = new System.Drawing.Point(28, 4);
			this.prevPageButton.Name = "prevPageButton";
			this.prevPageButton.Size = new System.Drawing.Size(35, 23);
			this.prevPageButton.TabIndex = 31;
			this.prevPageButton.Text = "<<";
			this.prevPageButton.UseVisualStyleBackColor = false;
			this.prevPageButton.Click += new System.EventHandler(this.prevPageButton_Click);
			// 
			// printPreviewVScrollBar
			// 
			this.printPreviewVScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
			this.printPreviewVScrollBar.Location = new System.Drawing.Point(942, 0);
			this.printPreviewVScrollBar.Name = "printPreviewVScrollBar";
			this.printPreviewVScrollBar.Size = new System.Drawing.Size(18, 639);
			this.printPreviewVScrollBar.TabIndex = 42;
			this.printPreviewVScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.printPreviewVScrollBar_Scroll);
			// 
			// printPreviewControl
			// 
			this.printPreviewControl.AutoZoom = true;
			this.printPreviewControl.BackColor = System.Drawing.Color.White;
			this.printPreviewControl.Columns = 1;
			this.printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.printPreviewControl.Document = null;
			this.printPreviewControl.GradePerZooms = 4;
			this.printPreviewControl.Location = new System.Drawing.Point(120, 0);
			this.printPreviewControl.Margin = new System.Windows.Forms.Padding(0);
			this.printPreviewControl.Name = "printPreviewControl";
			this.printPreviewControl.Rows = 1;
			this.printPreviewControl.Size = new System.Drawing.Size(822, 639);
			this.printPreviewControl.StartPage = 0;
			this.printPreviewControl.TabIndex = 43;
			this.printPreviewControl.TabStop = false;
			this.printPreviewControl.Zoom = 1D;
			this.printPreviewControl.Zooms = new double[] {
        0.1D,
        0.25D,
        0.5D,
        0.75D,
        1D,
        1.5D,
        2D,
        5D};
			this.printPreviewControl.StartPageChanged += new System.EventHandler(this.printPreviewControl_StartPageChanged);
			this.printPreviewControl.ZoomsChanged += new System.EventHandler(this.printPreviewControl_ZoomsChanged);
			this.printPreviewControl.AutoZoomChanged += new System.EventHandler(this.printPreviewControl_AutoZoomChanged);
			this.printPreviewControl.Resize += new System.EventHandler(this.printPreviewControl_Resize);
			// 
			// PrintPreviewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(960, 668);
			this.Controls.Add(this.printPreviewControl);
			this.Controls.Add(this.printPreviewVScrollBar);
			this.Controls.Add(this.BottomPanel);
			this.Controls.Add(this.LeftPanel);
			this.Controls.Add(this.printFlowLayoutPanel);
			this.DoubleBuffered = true;
			this.MinimumSize = new System.Drawing.Size(825, 150);
			this.Name = "PrintPreviewForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "印刷プレビュー";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PalettePrintPreviewForm_FormClosed);
			this.Load += new System.EventHandler(this.PrintPreviewForm_Load);
			this.LeftPanel.ResumeLayout(false);
			this.zoomGroupBox.ResumeLayout(false);
			this.zoomGroupBox.PerformLayout();
			this.dispGroupBox.ResumeLayout(false);
			this.dispGroupBox.PerformLayout();
			this.BottomPanel.ResumeLayout(false);
			this.BottomPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.zoomTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.printPreviewControl)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip hiddenToolTip;
        private System.Windows.Forms.FlowLayoutPanel printFlowLayoutPanel;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.GroupBox dispGroupBox;
        private System.Windows.Forms.RadioButton Disp6RadioButton;
        private System.Windows.Forms.RadioButton Disp4RadioButton;
        private System.Windows.Forms.RadioButton Disp3RadioButton;
        private System.Windows.Forms.RadioButton Disp2RadioButton;
        private System.Windows.Forms.RadioButton disp1RadioButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label rightLineLabel;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Label zoomLabel;
        private System.Windows.Forms.Button nextPageButton;
        private System.Windows.Forms.TrackBar zoomTrackBar;
        private System.Windows.Forms.Label maxPageLabel;
        private System.Windows.Forms.TextBox currentPageTextBox;
        private System.Windows.Forms.Button prevPageButton;
        private System.Windows.Forms.VScrollBar printPreviewVScrollBar;
        private PrintPreviewControl printPreviewControl;
        private System.Windows.Forms.GroupBox zoomGroupBox;
        private System.Windows.Forms.RadioButton zoomAutoRadioButton;
        private System.Windows.Forms.RadioButton zoomManualRadioButton;
        private System.Windows.Forms.RadioButton zoom500RadioButton;
        private System.Windows.Forms.RadioButton zoom200RadioButton;
        private System.Windows.Forms.RadioButton zoom150RadioButton;
        private System.Windows.Forms.RadioButton zoom100RadioButton;
        private System.Windows.Forms.RadioButton zoom75RadioButton;
        private System.Windows.Forms.RadioButton zoom50RadioButton;
        private System.Windows.Forms.RadioButton zoom25RadioButton;
        private System.Windows.Forms.RadioButton zoom10RadioButton;
    }
}