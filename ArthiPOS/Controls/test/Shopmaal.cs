using ArthiPOS.Controls.dashboard;
using BAL;
using EnvDTE;
using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArthiPOS.Controls.test
{
    public partial class Shopmaal : Form
    {
        public Shopmaal()
        {
            InitializeComponent();
            this.KeyPreview = true;

            // Attach Enter key events for field focus management
            txt_userid.KeyDown += Txt_userid_KeyDown;
            date_today.KeyDown += date_today_KeyDown;
            txt_nobegs.KeyDown += Txt_nobegs_KeyDown;
            txt_begtype.KeyDown += Txt_begtype_KeyDown;
            txt_rate.KeyDown += Txt_rate_KeyDown;
            txt_rate.TextChanged += Txt_rate_TextChange;
            txt_labourperitem.KeyDown += Txt_labourperitem_KeyDown;
            txt_total_amount.KeyDown += Txt_total_amount_KeyDown;
            txt_labour_amount.KeyDown += Txt_labour_amount_KeyDown;

            // Live total update when total or labour amount changes
            txt_total_amount.TextChanged += TotalOrLabour_Changed;
            txt_labour_amount.TextChanged += TotalOrLabour_Changed;

            // Save button & keyboard shortcut
            btnAddCalculate.Click += btnAddCalculate_Click_1;
            this.KeyDown += Shopmaal_KeyDown;

            // Set tab order explicitly (important)
            txt_userid.TabIndex = 0;
            date_today.TabIndex = 1;
            txt_nobegs.TabIndex = 2;
            txt_begtype.TabIndex = 3;
            txt_rate.TabIndex = 4;
            txt_total_amount.TabIndex = 5;
            txt_labour_amount.TabIndex = 6;
            btnAddCalculate.TabIndex = 7;
        }

        private void Txt_rate_TextChange(object sender, EventArgs e)
        {

            calculate();
        }
        private void calculate()
        {
            int gtotal = string.IsNullOrEmpty(txt_rate.Text) == true ? 0 : int.Parse(txt_rate.Text)*(string.IsNullOrEmpty(txt_nobegs.Text) == true ? 0 : int.Parse(txt_nobegs.Text));
            int labour = string.IsNullOrEmpty(txt_labourperitem.Text) == true ? 0 : int.Parse(txt_labourperitem.Text) * (string.IsNullOrEmpty(txt_nobegs.Text) == true ? 0 : int.Parse(txt_nobegs.Text));
            
            gtotal = gtotal - labour;
            txt_labour_amount.Text = labour + "";
            txt_total_amount.Text = lbl_total.Text = gtotal + "";
        }

        // 1️⃣ User ID textbox - opens search dialog, then focus to txt_nobegs
        private void Txt_userid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchDialog(1, txt_userid.Text);

                date_today.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void date_today_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txt_nobegs.Focus();
                txt_nobegs.Clear();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void Txt_begtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchDialog(3, txt_begtype.Text);
                txt_rate.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        // 2️⃣ No of bags → Total Amount
        private void Txt_labourperitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                calculate();
                txt_total_amount.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void Txt_rate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                calculate();
                txt_labourperitem.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Txt_nobegs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                calculate();
                txt_begtype.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // 3️⃣ Total Amount → Labour Amount
        private void Txt_total_amount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_labour_amount.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // 4️⃣ Labour Amount → Save button
        private void Txt_labour_amount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddCalculate.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // 5️⃣ Ctrl + Enter => Save (anywhere)
        private void Shopmaal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                SaveToDatabase();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.Enter)
            {
                SaveToDatabase();
                e.Handled = true;
                e.SuppressKeyPress = true;
                date_today.Focus();
            }
            
        }

        // -------------------------------------------------------
        // 4️⃣ Save Button Click
        // -------------------------------------------------------
        private void btnAddCalculate_Click_1(object sender, EventArgs e)
        {
            
            SaveToDatabase();
        }

        // -------------------------------------------------------
        // 5️⃣ Save to Database + Clear + Focus
        // -------------------------------------------------------
        public bool IsSaved { get; private set; } = false;

        private void SaveToDatabase()
        {

            try
            {
                calculate();

                // --- Validate input ---
                string uid = lbl_custid.Text?.Trim();
                string uname = txt_userid.Text?.Trim();
                string tdate = date_today.Text;
                string lasttdate = date_today.Text;

                if (string.IsNullOrEmpty(uid) || uid == "0" || string.IsNullOrEmpty(uname))
                {
                    MessageBox.Show("⚠️ Please select a valid user first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_userid.Focus();
                    return;
                }

                // --- Parse numeric fields safely ---
                int quantity = 0;
                int total = 0;
                int rate = 0;
                int labour = 0;

                int.TryParse(txt_nobegs.Text, out quantity);
                int.TryParse(txt_total_amount.Text, out total);
                int.TryParse(txt_labour_amount.Text, out labour);
                int.TryParse(txt_rate.Text, out rate);

                // --- Prepare other fields ---
                string product = txt_begtype.Text;
                string size = "";
                string productid = "";
                string details = txt_details.Text;

                // --- Call your BLogic insert/update ---
                List<object> obj = (List<object>)new BLogic().shopCrud_InsertUpdate(
                    "I",
                    tdate,
                    lasttdate,
                    uname,
                    uid,
                    quantity.ToString(),
                    ""+rate,
                    size,
                    product,
                    tdate,
                    total.ToString(),
                    "0",
                    0,
                    -1,
                    labour,
                    productid, details
                );

                if (obj == null)
                {
                    MessageBox.Show("❌ Data not saved. Please check your connection or input.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // --- Success message ---
                lbl_status.Text = "✅ Saved successfully!";
                lbl_status.ForeColor = System.Drawing.Color.Green;

                // --- Mark as saved and close ---
                IsSaved = true;

                // Optional: short delay before closing, so user can see message
                Task.Delay(400).ContinueWith(_ =>
                {
                    this.Invoke((Action)(() =>
                    {
                        //ClearFields();
                        txt_userid.Focus();
                        //this.Close();  // closes the dialog
                    }));
                });
            }
            catch (Exception ex)
            {
                lbl_status.Text = "❌ Error: " + ex.Message;
                lbl_status.ForeColor = System.Drawing.Color.Red;
            }
        }


        // -------------------------------------------------------
        // 6️⃣ Update lbl_total when Total/Labour change
        // -------------------------------------------------------
        private void TotalOrLabour_Changed(object sender, EventArgs e)
        {
            decimal total = GetDecimal(txt_total_amount.Text);
            decimal labour = GetDecimal(txt_labour_amount.Text);
            decimal grandTotal = total + labour;

            lbl_total.Text = grandTotal.ToString("N2");
        }

        // -------------------------------------------------------
        // 7️⃣ Utility methods
        // -------------------------------------------------------
        private void ClearFields()
        {
            txt_userid.Text = "";
            txt_total_amount.Text = "";
            txt_nobegs.Text = "";
            txt_labour_amount.Text = "";
            lbl_total.Text = "000000";
        }

        private decimal GetDecimal(string input)
        {
            decimal.TryParse(input, out decimal val);
            return val;
        }

        private int GetInt(string input)
        {
            int.TryParse(input, out int val);
            return val;
        }
        Search search;
        public void searchDialog(int action, string searchTxt)
        {


            using (search = new Search(action, searchTxt == "Search" ? "" : searchTxt, 1, 0, ""))
            {


                DialogResult res = search.ShowDialog();
                if (action == 1)
                {
                        txt_userid.Text = search.Name;
                        lbl_custid.Text = search.Id;
                        txt_nobegs.Focus();
                }
                else if (action == 3)
                {
                    txt_begtype.Text = search.Name;
                    txt_rate.Text = search.ShopComm;
                    txt_labourperitem.Text=search.ShopLabour;
                    calculate();
                    txt_nobegs.Focus();
                }

                search.Close();



                return;
            }
        }

        private void Shopmaal_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
