using ArthiPOS.Controls;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Printing;

namespace ShopRentManagementSystem
{
    public partial class FrmPaymentHistory : Form
    {
        private readonly JsonDataService _dataService;
        private readonly int _agreementId;
        private DataGridView dgvPayments;
        private Label lblTenantInfo;
        private Label lblTotalInfo;
        private Button btnRefresh;
        private Button btnClose;
        private Button btnDeleteSelected;
        private Button btnMonthlySummary;
        private Button btnExportExcel;
        private ComboBox cmbYearFilter;
        private ComboBox cmbMonthFilter;
        private ComboBox cmbTenantFilter;
        private ComboBox cmbPropertyFilter;
        private CheckBox chkShowAll;
        private ComboBox cmbTypeFilter;

        public FrmPaymentHistory(int agreementId = 0)
        {
            try
            {
                _agreementId = agreementId;
                _dataService = new JsonDataService();
                InitializeComponent();

                if (_agreementId == 0)
                {
                    this.Text = "All Payments History";
                    SetupAllPaymentsMode();
                }

                LoadPaymentHistory();
                SetupFilterControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing payment history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            try
            {
                this.Text = "Payment History";
                this.Size = new Size(1300, 650);
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                // Header Panel
                Panel pnlHeader = new Panel
                {
                    Height = 100,
                    Dock = DockStyle.Top,
                    BackColor = Color.SteelBlue,
                    Padding = new Padding(10)
                };

                lblTenantInfo = new Label
                {
                    Text = "Payment History",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = Color.White,
                    Dock = DockStyle.Top,
                    Height = 35,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                lblTotalInfo = new Label
                {
                    Text = "Total Paid: $0.00",
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.White,
                    Dock = DockStyle.Bottom,
                    Height = 25,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                // Filter Panel
                Panel pnlFilter = new Panel
                {
                    Height = 80,
                    Dock = DockStyle.Top,
                    BackColor = Color.FromArgb(240, 240, 240),
                    Padding = new Padding(10, 10, 10, 10)
                };

                // Year filter
                Label lblYear = new Label
                {
                    Text = "Year:",
                    Location = new Point(10, 15),
                    Size = new Size(40, 20),
                    Font = new Font("Segoe UI", 9)
                };

                cmbYearFilter = new ComboBox
                {
                    Location = new Point(55, 12),
                    Size = new Size(80, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };

                // Month filter
                Label lblMonth = new Label
                {
                    Text = "Month:",
                    Location = new Point(145, 15),
                    Size = new Size(50, 20),
                    Font = new Font("Segoe UI", 9)
                };

                cmbMonthFilter = new ComboBox
                {
                    Location = new Point(200, 12),
                    Size = new Size(100, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };

                // Tenant filter (for all payments mode)
                Label lblTenant = new Label
                {
                    Text = "Tenant:",
                    Location = new Point(310, 15),
                    Size = new Size(50, 20),
                    Font = new Font("Segoe UI", 9)
                };

                cmbTenantFilter = new ComboBox
                {
                    Location = new Point(365, 12),
                    Size = new Size(150, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9),
                    Visible = false
                };

                // Property filter (for all payments mode)
                Label lblProperty = new Label
                {
                    Text = "Property:",
                    Location = new Point(525, 15),
                    Size = new Size(60, 20),
                    Font = new Font("Segoe UI", 9)
                };

                cmbPropertyFilter = new ComboBox
                {
                    Location = new Point(590, 12),
                    Size = new Size(150, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9),
                    Visible = false
                };

                // Payment type filter
                Label lblType = new Label
                {
                    Text = "Type:",
                    Location = new Point(750, 15),
                    Size = new Size(40, 20),
                    Font = new Font("Segoe UI", 9)
                };

                cmbTypeFilter = new ComboBox
                {
                    Location = new Point(795, 12),
                    Size = new Size(100, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 9)
                };
                cmbTypeFilter.Items.AddRange(new string[] { "All Types", "Rent", "Commission" });
                cmbTypeFilter.SelectedIndex = 0;

                // Show all checkbox (for all payments mode)
                chkShowAll = new CheckBox
                {
                    Text = "Show Deleted",
                    Location = new Point(10, 45),
                    Size = new Size(120, 25),
                    Font = new Font("Segoe UI", 9),
                    Visible = false
                };

                Button btnClearFilter = new Button
                {
                    Text = "Clear Filters",
                    Location = new Point(905, 12),
                    Size = new Size(100, 25),
                    Font = new Font("Segoe UI", 9)
                };
                btnClearFilter.Click += (s, e) =>
                {
                    try
                    {
                        cmbYearFilter.SelectedIndex = 0;
                        cmbMonthFilter.SelectedIndex = 0;
                        cmbTypeFilter.SelectedIndex = 0;
                        if (_agreementId == 0)
                        {
                            cmbTenantFilter.SelectedIndex = 0;
                            cmbPropertyFilter.SelectedIndex = 0;
                            chkShowAll.Checked = false;
                        }
                        LoadPaymentHistory();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error clearing filters: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                pnlFilter.Controls.AddRange(new Control[] {
                    lblYear, cmbYearFilter,
                    lblMonth, cmbMonthFilter,
                    lblTenant, cmbTenantFilter,
                    lblProperty, cmbPropertyFilter,
                    lblType, cmbTypeFilter,
                    chkShowAll,
                    btnClearFilter
                });

                pnlHeader.Controls.Add(lblTenantInfo);
                pnlHeader.Controls.Add(lblTotalInfo);

                // Data Grid View
                dgvPayments = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    ReadOnly = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    RowHeadersVisible = false,
                    AllowUserToResizeRows = false,
                    BackgroundColor = Color.White,
                    AutoGenerateColumns = false // IMPORTANT: Prevent auto column generation
                };

                SetupDataGridViewErrorHandling();
                SetupDataGridView();

                // Status Panel
                Panel pnlStatus = new Panel
                {
                    Height = 30,
                    Dock = DockStyle.Bottom,
                    BackColor = Color.LightGray,
                    Padding = new Padding(10, 5, 10, 5)
                };

                Label lblStatus = new Label
                {
                    Text = "Double-click any payment to view details",
                    ForeColor = Color.DarkSlateGray,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 9, FontStyle.Italic)
                };

                pnlStatus.Controls.Add(lblStatus);

                // Buttons Panel
                Panel pnlButtons = new Panel
                {
                    Height = 60,
                    Dock = DockStyle.Bottom,
                    BackColor = SystemColors.Control,
                    Padding = new Padding(10)
                };

                btnDeleteSelected = new Button
                {
                    Text = "🗑️ Delete",
                    Size = new Size(120, 35),
                    Location = new Point(10, 13),
                    BackColor = Color.LightCoral,
                    ForeColor = Color.DarkRed,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ImageAlign = ContentAlignment.MiddleLeft,
                    TextImageRelation = TextImageRelation.ImageBeforeText
                };
                btnDeleteSelected.Click += BtnDeleteSelected_Click;

                btnRefresh = new Button
                {
                    Text = "🔄 Refresh",
                    Size = new Size(100, 35),
                    Location = new Point(140, 13),
                    Font = new Font("Segoe UI", 9)
                };
                btnRefresh.Click += BtnRefresh_Click;

                btnMonthlySummary = new Button
                {
                    Text = "📅 Monthly Summary",
                    Size = new Size(140, 35),
                    Location = new Point(250, 13),
                    BackColor = Color.LightBlue,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                btnMonthlySummary.Click += BtnMonthlySummary_Click;

                btnExportExcel = new Button
                {
                    Text = "📤 Export",
                    Size = new Size(100, 35),
                    Location = new Point(400, 13),
                    BackColor = Color.LightGreen,
                    Font = new Font("Segoe UI", 9)
                };
                btnExportExcel.Click += BtnExportExcel_Click;

                // Add a View All button
                Button btnViewAll = new Button
                {
                    Text = "📋 View Details",
                    Size = new Size(120, 35),
                    Location = new Point(510, 13),
                    BackColor = Color.LightSteelBlue,
                    Font = new Font("Segoe UI", 9)
                };
                btnViewAll.Click += BtnViewAll_Click;

                btnClose = new Button
                {
                    Text = "✖ Close",
                    Size = new Size(100, 35),
                    Location = new Point(640, 13),
                    DialogResult = DialogResult.Cancel,
                    Font = new Font("Segoe UI", 9)
                };

                pnlButtons.Controls.AddRange(new Control[] {
                    btnDeleteSelected, btnRefresh, btnMonthlySummary,
                    btnExportExcel, btnViewAll, btnClose
                });

                this.Controls.AddRange(new Control[] {
                    dgvPayments, pnlStatus, pnlButtons, pnlFilter, pnlHeader
                });

                // Enable double-click to show details
                dgvPayments.CellDoubleClick += DgvPayments_CellDoubleClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form controls: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViewErrorHandling()
        {
            dgvPayments.DataError += (sender, e) =>
            {
                MessageBox.Show($"Data error in grid: {e.Exception?.Message}", "Data Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.ThrowException = false; // Prevent crash
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                LoadPaymentHistory();
                SetupFilterControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupAllPaymentsMode()
        {
            try
            {
                // Show additional filters for all payments mode
                cmbTenantFilter.Visible = true;
                cmbPropertyFilter.Visible = true;
                chkShowAll.Visible = true;

                // Update title
                lblTenantInfo.Text = "💰 ALL PAYMENTS HISTORY";

                // Wire up filter events
                cmbTenantFilter.SelectedIndexChanged += (s, e) => ApplyFilter();
                cmbPropertyFilter.SelectedIndexChanged += (s, e) => ApplyFilter();
                cmbTypeFilter.SelectedIndexChanged += (s, e) => ApplyFilter();
                chkShowAll.CheckedChanged += (s, e) => ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up all payments mode: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                dgvPayments.Columns.Clear();

                if (_agreementId == 0)
                {
                    SetupAllPaymentsDataGridView();
                }
                else
                {
                    SetupSingleAgreementDataGridView();
                }

                // Ensure AutoGenerateColumns is false
                dgvPayments.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up grid: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupSingleAgreementDataGridView()
        {
            try
            {
                DataGridViewColumn[] columns =
                {
                    new DataGridViewTextBoxColumn {
                        Name = "Id",
                        HeaderText = "ID",
                        Width = 50,
                        DataPropertyName = "Id"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "PaymentDate",
                        HeaderText = "Payment Date",
                        Width = 120,
                        DataPropertyName = "PaymentDate"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "MonthYear",
                        HeaderText = "For Month",
                        Width = 100,
                        DataPropertyName = "MonthYear"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "Amount",
                        HeaderText = "Amount",
                        Width = 100,
                        DataPropertyName = "Amount"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "LabourAmount",
                        HeaderText = "Labour Amount",
                        Width = 100,
                        DataPropertyName = "LabourAmount"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "PaymentType",
                        HeaderText = "Type",
                        Width = 80,
                        DataPropertyName = "PaymentType"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "PaymentMethod",
                        HeaderText = "Method",
                        Width = 80,
                        DataPropertyName = "PaymentMethod"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "ReferenceNumber",
                        HeaderText = "Reference ID",
                        Width = 120,
                        DataPropertyName = "ReferenceNumber"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "CreatedDate",
                        HeaderText = "Recorded On",
                        Width = 150,
                        DataPropertyName = "CreatedDate"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "Notes",
                        HeaderText = "Notes",
                        Width = 200,
                        DataPropertyName = "Notes"
                    },
                    new DataGridViewButtonColumn
                    {
                        Name = "Delete",
                        HeaderText = "Action",
                        Width = 80,
                        Text = "Delete",
                        UseColumnTextForButtonValue = true
                    }
                };

                dgvPayments.Columns.AddRange(columns);
                ApplyCommonGridSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up agreement grid: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupAllPaymentsDataGridView()
        {
            try
            {
                DataGridViewColumn[] columns =
                {
                    new DataGridViewTextBoxColumn {
                        Name = "Id",
                        HeaderText = "ID",
                        Width = 50,
                        DataPropertyName = "Id"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "PaymentDate",
                        HeaderText = "📅 Date",
                        Width = 100,
                        DataPropertyName = "PaymentDate"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "TenantName",
                        HeaderText = "👤 Tenant",
                        Width = 150,
                        DataPropertyName = "TenantName"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "PropertyName",
                        HeaderText = "🏢 Property",
                        Width = 120,
                        DataPropertyName = "PropertyName"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "PortionName",
                        HeaderText = "📍 Portion",
                        Width = 80,
                        DataPropertyName = "PortionName"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "Amount",
                        HeaderText = "💰 Amount",
                        Width = 100,
                        DataPropertyName = "Amount"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "LabourAmount",
                        HeaderText = "💰 Labour Amount",
                        Width = 100,
                        DataPropertyName = "LabourAmount"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "PaymentType",
                        HeaderText = "🏷️ Type",
                        Width = 80,
                        DataPropertyName = "PaymentType"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "PaymentMethod",
                        HeaderText = "💳 Method",
                        Width = 80,
                        DataPropertyName = "PaymentMethod"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "ReferenceNumber",
                        HeaderText = "🔖 Reference ID",
                        Width = 120,
                        DataPropertyName = "ReferenceNumber"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "MonthYear",
                        HeaderText = "📆 For Month",
                        Width = 100,
                        DataPropertyName = "MonthYear"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "CreatedDate",
                        HeaderText = "🕒 Recorded",
                        Width = 120,
                        DataPropertyName = "CreatedDate"
                    },
                    new DataGridViewTextBoxColumn {
                        Name = "Notes",
                        HeaderText = "📝 Notes",
                        Width = 150,
                        DataPropertyName = "Notes"
                    },
                    new DataGridViewButtonColumn
                    {
                        Name = "Delete",
                        HeaderText = "Action",
                        Width = 80,
                        Text = "Delete",
                        UseColumnTextForButtonValue = true
                    }
                };

                dgvPayments.Columns.AddRange(columns);
                ApplyCommonGridSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up all payments grid: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyCommonGridSettings()
        {
            try
            {
                // Format columns - always check if column exists first
                if (dgvPayments.Columns.Contains("Amount"))
                {
                    dgvPayments.Columns["Amount"].DefaultCellStyle.Format = "C";
                    dgvPayments.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dgvPayments.Columns.Contains("LabourAmount"))
                {
                    dgvPayments.Columns["LabourAmount"].DefaultCellStyle.Format = "C";
                    dgvPayments.Columns["LabourAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dgvPayments.Columns.Contains("PaymentDate"))
                {
                    dgvPayments.Columns["PaymentDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                    dgvPayments.Columns["PaymentDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvPayments.Columns.Contains("CreatedDate"))
                {
                    dgvPayments.Columns["CreatedDate"].DefaultCellStyle.Format = "dd-MMM-yyyy HH:mm";
                    dgvPayments.Columns["CreatedDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvPayments.Columns.Contains("MonthYear"))
                {
                    dgvPayments.Columns["MonthYear"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Style headers
                dgvPayments.EnableHeadersVisualStyles = false;
                dgvPayments.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
                dgvPayments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvPayments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                dgvPayments.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying grid settings: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Validate row and column indices
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (e.RowIndex >= dgvPayments.Rows.Count) return;
                if (e.ColumnIndex >= dgvPayments.Columns.Count) return;

                // Check if it's the delete button column
                if (dgvPayments.Columns[e.ColumnIndex].Name == "Delete")
                {
                    // Verify it's a button column
                    if (dgvPayments.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                    {
                        DeletePayment(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing click: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeletePayment(int rowIndex)
        {
            try
            {
                // Validate row index
                if (rowIndex < 0 || rowIndex >= dgvPayments.Rows.Count)
                {
                    MessageBox.Show("Invalid row selected.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var row = dgvPayments.Rows[rowIndex];

                // Safely get cell values
                if (!int.TryParse(GetSafeCellValue(row, "Id"), out int paymentId))
                {
                    MessageBox.Show("Could not read payment ID.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime paymentDate = DateTime.MinValue;
                DateTime.TryParse(GetSafeCellValue(row, "PaymentDate"), out paymentDate);

                decimal amount = 0;
                decimal.TryParse(GetSafeCellValue(row, "Amount"), out amount);

                ShowDeleteConfirmationDialog(paymentId, paymentDate, amount, rowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting payment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSafeCellValue(DataGridViewRow row, string columnName)
        {
            try
            {
                if (row == null) return "";
                if (!dgvPayments.Columns.Contains(columnName)) return "";

                var cell = row.Cells[columnName];
                return cell?.Value?.ToString() ?? "";
            }
            catch
            {
                return "";
            }
        }

        private void DgvPayments_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // Validate indices
                if (e.RowIndex < 0 || e.RowIndex >= dgvPayments.Rows.Count) return;
                if (e.ColumnIndex < 0 || e.ColumnIndex >= dgvPayments.Columns.Count) return;

                var row = dgvPayments.Rows[e.RowIndex];
                if (row == null) return;

                // Color by payment type - with safe column access
                if (dgvPayments.Columns.Contains("PaymentType") &&
                    e.ColumnIndex == dgvPayments.Columns["PaymentType"].Index)
                {
                    if (e.Value != null)
                    {
                        string paymentType = e.Value.ToString();
                        if (paymentType.Contains("Commission") ||
                            paymentType.Equals(PaymentType.Commission.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            e.CellStyle.BackColor = Color.LightYellow;
                            e.CellStyle.ForeColor = Color.DarkGoldenrod;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                        else if (paymentType.Contains("Rent") ||
                                 paymentType.Equals(PaymentType.Rent.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkGreen;
                        }
                    }
                }

                // Color delete button - with safe column access
                if (dgvPayments.Columns.Contains("Delete") &&
                    e.ColumnIndex == dgvPayments.Columns["Delete"].Index)
                {
                    e.CellStyle.BackColor = Color.LightCoral;
                    e.CellStyle.ForeColor = Color.DarkRed;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Alternate row colors - but preserve special formatting
                if (e.RowIndex % 2 == 0)
                {
                    // Only apply if not already colored
                    if (e.CellStyle.BackColor == Color.Empty ||
                        e.CellStyle.BackColor == Color.White ||
                        e.CellStyle.BackColor == Color.FromArgb(250, 250, 250))
                    {
                        e.CellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    }
                }
            }
            catch (Exception ex)
            {
                // Silent fail for formatting errors - don't disrupt user experience
                System.Diagnostics.Debug.WriteLine($"Formatting error: {ex.Message}");
            }
        }

        private void SetupFilterControls()
        {
            try
            {
                // Populate year filter (last 5 years and current year)
                int currentYear = DateTime.Now.Year;
                cmbYearFilter.Items.Clear();
                cmbYearFilter.Items.Add("All Years");
                for (int year = currentYear; year >= currentYear - 5; year--)
                {
                    cmbYearFilter.Items.Add(year.ToString());
                }
                cmbYearFilter.SelectedIndexChanged += (s, e) => ApplyFilter();
                cmbYearFilter.SelectedIndex = 0;

                // Populate month filter
                cmbMonthFilter.Items.Clear();
                cmbMonthFilter.Items.Add("All Months");
                cmbMonthFilter.Items.AddRange(new string[] {
                    "January", "February", "March", "April", "May", "June",
                    "July", "August", "September", "October", "November", "December"
                });
                cmbMonthFilter.SelectedIndexChanged += (s, e) => ApplyFilter();
                cmbMonthFilter.SelectedIndex = 0;

                // Populate tenant filter for all payments mode
                if (_agreementId == 0)
                {
                    PopulateTenantFilter();
                    PopulatePropertyFilter();
                }
                else
                {
                    cmbTypeFilter.SelectedIndexChanged += (s, e) => ApplyFilter();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up filters: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateTenantFilter()
        {
            try
            {
                var tenants = _dataService.LoadTenants() ?? new List<Tenant>();
                cmbTenantFilter.Items.Clear();
                cmbTenantFilter.Items.Add("All Tenants");

                foreach (var tenant in tenants.OrderBy(t => t.Name))
                {
                    cmbTenantFilter.Items.Add(tenant.Name);
                }

                cmbTenantFilter.SelectedIndexChanged += (s, e) => ApplyFilter();
                cmbTenantFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tenants for filter: {ex.Message}");
            }
        }

        private void PopulatePropertyFilter()
        {
            try
            {
                var properties = _dataService.LoadProperties() ?? new List<Property>();
                cmbPropertyFilter.Items.Clear();
                cmbPropertyFilter.Items.Add("All Properties");

                foreach (var property in properties.OrderBy(p => p.Name))
                {
                    cmbPropertyFilter.Items.Add(property.Name);
                }

                cmbPropertyFilter.SelectedIndexChanged += (s, e) => ApplyFilter();
                cmbPropertyFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading properties for filter: {ex.Message}");
            }
        }

        private void LoadPaymentHistory()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (_agreementId == 0)
                {
                    LoadAllPayments();
                }
                else
                {
                    LoadSingleAgreementPayments();
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error loading payment history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllPayments()
        {
            try
            {
                // Load ALL payments
                var payments = _dataService.LoadAllPayments() ?? new List<Payment>();

                // Enrich with tenant, property, and portion information
                var tenants = _dataService.LoadTenants() ?? new List<Tenant>();
                var properties = _dataService.LoadProperties() ?? new List<Property>();
                var portions = _dataService.LoadPortions() ?? new List<Portion>();
                var agreements = _dataService.LoadAgreements() ?? new List<RentAgreement>();

                List<PaymentDisplay> paymentDisplays = new List<PaymentDisplay>();

                foreach (var payment in payments)
                {
                    try
                    {
                        // Skip deleted payments unless show all is checked
                        if (!chkShowAll.Checked && payment.IsDeleted)
                            continue;

                        // Find agreement
                        var agreement = agreements.FirstOrDefault(a => a.Id == payment.AgreementId);

                        // Create payment display with safe defaults
                        var display = new PaymentDisplay
                        {
                            Payment = payment,
                            TenantName = "Unknown",
                            PropertyName = "Unknown",
                            PortionName = "Unknown",
                            Agreement = agreement
                        };

                        if (agreement != null)
                        {
                            var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                            var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                            var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);

                            display.TenantName = tenant?.Name ?? "Unknown Tenant";
                            display.PropertyName = property?.Name ?? "Unknown Property";
                            display.PortionName = portion?.Name ?? "Unknown Portion";
                        }

                        paymentDisplays.Add(display);
                    }
                    catch (Exception ex)
                    {
                        // Log individual payment error but continue processing
                        System.Diagnostics.Debug.WriteLine($"Error processing payment {payment?.Id}: {ex.Message}");
                    }
                }

                // Apply filters
                paymentDisplays = ApplyFilters(paymentDisplays);

                // Update header
                lblTenantInfo.Text = $"💰 ALL PAYMENTS - Total: {paymentDisplays.Count}";

                // Safely bind data
                dgvPayments.DataSource = null;
                dgvPayments.DataSource = paymentDisplays;

                // Update total
                UpdateTotalInfo(paymentDisplays);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading all payments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<PaymentDisplay> ApplyFilters(List<PaymentDisplay> payments)
        {
            try
            {
                if (payments == null)
                    return new List<PaymentDisplay>();

                IEnumerable<PaymentDisplay> filtered = payments;

                // Apply year filter
                if (cmbYearFilter.SelectedIndex > 0 && int.TryParse(cmbYearFilter.SelectedItem?.ToString(), out int selectedYear))
                {
                    filtered = filtered.Where(p => p.PaymentDate.Year == selectedYear);
                }

                // Apply month filter
                if (cmbMonthFilter.SelectedIndex > 0)
                {
                    int selectedMonth = cmbMonthFilter.SelectedIndex; // 1-12
                    filtered = filtered.Where(p => p.PaymentDate.Month == selectedMonth);
                }

                // Apply tenant filter
                if (_agreementId == 0 && cmbTenantFilter.SelectedIndex > 0)
                {
                    string selectedTenant = cmbTenantFilter.SelectedItem?.ToString() ?? "";
                    filtered = filtered.Where(p => p.TenantName == selectedTenant);
                }

                // Apply property filter
                if (_agreementId == 0 && cmbPropertyFilter.SelectedIndex > 0)
                {
                    string selectedProperty = cmbPropertyFilter.SelectedItem?.ToString() ?? "";
                    filtered = filtered.Where(p => p.PropertyName == selectedProperty);
                }

                // Apply type filter
                if (cmbTypeFilter.SelectedIndex > 0)
                {
                    string selectedType = cmbTypeFilter.SelectedItem?.ToString() ?? "";
                    filtered = filtered.Where(p => p.PaymentType == selectedType);
                }

                return filtered.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return payments ?? new List<PaymentDisplay>();
            }
        }

        private void LoadSingleAgreementPayments()
        {
            try
            {
                // Load payments for specific agreement
                var payments = _dataService.GetPaymentsByAgreement(_agreementId) ?? new List<Payment>();

                // Get agreement details
                var agreements = _dataService.LoadAgreements() ?? new List<RentAgreement>();
                var agreement = agreements.FirstOrDefault(a => a.Id == _agreementId);

                if (agreement == null)
                {
                    MessageBox.Show("Agreement not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Get tenant details
                var tenants = _dataService.LoadTenants() ?? new List<Tenant>();
                var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);

                // Get property and portion details
                var properties = _dataService.LoadProperties() ?? new List<Property>();
                var portions = _dataService.LoadPortions() ?? new List<Portion>();
                var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);
                var portion = portions.FirstOrDefault(p => p.Id == agreement.PortionId);

                // Update header
                string tenantInfo = tenant != null ? tenant.Name : "Unknown Tenant";
                string propertyInfo = property != null ? $"{property.Name} - {portion?.Name}" : "Unknown Property";
                string rentType = tenant != null && tenant.Type == TenantType.OnCommission ? "Commission" : "Rent";
                lblTenantInfo.Text = $"📋 Payment History - {tenantInfo} ({propertyInfo}) - {rentType}";

                // Apply type filter for single agreement
                if (cmbTypeFilter.SelectedIndex > 0)
                {
                    string selectedType = cmbTypeFilter.SelectedItem?.ToString() ?? "";
                    payments = payments.Where(p => p.PaymentType.ToString() == selectedType).ToList();
                }

                // Apply year filter
                if (cmbYearFilter.SelectedIndex > 0 && int.TryParse(cmbYearFilter.SelectedItem?.ToString(), out int selectedYear))
                {
                    payments = payments.Where(p => p.PaymentDate.Year == selectedYear).ToList();
                }

                // Apply month filter
                if (cmbMonthFilter.SelectedIndex > 0)
                {
                    int selectedMonth = cmbMonthFilter.SelectedIndex;
                    payments = payments.Where(p => p.PaymentDate.Month == selectedMonth).ToList();
                }

                // Bind data to grid
                dgvPayments.DataSource = null;
                dgvPayments.DataSource = payments;

                // Update total
                UpdateTotalInfo(payments);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading agreement payments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotalInfo<T>(List<T> items)
        {
            try
            {
                if (items == null || !items.Any())
                {
                    lblTotalInfo.Text = "Total Paid: $0.00, Total Labour: $0.00 | Payments: 0";
                    return;
                }

                decimal totalPaid = 0;
                decimal totalLabour = 0;
                int count = items.Count;

                if (typeof(T) == typeof(Payment))
                {
                    var paymentList = items as List<Payment>;
                    if (paymentList != null)
                    {
                        totalPaid = paymentList.Sum(p => p?.Amount ?? 0);
                        totalLabour = (decimal)paymentList.Sum(p => p?.LaborAmount ?? 0);
                    }
                }
                else if (typeof(T) == typeof(PaymentDisplay))
                {
                    var paymentDisplayList = items as List<PaymentDisplay>;
                    if (paymentDisplayList != null)
                    {
                        totalPaid = paymentDisplayList.Sum(p => p?.Amount ?? 0);
                        totalLabour = paymentDisplayList.Sum(p => p?.LabourAmount ?? 0);
                    }
                }

                lblTotalInfo.Text = $"Total Paid: {totalPaid:C}, Total Labour: {totalLabour:C} | Payments: {count}";
            }
            catch (Exception ex)
            {
                lblTotalInfo.Text = "Total: Error calculating totals";
                System.Diagnostics.Debug.WriteLine($"UpdateTotalInfo error: {ex.Message}");
            }
        }

        private void ApplyFilter()
        {
            try
            {
                LoadPaymentHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filter: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPayments.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a payment to delete.", "Select Payment",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var selectedRow = dgvPayments.SelectedRows[0];
                var rowIndex = selectedRow.Index;

                if (!int.TryParse(GetSafeCellValue(selectedRow, "Id"), out int paymentId))
                {
                    MessageBox.Show("Could not read payment ID.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime paymentDate = DateTime.MinValue;
                DateTime.TryParse(GetSafeCellValue(selectedRow, "PaymentDate"), out paymentDate);

                decimal amount = 0;
                decimal.TryParse(GetSafeCellValue(selectedRow, "Amount"), out amount);

                ShowDeleteConfirmationDialog(paymentId, paymentDate, amount, rowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting selected payment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDeleteConfirmationDialog(int paymentId, DateTime paymentDate, decimal amount, int rowIndex)
        {
            try
            {
                // Get tenant name for better confirmation
                var payments = _dataService.LoadAllPayments() ?? new List<Payment>();
                var payment = payments.FirstOrDefault(p => p.Id == paymentId);

                string tenantName = "Unknown Tenant";
                if (payment != null)
                {
                    var agreements = _dataService.LoadAgreements() ?? new List<RentAgreement>();
                    var agreement = agreements.FirstOrDefault(a => a.Id == payment.AgreementId);
                    if (agreement != null)
                    {
                        var tenants = _dataService.LoadTenants() ?? new List<Tenant>();
                        var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);
                        tenantName = tenant?.Name ?? "Unknown";
                    }
                }

                using (var deleteDialog = new Form())
                {
                    deleteDialog.Text = "Confirm Payment Deletion";
                    deleteDialog.Size = new Size(450, 250);
                    deleteDialog.StartPosition = FormStartPosition.CenterParent;
                    deleteDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                    deleteDialog.BackColor = Color.WhiteSmoke;

                    // Warning icon
                    PictureBox warningIcon = new PictureBox
                    {
                        Image = SystemIcons.Warning.ToBitmap(),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(48, 48),
                        Location = new Point(20, 20)
                    };

                    // Warning message
                    Label lblWarning = new Label
                    {
                        Text = "⚠️ WARNING: This action cannot be undone!",
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        ForeColor = Color.DarkRed,
                        Location = new Point(80, 20),
                        Size = new Size(350, 20)
                    };

                    // Details
                    Label lblDetails = new Label
                    {
                        Text = $"You are about to delete a payment:\n\n" +
                               $"• Tenant: {tenantName}\n" +
                               $"• Date: {paymentDate:dd-MMM-yyyy}\n" +
                               $"• Amount: {amount:C}\n\n" +
                               $"This will affect rent calculations and reports.",
                        Location = new Point(80, 50),
                        Size = new Size(350, 100),
                        Font = new Font("Segoe UI", 9, FontStyle.Regular)
                    };

                    // Reason input
                    Label lblReason = new Label
                    {
                        Text = "Reason for deletion:",
                        Location = new Point(20, 160),
                        Size = new Size(120, 20),
                        Font = new Font("Segoe UI", 9, FontStyle.Regular)
                    };

                    TextBox txtReason = new TextBox
                    {
                        Location = new Point(140, 160),
                        Size = new Size(270, 25)
                    };

                    // Buttons
                    Button btnConfirmDelete = new Button
                    {
                        Text = "🗑️ Delete Permanently",
                        Location = new Point(150, 195),
                        Size = new Size(140, 30),
                        BackColor = Color.IndianRed,
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold)
                    };

                    Button btnCancel = new Button
                    {
                        Text = "Cancel",
                        Location = new Point(300, 195),
                        Size = new Size(80, 30),
                        DialogResult = DialogResult.Cancel
                    };

                    btnConfirmDelete.Click += (s, args) =>
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(txtReason.Text))
                            {
                                MessageBox.Show("Please enter a reason for deletion.", "Reason Required",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtReason.Focus();
                                return;
                            }

                            // Perform deletion
                            bool success = _dataService.DeletePayment(paymentId, txtReason.Text);

                            if (success)
                            {
                                // Remove row from grid immediately
                                if (dgvPayments.DataSource is List<Payment> paymentList)
                                {
                                    if (rowIndex >= 0 && rowIndex < paymentList.Count)
                                    {
                                        paymentList.RemoveAt(rowIndex);
                                    }
                                }
                                else if (dgvPayments.DataSource is List<PaymentDisplay> displayList)
                                {
                                    if (rowIndex >= 0 && rowIndex < displayList.Count)
                                    {
                                        displayList.RemoveAt(rowIndex);
                                    }
                                }

                                // Recalculate total
                                LoadPaymentHistory();

                                MessageBox.Show("✅ Payment deleted successfully!", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                deleteDialog.DialogResult = DialogResult.OK;

                                // Notify parent form to refresh
                                NotifyParentForm();
                            }
                            else
                            {
                                MessageBox.Show("❌ Failed to delete payment. Please try again.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error during deletion: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    deleteDialog.AcceptButton = btnConfirmDelete;
                    deleteDialog.CancelButton = btnCancel;

                    deleteDialog.Controls.AddRange(new Control[] {
                        warningIcon, lblWarning, lblDetails, lblReason, txtReason,
                        btnConfirmDelete, btnCancel
                    });

                    // Set focus to reason textbox
                    deleteDialog.Shown += (s, args) => txtReason.Focus();

                    deleteDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing delete confirmation: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotifyParentForm()
        {
            try
            {
                // If this form was opened from Rent Collection Overview, refresh it
                if (this.Owner != null)
                {
                    if (this.Owner is FrmRentCollectionOverview overview)
                    {
                        overview.RefreshData();
                    }
                    else if (this.Owner is Form mainForm)
                    {
                        // Try to find and refresh any open Rent Collection Overview
                        foreach (Form childForm in mainForm.MdiChildren)
                        {
                            if (childForm is FrmRentCollectionOverview childOverview)
                            {
                                childOverview.RefreshData();
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error notifying parent: {ex.Message}");
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPaymentHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnViewAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (_agreementId == 0)
                {
                    // For all payments, show complete history
                    ShowCompletePaymentHistory();
                }
                else
                {
                    // For single agreement, show all including deleted
                    ShowAllPaymentHistory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing all payments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowCompletePaymentHistory()
        {
            try
            {
                var allPayments = _dataService.LoadAllPayments() ?? new List<Payment>();

                using (var form = new Form())
                {
                    form.Text = "Complete Payment History (Including Deleted)";
                    form.Size = new Size(1200, 500);
                    form.StartPosition = FormStartPosition.CenterParent;

                    var dgv = new DataGridView
                    {
                        Dock = DockStyle.Fill,
                        AllowUserToAddRows = false,
                        ReadOnly = true,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                        BackgroundColor = Color.White
                    };

                    dgv.Columns.Add("Id", "ID");
                    dgv.Columns.Add("Date", "Payment Date");
                    dgv.Columns.Add("Tenant", "Tenant");
                    dgv.Columns.Add("Amount", "Amount");
                    dgv.Columns.Add("LabourAmount", "Labour Amount");
                    dgv.Columns.Add("Type", "Type");
                    dgv.Columns.Add("Status", "Status");
                    dgv.Columns.Add("DeletedDate", "Deleted On");
                    dgv.Columns.Add("Reason", "Reason");

                    // Enrich with tenant names
                    var tenants = _dataService.LoadTenants() ?? new List<Tenant>();
                    var agreements = _dataService.LoadAgreements() ?? new List<RentAgreement>();

                    if (allPayments != null)
                    {
                        foreach (var payment in allPayments.OrderByDescending(p => p.PaymentDate))
                        {
                            var agreement = agreements?.FirstOrDefault(a => a.Id == payment.AgreementId);
                            string tenantName = "Unknown";
                            if (agreement != null)
                            {
                                var tenant = tenants?.FirstOrDefault(t => t.Id == agreement.TenantId);
                                tenantName = tenant?.Name ?? "Unknown";
                            }

                            int rowIndex = dgv.Rows.Add(
                                payment.Id,
                                payment.PaymentDate.ToString("dd-MMM-yyyy"),
                                tenantName,
                                payment.Amount.ToString("C"),
                                payment.LaborAmount?.ToString("C"),
                                payment.PaymentType,
                                payment.IsDeleted ? "❌ DELETED" : "✅ ACTIVE",
                                payment.DeletedDate?.ToString("dd-MMM-yyyy HH:mm") ?? "",
                                payment.Notes
                            );

                            // Color coding
                            if (payment.IsDeleted)
                            {
                                dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                                dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                            }
                        }
                    }

                    dgv.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Columns["LabourAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    form.Controls.Add(dgv);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing complete history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAllPaymentHistory()
        {
            try
            {
                var allPayments = _dataService.LoadAllPayments() ?? new List<Payment>();
                var agreementPayments = allPayments?.Where(p => p.AgreementId == _agreementId)
                                                   .OrderByDescending(p => p.PaymentDate)
                                                   .ToList() ?? new List<Payment>();

                using (var form = new Form())
                {
                    form.Text = "Complete Payment History (Including Deleted)";
                    form.Size = new Size(1100, 500);
                    form.StartPosition = FormStartPosition.CenterParent;

                    var dgv = new DataGridView
                    {
                        Dock = DockStyle.Fill,
                        AllowUserToAddRows = false,
                        ReadOnly = true,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    };

                    dgv.Columns.Add("Id", "ID");
                    dgv.Columns.Add("Date", "Payment Date");
                    dgv.Columns.Add("Amount", "Amount");
                    dgv.Columns.Add("LabourAmount", "Labour Amount");
                    dgv.Columns.Add("Status", "Status");
                    dgv.Columns.Add("DeletedDate", "Deleted On");
                    dgv.Columns.Add("Reason", "Reason/Notes");

                    foreach (var payment in agreementPayments)
                    {
                        int rowIndex = dgv.Rows.Add(
                            payment.Id,
                            payment.PaymentDate.ToString("dd-MMM-yyyy"),
                            (payment.Amount + payment.LaborAmount)?.ToString("C"),
                            payment.IsDeleted ? "❌ DELETED" : "✅ ACTIVE",
                            payment.DeletedDate?.ToString("dd-MMM-yyyy HH:mm") ?? "",
                            payment.Notes
                        );

                        // Color coding
                        if (payment.IsDeleted)
                        {
                            dgv.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                            dgv.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                        }
                    }

                    dgv.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv.Columns["LabourAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    form.Controls.Add(dgv);
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing complete history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMonthlySummary_Click(object sender, EventArgs e)
        {
            try
            {
                ShowMonthlyPaymentSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing monthly summary: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowMonthlyPaymentSummary()
        {
            try
            {
                List<MonthlySummaryItem> monthlySummary;

                if (_agreementId == 0)
                {
                    monthlySummary = GetMonthlySummaryForAllPayments();
                }
                else
                {
                    monthlySummary = GetMonthlySummaryForAgreement();
                }

                // Apply filters to the monthly summary
                monthlySummary = ApplyMonthlySummaryFilters(monthlySummary);

                // Generate complete monthly summary including missing months
                monthlySummary = GenerateCompleteMonthlySummary(monthlySummary);

                if (!monthlySummary.Any())
                {
                    MessageBox.Show("No payment history found.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var summaryForm = new Form())
                {
                    summaryForm.Text = "📅 Monthly Payment Summary";
                    summaryForm.Size = new Size(1100, 600);
                    summaryForm.StartPosition = FormStartPosition.CenterParent;
                    summaryForm.BackColor = Color.White;

                    Panel mainPanel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Padding = new Padding(15)
                    };

                    // Header
                    Label lblHeader = new Label
                    {
                        Text = "📊 MONTHLY PAYMENT SUMMARY",
                        Font = new Font("Segoe UI", 16, FontStyle.Bold),
                        ForeColor = Color.SteelBlue,
                        Dock = DockStyle.Top,
                        Height = 50,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // Data Grid View
                    DataGridView dgvSummary = new DataGridView
                    {
                        Dock = DockStyle.Fill,
                        AllowUserToAddRows = false,
                        ReadOnly = true,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                        BackgroundColor = Color.White,
                        BorderStyle = BorderStyle.Fixed3D,
                        AllowUserToOrderColumns = true,
                        ColumnHeadersHeight = 40
                    };

                    // Add columns
                    DataGridViewTextBoxColumn colPeriod = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Period",
                        Name = "Period",
                        Width = 80
                    };

                    DataGridViewTextBoxColumn colMonth = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Month",
                        Name = "Month",
                        Width = 150
                    };

                    DataGridViewTextBoxColumn colPayments = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Total Payments",
                        Name = "Payments",
                        Width = 100
                    };

                    DataGridViewTextBoxColumn colRent = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Rent Payments",
                        Name = "Rent",
                        Width = 100
                    };

                    DataGridViewTextBoxColumn colCommission = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Commission Payments",
                        Name = "Commission",
                        Width = 120
                    };

                    DataGridViewTextBoxColumn colTotalAmount = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Total Amount",
                        Name = "TotalAmount",
                        Width = 120
                    };

                    DataGridViewTextBoxColumn colTotalLabour = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Total Labour",
                        Name = "TotalLabour",
                        Width = 120
                    };

                    DataGridViewTextBoxColumn colTotalCombined = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Total Combined",
                        Name = "TotalCombined",
                        Width = 120
                    };

                    DataGridViewTextBoxColumn colAverage = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Avg Payment",
                        Name = "Average",
                        Width = 100
                    };

                    DataGridViewTextBoxColumn colLastPayment = new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Last Payment",
                        Name = "LastPayment",
                        Width = 120
                    };

                    dgvSummary.Columns.AddRange(new DataGridViewColumn[] {
                colPeriod, colMonth, colPayments, colRent, colCommission,
                colTotalAmount, colTotalLabour, colTotalCombined,
                colAverage, colLastPayment
            });

                    // Add data
                    foreach (var month in monthlySummary.OrderByDescending(m => m.Year).ThenByDescending(m => m.Month))
                    {
                        decimal totalCombined = month.TotalAmount + month.TotalLabourAmount;
                        decimal averagePayment = month.PaymentCount > 0 ? totalCombined / month.PaymentCount : 0;

                        int rowIndex = dgvSummary.Rows.Add(
                            $"{month.Year}-{month.Month:00}",
                            $"{month.MonthName} {month.Year}",
                            month.PaymentCount,
                            month.RentPayments,
                            month.CommissionPayments,
                            month.TotalAmount.ToString("N2"),
                            month.TotalLabourAmount.ToString("N2"),
                            totalCombined.ToString("N2"),
                            averagePayment.ToString("N2"),
                            month.LastPaymentDate > DateTime.MinValue ? month.LastPaymentDate.ToString("dd-MMM-yyyy") : "No payments"
                        );

                        // Color code based on payment count
                        DataGridViewRow row = dgvSummary.Rows[rowIndex];

                        if (month.PaymentCount >= 3)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
                            row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                        }
                        else if (month.PaymentCount == 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                            row.DefaultCellStyle.ForeColor = Color.DarkRed;
                        }
                        else if (month.PaymentCount == 1)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                            row.DefaultCellStyle.ForeColor = Color.DarkGoldenrod;
                        }

                        // Format amount columns
                        row.Cells["TotalAmount"].Style.Font = new Font(dgvSummary.Font, FontStyle.Bold);
                        row.Cells["TotalLabour"].Style.Font = new Font(dgvSummary.Font, FontStyle.Bold);
                        row.Cells["TotalCombined"].Style.Font = new Font(dgvSummary.Font, FontStyle.Bold);
                    }

                    // Format columns
                    dgvSummary.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvSummary.Columns["TotalLabour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvSummary.Columns["TotalCombined"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvSummary.Columns["Average"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvSummary.Columns["Payments"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvSummary.Columns["Rent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvSummary.Columns["Commission"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Style headers
                    dgvSummary.EnableHeadersVisualStyles = false;
                    dgvSummary.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
                    dgvSummary.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgvSummary.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dgvSummary.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Calculate statistics
                    decimal totalAllTime = monthlySummary.Sum(m => m.TotalAmount);
                    decimal totalLabourAllTime = monthlySummary.Sum(m => m.TotalLabourAmount);
                    decimal totalCombinedAllTime = totalAllTime + totalLabourAllTime;
                    int totalPayments = monthlySummary.Sum(m => m.PaymentCount);
                    decimal avgMonthly = monthlySummary.Count > 0 ? monthlySummary.Average(m => m.TotalAmount) : 0;
                    decimal avgMonthlyLabour = monthlySummary.Count > 0 ? monthlySummary.Average(m => m.TotalLabourAmount) : 0;

                    var monthsWithPayments = monthlySummary.Where(m => m.PaymentCount > 0).ToList();
                    var bestMonth = monthsWithPayments.Any() ? monthsWithPayments.OrderByDescending(m => m.PaymentCount).FirstOrDefault() : null;
                    var highestEarningMonth = monthsWithPayments.Any() ? monthsWithPayments.OrderByDescending(m => m.TotalAmount + m.TotalLabourAmount).FirstOrDefault() : null;

                    // Summary statistics panel
                    Panel statsPanel = new Panel
                    {
                        Dock = DockStyle.Bottom,
                        Height = 100,
                        BackColor = Color.FromArgb(240, 245, 250),
                        Padding = new Padding(15, 10, 15, 10)
                    };

                    Label lblStats = new Label
                    {
                        Text = $"📈 SUMMARY STATISTICS:\n" +
                               $"• Total Payments: {totalPayments} | Total Amount: {totalAllTime:N2} | Total Labour: {totalLabourAllTime:N2}\n" +
                               $"• Combined Total: {totalCombinedAllTime:N2} | Avg Monthly: {(avgMonthly + avgMonthlyLabour):N2}\n" +
                               $"• Best Month (Payments): {(bestMonth != null ? $"{bestMonth.MonthName} {bestMonth.Year} ({bestMonth.PaymentCount} payments)" : "N/A")}\n" +
                               $"• Highest Earning: {(highestEarningMonth != null ? $"{highestEarningMonth.MonthName} {highestEarningMonth.Year} ({(highestEarningMonth.TotalAmount + highestEarningMonth.TotalLabourAmount):N2})" : "N/A")}",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = new Font("Segoe UI", 9, FontStyle.Regular),
                        ForeColor = Color.DarkSlateGray
                    };

                    statsPanel.Controls.Add(lblStats);

                    // Button Panel
                    Panel buttonPanel = new Panel
                    {
                        Dock = DockStyle.Bottom,
                        Height = 70,
                        BackColor = Color.FromArgb(240, 240, 240),
                        Padding = new Padding(20, 10, 20, 10)
                    };

                    Button btnExportSummary = new Button
                    {
                        Text = "📤 Export to CSV",
                        Size = new Size(140, 35),
                        Location = new Point(20, 10),
                        BackColor = Color.LightSteelBlue,
                        Font = new Font("Segoe UI", 9, FontStyle.Regular)
                    };
                    btnExportSummary.Click += (s, ev) => ExportMonthlySummary(monthlySummary);

                    Button btnPrint = new Button
                    {
                        Text = "🖨️ Print Summary",
                        Size = new Size(140, 35),
                        Location = new Point(170, 10),
                        BackColor = Color.LightSteelBlue,
                        Font = new Font("Segoe UI", 9, FontStyle.Regular)
                    };
                    btnPrint.Click += (s, ev) => PrintMonthlySummary(monthlySummary);

                    Button btnClose = new Button
                    {
                        Text = "Close",
                        Size = new Size(100, 35),
                        Location = new Point(320, 10),
                        DialogResult = DialogResult.Cancel,
                        Font = new Font("Segoe UI", 9)
                    };
                    btnClose.Click += (s, ev) => summaryForm.Close();

                    buttonPanel.Controls.AddRange(new Control[] { btnExportSummary, btnPrint, btnClose });

                    mainPanel.Controls.AddRange(new Control[] { lblHeader, dgvSummary, statsPanel });
                    summaryForm.Controls.AddRange(new Control[] { mainPanel, buttonPanel });

                    // Add double-click event to view monthly details
                    dgvSummary.CellDoubleClick += (s, ev) =>
                    {
                        try
                        {
                            if (ev.RowIndex >= 0 && ev.ColumnIndex >= 0)
                            {
                                var selectedMonth = monthlySummary
                                    .OrderByDescending(m => m.Year)
                                    .ThenByDescending(m => m.Month)
                                    .ElementAt(ev.RowIndex);

                                ShowMonthlyDetails(selectedMonth);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error showing monthly details: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    summaryForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating monthly summary: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<MonthlySummaryItem> ApplyMonthlySummaryFilters(List<MonthlySummaryItem> monthlySummary)
        {
            try
            {
                if (monthlySummary == null || !monthlySummary.Any())
                    return new List<MonthlySummaryItem>();

                IEnumerable<MonthlySummaryItem> filtered = monthlySummary;

                // Apply year filter
                if (cmbYearFilter.SelectedIndex > 0 && int.TryParse(cmbYearFilter.SelectedItem?.ToString(), out int selectedYear))
                {
                    filtered = filtered.Where(m => m.Year == selectedYear);
                }

                // Apply month filter - need to convert month name to number
                if (cmbMonthFilter.SelectedIndex > 0)
                {
                    string selectedMonthName = cmbMonthFilter.SelectedItem?.ToString() ?? "";
                    int selectedMonth = Array.IndexOf(new[] {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            }, selectedMonthName) + 1;

                    filtered = filtered.Where(m => m.Month == selectedMonth);
                }

                return filtered.ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying monthly filters: {ex.Message}");
                return monthlySummary ?? new List<MonthlySummaryItem>();
            }
        }

        private List<MonthlySummaryItem> GenerateCompleteMonthlySummary(List<MonthlySummaryItem> existingSummary)
        {
            try
            {
                var completeSummary = new List<MonthlySummaryItem>();

                if (!existingSummary.Any())
                    return completeSummary;

                // Determine date range
                int minYear = existingSummary.Min(m => m.Year);
                int maxYear = existingSummary.Max(m => m.Year);

                // If year filter is applied, use that year only
                if (cmbYearFilter.SelectedIndex > 0 && int.TryParse(cmbYearFilter.SelectedItem?.ToString(), out int selectedYear))
                {
                    minYear = maxYear = selectedYear;
                }

                // Generate all months in the range
                for (int year = minYear; year <= maxYear; year++)
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        // Skip months if month filter is applied
                        if (cmbMonthFilter.SelectedIndex > 0)
                        {
                            string selectedMonthName = cmbMonthFilter.SelectedItem?.ToString() ?? "";
                            int selectedMonth = Array.IndexOf(new[] {
                        "January", "February", "March", "April", "May", "June",
                        "July", "August", "September", "October", "November", "December"
                    }, selectedMonthName) + 1;

                            if (month != selectedMonth)
                                continue;
                        }

                        var existingMonth = existingSummary.FirstOrDefault(m => m.Year == year && m.Month == month);

                        if (existingMonth != null)
                        {
                            completeSummary.Add(existingMonth);
                        }
                        else
                        {
                            // Create empty month entry
                            completeSummary.Add(new MonthlySummaryItem
                            {
                                Year = year,
                                Month = month,
                                MonthName = new DateTime(year, month, 1).ToString("MMMM"),
                                TotalAmount = 0,
                                TotalLabourAmount = 0,
                                PaymentCount = 0,
                                RentPayments = 0,
                                CommissionPayments = 0,
                                LastPaymentDate = DateTime.MinValue,
                                AverageAmount = 0
                            });
                        }
                    }
                }

                return completeSummary.OrderByDescending(m => m.Year).ThenByDescending(m => m.Month).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating complete summary: {ex.Message}");
                return existingSummary ?? new List<MonthlySummaryItem>();
            }
        }

        private void ShowMonthlyDetails(MonthlySummaryItem month)
        {
            try
            {
                // Get payments for this month
                var payments = _agreementId == 0
                    ? GetPaymentsForMonth(month.Year, month.Month)
                    : GetPaymentsForMonthAndAgreement(month.Year, month.Month, _agreementId);

                if (payments == null || !payments.Any())
                {
                    MessageBox.Show($"No payments found for {month.MonthName} {month.Year}",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var detailsForm = new Form())
                {
                    detailsForm.Text = $"Payments for {month.MonthName} {month.Year}";
                    detailsForm.Size = new Size(1200, 500);
                    detailsForm.StartPosition = FormStartPosition.CenterParent;

                    var dgv = new DataGridView
                    {
                        Dock = DockStyle.Fill,
                        AllowUserToAddRows = false,
                        ReadOnly = true,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                        BackgroundColor = Color.White
                    };

                    if (_agreementId == 0)
                    {
                        dgv.Columns.Add("Date", "Payment Date");
                        dgv.Columns.Add("Tenant", "Tenant");
                        dgv.Columns.Add("Property", "Property");
                        dgv.Columns.Add("Amount", "Amount");
                        dgv.Columns.Add("Labour", "Labour");
                        dgv.Columns.Add("Type", "Type");
                        dgv.Columns.Add("Method", "Method");
                        dgv.Columns.Add("Reference", "Reference");
                        dgv.Columns.Add("Notes", "Notes");
                    }
                    else
                    {
                        dgv.Columns.Add("Date", "Payment Date");
                        dgv.Columns.Add("Amount", "Amount");
                        dgv.Columns.Add("Labour", "Labour");
                        dgv.Columns.Add("Type", "Type");
                        dgv.Columns.Add("Method", "Method");
                        dgv.Columns.Add("Reference", "Reference");
                        dgv.Columns.Add("Notes", "Notes");
                    }

                    foreach (var payment in payments.OrderBy(p => p.PaymentDate))
                    {
                        if (_agreementId == 0)
                        {
                            var tenantName = GetTenantNameForPayment(payment);
                            var propertyName = GetPropertyNameForPayment(payment);

                            dgv.Rows.Add(
                                payment.PaymentDate.ToString("dd-MMM-yyyy"),
                                tenantName,
                                propertyName,
                                payment.Amount.ToString("N2"),
                                payment.LaborAmount?.ToString("N2"),
                                payment.PaymentType.ToString(),
                                "Cash", // You might want to store payment method in your Payment model
                                payment.Id,
                                payment.Notes
                            );
                        }
                        else
                        {
                            dgv.Rows.Add(
                                payment.PaymentDate.ToString("dd-MMM-yyyy"),
                                payment.Amount.ToString("N2"),
                                payment.LaborAmount?.ToString("N2"),
                                payment.PaymentType.ToString(),
                                "Cash",
                                payment.Id,
                                payment.Notes
                            );
                        }
                    }

                    // Format currency columns
                    if (dgv.Columns.Contains("Amount"))
                        dgv.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (dgv.Columns.Contains("Labour"))
                        dgv.Columns["Labour"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    // Add summary label
                    Label lblSummary = new Label
                    {
                        Text = $"Total: {month.TotalAmount:N2} | Labour: {month.TotalLabourAmount:N2} | Combined: {(month.TotalAmount + month.TotalLabourAmount):N2} | Payments: {month.PaymentCount}",
                        Dock = DockStyle.Bottom,
                        Height = 30,
                        BackColor = Color.LightSteelBlue,
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    detailsForm.Controls.AddRange(new Control[] { dgv, lblSummary });
                    detailsForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing monthly details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Payment> GetPaymentsForMonth(int year, int month)
        {
            try
            {
                var allPayments = _dataService.LoadAllPayments() ?? new List<Payment>();
                return allPayments
                    .Where(p => !p.IsDeleted &&
                               p.PaymentDate.Year == year &&
                               p.PaymentDate.Month == month)
                    .ToList();
            }
            catch
            {
                return new List<Payment>();
            }
        }

        private List<Payment> GetPaymentsForMonthAndAgreement(int year, int month, int agreementId)
        {
            try
            {
                var payments = _dataService.GetPaymentsByAgreement(agreementId) ?? new List<Payment>();
                return payments
                    .Where(p => p.PaymentDate.Year == year && p.PaymentDate.Month == month)
                    .ToList();
            }
            catch
            {
                return new List<Payment>();
            }
        }

        private string GetTenantNameForPayment(Payment payment)
        {
            try
            {
                if (payment == null) return "Unknown";

                var agreements = _dataService.LoadAgreements() ?? new List<RentAgreement>();
                var agreement = agreements.FirstOrDefault(a => a.Id == payment.AgreementId);

                if (agreement == null) return "Unknown";

                var tenants = _dataService.LoadTenants() ?? new List<Tenant>();
                var tenant = tenants.FirstOrDefault(t => t.Id == agreement.TenantId);

                return tenant?.Name ?? "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }

        private string GetPropertyNameForPayment(Payment payment)
        {
            try
            {
                if (payment == null) return "Unknown";

                var agreements = _dataService.LoadAgreements() ?? new List<RentAgreement>();
                var agreement = agreements.FirstOrDefault(a => a.Id == payment.AgreementId);

                if (agreement == null) return "Unknown";

                var properties = _dataService.LoadProperties() ?? new List<Property>();
                var property = properties.FirstOrDefault(p => p.Id == agreement.PropertyId);

                return property?.Name ?? "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }

        private void PrintMonthlySummary(List<MonthlySummaryItem> monthlySummary)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += (sender, e) =>
                {
                    Graphics g = e.Graphics;
                    Font titleFont = new Font("Arial", 18, FontStyle.Bold);
                    Font headerFont = new Font("Arial", 12, FontStyle.Bold);
                    Font contentFont = new Font("Arial", 10);

                    float yPos = 50;
                    float leftMargin = e.MarginBounds.Left;

                    // Print title
                    g.DrawString("Monthly Payment Summary", titleFont, Brushes.Black, leftMargin, yPos);
                    yPos += 40;

                    // Print date
                    g.DrawString($"Generated on: {DateTime.Now:dd-MMM-yyyy HH:mm}", contentFont, Brushes.Black, leftMargin, yPos);
                    yPos += 30;

                    // Print table headers
                    float[] columnWidths = { 100, 150, 80, 80, 100, 100, 100, 100 };
                    string[] headers = { "Period", "Month", "Payments", "Rent", "Commission", "Amount", "Labour", "Total" };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        g.DrawString(headers[i], headerFont, Brushes.Black, leftMargin, yPos);
                        leftMargin += columnWidths[i];
                    }

                    yPos += 30;
                    leftMargin = e.MarginBounds.Left;

                    // Print data
                    foreach (var month in monthlySummary.Take(20)) // Limit to first 20 rows
                    {
                        decimal total = month.TotalAmount + month.TotalLabourAmount;

                        g.DrawString($"{month.Year}-{month.Month:00}", contentFont, Brushes.Black, leftMargin, yPos);
                        leftMargin += columnWidths[0];

                        g.DrawString($"{month.MonthName} {month.Year}", contentFont, Brushes.Black, leftMargin, yPos);
                        leftMargin += columnWidths[1];

                        g.DrawString(month.PaymentCount.ToString(), contentFont, Brushes.Black, leftMargin, yPos);
                        leftMargin += columnWidths[2];

                        g.DrawString(month.RentPayments.ToString(), contentFont, Brushes.Black, leftMargin, yPos);
                        leftMargin += columnWidths[3];

                        g.DrawString(month.CommissionPayments.ToString(), contentFont, Brushes.Black, leftMargin, yPos);
                        leftMargin += columnWidths[4];

                        g.DrawString(month.TotalAmount.ToString("N2"), contentFont, Brushes.Black, leftMargin, yPos);
                        leftMargin += columnWidths[5];

                        g.DrawString(month.TotalLabourAmount.ToString("N2"), contentFont, Brushes.Black, leftMargin, yPos);
                        leftMargin += columnWidths[6];

                        g.DrawString(total.ToString("N2"), contentFont, Brushes.Black, leftMargin, yPos);

                        yPos += 25;
                        leftMargin = e.MarginBounds.Left;

                        // Check if we need another page
                        if (yPos > e.MarginBounds.Bottom)
                        {
                            e.HasMorePages = true;
                            yPos = 50;
                        }
                    }
                };

                printDialog.Document = printDoc;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<MonthlySummaryItem> GetMonthlySummaryForAllPayments()
        {
            try
            {
                var payments = _dataService.LoadAllPayments()
                    ?.Where(p => !p.IsDeleted)
                    .ToList() ?? new List<Payment>();

                var monthlySummary = payments
                    .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
                    .Select(g => new MonthlySummaryItem
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM"),
                        TotalAmount = g.Sum(p => p.Amount),
                        TotalLabourAmount = (decimal)g.Sum(p => p.LaborAmount),
                        PaymentCount = g.Count(),
                        RentPayments = g.Count(p => p.PaymentType == PaymentType.Rent),
                        CommissionPayments = g.Count(p => p.PaymentType == PaymentType.Commission),
                        LastPaymentDate = g.Max(p => p.PaymentDate),
                        AverageAmount = g.Average(p => p.Amount)
                    })
                    .OrderByDescending(m => m.Year)
                    .ThenByDescending(m => m.Month)
                    .ToList();

                return monthlySummary;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting monthly summary: {ex.Message}");
                return new List<MonthlySummaryItem>();
            }
        }

        private List<MonthlySummaryItem> GetMonthlySummaryForAgreement()
        {
            try
            {
                var payments = _dataService.GetPaymentsByAgreement(_agreementId) ?? new List<Payment>();

                var monthlySummary = payments
                    .GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
                    .Select(g => new MonthlySummaryItem
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM"),
                        TotalAmount = g.Sum(p => p.Amount),
                        TotalLabourAmount = (decimal)g.Sum(p => p.LaborAmount),
                        PaymentCount = g.Count(),
                        RentPayments = g.Count(p => p.PaymentType == PaymentType.Rent),
                        CommissionPayments = g.Count(p => p.PaymentType == PaymentType.Commission),
                        LastPaymentDate = g.Max(p => p.PaymentDate),
                        AverageAmount = g.Average(p => p.Amount)
                    })
                    .OrderByDescending(m => m.Year)
                    .ThenByDescending(m => m.Month)
                    .ToList();

                return monthlySummary;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting monthly summary for agreement: {ex.Message}");
                return new List<MonthlySummaryItem>();
            }
        }

        private void ExportMonthlySummary(List<MonthlySummaryItem> monthlySummary)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv",
                    FileName = $"Monthly_Payment_Summary_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                    Title = "Export Monthly Summary"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = new StreamWriter(saveDialog.FileName))
                    {
                        writer.WriteLine("Year,Month,Month Name,Total Payments,Rent Payments,Commission Payments,Total Amount,Total Labour,Average Amount,Last Payment Date");

                        foreach (var month in monthlySummary)
                        {
                            writer.WriteLine($"{month.Year},{month.Month},{month.MonthName},{month.PaymentCount}," +
                                           $"{month.RentPayments},{month.CommissionPayments},{month.TotalAmount:F2}," +
                                           $"{month.TotalLabourAmount:F2},{month.AverageAmount:F2},{month.LastPaymentDate:dd-MMM-yyyy}");
                        }
                    }

                    MessageBox.Show($"Monthly summary exported successfully to:\n{saveDialog.FileName}",
                        "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting summary: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportPaymentHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportPaymentHistory()
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx",
                    FileName = $"Payment_History_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                    Title = "Export Payment History"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveDialog.FileName.EndsWith(".csv"))
                    {
                        using (var writer = new StreamWriter(saveDialog.FileName))
                        {
                            if (_agreementId == 0)
                            {
                                writer.WriteLine("ID,Payment Date,Tenant,Property,Portion,Amount,Labour Amount,Type,Method,Reference ID,For Month,Recorded On,Notes");

                                foreach (DataGridViewRow row in dgvPayments.Rows)
                                {
                                    if (row.IsNewRow) continue;

                                    writer.WriteLine(
                                        $"\"{GetSafeCellValue(row, "Id")}\"," +
                                        $"\"{GetSafeCellValue(row, "PaymentDate")}\"," +
                                        $"\"{GetSafeCellValue(row, "TenantName")}\"," +
                                        $"\"{GetSafeCellValue(row, "PropertyName")}\"," +
                                        $"\"{GetSafeCellValue(row, "PortionName")}\"," +
                                        $"\"{GetSafeCellValue(row, "Amount")}\"," +
                                        $"\"{GetSafeCellValue(row, "LabourAmount")}\"," +
                                        $"\"{GetSafeCellValue(row, "PaymentType")}\"," +
                                        $"\"{GetSafeCellValue(row, "PaymentMethod")}\"," +
                                        $"\"{GetSafeCellValue(row, "ReferenceNumber")}\"," +
                                        $"\"{GetSafeCellValue(row, "MonthYear")}\"," +
                                        $"\"{GetSafeCellValue(row, "CreatedDate")}\"," +
                                        $"\"{GetSafeCellValue(row, "Notes")?.Replace("\"", "\"\"")}\""
                                    );
                                }
                            }
                            else
                            {
                                writer.WriteLine("ID,Payment Date,For Month,Amount,Labour Amount,Type,Method,Reference ID,Recorded On,Notes");

                                foreach (DataGridViewRow row in dgvPayments.Rows)
                                {
                                    if (row.IsNewRow) continue;

                                    writer.WriteLine(
                                        $"\"{GetSafeCellValue(row, "Id")}\"," +
                                        $"\"{GetSafeCellValue(row, "PaymentDate")}\"," +
                                        $"\"{GetSafeCellValue(row, "MonthYear")}\"," +
                                        $"\"{GetSafeCellValue(row, "Amount")}\"," +
                                        $"\"{GetSafeCellValue(row, "LabourAmount")}\"," +
                                        $"\"{GetSafeCellValue(row, "PaymentType")}\"," +
                                        $"\"{GetSafeCellValue(row, "PaymentMethod")}\"," +
                                        $"\"{GetSafeCellValue(row, "ReferenceNumber")}\"," +
                                        $"\"{GetSafeCellValue(row, "CreatedDate")}\"," +
                                        $"\"{GetSafeCellValue(row, "Notes")?.Replace("\"", "\"\"")}\""
                                    );
                                }
                            }
                        }

                        MessageBox.Show($"Payment history exported successfully to:\n{saveDialog.FileName}",
                            "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Excel export requires EPPlus library. Exporting as CSV instead.",
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPayments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    ShowPaymentDetails(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing payment details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowPaymentDetails(int rowIndex)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= dgvPayments.Rows.Count) return;

                if (_agreementId == 0)
                {
                    // For all payments view
                    if (!int.TryParse(GetSafeCellValue(dgvPayments.Rows[rowIndex], "Id"), out int paymentId))
                        return;

                    DateTime paymentDate = DateTime.MinValue;
                    DateTime.TryParse(GetSafeCellValue(dgvPayments.Rows[rowIndex], "PaymentDate"), out paymentDate);

                    string tenantName = GetSafeCellValue(dgvPayments.Rows[rowIndex], "TenantName");
                    string propertyName = GetSafeCellValue(dgvPayments.Rows[rowIndex], "PropertyName");

                    decimal amount = 0;
                    decimal.TryParse(GetSafeCellValue(dgvPayments.Rows[rowIndex], "Amount"), out amount);

                    decimal labourAmount = 0;
                    decimal.TryParse(GetSafeCellValue(dgvPayments.Rows[rowIndex], "LabourAmount"), out labourAmount);

                    string paymentType = GetSafeCellValue(dgvPayments.Rows[rowIndex], "PaymentType");
                    string notes = GetSafeCellValue(dgvPayments.Rows[rowIndex], "Notes");

                    ShowAllPaymentsDetails(paymentId, paymentDate, tenantName, propertyName, amount, labourAmount, paymentType, notes);
                }
                else
                {
                    // For single agreement view
                    if (!int.TryParse(GetSafeCellValue(dgvPayments.Rows[rowIndex], "Id"), out int paymentId))
                        return;

                    DateTime paymentDate = DateTime.MinValue;
                    DateTime.TryParse(GetSafeCellValue(dgvPayments.Rows[rowIndex], "PaymentDate"), out paymentDate);

                    decimal amount = 0;
                    decimal.TryParse(GetSafeCellValue(dgvPayments.Rows[rowIndex], "Amount"), out amount);

                    decimal labourAmount = 0;
                    decimal.TryParse(GetSafeCellValue(dgvPayments.Rows[rowIndex], "LabourAmount"), out labourAmount);

                    string paymentType = GetSafeCellValue(dgvPayments.Rows[rowIndex], "PaymentType");
                    string notes = GetSafeCellValue(dgvPayments.Rows[rowIndex], "Notes");

                    ShowSinglePaymentDetails(paymentId, paymentDate, amount, labourAmount, paymentType, notes, rowIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing payment details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAllPaymentsDetails(int paymentId, DateTime paymentDate, string tenantName,
            string propertyName, decimal amount, decimal labourAmount, string paymentType, string notes)
        {
            try
            {
                using (var detailsForm = new Form())
                {
                    detailsForm.Text = "Payment Details";
                    detailsForm.Size = new Size(500, 400);
                    detailsForm.StartPosition = FormStartPosition.CenterParent;
                    detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;

                    Panel mainPanel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Padding = new Padding(20)
                    };

                    // Header
                    Label lblHeader = new Label
                    {
                        Text = "💰 PAYMENT DETAILS",
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        ForeColor = Color.SteelBlue,
                        Location = new Point(0, 10),
                        Size = new Size(440, 30),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // Details
                    int yPos = 60;
                    AddDetailLabel(mainPanel, "📅 Date:", $"{paymentDate:dddd, dd MMMM yyyy}", ref yPos);
                    AddDetailLabel(mainPanel, "👤 Tenant:", tenantName ?? "N/A", ref yPos);
                    AddDetailLabel(mainPanel, "🏢 Property:", propertyName ?? "N/A", ref yPos);
                    AddDetailLabel(mainPanel, "💰 Amount:", $"{amount:C}", ref yPos);
                    AddDetailLabel(mainPanel, "💰 Labour Amount:", $"{labourAmount:C}", ref yPos);
                    AddDetailLabel(mainPanel, "🏷️ Type:", paymentType ?? "N/A", ref yPos);

                    // Notes
                    Label lblNotes = new Label
                    {
                        Text = "📝 Notes:",
                        Location = new Point(0, yPos),
                        Size = new Size(440, 25),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    };
                    yPos += 30;

                    TextBox txtNotes = new TextBox
                    {
                        Location = new Point(0, yPos),
                        Size = new Size(440, 80),
                        Multiline = true,
                        ReadOnly = true,
                        Text = notes ?? "",
                        ScrollBars = ScrollBars.Vertical,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    yPos += 100;

                    // Buttons
                    Button btnClose = new Button
                    {
                        Text = "Close",
                        Location = new Point(200, yPos),
                        Size = new Size(100, 35),
                        DialogResult = DialogResult.Cancel
                    };

                    mainPanel.Controls.AddRange(new Control[] {
                    lblHeader, lblNotes, txtNotes, btnClose
                });

                    detailsForm.Controls.Add(mainPanel);
                    detailsForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing payment details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDetailLabel(Panel panel, string label, string value, ref int yPos)
        {
            try
            {
                Label lblLabel = new Label
                {
                    Text = label,
                    Location = new Point(0, yPos),
                    Size = new Size(100, 25),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };

                Label lblValue = new Label
                {
                    Text = value,
                    Location = new Point(110, yPos),
                    Size = new Size(330, 25),
                    Font = new Font("Segoe UI", 10)
                };

                panel.Controls.Add(lblLabel);
                panel.Controls.Add(lblValue);
                yPos += 30;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding detail label: {ex.Message}");
            }
        }

        private void ShowSinglePaymentDetails(int paymentId, DateTime paymentDate, decimal amount,
            decimal labourAmount, string paymentType, string notes, int rowIndex)
        {
            try
            {
                using (var detailsForm = new Form())
                {
                    detailsForm.Text = "Payment Details";
                    detailsForm.Size = new Size(500, 350);
                    detailsForm.StartPosition = FormStartPosition.CenterParent;
                    detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;

                    Panel mainPanel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Padding = new Padding(20)
                    };

                    // Header
                    Label lblHeader = new Label
                    {
                        Text = "💰 PAYMENT DETAILS",
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        ForeColor = Color.SteelBlue,
                        Location = new Point(0, 10),
                        Size = new Size(440, 30),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // Details
                    Label lblDate = new Label
                    {
                        Text = $"📅 Date: {paymentDate:dddd, dd MMMM yyyy}",
                        Location = new Point(0, 60),
                        Size = new Size(440, 25),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    };

                    Label lblAmount = new Label
                    {
                        Text = $"💰 Amount: {amount:C}",
                        Location = new Point(0, 90),
                        Size = new Size(440, 25),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    };

                    Label lblLabourAmount = new Label
                    {
                        Text = $"💰 Labour Amount: {labourAmount:C}",
                        Location = new Point(0, 120),
                        Size = new Size(440, 25),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    };

                    Label lblType = new Label
                    {
                        Text = $"🏷️ Type: {paymentType}",
                        Location = new Point(0, 150),
                        Size = new Size(440, 25),
                        Font = new Font("Segoe UI", 10)
                    };

                    Label lblNotes = new Label
                    {
                        Text = "📝 Notes:",
                        Location = new Point(0, 180),
                        Size = new Size(440, 25),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold)
                    };

                    TextBox txtNotes = new TextBox
                    {
                        Location = new Point(0, 210),
                        Size = new Size(440, 80),
                        Multiline = true,
                        ReadOnly = true,
                        Text = notes,
                        ScrollBars = ScrollBars.Vertical,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    // Buttons
                    Button btnDelete = new Button
                    {
                        Text = "🗑️ Delete Payment",
                        Location = new Point(150, 300),
                        Size = new Size(140, 35),
                        BackColor = Color.LightCoral,
                        ForeColor = Color.DarkRed,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold)
                    };
                    btnDelete.Click += (s, e) =>
                    {
                        detailsForm.Close();
                        ShowDeleteConfirmationDialog(paymentId, paymentDate, amount + labourAmount, rowIndex);
                    };

                    Button btnClose = new Button
                    {
                        Text = "Close",
                        Location = new Point(300, 300),
                        Size = new Size(80, 35),
                        DialogResult = DialogResult.Cancel
                    };

                    mainPanel.Controls.AddRange(new Control[] {
                    lblHeader, lblDate, lblAmount, lblLabourAmount, lblType, lblNotes, txtNotes,
                    btnDelete, btnClose
                });

                    detailsForm.Controls.Add(mainPanel);
                    detailsForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing payment details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Monthly Summary Item class
    public class MonthlySummaryItem
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public decimal TotalLabourAmount { get; set; }
        public int PaymentCount { get; set; }
        public int RentPayments { get; set; }
        public int CommissionPayments { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public decimal AverageAmount { get; set; }
    }

    // Helper class for displaying enriched payment data
    public class PaymentDisplay
    {
        public int Id => Payment?.Id ?? 0;
        public DateTime PaymentDate => Payment?.PaymentDate ?? DateTime.MinValue;
        public string TenantName { get; set; } = "";
        public string PropertyName { get; set; } = "";
        public string PortionName { get; set; } = "";
        public decimal Amount => Payment?.Amount ?? 0;
        public decimal LabourAmount => (decimal)(Payment?.LaborAmount ?? 0);
        public string PaymentType => Payment?.PaymentType.ToString() ?? "";
        public string PaymentMethod => "Cash";
        public string ReferenceNumber => Payment?.Id.ToString() ?? "";
        public string MonthYear => Payment?.PaymentDate.ToString("MMM-yyyy") ?? "";
        public DateTime CreatedDate => Payment?.PaymentDate ?? DateTime.MinValue;
        public string Notes => Payment?.Notes ?? "";
        public Payment Payment { get; set; }
        public RentAgreement Agreement { get; set; }
    }
}