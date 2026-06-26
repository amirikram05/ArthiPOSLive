using System.Drawing;
using System.Windows.Forms;
using Bunifu.Framework.UI;
using MaterialSkin.Controls;
using MetroFramework.Controls;
using DevExpress.XtraEditors;
using System;
namespace ArthiPOS.Rent
{
    partial class TestFormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestFormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mastersMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.portionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tenantMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.productsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.agreementMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rentOverviewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.commissionPaymentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.recordPaymentMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.auctionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.posMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.invoicingMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.augraiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cashInOutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.profilesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.reportingMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.billPaidOutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.monthlyReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.dueReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tenantReportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tenantPaymentHistoryMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tenantLedgerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tenantDueStatementMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyReportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.occupancyReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.incomeReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.financialReportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.collectionReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.revenueReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.outstandingReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.commissionReportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.commissionSummaryMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.productSalesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.commissionDueMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.auctionReportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.dailySalesReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.commissionStatementMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cashFlowReportMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.userGuideMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.profileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.adminProfileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.logoutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.quickToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbDashboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRentCollection = new System.Windows.Forms.ToolStripButton();
            this.tsbDueReport = new System.Windows.Forms.ToolStripButton();
            this.tsbTenantHistory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPOS = new System.Windows.Forms.ToolStripButton();
            this.tsbInvoicing = new System.Windows.Forms.ToolStripButton();
            this.tsbAugrai = new System.Windows.Forms.ToolStripButton();
            this.tsbCashInOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCalculator = new System.Windows.Forms.ToolStripButton();
            this.tsbReports = new System.Windows.Forms.ToolStripButton();
            this.tsbDriveUpload = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.userLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.welcomePanel = new System.Windows.Forms.Panel();
            this.centerPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.quickActionsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblQuickActions = new System.Windows.Forms.Label();
            this.statsPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rentStatsPanel = new System.Windows.Forms.Panel();
            this.lblActiveAgreements = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblMonthlyRent = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTotalProperties = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rentStatsHeader = new System.Windows.Forms.Label();
            this.auctionStatsPanel = new System.Windows.Forms.Panel();
            this.lblTodaySales = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTodayCommission = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalVendors = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.auctionStatsHeader = new System.Windows.Forms.Label();
            this.welcomeHeaderPanel = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.dateTimer = new System.Windows.Forms.Timer(this.components);
            this.logMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contractMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eng_langMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.urdu_langMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.quickToolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.welcomePanel.SuspendLayout();
            this.centerPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statsPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.rentStatsPanel.SuspendLayout();
            this.auctionStatsPanel.SuspendLayout();
            this.welcomeHeaderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.auctionMenu,
            this.mastersMenu,
            this.transactionsMenu,
            this.reportsMenu,
            this.helpMenu,
            this.profileMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1028, 27);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dashboardMenu,
            this.toolStripSeparator1,
            this.exitMenu});
            this.fileMenu.ForeColor = System.Drawing.Color.White;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(41, 23);
            this.fileMenu.Text = "File";
            // 
            // dashboardMenu
            // 
            this.dashboardMenu.Name = "dashboardMenu";
            this.dashboardMenu.Size = new System.Drawing.Size(180, 24);
            this.dashboardMenu.Text = "Dashboard";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // exitMenu
            // 
            this.exitMenu.Name = "exitMenu";
            this.exitMenu.Size = new System.Drawing.Size(180, 24);
            this.exitMenu.Text = "Exit";
            // 
            // mastersMenu
            // 
            this.mastersMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertyMenu,
            this.portionMenu,
            this.tenantMenu,
            this.productsMenu});
            this.mastersMenu.ForeColor = System.Drawing.Color.White;
            this.mastersMenu.Name = "mastersMenu";
            this.mastersMenu.Size = new System.Drawing.Size(91, 23);
            this.mastersMenu.Text = "Rent Mgmt";
            // 
            // propertyMenu
            // 
            this.propertyMenu.Name = "propertyMenu";
            this.propertyMenu.Size = new System.Drawing.Size(217, 24);
            this.propertyMenu.Text = "Property Management";
            // 
            // portionMenu
            // 
            this.portionMenu.Name = "portionMenu";
            this.portionMenu.Size = new System.Drawing.Size(217, 24);
            this.portionMenu.Text = "Portion Management";
            // 
            // tenantMenu
            // 
            this.tenantMenu.Name = "tenantMenu";
            this.tenantMenu.Size = new System.Drawing.Size(217, 24);
            this.tenantMenu.Text = "Tenant Management";
            // 
            // productsMenu
            // 
            this.productsMenu.Name = "productsMenu";
            this.productsMenu.Size = new System.Drawing.Size(217, 24);
            this.productsMenu.Text = "Product Management";
            // 
            // transactionsMenu
            // 
            this.transactionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agreementMenu,
            this.rentOverviewMenu,
            this.commissionPaymentMenu,
            this.recordPaymentMenu});
            this.transactionsMenu.ForeColor = System.Drawing.Color.White;
            this.transactionsMenu.Name = "transactionsMenu";
            this.transactionsMenu.Size = new System.Drawing.Size(128, 23);
            this.transactionsMenu.Text = "Rent Transactions";
            // 
            // agreementMenu
            // 
            this.agreementMenu.Name = "agreementMenu";
            this.agreementMenu.Size = new System.Drawing.Size(257, 24);
            this.agreementMenu.Text = "Rent Agreement";
            // 
            // rentOverviewMenu
            // 
            this.rentOverviewMenu.Name = "rentOverviewMenu";
            this.rentOverviewMenu.Size = new System.Drawing.Size(257, 24);
            this.rentOverviewMenu.Text = "Rent Collection Overview";
            // 
            // commissionPaymentMenu
            // 
            this.commissionPaymentMenu.Name = "commissionPaymentMenu";
            this.commissionPaymentMenu.Size = new System.Drawing.Size(257, 24);
            this.commissionPaymentMenu.Text = "Record Commission Payment";
            // 
            // recordPaymentMenu
            // 
            this.recordPaymentMenu.Name = "recordPaymentMenu";
            this.recordPaymentMenu.Size = new System.Drawing.Size(257, 24);
            this.recordPaymentMenu.Text = "Record Payment (All Types)";
            // 
            // auctionMenu
            // 
            this.auctionMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.posMenu,
            this.invoicingMenu,
            this.augraiMenu,
            this.cashInOutMenu,
            this.profilesMenu,
            this.reportingMenu,
            this.billPaidOutMenu});
            this.auctionMenu.ForeColor = System.Drawing.Color.White;
            this.auctionMenu.Name = "auctionMenu";
            this.auctionMenu.Size = new System.Drawing.Size(68, 23);
            this.auctionMenu.Text = "Auction";
            // 
            // posMenu
            // 
            this.posMenu.Name = "posMenu";
            this.posMenu.Size = new System.Drawing.Size(180, 24);
            this.posMenu.Text = "POS";
            // 
            // invoicingMenu
            // 
            this.invoicingMenu.Name = "invoicingMenu";
            this.invoicingMenu.Size = new System.Drawing.Size(180, 24);
            this.invoicingMenu.Text = "Invoicing";
            // 
            // augraiMenu
            // 
            this.augraiMenu.Name = "augraiMenu";
            this.augraiMenu.Size = new System.Drawing.Size(180, 24);
            this.augraiMenu.Text = "Augrai";
            // 
            // cashInOutMenu
            // 
            this.cashInOutMenu.Name = "cashInOutMenu";
            this.cashInOutMenu.Size = new System.Drawing.Size(180, 24);
            this.cashInOutMenu.Text = "Cash In/Out";
            // 
            // profilesMenu
            // 
            this.profilesMenu.Name = "profilesMenu";
            this.profilesMenu.Size = new System.Drawing.Size(180, 24);
            this.profilesMenu.Text = "Profiles";
            // 
            // reportingMenu
            // 
            this.reportingMenu.Name = "reportingMenu";
            this.reportingMenu.Size = new System.Drawing.Size(180, 24);
            this.reportingMenu.Text = "Reporting";
            // 
            // billPaidOutMenu
            // 
            this.billPaidOutMenu.Name = "billPaidOutMenu";
            this.billPaidOutMenu.Size = new System.Drawing.Size(180, 24);
            this.billPaidOutMenu.Text = "Bill Paid Out";
            // 
            // reportsMenu
            // 
            this.reportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monthlyReportMenu,
            this.dueReportMenu,
            this.toolStripSeparator2,
            this.tenantReportsMenu,
            this.propertyReportsMenu,
            this.toolStripSeparator3,
            this.financialReportsMenu,
            this.commissionReportsMenu,
            this.auctionReportsMenu});
            this.reportsMenu.ForeColor = System.Drawing.Color.White;
            this.reportsMenu.Name = "reportsMenu";
            this.reportsMenu.Size = new System.Drawing.Size(68, 23);
            this.reportsMenu.Text = "Reports";
            // 
            // monthlyReportMenu
            // 
            this.monthlyReportMenu.Name = "monthlyReportMenu";
            this.monthlyReportMenu.Size = new System.Drawing.Size(259, 24);
            this.monthlyReportMenu.Text = "📊 Monthly Summary Report";
            // 
            // dueReportMenu
            // 
            this.dueReportMenu.Name = "dueReportMenu";
            this.dueReportMenu.Size = new System.Drawing.Size(259, 24);
            this.dueReportMenu.Text = "⚠️ Due Report";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(256, 6);
            // 
            // tenantReportsMenu
            // 
            this.tenantReportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tenantPaymentHistoryMenu,
            this.tenantLedgerMenu,
            this.tenantDueStatementMenu});
            this.tenantReportsMenu.Name = "tenantReportsMenu";
            this.tenantReportsMenu.Size = new System.Drawing.Size(259, 24);
            this.tenantReportsMenu.Text = "👥 Tenant Reports";
            // 
            // tenantPaymentHistoryMenu
            // 
            this.tenantPaymentHistoryMenu.Name = "tenantPaymentHistoryMenu";
            this.tenantPaymentHistoryMenu.Size = new System.Drawing.Size(246, 24);
            this.tenantPaymentHistoryMenu.Text = "🧾 Tenant Payment History";
            // 
            // tenantLedgerMenu
            // 
            this.tenantLedgerMenu.Name = "tenantLedgerMenu";
            this.tenantLedgerMenu.Size = new System.Drawing.Size(246, 24);
            this.tenantLedgerMenu.Text = "📒 Tenant Ledger Report";
            // 
            // tenantDueStatementMenu
            // 
            this.tenantDueStatementMenu.Name = "tenantDueStatementMenu";
            this.tenantDueStatementMenu.Size = new System.Drawing.Size(246, 24);
            this.tenantDueStatementMenu.Text = "📝 Tenant Due Statement";
            // 
            // propertyReportsMenu
            // 
            this.propertyReportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertyReportMenu,
            this.occupancyReportMenu,
            this.incomeReportMenu});
            this.propertyReportsMenu.Name = "propertyReportsMenu";
            this.propertyReportsMenu.Size = new System.Drawing.Size(259, 24);
            this.propertyReportsMenu.Text = "🏢 Property Reports";
            // 
            // propertyReportMenu
            // 
            this.propertyReportMenu.Name = "propertyReportMenu";
            this.propertyReportMenu.Size = new System.Drawing.Size(238, 24);
            this.propertyReportMenu.Text = "Property Summary Report";
            // 
            // occupancyReportMenu
            // 
            this.occupancyReportMenu.Name = "occupancyReportMenu";
            this.occupancyReportMenu.Size = new System.Drawing.Size(238, 24);
            this.occupancyReportMenu.Text = "Occupancy Report";
            // 
            // incomeReportMenu
            // 
            this.incomeReportMenu.Name = "incomeReportMenu";
            this.incomeReportMenu.Size = new System.Drawing.Size(238, 24);
            this.incomeReportMenu.Text = "Property Income Report";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(256, 6);
            // 
            // financialReportsMenu
            // 
            this.financialReportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collectionReportMenu,
            this.revenueReportMenu,
            this.outstandingReportMenu});
            this.financialReportsMenu.Name = "financialReportsMenu";
            this.financialReportsMenu.Size = new System.Drawing.Size(259, 24);
            this.financialReportsMenu.Text = "💰 Financial Reports";
            // 
            // collectionReportMenu
            // 
            this.collectionReportMenu.Name = "collectionReportMenu";
            this.collectionReportMenu.Size = new System.Drawing.Size(242, 24);
            this.collectionReportMenu.Text = "Collection Efficiency Report";
            // 
            // revenueReportMenu
            // 
            this.revenueReportMenu.Name = "revenueReportMenu";
            this.revenueReportMenu.Size = new System.Drawing.Size(242, 24);
            this.revenueReportMenu.Text = "Revenue Analysis Report";
            // 
            // outstandingReportMenu
            // 
            this.outstandingReportMenu.Name = "outstandingReportMenu";
            this.outstandingReportMenu.Size = new System.Drawing.Size(242, 24);
            this.outstandingReportMenu.Text = "Outstanding Dues Report";
            // 
            // commissionReportsMenu
            // 
            this.commissionReportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commissionSummaryMenu,
            this.productSalesMenu,
            this.commissionDueMenu});
            this.commissionReportsMenu.Name = "commissionReportsMenu";
            this.commissionReportsMenu.Size = new System.Drawing.Size(259, 24);
            this.commissionReportsMenu.Text = "📈 Commission Reports";
            // 
            // commissionSummaryMenu
            // 
            this.commissionSummaryMenu.Name = "commissionSummaryMenu";
            this.commissionSummaryMenu.Size = new System.Drawing.Size(260, 24);
            this.commissionSummaryMenu.Text = "Commission Summary Report";
            // 
            // productSalesMenu
            // 
            this.productSalesMenu.Name = "productSalesMenu";
            this.productSalesMenu.Size = new System.Drawing.Size(260, 24);
            this.productSalesMenu.Text = "Product Sales Report";
            // 
            // commissionDueMenu
            // 
            this.commissionDueMenu.Name = "commissionDueMenu";
            this.commissionDueMenu.Size = new System.Drawing.Size(260, 24);
            this.commissionDueMenu.Text = "Commission Due Report";
            // 
            // auctionReportsMenu
            // 
            this.auctionReportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dailySalesReportMenu,
            this.commissionStatementMenu,
            this.cashFlowReportMenu});
            this.auctionReportsMenu.Name = "auctionReportsMenu";
            this.auctionReportsMenu.Size = new System.Drawing.Size(259, 24);
            this.auctionReportsMenu.Text = "🥦 Auction Reports";
            // 
            // dailySalesReportMenu
            // 
            this.dailySalesReportMenu.Name = "dailySalesReportMenu";
            this.dailySalesReportMenu.Size = new System.Drawing.Size(220, 24);
            this.dailySalesReportMenu.Text = "Daily Sales Report";
            // 
            // commissionStatementMenu
            // 
            this.commissionStatementMenu.Name = "commissionStatementMenu";
            this.commissionStatementMenu.Size = new System.Drawing.Size(220, 24);
            this.commissionStatementMenu.Text = "Commission Statement";
            // 
            // cashFlowReportMenu
            // 
            this.cashFlowReportMenu.Name = "cashFlowReportMenu";
            this.cashFlowReportMenu.Size = new System.Drawing.Size(220, 24);
            this.cashFlowReportMenu.Text = "Cash Flow Report";
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contractMenu,
            this.logMenu,
            this.userGuideMenu,
            this.shortcutsMenu,
            this.toolStripSeparator4,
            this.aboutMenu});
            this.helpMenu.ForeColor = System.Drawing.Color.White;
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(49, 23);
            this.helpMenu.Text = "Help";
            // 
            // userGuideMenu
            // 
            this.userGuideMenu.Name = "userGuideMenu";
            this.userGuideMenu.Size = new System.Drawing.Size(198, 24);
            this.userGuideMenu.Text = "User Guide";
            // 
            // shortcutsMenu
            // 
            this.shortcutsMenu.Name = "shortcutsMenu";
            this.shortcutsMenu.Size = new System.Drawing.Size(198, 24);
            this.shortcutsMenu.Text = "Keyboard Shortcuts";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(195, 6);
            // 
            // aboutMenu
            // 
            this.aboutMenu.Name = "aboutMenu";
            this.aboutMenu.Size = new System.Drawing.Size(198, 24);
            this.aboutMenu.Text = "About";
            // 
            // profileMenu
            // 
            this.profileMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.profileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem,
            this.adminProfileMenu,
            this.settingsMenu,
            this.toolStripSeparator6,
            this.logoutMenu});
            this.profileMenu.ForeColor = System.Drawing.Color.White;
            this.profileMenu.Name = "profileMenu";
            this.profileMenu.Size = new System.Drawing.Size(59, 23);
            this.profileMenu.Text = "Profile";
            // 
            // adminProfileMenu
            // 
            this.adminProfileMenu.Name = "adminProfileMenu";
            this.adminProfileMenu.Size = new System.Drawing.Size(180, 24);
            this.adminProfileMenu.Text = "Admin Profile";
            // 
            // settingsMenu
            // 
            this.settingsMenu.Name = "settingsMenu";
            this.settingsMenu.Size = new System.Drawing.Size(180, 24);
            this.settingsMenu.Text = "Settings";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(177, 6);
            // 
            // logoutMenu
            // 
            this.logoutMenu.Name = "logoutMenu";
            this.logoutMenu.Size = new System.Drawing.Size(180, 24);
            this.logoutMenu.Text = "Logout";
            // 
            // quickToolStrip
            // 
            this.quickToolStrip.BackColor = System.Drawing.Color.Gray;
            this.quickToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quickToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.quickToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.quickToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDashboard,
            this.toolStripSeparator7,
            this.tsbPOS,
            this.tsbInvoicing,
            this.tsbCashInOut,
            this.tsbAugrai,
            this.toolStripSeparator8,
            this.tsbRentCollection,
            this.tsbDueReport,
            this.tsbTenantHistory,
            this.toolStripSeparator9,
            this.tsbCalculator,
            this.tsbReports,
            this.tsbDriveUpload});
            this.quickToolStrip.Location = new System.Drawing.Point(0, 27);
            this.quickToolStrip.Name = "quickToolStrip";
            this.quickToolStrip.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.quickToolStrip.Size = new System.Drawing.Size(1028, 31);
            this.quickToolStrip.TabIndex = 1;
            this.quickToolStrip.Text = "toolStrip1";
            // 
            // tsbDashboard
            // 
            this.tsbDashboard.BackColor = System.Drawing.Color.Transparent;
            this.tsbDashboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDashboard.Name = "tsbDashboard";
            this.tsbDashboard.Size = new System.Drawing.Size(82, 20);
            this.tsbDashboard.Text = "🏠 Dashboard";
            this.tsbDashboard.ToolTipText = "Dashboard (F5)";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 23);
            // 
            // tsbRentCollection
            // 
            this.tsbRentCollection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRentCollection.Image = ((System.Drawing.Image)(resources.GetObject("tsbRentCollection.Image")));
            this.tsbRentCollection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRentCollection.Name = "tsbRentCollection";
            this.tsbRentCollection.Size = new System.Drawing.Size(105, 20);
            this.tsbRentCollection.Text = "💰 Rent Collection";
            this.tsbRentCollection.ToolTipText = "Rent Collection (Ctrl+R)";
            // 
            // tsbDueReport
            // 
            this.tsbDueReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDueReport.Image = ((System.Drawing.Image)(resources.GetObject("tsbDueReport.Image")));
            this.tsbDueReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDueReport.Name = "tsbDueReport";
            this.tsbDueReport.Size = new System.Drawing.Size(85, 20);
            this.tsbDueReport.Text = "⚠️ Due Report";
            this.tsbDueReport.ToolTipText = "Due Report (Ctrl+D)";
            // 
            // tsbTenantHistory
            // 
            this.tsbTenantHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbTenantHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTenantHistory.Name = "tsbTenantHistory";
            this.tsbTenantHistory.Size = new System.Drawing.Size(101, 20);
            this.tsbTenantHistory.Text = "🧾 Tenant History";
            this.tsbTenantHistory.ToolTipText = "Tenant Payment History (Ctrl+T)";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 23);
            // 
            // tsbPOS
            // 
            this.tsbPOS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbPOS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPOS.Name = "tsbPOS";
            this.tsbPOS.Size = new System.Drawing.Size(44, 20);
            this.tsbPOS.Text = "📱 POS";
            this.tsbPOS.ToolTipText = "POS System (Ctrl+P)";
            // 
            // tsbInvoicing
            // 
            this.tsbInvoicing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbInvoicing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInvoicing.Name = "tsbInvoicing";
            this.tsbInvoicing.Size = new System.Drawing.Size(74, 20);
            this.tsbInvoicing.Text = "🧾 Invoicing";
            this.tsbInvoicing.ToolTipText = "Invoicing System (Ctrl+I)";
            // 
            // tsbAugrai
            // 
            this.tsbAugrai.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAugrai.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAugrai.Name = "tsbAugrai";
            this.tsbAugrai.Size = new System.Drawing.Size(61, 20);
            this.tsbAugrai.Text = "🥦 Augrai";
            this.tsbAugrai.ToolTipText = "Augrai System (Ctrl+A)";
            // 
            // tsbCashInOut
            // 
            this.tsbCashInOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCashInOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCashInOut.Name = "tsbCashInOut";
            this.tsbCashInOut.Size = new System.Drawing.Size(90, 20);
            this.tsbCashInOut.Text = "💵 Cash In/Out";
            this.tsbCashInOut.ToolTipText = "Cash Management";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 23);
            // 
            // tsbCalculator
            // 
            this.tsbCalculator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCalculator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCalculator.Name = "tsbCalculator";
            this.tsbCalculator.Size = new System.Drawing.Size(80, 20);
            this.tsbCalculator.Text = "🧮 Calculator";
            this.tsbCalculator.ToolTipText = "Open Calculator";
            // 
            // tsbReports
            // 
            this.tsbReports.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbReports.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReports.Name = "tsbReports";
            this.tsbReports.Size = new System.Drawing.Size(66, 20);
            this.tsbReports.Text = "📊 Reports";
            this.tsbReports.ToolTipText = "Auction Reports";
            // 
            // tsbDriveUpload
            // 
            this.tsbDriveUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDriveUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDriveUpload.Name = "tsbDriveUpload";
            this.tsbDriveUpload.Size = new System.Drawing.Size(94, 20);
            this.tsbDriveUpload.Text = "☁️ Drive Upload";
            this.tsbDriveUpload.ToolTipText = "Google Drive Upload";
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.SteelBlue;
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.toolStripStatusLabel1,
            this.userLabel,
            this.dateLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 585);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1028, 24);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 19);
            this.statusLabel.Text = "Ready";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(847, 19);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // userLabel
            // 
            this.userLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.userLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.userLabel.ForeColor = System.Drawing.Color.White;
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(113, 19);
            this.userLabel.Text = "User: Administrator";
            // 
            // dateLabel
            // 
            this.dateLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.dateLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.dateLabel.ForeColor = System.Drawing.Color.White;
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(14, 19);
            this.dateLabel.Text = " ";
            // 
            // welcomePanel
            // 
            this.welcomePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.welcomePanel.Controls.Add(this.centerPanel);
            this.welcomePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.welcomePanel.Location = new System.Drawing.Point(0, 58);
            this.welcomePanel.Margin = new System.Windows.Forms.Padding(2);
            this.welcomePanel.Name = "welcomePanel";
            this.welcomePanel.Size = new System.Drawing.Size(1028, 527);
            this.welcomePanel.TabIndex = 3;
            // 
            // centerPanel
            // 
            this.centerPanel.BackColor = System.Drawing.Color.White;
            this.centerPanel.Controls.Add(this.panel1);
            this.centerPanel.Controls.Add(this.statsPanel);
            this.centerPanel.Controls.Add(this.welcomeHeaderPanel);
            this.centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerPanel.Location = new System.Drawing.Point(0, 0);
            this.centerPanel.Margin = new System.Windows.Forms.Padding(2);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.Size = new System.Drawing.Size(1028, 527);
            this.centerPanel.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.quickActionsFlowPanel);
            this.panel1.Controls.Add(this.lblQuickActions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 390);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(30, 24, 30, 24);
            this.panel1.Size = new System.Drawing.Size(1028, 137);
            this.panel1.TabIndex = 2;
            // 
            // quickActionsFlowPanel
            // 
            this.quickActionsFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quickActionsFlowPanel.Location = new System.Drawing.Point(30, 24);
            this.quickActionsFlowPanel.Margin = new System.Windows.Forms.Padding(2);
            this.quickActionsFlowPanel.Name = "quickActionsFlowPanel";
            this.quickActionsFlowPanel.Padding = new System.Windows.Forms.Padding(0, 24, 0, 0);
            this.quickActionsFlowPanel.Size = new System.Drawing.Size(968, 89);
            this.quickActionsFlowPanel.TabIndex = 1;
            // 
            // lblQuickActions
            // 
            this.lblQuickActions.AutoSize = true;
            this.lblQuickActions.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuickActions.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblQuickActions.Location = new System.Drawing.Point(30, 24);
            this.lblQuickActions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQuickActions.Name = "lblQuickActions";
            this.lblQuickActions.Size = new System.Drawing.Size(130, 25);
            this.lblQuickActions.TabIndex = 0;
            this.lblQuickActions.Text = "Quick Actions";
            // 
            // statsPanel
            // 
            this.statsPanel.Controls.Add(this.panel2);
            this.statsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statsPanel.Location = new System.Drawing.Point(0, 146);
            this.statsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.statsPanel.Name = "statsPanel";
            this.statsPanel.Padding = new System.Windows.Forms.Padding(30, 24, 30, 24);
            this.statsPanel.Size = new System.Drawing.Size(1028, 244);
            this.statsPanel.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rentStatsPanel);
            this.panel2.Controls.Add(this.auctionStatsPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(30, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(968, 196);
            this.panel2.TabIndex = 0;
            // 
            // rentStatsPanel
            // 
            this.rentStatsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.rentStatsPanel.Controls.Add(this.lblActiveAgreements);
            this.rentStatsPanel.Controls.Add(this.label10);
            this.rentStatsPanel.Controls.Add(this.lblMonthlyRent);
            this.rentStatsPanel.Controls.Add(this.label8);
            this.rentStatsPanel.Controls.Add(this.lblTotalProperties);
            this.rentStatsPanel.Controls.Add(this.label6);
            this.rentStatsPanel.Controls.Add(this.rentStatsHeader);
            this.rentStatsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rentStatsPanel.Location = new System.Drawing.Point(710, 0);
            this.rentStatsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.rentStatsPanel.Name = "rentStatsPanel";
            this.rentStatsPanel.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.rentStatsPanel.Size = new System.Drawing.Size(258, 196);
            this.rentStatsPanel.TabIndex = 1;
            // 
            // lblActiveAgreements
            // 
            this.lblActiveAgreements.AutoSize = true;
            this.lblActiveAgreements.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveAgreements.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblActiveAgreements.Location = new System.Drawing.Point(15, 146);
            this.lblActiveAgreements.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblActiveAgreements.Name = "lblActiveAgreements";
            this.lblActiveAgreements.Size = new System.Drawing.Size(49, 37);
            this.lblActiveAgreements.TabIndex = 6;
            this.lblActiveAgreements.Text = "12";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DimGray;
            this.label10.Location = new System.Drawing.Point(19, 130);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 19);
            this.label10.TabIndex = 5;
            this.label10.Text = "Active Agreements";
            // 
            // lblMonthlyRent
            // 
            this.lblMonthlyRent.AutoSize = true;
            this.lblMonthlyRent.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonthlyRent.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblMonthlyRent.Location = new System.Drawing.Point(15, 98);
            this.lblMonthlyRent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMonthlyRent.Name = "lblMonthlyRent";
            this.lblMonthlyRent.Size = new System.Drawing.Size(120, 37);
            this.lblMonthlyRent.TabIndex = 4;
            this.lblMonthlyRent.Text = "₹45,000";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(19, 81);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 19);
            this.label8.TabIndex = 3;
            this.label8.Text = "Monthly Rent";
            // 
            // lblTotalProperties
            // 
            this.lblTotalProperties.AutoSize = true;
            this.lblTotalProperties.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProperties.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTotalProperties.Location = new System.Drawing.Point(15, 49);
            this.lblTotalProperties.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalProperties.Name = "lblTotalProperties";
            this.lblTotalProperties.Size = new System.Drawing.Size(49, 37);
            this.lblTotalProperties.TabIndex = 2;
            this.lblTotalProperties.Text = "25";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(19, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 19);
            this.label6.TabIndex = 1;
            this.label6.Text = "Total Properties";
            // 
            // rentStatsHeader
            // 
            this.rentStatsHeader.AutoSize = true;
            this.rentStatsHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rentStatsHeader.ForeColor = System.Drawing.Color.SteelBlue;
            this.rentStatsHeader.Location = new System.Drawing.Point(15, 0);
            this.rentStatsHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rentStatsHeader.Name = "rentStatsHeader";
            this.rentStatsHeader.Size = new System.Drawing.Size(158, 25);
            this.rentStatsHeader.TabIndex = 0;
            this.rentStatsHeader.Text = "🏢 Rent Statistics";
            // 
            // auctionStatsPanel
            // 
            this.auctionStatsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.auctionStatsPanel.Controls.Add(this.lblTodaySales);
            this.auctionStatsPanel.Controls.Add(this.label5);
            this.auctionStatsPanel.Controls.Add(this.lblTodayCommission);
            this.auctionStatsPanel.Controls.Add(this.label3);
            this.auctionStatsPanel.Controls.Add(this.lblTotalVendors);
            this.auctionStatsPanel.Controls.Add(this.label1);
            this.auctionStatsPanel.Controls.Add(this.auctionStatsHeader);
            this.auctionStatsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.auctionStatsPanel.Location = new System.Drawing.Point(0, 0);
            this.auctionStatsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.auctionStatsPanel.Name = "auctionStatsPanel";
            this.auctionStatsPanel.Padding = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.auctionStatsPanel.Size = new System.Drawing.Size(710, 196);
            this.auctionStatsPanel.TabIndex = 0;
            // 
            // lblTodaySales
            // 
            this.lblTodaySales.AutoSize = true;
            this.lblTodaySales.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTodaySales.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTodaySales.Location = new System.Drawing.Point(15, 146);
            this.lblTodaySales.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTodaySales.Name = "lblTodaySales";
            this.lblTodaySales.Size = new System.Drawing.Size(143, 37);
            this.lblTodaySales.TabIndex = 6;
            this.lblTodaySales.Text = "₹1,25,000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(19, 130);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "Today Sales";
            // 
            // lblTodayCommission
            // 
            this.lblTodayCommission.AutoSize = true;
            this.lblTodayCommission.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTodayCommission.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTodayCommission.Location = new System.Drawing.Point(15, 98);
            this.lblTodayCommission.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTodayCommission.Name = "lblTodayCommission";
            this.lblTodayCommission.Size = new System.Drawing.Size(120, 37);
            this.lblTodayCommission.TabIndex = 4;
            this.lblTodayCommission.Text = "₹12,500";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(19, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Today Commission";
            // 
            // lblTotalVendors
            // 
            this.lblTotalVendors.AutoSize = true;
            this.lblTotalVendors.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVendors.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTotalVendors.Location = new System.Drawing.Point(15, 49);
            this.lblTotalVendors.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalVendors.Name = "lblTotalVendors";
            this.lblTotalVendors.Size = new System.Drawing.Size(49, 37);
            this.lblTotalVendors.TabIndex = 2;
            this.lblTotalVendors.Text = "48";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total Vendors";
            // 
            // auctionStatsHeader
            // 
            this.auctionStatsHeader.AutoSize = true;
            this.auctionStatsHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.auctionStatsHeader.ForeColor = System.Drawing.Color.ForestGreen;
            this.auctionStatsHeader.Location = new System.Drawing.Point(15, 0);
            this.auctionStatsHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.auctionStatsHeader.Name = "auctionStatsHeader";
            this.auctionStatsHeader.Size = new System.Drawing.Size(186, 25);
            this.auctionStatsHeader.TabIndex = 0;
            this.auctionStatsHeader.Text = "🥦 Auction Statistics";
            // 
            // welcomeHeaderPanel
            // 
            this.welcomeHeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.welcomeHeaderPanel.Controls.Add(this.lblHeader);
            this.welcomeHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.welcomeHeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.welcomeHeaderPanel.Margin = new System.Windows.Forms.Padding(2);
            this.welcomeHeaderPanel.Name = "welcomeHeaderPanel";
            this.welcomeHeaderPanel.Padding = new System.Windows.Forms.Padding(30, 24, 30, 24);
            this.welcomeHeaderPanel.Size = new System.Drawing.Size(1028, 146);
            this.welcomeHeaderPanel.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblHeader.Location = new System.Drawing.Point(30, 24);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(968, 98);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "🏪 Vegetable Auction & Shop Rent Management System";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimer
            // 
            this.dateTimer.Interval = 60000;
            // 
            // logMenu
            // 
            this.logMenu.Name = "logMenu";
            this.logMenu.Size = new System.Drawing.Size(198, 24);
            this.logMenu.Text = "Log";
            // 
            // contractMenu
            // 
            this.contractMenu.Name = "contractMenu";
            this.contractMenu.Size = new System.Drawing.Size(198, 24);
            this.contractMenu.Text = "Contracts";
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eng_langMenu,
            this.urdu_langMenu});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // eng_langMenu
            // 
            this.eng_langMenu.Name = "eng_langMenu";
            this.eng_langMenu.Size = new System.Drawing.Size(180, 24);
            this.eng_langMenu.Text = "Eng";
            // 
            // urdu_langMenu
            // 
            this.urdu_langMenu.Name = "urdu_langMenu";
            this.urdu_langMenu.Size = new System.Drawing.Size(180, 24);
            this.urdu_langMenu.Text = "Urdu";
            // 
            // TestFormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Controls.Add(this.welcomePanel);
            this.Controls.Add(this.quickToolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TestFormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vegetable Auction & Shop Rent Management System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.quickToolStrip.ResumeLayout(false);
            this.quickToolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.welcomePanel.ResumeLayout(false);
            this.centerPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statsPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.rentStatsPanel.ResumeLayout(false);
            this.rentStatsPanel.PerformLayout();
            this.auctionStatsPanel.ResumeLayout(false);
            this.auctionStatsPanel.PerformLayout();
            this.welcomeHeaderPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem dashboardMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitMenu;
        private System.Windows.Forms.ToolStripMenuItem mastersMenu;
        private System.Windows.Forms.ToolStripMenuItem propertyMenu;
        private System.Windows.Forms.ToolStripMenuItem portionMenu;
        private System.Windows.Forms.ToolStripMenuItem tenantMenu;
        private System.Windows.Forms.ToolStripMenuItem productsMenu;
        private System.Windows.Forms.ToolStripMenuItem transactionsMenu;
        private System.Windows.Forms.ToolStripMenuItem agreementMenu;
        private System.Windows.Forms.ToolStripMenuItem rentOverviewMenu;
        private System.Windows.Forms.ToolStripMenuItem commissionPaymentMenu;
        private System.Windows.Forms.ToolStripMenuItem recordPaymentMenu;
        private System.Windows.Forms.ToolStripMenuItem auctionMenu;
        private System.Windows.Forms.ToolStripMenuItem posMenu;
        private System.Windows.Forms.ToolStripMenuItem invoicingMenu;
        private System.Windows.Forms.ToolStripMenuItem augraiMenu;
        private System.Windows.Forms.ToolStripMenuItem cashInOutMenu;
        private System.Windows.Forms.ToolStripMenuItem profilesMenu;
        private System.Windows.Forms.ToolStripMenuItem reportingMenu;
        private System.Windows.Forms.ToolStripMenuItem billPaidOutMenu;
        private System.Windows.Forms.ToolStripMenuItem reportsMenu;
        private System.Windows.Forms.ToolStripMenuItem monthlyReportMenu;
        private System.Windows.Forms.ToolStripMenuItem dueReportMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tenantReportsMenu;
        private System.Windows.Forms.ToolStripMenuItem tenantPaymentHistoryMenu;
        private System.Windows.Forms.ToolStripMenuItem tenantLedgerMenu;
        private System.Windows.Forms.ToolStripMenuItem tenantDueStatementMenu;
        private System.Windows.Forms.ToolStripMenuItem propertyReportsMenu;
        private System.Windows.Forms.ToolStripMenuItem propertyReportMenu;
        private System.Windows.Forms.ToolStripMenuItem occupancyReportMenu;
        private System.Windows.Forms.ToolStripMenuItem incomeReportMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem financialReportsMenu;
        private System.Windows.Forms.ToolStripMenuItem collectionReportMenu;
        private System.Windows.Forms.ToolStripMenuItem revenueReportMenu;
        private System.Windows.Forms.ToolStripMenuItem outstandingReportMenu;
        private System.Windows.Forms.ToolStripMenuItem commissionReportsMenu;
        private System.Windows.Forms.ToolStripMenuItem commissionSummaryMenu;
        private System.Windows.Forms.ToolStripMenuItem productSalesMenu;
        private System.Windows.Forms.ToolStripMenuItem commissionDueMenu;
        private System.Windows.Forms.ToolStripMenuItem auctionReportsMenu;
        private System.Windows.Forms.ToolStripMenuItem dailySalesReportMenu;
        private System.Windows.Forms.ToolStripMenuItem commissionStatementMenu;
        private System.Windows.Forms.ToolStripMenuItem cashFlowReportMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem userGuideMenu;
        private System.Windows.Forms.ToolStripMenuItem shortcutsMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem aboutMenu;
        private System.Windows.Forms.ToolStripMenuItem profileMenu;
        private System.Windows.Forms.ToolStripMenuItem adminProfileMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem logoutMenu;
        private System.Windows.Forms.ToolStrip quickToolStrip;
        private System.Windows.Forms.ToolStripButton tsbDashboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsbRentCollection;
        private System.Windows.Forms.ToolStripButton tsbDueReport;
        private System.Windows.Forms.ToolStripButton tsbTenantHistory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbPOS;
        private System.Windows.Forms.ToolStripButton tsbInvoicing;
        private System.Windows.Forms.ToolStripButton tsbAugrai;
        private System.Windows.Forms.ToolStripButton tsbCashInOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton tsbCalculator;
        private System.Windows.Forms.ToolStripButton tsbReports;
        private System.Windows.Forms.ToolStripButton tsbDriveUpload;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel userLabel;
        private System.Windows.Forms.ToolStripStatusLabel dateLabel;
        private System.Windows.Forms.Panel welcomePanel;
        private System.Windows.Forms.Panel centerPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel quickActionsFlowPanel;
        private System.Windows.Forms.Label lblQuickActions;
        private System.Windows.Forms.Panel statsPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel rentStatsPanel;
        private System.Windows.Forms.Label lblActiveAgreements;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblMonthlyRent;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotalProperties;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label rentStatsHeader;
        private System.Windows.Forms.Panel auctionStatsPanel;
        private System.Windows.Forms.Label lblTodaySales;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTodayCommission;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalVendors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label auctionStatsHeader;
        private System.Windows.Forms.Panel welcomeHeaderPanel;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Timer dateTimer;
        private ToolStripMenuItem contractMenu;
        private ToolStripMenuItem logMenu;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripMenuItem eng_langMenu;
        private ToolStripMenuItem urdu_langMenu;
    }
}