namespace ArthiPOS.Rent.CashFlow
{
    partial class FrmDashboard1
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
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblDateRangeLabel = new System.Windows.Forms.Label();
            this.cmbDateRange = new System.Windows.Forms.ComboBox();
            this.lblCustom = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlSummaryCards = new System.Windows.Forms.Panel();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.cardCashIn = new System.Windows.Forms.Panel();
            this.lblCashInTitle = new System.Windows.Forms.Label();
            this.lblCashInTotal = new System.Windows.Forms.Label();
            this.cardCashOut = new System.Windows.Forms.Panel();
            this.lblCashOutTitle = new System.Windows.Forms.Label();
            this.lblCashOutTotal = new System.Windows.Forms.Label();
            this.cardNetFlow = new System.Windows.Forms.Panel();
            this.lblNetFlowTitle = new System.Windows.Forms.Label();
            this.lblNetCashFlow = new System.Windows.Forms.Label();
            this.cardCashInCount = new System.Windows.Forms.Panel();
            this.lblCashInCountTitle = new System.Windows.Forms.Label();
            this.lblCashInCount = new System.Windows.Forms.Label();
            this.cardCashOutCount = new System.Windows.Forms.Panel();
            this.lblCashOutCountTitle = new System.Windows.Forms.Label();
            this.lblCashOutCount = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.tvYearlyBreakdown = new System.Windows.Forms.TreeView();
            this.lblBreakdown = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.pnlTransactionToolbar = new System.Windows.Forms.Panel();
            this.btnExportTransactions = new System.Windows.Forms.Button();
            this.btnCopyTransactions = new System.Windows.Forms.Button();
            this.lblTransactions = new System.Windows.Forms.Label();
            this.pnlTitle.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlSummaryCards.SuspendLayout();
            this.cardCashIn.SuspendLayout();
            this.cardCashOut.SuspendLayout();
            this.cardNetFlow.SuspendLayout();
            this.cardCashInCount.SuspendLayout();
            this.cardCashOutCount.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.pnlTransactionToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(66)))), ((int)(((byte)(91)))));
            this.pnlTitle.Controls.Add(this.lblSubTitle);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1174, 52);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblSubTitle.ForeColor = System.Drawing.Color.LightGray;
            this.lblSubTitle.Location = new System.Drawing.Point(369, 22);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(257, 17);
            this.lblSubTitle.TabIndex = 0;
            this.lblSubTitle.Text = "Income (Rent/Commission) vs Expenses";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(17, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(343, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "💰 CASH FLOW DASHBOARD";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlFilter.Controls.Add(this.cmbDateRange);
            this.pnlFilter.Controls.Add(this.lblDateRangeLabel);
            this.pnlFilter.Controls.Add(this.lblCustom);
            this.pnlFilter.Controls.Add(this.dtpStartDate);
            this.pnlFilter.Controls.Add(this.lblTo);
            this.pnlFilter.Controls.Add(this.dtpEndDate);
            this.pnlFilter.Controls.Add(this.btnApplyFilter);
            this.pnlFilter.Controls.Add(this.btnRefresh);
            this.pnlFilter.Controls.Add(this.btnPrint);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 52);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(9);
            this.pnlFilter.Size = new System.Drawing.Size(1174, 69);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblDateRangeLabel
            // 
            this.lblDateRangeLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDateRangeLabel.Location = new System.Drawing.Point(17, 17);
            this.lblDateRangeLabel.Name = "lblDateRangeLabel";
            this.lblDateRangeLabel.Size = new System.Drawing.Size(69, 22);
            this.lblDateRangeLabel.TabIndex = 0;
            this.lblDateRangeLabel.Text = "Date Range:";
            // 
            // cmbDateRange
            // 
            this.cmbDateRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateRange.DropDownWidth = 200;
            this.cmbDateRange.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbDateRange.Location = new System.Drawing.Point(70, 13);
            this.cmbDateRange.Name = "cmbDateRange";
            this.cmbDateRange.Size = new System.Drawing.Size(149, 23);
            this.cmbDateRange.TabIndex = 0;
            this.cmbDateRange.SelectedIndexChanged += new System.EventHandler(this.CmbDateRange_SelectedIndexChanged);
            // 
            // lblCustom
            // 
            this.lblCustom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustom.Location = new System.Drawing.Point(231, 17);
            this.lblCustom.Name = "lblCustom";
            this.lblCustom.Size = new System.Drawing.Size(51, 22);
            this.lblCustom.TabIndex = 1;
            this.lblCustom.Text = "Custom:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(287, 13);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(103, 20);
            this.dtpStartDate.TabIndex = 1;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(394, 17);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(26, 17);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "to";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(420, 13);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(103, 20);
            this.dtpEndDate.TabIndex = 2;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.BackColor = System.Drawing.Color.SteelBlue;
            this.btnApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyFilter.ForeColor = System.Drawing.Color.White;
            this.btnApplyFilter.Location = new System.Drawing.Point(540, 13);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(86, 24);
            this.btnApplyFilter.TabIndex = 3;
            this.btnApplyFilter.Text = "Apply Filter";
            this.btnApplyFilter.UseVisualStyleBackColor = false;
            this.btnApplyFilter.Click += new System.EventHandler(this.BtnApplyFilter_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.LightGray;
            this.btnRefresh.Location = new System.Drawing.Point(634, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(69, 24);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "⟳ Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.LightBlue;
            this.btnPrint.Location = new System.Drawing.Point(711, 13);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(69, 24);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "🖨️ Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // pnlSummaryCards
            // 
            this.pnlSummaryCards.BackColor = System.Drawing.Color.White;
            this.pnlSummaryCards.Controls.Add(this.lblPeriod);
            this.pnlSummaryCards.Controls.Add(this.cardCashIn);
            this.pnlSummaryCards.Controls.Add(this.cardCashOut);
            this.pnlSummaryCards.Controls.Add(this.cardNetFlow);
            this.pnlSummaryCards.Controls.Add(this.cardCashInCount);
            this.pnlSummaryCards.Controls.Add(this.cardCashOutCount);
            this.pnlSummaryCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummaryCards.Location = new System.Drawing.Point(0, 121);
            this.pnlSummaryCards.Name = "pnlSummaryCards";
            this.pnlSummaryCards.Padding = new System.Windows.Forms.Padding(9);
            this.pnlSummaryCards.Size = new System.Drawing.Size(1174, 104);
            this.pnlSummaryCards.TabIndex = 2;
            // 
            // lblPeriod
            // 
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblPeriod.ForeColor = System.Drawing.Color.Gray;
            this.lblPeriod.Location = new System.Drawing.Point(17, 4);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(343, 17);
            this.lblPeriod.TabIndex = 0;
            this.lblPeriod.Text = "Period: --";
            // 
            // cardCashIn
            // 
            this.cardCashIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.cardCashIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardCashIn.Controls.Add(this.lblCashInTitle);
            this.cardCashIn.Controls.Add(this.lblCashInTotal);
            this.cardCashIn.Location = new System.Drawing.Point(17, 30);
            this.cardCashIn.Name = "cardCashIn";
            this.cardCashIn.Size = new System.Drawing.Size(172, 61);
            this.cardCashIn.TabIndex = 1;
            // 
            // lblCashInTitle
            // 
            this.lblCashInTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCashInTitle.ForeColor = System.Drawing.Color.White;
            this.lblCashInTitle.Location = new System.Drawing.Point(9, 7);
            this.lblCashInTitle.Name = "lblCashInTitle";
            this.lblCashInTitle.Size = new System.Drawing.Size(154, 17);
            this.lblCashInTitle.TabIndex = 0;
            this.lblCashInTitle.Text = "💰 Total Cash In";
            this.lblCashInTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCashInTotal
            // 
            this.lblCashInTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCashInTotal.ForeColor = System.Drawing.Color.White;
            this.lblCashInTotal.Location = new System.Drawing.Point(9, 26);
            this.lblCashInTotal.Name = "lblCashInTotal";
            this.lblCashInTotal.Size = new System.Drawing.Size(154, 26);
            this.lblCashInTotal.TabIndex = 1;
            this.lblCashInTotal.Text = "₹0";
            this.lblCashInTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardCashOut
            // 
            this.cardCashOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.cardCashOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardCashOut.Controls.Add(this.lblCashOutTitle);
            this.cardCashOut.Controls.Add(this.lblCashOutTotal);
            this.cardCashOut.Location = new System.Drawing.Point(201, 30);
            this.cardCashOut.Name = "cardCashOut";
            this.cardCashOut.Size = new System.Drawing.Size(172, 61);
            this.cardCashOut.TabIndex = 2;
            // 
            // lblCashOutTitle
            // 
            this.lblCashOutTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCashOutTitle.ForeColor = System.Drawing.Color.White;
            this.lblCashOutTitle.Location = new System.Drawing.Point(9, 7);
            this.lblCashOutTitle.Name = "lblCashOutTitle";
            this.lblCashOutTitle.Size = new System.Drawing.Size(154, 17);
            this.lblCashOutTitle.TabIndex = 0;
            this.lblCashOutTitle.Text = "💸 Total Cash Out";
            this.lblCashOutTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCashOutTotal
            // 
            this.lblCashOutTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCashOutTotal.ForeColor = System.Drawing.Color.White;
            this.lblCashOutTotal.Location = new System.Drawing.Point(9, 26);
            this.lblCashOutTotal.Name = "lblCashOutTotal";
            this.lblCashOutTotal.Size = new System.Drawing.Size(154, 26);
            this.lblCashOutTotal.TabIndex = 1;
            this.lblCashOutTotal.Text = "₹0";
            this.lblCashOutTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardNetFlow
            // 
            this.cardNetFlow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.cardNetFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardNetFlow.Controls.Add(this.lblNetFlowTitle);
            this.cardNetFlow.Controls.Add(this.lblNetCashFlow);
            this.cardNetFlow.Location = new System.Drawing.Point(386, 30);
            this.cardNetFlow.Name = "cardNetFlow";
            this.cardNetFlow.Size = new System.Drawing.Size(172, 61);
            this.cardNetFlow.TabIndex = 3;
            // 
            // lblNetFlowTitle
            // 
            this.lblNetFlowTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblNetFlowTitle.ForeColor = System.Drawing.Color.White;
            this.lblNetFlowTitle.Location = new System.Drawing.Point(9, 7);
            this.lblNetFlowTitle.Name = "lblNetFlowTitle";
            this.lblNetFlowTitle.Size = new System.Drawing.Size(154, 17);
            this.lblNetFlowTitle.TabIndex = 0;
            this.lblNetFlowTitle.Text = "📊 Net Cash Flow";
            this.lblNetFlowTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNetCashFlow
            // 
            this.lblNetCashFlow.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblNetCashFlow.ForeColor = System.Drawing.Color.White;
            this.lblNetCashFlow.Location = new System.Drawing.Point(9, 26);
            this.lblNetCashFlow.Name = "lblNetCashFlow";
            this.lblNetCashFlow.Size = new System.Drawing.Size(154, 26);
            this.lblNetCashFlow.TabIndex = 1;
            this.lblNetCashFlow.Text = "₹0";
            this.lblNetCashFlow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardCashInCount
            // 
            this.cardCashInCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.cardCashInCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardCashInCount.Controls.Add(this.lblCashInCountTitle);
            this.cardCashInCount.Controls.Add(this.lblCashInCount);
            this.cardCashInCount.Location = new System.Drawing.Point(570, 30);
            this.cardCashInCount.Name = "cardCashInCount";
            this.cardCashInCount.Size = new System.Drawing.Size(172, 61);
            this.cardCashInCount.TabIndex = 4;
            // 
            // lblCashInCountTitle
            // 
            this.lblCashInCountTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCashInCountTitle.ForeColor = System.Drawing.Color.White;
            this.lblCashInCountTitle.Location = new System.Drawing.Point(9, 7);
            this.lblCashInCountTitle.Name = "lblCashInCountTitle";
            this.lblCashInCountTitle.Size = new System.Drawing.Size(154, 17);
            this.lblCashInCountTitle.TabIndex = 0;
            this.lblCashInCountTitle.Text = "📋 Cash In Txns";
            this.lblCashInCountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCashInCount
            // 
            this.lblCashInCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCashInCount.ForeColor = System.Drawing.Color.White;
            this.lblCashInCount.Location = new System.Drawing.Point(9, 26);
            this.lblCashInCount.Name = "lblCashInCount";
            this.lblCashInCount.Size = new System.Drawing.Size(154, 26);
            this.lblCashInCount.TabIndex = 1;
            this.lblCashInCount.Text = "0";
            this.lblCashInCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cardCashOutCount
            // 
            this.cardCashOutCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.cardCashOutCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardCashOutCount.Controls.Add(this.lblCashOutCountTitle);
            this.cardCashOutCount.Controls.Add(this.lblCashOutCount);
            this.cardCashOutCount.Location = new System.Drawing.Point(754, 30);
            this.cardCashOutCount.Name = "cardCashOutCount";
            this.cardCashOutCount.Size = new System.Drawing.Size(172, 61);
            this.cardCashOutCount.TabIndex = 5;
            // 
            // lblCashOutCountTitle
            // 
            this.lblCashOutCountTitle.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblCashOutCountTitle.ForeColor = System.Drawing.Color.White;
            this.lblCashOutCountTitle.Location = new System.Drawing.Point(9, 7);
            this.lblCashOutCountTitle.Name = "lblCashOutCountTitle";
            this.lblCashOutCountTitle.Size = new System.Drawing.Size(154, 17);
            this.lblCashOutCountTitle.TabIndex = 0;
            this.lblCashOutCountTitle.Text = "📋 Cash Out Txns";
            this.lblCashOutCountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCashOutCount
            // 
            this.lblCashOutCount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblCashOutCount.ForeColor = System.Drawing.Color.White;
            this.lblCashOutCount.Location = new System.Drawing.Point(9, 26);
            this.lblCashOutCount.Name = "lblCashOutCount";
            this.lblCashOutCount.Size = new System.Drawing.Size(154, 26);
            this.lblCashOutCount.TabIndex = 1;
            this.lblCashOutCount.Text = "0";
            this.lblCashOutCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlContent.Controls.Add(this.splitContainer);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 225);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(9);
            this.pnlContent.Size = new System.Drawing.Size(1174, 424);
            this.pnlContent.TabIndex = 3;
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(9, 9);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.pnlLeft);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlRight);
            this.splitContainer.Size = new System.Drawing.Size(1156, 406);
            this.splitContainer.SplitterDistance = 385;
            this.splitContainer.SplitterWidth = 3;
            this.splitContainer.TabIndex = 1;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlLeft.Controls.Add(this.tvYearlyBreakdown);
            this.pnlLeft.Controls.Add(this.lblBreakdown);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(9);
            this.pnlLeft.Size = new System.Drawing.Size(381, 402);
            this.pnlLeft.TabIndex = 0;
            // 
            // tvYearlyBreakdown
            // 
            this.tvYearlyBreakdown.BackColor = System.Drawing.Color.White;
            this.tvYearlyBreakdown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvYearlyBreakdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvYearlyBreakdown.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tvYearlyBreakdown.HideSelection = false;
            this.tvYearlyBreakdown.Location = new System.Drawing.Point(9, 35);
            this.tvYearlyBreakdown.Name = "tvYearlyBreakdown";
            this.tvYearlyBreakdown.ShowNodeToolTips = true;
            this.tvYearlyBreakdown.Size = new System.Drawing.Size(363, 358);
            this.tvYearlyBreakdown.TabIndex = 0;
            this.tvYearlyBreakdown.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvYearlyBreakdown_AfterSelect);
            // 
            // lblBreakdown
            // 
            this.lblBreakdown.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBreakdown.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBreakdown.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblBreakdown.Location = new System.Drawing.Point(9, 9);
            this.lblBreakdown.Name = "lblBreakdown";
            this.lblBreakdown.Size = new System.Drawing.Size(363, 26);
            this.lblBreakdown.TabIndex = 1;
            this.lblBreakdown.Text = "📅 Yearly & Monthly Breakdown";
            this.lblBreakdown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlRight.Controls.Add(this.dgvTransactions);
            this.pnlRight.Controls.Add(this.pnlTransactionToolbar);
            this.pnlRight.Controls.Add(this.lblTransactions);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(9);
            this.pnlRight.Size = new System.Drawing.Size(764, 402);
            this.pnlRight.TabIndex = 0;
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransactions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransactions.Location = new System.Drawing.Point(9, 65);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowHeadersVisible = false;
            this.dgvTransactions.Size = new System.Drawing.Size(746, 328);
            this.dgvTransactions.TabIndex = 0;
            // 
            // pnlTransactionToolbar
            // 
            this.pnlTransactionToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlTransactionToolbar.Controls.Add(this.btnExportTransactions);
            this.pnlTransactionToolbar.Controls.Add(this.btnCopyTransactions);
            this.pnlTransactionToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTransactionToolbar.Location = new System.Drawing.Point(9, 35);
            this.pnlTransactionToolbar.Name = "pnlTransactionToolbar";
            this.pnlTransactionToolbar.Size = new System.Drawing.Size(746, 30);
            this.pnlTransactionToolbar.TabIndex = 1;
            // 
            // btnExportTransactions
            // 
            this.btnExportTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportTransactions.Location = new System.Drawing.Point(4, 4);
            this.btnExportTransactions.Name = "btnExportTransactions";
            this.btnExportTransactions.Size = new System.Drawing.Size(69, 22);
            this.btnExportTransactions.TabIndex = 0;
            this.btnExportTransactions.Text = "📤 Export";
            // 
            // btnCopyTransactions
            // 
            this.btnCopyTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyTransactions.Location = new System.Drawing.Point(77, 4);
            this.btnCopyTransactions.Name = "btnCopyTransactions";
            this.btnCopyTransactions.Size = new System.Drawing.Size(69, 22);
            this.btnCopyTransactions.TabIndex = 1;
            this.btnCopyTransactions.Text = "📋 Copy";
            // 
            // lblTransactions
            // 
            this.lblTransactions.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTransactions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTransactions.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTransactions.Location = new System.Drawing.Point(9, 9);
            this.lblTransactions.Name = "lblTransactions";
            this.lblTransactions.Size = new System.Drawing.Size(746, 26);
            this.lblTransactions.TabIndex = 2;
            this.lblTransactions.Text = "📋 Transaction Details";
            this.lblTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmDashboard1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1174, 649);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSummaryCards);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlTitle);
            this.Name = "FrmDashboard1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Financial Dashboard - Cash Flow Analysis";
            this.pnlTitle.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlSummaryCards.ResumeLayout(false);
            this.cardCashIn.ResumeLayout(false);
            this.cardCashOut.ResumeLayout(false);
            this.cardNetFlow.ResumeLayout(false);
            this.cardCashInCount.ResumeLayout(false);
            this.cardCashOutCount.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.pnlTransactionToolbar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        // ── Field declarations (replaces the inline field declarations in FrmDashboard.cs) ──
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;

        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblDateRangeLabel;
        private System.Windows.Forms.ComboBox cmbDateRange;
        private System.Windows.Forms.Label lblCustom;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPrint;

        private System.Windows.Forms.Panel pnlSummaryCards;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Panel cardCashIn;
        private System.Windows.Forms.Label lblCashInTitle;
        private System.Windows.Forms.Label lblCashInTotal;
        private System.Windows.Forms.Panel cardCashOut;
        private System.Windows.Forms.Label lblCashOutTitle;
        private System.Windows.Forms.Label lblCashOutTotal;
        private System.Windows.Forms.Panel cardNetFlow;
        private System.Windows.Forms.Label lblNetFlowTitle;
        private System.Windows.Forms.Label lblNetCashFlow;
        private System.Windows.Forms.Panel cardCashInCount;
        private System.Windows.Forms.Label lblCashInCountTitle;
        private System.Windows.Forms.Label lblCashInCount;
        private System.Windows.Forms.Panel cardCashOutCount;
        private System.Windows.Forms.Label lblCashOutCountTitle;
        private System.Windows.Forms.Label lblCashOutCount;

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblBreakdown;
        private System.Windows.Forms.TreeView tvYearlyBreakdown;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblTransactions;
        private System.Windows.Forms.Panel pnlTransactionToolbar;
        private System.Windows.Forms.Button btnExportTransactions;
        private System.Windows.Forms.Button btnCopyTransactions;
        private System.Windows.Forms.DataGridView dgvTransactions;
    }
}