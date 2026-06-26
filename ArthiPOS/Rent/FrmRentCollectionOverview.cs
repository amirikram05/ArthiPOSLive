using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using ArthiPOS.Controls;
using DevExpress.Utils.Serializing;
using EnvDTE;

namespace ShopRentManagementSystem
{
    public partial class FrmRentCollectionOverview : Form
    {
        private readonly JsonDataService _dataService;
        private DataGridView dgvRentOverview;
        private Button btnRefresh;
        private Button btnCollectPayment;
        private Button btnPaymentHistory;
        private Button btnExportExcel;
        private TextBox txtSearch;
        private ComboBox cmbFilter;
        private Button btnCommissionPayment;

        // Store original data for filtering
        private List<RentOverview> _allData = new List<RentOverview>();

        public FrmRentCollectionOverview()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            SetupDataGridView(); // Only call once
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Rent Collection Overview";
            this.Size = new Size(1300, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = SystemColors.Control;

            // Search Panel
            var pnlSearch = new Panel
            {
                Height = 50,
                Dock = DockStyle.Top,
                BackColor = Color.LightGray,
                Padding = new Padding(10)
            };

            txtSearch = new UrduTextBox
            {
                WaterMarkText = "Search by tenant, property, mobile...",
                Width = 250,
                Location = new Point(10, 10)
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;

            cmbFilter = new ComboBox
            {
                Width = 150,
                Location = new Point(270, 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmbFilter.Items.AddRange(new[] { "All", "On Rent", "On Commission", "Due Only", "Paid" });
            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;

            // Add a label for better UX
            Label lblFilter = new Label
            {
                Text = "Filter:",
                Location = new Point(220, 13),
                Size = new Size(45, 20),
                Font = new Font("Segoe UI", 9)
            };

            pnlSearch.Controls.AddRange(new Control[] { txtSearch, lblFilter, cmbFilter });

            // Data Grid View
            dgvRentOverview = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.Fixed3D,
                Font = new Font("Segoe UI", 9),
                AutoGenerateColumns = false // IMPORTANT: Prevent auto column generation
            };

            // Buttons Panel
            var pnlButtons = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.LightGray,
                Padding = new Padding(10)
            };

            btnRefresh = new Button
            {
                Text = "?? Refresh",
                Location = new Point(10, 10),
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 9),
                BackColor = SystemColors.ControlLight
            };
            btnRefresh.Click += BtnRefresh_Click;

            btnCollectPayment = new Button
            {
                Text = "?? Collect Payment",
                Location = new Point(120, 10),
                Size = new Size(130, 35),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnCollectPayment.Click += BtnCollectPayment_Click;

            // Add missing commission payment button
            btnCommissionPayment = new Button
            {
                Text = "?? Commission Payment",
                Location = new Point(260, 10),
                Size = new Size(150, 35),
                BackColor = Color.LightGoldenrodYellow,
                Font = new Font("Segoe UI", 9)
            };
            btnCommissionPayment.Click += BtnCommissionPayment_Click;

            btnPaymentHistory = new Button
            {
                Text = "?? Payment History",
                Location = new Point(420, 10),
                Size = new Size(130, 35),
                BackColor = Color.LightBlue,
                Font = new Font("Segoe UI", 9)
            };
            btnPaymentHistory.Click += BtnPaymentHistory_Click;

            btnExportExcel = new Button
            {
                Text = "?? Export to Excel",
                Location = new Point(560, 10),
                Size = new Size(130, 35),
                BackColor = Color.LightSteelBlue,
                Font = new Font("Segoe UI", 9)
            };
            btnExportExcel.Click += BtnExportExcel_Click;

            pnlButtons.Controls.AddRange(new Control[] {
                btnRefresh, btnCollectPayment, btnCommissionPayment,
                btnPaymentHistory, btnExportExcel
            });

            this.Controls.AddRange(new Control[] { dgvRentOverview, pnlSearch, pnlButtons });

            // Don't call SetupDataGridView here - call it in constructor
        }

        private void SetupDataGridView()
        {
            // Clear existing columns if any
            dgvRentOverview.Columns.Clear();

            var columns = new[]
            {
                new DataGridViewTextBoxColumn {
                    Name = "PropertyName",
                    HeaderText = "?? Property",
                    Width = 120,
                    DataPropertyName = "PropertyName" // Bind to property name
                },
                new DataGridViewTextBoxColumn {
                    Name = "PortionName",
                    HeaderText = "?? Portion",
                    Width = 80,
                    DataPropertyName = "PortionName"
                },
                new DataGridViewTextBoxColumn {
                    Name = "PortionSize",
                    HeaderText = "?? Size",
                    Width = 80,
                    DataPropertyName = "PortionSize"
                },
                new DataGridViewTextBoxColumn {
                    Name = "TenantName",
                    HeaderText = "?? Tenant",
                    Width = 150,
                    DataPropertyName = "TenantName"
                },
                new DataGridViewTextBoxColumn {
                    Name = "Mobile",
                    HeaderText = "?? Mobile",
                    Width = 100,
                    DataPropertyName = "Mobile"
                },
                new DataGridViewTextBoxColumn {
                    Name = "TenantType",
                    HeaderText = "?? Type",
                    Width = 90,
                    DataPropertyName = "TenantType"
                },
                new DataGridViewTextBoxColumn {
                    Name = "PaymentInfo",
                    HeaderText = "?? Payment Info",
                    Width = 120,
                    DataPropertyName = "PaymentInfo"
                },
                new DataGridViewTextBoxColumn {
                    Name = "MonthlyRent",
                    HeaderText = "??? Monthly Rent",
                    Width = 100,
                    DataPropertyName = "MonthlyRent"
                },
                new DataGridViewTextBoxColumn {
                    Name = "LastPaidAmount",
                    HeaderText = "?? Last Paid",
                    Width = 100,
                    DataPropertyName = "LastPaidAmount"
                },
                new DataGridViewTextBoxColumn {
                    Name = "DueAmount",
                    HeaderText = "?? Due Amount",
                    Width = 100,
                    DataPropertyName = "DueAmount"
                },
                new DataGridViewTextBoxColumn {
                    Name = "LastPaymentDate",
                    HeaderText = "?? Last Payment",
                    Width = 120,
                    DataPropertyName = "LastPaymentDate"
                },
                new DataGridViewTextBoxColumn {
                    Name = "NextDueDate",
                    HeaderText = "? Next Due",
                    Width = 120,
                    DataPropertyName = "NextDueDate"
                },
                new DataGridViewTextBoxColumn {
                    Name = "StatusColor",
                    HeaderText = "?? Status",
                    Width = 80,
                    DataPropertyName = "StatusColor"
                },
                // Hidden columns
                new DataGridViewTextBoxColumn {
                    Name = "AgreementId",
                    HeaderText = "Agreement ID",
                    Visible = false,
                    DataPropertyName = "AgreementId"
                },
                new DataGridViewTextBoxColumn {
                    Name = "TenantId",
                    HeaderText = "Tenant ID",
                    Visible = false,
                    DataPropertyName = "TenantId"
                },
                new DataGridViewTextBoxColumn {
                    Name = "PropertyId",
                    HeaderText = "Property ID",
                    Visible = false,
                    DataPropertyName = "PropertyId"
                },
                new DataGridViewTextBoxColumn {
                    Name = "PortionId",
                    HeaderText = "Portion ID",
                    Visible = false,
                    DataPropertyName = "PortionId"
                },
                new DataGridViewTextBoxColumn {
                    Name = "DaysOverdue",
                    HeaderText = "Days Overdue",
                    Visible = false,
                    DataPropertyName = "DaysOverdue"
                }
            };

            dgvRentOverview.Columns.AddRange(columns);

            // Format columns
            dgvRentOverview.Columns["MonthlyRent"].DefaultCellStyle.Format = "C";
            dgvRentOverview.Columns["LastPaidAmount"].DefaultCellStyle.Format = "C";
            dgvRentOverview.Columns["DueAmount"].DefaultCellStyle.Format = "C";
            dgvRentOverview.Columns["MonthlyRent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRentOverview.Columns["LastPaidAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRentOverview.Columns["DueAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvRentOverview.Columns["LastPaymentDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dgvRentOverview.Columns["NextDueDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

            // Style headers
            dgvRentOverview.EnableHeadersVisualStyles = false;
            dgvRentOverview.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvRentOverview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRentOverview.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvRentOverview.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Cell formatting for colors
            dgvRentOverview.CellFormatting += DgvRentOverview_CellFormatting;

            // Double-click to collect payment
            dgvRentOverview.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    CollectPaymentForSelectedRow();
                }
            };
        }

        private void DgvRentOverview_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvRentOverview.Rows.Count)
            {
                var row = dgvRentOverview.Rows[e.RowIndex];

                // Tenant type coloring
                if (e.ColumnIndex == dgvRentOverview.Columns["TenantType"].Index && row.Cells["TenantType"].Value != null)
                {
                    string tenantType = row.Cells["TenantType"].Value.ToString();
                    if (tenantType == "OnCommission")
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 255, 200);
                        e.CellStyle.ForeColor = Color.DarkGoldenrod;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.FromArgb(230, 255, 230);
                        e.CellStyle.ForeColor = Color.DarkGreen;
                    }
                }

                // Status color
                if (e.ColumnIndex == dgvRentOverview.Columns["StatusColor"].Index && e.Value != null)
                {
                    string status = e.Value.ToString();
                    if (status == "Red")
                    {
                        e.CellStyle.BackColor = Color.LightPink;
                        e.CellStyle.ForeColor = Color.DarkRed;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                    else if (status == "Green")
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.DarkGreen;
                    }
                    else if (status == "Orange")
                    {
                        e.CellStyle.BackColor = Color.Orange;
                        e.CellStyle.ForeColor = Color.DarkRed;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                // Due amount coloring
                if (e.ColumnIndex == dgvRentOverview.Columns["DueAmount"].Index && e.Value != null)
                {
                    if (decimal.TryParse(e.Value.ToString(), out decimal due) && due > 0)
                    {
                        e.CellStyle.BackColor = Color.LightPink;
                        e.CellStyle.ForeColor = Color.DarkRed;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                // Alternate row colors for better readability
                if (e.RowIndex % 2 == 0)
                {
                    e.CellStyle.BackColor = Color.FromArgb(250, 250, 250);
                }
            }
        }

        public void LoadData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                _allData = _dataService.GetAllRentOverviews();

                // Calculate days overdue for each tenant
                foreach (var overview in _allData)
                {
                    if (overview.DueAmount > 0 && overview.NextDueDate < DateTime.Now)
                    {
                        overview.DaysOverdue = (DateTime.Now - overview.NextDueDate).Days;
                    }
                    else
                    {
                        overview.DaysOverdue = 0;
                    }
                }

                // Apply current filter
                ApplyFilter();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void RefreshData()
        {
            LoadData();
        }

        private void BtnCollectPayment_Click(object sender, EventArgs e)
        {
            CollectPaymentForSelectedRow();
        }

        private void CollectPaymentForSelectedRow()
        {
            if (dgvRentOverview.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a tenant to collect payment.", "Select Tenant",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvRentOverview.SelectedRows[0];
            ProcessPayment(selectedRow);
        }

        private void ProcessPayment(DataGridViewRow selectedRow)
        {
            try
            {
                var tenantId = Convert.ToInt32(selectedRow.Cells["TenantId"].Value);
                var propertyId = Convert.ToInt32(selectedRow.Cells["PropertyId"].Value);
                var portionId = Convert.ToInt32(selectedRow.Cells["PortionId"].Value);
                var agreementId = Convert.ToInt32(selectedRow.Cells["AgreementId"].Value);
                var dueAmount = Convert.ToDecimal(selectedRow.Cells["DueAmount"].Value);
                var tenantType = selectedRow.Cells["TenantType"].Value?.ToString();
                var tenantName = selectedRow.Cells["TenantName"].Value?.ToString();

                if (tenantType == "OnCommission")
                {
                    // Ask user what type of payment for commission tenants
                    var result = MessageBox.Show($"Tenant '{tenantName}' is on commission.\n\n" +
                        "Do you want to:\n" +
                        "• Record a regular payment?\n" +
                        "• Record a commission payment?",
                        "Commission Tenant Payment",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Regular payment
                        var paymentForm = new FrmCollectPayment(agreementId, tenantId, propertyId, portionId, dueAmount);
                        paymentForm.Owner = this;
                        paymentForm.FormClosed += (s, args) => LoadData();
                        paymentForm.ShowDialog();
                    }
                    else if (result == DialogResult.No)
                    {
                        // Commission payment
                        var commissionForm = new FrmCommissionPayment(agreementId);
                        commissionForm.Owner = this;
                        commissionForm.FormClosed += (s, args) => LoadData();
                        commissionForm.ShowDialog();
                    }
                }
                else
                {
                    // Regular rent payment
                    var paymentForm = new FrmCollectPayment(agreementId, tenantId, propertyId, portionId, dueAmount);
                    paymentForm.Owner = this;
                    paymentForm.FormClosed += (s, args) => LoadData();
                    paymentForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing payment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCommissionPayment_Click(object sender, EventArgs e)
        {
            if (dgvRentOverview.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a commission tenant.", "Select Tenant",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvRentOverview.SelectedRows[0];
            var tenantType = selectedRow.Cells["TenantType"].Value?.ToString();

            if (tenantType != "OnCommission")
            {
                MessageBox.Show("Selected tenant is not on commission. Please select a commission tenant.",
                    "Not a Commission Tenant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var agreementId = Convert.ToInt32(selectedRow.Cells["AgreementId"].Value);
                var commissionForm = new FrmCommissionPayment(agreementId);
                commissionForm.Owner = this;
                commissionForm.FormClosed += (s, args) => LoadData();
                commissionForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening commission form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnPaymentHistory_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1: Check DataGridView
                if (dgvRentOverview == null)
                {
                    MessageBox.Show("DataGridView is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 2: Check selected rows
                if (dgvRentOverview.SelectedRows == null || dgvRentOverview.SelectedRows.Count == 0)
                {
                    MessageBox.Show("No row selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var selectedRow = dgvRentOverview.SelectedRows[0];

                // Step 3: Check AgreementId cell
                if (selectedRow.Cells["AgreementId"] == null)
                {
                    MessageBox.Show("AgreementId column not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 4: Check cell value
                var agreementIdValue = selectedRow.Cells["AgreementId"].Value;
                if (agreementIdValue == null)
                {
                    MessageBox.Show("AgreementId value is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 5: Parse the value
                int agreementId = Convert.ToInt32(agreementIdValue);

                // Continue with your code...
                string tenantName = selectedRow.Cells["TenantName"].Value?.ToString() ?? "Selected Tenant";
                ShowPaymentHistoryChoiceDialog(agreementId, 0,tenantName);
            }
            catch (NullReferenceException nre)
            {
                MessageBox.Show($"Null Reference Error: {nre.Message}\n\nSource: {nre.Source}\n\nTarget Site: {nre.TargetSite}",
                    "Null Reference Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\nStack Trace: {ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /*private void BtnPaymentHistory_Click(object sender, EventArgs e)
        {
            if (dgvRentOverview.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a tenant to view payment history.",
                    "Select Tenant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var selectedRow = dgvRentOverview.SelectedRows[0];
                int agreementId = 0, tenantId=0, propertyId=0, portionId=0;
                string tenantName = "";

                if (dgvRentOverview.Columns.Contains("AgreementId") &&
                    selectedRow.Cells["AgreementId"].Value != null)
                {
                    agreementId = Convert.ToInt32(selectedRow.Cells["AgreementId"].Value);
                }
                else
                {
                     tenantId = Convert.ToInt32(selectedRow.Cells["TenantId"].Value);
                     propertyId = Convert.ToInt32(selectedRow.Cells["PropertyId"].Value);
                     portionId = Convert.ToInt32(selectedRow.Cells["PortionId"].Value);
                     tenantName = selectedRow.Cells["TenantName"].Value.ToString();

                    var agreements = _dataService.LoadAgreements();
                    var agreement = agreements.FirstOrDefault(a =>
                        a.TenantId == tenantId &&
                        a.PropertyId == propertyId &&
                        a.PortionId == portionId &&
                        a.IsActive);

                    if (agreement != null)
                    {
                        agreementId = agreement.Id;
                    }
                }

                if (agreementId > 0)
                {
                    //var paymentHistoryForm = new FrmPaymentHistory(agreementId, tenantId, propertyId);
                    //paymentHistoryForm.Owner = this;
                    //paymentHistoryForm.StartPosition = FormStartPosition.CenterParent;

                    //if (paymentHistoryForm.ShowDialog() == DialogResult.OK)
                    //{
                    //    LoadData();
                    //}
                    ShowSimplePaymentHistoryChoice(agreementId, tenantName);
                }
                else
                {
                    MessageBox.Show("Could not find rent agreement for selected tenant.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening payment history: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       */
        private void ShowSimplePaymentHistoryChoice(int agreementId, string tenantName)
        {
            var result = MessageBox.Show($"Do you want to view payment history for:\n\n" +
                                        $"1. Only {tenantName}?\n" +
                                        $"2. All tenants?\n\n" +
                                        $"Click 'Yes' for selected tenant, 'No' for all tenants, or 'Cancel' to go back.",
                                        "View Payment History",
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button1);

            switch (result)
            {
                case DialogResult.Yes:
                    // Show selected tenant's payment history
                    var paymentHistoryForm = new FrmPaymentHistory(agreementId);
                    paymentHistoryForm.Owner = this;
                    paymentHistoryForm.StartPosition = FormStartPosition.CenterParent;

                    if (paymentHistoryForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                    break;

                case DialogResult.No:
                    // Show ALL payments history
                    var allPaymentsForm = new FrmPaymentHistory(0);
                    allPaymentsForm.Owner = this;
                    allPaymentsForm.StartPosition = FormStartPosition.CenterParent;
                    allPaymentsForm.ShowDialog();
                    break;

                case DialogResult.Cancel:
                    // User cancelled
                    break;
            }
        }
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv",
                    FileName = $"Rent_Collection_Overview_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                    Title = "Export Rent Collection Overview",
                    DefaultExt = "xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // For CSV export
                    if (saveDialog.FileName.EndsWith(".csv"))
                    {
                        ExportToCsv(saveDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show($"Excel export requires EPPlus library.\n\n" +
                            "Add NuGet package: Install-Package EPPlus\n" +
                            "Then implement Excel export functionality.",
                            "Export Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error preparing export: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowPaymentHistoryChoiceDialog(int agreementId, int tenantId, string tenantName)
        {
            using (var choiceDialog = new Form())
            {
                choiceDialog.Text = "View Payment History";
                choiceDialog.Size = new Size(450, 250);
                choiceDialog.StartPosition = FormStartPosition.CenterParent;
                choiceDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                choiceDialog.MaximizeBox = false;
                choiceDialog.MinimizeBox = false;
                choiceDialog.BackColor = Color.White;

                // Icon
                PictureBox historyIcon = new PictureBox
                {
                    Image = SystemIcons.Question.ToBitmap(),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(48, 48),
                    Location = new Point(20, 20)
                };

                // Question
                Label lblQuestion = new Label
                {
                    Text = "What would you like to view?",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.SteelBlue,
                    Location = new Point(80, 20),
                    Size = new Size(350, 25)
                };

                // Tenant info
                Label lblTenantInfo = new Label
                {
                    Text = $"Selected Tenant: {tenantName}",
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    ForeColor = Color.DarkSlateGray,
                    Location = new Point(80, 50),
                    Size = new Size(350, 20)
                };

                // Option 1: Selected tenant only
                RadioButton rbSelectedTenant = new RadioButton
                {
                    Text = $"📋 View only {tenantName}'s payment history",
                    Location = new Point(80, 80),
                    Size = new Size(350, 25),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Checked = true // Default selection
                };

                // Option 2: All payments
                RadioButton rbAllPayments = new RadioButton
                {
                    Text = "💰 View ALL payments history (all tenants)",
                    Location = new Point(80, 110),
                    Size = new Size(350, 25),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };

                // Option 3: Both (show comparison)
                RadioButton rbBoth = new RadioButton
                {
                    Text = "📊 Compare selected tenant with all payments",
                    Location = new Point(80, 140),
                    Size = new Size(350, 25),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };

                // Buttons
                Button btnView = new Button
                {
                    Text = "👁️ View",
                    Location = new Point(150, 175),
                    Size = new Size(100, 30),
                    BackColor = Color.LightBlue,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };

                Button btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(260, 175),
                    Size = new Size(80, 30),
                    DialogResult = DialogResult.Cancel
                };

                btnView.Click += (s, e) =>
                {
                    if (rbSelectedTenant.Checked)
                    {
                        // Show selected tenant's payment history
                        var paymentHistoryForm = new FrmPaymentHistory(agreementId);
                        paymentHistoryForm.Owner = this;
                        paymentHistoryForm.StartPosition = FormStartPosition.CenterParent;

                        if (paymentHistoryForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadData();
                        }
                    }
                    else if (rbAllPayments.Checked)
                    {
                        // Show ALL payments history
                        var allPaymentsForm = new FrmPaymentHistory(0); // 0 shows all payments
                        allPaymentsForm.Owner = this;
                        allPaymentsForm.StartPosition = FormStartPosition.CenterParent;
                        allPaymentsForm.ShowDialog();
                    }
                    else if (rbBoth.Checked)
                    {
                        // Show both in comparison view
                        ShowComparisonView(agreementId, tenantId, tenantName);
                    }

                    choiceDialog.DialogResult = DialogResult.OK;
                };

                choiceDialog.AcceptButton = btnView;
                choiceDialog.CancelButton = btnCancel;

                choiceDialog.Controls.AddRange(new Control[] {
            historyIcon, lblQuestion, lblTenantInfo,
            rbSelectedTenant, rbAllPayments, rbBoth,
            btnView, btnCancel
        });

                choiceDialog.ShowDialog();
            }
        }

        private void ShowComparisonView(int agreementId, int tenantId, string tenantName)
        {
            try
            {
                // Create a comparison form
                using (var comparisonForm = new Form())
                {
                    comparisonForm.Text = $"Payment Comparison - {tenantName} vs All";
                    comparisonForm.Size = new Size(1200, 600);
                    comparisonForm.StartPosition = FormStartPosition.CenterParent;
                    comparisonForm.BackColor = Color.White;

                    TabControl tabControl = new TabControl
                    {
                        Dock = DockStyle.Fill
                    };

                    // Tab 1: Selected Tenant Payments
                    TabPage tabTenant = new TabPage
                    {
                        Text = $"👤 {tenantName}"
                    };

                    var tenantPaymentsForm = new FrmPaymentHistory(agreementId);
                    tenantPaymentsForm.TopLevel = false;
                    tenantPaymentsForm.FormBorderStyle = FormBorderStyle.None;
                    tenantPaymentsForm.Dock = DockStyle.Fill;
                    tabTenant.Controls.Add(tenantPaymentsForm);

                    // Tab 2: All Payments
                    TabPage tabAll = new TabPage
                    {
                        Text = "💰 All Payments"
                    };

                    var allPaymentsForm = new FrmPaymentHistory(0);
                    allPaymentsForm.TopLevel = false;
                    allPaymentsForm.FormBorderStyle = FormBorderStyle.None;
                    allPaymentsForm.Dock = DockStyle.Fill;
                    tabAll.Controls.Add(allPaymentsForm);

                    // Tab 3: Summary Comparison
                    TabPage tabSummary = new TabPage
                    {
                        Text = "📊 Summary"
                    };

                    var summaryPanel = CreateComparisonSummary(agreementId, tenantId, tenantName);
                    summaryPanel.Dock = DockStyle.Fill;
                    tabSummary.Controls.Add(summaryPanel);

                    tabControl.TabPages.AddRange(new TabPage[] { tabTenant, tabAll, tabSummary });

                    comparisonForm.Controls.Add(tabControl);

                    // Show the forms
                    tenantPaymentsForm.Show();
                    allPaymentsForm.Show();

                    comparisonForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing comparison: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateComparisonSummary(int agreementId, int tenantId, string tenantName)
        {
            Panel panel = new Panel
            {
                Padding = new Padding(20),
                BackColor = Color.White
            };

            try
            {
                // Get tenant payments
                var tenantPayments = _dataService.GetPaymentsByAgreement(agreementId);

                // Get all payments
                var allPayments = _dataService.LoadAllPayments()
                    .Where(p => !p.IsDeleted)
                    .ToList();

                // Calculate statistics
                decimal tenantTotal = tenantPayments.Sum(p => p.Amount);
                decimal allTotal = allPayments.Sum(p => p.Amount);
                int tenantCount = tenantPayments.Count;
                int allCount = allPayments.Count;

                decimal tenantAvg = tenantCount > 0 ? tenantTotal / tenantCount : 0;
                decimal allAvg = allCount > 0 ? allTotal / allCount : 0;

                decimal percentageOfTotal = allTotal > 0 ? (tenantTotal / allTotal) * 100 : 0;

                // Tenant payments by type
                int tenantRentCount = tenantPayments.Count(p => p.PaymentType == PaymentType.Rent);
                int tenantCommissionCount = tenantPayments.Count(p => p.PaymentType == PaymentType.Commission);
                decimal tenantRentTotal = tenantPayments.Where(p => p.PaymentType == PaymentType.Rent).Sum(p => p.Amount);
                decimal tenantCommissionTotal = tenantPayments.Where(p => p.PaymentType == PaymentType.Commission).Sum(p => p.Amount);

                // All payments by type
                int allRentCount = allPayments.Count(p => p.PaymentType == PaymentType.Rent);
                int allCommissionCount = allPayments.Count(p => p.PaymentType == PaymentType.Commission);
                decimal allRentTotal = allPayments.Where(p => p.PaymentType == PaymentType.Rent).Sum(p => p.Amount);
                decimal allCommissionTotal = allPayments.Where(p => p.PaymentType == PaymentType.Commission).Sum(p => p.Amount);

                int yPos = 20;

                // Header
                Label lblHeader = new Label
                {
                    Text = $"📊 PAYMENT COMPARISON SUMMARY",
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    ForeColor = Color.SteelBlue,
                    Location = new Point(0, yPos),
                    Size = new Size(panel.Width - 40, 40),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panel.Controls.Add(lblHeader);
                yPos += 50;

                // Tenant vs All comparison
                Panel pnlComparison = new Panel
                {
                    Location = new Point(0, yPos),
                    Size = new Size(panel.Width - 40, 180),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.FromArgb(240, 240, 240),
                    Padding = new Padding(10)
                };

                DataGridView dgvComparison = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AllowUserToAddRows = false,
                    ReadOnly = true,
                    RowHeadersVisible = false,
                    BackgroundColor = Color.White
                };

                dgvComparison.Columns.Add("Metric", "Metric");
                dgvComparison.Columns.Add("Tenant", tenantName);
                dgvComparison.Columns.Add("AllTenants", "All Tenants");
                dgvComparison.Columns.Add("Difference", "Difference");

                // Add rows
                dgvComparison.Rows.Add("Total Payments", tenantCount, allCount, allCount - tenantCount);
                dgvComparison.Rows.Add("Total Amount", tenantTotal.ToString("C"), allTotal.ToString("C"), (allTotal - tenantTotal).ToString("C"));
                dgvComparison.Rows.Add("Average Payment", tenantAvg.ToString("C"), allAvg.ToString("C"), (allAvg - tenantAvg).ToString("C"));
                dgvComparison.Rows.Add("Rent Payments", tenantRentCount, allRentCount, allRentCount - tenantRentCount);
                dgvComparison.Rows.Add("Commission Payments", tenantCommissionCount, allCommissionCount, allCommissionCount - tenantCommissionCount);

                pnlComparison.Controls.Add(dgvComparison);
                panel.Controls.Add(pnlComparison);
                yPos += 190;

                // Percentage of total
                Label lblPercentage = new Label
                {
                    Text = $"📈 {tenantName} represents {percentageOfTotal:F1}% of total payments value",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = percentageOfTotal > 20 ? Color.DarkGreen : Color.DarkOrange,
                    Location = new Point(0, yPos),
                    Size = new Size(panel.Width - 40, 30),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panel.Controls.Add(lblPercentage);
                yPos += 40;

                // Performance indicator
                string performance = "";
                Color performanceColor = Color.Black;

                if (tenantAvg > allAvg * 1.2m)
                {
                    performance = "⭐ HIGH PERFORMER - Above average payment amount";
                    performanceColor = Color.DarkGreen;
                }
                else if (tenantAvg < allAvg * 0.8m)
                {
                    performance = "⚠️ BELOW AVERAGE - Lower than average payment amount";
                    performanceColor = Color.DarkOrange;
                }
                else
                {
                    performance = "✓ AVERAGE PERFORMER - Similar to other tenants";
                    performanceColor = Color.DarkBlue;
                }

                Label lblPerformance = new Label
                {
                    Text = performance,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = performanceColor,
                    Location = new Point(0, yPos),
                    Size = new Size(panel.Width - 40, 30),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                panel.Controls.Add(lblPerformance);
                yPos += 40;

                // Last payment info
                if (tenantPayments.Any())
                {
                    var lastPayment = tenantPayments.OrderByDescending(p => p.PaymentDate).First();
                    Label lblLastPayment = new Label
                    {
                        Text = $"📅 Last payment: {lastPayment.PaymentDate:dd-MMM-yyyy} - {lastPayment.Amount:C}",
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        ForeColor = Color.DarkSlateGray,
                        Location = new Point(0, yPos),
                        Size = new Size(panel.Width - 40, 25),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    panel.Controls.Add(lblLastPayment);
                }
            }
            catch (Exception ex)
            {
                Label lblError = new Label
                {
                    Text = $"Error generating summary: {ex.Message}",
                    ForeColor = Color.Red,
                    Location = new Point(20, 20),
                    Size = new Size(400, 100)
                };
                panel.Controls.Add(lblError);
            }

            return panel;
        }
        private void ExportToCsv(string filePath)
        {
            try
            {
                // Get current displayed data
                var data = dgvRentOverview.DataSource as List<RentOverview>;
                if (data == null && _allData != null)
                {
                    data = _allData;
                }

                if (data != null)
                {
                    using (var writer = new System.IO.StreamWriter(filePath))
                    {
                        // Write header
                        writer.WriteLine("Property,Portion,Size,Tenant Name,Mobile,Tenant Type," +
                            "Payment Info,Monthly Rent,Last Paid Amount,Due Amount," +
                            "Last Payment Date,Next Due Date,Status");

                        // Write data
                        foreach (var item in data)
                        {
                            string status = item.DueAmount > 0 ? "Due" : "Paid";
                            if (item.DueAmount > 0 && item.NextDueDate < DateTime.Now)
                                status = "Overdue";

                            writer.WriteLine($"\"{item.PropertyName}\",\"{item.PortionName}\",\"{item.PortionSize}\"," +
                                $"\"{item.TenantName}\",\"{item.Mobile}\",\"{item.TenantType}\",\"{item.PaymentInfo}\"," +
                                $"{item.MonthlyRent:F2},{item.LastPaidAmount:F2},{item.DueAmount:F2}," +
                                $"\"{item.LastPaymentDate:dd-MMM-yyyy}\",\"{item.NextDueDate:dd-MMM-yyyy}\",\"{status}\"");
                        }
                    }

                    MessageBox.Show($"Data exported successfully to:\n{filePath}",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"CSV export error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void CmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (_allData == null) return;

            var filtered = _allData.AsEnumerable();

            // Text search
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                var searchTerm = txtSearch.Text.ToLower();
                filtered = filtered.Where(x =>
                    (x.PropertyName?.ToLower() ?? "").Contains(searchTerm) ||
                    (x.TenantName?.ToLower() ?? "").Contains(searchTerm) ||
                    (x.PortionName?.ToLower() ?? "").Contains(searchTerm) ||
                    (x.Mobile?.Contains(searchTerm) ?? false) ||
                    (x.PaymentInfo?.ToLower() ?? "").Contains(searchTerm));
            }

            // Status filter
            string filter = cmbFilter.SelectedItem?.ToString() ?? "All";
            switch (filter)
            {
                case "On Rent":
                    filtered = filtered.Where(x => x.TenantType == TenantType.OnRent);
                    break;
                case "On Commission":
                    filtered = filtered.Where(x => x.TenantType == TenantType.OnCommission);
                    break;
                case "Due Only":
                    filtered = filtered.Where(x => x.DueAmount > 0);
                    break;
                case "Paid":
                    filtered = filtered.Where(x => x.DueAmount <= 0);
                    break;
            }

            var filteredList = filtered.ToList();
            dgvRentOverview.DataSource = filteredList;

            // Show result count in title
            if (filteredList.Count != _allData.Count)
            {
                this.Text = $"Rent Collection Overview - Showing {filteredList.Count} of {_allData.Count} tenants";
            }
            else
            {
                this.Text = "Rent Collection Overview";
            }
        }
    }
}