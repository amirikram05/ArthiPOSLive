using System;
using System.Windows.Forms;
using System.Linq;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmProperty : Form
    {
        private readonly JsonDataService _dataService;
        private DataGridView dgvProperties;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;

        public FrmProperty()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadProperties();
        }

        private void InitializeComponent()
        {
            this.Text = "Property Management";
            this.Size = new System.Drawing.Size(700, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvProperties = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvProperties.Columns.Add("Id", "ID");
            dgvProperties.Columns.Add("Name", "Property Name");
            dgvProperties.Columns.Add("Address", "Address");
            dgvProperties.Columns.Add("Type", "Property Type");
            dgvProperties.Columns.Add("CreatedDate", "Created Date");

            dgvProperties.Columns["Id"].Width = 50;
            dgvProperties.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

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

            this.Controls.AddRange(new Control[] { dgvProperties, pnlButtons });
        }

        private void LoadProperties()
        {
            var properties = _dataService.LoadProperties();
            dgvProperties.Rows.Clear();

            foreach (var property in properties)
            {
                dgvProperties.Rows.Add(
                    property.Id,
                    property.Name,
                    property.Address,
                    property.Type.ToString(),
                    property.CreatedDate
                );
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Add New Property";
                dialog.Size = new System.Drawing.Size(400, 250);
                dialog.StartPosition = FormStartPosition.CenterParent;

                int yPos = 20;

                var lblName = new Label { Text = "Property Name:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtName = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25) };
                yPos += 30;

                var lblAddress = new Label { Text = "Address:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtAddress = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25) };
                yPos += 30;

                var lblType = new Label { Text = "Property Type:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var cmbType = new ComboBox
                {
                    Location = new System.Drawing.Point(130, yPos),
                    Size = new System.Drawing.Size(200, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbType.Items.AddRange(new[] { "Commercial", "Non-Commercial" });
                cmbType.SelectedIndex = 0;
                yPos += 40;

                var btnSave = new Button { Text = "Save", Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(80, 30) };
                var btnCancel = new Button { Text = "Cancel", Location = new System.Drawing.Point(220, yPos), Size = new System.Drawing.Size(80, 30) };

                btnSave.Click += (s, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("Property name is required.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var properties = _dataService.LoadProperties();
                    var newId = properties.Count > 0 ? properties.Max(p => p.Id) + 1 : 1;

                    properties.Add(new Property
                    {
                        Id = newId,
                        Name = txtName.Text,
                        Address = txtAddress.Text,
                        Type = cmbType.SelectedItem.ToString() == "Commercial" ? PropertyType.Commercial : PropertyType.NonCommercial,
                        CreatedDate = DateTime.Now
                    });

                    _dataService.SaveProperties(properties);
                    LoadProperties();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.Controls.AddRange(new Control[] { lblName, txtName, lblAddress, txtAddress, lblType, cmbType, btnSave, btnCancel });

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Property added successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProperties.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a property to edit.", "Select Property",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvProperties.SelectedRows[0];
            var propertyId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            var properties = _dataService.LoadProperties();
            var property = properties.FirstOrDefault(p => p.Id == propertyId);

            if (property == null) return;

            using (var dialog = new Form())
            {
                dialog.Text = "Edit Property";
                dialog.Size = new System.Drawing.Size(400, 250);
                dialog.StartPosition = FormStartPosition.CenterParent;

                int yPos = 20;

                var lblName = new Label { Text = "Property Name:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtName = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25), Text = property.Name };
                yPos += 30;

                var lblAddress = new Label { Text = "Address:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtAddress = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25), Text = property.Address };
                yPos += 30;

                var lblType = new Label { Text = "Property Type:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var cmbType = new ComboBox
                {
                    Location = new System.Drawing.Point(130, yPos),
                    Size = new System.Drawing.Size(200, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbType.Items.AddRange(new[] { "Commercial", "Non-Commercial" });
                cmbType.SelectedItem = property.Type.ToString();
                yPos += 40;

                var btnSave = new Button { Text = "Update", Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(80, 30) };
                var btnCancel = new Button { Text = "Cancel", Location = new System.Drawing.Point(220, yPos), Size = new System.Drawing.Size(80, 30) };

                btnSave.Click += (s, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("Property name is required.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    property.Name = txtName.Text;
                    property.Address = txtAddress.Text;
                    property.Type = cmbType.SelectedItem.ToString() == "Commercial" ? PropertyType.Commercial : PropertyType.NonCommercial;

                    _dataService.SaveProperties(properties);
                    LoadProperties();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.Controls.AddRange(new Control[] { lblName, txtName, lblAddress, txtAddress, lblType, cmbType, btnSave, btnCancel });

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Property updated successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProperties.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a property to delete.", "Select Property",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvProperties.SelectedRows[0];
            var propertyId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            var propertyName = selectedRow.Cells["Name"].Value.ToString();

            var result = MessageBox.Show($"Are you sure you want to delete property '{propertyName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var properties = _dataService.LoadProperties();
                properties.RemoveAll(p => p.Id == propertyId);
                _dataService.SaveProperties(properties);
                LoadProperties();

                MessageBox.Show("Property deleted successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}