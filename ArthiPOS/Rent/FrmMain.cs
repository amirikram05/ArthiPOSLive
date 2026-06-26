using ArthiPOS.Rent.CashFlow;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Reports;
using ShopRentManagementSystem.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShopRentManagementSystem
{
    public partial class FrmMain : Form
    {
        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private Panel welcomePanel;

        public FrmMain()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            ShowWelcomeScreen();
        }

        private void InitializeComponent()
        {
            this.Text = "Shop Portion Rent Management System";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsMdiContainer = true;
            this.BackColor = Color.WhiteSmoke;

            // Menu Strip
            menuStrip = new MenuStrip();
            menuStrip.BackColor = Color.SteelBlue;
            menuStrip.ForeColor = Color.White;
            menuStrip.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // File Menu
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem dashboardMenu = new ToolStripMenuItem("Dashboard");

            dashboardMenu.Click += (s, e) => OpenForm(new FrmDashboard1());
            //dashboardMenu.Click += (s, e) => ShowWelcomeScreen();
            ToolStripSeparator separator1 = new ToolStripSeparator();
            ToolStripMenuItem exitMenu = new ToolStripMenuItem("Exit");
            exitMenu.Click += (s, e) => Application.Exit();
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { dashboardMenu, separator1, exitMenu });

            // Masters Menu
            ToolStripMenuItem mastersMenu = new ToolStripMenuItem("Masters");
            ToolStripMenuItem propertyMenu = new ToolStripMenuItem("Property Management");
            ToolStripMenuItem portionMenu = new ToolStripMenuItem("Portion Management");
            ToolStripMenuItem tenantMenu = new ToolStripMenuItem("Tenant Management");
            ToolStripMenuItem productsMenu = new ToolStripMenuItem("Product Management");

            propertyMenu.Click += (s, e) => OpenForm(new FrmProperty());
            portionMenu.Click += (s, e) => OpenForm(new FrmPortion());
            tenantMenu.Click += (s, e) => OpenForm(new FrmTenant());
            productsMenu.Click += (s, e) => OpenForm(new FrmProducts());

            mastersMenu.DropDownItems.AddRange(new ToolStripItem[] { propertyMenu, portionMenu, tenantMenu, productsMenu });

            // Transactions Menu
            ToolStripMenuItem transactionsMenu = new ToolStripMenuItem("Transactions");
            ToolStripMenuItem agreementMenu = new ToolStripMenuItem("Rent Agreement");
            ToolStripMenuItem rentOverviewMenu = new ToolStripMenuItem("Rent Collection Overview");
            ToolStripMenuItem commissionPaymentMenu = new ToolStripMenuItem("Record Commission Payment");
            ToolStripMenuItem recordPaymentMenu = new ToolStripMenuItem("Record Payment (All Types)");


            agreementMenu.Click += (s, e) => OpenForm(new FrmRentAgreement());
            rentOverviewMenu.Click += (s, e) => OpenForm(new FrmRentCollectionOverview());
            commissionPaymentMenu.Click += (s, e) => OpenCommissionPaymentForm();
            recordPaymentMenu.Click += (s, e) => OpenForm(new FrmRecordPayment());

            transactionsMenu.DropDownItems.AddRange(new ToolStripItem[] {
                agreementMenu, rentOverviewMenu, commissionPaymentMenu, recordPaymentMenu
            });

            // Reports Menu - UPDATED with Tenant Payment History
            ToolStripMenuItem reportsMenu = new ToolStripMenuItem("Reports");

            // Summary Reports
            ToolStripMenuItem monthlyReportMenu = new ToolStripMenuItem("📊 Monthly Summary Report");
            ToolStripMenuItem dueReportMenu = new ToolStripMenuItem("⚠️ Due Report");

            // Tenant Reports
            ToolStripMenuItem tenantReportsMenu = new ToolStripMenuItem("👥 Tenant Reports");
            ToolStripMenuItem tenantPaymentHistoryMenu = new ToolStripMenuItem("🧾 Tenant Payment History");
            ToolStripMenuItem tenantLedgerMenu = new ToolStripMenuItem("📒 Tenant Ledger Report");
            ToolStripMenuItem tenantDueStatementMenu = new ToolStripMenuItem("📝 Tenant Due Statement");

            tenantPaymentHistoryMenu.Click += (s, e) => OpenForm(new FrmTenantPaymentHistory());
            tenantLedgerMenu.Click += (s, e) => OpenForm(new FrmTenantLedgerReport());
            tenantDueStatementMenu.Click += (s, e) => OpenForm(new FrmTenantDueStatement());

            tenantReportsMenu.DropDownItems.AddRange(new ToolStripItem[] {
                tenantPaymentHistoryMenu, tenantLedgerMenu, tenantDueStatementMenu
            });

            // Property Reports
            ToolStripMenuItem propertyReportsMenu = new ToolStripMenuItem("🏢 Property Reports");
            ToolStripMenuItem propertyReportMenu = new ToolStripMenuItem("Property Summary Report");
            ToolStripMenuItem occupancyReportMenu = new ToolStripMenuItem("Occupancy Report");
            ToolStripMenuItem incomeReportMenu = new ToolStripMenuItem("Property Income Report");

            propertyReportMenu.Click += (s, e) => OpenForm(new FrmPropertyReport());
            occupancyReportMenu.Click += (s, e) => OpenOccupancyReport();
            incomeReportMenu.Click += (s, e) => OpenPropertyIncomeReport();

            propertyReportsMenu.DropDownItems.AddRange(new ToolStripItem[] {
                propertyReportMenu, occupancyReportMenu, incomeReportMenu
            });

            // Financial Reports
            ToolStripMenuItem financialReportsMenu = new ToolStripMenuItem("💰 Financial Reports");
            ToolStripMenuItem collectionReportMenu = new ToolStripMenuItem("Collection Efficiency Report");
            ToolStripMenuItem revenueReportMenu = new ToolStripMenuItem("Revenue Analysis Report");
            ToolStripMenuItem outstandingReportMenu = new ToolStripMenuItem("Outstanding Dues Report");

            collectionReportMenu.Click += (s, e) => OpenCollectionReport();
            revenueReportMenu.Click += (s, e) => OpenRevenueReport();
            outstandingReportMenu.Click += (s, e) => OpenOutstandingReport();

            financialReportsMenu.DropDownItems.AddRange(new ToolStripItem[] {
                collectionReportMenu, revenueReportMenu, outstandingReportMenu
            });

            // Commission Reports
            ToolStripMenuItem commissionReportsMenu = new ToolStripMenuItem("📈 Commission Reports");
            ToolStripMenuItem commissionSummaryMenu = new ToolStripMenuItem("Commission Summary Report");
            ToolStripMenuItem productSalesMenu = new ToolStripMenuItem("Product Sales Report");
            ToolStripMenuItem commissionDueMenu = new ToolStripMenuItem("Commission Due Report");

            commissionSummaryMenu.Click += (s, e) => OpenCommissionSummary();
            productSalesMenu.Click += (s, e) => OpenProductSalesReport();
            commissionDueMenu.Click += (s, e) => OpenCommissionDueReport();

            commissionReportsMenu.DropDownItems.AddRange(new ToolStripItem[] {
                commissionSummaryMenu, productSalesMenu, commissionDueMenu
            });

            
            // Add all report menus
            monthlyReportMenu.Click += (s, e) => OpenForm(new FrmMonthlySummary());
            dueReportMenu.Click += (s, e) => OpenForm(new FrmDueReport());

            reportsMenu.DropDownItems.AddRange(new ToolStripItem[] {
                monthlyReportMenu,
                dueReportMenu,
                new ToolStripSeparator(),
                tenantReportsMenu,
                propertyReportsMenu,
                new ToolStripSeparator(),
                financialReportsMenu,
                commissionReportsMenu
            });

            // Quick Access Toolbar
            ToolStrip quickAccessToolbar = new ToolStrip
            {
                BackColor = Color.FromArgb(245, 245, 245),
                GripStyle = ToolStripGripStyle.Hidden,
                Dock = DockStyle.Top,
                Padding = new Padding(10, 5, 10, 5)
            };

            // Add quick access buttons
            ToolStripButton btnDashboard = new ToolStripButton
            {
                Text = "🏠 Dashboard",
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue
            };
            //btnDashboard.Click += (s, e) => ShowWelcomeScreen();
            btnDashboard.Click += (s, e) => OpenForm(new FrmDashboard1());

            ToolStripButton btnRentCollection = new ToolStripButton
            {
                Text = "💰 Collect Rent",
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkGreen
            };
            btnRentCollection.Click += (s, e) => OpenForm(new FrmRentCollectionOverview());

            ToolStripButton btnDueReport = new ToolStripButton
            {
                Text = "⚠️ View Dues",
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkRed
            };
            btnDueReport.Click += (s, e) => OpenForm(new FrmDueReport());

            ToolStripButton btnTenantHistory = new ToolStripButton
            {
                Text = "🧾 Tenant History",
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkOrange
            };
            btnTenantHistory.Click += (s, e) => OpenForm(new FrmTenantPaymentHistory());

            quickAccessToolbar.Items.AddRange(new ToolStripItem[] {
                btnDashboard,
                new ToolStripSeparator(),
                btnRentCollection,
                btnDueReport,
                btnTenantHistory
            });

            // Help Menu
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");
            ToolStripMenuItem aboutMenu = new ToolStripMenuItem("About");
            ToolStripMenuItem userGuideMenu = new ToolStripMenuItem("User Guide");
            ToolStripMenuItem shortcutsMenu = new ToolStripMenuItem("Keyboard Shortcuts");

            aboutMenu.Click += (s, e) => ShowAboutDialog();
            userGuideMenu.Click += (s, e) => ShowUserGuide();
            shortcutsMenu.Click += (s, e) => ShowKeyboardShortcuts();

            helpMenu.DropDownItems.AddRange(new ToolStripItem[] {
                userGuideMenu, shortcutsMenu, new ToolStripSeparator(), aboutMenu
            });

            menuStrip.Items.AddRange(new ToolStripItem[] {
                fileMenu, mastersMenu, transactionsMenu, reportsMenu, helpMenu
            });

            // Status Strip
            statusStrip = new StatusStrip();
            statusStrip.BackColor = Color.SteelBlue;
            statusStrip.Font = new Font("Segoe UI", 9);

            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("Ready");
            statusLabel.ForeColor = Color.White;

            ToolStripStatusLabel userLabel = new ToolStripStatusLabel($"User: Administrator");
            userLabel.ForeColor = Color.White;
            userLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            userLabel.BorderStyle = Border3DStyle.Etched;

            ToolStripStatusLabel dateLabel = new ToolStripStatusLabel();
            dateLabel.ForeColor = Color.White;
            dateLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            dateLabel.BorderStyle = Border3DStyle.Etched;

            // Update date every minute
            Timer timer = new Timer { Interval = 60000 };
            timer.Tick += (s, e) => UpdateDateTimeLabel(dateLabel);
            timer.Start();
            UpdateDateTimeLabel(dateLabel);

            statusStrip.Items.AddRange(new ToolStripItem[] {
                statusLabel,
                new ToolStripStatusLabel { Spring = true },
                userLabel,
                dateLabel
            });

            // Welcome Panel
            welcomePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 248, 255),
                Padding = new Padding(40)
            };

            // Create a more attractive welcome screen
            TableLayoutPanel tableLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            // Header
            Label lblHeader = new Label
            {
                Text = "🏪 Shop Portion Rent Management System",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.DarkSlateBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            tableLayout.SetColumnSpan(lblHeader, 2);
            tableLayout.SetRow(lblHeader, 0);

            // Left panel - Features
            Panel featuresPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            Label lblFeatures = new Label
            {
                Text = "🌟 Key Features:\n\n" +
                       "✓ Property Management (Commercial/Non-Commercial)\n" +
                       "✓ Tenant Management (Rent & Commission)\n" +
                       "✓ Automated Rent & Commission Calculations\n" +
                       "✓ Comprehensive Reporting System\n" +
                       "✓ Due Tracking with Alerts\n" +
                       "✓ Payment History & Ledger\n" +
                       "✓ Occupancy & Revenue Analysis\n" +
                       "✓ Export to Excel/PDF",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.DarkSlateGray,
                Dock = DockStyle.Fill
            };

            featuresPanel.Controls.Add(lblFeatures);
            tableLayout.SetColumn(featuresPanel, 0);
            tableLayout.SetRow(featuresPanel, 1);

            // Right panel - Quick Stats
            Panel statsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20)
            };

            Label lblStatsTitle = new Label
            {
                Text = "📊 Quick Statistics",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.SteelBlue,
                Dock = DockStyle.Top,
                Height = 40
            };

            Panel statsContent = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 50, 0, 0)
            };

            Label lblStatsContent = new Label
            {
                Name = "lblQuickStats",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.DimGray,
                Dock = DockStyle.Fill
            };

            statsContent.Controls.Add(lblStatsContent);
            statsPanel.Controls.Add(statsContent);
            statsPanel.Controls.Add(lblStatsTitle);

            tableLayout.SetColumn(statsPanel, 1);
            tableLayout.SetRow(statsPanel, 1);

            // Footer
            Label lblFooter = new Label
            {
                Text = "💡 Tip: Use the menu or quick access toolbar to navigate. Press F1 for help.",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Height = 40
            };
            tableLayout.SetColumnSpan(lblFooter, 2);
            tableLayout.SetRow(lblFooter, 2);

            // Set row heights
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 70));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));

            tableLayout.Controls.Add(lblHeader, 0, 0);
            tableLayout.Controls.Add(featuresPanel, 0, 1);
            tableLayout.Controls.Add(statsPanel, 1, 1);
            tableLayout.Controls.Add(lblFooter, 0, 2);

            welcomePanel.Controls.Add(tableLayout);

            // Add controls to form
            this.Controls.Add(welcomePanel);
            this.Controls.Add(quickAccessToolbar);
            this.Controls.Add(statusStrip);
            this.Controls.Add(menuStrip);

            this.MainMenuStrip = menuStrip;

            // Set keyboard shortcuts
            this.KeyPreview = true;
            this.KeyDown += FrmMain_KeyDown;


            // Add this after the Transactions Menu or before Reports Menu
            ToolStripMenuItem expenseMenu = new ToolStripMenuItem("Expenses");

            ToolStripMenuItem expenseDashboardMenu = new ToolStripMenuItem("📊 Expense Dashboard");
            expenseDashboardMenu.Click += (s, e) => OpenForm(new FrmExpenseDashboard());

            ToolStripMenuItem expenseManagementMenu = new ToolStripMenuItem("💰 Manage Expenses");
            expenseManagementMenu.Click += (s, e) => OpenForm(new FrmExpenses());

            ToolStripMenuItem expenseReportMenu = new ToolStripMenuItem("📈 Expense Reports");
            expenseReportMenu.Click += (s, e) => OpenForm(new FrmExpenseReport());

            ToolStripMenuItem expenseCategoriesMenu = new ToolStripMenuItem("🏷️ Expense Categories");
            expenseCategoriesMenu.Click += (s, e) => ShowExpenseCategories();

            expenseMenu.DropDownItems.AddRange(new ToolStripItem[] {
                    expenseDashboardMenu,
                    expenseManagementMenu,
                    expenseReportMenu,
                    new ToolStripSeparator(),
                    expenseCategoriesMenu
                });
            // Add to menuStrip.Items before reportsMenu
            menuStrip.Items.Insert(3, expenseMenu); // Insert at position 3 (after Transactions)

            // Also add to Quick Access Toolbar
            ToolStripButton btnExpenseDashboard = new ToolStripButton
            {
                Text = "💰 Expenses",
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.DarkRed
            };
            btnExpenseDashboard.Click += (s, e) => OpenForm(new FrmExpenseDashboard());

            // Add to quickAccessToolbar after btnTenantHistory
            quickAccessToolbar.Items.Insert(quickAccessToolbar.Items.Count - 1, btnExpenseDashboard);


            // Load quick stats
            LoadQuickStats();
        }



        private void ShowExpenseCategories()
        {
            string categories = "📋 Expense Categories\n\n";
            foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
            {
                categories += $"• {GetCategoryDisplayName(category)}\n";
            }

            MessageBox.Show(categories, "Expense Categories",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetCategoryDisplayName(ExpenseCategory category)
        {
            return category switch
            {
                ExpenseCategory.Utilities => "Utilities (Electricity, Water, Gas)",
                ExpenseCategory.Maintenance => "Maintenance & Repairs",
                ExpenseCategory.Insurance => "Insurance Premiums",
                ExpenseCategory.Taxes => "Property Taxes",
                ExpenseCategory.Cleaning => "Cleaning Services",
                ExpenseCategory.Security => "Security Services",
                ExpenseCategory.Marketing => "Marketing & Advertising",
                ExpenseCategory.ProfessionalFees => "Professional Fees (Legal, Accounting)",
                ExpenseCategory.Supplies => "Office Supplies",
                ExpenseCategory.Salaries => "Salaries & Wages",
                ExpenseCategory.Equipment => "Equipment Purchase/Lease",
                ExpenseCategory.Miscellaneous => "Miscellaneous",
                _ => category.ToString()
            };
        }
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
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
                case Keys.Control | Keys.D:
                    OpenForm(new FrmDueReport());
                    break;
                case Keys.Control | Keys.T:
                    OpenForm(new FrmTenantPaymentHistory());
                    break;
                case Keys.Control | Keys.R:
                    OpenForm(new FrmRentCollectionOverview());
                    break;
            }
        }

        private void UpdateDateTimeLabel(ToolStripStatusLabel label)
        {
            label.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
        }

        private void LoadQuickStats()
        {
            try
            {
                var dataService = new Services.JsonDataService();
                var cashFlowService = new CashFlowService();
                var lblQuickStats = welcomePanel?.Controls.Find("lblQuickStats", true).FirstOrDefault() as Label;

                if (lblQuickStats != null)
                {
                    var properties = dataService.LoadProperties();
                    var tenants = dataService.LoadTenants();
                    var agreements = dataService.LoadAgreements().Where(a => a.IsActive).ToList();

                    // Get current month cash flow
                    var now = DateTime.Now;
                    var monthRange = new DateRange(
                        new DateTime(now.Year, now.Month, 1),
                        now
                    );
                    var cashFlow = cashFlowService.GetCashFlowSummary(monthRange);

                    int totalProperties = properties.Count;
                    int totalTenants = tenants.Count;
                    int activeAgreements = agreements.Count;

                    int rentTenants = tenants.Count(t => t.Type == TenantType.OnRent);
                    int commissionTenants = tenants.Count(t => t.Type == TenantType.OnCommission);

                    lblQuickStats.Text = $"🏢 Properties: {totalProperties}\n" +
                                        $"👥 Total Tenants: {totalTenants}\n" +
                                        $"   • Rent Tenants: {rentTenants}\n" +
                                        $"   • Commission Tenants: {commissionTenants}\n" +
                                        $"📝 Active Agreements: {activeAgreements}\n" +
                                        $"💰 Cash Flow (This Month):\n" +
                                        $"   • Cash In: {cashFlow.TotalCashIn:C}\n" +
                                        $"   • Cash Out: {cashFlow.TotalCashOut:C}\n" +
                                        $"   • Net: {cashFlow.NetCashFlow:C}\n" +
                                        $"📅 Date: {DateTime.Now:dd MMM yyyy}";
                }
            }
            catch
            {
                // Ignore errors in quick stats
            }
        }
        /*private void LoadQuickStats()
        {
            try
            {
                var dataService = new Services.JsonDataService();
                var lblQuickStats = welcomePanel?.Controls.Find("lblQuickStats", true).FirstOrDefault() as Label;

                if (lblQuickStats != null)
                {
                    var properties = dataService.LoadProperties();
                    var tenants = dataService.LoadTenants();
                    var agreements = dataService.LoadAgreements().Where(a => a.IsActive).ToList();
                    var payments = dataService.LoadPayments()
                        .Where(p => p.PaymentDate.Month == DateTime.Now.Month)
                        .ToList();

                    int totalProperties = properties.Count;
                    int totalTenants = tenants.Count;
                    int activeAgreements = agreements.Count;
                    decimal monthlyCollection = payments.Sum(p => p.Amount);

                    int rentTenants = tenants.Count(t => t.Type == TenantType.OnRent);
                    int commissionTenants = tenants.Count(t => t.Type == TenantType.OnCommission);

                    lblQuickStats.Text = $"🏢 Properties: {totalProperties}\n" +
                                        $"👥 Total Tenants: {totalTenants}\n" +
                                        $"   • Rent Tenants: {rentTenants}\n" +
                                        $"   • Commission Tenants: {commissionTenants}\n" +
                                        $"📝 Active Agreements: {activeAgreements}\n" +
                                        $"💰 This Month's Collection: {monthlyCollection:C}\n" +
                                        $"📅 Date: {DateTime.Now:dd MMM yyyy}";
                }
            }
            catch
            {
                // Ignore errors in quick stats
            }
        }
        */
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
                }
            };
            form.Show();
        }

        private void OpenCommissionPaymentForm()
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Select Agreement for Commission Payment";
                dialog.Size = new Size(400, 300);
                dialog.StartPosition = FormStartPosition.CenterParent;

                var dataService = new Services.JsonDataService();
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

        private void ShowWelcomeScreen()
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
            welcomePanel.Visible = true;
            LoadQuickStats();
        }

        // Replace placeholder methods with actual implementations
        private void OpenMonthlySummary()
        {
            OpenForm(new FrmMonthlySummary());
        }

        private void OpenDueReport()
        {
            OpenForm(new FrmDueReport());
        }

        private void OpenTenantPaymentHistory()
        {
            // Show dialog to select tenant first
            using (var dialog = new Form())
            {
                dialog.Text = "Select Tenant";
                dialog.Size = new Size(400, 200);
                dialog.StartPosition = FormStartPosition.CenterParent;

                var dataService = new JsonDataService();
                var tenants = dataService.LoadTenants();

                Label lblSelect = new Label
                {
                    Text = "Select Tenant:",
                    Location = new Point(20, 20),
                    Size = new Size(350, 25)
                };

                ComboBox cmbTenants = new ComboBox
                {
                    Location = new Point(20, 50),
                    Size = new Size(350, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    DisplayMember = "Name"
                };

                cmbTenants.DataSource = tenants;

                Button btnOpen = new Button
                {
                    Text = "View Payment History",
                    Location = new Point(20, 90),
                    Size = new Size(150, 35),
                    BackColor = Color.SteelBlue,
                    ForeColor = Color.White
                };

                btnOpen.Click += (s, e) =>
                {
                    if (cmbTenants.SelectedItem is Tenant selectedTenant)
                    {
                        var reportForm = new FrmTenantPaymentHistory(selectedTenant.Id);
                        reportForm.MdiParent = this;
                        reportForm.Show();
                        dialog.Close();
                    }
                };

                dialog.Controls.AddRange(new Control[] { lblSelect, cmbTenants, btnOpen });
                dialog.ShowDialog();
            }
        }

        // Create similar implementations for other report forms
        private void OpenTenantLedgerReport()
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Select Tenant for Ledger";
                dialog.Size = new Size(400, 200);
                dialog.StartPosition = FormStartPosition.CenterParent;

                var dataService = new JsonDataService();
                var tenants = dataService.LoadTenants();

                Label lblSelect = new Label
                {
                    Text = "Select Tenant:",
                    Location = new Point(20, 20),
                    Size = new Size(350, 25)
                };

                ComboBox cmbTenants = new ComboBox
                {
                    Location = new Point(20, 50),
                    Size = new Size(350, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    DisplayMember = "Name"
                };

                cmbTenants.DataSource = tenants;

                Button btnOpen = new Button
                {
                    Text = "View Ledger",
                    Location = new Point(20, 90),
                    Size = new Size(150, 35),
                    BackColor = Color.DarkSlateBlue,
                    ForeColor = Color.White
                };

                btnOpen.Click += (s, e) =>
                {
                    if (cmbTenants.SelectedItem is Tenant selectedTenant)
                    {
                        // You need to create FrmTenantLedgerReport form
                        // For now, generate and open HTML report
                        var reportGenerator = new ReportGenerator(new JsonDataService());
                        reportGenerator.GenerateAndOpenTenantLedger(selectedTenant.Id);
                        dialog.Close();
                    }
                };

                dialog.Controls.AddRange(new Control[] { lblSelect, cmbTenants, btnOpen });
                dialog.ShowDialog();
            }
        }

        // Update other report menu items to use ReportGenerator
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

        // Placeholder methods for new reports (to be implemented)

        private void ShowAboutDialog()
        {
            MessageBox.Show(
                "🏪 Shop Portion Rent Management System v2.0\n\n" +
                "📅 Version: 2.0.0\n" +
                "📦 Build Date: " + DateTime.Now.ToString("yyyy-MM-dd") + "\n" +
                "👨‍💻 Developer: Your Company\n\n" +
                "📋 Features:\n" +
                "• Property Management (Commercial/Non-Commercial)\n" +
                "• Tenant Types (Rent/Commission) Support\n" +
                "• Automated Rent Increase Calculations\n" +
                "• Commission Payment Tracking\n" +
                "• Complete Reporting System\n" +
                "• Payment History & Ledger\n" +
                "• Due Tracking with Alerts\n\n" +
                "📞 Support: support@yourcompany.com",
                "About System", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowUserGuide()
        {
            MessageBox.Show(
                "📘 User Guide\n\n" +
                "1. Masters:\n" +
                "   • Add properties, portions, tenants, and products\n\n" +
                "2. Transactions:\n" +
                "   • Create rent agreements\n" +
                "   • Record rent and commission payments\n\n" +
                "3. Reports:\n" +
                "   • View monthly summaries\n" +
                "   • Check due reports\n" +
                "   • Review tenant payment history\n\n" +
                "4. Keyboard Shortcuts:\n" +
                "   • F1: This help guide\n" +
                "   • F5: Refresh dashboard\n" +
                "   • Esc: Close current window\n" +
                "   • Ctrl+D: Due report\n" +
                "   • Ctrl+T: Tenant history\n" +
                "   • Ctrl+R: Rent collection",
                "User Guide", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowKeyboardShortcuts()
        {
            MessageBox.Show(
                "⌨️ Keyboard Shortcuts\n\n" +
                "Global Shortcuts:\n" +
                "• F1: Show help\n" +
                "• F5: Refresh dashboard\n" +
                "• Esc: Close current window\n\n" +
                "Quick Access:\n" +
                "• Ctrl + D: Due Report\n" +
                "• Ctrl + T: Tenant Payment History\n" +
                "• Ctrl + R: Rent Collection\n\n" +
                "Form Navigation:\n" +
                "• Tab: Next control\n" +
                "• Shift+Tab: Previous control\n" +
                "• Enter: Accept/Save\n" +
                "• Ctrl+S: Save (where applicable)",
                "Keyboard Shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

