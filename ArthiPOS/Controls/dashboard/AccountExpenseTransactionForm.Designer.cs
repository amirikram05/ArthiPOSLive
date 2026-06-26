namespace ArthiPOS.Controls.dashboard
{
        partial class AccountExpenseTransactionForm
        {
            private System.ComponentModel.IContainer components = null;

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                    components.Dispose();
                base.Dispose(disposing);
            }

            #region Windows Form Designer generated code

            private void InitializeComponent()
            {
            this.groupBoxEditor = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelEditor = new System.Windows.Forms.TableLayoutPanel();
            this.lblExpenseName = new System.Windows.Forms.Label();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.txtExpenseId = new System.Windows.Forms.TextBox();
            this.lblExpenseId = new System.Windows.Forms.Label();
            this.txtAccountTransactionName = new System.Windows.Forms.TextBox();
            this.lblCategoryTypeDescription = new System.Windows.Forms.Label();
            this.cmbCategoryTypeDescription = new System.Windows.Forms.ComboBox();
            this.lblCategoryNameId = new System.Windows.Forms.Label();
            this.txtCategoryNameId = new System.Windows.Forms.TextBox();
            this.lblAccountTransactionName = new System.Windows.Forms.Label();
            this.lblTransactionType = new System.Windows.Forms.Label();
            this.txtTransactionType = new System.Windows.Forms.TextBox();
            this.lblCategoryType = new System.Windows.Forms.Label();
            this.lblAccountTransactionId = new System.Windows.Forms.Label();
            this.txtAccountTransactionId = new System.Windows.Forms.TextBox();
            this.lblTransactionId = new System.Windows.Forms.Label();
            this.txtTransactionId = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panelSearch = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.check_eng_urdu = new System.Windows.Forms.CheckBox();
            this.lblSearchBy = new System.Windows.Forms.Label();
            this.cmbSearchBy = new System.Windows.Forms.ComboBox();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.txtSearch = new ArthiPOS.Controls.UrduTextBox();
            this.txtCategoryType = new ArthiPOS.Controls.UrduTextBox();
            this.txtExpenseName = new ArthiPOS.Controls.UrduTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.colExpenseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoryType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccountTransactionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransactionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoryTypeDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTransactionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAccountTransactionId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpenseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoryNameId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxEditor.SuspendLayout();
            this.tableLayoutPanelEditor.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxEditor
            // 
            this.groupBoxEditor.Controls.Add(this.tableLayoutPanelEditor);
            this.groupBoxEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxEditor.Location = new System.Drawing.Point(0, 0);
            this.groupBoxEditor.Name = "groupBoxEditor";
            this.groupBoxEditor.Padding = new System.Windows.Forms.Padding(9);
            this.groupBoxEditor.Size = new System.Drawing.Size(1154, 206);
            this.groupBoxEditor.TabIndex = 0;
            this.groupBoxEditor.TabStop = false;
            this.groupBoxEditor.Text = "Editor";
            // 
            // tableLayoutPanelEditor
            // 
            this.tableLayoutPanelEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelEditor.ColumnCount = 4;
            this.tableLayoutPanelEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEditor.Controls.Add(this.lblExpenseName, 0, 0);
            this.tableLayoutPanelEditor.Controls.Add(this.lblCategoryName, 0, 1);
            this.tableLayoutPanelEditor.Controls.Add(this.txtCategoryName, 1, 1);
            this.tableLayoutPanelEditor.Controls.Add(this.txtCategoryType, 1, 3);
            this.tableLayoutPanelEditor.Controls.Add(this.txtExpenseName, 1, 0);
            this.tableLayoutPanelEditor.Controls.Add(this.txtExpenseId, 3, 0);
            this.tableLayoutPanelEditor.Controls.Add(this.lblExpenseId, 2, 0);
            this.tableLayoutPanelEditor.Controls.Add(this.txtAccountTransactionName, 1, 5);
            this.tableLayoutPanelEditor.Controls.Add(this.lblCategoryTypeDescription, 0, 2);
            this.tableLayoutPanelEditor.Controls.Add(this.cmbCategoryTypeDescription, 1, 2);
            this.tableLayoutPanelEditor.Controls.Add(this.lblCategoryNameId, 2, 1);
            this.tableLayoutPanelEditor.Controls.Add(this.txtCategoryNameId, 3, 1);
            this.tableLayoutPanelEditor.Controls.Add(this.lblAccountTransactionName, 0, 5);
            this.tableLayoutPanelEditor.Controls.Add(this.lblTransactionType, 0, 4);
            this.tableLayoutPanelEditor.Controls.Add(this.txtTransactionType, 1, 4);
            this.tableLayoutPanelEditor.Controls.Add(this.lblCategoryType, 0, 3);
            this.tableLayoutPanelEditor.Controls.Add(this.lblAccountTransactionId, 2, 5);
            this.tableLayoutPanelEditor.Controls.Add(this.txtAccountTransactionId, 3, 5);
            this.tableLayoutPanelEditor.Controls.Add(this.lblTransactionId, 2, 4);
            this.tableLayoutPanelEditor.Controls.Add(this.txtTransactionId, 3, 4);
            this.tableLayoutPanelEditor.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanelEditor.Name = "tableLayoutPanelEditor";
            this.tableLayoutPanelEditor.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tableLayoutPanelEditor.RowCount = 6;
            this.tableLayoutPanelEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelEditor.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelEditor.Size = new System.Drawing.Size(1011, 191);
            this.tableLayoutPanelEditor.TabIndex = 0;
            // 
            // lblExpenseName
            // 
            this.lblExpenseName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblExpenseName.AutoSize = true;
            this.lblExpenseName.Location = new System.Drawing.Point(3, 11);
            this.lblExpenseName.Name = "lblExpenseName";
            this.lblExpenseName.Size = new System.Drawing.Size(79, 13);
            this.lblExpenseName.TabIndex = 0;
            this.lblExpenseName.Text = "ExpenseName:";
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Location = new System.Drawing.Point(3, 41);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(80, 13);
            this.lblCategoryName.TabIndex = 4;
            this.lblCategoryName.Text = "CategoryName:";
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCategoryName.Location = new System.Drawing.Point(143, 38);
            this.txtCategoryName.Margin = new System.Windows.Forms.Padding(3, 5, 10, 5);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(358, 20);
            this.txtCategoryName.TabIndex = 5;
            // 
            // txtExpenseId
            // 
            this.txtExpenseId.Location = new System.Drawing.Point(643, 8);
            this.txtExpenseId.Margin = new System.Windows.Forms.Padding(3, 5, 10, 5);
            this.txtExpenseId.Name = "txtExpenseId";
            this.txtExpenseId.Size = new System.Drawing.Size(358, 20);
            this.txtExpenseId.TabIndex = 17;
            // 
            // lblExpenseId
            // 
            this.lblExpenseId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblExpenseId.AutoSize = true;
            this.lblExpenseId.Location = new System.Drawing.Point(514, 11);
            this.lblExpenseId.Name = "lblExpenseId";
            this.lblExpenseId.Size = new System.Drawing.Size(68, 13);
            this.lblExpenseId.TabIndex = 16;
            this.lblExpenseId.Text = "Expense_ID:";
            // 
            // txtAccountTransactionName
            // 
            this.txtAccountTransactionName.Location = new System.Drawing.Point(143, 158);
            this.txtAccountTransactionName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtAccountTransactionName.Name = "txtAccountTransactionName";
            this.txtAccountTransactionName.Size = new System.Drawing.Size(358, 20);
            this.txtAccountTransactionName.TabIndex = 7;
            // 
            // lblCategoryTypeDescription
            // 
            this.lblCategoryTypeDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategoryTypeDescription.AutoSize = true;
            this.lblCategoryTypeDescription.Location = new System.Drawing.Point(3, 72);
            this.lblCategoryTypeDescription.Name = "lblCategoryTypeDescription";
            this.lblCategoryTypeDescription.Size = new System.Drawing.Size(129, 13);
            this.lblCategoryTypeDescription.TabIndex = 10;
            this.lblCategoryTypeDescription.Text = "CategoryTypeDescription:";
            // 
            // cmbCategoryTypeDescription
            // 
            this.cmbCategoryTypeDescription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryTypeDescription.FormattingEnabled = true;
            this.cmbCategoryTypeDescription.Items.AddRange(new object[] {
            "CashIn",
            "CashOut",
            "CashNon"});
            this.cmbCategoryTypeDescription.Location = new System.Drawing.Point(143, 68);
            this.cmbCategoryTypeDescription.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.cmbCategoryTypeDescription.Name = "cmbCategoryTypeDescription";
            this.cmbCategoryTypeDescription.Size = new System.Drawing.Size(358, 21);
            this.cmbCategoryTypeDescription.TabIndex = 11;
            this.cmbCategoryTypeDescription.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryTypeDescription_SelectedIndexChanged);
            // 
            // lblCategoryNameId
            // 
            this.lblCategoryNameId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategoryNameId.AutoSize = true;
            this.lblCategoryNameId.Location = new System.Drawing.Point(514, 41);
            this.lblCategoryNameId.Name = "lblCategoryNameId";
            this.lblCategoryNameId.Size = new System.Drawing.Size(91, 13);
            this.lblCategoryNameId.TabIndex = 18;
            this.lblCategoryNameId.Text = "CategoryNameID:";
            // 
            // txtCategoryNameId
            // 
            this.txtCategoryNameId.Location = new System.Drawing.Point(643, 38);
            this.txtCategoryNameId.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtCategoryNameId.Name = "txtCategoryNameId";
            this.txtCategoryNameId.Size = new System.Drawing.Size(358, 20);
            this.txtCategoryNameId.TabIndex = 19;
            // 
            // lblAccountTransactionName
            // 
            this.lblAccountTransactionName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAccountTransactionName.AutoSize = true;
            this.lblAccountTransactionName.Location = new System.Drawing.Point(3, 165);
            this.lblAccountTransactionName.Name = "lblAccountTransactionName";
            this.lblAccountTransactionName.Size = new System.Drawing.Size(134, 13);
            this.lblAccountTransactionName.TabIndex = 6;
            this.lblAccountTransactionName.Text = "AccountTransactionName:";
            // 
            // lblTransactionType
            // 
            this.lblTransactionType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTransactionType.AutoSize = true;
            this.lblTransactionType.Location = new System.Drawing.Point(3, 131);
            this.lblTransactionType.Name = "lblTransactionType";
            this.lblTransactionType.Size = new System.Drawing.Size(90, 13);
            this.lblTransactionType.TabIndex = 8;
            this.lblTransactionType.Text = "TransactionType:";
            // 
            // txtTransactionType
            // 
            this.txtTransactionType.Location = new System.Drawing.Point(143, 128);
            this.txtTransactionType.Margin = new System.Windows.Forms.Padding(3, 5, 10, 5);
            this.txtTransactionType.Name = "txtTransactionType";
            this.txtTransactionType.Size = new System.Drawing.Size(358, 20);
            this.txtTransactionType.TabIndex = 9;
            // 
            // lblCategoryType
            // 
            this.lblCategoryType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategoryType.AutoSize = true;
            this.lblCategoryType.Location = new System.Drawing.Point(3, 102);
            this.lblCategoryType.Name = "lblCategoryType";
            this.lblCategoryType.Size = new System.Drawing.Size(82, 13);
            this.lblCategoryType.TabIndex = 2;
            this.lblCategoryType.Text = "Category_Type:";
            // 
            // lblAccountTransactionId
            // 
            this.lblAccountTransactionId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAccountTransactionId.AutoSize = true;
            this.lblAccountTransactionId.Location = new System.Drawing.Point(514, 165);
            this.lblAccountTransactionId.Name = "lblAccountTransactionId";
            this.lblAccountTransactionId.Size = new System.Drawing.Size(123, 13);
            this.lblAccountTransactionId.TabIndex = 14;
            this.lblAccountTransactionId.Text = "AccountTransaction_ID:";
            // 
            // txtAccountTransactionId
            // 
            this.txtAccountTransactionId.Location = new System.Drawing.Point(643, 158);
            this.txtAccountTransactionId.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtAccountTransactionId.Name = "txtAccountTransactionId";
            this.txtAccountTransactionId.Size = new System.Drawing.Size(358, 20);
            this.txtAccountTransactionId.TabIndex = 15;
            // 
            // lblTransactionId
            // 
            this.lblTransactionId.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTransactionId.AutoSize = true;
            this.lblTransactionId.Location = new System.Drawing.Point(514, 131);
            this.lblTransactionId.Name = "lblTransactionId";
            this.lblTransactionId.Size = new System.Drawing.Size(80, 13);
            this.lblTransactionId.TabIndex = 12;
            this.lblTransactionId.Text = "Transaction_id:";
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.Location = new System.Drawing.Point(643, 128);
            this.txtTransactionId.Margin = new System.Windows.Forms.Padding(3, 5, 10, 5);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.Size = new System.Drawing.Size(358, 20);
            this.txtTransactionId.TabIndex = 13;
            // 
            // panelButtons
            // 
            this.panelButtons.AutoSize = true;
            this.panelButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelButtons.Controls.Add(this.btnNew);
            this.panelButtons.Controls.Add(this.btnAdd);
            this.panelButtons.Controls.Add(this.btnUpdate);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 206);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(7, 3, 7, 7);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.panelButtons.Size = new System.Drawing.Size(1154, 43);
            this.panelButtons.TabIndex = 1;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(12, 10);
            this.btnNew.Margin = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(86, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New/Clear";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(108, 10);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(204, 10);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(86, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(300, 10);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(86, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panelSearch
            // 
            this.panelSearch.AutoSize = true;
            this.panelSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelSearch.Controls.Add(this.lblSearch);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.check_eng_urdu);
            this.panelSearch.Controls.Add(this.lblSearchBy);
            this.panelSearch.Controls.Add(this.cmbSearchBy);
            this.panelSearch.Controls.Add(this.btnClearSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 249);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(7);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(9, 5, 9, 5);
            this.panelSearch.Size = new System.Drawing.Size(1154, 40);
            this.panelSearch.TabIndex = 3;
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 13);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(44, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search:";
            // 
            // check_eng_urdu
            // 
            this.check_eng_urdu.Location = new System.Drawing.Point(314, 8);
            this.check_eng_urdu.Name = "check_eng_urdu";
            this.check_eng_urdu.Size = new System.Drawing.Size(84, 24);
            this.check_eng_urdu.TabIndex = 5;
            this.check_eng_urdu.Text = "Eng/Urdu";
            this.check_eng_urdu.UseVisualStyleBackColor = true;
            this.check_eng_urdu.CheckedChanged += new System.EventHandler(this.check_eng_urdu_CheckedChanged);
            // 
            // lblSearchBy
            // 
            this.lblSearchBy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSearchBy.AutoSize = true;
            this.lblSearchBy.Location = new System.Drawing.Point(404, 13);
            this.lblSearchBy.Name = "lblSearchBy";
            this.lblSearchBy.Size = new System.Drawing.Size(22, 13);
            this.lblSearchBy.TabIndex = 2;
            this.lblSearchBy.Text = "By:";
            // 
            // cmbSearchBy
            // 
            this.cmbSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchBy.FormattingEnabled = true;
            this.cmbSearchBy.Location = new System.Drawing.Point(432, 8);
            this.cmbSearchBy.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.Size = new System.Drawing.Size(189, 21);
            this.cmbSearchBy.TabIndex = 3;
            this.cmbSearchBy.SelectedIndexChanged += new System.EventHandler(this.cmbSearchBy_SelectedIndexChanged);
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Location = new System.Drawing.Point(634, 8);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(64, 22);
            this.btnClearSearch.TabIndex = 4;
            this.btnClearSearch.Text = "Clear";
            this.btnClearSearch.UseVisualStyleBackColor = true;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.AllowUserToOrderColumns = true;
            this.dgvTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvTransactions.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colExpenseName,
            this.colCategoryType,
            this.colCategoryName,
            this.colAccountTransactionName,
            this.colTransactionType,
            this.colCategoryTypeDescription,
            this.colTransactionId,
            this.colAccountTransactionId,
            this.colExpenseId,
            this.colCategoryNameId});
            this.dgvTransactions.Location = new System.Drawing.Point(10, 3);
            this.dgvTransactions.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.dgvTransactions.MultiSelect = false;
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowHeadersVisible = false;
            this.dgvTransactions.RowTemplate.Height = 25;
            this.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.Size = new System.Drawing.Size(1134, 365);
            this.dgvTransactions.TabIndex = 2;
            this.dgvTransactions.SelectionChanged += new System.EventHandler(this.dgvTransactions_SelectionChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearch.IsNumeric = false;
            this.txtSearch.LangEnglish = false;
            this.txtSearch.Location = new System.Drawing.Point(62, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch.Size = new System.Drawing.Size(246, 23);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtSearch.WaterMarkText = "Search";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // txtCategoryType
            // 
            this.txtCategoryType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCategoryType.IsNumeric = false;
            this.txtCategoryType.LangEnglish = false;
            this.txtCategoryType.Location = new System.Drawing.Point(143, 97);
            this.txtCategoryType.Name = "txtCategoryType";
            this.txtCategoryType.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCategoryType.Size = new System.Drawing.Size(358, 23);
            this.txtCategoryType.TabIndex = 7;
            this.txtCategoryType.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtCategoryType.WaterMarkText = "";
            // 
            // txtExpenseName
            // 
            this.txtExpenseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtExpenseName.IsNumeric = false;
            this.txtExpenseName.LangEnglish = false;
            this.txtExpenseName.Location = new System.Drawing.Point(143, 6);
            this.txtExpenseName.Name = "txtExpenseName";
            this.txtExpenseName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtExpenseName.Size = new System.Drawing.Size(358, 23);
            this.txtExpenseName.TabIndex = 8;
            this.txtExpenseName.WaterMarkColor = System.Drawing.Color.Gray;
            this.txtExpenseName.WaterMarkText = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvTransactions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 289);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1154, 380);
            this.panel1.TabIndex = 4;
            // 
            // colExpenseName
            // 
            this.colExpenseName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.colExpenseName.DataPropertyName = "ExpenseName";
            this.colExpenseName.HeaderText = "ExpenseName";
            this.colExpenseName.Name = "colExpenseName";
            this.colExpenseName.ReadOnly = true;
            this.colExpenseName.Width = 101;
            // 
            // colCategoryType
            // 
            this.colCategoryType.DataPropertyName = "Category_Type";
            this.colCategoryType.HeaderText = "Category_Type";
            this.colCategoryType.Name = "colCategoryType";
            this.colCategoryType.ReadOnly = true;
            this.colCategoryType.Width = 104;
            // 
            // colCategoryName
            // 
            this.colCategoryName.DataPropertyName = "CategoryName";
            this.colCategoryName.HeaderText = "CategoryName";
            this.colCategoryName.Name = "colCategoryName";
            this.colCategoryName.ReadOnly = true;
            this.colCategoryName.Width = 102;
            // 
            // colAccountTransactionName
            // 
            this.colAccountTransactionName.DataPropertyName = "AccountTransactionName";
            this.colAccountTransactionName.HeaderText = "AccountTransactionName";
            this.colAccountTransactionName.Name = "colAccountTransactionName";
            this.colAccountTransactionName.ReadOnly = true;
            this.colAccountTransactionName.Width = 156;
            // 
            // colTransactionType
            // 
            this.colTransactionType.DataPropertyName = "TransactionType";
            this.colTransactionType.HeaderText = "TransactionType";
            this.colTransactionType.Name = "colTransactionType";
            this.colTransactionType.ReadOnly = true;
            this.colTransactionType.Width = 112;
            // 
            // colCategoryTypeDescription
            // 
            this.colCategoryTypeDescription.DataPropertyName = "CategoryTypeDescription";
            this.colCategoryTypeDescription.HeaderText = "CategoryTypeDescription";
            this.colCategoryTypeDescription.Name = "colCategoryTypeDescription";
            this.colCategoryTypeDescription.ReadOnly = true;
            this.colCategoryTypeDescription.Width = 151;
            // 
            // colTransactionId
            // 
            this.colTransactionId.DataPropertyName = "Transaction_id";
            this.colTransactionId.HeaderText = "Transaction_id";
            this.colTransactionId.Name = "colTransactionId";
            this.colTransactionId.ReadOnly = true;
            this.colTransactionId.Width = 102;
            // 
            // colAccountTransactionId
            // 
            this.colAccountTransactionId.DataPropertyName = "AccountTransaction_ID";
            this.colAccountTransactionId.HeaderText = "AccountTransaction_ID";
            this.colAccountTransactionId.Name = "colAccountTransactionId";
            this.colAccountTransactionId.ReadOnly = true;
            this.colAccountTransactionId.Width = 145;
            // 
            // colExpenseId
            // 
            this.colExpenseId.DataPropertyName = "Expense_ID";
            this.colExpenseId.HeaderText = "Expense_ID";
            this.colExpenseId.Name = "colExpenseId";
            this.colExpenseId.ReadOnly = true;
            this.colExpenseId.Width = 90;
            // 
            // colCategoryNameId
            // 
            this.colCategoryNameId.DataPropertyName = "CategoryNameID";
            this.colCategoryNameId.HeaderText = "CategoryNameID";
            this.colCategoryNameId.Name = "colCategoryNameId";
            this.colCategoryNameId.ReadOnly = true;
            this.colCategoryNameId.Width = 113;
            // 
            // AccountExpenseTransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 669);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.groupBoxEditor);
            this.Name = "AccountExpenseTransactionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Expense Transactions";
            this.groupBoxEditor.ResumeLayout(false);
            this.tableLayoutPanelEditor.ResumeLayout(false);
            this.tableLayoutPanelEditor.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.GroupBox groupBoxEditor;
            private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEditor;
            private System.Windows.Forms.Label lblExpenseName;
            private System.Windows.Forms.Label lblCategoryType;
            private System.Windows.Forms.Label lblCategoryName;
            private System.Windows.Forms.TextBox txtCategoryName;
            private System.Windows.Forms.Label lblAccountTransactionName;
            private System.Windows.Forms.TextBox txtAccountTransactionName;
            private System.Windows.Forms.Label lblTransactionType;
            private System.Windows.Forms.TextBox txtTransactionType;
            private System.Windows.Forms.Label lblCategoryTypeDescription;
            private System.Windows.Forms.ComboBox cmbCategoryTypeDescription;
            private System.Windows.Forms.Label lblTransactionId;
            private System.Windows.Forms.TextBox txtTransactionId;
            private System.Windows.Forms.Label lblAccountTransactionId;
            private System.Windows.Forms.TextBox txtAccountTransactionId;
            private System.Windows.Forms.Label lblExpenseId;
            private System.Windows.Forms.Label lblCategoryNameId;
            private System.Windows.Forms.TextBox txtCategoryNameId;
            private System.Windows.Forms.FlowLayoutPanel panelButtons;
            private System.Windows.Forms.Button btnNew;
            private System.Windows.Forms.Button btnAdd;
            private System.Windows.Forms.Button btnUpdate;
            private System.Windows.Forms.Button btnDelete;
            private System.Windows.Forms.FlowLayoutPanel panelSearch;
            private System.Windows.Forms.Label lblSearch;
            private System.Windows.Forms.Label lblSearchBy;
            private System.Windows.Forms.ComboBox cmbSearchBy;
            private System.Windows.Forms.Button btnClearSearch;
            private System.Windows.Forms.DataGridView dgvTransactions;
        private UrduTextBox txtSearch;
        private System.Windows.Forms.CheckBox check_eng_urdu;
        private System.Windows.Forms.TextBox txtExpenseId;
        private UrduTextBox txtCategoryType;
        private UrduTextBox txtExpenseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpenseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccountTransactionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransactionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryTypeDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTransactionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAccountTransactionId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpenseId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryNameId;
        private System.Windows.Forms.Panel panel1;
    }
    }
