using System;
using System.Windows.Forms;
using System.Linq;
using ShopRentManagementSystem.Models;
using ShopRentManagementSystem.Services;

namespace ShopRentManagementSystem
{
    public partial class FrmTenant : Form
    {
        private readonly JsonDataService _dataService;
        private DataGridView dgvTenants;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;

        public FrmTenant()
        {
            InitializeComponent();
            _dataService = new JsonDataService();
            LoadTenants();
        }

        private void InitializeComponent()
        {
            this.Text = "Tenant Management";
            this.Size = new System.Drawing.Size(900, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvTenants = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvTenants.Columns.Add("Id", "ID");
            dgvTenants.Columns.Add("Name", "Tenant Name");
            dgvTenants.Columns.Add("CNIC", "CNIC");
            dgvTenants.Columns.Add("Mobile", "Mobile");
            dgvTenants.Columns.Add("Type", "Tenant Type");
            dgvTenants.Columns.Add("Commission", "Commission %");
            dgvTenants.Columns.Add("SecurityDeposit", "Security Deposit");

            dgvTenants.Columns["Id"].Width = 50;
            dgvTenants.Columns["SecurityDeposit"].DefaultCellStyle.Format = "C";

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

            this.Controls.AddRange(new Control[] { dgvTenants, pnlButtons });
        }

        private void LoadTenants()
        {
            var tenants = _dataService.LoadTenants();
            dgvTenants.Rows.Clear();

            foreach (var tenant in tenants)
            {
                dgvTenants.Rows.Add(
                    tenant.Id,
                    tenant.Name,
                    tenant.CNIC,
                    tenant.Mobile,
                    tenant.Type.ToString(),
                    tenant.Type == TenantType.OnCommission ? $"{tenant.CommissionPercentage}%" : "N/A",
                    tenant.SecurityDeposit
                );
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Add New Tenant";
                dialog.Size = new System.Drawing.Size(450, 400);
                dialog.StartPosition = FormStartPosition.CenterParent;

                int yPos = 20;

                // Basic Information
                var lblName = new Label { Text = "Tenant Name:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtName = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25) };
                yPos += 30;

                var lblCNIC = new Label { Text = "CNIC:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtCNIC = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25) };
                yPos += 30;

                var lblMobile = new Label { Text = "Mobile:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtMobile = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25) };
                yPos += 30;

                var lblDeposit = new Label { Text = "Security Deposit:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var numDeposit = new NumericUpDown { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25), Minimum = 0, Maximum = 1000000, DecimalPlaces = 2 };
                yPos += 30;

                var lblStampDetails = new Label { Text = "Stamp Details:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtStampDetails = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25) };
                yPos += 30;

                var lblStampDate = new Label { Text = "Stamp Date:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var dtpStampDate = new DateTimePicker { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25), Value = DateTime.Now };
                yPos += 30;

                // Tenant Type
                var lblTenantType = new Label { Text = "Tenant Type:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var cmbTenantType = new ComboBox
                {
                    Location = new System.Drawing.Point(130, yPos),
                    Size = new System.Drawing.Size(200, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbTenantType.Items.AddRange(new[] { "On Rent", "On Commission" });
                cmbTenantType.SelectedIndex = 0;
                yPos += 30;

                // Commission Fields Panel
                Panel pnlCommission = new Panel
                {
                    Location = new System.Drawing.Point(20, yPos),
                    Size = new System.Drawing.Size(350, 80),
                    Visible = false,
                    BorderStyle = BorderStyle.FixedSingle
                };

                var lblCommission = new Label { Text = "Commission %:", Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(100, 25) };
                var numCommission = new NumericUpDown { Location = new System.Drawing.Point(120, 10), Size = new System.Drawing.Size(100, 25), Minimum = 0, Maximum = 100, DecimalPlaces = 2, Value = 15 };

                var lblFrequency = new Label { Text = "Payment Frequency:", Location = new System.Drawing.Point(10, 45), Size = new System.Drawing.Size(100, 25) };
                var cmbFrequency = new ComboBox
                {
                    Location = new System.Drawing.Point(120, 45),
                    Size = new System.Drawing.Size(150, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbFrequency.Items.AddRange(new[] { "Daily", "Every 5 Days", "Every 10 Days", "Weekly", "Monthly", "Custom" });
                cmbFrequency.SelectedIndex = 2; // Default to Every 10 Days

                pnlCommission.Controls.AddRange(new Control[] { lblCommission, numCommission, lblFrequency, cmbFrequency });

                // Show/hide commission fields based on tenant type
                cmbTenantType.SelectedIndexChanged += (s, args) =>
                {
                    pnlCommission.Visible = cmbTenantType.SelectedItem.ToString() == "On Commission";
                };

                yPos += 90;

                var btnSave = new Button { Text = "Save", Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(80, 30) };
                var btnCancel = new Button { Text = "Cancel", Location = new System.Drawing.Point(220, yPos), Size = new System.Drawing.Size(80, 30) };

                btnSave.Click += (s, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("Tenant name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var tenants = _dataService.LoadTenants();
                    var newId = tenants.Count > 0 ? tenants.Max(t => t.Id) + 1 : 1;

                    bool isCommission = cmbTenantType.SelectedItem.ToString() == "On Commission";

                    var tenant = new Tenant
                    {
                        Id = newId,
                        Name = txtName.Text,
                        CNIC = txtCNIC.Text,
                        Mobile = txtMobile.Text,
                        SecurityDeposit = numDeposit.Value,
                        StampPaperDetails = txtStampDetails.Text,
                        StampPaperDate = dtpStampDate.Value,
                        Type = isCommission ? TenantType.OnCommission : TenantType.OnRent,
                        CommissionPercentage = isCommission ? numCommission.Value : (decimal?)null,
                        CommissionFrequency = isCommission ?
                            (CommissionFrequency)Enum.Parse(typeof(CommissionFrequency), cmbFrequency.SelectedItem.ToString().Replace(" ", "")) :
                            (CommissionFrequency?)null
                    };

                    tenants.Add(tenant);
                    _dataService.SaveTenants(tenants);
                    LoadTenants();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.Controls.AddRange(new Control[] {
                    lblName, txtName, lblCNIC, txtCNIC, lblMobile, txtMobile,
                    lblDeposit, numDeposit, lblStampDetails, txtStampDetails,
                    lblStampDate, dtpStampDate, lblTenantType, cmbTenantType,
                    pnlCommission, btnSave, btnCancel
                });

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Tenant added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTenants.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a tenant to edit.", "Select Tenant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvTenants.SelectedRows[0];
            var tenantId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            var tenants = _dataService.LoadTenants();
            var tenant = tenants.FirstOrDefault(t => t.Id == tenantId);

            if (tenant == null) return;

            using (var dialog = new Form())
            {
                dialog.Text = "Edit Tenant";
                dialog.Size = new System.Drawing.Size(450, 400);
                dialog.StartPosition = FormStartPosition.CenterParent;

                int yPos = 20;

                var lblName = new Label { Text = "Tenant Name:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtName = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25), Text = tenant.Name };
                yPos += 30;

                var lblCNIC = new Label { Text = "CNIC:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtCNIC = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25), Text = tenant.CNIC };
                yPos += 30;

                var lblMobile = new Label { Text = "Mobile:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtMobile = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25), Text = tenant.Mobile };
                yPos += 30;

                var lblDeposit = new Label { Text = "Security Deposit:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var numDeposit = new NumericUpDown { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25), Minimum = 0, Maximum = 1000000, DecimalPlaces = 2, Value = tenant.SecurityDeposit };
                yPos += 30;

                var lblStampDetails = new Label { Text = "Stamp Details:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var txtStampDetails = new TextBox { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25), Text = tenant.StampPaperDetails };
                yPos += 30;

                var lblStampDate = new Label { Text = "Stamp Date:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var dtpStampDate = new DateTimePicker { Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(200, 25), Value = tenant.StampPaperDate };
                yPos += 30;

                var lblTenantType = new Label { Text = "Tenant Type:", Location = new System.Drawing.Point(20, yPos), Size = new System.Drawing.Size(100, 25) };
                var cmbTenantType = new ComboBox
                {
                    Location = new System.Drawing.Point(130, yPos),
                    Size = new System.Drawing.Size(200, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbTenantType.Items.AddRange(new[] { "On Rent", "On Commission" });
                cmbTenantType.SelectedItem = tenant.Type.ToString();
                yPos += 30;

                Panel pnlCommission = new Panel
                {
                    Location = new System.Drawing.Point(20, yPos),
                    Size = new System.Drawing.Size(350, 80),
                    Visible = tenant.Type == TenantType.OnCommission,
                    BorderStyle = BorderStyle.FixedSingle
                };

                var lblCommission = new Label { Text = "Commission %:", Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(100, 25) };
                var numCommission = new NumericUpDown { Location = new System.Drawing.Point(120, 10), Size = new System.Drawing.Size(100, 25), Minimum = 0, Maximum = 100, DecimalPlaces = 2, Value = tenant.CommissionPercentage ?? 15 };

                var lblFrequency = new Label { Text = "Payment Frequency:", Location = new System.Drawing.Point(10, 45), Size = new System.Drawing.Size(100, 25) };
                var cmbFrequency = new ComboBox
                {
                    Location = new System.Drawing.Point(120, 45),
                    Size = new System.Drawing.Size(150, 25),
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbFrequency.Items.AddRange(new[] { "Daily", "Every 5 Days", "Every 10 Days", "Weekly", "Monthly", "Custom" });
                if (tenant.CommissionFrequency.HasValue)
                    cmbFrequency.SelectedItem = tenant.CommissionFrequency.Value.ToString().InsertSpaceBeforeCapitals();
                else
                    cmbFrequency.SelectedIndex = 2;

                pnlCommission.Controls.AddRange(new Control[] { lblCommission, numCommission, lblFrequency, cmbFrequency });

                cmbTenantType.SelectedIndexChanged += (s, args) =>
                {
                    pnlCommission.Visible = cmbTenantType.SelectedItem.ToString() == "On Commission";
                };

                yPos += 90;

                var btnSave = new Button { Text = "Update", Location = new System.Drawing.Point(130, yPos), Size = new System.Drawing.Size(80, 30) };
                var btnCancel = new Button { Text = "Cancel", Location = new System.Drawing.Point(220, yPos), Size = new System.Drawing.Size(80, 30) };

                btnSave.Click += (s, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtName.Text))
                    {
                        MessageBox.Show("Tenant name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    tenant.Name = txtName.Text;
                    tenant.CNIC = txtCNIC.Text;
                    tenant.Mobile = txtMobile.Text;
                    tenant.SecurityDeposit = numDeposit.Value;
                    tenant.StampPaperDetails = txtStampDetails.Text;
                    tenant.StampPaperDate = dtpStampDate.Value;
                    tenant.Type = cmbTenantType.SelectedItem.ToString() == "On Commission" ? TenantType.OnCommission : TenantType.OnRent;

                    if (tenant.Type == TenantType.OnCommission)
                    {
                        tenant.CommissionPercentage = numCommission.Value;
                        tenant.CommissionFrequency = (CommissionFrequency)Enum.Parse(typeof(CommissionFrequency), cmbFrequency.SelectedItem.ToString().Replace(" ", ""));
                    }
                    else
                    {
                        tenant.CommissionPercentage = null;
                        tenant.CommissionFrequency = null;
                    }

                    _dataService.SaveTenants(tenants);
                    LoadTenants();
                    dialog.DialogResult = DialogResult.OK;
                };

                btnCancel.Click += (s, args) => dialog.DialogResult = DialogResult.Cancel;

                dialog.Controls.AddRange(new Control[] {
                    lblName, txtName, lblCNIC, txtCNIC, lblMobile, txtMobile,
                    lblDeposit, numDeposit, lblStampDetails, txtStampDetails,
                    lblStampDate, dtpStampDate, lblTenantType, cmbTenantType,
                    pnlCommission, btnSave, btnCancel
                });

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Tenant updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTenants.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a tenant to delete.", "Select Tenant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvTenants.SelectedRows[0];
            var tenantId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            var tenantName = selectedRow.Cells["Name"].Value.ToString();

            var result = MessageBox.Show($"Are you sure you want to delete tenant '{tenantName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var tenants = _dataService.LoadTenants();
                tenants.RemoveAll(t => t.Id == tenantId);
                _dataService.SaveTenants(tenants);
                LoadTenants();

                MessageBox.Show("Tenant deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

// Extension method for string formatting
public static class StringExtensions
{
    public static string InsertSpaceBeforeCapitals(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        var result = new System.Text.StringBuilder();
        result.Append(str[0]);

        for (int i = 1; i < str.Length; i++)
        {
            if (char.IsUpper(str[i]))
                result.Append(' ');
            result.Append(str[i]);
        }

        return result.ToString();
    }
}