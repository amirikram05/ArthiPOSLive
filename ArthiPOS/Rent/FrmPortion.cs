using System;
using System.Windows.Forms;
using System.Linq;
using ShopRentManagementSystem.Services;
using ShopRentManagementSystem.Models;

namespace ShopRentManagementSystem
{
    public partial class FrmPortion : Form
    {
        private readonly JsonDataService _dataService;
        private DataGridView dgvPortions;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private ComboBox cmbProperties;

        public FrmPortion()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadProperties();
            LoadPortions();
        }

        private void InitializeComponent()
        {
            this.Text = "Portion Management";
            this.Size = new System.Drawing.Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Filter Panel
            Panel pnlFilter = new Panel { Height = 50, Dock = DockStyle.Top, BackColor = System.Drawing.Color.LightGray };

            Label lblProperty = new Label
            {
                Text = "Filter by Property:",
                Location = new System.Drawing.Point(20, 15),
                Size = new System.Drawing.Size(100, 25)
            };

            cmbProperties = new ComboBox
            {
                Location = new System.Drawing.Point(130, 15),
                Size = new System.Drawing.Size(200, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbProperties.SelectedIndexChanged += CmbProperties_SelectedIndexChanged;

            pnlFilter.Controls.AddRange(new Control[] { lblProperty, cmbProperties });

            // Data Grid View
            dgvPortions = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Setup columns
            dgvPortions.Columns.Add("Id", "ID");
            dgvPortions.Columns.Add("PropertyName", "Property");
            dgvPortions.Columns.Add("Name", "Portion Name");
            dgvPortions.Columns.Add("Size", "Size");
            dgvPortions.Columns.Add("IsActive", "Active");

            dgvPortions.Columns["Id"].Width = 50;
            dgvPortions.Columns["IsActive"].Width = 60;

            // Buttons Panel
            Panel pnlButtons = new Panel { Height = 50, Dock = DockStyle.Bottom };

            btnAdd = new Button
            {
                Text = "Add New",
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(80, 30)
            };
            btnAdd.Click += BtnAdd_Click;

            btnEdit = new Button
            {
                Text = "Edit",
                Location = new System.Drawing.Point(100, 10),
                Size = new System.Drawing.Size(80, 30)
            };
            btnEdit.Click += BtnEdit_Click;

            btnDelete = new Button
            {
                Text = "Delete",
                Location = new System.Drawing.Point(190, 10),
                Size = new System.Drawing.Size(80, 30),
                BackColor = System.Drawing.Color.LightPink
            };
            btnDelete.Click += BtnDelete_Click;

            pnlButtons.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete });

            this.Controls.AddRange(new Control[] { dgvPortions, pnlFilter, pnlButtons });
        }

        private void LoadProperties()
        {
            var properties = _dataService.LoadProperties();
            cmbProperties.Items.Clear();
            cmbProperties.Items.Add("All Properties");

            foreach (var property in properties)
            {
                cmbProperties.Items.Add(new { Id = property.Id, Name = property.Name });
            }

            if (cmbProperties.Items.Count > 0)
                cmbProperties.SelectedIndex = 0;
        }

        private void LoadPortions()
        {
            var portions = _dataService.LoadPortions();
            var properties = _dataService.LoadProperties();

            dgvPortions.Rows.Clear();

            foreach (var portion in portions)
            {
                var property = properties.FirstOrDefault(p => p.Id == portion.PropertyId);
                dgvPortions.Rows.Add(
                    portion.Id,
                    property?.Name ?? "N/A",
                    portion.Name,
                    portion.Size,
                    portion.IsActive ? "Yes" : "No"
                );
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Add New Portion";
                dialog.Size = new System.Drawing.Size(400, 250);
                dialog.StartPosition = FormStartPosition.CenterParent;

                var properties = _dataService.LoadProperties();

                var lblProperty = new Label { Text = "Property:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(100, 25) };
                var cmbProperty = new ComboBox
                {
                    Location = new System.Drawing.Point(130, 20),
                    Size = new System.Drawing.Size(200, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                foreach (var property in properties)
                {
                    cmbProperty.Items.Add(new { Id = property.Id, Name = property.Name });
                }

                if (cmbProperty.Items.Count > 0)
                    cmbProperty.SelectedIndex = 0;

                var lblName = new Label { Text = "Portion Name:", Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(100, 25) };
                var txtName = new TextBox { Location = new System.Drawing.Point(130, 60), Size = new System.Drawing.Size(200, 25) };

                var lblSize = new Label { Text = "Size (e.g., 5x10):", Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(100, 25) };
                var txtSize = new TextBox { Location = new System.Drawing.Point(130, 100), Size = new System.Drawing.Size(200, 25) };

                var btnSave = new Button { Text = "Save", Location = new System.Drawing.Point(130, 150), Size = new System.Drawing.Size(80, 30) };
                var btnCancel = new Button { Text = "Cancel", Location = new System.Drawing.Point(220, 150), Size = new System.Drawing.Size(80, 30) };

                btnSave.Click += (s, args) =>
                {
                    if (cmbProperty.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a property.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("Portion name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtSize.Text))
                    {
                        MessageBox.Show("Portion size is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var portions = _dataService.LoadPortions();
                    var newId = portions.Count > 0 ? portions.Max(p => p.Id) + 1 : 1;
                    var selectedProperty = (dynamic)cmbProperty.SelectedItem;

                    portions.Add(new Portion
                    {
                        Id = newId,
                        PropertyId = selectedProperty.Id,
                        Name = txtName.Text,
                        Size = txtSize.Text,
                        IsActive = true
                    });

                    _dataService.SavePortions(portions);
                    LoadPortions();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.Controls.AddRange(new Control[] { lblProperty, cmbProperty, lblName, txtName, lblSize, txtSize, btnSave, btnCancel });

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Portion added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPortions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a portion to edit.", "Select Portion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvPortions.SelectedRows[0];
            var portionId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            var portions = _dataService.LoadPortions();
            var portion = portions.FirstOrDefault(p => p.Id == portionId);

            if (portion == null) return;

            using (var dialog = new Form())
            {
                dialog.Text = "Edit Portion";
                dialog.Size = new System.Drawing.Size(400, 250);
                dialog.StartPosition = FormStartPosition.CenterParent;

                var properties = _dataService.LoadProperties();

                var lblProperty = new Label { Text = "Property:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(100, 25) };
                var cmbProperty = new ComboBox
                {
                    Location = new System.Drawing.Point(130, 20),
                    Size = new System.Drawing.Size(200, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                foreach (var property in properties)
                {
                    cmbProperty.Items.Add(new { Id = property.Id, Name = property.Name });
                }

                // Select current property
                for (int i = 0; i < cmbProperty.Items.Count; i++)
                {
                    dynamic item = cmbProperty.Items[i];
                    if (item.Id == portion.PropertyId)
                    {
                        cmbProperty.SelectedIndex = i;
                        break;
                    }
                }

                var lblName = new Label { Text = "Portion Name:", Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(100, 25) };
                var txtName = new TextBox { Location = new System.Drawing.Point(130, 60), Size = new System.Drawing.Size(200, 25), Text = portion.Name };

                var lblSize = new Label { Text = "Size (e.g., 5x10):", Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(100, 25) };
                var txtSize = new TextBox { Location = new System.Drawing.Point(130, 100), Size = new System.Drawing.Size(200, 25), Text = portion.Size };

                var chkActive = new CheckBox { Text = "Active", Location = new System.Drawing.Point(130, 130), Size = new System.Drawing.Size(100, 25), Checked = portion.IsActive };

                var btnSave = new Button { Text = "Update", Location = new System.Drawing.Point(130, 170), Size = new System.Drawing.Size(80, 30) };
                var btnCancel = new Button { Text = "Cancel", Location = new System.Drawing.Point(220, 170), Size = new System.Drawing.Size(80, 30) };

                btnSave.Click += (s, args) =>
                {
                    if (cmbProperty.SelectedItem == null)
                    {
                        MessageBox.Show("Please select a property.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("Portion name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtSize.Text))
                    {
                        MessageBox.Show("Portion size is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var selectedProperty = (dynamic)cmbProperty.SelectedItem;

                    portion.PropertyId = selectedProperty.Id;
                    portion.Name = txtName.Text;
                    portion.Size = txtSize.Text;
                    portion.IsActive = chkActive.Checked;

                    _dataService.SavePortions(portions);
                    LoadPortions();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.Controls.AddRange(new Control[] { lblProperty, cmbProperty, lblName, txtName, lblSize, txtSize, chkActive, btnSave, btnCancel });

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Portion updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPortions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a portion to delete.", "Select Portion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvPortions.SelectedRows[0];
            var portionId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            var portionName = selectedRow.Cells["Name"].Value.ToString();

            var result = MessageBox.Show($"Are you sure you want to delete portion '{portionName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var portions = _dataService.LoadPortions();
                portions.RemoveAll(p => p.Id == portionId);
                _dataService.SavePortions(portions);
                LoadPortions();

                MessageBox.Show("Portion deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CmbProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProperties.SelectedIndex == 0) // "All Properties"
            {
                LoadPortions();
            }
            else if (cmbProperties.SelectedItem != null)
            {
                var selectedProperty = (dynamic)cmbProperties.SelectedItem;
                FilterPortionsByProperty(selectedProperty.Id);
            }
        }

        private void FilterPortionsByProperty(int propertyId)
        {
            var portions = _dataService.LoadPortions();
            var properties = _dataService.LoadProperties();

            var filteredPortions = portions.Where(p => p.PropertyId == propertyId).ToList();

            dgvPortions.Rows.Clear();

            foreach (var portion in filteredPortions)
            {
                var property = properties.FirstOrDefault(p => p.Id == portion.PropertyId);
                dgvPortions.Rows.Add(
                    portion.Id,
                    property?.Name ?? "N/A",
                    portion.Name,
                    portion.Size,
                    portion.IsActive ? "Yes" : "No"
                );
            }
        }
    }
}