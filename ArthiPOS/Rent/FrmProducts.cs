using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ArthiPOS.Controls;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmProducts : Form
    {
        private readonly JsonDataService _dataService;
        private DataGridView dgvProducts;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private TextBox txtSearch;
        private ComboBox cmbFilter;

        public FrmProducts()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadProducts();
        }

        private void InitializeComponent()
        {
            this.Text = "Product Management";
            this.Size = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = SystemColors.Control;

            // Search Panel
            Panel pnlSearch = new Panel
            {
                Height = 50,
                Dock = DockStyle.Top,
                BackColor = Color.LightGray,
                Padding = new Padding(10)
            };

            txtSearch = new UrduTextBox
            {
                WaterMarkText = "Search products...",
                Location = new Point(10, 12),
                Size = new Size(200, 25)
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;

            cmbFilter = new ComboBox
            {
                Location = new Point(220, 12),
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFilter.Items.AddRange(new[] { "All", "Active Only", "Inactive Only" });
            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += CmbFilter_SelectedIndexChanged;

            pnlSearch.Controls.AddRange(new Control[] { txtSearch, cmbFilter });

            // Data Grid View
            dgvProducts = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BackgroundColor = SystemColors.Window
            };

            SetupDataGridView();

            // Buttons Panel
            Panel pnlButtons = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.LightGray,
                Padding = new Padding(10)
            };

            btnAdd = new Button
            {
                Text = "➕ Add New Product",
                Location = new Point(10, 15),
                Size = new Size(140, 30),
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnAdd.Click += BtnAdd_Click;

            btnEdit = new Button
            {
                Text = "✏️ Edit Product",
                Location = new Point(160, 15),
                Size = new Size(120, 30)
            };
            btnEdit.Click += BtnEdit_Click;

            btnDelete = new Button
            {
                Text = "🗑️ Delete",
                Location = new Point(290, 15),
                Size = new Size(100, 30),
                BackColor = Color.LightCoral
            };
            btnDelete.Click += BtnDelete_Click;

            btnRefresh = new Button
            {
                Text = "🔄 Refresh",
                Location = new Point(400, 15),
                Size = new Size(100, 30)
            };
            btnRefresh.Click += BtnRefresh_Click;

            pnlButtons.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete, btnRefresh });

            this.Controls.AddRange(new Control[] { dgvProducts, pnlSearch, pnlButtons });
        }

        private void SetupDataGridView()
        {
            dgvProducts.Columns.Clear();

            var columns = new[]
            {
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", Width = 50 },
                new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Product Name", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "Unit", HeaderText = "Unit", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "UnitPrice", HeaderText = "Unit Price", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "IsActive", HeaderText = "Active", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "CreatedDate", HeaderText = "Created Date", Width = 120 }
            };

            dgvProducts.Columns.AddRange(columns);

            // Format columns
            dgvProducts.Columns["UnitPrice"].DefaultCellStyle.Format = "C";
            dgvProducts.Columns["UnitPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProducts.Columns["CreatedDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

            // Style headers
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Cell formatting
            dgvProducts.CellFormatting += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    var row = dgvProducts.Rows[e.RowIndex];

                    // Color active status
                    if (e.ColumnIndex == dgvProducts.Columns["IsActive"].Index && e.Value != null)
                    {
                        if (e.Value.ToString() == "Yes")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkGreen;
                        }
                        else
                        {
                            e.CellStyle.BackColor = Color.LightPink;
                            e.CellStyle.ForeColor = Color.DarkRed;
                        }
                    }

                    // Alternate row colors
                    if (e.RowIndex % 2 == 0)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(250, 250, 250);
                    }
                }
            };
        }

        private void LoadProducts()
        {
            var products = _dataService.LoadProducts();
            dgvProducts.Rows.Clear();

            foreach (var product in products.OrderBy(p => p.Name))
            {
                dgvProducts.Rows.Add(
                    product.Id,
                    product.Name,
                    product.Unit,
                    product.UnitPrice,
                    product.IsActive ? "Yes" : "No",
                    product.CreatedDate
                );
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Add New Product";
                dialog.Size = new Size(450, 300);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.BackColor = SystemColors.Control;

                int yPos = 20;
                int labelWidth = 120;
                int controlWidth = 250;

                // Product Name
                var lblName = new Label
                {
                    Text = "Product Name:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                var txtName = new TextBox
                {
                    Location = new Point(150, yPos),
                    Size = new Size(controlWidth, 25),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                yPos += 35;

                // Unit
                var lblUnit = new Label
                {
                    Text = "Unit:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                var txtUnit = new TextBox
                {
                    Location = new Point(150, yPos),
                    Size = new Size(controlWidth, 25),
                    Text = "kg",
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                yPos += 35;

                // Unit Price
                var lblUnitPrice = new Label
                {
                    Text = "Unit Price:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                var numUnitPrice = new NumericUpDown
                {
                    Location = new Point(150, yPos),
                    Size = new Size(controlWidth, 25),
                    Minimum = 0,
                    Maximum = 1000000,
                    DecimalPlaces = 2,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                yPos += 35;

                // Active Status
                var chkActive = new CheckBox
                {
                    Text = "Active Product",
                    Location = new Point(150, yPos),
                    Size = new Size(200, 25),
                    Checked = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                yPos += 45;

                // Buttons
                var btnSave = new Button
                {
                    Text = "💾 Save Product",
                    Location = new Point(150, yPos),
                    Size = new Size(120, 35),
                    BackColor = Color.LightGreen,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                var btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(280, yPos),
                    Size = new Size(100, 35),
                    DialogResult = DialogResult.Cancel
                };

                btnSave.Click += (s, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("Product name is required.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtName.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtUnit.Text))
                    {
                        MessageBox.Show("Unit is required (e.g., kg, piece, box).", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUnit.Focus();
                        return;
                    }

                    if (numUnitPrice.Value <= 0)
                    {
                        MessageBox.Show("Please enter a valid unit price.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        numUnitPrice.Focus();
                        return;
                    }

                    var products = _dataService.LoadProducts();

                    // Check for duplicate product name
                    if (products.Any(p => p.Name.Equals(txtName.Text, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show($"A product named '{txtName.Text}' already exists.", "Duplicate Product",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var newId = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;

                    products.Add(new Product
                    {
                        Id = newId,
                        Name = txtName.Text.Trim(),
                        Unit = txtUnit.Text.Trim(),
                        UnitPrice = numUnitPrice.Value,
                        IsActive = chkActive.Checked,
                        CreatedDate = DateTime.Now
                    });

                    _dataService.SaveProducts(products);
                    LoadProducts();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.AcceptButton = btnSave;
                dialog.CancelButton = btnCancel;

                dialog.Controls.AddRange(new Control[] {
                    lblName, txtName, lblUnit, txtUnit, lblUnitPrice, numUnitPrice,
                    chkActive, btnSave, btnCancel
                });

                // Set focus to name field
                dialog.Shown += (s, args) => txtName.Focus();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Product added successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to edit.", "Select Product",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvProducts.SelectedRows[0];
            var productId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            var products = _dataService.LoadProducts();
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (product == null) return;

            using (var dialog = new Form())
            {
                dialog.Text = "Edit Product";
                dialog.Size = new Size(450, 300);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.BackColor = SystemColors.Control;

                int yPos = 20;
                int labelWidth = 120;
                int controlWidth = 250;

                // Product Name
                var lblName = new Label
                {
                    Text = "Product Name:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                var txtName = new TextBox
                {
                    Location = new Point(150, yPos),
                    Size = new Size(controlWidth, 25),
                    Text = product.Name,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                yPos += 35;

                // Unit
                var lblUnit = new Label
                {
                    Text = "Unit:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                var txtUnit = new TextBox
                {
                    Location = new Point(150, yPos),
                    Size = new Size(controlWidth, 25),
                    Text = product.Unit,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                yPos += 35;

                // Unit Price
                var lblUnitPrice = new Label
                {
                    Text = "Unit Price:",
                    Location = new Point(20, yPos),
                    Size = new Size(labelWidth, 25),
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                var numUnitPrice = new NumericUpDown
                {
                    Location = new Point(150, yPos),
                    Size = new Size(controlWidth, 25),
                    Minimum = 0,
                    Maximum = 1000000,
                    DecimalPlaces = 2,
                    Value = product.UnitPrice,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                yPos += 35;

                // Active Status
                var chkActive = new CheckBox
                {
                    Text = "Active Product",
                    Location = new Point(150, yPos),
                    Size = new Size(200, 25),
                    Checked = product.IsActive,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular)
                };
                yPos += 45;

                // Buttons
                var btnSave = new Button
                {
                    Text = "💾 Update Product",
                    Location = new Point(150, yPos),
                    Size = new Size(130, 35),
                    BackColor = Color.LightBlue,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                var btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new Point(290, yPos),
                    Size = new Size(100, 35),
                    DialogResult = DialogResult.Cancel
                };

                btnSave.Click += (s, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("Product name is required.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtName.Focus();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtUnit.Text))
                    {
                        MessageBox.Show("Unit is required (e.g., kg, piece, box).", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUnit.Focus();
                        return;
                    }

                    if (numUnitPrice.Value <= 0)
                    {
                        MessageBox.Show("Please enter a valid unit price.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        numUnitPrice.Focus();
                        return;
                    }

                    // Check for duplicate product name (excluding current product)
                    var duplicate = products.FirstOrDefault(p =>
                        p.Id != productId &&
                        p.Name.Equals(txtName.Text, StringComparison.OrdinalIgnoreCase));

                    if (duplicate != null)
                    {
                        MessageBox.Show($"A product named '{txtName.Text}' already exists.", "Duplicate Product",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    product.Name = txtName.Text.Trim();
                    product.Unit = txtUnit.Text.Trim();
                    product.UnitPrice = numUnitPrice.Value;
                    product.IsActive = chkActive.Checked;

                    _dataService.SaveProducts(products);
                    LoadProducts();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.AcceptButton = btnSave;
                dialog.CancelButton = btnCancel;

                dialog.Controls.AddRange(new Control[] {
                    lblName, txtName, lblUnit, txtUnit, lblUnitPrice, numUnitPrice,
                    chkActive, btnSave, btnCancel
                });

                // Set focus to name field
                dialog.Shown += (s, args) => txtName.Focus();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Product updated successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete.", "Select Product",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvProducts.SelectedRows[0];
            var productId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            var productName = selectedRow.Cells["Name"].Value.ToString();

            // Check if product is used in any agreements
            var agreements = _dataService.LoadAgreements();
            bool isUsed = agreements.Any(a => a.ProductIds != null && a.ProductIds.Contains(productId));

            if (isUsed)
            {
                MessageBox.Show($"Cannot delete '{productName}' because it is assigned to one or more commission agreements.\n\nPlease remove it from agreements first or mark it as inactive.",
                    "Product In Use", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete product '{productName}'?\n\nThis action cannot be undone!",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                var products = _dataService.LoadProducts();
                products.RemoveAll(p => p.Id == productId);
                _dataService.SaveProducts(products);
                LoadProducts();

                MessageBox.Show("Product deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
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
            var products = _dataService.LoadProducts();
            var filtered = products.AsEnumerable();

            // Text search
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                var searchTerm = txtSearch.Text.ToLower();
                filtered = filtered.Where(p =>
                    (p.Name?.ToLower() ?? "").Contains(searchTerm) ||
                    (p.Unit?.ToLower() ?? "").Contains(searchTerm));
            }

            // Status filter
            string filter = cmbFilter.SelectedItem?.ToString() ?? "All";
            switch (filter)
            {
                case "Active Only":
                    filtered = filtered.Where(p => p.IsActive);
                    break;
                case "Inactive Only":
                    filtered = filtered.Where(p => !p.IsActive);
                    break;
            }

            dgvProducts.Rows.Clear();
            foreach (var product in filtered.OrderBy(p => p.Name))
            {
                dgvProducts.Rows.Add(
                    product.Id,
                    product.Name,
                    product.Unit,
                    product.UnitPrice,
                    product.IsActive ? "Yes" : "No",
                    product.CreatedDate
                );
            }
        }
    }
}