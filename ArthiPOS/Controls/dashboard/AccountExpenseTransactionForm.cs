using ArthiPOS.Controls.dashboard;
using BAL;
using DataMember;
using DevExpress.XtraCharts.Designer.Native;
using DevExpress.XtraEditors;
using MaterialDesignColors;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static DevExpress.XtraPrinting.Native.PageSizeInfo;
namespace ArthiPOS.Controls.dashboard
{
    public partial class AccountExpenseTransactionForm : Form
    {
        private readonly BindingList<AccountExpenseTransaction> _items =
            new BindingList<AccountExpenseTransaction>();

        private readonly BindingList<AccountExpenseTransaction> _view =
            new BindingList<AccountExpenseTransaction>();
        private readonly BindingSource _bindingSource = new BindingSource();

        public AccountExpenseTransactionForm()
        {
            InitializeComponent();

            dgvTransactions.AutoGenerateColumns = false;

            // Bind grid to filtered view
            _bindingSource.DataSource = _view;
            dgvTransactions.DataSource = _bindingSource;

            InitializeSearchCombo();

            // Load your sample data (supports Unicode)
            Display();

            // Initial view
            ApplyFilter();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
                case Keys.Up:
                        //selectUpRow(dgvTransactions);
                    return true;
                case Keys.Down:
                        //selectDownRow(dgvTransactions);
                    return true;
                case Keys.F1:
                    //Stuff
                    return true;
                case Keys.Shift | Keys.Enter:
                    return true;
                case Keys.Alt | Keys.Down:
                    //Stuff
                    return true;
                case Keys.Enter:

                    if(txtExpenseName.ContainsFocus)
                    {
                        searchDialog(103, txtSearch.Text);

                    }

                    return true;
                case Keys.Control | Keys.Enter:

                    return true;
                case Keys.Alt | Keys.Enter:
                    return true;


            }



            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void searchDialog(int action, string searchTxt)
        {
            try 
            { 

                Search search = new Search(action, searchTxt == "Search" ? "" : searchTxt);

                using (search)
                {

                    DialogResult res = search.ShowDialog();
                
                    search.Close();



                    return;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void selectUpRow(DataGridView grid)
        {
            DataGridView dgv = grid;
            int totalRows = dgv.Rows.Count;

            int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
            if (rowIndex == 0)
                return;
            int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
            DataGridViewRow selectedRow = dgv.Rows[rowIndex];
            dgv.ClearSelection();
            dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
            grid.FirstDisplayedScrollingRowIndex = rowIndex - 1;
            currentrow--;
            if (currentrow < 0)
            {
                currentrow = 0;
            }
            grid.Rows[currentrow].Selected = true;

        }

        int currentrow = 0;

        private void selectDownRow(DataGridView grid)
        {
            DataGridView dgv = grid;
            int totalRows = dgv.Rows.Count;

            int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
            if (rowIndex == totalRows - 1)
                return;
            int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
            DataGridViewRow selectedRow = dgv.Rows[rowIndex];
            dgv.ClearSelection();
            dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
            grid.FirstDisplayedScrollingRowIndex = rowIndex + 1;
            currentrow++;
            if (currentrow > totalRows)
            {
                currentrow = totalRows;
            }
            grid.Rows[currentrow].Selected = true;
        }

        private void InitializeSearchCombo()
        {
            cmbSearchBy.Items.Clear();
            cmbSearchBy.Items.AddRange(new object[]
            {
                "All",
                "ExpenseName",
                "Category_Type",
                "CategoryName",
                "AccountTransactionName",
                "TransactionType",
                "CategoryTypeDescription",
                "Transaction_id",
                "AccountTransaction_ID",
                "Expense_ID",
                "CategoryNameID"
            });
            cmbSearchBy.SelectedIndex = 0;
        }

        private void ApplyFilter()
        {
            string text = (txtSearch.Text ?? "").Trim();
            string by = cmbSearchBy.SelectedItem?.ToString() ?? "All";
            var previouslySelectedId = GetSelectedItem()?.Expense_ID;

            var filtered = _items.Where(i => Matches(i, text, by)).ToList();

            _view.RaiseListChangedEvents = false;
            _view.Clear();
            foreach (var it in filtered) _view.Add(it);
            _view.RaiseListChangedEvents = true;
            _view.ResetBindings();

            // restore selection if possible
            if (previouslySelectedId != null)
            {
                for (int r = 0; r < dgvTransactions.Rows.Count; r++)
                {
                    var rowItem = dgvTransactions.Rows[r].DataBoundItem as AccountExpenseTransaction;
                    if (rowItem?.Expense_ID == previouslySelectedId)
                    {
                        dgvTransactions.ClearSelection();
                        dgvTransactions.Rows[r].Selected = true;
                        dgvTransactions.CurrentCell = dgvTransactions.Rows[r].Cells[0];
                        break;
                    }
                }
            }

            dgvTransactions_SelectionChanged(this, EventArgs.Empty);
        }

        private static string NormKey(string s)
        {
            s = (s ?? "");
            var arr = s.Where(ch => char.IsLetterOrDigit(ch)).ToArray();
            return new string(arr).ToLowerInvariant();
        }

        private static string MapToComboCashValue(string input)
        {
            var key = NormKey(input);
            if (key == "cashin") return "CashIn";
            if (key == "cashout") return "CashOut";
            if (key == "noncash" || key == "cashnon") return "CashNon";
            return null;
        }

        private bool Matches(AccountExpenseTransaction i, string text, string by)
        {
            if (string.IsNullOrEmpty(text)) return true;
            var t = text.ToLowerInvariant();

            bool Contains(string s) => (s ?? "").ToLowerInvariant().Contains(t);
            bool ContainsInt(int? v) => v?.ToString().Contains(text) ?? false;

            switch (by)
            {
                case "ExpenseName": return Contains(i.ExpenseName);
                case "Category_Type": return Contains(i.Category_Type);
                case "CategoryName": return Contains(i.CategoryName);
                case "AccountTransactionName": return Contains(i.AccountTransactionName);
                case "TransactionType": return Contains(i.TransactionType);
                case "CategoryTypeDescription":
                    // Make searching robust for CashIn/CashOut/NonCash/CashNon variants
                    return NormKey(i.CategoryTypeDescription).Contains(NormKey(text));
                case "Transaction_id": return ContainsInt(i.Transaction_id);
                case "AccountTransaction_ID": return ContainsInt(i.AccountTransaction_ID);
                case "Expense_ID": return ContainsInt(i.Expense_ID);
                case "CategoryNameID": return ContainsInt(i.CategoryNameID);
                default:
                    return
                        Contains(i.ExpenseName) ||
                        Contains(i.Category_Type) ||
                        Contains(i.CategoryName) ||
                        Contains(i.AccountTransactionName) ||
                        Contains(i.TransactionType) ||
                        NormKey(i.CategoryTypeDescription).Contains(NormKey(text)) ||
                        ContainsInt(i.Transaction_id) ||
                        ContainsInt(i.AccountTransaction_ID) ||
                        ContainsInt(i.Expense_ID) ||
                        ContainsInt(i.CategoryNameID);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearInputs();
            dgvTransactions.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryBuildFromInputs(out var item, out var error))
            {
                MessageBox.Show(error, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (item.Expense_ID == null || item.Expense_ID == 0)
            {
                int next = _items.Select(i => i.Expense_ID ?? 0).DefaultIfEmpty(0).Max() + 1;
                item.Expense_ID = next;
            }

            if (_items.Any(i => i.Expense_ID == item.Expense_ID))
            {
                MessageBox.Show("An item with this Expense_ID already exists. Use Update instead.",
                                "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            addCatExpenseTrans(item);
            Display();
            ApplyFilter();

            var addedIdx = _view.IndexOf(item);
            if (addedIdx >= 0)
            {
                dgvTransactions.ClearSelection();
                dgvTransactions.Rows[addedIdx].Selected = true;
                dgvTransactions.CurrentCell = dgvTransactions.Rows[addedIdx].Cells[0];
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedItem();
            if (selected == null)
            {
                MessageBox.Show("Select a row to update.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryBuildFromInputs(out var edited, out var error))
            {
                MessageBox.Show(error, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (edited.Expense_ID != selected.Expense_ID &&
                _items.Any(i => i.Expense_ID == edited.Expense_ID))
            {
                MessageBox.Show("Expense_ID conflicts with another record.",
                                "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selected.CopyFrom(edited);

            ApplyFilter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedItem();
            if (selected == null)
            {
                MessageBox.Show("Select a row to delete.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Delete selected record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _items.Remove(selected);
                ClearInputs();
                ApplyFilter();
            }
        }

        private void dgvTransactions_SelectionChanged(object sender, EventArgs e)
        {
            var selected = GetSelectedItem();
            if (selected == null) return;

            txtExpenseName.Text = selected.ExpenseName ?? "";
            txtCategoryType.Text = selected.Category_Type ?? "";
            txtCategoryName.Text = selected.CategoryName ?? "";
            txtAccountTransactionName.Text = selected.AccountTransactionName ?? "";
            txtTransactionType.Text = selected.TransactionType ?? "";
            // Set Combo from string (supports NonCash -> CashNon)
            SetCashComboFromString(selected.CategoryTypeDescription);
            txtTransactionId.Text = selected.Transaction_id?.ToString() ?? "";
            txtAccountTransactionId.Text = selected.AccountTransaction_ID?.ToString() ?? "";
            txtExpenseId.Text = selected.Expense_ID?.ToString() ?? "";
            txtCategoryNameId.Text = selected.CategoryNameID?.ToString() ?? "";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cmbSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbSearchBy.SelectedIndex = 0;
            ApplyFilter();
        }

        private void cmbCategoryTypeDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            // No immediate action required; value is read when Add/Update is clicked.
            // You could auto-update the selected row here if you want.
        }

        private AccountExpenseTransaction GetSelectedItem()
        {
            return dgvTransactions.CurrentRow?.DataBoundItem as AccountExpenseTransaction;
        }

        private void ClearInputs()
        {
            txtExpenseName.Clear();
            txtCategoryType.Clear();
            txtCategoryName.Clear();
            txtAccountTransactionName.Clear();
            txtTransactionType.Clear();
            cmbCategoryTypeDescription.SelectedIndex = -1;
            txtTransactionId.Clear();
            txtAccountTransactionId.Clear();
            txtExpenseId.Clear();
            txtCategoryNameId.Clear();
        }

        private void SetCashComboFromString(string value)
        {
            var mapped = MapToComboCashValue(value);
            if (mapped == null)
            {
                cmbCategoryTypeDescription.SelectedIndex = -1;
                return;
            }
            // select item ignoring case
            for (int i = 0; i < cmbCategoryTypeDescription.Items.Count; i++)
            {
                var it = cmbCategoryTypeDescription.Items[i]?.ToString();
                if (string.Equals(it, mapped, StringComparison.OrdinalIgnoreCase))
                {
                    cmbCategoryTypeDescription.SelectedIndex = i;
                    return;
                }
            }
            cmbCategoryTypeDescription.SelectedIndex = -1;
        }

        private bool TryBuildFromInputs(out AccountExpenseTransaction item, out string error)
        {
            item = new AccountExpenseTransaction
            {
                ExpenseName = txtExpenseName.Text.Trim(),
                Category_Type = txtCategoryType.Text.Trim(),
                CategoryName = txtCategoryName.Text.Trim(),
                AccountTransactionName = txtAccountTransactionName.Text.Trim(),
                TransactionType = txtTransactionType.Text.Trim(),
                // Write back the combo selection. We keep the exact display text ("CashIn","CashOut","CashNon").
                CategoryTypeDescription = cmbCategoryTypeDescription.SelectedItem?.ToString(),
            };

            if (!TryParseNullableInt(txtTransactionId.Text, "Transaction_id", out var transactionId, out error)) return false;
            if (!TryParseNullableInt(txtAccountTransactionId.Text, "AccountTransaction_ID", out var accountTransactionId, out error)) return false;
            if (!TryParseNullableInt(txtExpenseId.Text, "Expense_ID", out var expenseId, out error)) return false;
            if (!TryParseNullableInt(txtCategoryNameId.Text, "CategoryNameID", out var categoryNameId, out error)) return false;

            item.Transaction_id = transactionId;
            item.AccountTransaction_ID = accountTransactionId;
            item.Expense_ID = expenseId;
            item.CategoryNameID = categoryNameId;

            error = null;
            return true;
        }

        private bool TryParseNullableInt(string text, string field, out int? value, out string error)
        {
            error = null;
            var t = (text ?? "").Trim();
            if (string.IsNullOrEmpty(t))
            {
                value = null;
                return true;
            }
            if (int.TryParse(t, out var v))
            {
                value = v;
                return true;
            }
            value = null;
            error = $"{field} must be an integer (or left blank).";
            return false;
        }

        private static int? ParseNullableInt(string s)
        {
            if (int.TryParse((s ?? "").Trim(), out var v)) return v;
            return null;
        }
        private void addCatExpenseTrans(AccountExpenseTransaction item)
        {
            _items.Add(item);
            new BLogic().addExpenseName(item.ExpenseName);
        }
        public void Display()
        {
            string search = "";
            _items.Clear();
            List<Object> obj = (List<object>)new BLogic().searchProfile("", "ExpenseType", search, 1, 100);
            DataTable dt = (DataTable)obj[1];
            foreach (DataRow row in dt.Rows)
            {
                _items.Add(new AccountExpenseTransaction
                {
                    Expense_ID = int.Parse(row[9].ToString()),
                    ExpenseName = row[0].ToString(),

                    CategoryNameID = int.Parse(row[10].ToString()),
                    Category_Type = row[1].ToString(),
                    CategoryName = row[2].ToString(),
                    CategoryTypeDescription = row[5].ToString(),


                    AccountTransaction_ID = int.Parse(row[7].ToString()),
                    AccountTransactionName = row[3].ToString(),

                    Transaction_id = int.Parse(row[6].ToString()),
                    TransactionType = row[4].ToString()
                });
            }

            return;

        }

        private void check_eng_urdu_CheckedChanged(object sender, EventArgs e)
        {
            if (!check_eng_urdu.Checked)
            {
                txtSearch.LangEnglish = false;//urdu
            }
            else
            {
                txtSearch.LangEnglish = true;//English
            }
        }
    }
}