using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Reports;
using ShopRentManagementSystem.Services;
using ShopRentManagementSystem;
using ArthiPOS.Controls.dashboard;
using BAL;
using ArthiPOS.controls.dashboard;
using ArthiPOS.Reporting;
using ArthiPOS.Controls.test;
using ArthiPOS.controls;
using ArthiPOS.Controls;
using CommonUtilities;
using System.Globalization;
using System.Threading;

namespace ArthiPOS.Rent
{
    partial class TestFormMain:Form
    {
        public TestFormMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            // Setup event handlers
            SetupEventHandlers();
            ShowWelcomeScreen();

            // Start the timer for date label
            dateTimer.Tick += (s, e) => UpdateDateTimeLabel();
            dateTimer.Start();
            UpdateDateTimeLabel();

            // Load quick stats
            LoadQuickStats();
            LoadAuctionStats();
        }

        private void SetupEventHandlers()
        {
            // File menu
            dashboardMenu.Click += (s, e) => ShowWelcomeScreen();
            exitMenu.Click += (s, e) => Application.Exit();

            // Rent Management menu
            propertyMenu.Click += (s, e) => OpenForm(new FrmProperty());
            portionMenu.Click += (s, e) => OpenForm(new FrmPortion());
            tenantMenu.Click += (s, e) => OpenForm(new FrmTenant());
            productsMenu.Click += (s, e) => OpenForm(new FrmProducts());

            // Rent Transactions menu
            agreementMenu.Click += (s, e) => OpenForm(new FrmRentAgreement());
            rentOverviewMenu.Click += (s, e) => OpenForm(new FrmRentCollectionOverview());
            commissionPaymentMenu.Click += (s, e) => OpenCommissionPaymentForm();
            recordPaymentMenu.Click += (s, e) => OpenForm(new FrmRecordPayment());

            // Auction menu
            posMenu.Click += (s, e) => OpenPOSForm();
            invoicingMenu.Click += (s, e) => OpenInvoicingForm();
            augraiMenu.Click += (s, e) => OpenAugraiForm();
            cashInOutMenu.Click += (s, e) => OpenCashInOutForm();
            profilesMenu.Click += (s, e) => OpenProfilesForm();
            reportingMenu.Click += (s, e) => OpenAuctionReportingForm();
            billPaidOutMenu.Click += (s, e) => OpenBillPaidOutForm();

            // Reports menu - Rent Reports
            monthlyReportMenu.Click += (s, e) => OpenForm(new FrmMonthlySummary());
            dueReportMenu.Click += (s, e) => OpenForm(new FrmDueReport());

            // Tenant reports
            tenantPaymentHistoryMenu.Click += (s, e) => OpenForm(new FrmTenantPaymentHistory());
            tenantLedgerMenu.Click += (s, e) => OpenForm(new FrmTenantLedgerReport());
            tenantDueStatementMenu.Click += (s, e) => OpenForm(new FrmTenantDueStatement());

            // Property reports
            propertyReportMenu.Click += (s, e) => OpenForm(new FrmPropertyReport());
            occupancyReportMenu.Click += (s, e) => OpenOccupancyReport();
            incomeReportMenu.Click += (s, e) => OpenPropertyIncomeReport();

            // Financial reports
            collectionReportMenu.Click += (s, e) => OpenCollectionReport();
            revenueReportMenu.Click += (s, e) => OpenRevenueReport();
            outstandingReportMenu.Click += (s, e) => OpenOutstandingReport();

            // Commission reports
            commissionSummaryMenu.Click += (s, e) => OpenCommissionSummary();
            productSalesMenu.Click += (s, e) => OpenProductSalesReport();
            commissionDueMenu.Click += (s, e) => OpenCommissionDueReport();

            // Auction reports
            dailySalesReportMenu.Click += (s, e) => OpenDailySalesReport();
            commissionStatementMenu.Click += (s, e) => OpenCommissionStatement();
            cashFlowReportMenu.Click += (s, e) => OpenCashFlowReport();

            // Profile menu
            adminProfileMenu.Click += (s, e) => OpenAdminProfile();
            settingsMenu.Click += (s, e) => OpenSettings();
            logoutMenu.Click += (s, e) => Logout();

            // Help menu
            userGuideMenu.Click += (s, e) => ShowUserGuide();
            shortcutsMenu.Click += (s, e) => ShowKeyboardShortcuts();
            aboutMenu.Click += (s, e) => ShowAboutDialog();

            // Quick Tool Strip buttons
            tsbDashboard.Click += (s, e) => ShowWelcomeScreen();
            tsbRentCollection.Click += (s, e) => OpenForm(new FrmRentCollectionOverview());
            tsbDueReport.Click += (s, e) => OpenForm(new FrmDueReport());
            tsbTenantHistory.Click += (s, e) => OpenForm(new FrmTenantPaymentHistory());
            tsbPOS.Click += (s, e) => OpenPOSForm();
            tsbInvoicing.Click += (s, e) => OpenInvoicingForm();
            tsbAugrai.Click += (s, e) => OpenAugraiForm();
            tsbCashInOut.Click += (s, e) => OpenCashInOutForm();
            tsbCalculator.Click += (s, e) => OpenCalculator();
            tsbReports.Click += (s, e) => OpenAuctionReportingForm();
            tsbDriveUpload.Click += (s, e) => OpenDriveUpload();
            logMenu.Click += (s, e) => OpenForm(new LogExecMangeForm());
            contractMenu.Click += (s, e) => OpenForm(new FrmUrduDocumentEditor());
            eng_langMenu.Click += (s, e) => changeLanguage(0);
            urdu_langMenu.Click += (s, e) => changeLanguage(1);

            // Keyboard shortcuts
            this.KeyPreview = true;
            this.KeyDown += TestFormMain_KeyDown;
        }
        private void changeLanguage(int choice)
        {
            string lang = "en-US";


            if (choice == 0)
            {
                lang = "en-US";
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");



                LogUtill.loadLastLanguage(lang);
                RegistryAccess.SetStringRegistryValue("Language", lang);
            }
            else
            {
                lang = "ur-PK";
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ur-PK");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ur-PK");

                LogUtill.loadLastLanguage(lang);
                RegistryAccess.SetStringRegistryValue("Language", lang);
            }

        }


        private void TestFormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    ShowKeyboardShortcuts();
                    break;
                case Keys.F5:
                    ShowWelcomeScreen();
                    break;
                case Keys.Escape:
                    if (this.MdiChildren.Length > 0)
                        this.MdiChildren[0].Close();
                    else
                        ShowWelcomeScreen();
                    break;
                case Keys.D when e.Control:
                    OpenForm(new FrmDueReport());
                    break;
                case Keys.T when e.Control:
                    OpenForm(new FrmTenantPaymentHistory());
                    break;
                case Keys.R when e.Control:
                    OpenForm(new FrmRentCollectionOverview());
                    break;
                case Keys.P when e.Control:
                    OpenPOSForm();
                    break;
                case Keys.I when e.Control:
                    OpenInvoicingForm();
                    break;
                case Keys.A when e.Control:
                    OpenAugraiForm();
                    break;
            }
        }

        private void UpdateDateTimeLabel()
        {
            dateLabel.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
        }

        private void LoadQuickStats()
        {
            try
            {
                var dataService = new JsonDataService();
                var properties = dataService.LoadProperties();
                var tenants = dataService.LoadTenants();
                var agreements = dataService.LoadAgreements().Where(a => a.IsActive).ToList();
                var payments = dataService.LoadPayments()
                    .Where(p => p.PaymentDate.Month == DateTime.Now.Month)
                    .ToList();

                int totalProperties = properties.Count;
                decimal monthlyCollection = payments.Sum(p => p.Amount);
                int activeAgreements = agreements.Count;

                lblTotalProperties.Text = totalProperties.ToString();
                lblMonthlyRent.Text = $"₹{monthlyCollection:N0}";
                lblActiveAgreements.Text = activeAgreements.ToString();
            }
            catch
            {
                // Ignore errors in quick stats
            }
        }

        private void LoadAuctionStats()
        {
            try
            {
                // Simulated auction data - Replace with actual data service
                lblTotalVendors.Text = "48";
                lblTodayCommission.Text = "₹12,500";
                lblTodaySales.Text = "₹1,25,000";
            }
            catch
            {
                // Ignore errors in auction stats
            }
        }
        private void addusercontrol(UserControl control)
        {
            centerPanel.Controls.Clear();

            // Stretch control to fill centerPanel
            control.Dock = DockStyle.Fill;
            control.AutoSize = false;

            centerPanel.Controls.Add(control);
        }
        private void OpenForm(Form form)
        {
            // Close existing forms if they're the same type
            foreach (Form childForm in this.MdiChildren)
            {
                if (childForm.GetType() == form.GetType())
                {
                    childForm.BringToFront();
                    return;
                }
            }

            welcomePanel.Visible = false;
            form.MdiParent = this;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.WindowState = FormWindowState.Maximized;
            form.FormClosed += (s, e) =>
            {
                if (this.MdiChildren.Length == 0)
                {
                    welcomePanel.Visible = true;
                    LoadQuickStats(); // Refresh stats when returning to dashboard
                    LoadAuctionStats();
                }
            };
            form.Show();
        }

        // Auction System Methods
        private void OpenPOSForm()
        {
            //MessageBox.Show("Opening POS System...", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OpenForm(new SalesNew());
        }

        private void OpenInvoicingForm()
        {
            //MessageBox.Show("Opening Invoicing System...", "Invoicing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            addusercontrol(new InvoicingPage());
        }

        private void OpenAugraiForm()
        {
            //MessageBox.Show("Opening Augrai System...", "Augrai", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OpenForm(new RepAugraiNewF());
        }

        private void OpenCashInOutForm()
        {
            //MessageBox.Show("Opening Cash In/Out System...", "Cash Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
             OpenForm(new CashInout());
        }

        private void OpenProfilesForm()
        {
            //MessageBox.Show("Opening Profiles Management...", "Profiles", MessageBoxButtons.OK, MessageBoxIcon.Information);
            addusercontrol(new Profiles());
        }

        private void OpenAuctionReportingForm()
        {
            //MessageBox.Show("Opening Auction Reports...", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
            addusercontrol(new Reports());
        }

        private void OpenBillPaidOutForm()
        {
            //MessageBox.Show("Opening Bill Paid Out System...", "Bill Payments", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OpenForm(new BillPaidOut());
        }

        private void OpenCommissionPaymentForm()
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Select Agreement for Commission Payment";
                dialog.Size = new Size(400, 300);
                dialog.StartPosition = FormStartPosition.CenterParent;

                var dataService = new JsonDataService();
                var agreements = dataService.LoadAgreements().Where(a => a.IsActive).ToList();
                var tenants = dataService.LoadTenants();

                var agreementsWithCommission = agreements
                    .Where(a => tenants.FirstOrDefault(t => t.Id == a.TenantId)?.Type == TenantType.OnCommission)
                    .ToList();

                if (!agreementsWithCommission.Any())
                {
                    MessageBox.Show("No commission tenants found.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Label lblSelect = new Label
                {
                    Text = "Select Agreement:",
                    Location = new Point(20, 20),
                    Size = new Size(350, 25)
                };

                ComboBox cmbAgreements = new ComboBox
                {
                    Location = new Point(20, 50),
                    Size = new Size(350, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                foreach (var agreement in agreementsWithCommission)
                {
                    var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                    cmbAgreements.Items.Add(new
                    {
                        Id = agreement.Id,
                        Text = $"{tenant?.Name} - Agreement #{agreement.Id}"
                    });
                }

                if (cmbAgreements.Items.Count > 0)
                    cmbAgreements.SelectedIndex = 0;

                Button btnOpen = new Button
                {
                    Text = "Open Commission Payment",
                    Location = new Point(20, 90),
                    Size = new Size(200, 35),
                    BackColor = Color.LightGreen
                };

                btnOpen.Click += (s, e) =>
                {
                    if (cmbAgreements.SelectedItem != null)
                    {
                        dynamic selected = cmbAgreements.SelectedItem;
                        int agreementId = selected.Id;

                        var commissionForm = new FrmCommissionPayment(agreementId);
                        commissionForm.Owner = this;
                        commissionForm.ShowDialog();

                        dialog.DialogResult = DialogResult.OK;
                    }
                };

                dialog.Controls.AddRange(new Control[] { lblSelect, cmbAgreements, btnOpen });
                dialog.ShowDialog();
            }
        }

        // Quick Tool Strip Methods
        private void OpenDriveUpload()
        {
            MessageBox.Show("Opening Google Drive Upload...", "Drive Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Implement drive upload functionality
        }

        private void OpenCalculator()
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        // Profile Menu Methods
        private void OpenAdminProfile()
        {
            //MessageBox.Show("Opening Admin Profile...", "Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
            addusercontrol(new AdminProfile());
        }

        private void OpenSettings()
        {
            //MessageBox.Show("Opening Settings...", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
             OpenForm(new AddConfig(true));
        }

        private void Logout()
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                Application.Restart();
            }
        }

        private void ShowWelcomeScreen()
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
            welcomePanel.Visible = true;
            LoadQuickStats();
            LoadAuctionStats();
        }

        // Existing Report Methods
        private void OpenOccupancyReport()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            reportGenerator.GenerateAndOpenOccupancyReport();
        }

        private void OpenPropertyIncomeReport()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            reportGenerator.GenerateAndOpenPropertyIncomeReport();
        }

        private void OpenCollectionReport()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            reportGenerator.GenerateAndOpenCollectionEfficiencyReport();
        }

        private void OpenRevenueReport()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            reportGenerator.GenerateAndOpenRevenueAnalysisReport();
        }

        private void OpenOutstandingReport()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            reportGenerator.GenerateAndOpenOutstandingDuesReport();
        }

        private void OpenCommissionSummary()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            reportGenerator.GenerateAndOpenCommissionSummaryReport();
        }

        private void OpenProductSalesReport()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            reportGenerator.GenerateAndOpenProductSalesReport();
        }

        private void OpenCommissionDueReport()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            reportGenerator.GenerateAndOpenCommissionDueReport();
        }

        // New Auction Report Methods
        private void OpenDailySalesReport()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            //reportGenerator.GenerateAndOpenDailySalesReport();
        }

        private void OpenCommissionStatement()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            //reportGenerator.GenerateAndOpenCommissionStatement();
        }

        private void OpenCashFlowReport()
        {
            var reportGenerator = new ReportGenerator(new JsonDataService());
            //reportGenerator.GenerateAndOpenCashFlowReport();
        }

        private void ShowAboutDialog()
        {
            MessageBox.Show(
                "🥦 Vegetable Auction & Shop Rent Management System v3.0\n\n" +
                "📅 Version: 3.0.0\n" +
                "📦 Build Date: " + DateTime.Now.ToString("yyyy-MM-dd") + "\n" +
                "👨‍💻 Developer: Your Company\n\n" +
                "📋 Features:\n" +
                "• Vegetable Auction Management (POS, Invoicing, Augrai)\n" +
                "• Shop Portion Rent Management\n" +
                "• Cash Flow & Commission Tracking\n" +
                "• Vendor & Tenant Management\n" +
                "• Comprehensive Reporting System\n" +
                "• Drive Integration for Backup\n" +
                "• Real-time Sales Tracking\n\n" +
                "📞 Support: support@yourcompany.com",
                "About System", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowUserGuide()
        {
            MessageBox.Show(
                "📘 User Guide - Vegetable Auction & Rent System\n\n" +
                "1. Auction System:\n" +
                "   • POS: Process vegetable sales\n" +
                "   • Invoicing: Generate invoices\n" +
                "   • Augrai: Auction management\n" +
                "   • Cash In/Out: Manage cash flow\n\n" +
                "2. Rent Management:\n" +
                "   • Manage properties and tenants\n" +
                "   • Track rent and commission payments\n\n" +
                "3. Quick Access Toolbar:\n" +
                "   • Dashboard, Rent Collection, Due Report, Tenant History\n" +
                "   • POS, Invoicing, Augrai, Cash In/Out\n" +
                "   • Calculator, Reports, Drive Upload\n\n" +
                "4. Keyboard Shortcuts:\n" +
                "   • Ctrl+P: POS\n" +
                "   • Ctrl+I: Invoicing\n" +
                "   • Ctrl+A: Augrai\n" +
                "   • F1: Help\n" +
                "   • F5: Refresh Dashboard",
                "User Guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowKeyboardShortcuts()
        {
            //MessageBox.Show(
            //    "⌨️ Keyboard Shortcuts\n\n" +
            //    "Global Shortcuts:\n" +
            //    "• F1: Show help\n" +
            //    "• F5: Refresh dashboard\n" +
            //    "• Esc: Close current window\n\n" +
            //    "Auction System:\n" +
            //    "• Ctrl + P: POS\n" +
            //    "• Ctrl + I: Invoicing\n" +
            //    "• Ctrl + A: Augrai\n\n" +
            //    "Rent Management:\n" +
            //    "• Ctrl + D: Due Report\n" +
            //    "• Ctrl + T: Tenant History\n" +
            //    "• Ctrl + R: Rent Collection\n\n" +
            //    "Quick Toolbar:\n" +
            //    "• Click on icons for quick access\n" +
            //    "• Hover for tooltips with shortcuts\n\n" +
            //    "Navigation:\n" +
            //    "• Tab: Next control\n" +
            //    "• Shift+Tab: Previous control",
            //    "Keyboard Shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Information);
            controls.Help help = new controls.Help();
            help.ShowDialog();
        }
    }
}