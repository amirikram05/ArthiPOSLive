using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    class OleDbHandler
    {
        public void method(string text)
        {
            String strSQL = "Select * FROM tbl_account where id='" + text + "'";

            double dCurrBalance;

            OleDbConnection Conn = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE = C:\\Program Files\\Microsoft SQL Server\\MSSQL12.MSSQLSERVER\\MSSQL\\DATA\\db_pt.mdb; ");

            Conn.Open();

            OleDbDataReader oReader;

            OleDbCommand cmd = new OleDbCommand(strSQL, Conn);

            OleDbTransaction Trans = Conn.BeginTransaction(IsolationLevel.ReadCommitted);

            cmd.Transaction = Trans;

            try

            {

                oReader = cmd.ExecuteReader();

                oReader.Read();

                dCurrBalance = oReader.GetDouble(0);

                oReader.Close();

                if (dCurrBalance < Convert.ToDouble(text))

                {

                    throw (new Exception("Insufficient funds for transfer"));

                }

                strSQL = "Update p_balance_sheet set capital_cash =  capital_cash - " + text + " where id = '"

                + text + "'";

                cmd.CommandText = strSQL;

                cmd.ExecuteNonQuery();


                Trans.Commit();

                MessageBox.Show("Transaction Complete");
            }

            catch (Exception ex)

            {

                Trans.Rollback();

                MessageBox.Show("Transaction Fail");

            }

            finally

            {

                Conn.Close();

            }
        }
    }
}
