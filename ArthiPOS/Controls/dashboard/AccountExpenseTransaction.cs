using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArthiPOS.Controls.dashboard
{
    public class AccountExpenseTransaction : INotifyPropertyChanged
    {
        private string _ExpenseName;
        private string _Category_Type;
        private string _CategoryName;
        private string _AccountTransactionName;
        private string _TransactionType;
        private string _CategoryTypeDescription;
        private int? _Transaction_id;
        private int? _AccountTransaction_ID;
        private int? _Expense_ID;
        private int? _CategoryNameID;

        public string ExpenseName
        {
            get => _ExpenseName; set { _ExpenseName = value; OnPropertyChanged(); }
        }
        public string Category_Type
        {
            get => _Category_Type; set { _Category_Type = value; OnPropertyChanged(); }
        }
        public string CategoryName
        {
            get => _CategoryName; set { _CategoryName = value; OnPropertyChanged(); }
        }
        public string AccountTransactionName
        {
            get => _AccountTransactionName; set { _AccountTransactionName = value; OnPropertyChanged(); }
        }
        public string TransactionType
        {
            get => _TransactionType; set { _TransactionType = value; OnPropertyChanged(); }
        }
        public string CategoryTypeDescription
        {
            get => _CategoryTypeDescription; set { _CategoryTypeDescription = value; OnPropertyChanged(); }
        }
        public int? Transaction_id
        {
            get => _Transaction_id; set { _Transaction_id = value; OnPropertyChanged(); }
        }
        public int? AccountTransaction_ID
        {
            get => _AccountTransaction_ID; set { _AccountTransaction_ID = value; OnPropertyChanged(); }
        }
        public int? Expense_ID
        {
            get => _Expense_ID; set { _Expense_ID = value; OnPropertyChanged(); }
        }
        public int? CategoryNameID
        {
            get => _CategoryNameID; set { _CategoryNameID = value; OnPropertyChanged(); }
        }

        public AccountExpenseTransaction Clone() => new AccountExpenseTransaction
        {
            ExpenseName = this.ExpenseName,
            Category_Type = this.Category_Type,
            CategoryName = this.CategoryName,
            AccountTransactionName = this.AccountTransactionName,
            TransactionType = this.TransactionType,
            CategoryTypeDescription = this.CategoryTypeDescription,
            Transaction_id = this.Transaction_id,
            AccountTransaction_ID = this.AccountTransaction_ID,
            Expense_ID = this.Expense_ID,
            CategoryNameID = this.CategoryNameID
        };

        public void CopyFrom(AccountExpenseTransaction other)
        {
            if (other == null) return;
            ExpenseName = other.ExpenseName;
            Category_Type = other.Category_Type;
            CategoryName = other.CategoryName;
            AccountTransactionName = other.AccountTransactionName;
            TransactionType = other.TransactionType;
            CategoryTypeDescription = other.CategoryTypeDescription;
            Transaction_id = other.Transaction_id;
            AccountTransaction_ID = other.AccountTransaction_ID;
            Expense_ID = other.Expense_ID;
            CategoryNameID = other.CategoryNameID;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}