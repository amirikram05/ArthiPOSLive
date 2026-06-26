using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;
using ArthiPOS.Controls;
using ShopRentManagementSystem.Reports;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.ComponentModel;

namespace ShopRentManagementSystem
{
    public partial class FrmPropertyReport : Form
    {
        private readonly JsonDataService _dataService;
        private readonly ReportGenerator _reportGenerator;
        private TabControl tabControl;
        private TabPage tabGridView;
        private TabPage tabHtmlReport;
        private DataGridView dgvProperties;
        private ComboBox cmbPropertyTypeFilter;
        private UrduTextBox txtSearch;
        private Button btnGenerate;
        private Button btnExport;
        private Button btnClose;
        private Label lblSummary;
        private CheckBox chkShowOccupiedOnly;
        private Panel pnlBrowserPreview;
        private Button btnPrint;
        private Button btnRefreshHtml;
        private ComboBox cmbPropertySelection;
        private Button btnOpenInBrowser;
        private Label lblHtmlPreview;
        private string _currentHtmlReport = "";
        private List<Property> _allProperties = new List<Property>();
        private List<Portion> _allPortions = new List<Portion>();
        private List<RentAgreement> _allAgreements = new List<RentAgreement>();
        private List<Tenant> _allTenants = new List<Tenant>();

        // Helper class for ComboBox items
        public class PropertyComboBoxItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        public FrmPropertyReport()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            _reportGenerator = new ReportGenerator(_dataService);
            SetupKeyboardShortcuts();
            LoadAllData();
        }

        private void SetupKeyboardShortcuts()
        {
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        this.Close();
                        break;
                    case Keys.F5:
                        LoadAllData();
                        break;
                    case Keys.E:
                        if (e.Control) btnExport.PerformClick();
                        break;
                    case Keys.P:
                        if (e.Control) PrintReport();
                        break;
                    case Keys.B:
                        if (e.Control) OpenInBrowser();
                        break;
                }
            };
        }

        private void LoadAllData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Load all data
                _allProperties = _dataService.LoadProperties() ?? new List<Property>();
                _allPortions = _dataService.LoadPortions() ?? new List<Portion>();
                _allAgreements = _dataService.LoadAgreements() ?? new List<RentAgreement>();
                _allTenants = _dataService.LoadTenants() ?? new List<Tenant>();

                // Refresh grid view - Show ALL properties initially
                RefreshGridView();

                // Refresh HTML report tab
                InitializeHtmlReportTab();

                // Load summary
                UpdateSummary();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.Text = "🏢 Property Report";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.KeyPreview = true;

            // Header Panel
            Panel pnlHeader = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.SteelBlue,
                Padding = new Padding(20)
            };

            Label lblTitle = new Label
            {
                Text = "🏢 PROPERTY MANAGEMENT REPORT",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlHeader.Controls.Add(lblTitle);

            // Filter Panel
            Panel pnlFilter = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(20)
            };

            // Search
            txtSearch = new UrduTextBox
            {
                WaterMarkText = "Search properties...",
                Location = new Point(20, 15),
                Size = new Size(200, 25),
                LangEnglish = true
            };
            txtSearch.TextChanged += (s, e) => ApplyFilters();

            // Property Type Filter
            Label lblType = new Label
            {
                Text = "Property Type:",
                Location = new Point(240, 15),
                Size = new Size(90, 25)
            };

            cmbPropertyTypeFilter = new ComboBox
            {
                Location = new Point(340, 15),
                Size = new Size(120, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPropertyTypeFilter.Items.AddRange(new[] { "All Types", "Commercial", "NonCommercial" });
            cmbPropertyTypeFilter.SelectedIndex = 0;
            cmbPropertyTypeFilter.SelectedIndexChanged += (s, e) => ApplyFilters();

            // Occupied Only Filter
            chkShowOccupiedOnly = new CheckBox
            {
                Text = "Show Occupied Only",
                Location = new Point(480, 15),
                Size = new Size(150, 25)
            };
            chkShowOccupiedOnly.CheckedChanged += (s, e) => ApplyFilters();

            // Buttons
            btnGenerate = new Button
            {
                Text = "🔄 Refresh All (F5)",
                Location = new Point(650, 10),
                Size = new Size(130, 30),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGenerate.Click += (s, e) => LoadAllData();

            btnExport = new Button
            {
                Text = "📤 Export (Ctrl+E)",
                Location = new Point(790, 10),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnExport.Click += BtnExport_Click;

            pnlFilter.Controls.AddRange(new Control[] {
                txtSearch, lblType, cmbPropertyTypeFilter,
                chkShowOccupiedOnly, btnGenerate, btnExport
            });

            // Summary Label
            lblSummary = new Label
            {
                Height = 40,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(240, 248, 255),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(10)
            };

            // Tab Control
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Appearance = TabAppearance.FlatButtons,
                ItemSize = new Size(0, 1),
                SizeMode = TabSizeMode.Fixed
            };
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            // Tab 1: Grid View
            tabGridView = new TabPage
            {
                Text = "GridView",
                BackColor = Color.White
            };

            // Data Grid View
            dgvProperties = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = SystemColors.Window,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            tabGridView.Controls.Add(dgvProperties);

            // Tab 2: HTML Report
            tabHtmlReport = new TabPage
            {
                Text = "HTML Report",
                BackColor = Color.White
            };

            // HTML Report Controls Panel
            Panel pnlHtmlControls = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(245, 245, 245),
                Padding = new Padding(10)
            };

            Label lblSelectProperty = new Label
            {
                Text = "Select Property:",
                Location = new Point(10, 10),
                Size = new Size(90, 25),
                Font = new Font("Segoe UI", 9)
            };

            cmbPropertySelection = new ComboBox
            {
                Location = new Point(110, 8),
                Size = new Size(250, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                DisplayMember = "Name",
                ValueMember = "Id"
            };

            btnRefreshHtml = new Button
            {
                Text = "🔄 Generate Report",
                Location = new Point(370, 8),
                Size = new Size(140, 25),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnRefreshHtml.Click += BtnRefreshHtml_Click;

            btnPrint = new Button
            {
                Text = "🖨️ Print Report",
                Location = new Point(520, 8),
                Size = new Size(120, 25),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnPrint.Click += BtnPrint_Click;

            btnOpenInBrowser = new Button
            {
                Text = "🌐 Open in Browser",
                Location = new Point(650, 8),
                Size = new Size(150, 25),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOpenInBrowser.Click += (s, e) => OpenInBrowser();

            pnlHtmlControls.Controls.AddRange(new Control[] {
                lblSelectProperty, cmbPropertySelection, btnRefreshHtml,
                btnPrint, btnOpenInBrowser
            });

            // Preview Panel
            pnlBrowserPreview = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            // Label for HTML preview
            lblHtmlPreview = new Label
            {
                Text = "📋 HTML Report Preview\n\n" +
                       "1. Select a property from the dropdown above\n" +
                       "2. Click 'Generate Report' to create the report\n" +
                       "3. Use the buttons to print, save, or open in browser",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.DarkSlateBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                AutoSize = false
            };

            // Add buttons to preview panel
            Panel pnlPreviewButtons = new Panel
            {
                Height = 40,
                Dock = DockStyle.Bottom,
                Padding = new Padding(10)
            };

            Button btnQuickPreview = new Button
            {
                Text = "👁️ Quick Preview",
                Size = new Size(120, 30),
                Location = new Point(10, 5),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnQuickPreview.Click += (s, e) => ShowQuickPreview();

            Button btnSaveHtml = new Button
            {
                Text = "💾 Save HTML",
                Size = new Size(120, 30),
                Location = new Point(140, 5),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSaveHtml.Click += (s, e) => SaveHtmlToFile();

            pnlPreviewButtons.Controls.AddRange(new Control[] { btnQuickPreview, btnSaveHtml });

            pnlBrowserPreview.Controls.Add(lblHtmlPreview);
            pnlBrowserPreview.Controls.Add(pnlPreviewButtons);

            tabHtmlReport.Controls.AddRange(new Control[] { pnlBrowserPreview, pnlHtmlControls });

            tabControl.TabPages.Add(tabGridView);
            tabControl.TabPages.Add(tabHtmlReport);

            // Buttons Panel
            Panel pnlButtons = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(20)
            };

            // View Toggle Buttons
            Button btnViewGrid = new Button
            {
                Text = "📋 Grid View",
                Location = new Point(20, 15),
                Size = new Size(100, 30),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnViewGrid.Click += (s, e) => tabControl.SelectedIndex = 0;

            Button btnViewReport = new Button
            {
                Text = "📊 HTML Report",
                Location = new Point(130, 15),
                Size = new Size(120, 30),
                BackColor = Color.DarkSlateBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnViewReport.Click += (s, e) => tabControl.SelectedIndex = 1;

            // Statistics label
            Label lblStats = new Label
            {
                Name = "lblDetailedStats",
                Location = new Point(270, 20),
                Size = new Size(400, 20),
                Font = new Font("Segoe UI", 9)
            };

            btnClose = new Button
            {
                Text = "✖ Close (Esc)",
                Location = new Point(900, 15),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            btnClose.Click += (s, e) => this.Close();

            pnlButtons.Controls.AddRange(new Control[] {
                btnViewGrid, btnViewReport, lblStats, btnClose
            });

            this.Controls.AddRange(new Control[] {
                tabControl, lblSummary, pnlFilter, pnlHeader, pnlButtons
            });

            this.CancelButton = btnClose;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1) // HTML Report tab
            {
                // Ensure properties are loaded
                if (cmbPropertySelection.Items.Count == 0)
                {
                    InitializeHtmlReportTab();
                }
            }
        }

        private void InitializeHtmlReportTab()
        {
            try
            {
                // Clear existing items
                cmbPropertySelection.Items.Clear();
                cmbPropertySelection.SelectedItem = null;
                cmbPropertySelection.Text = "";

                // Add "All Properties" option
                cmbPropertySelection.Items.Add(new PropertyComboBoxItem
                {
                    Id = 0,
                    Name = "📊 All Properties Summary"
                });

                // Load properties if not already loaded
                if (_allProperties == null || _allProperties.Count == 0)
                {
                    _allProperties = _dataService.LoadProperties() ?? new List<Property>();
                }

                // Add individual properties
                if(cmbPropertySelection.SelectedIndex==0)
                {
                    return;
                }
                if (_allProperties != null && _allProperties.Count > 0)
                {
                    foreach (var property in _allProperties
                        .Where(p => p != null && !string.IsNullOrEmpty(p.Name))
                        .OrderBy(p => p.Name))
                    {
                        cmbPropertySelection.Items.Add(new PropertyComboBoxItem
                        {
                            Id = property.Id,
                            Name = $"{property.Name} ({property.Type})"
                        });
                    }
                }
                else
                {
                    cmbPropertySelection.Items.Add(new PropertyComboBoxItem
                    {
                        Id = -1,
                        Name = "No properties available"
                    });
                }

                // Select first item
                if (cmbPropertySelection.Items.Count > 0)
                {
                    cmbPropertySelection.SelectedIndex = 0;
                }

                // Update preview message
                lblHtmlPreview.Text = $"📋 HTML Report Ready\n\n" +
                                    $"• {_allProperties?.Count ?? 0} properties loaded\n" +
                                    $"• Select a property and click 'Generate Report'";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading properties: {ex.Message}",
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRefreshHtml_Click(object sender, EventArgs e)
        {
            GenerateHtmlReport();
        }

        private void GenerateHtmlReport()
        {
            try
            {
                if (cmbPropertySelection.SelectedItem == null)
                {
                    MessageBox.Show("Please select a property first.",
                        "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                var selectedItem = cmbPropertySelection.SelectedItem as PropertyComboBoxItem;

                if (selectedItem == null)
                {
                    MessageBox.Show("Invalid selection.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string htmlContent = "";

                if (selectedItem.Id == 0)
                {
                    // All properties summary
                    htmlContent = _reportGenerator.GeneratePropertySummaryReport();
                }
                else if (selectedItem.Id > 0)
                {
                    // Single property report
                    htmlContent = _reportGenerator.GeneratePropertySummaryReport(selectedItem.Id);
                }
                else
                {
                    MessageBox.Show("No properties available to generate report.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!string.IsNullOrEmpty(htmlContent))
                {
                    _currentHtmlReport = htmlContent;
                    UpdateHtmlPreviewInfo(htmlContent);

                    MessageBox.Show($"Report generated successfully!\n\n" +
                                  $"Property: {selectedItem.Name}\n" +
                                  $"Report Size: {htmlContent.Length / 1024} KB",
                        "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to generate report. No data available.",
                        "Generation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error generating report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateHtmlPreviewInfo(string htmlContent)
        {
            try
            {
                // Extract basic info from HTML for preview
                int propertyCount = 0;
                int portionCount = 0;
                decimal totalRent = 0;

                // Simple parsing (you might want to improve this)
                if (htmlContent.Contains("Total Properties:"))
                {
                    string[] lines = htmlContent.Split('\n');
                    foreach (string line in lines)
                    {
                        if (line.Contains("Total Properties:"))
                        {
                            string countStr = new string(line.Where(char.IsDigit).ToArray());
                            int.TryParse(countStr, out propertyCount);
                        }
                        else if (line.Contains("Total Portions:"))
                        {
                            string countStr = new string(line.Where(char.IsDigit).ToArray());
                            int.TryParse(countStr, out portionCount);
                        }
                        else if (line.Contains("Total Monthly Rent:"))
                        {
                            string rentStr = new string(line.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
                            decimal.TryParse(rentStr, out totalRent);
                        }
                    }
                }

                var selectedItem = cmbPropertySelection.SelectedItem as PropertyComboBoxItem;
                string propertyName = selectedItem?.Name ?? "Unknown";

                lblHtmlPreview.Text = $"📋 Report Generated Successfully!\n\n" +
                                    $"• Property: {propertyName}\n" +
                                    $"• Generated: {DateTime.Now:HH:mm:ss}\n" +
                                    $"• Properties in Report: {propertyCount}\n" +
                                    $"• Portions in Report: {portionCount}\n" +
                                    $"• Total Monthly Rent: {totalRent:C}\n" +
                                    $"• Report Size: {htmlContent.Length / 1024} KB\n\n" +
                                    $"✅ Ready to print, save, or open in browser.";
            }
            catch
            {
                lblHtmlPreview.Text = $"📋 HTML Report Ready!\n\n" +
                                    $"Report generated successfully.\n" +
                                    $"Size: {htmlContent.Length / 1024} KB\n\n" +
                                    $"Use the buttons below to print, save, or view.";
            }
        }

        private void OpenInBrowser()
        {
            if (string.IsNullOrEmpty(_currentHtmlReport))
            {
                MessageBox.Show("Please generate a report first by clicking 'Generate Report'.",
                    "No Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                // Create a temporary HTML file
                string tempPath = Path.GetTempPath();
                string fileName = $"Property_Report_{DateTime.Now:yyyyMMdd_HHmmss}.html";
                string filePath = Path.Combine(tempPath, fileName);

                // Save HTML to file
                File.WriteAllText(filePath, _currentHtmlReport);

                // Open in default browser
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });

                Cursor.Current = Cursors.Default;

                // Show confirmation
                MessageBox.Show($"Report opened in your default browser.\n\n" +
                              $"File saved temporarily at:\n{filePath}",
                    "Report Opened", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Error opening report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowQuickPreview()
        {
            if (string.IsNullOrEmpty(_currentHtmlReport))
            {
                MessageBox.Show("Please generate a report first.",
                    "No Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string previewText = ExtractSummaryFromHtml(_currentHtmlReport);

                Form previewForm = new Form
                {
                    Text = "📋 Report Summary Preview",
                    Size = new Size(700, 500),
                    StartPosition = FormStartPosition.CenterParent,
                    BackColor = Color.White
                };

                RichTextBox txtPreview = new RichTextBox
                {
                    Multiline = true,
                    ReadOnly = true,
                    Dock = DockStyle.Fill,
                    Font = new Font("Consolas", 10),
                    BackColor = Color.FromArgb(248, 248, 248),
                    Text = previewText
                };

                Button btnClosePreview = new Button
                {
                    Text = "Close",
                    Size = new Size(100, 30),
                    Location = new Point(300, 430),
                    DialogResult = DialogResult.OK
                };

                previewForm.Controls.Add(txtPreview);
                previewForm.Controls.Add(btnClosePreview);
                previewForm.AcceptButton = btnClosePreview;

                previewForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing preview: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ExtractSummaryFromHtml(string html)
        {
            StringBuilder summary = new StringBuilder();

            // Remove HTML tags
            string text = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", "\n");

            // Extract key information
            string[] lines = text.Split('\n');
            int tableRows = 0;

            foreach (string line in lines)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed)) continue;

                // Add important sections
                if (trimmed.Contains("PROPERTY MANAGEMENT REPORT") ||
                    trimmed.Contains("Generated on:") ||
                    trimmed.Contains("Total Properties:") ||
                    trimmed.Contains("Total Portions:") ||
                    trimmed.Contains("Occupied Portions:") ||
                    trimmed.Contains("Vacant Portions:") ||
                    trimmed.Contains("Overall Occupancy Rate:") ||
                    trimmed.Contains("Total Monthly Rent:"))
                {
                    summary.AppendLine(trimmed);
                }

                // Show table header and first few rows
                if (trimmed.Contains("Property Name") ||
                    trimmed.Contains("Portion No") ||
                    trimmed.Contains("Floor") ||
                    trimmed.Contains("Area"))
                {
                    if (tableRows == 0)
                    {
                        summary.AppendLine("\n=== DETAILS ===");
                    }
                    summary.AppendLine(trimmed);
                    tableRows++;
                }
                else if (tableRows > 0 && tableRows < 15) // Show first 15 data rows
                {
                    summary.AppendLine(trimmed);
                    tableRows++;
                }
            }

            if (summary.Length == 0)
            {
                summary.AppendLine("=== PROPERTY REPORT SUMMARY ===\n");
                summary.AppendLine("Full report is available. Please open in browser for complete details.");
            }

            return summary.ToString();
        }

        private void SaveHtmlToFile()
        {
            if (string.IsNullOrEmpty(_currentHtmlReport))
            {
                MessageBox.Show("No report to save. Please generate a report first.",
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedItem = cmbPropertySelection.SelectedItem as PropertyComboBoxItem;
                string defaultName = selectedItem?.Id == 0 ?
                    "All_Properties_Report" :
                    $"Property_{selectedItem?.Id}_Report";

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "HTML Files|*.html|All Files|*.*",
                    FileName = $"{defaultName}_{DateTime.Now:yyyyMMdd_HHmmss}.html",
                    Title = "Save HTML Report",
                    DefaultExt = "html"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveDialog.FileName, _currentHtmlReport);

                    MessageBox.Show($"Report saved successfully to:\n{saveDialog.FileName}",
                        "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintReport()
        {
            if (string.IsNullOrEmpty(_currentHtmlReport))
            {
                MessageBox.Show("Please generate a report first.",
                    "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Create temporary HTML file
                string tempFile = Path.GetTempFileName() + ".html";
                File.WriteAllText(tempFile, _currentHtmlReport);

                // Print using default browser
                Process.Start(new ProcessStartInfo
                {
                    FileName = tempFile,
                    UseShellExecute = true,
                    Verb = "print"
                });

                MessageBox.Show("Print job sent to printer.",
                    "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            try
            {
                var filteredProperties = _allProperties.AsEnumerable();

                // Text search - ONLY if there's search text
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchTerm = txtSearch.Text.ToLower();
                    filteredProperties = filteredProperties.Where(p =>
                        (p.Name?.ToLower() ?? "").Contains(searchTerm) ||
                        (p.Address?.ToLower() ?? "").Contains(searchTerm));
                }

                // Property type filter - ONLY if NOT "All Types"
                if (cmbPropertyTypeFilter.SelectedItem != null &&
                    cmbPropertyTypeFilter.SelectedItem.ToString() != "All Types")
                {
                    var selectedType = cmbPropertyTypeFilter.SelectedItem.ToString() == "Commercial" ?
                        PropertyType.Commercial : PropertyType.NonCommercial;
                    filteredProperties = filteredProperties.Where(p => p.Type == selectedType);
                }

                // Occupied only filter
                if (chkShowOccupiedOnly.Checked)
                {
                    var activeAgreements = _allAgreements.Where(a => a.IsActive).ToList();
                    var occupiedPropertyIds = activeAgreements.Select(a => a.PropertyId).Distinct().ToList();
                    filteredProperties = filteredProperties.Where(p => occupiedPropertyIds.Contains(p.Id));
                }

                RefreshGridView(filteredProperties.ToList());
                UpdateSummary();
                UpdateDetailedStats(filteredProperties.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error applying filters: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshGridView(List<Property> properties = null)
        {
            if (properties == null)
            {
                properties = _allProperties;
            }

            dgvProperties.Rows.Clear();
            dgvProperties.Columns.Clear();

            // Define columns
            var columns = new[]
            {
                new DataGridViewTextBoxColumn { HeaderText = "ID", Width = 50 },
                new DataGridViewTextBoxColumn { HeaderText = "Property Name", Width = 150 },
                new DataGridViewTextBoxColumn { HeaderText = "Type", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Address", Width = 200 },
                new DataGridViewTextBoxColumn { HeaderText = "Total Portions", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Occupied", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Vacant", Width = 80 },
                new DataGridViewTextBoxColumn { HeaderText = "Occupancy Rate", Width = 100 },
                new DataGridViewTextBoxColumn { HeaderText = "Total Monthly Rent", Width = 120 },
                new DataGridViewTextBoxColumn { HeaderText = "Status", Width = 80 }
            };
            dgvProperties.Columns.AddRange(columns);

            // Add data
            foreach (var property in properties.OrderBy(p => p.Name))
            {
                var propertyPortions = _allPortions.Where(p => p.PropertyId == property.Id && p.IsActive).ToList();
                var occupiedPortions = _allAgreements.Where(a => a.PropertyId == property.Id && a.IsActive)
                                                    .Select(a => a.PortionId)
                                                    .Distinct()
                                                    .ToList();

                int totalPortions = propertyPortions.Count;
                int occupied = occupiedPortions.Count;
                int vacant = totalPortions - occupied;
                decimal occupancyRate = totalPortions > 0 ? (occupied * 100m / totalPortions) : 0;

                decimal totalRent = _allAgreements.Where(a => a.PropertyId == property.Id && a.IsActive)
                                                 .Sum(a => a.MonthlyRent);

                string status = occupancyRate >= 90 ? "Fully Occupied" :
                              occupancyRate >= 70 ? "Good" :
                              occupancyRate >= 50 ? "Moderate" : "Low";

                int rowIndex = dgvProperties.Rows.Add(
                    property.Id,
                    property.Name,
                    property.Type.ToString(),
                    property.Address ?? "",
                    totalPortions,
                    occupied,
                    vacant,
                    $"{occupancyRate:F1}%",
                    totalRent.ToString("C"),
                    status
                );

                // Color coding
                var row = dgvProperties.Rows[rowIndex];

                if (property.Type == PropertyType.Commercial)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 255);
                }
                try { 
                if (row.Cells["Status"].Displayed) { return; }
                if (occupancyRate >= 90)
                {
                    row.Cells["Status"].Style.ForeColor = Color.DarkGreen;
                    row.Cells["Status"].Style.Font = new Font(row.DefaultCellStyle.Font, FontStyle.Bold);
                }
                else if (occupancyRate >= 70)
                {
                    row.Cells["Status"].Style.ForeColor = Color.DarkBlue;
                }
                else if (occupancyRate < 50)
                {
                    row.Cells["Status"].Style.ForeColor = Color.DarkRed;
                    row.Cells["Status"].Style.Font = new Font(row.DefaultCellStyle.Font, FontStyle.Bold);
                }
                }
                catch(Exception e)
                {

                }
            }

            // Update column widths
            dgvProperties.AutoResizeColumns();

            // Show message if no properties
            if (dgvProperties.Rows.Count == 0)
            {
                dgvProperties.Columns.Clear();
                dgvProperties.Rows.Add("No properties found matching the criteria");
                dgvProperties.Rows[0].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                dgvProperties.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                dgvProperties.Rows[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void UpdateSummary()
        {
            try
            {
                int totalProperties = _allProperties.Count;
                int commercialProperties = _allProperties.Count(p => p.Type == PropertyType.Commercial);
                int nonCommercialProperties = _allProperties.Count(p => p.Type == PropertyType.NonCommercial);

                int totalPortions = _allPortions.Count(p => p.IsActive);
                int occupiedPortions = _allAgreements.Where(a => a.IsActive)
                                                    .Select(a => a.PortionId)
                                                    .Distinct()
                                                    .Count();
                decimal occupancyRate = totalPortions > 0 ? (occupiedPortions * 100m / totalPortions) : 0;

                lblSummary.Text = $"🏢 Total Properties: {totalProperties} | " +
                                $"🏪 Commercial: {commercialProperties} | " +
                                $"🏠 Non-Commercial: {nonCommercialProperties} | " +
                                $"📍 Total Portions: {totalPortions} | " +
                                $"✅ Occupied: {occupiedPortions} | " +
                                $"📊 Occupancy Rate: {occupancyRate:F1}%";
            }
            catch (Exception ex)
            {
                lblSummary.Text = "Error loading summary data";
            }
        }

        private void UpdateDetailedStats(List<Property> properties)
        {
            var lblStats = this.Controls.Find("lblDetailedStats", true).FirstOrDefault() as Label;
            if (lblStats != null)
            {
                int total = properties.Count;
                int occupied = _allAgreements.Where(a => a.IsActive)
                                           .Select(a => a.PropertyId)
                                           .Distinct()
                                           .Count(pId => properties.Any(p => p.Id == pId));
                int fullyOccupied = properties.Count(p =>
                {
                    var propertyPortions = _allPortions.Count(port => port.PropertyId == p.Id && port.IsActive);
                    var propertyAgreements = _allAgreements.Count(a => a.PropertyId == p.Id && a.IsActive);
                    return propertyPortions > 0 && propertyPortions == propertyAgreements;
                });

                lblStats.Text = $"Showing {total} properties | {occupied} occupied | {fullyOccupied} fully occupied";
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportToCsv();
        }

        private void ExportToCsv()
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV Files|*.csv|Excel Files|*.xlsx",
                    FileName = $"Property_Report_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                    Title = "Export Property Data"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var writer = new StreamWriter(saveDialog.FileName))
                    {
                        // Write headers
                        for (int i = 0; i < dgvProperties.Columns.Count; i++)
                        {
                            writer.Write(dgvProperties.Columns[i].HeaderText);
                            if (i < dgvProperties.Columns.Count - 1)
                                writer.Write(",");
                        }
                        writer.WriteLine();

                        // Write data
                        foreach (DataGridViewRow row in dgvProperties.Rows)
                        {
                            for (int i = 0; i < dgvProperties.Columns.Count; i++)
                            {
                                var value = row.Cells[i].Value?.ToString() ?? "";
                                if (value.Contains(",") || value.Contains("\""))
                                {
                                    value = "\"" + value.Replace("\"", "\"\"") + "\"";
                                }
                                writer.Write(value);
                                if (i < dgvProperties.Columns.Count - 1)
                                    writer.Write(",");
                            }
                            writer.WriteLine();
                        }
                    }

                    MessageBox.Show($"Data exported successfully to:\n{saveDialog.FileName}",
                        "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintReport();
        }
    }
}