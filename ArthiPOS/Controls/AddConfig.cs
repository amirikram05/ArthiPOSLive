using ArthiPOS.Controls.dashboard;
using ArthiPOS.Controls.test;
using ArthiPOS.shop;
using ArthiPOS.Utill;
using CommonUtilities;
using DataMember;
using DataMember.memberlog;
using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ArthiPOS.Controls
{
    public partial class AddConfig : Form
    {
        public AddConfig(bool config)
        {
            InitializeComponent();
            DatabaseLog db = LogUtill.getDatabaseLog();
            txt_servername.Text = db.ServerName;
            txt_uname.Text = db.UserName;
            txt_password.Text = db.Password;
            txt_livedb.Text = db.LiveDB;
            txt_backupdb.Text = db.Backupdb;
            txt_testingdb.Text = db.Testing_Database;
            lbl_conn.Text = db.connectionName;
            txt_localdb.Text = db.LocalDB;
            panel_config.Enabled = config;
            if (db.Status == "Live")
                db_list.SelectedIndex = 0;
            else if (db.Status == "Testing")
                db_list.SelectedIndex = 1;
            else if (db.Status == "Live_Local")
                db_list.SelectedIndex = 2;


        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            string conName = this.lbl_conn.Text.Trim();
            string servername = txt_servername.Text.Trim();
            string uname = this.txt_uname.Text.Trim();
            string password = this.txt_password.Text.Trim();
            string livedb = this.txt_livedb.Text.Trim();
            string backup = this.txt_backupdb.Text.Trim();
            string test_db = this.txt_testingdb.Text.Trim();
            string local_db = this.txt_localdb.Text.Trim();

            string dbname = livedb;

            if (db_list.SelectedIndex == 0)
            {
                dbname = livedb;
            }
            else if (db_list.SelectedIndex == 1)
            {
                dbname = test_db;
            }
            else if (db_list.SelectedIndex == 2)
                dbname = "dbt";

#if DEBUG
            Console.WriteLine("Running in Debug mode");
#else
                dbname = livedb;
#endif

            LogUtill.loadDBConfig(servername, uname, password, livedb, backup, lbl_conn.Text, test_db, dbname, local_db, 1);
            DatabaseLog db = LogUtill.getDatabaseLog();
            lbl_conn.Text = db.connectionName;

            //new BLogic().account_update(acc);
            //configureDB(conName, dbname, servername, uname, password);
        }
        /*public void configureDB(string connectionname, string dbname, string servername, string username, string password)
        {
            string ApplicationPath = Application.StartupPath;
            string YourPath = Path.GetDirectoryName(ApplicationPath);
            bool isNew = false;

            var path11 = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            string path= path11.Substring(6)+ "\\ArthiPOS.exe.config";
            //string path = Path.GetDirectoryName(YourPath) + "\\ArthiPOS.exe.config";
            XmlDocument doc = new XmlDocument();
            //return;
            doc.Load(path);
            XmlNodeList list = doc.DocumentElement.SelectNodes(string.Format("connectionStrings/add[@name='{0}']", connectionname));
            XmlNode node;
            isNew = list.Count == 0;
            if (isNew)
            {
                node = doc.CreateNode(XmlNodeType.Element, "add", null);
                XmlAttribute attribute = doc.CreateAttribute("name");
                attribute.Value = connectionname;
                node.Attributes.Append(attribute);

                attribute = doc.CreateAttribute("connectionString");
                attribute.Value = "";
                node.Attributes.Append(attribute);

                attribute = doc.CreateAttribute("providerName");
                attribute.Value = "System.Data.SqlClient";
                node.Attributes.Append(attribute);
            }
            else
            {
                node = list[0];
            }
            string conString = node.Attributes["connectionString"].Value;
            SqlConnectionStringBuilder conStringBuilder = new SqlConnectionStringBuilder(conString);
            conStringBuilder.InitialCatalog = dbname;
            conStringBuilder.DataSource = servername;
            conStringBuilder.PersistSecurityInfo = true;
            conStringBuilder.UserID = username;
            conStringBuilder.Password = password;
            node.Attributes["connectionString"].Value = conStringBuilder.ConnectionString;
            if (isNew)
            {
                doc.DocumentElement.SelectNodes("connectionStrings")[0].AppendChild(node);
            }
            doc.Save(path);
        }
        */
        private void tw_db_Toggled(object sender, EventArgs e)
        {
            //bool testConnection = new BLogic().testConnection();
            //if (testConnection)
            //{
            //    MessageBox.Show("Success DB Connection.");
            //}
            //else
            //{
            //    MessageBox.Show("Fail DB Connection.");
            //}
            string conName = this.lbl_conn.Text.Trim();
            string servername = txt_servername.Text.Trim();
            string uname = this.txt_uname.Text.Trim();
            string password = this.txt_password.Text.Trim();
            string livedb = this.txt_livedb.Text.Trim();
            string backup = this.txt_backupdb.Text.Trim();
            string test_db = this.txt_testingdb.Text.Trim();
            string local_db = this.txt_localdb.Text.Trim();

            string dbname = livedb;

            if (db_list.SelectedIndex == 0)
            {
                dbname = livedb;
            }
            else
            {
                dbname = test_db;
            }

            LogUtill.loadDBConfig(servername, uname, password, livedb, backup, lbl_conn.Text, test_db, dbname, local_db, 0);
            DatabaseLog db = LogUtill.getDatabaseLog();
            lbl_conn.Text = db.connectionName;
            RegistryAccess.SetStringRegistryValue("DBStatus", db.Status);
            RegistryAccess.SetStringRegistryValue("DBString", db.connectionName);
        }
        Account acc;
        private async void btn_accountActiviate_Click(object sender, EventArgs e)
        {
            /*if (!CommonUtill.CheckForInternetConnection())
            {
                MessageBox.Show("Internet Not Available....");
                return;
            }
            
            string registration_no = txt_registration_no.Text;
            if (string.IsNullOrEmpty(registration_no))
            {
                MessageBox.Show("Please Enter Your Registration Key....");
            }
            else
            {
                FirebaseApi api = new FirebaseApi();
                string email = await api.GetEmailFromUserId(registration_no);

                Account account = new BLogic().accountActivationAdd(registration_no,panel_config.Enabled);
                if (account != null)
                {
                    acc = account;
                    RegistryAccess.SetStringRegistryValue(Const.REGKEY, registration_no);
                    MessageBox.Show(account.username + " Account Activated....Please Login ");
                    panel_config.Enabled = true;
                    txt_servername.Focus();
                }
                else
                {
                    MessageBox.Show("Registration Key Not Valid. Please Add Valid Registration Key....");
                }
            }*/

            try
            {
                string rootFolderName = "ArthiApp";
                string configFolderName = "Config";
                string fileName = txt_registration_no.Text;
                string saveTo = "config.json";
                DriveService service = await GoogleDriveHelper.GetServiceAsync();

                if (service == null)
                    MessageBox.Show("Fail");
                // Check if root folder exists
                string rootFolderId = await GoogleDriveHelper.GetFolderIdByName(service, rootFolderName);
                if (rootFolderId == null)
                {
                    // Create root folder
                    rootFolderId = await GoogleDriveHelper.CreateFolder(service, rootFolderName);
                }

                // Check if Backup folder exists
                string configFolderId = await GoogleDriveHelper.GetFolderIdByName(service, configFolderName, rootFolderId);
                if (configFolderId == null)
                {
                    // Create Backup folder
                    configFolderId = await GoogleDriveHelper.CreateFolder(service, configFolderName, rootFolderId);
                }


                if (configFolderId != null)
                {
                    List<string> ls = await GoogleDriveHelper.ListFilesInFolder(service, configFolderId);
                    // Check if config.json file exists in Config folder
                    string fileId = await GoogleDriveHelper.GetFileIdByName(service, fileName, configFolderId);
                    if (fileId == null)
                    {
                        MessageBox.Show($"File '{fileName}' not found in folder '{configFolderName}'.");
                        return;
                    }
                    if (fileId != null)
                    {
                        await GoogleDriveHelper.DownloadFile(service, fileId, saveTo);

                        // Load the configuration data
                        Config config = ConfigHelper.LoadConfig(saveTo);
                        //txt_uname.Text = config.user_name;
                        //txt_password.Text = config.password;
                        txt_livedb.Text = config.livedb;
                        txt_localdb.Text = config.localdb;
                        txt_backupdb.Text = config.backupdb;
                        txt_testingdb.Text = config.testdb;
                        panel_config.Enabled = true;


                        DataMember.Account acc = new DataMember.Account()
                        {
                            username = config.user_name,
                            password = config.password,
                            address = config.address,
                            shop_name = config.shop_name,
                            propriters_name = config.propriters_name,
                            email = config.email,
                            trade_mark = config.trade_mark,
                            license_exp_date = config.license_exp,
                            license_no = txt_registration_no.Text,
                            name1 = config.name1,
                            phone1 = config.phone1,
                            api_key_exp_date = config.registrationkey_exp,
                            api_key = txt_registration_no.Text,
                            business_type = config.business_type,
                            local = "1",
                            web_id=config.web_id

                        };
                        ProfileEdit pe = new ProfileEdit(acc);
                        pe.ShowDialog();
                        string registration_no = txt_registration_no.Text;

                        if (acc != null)
                        {
                            RegistryAccess.SetStringRegistryValue(Const.REGKEY, registration_no);
                            MessageBox.Show(acc.username + " Account Activated....Please Login ");
                            panel_config.Enabled = true;
                            txt_servername.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Registration Key Not Valid. Please Add Valid Registration Key....");
                        }

                    }
                    else
                    {
                        MessageBox.Show($"File {fileName} not found in folder {configFolderName}");
                    }
                }
                else
                {
                    MessageBox.Show($"Folder {configFolderName} not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private async void firecl(string registration_no)
        {
            string apiKey = "AIzaSyAE2FNiDKDemUQGWLaBZfhCvKAtsgxm8gU";
        }

        private void db_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string conName = this.lbl_conn.Text.Trim();
            string servername = txt_servername.Text.Trim();
            string uname = this.txt_uname.Text.Trim();
            string password = this.txt_password.Text.Trim();
            string livedb = this.txt_livedb.Text.Trim();
            string backup = this.txt_backupdb.Text.Trim();
            string test_db = this.txt_testingdb.Text.Trim();
            string local_db = this.txt_localdb.Text.Trim();
            int localCheck = 0;
            string dbname = livedb;
            if (db_list.SelectedIndex == 0)
            {
                dbname = livedb;
                RegistryAccess.SetStringRegistryValue("db", "Live");
            }
            else if (db_list.SelectedIndex == 1)
            {
                dbname = test_db;
                RegistryAccess.SetStringRegistryValue("db", "Test");

            }
            else if (db_list.SelectedIndex == 2)
            {
                dbname = "Local";
                localCheck = 1;
                RegistryAccess.SetStringRegistryValue("db", "Local");


            }



            LogUtill.loadDBConfig(servername, uname, password, livedb, backup, lbl_conn.Text, test_db, dbname, local_db, localCheck);
            DatabaseLog db = LogUtill.getDatabaseLog();
            lbl_conn.Text = db.connectionName;
            RegistryAccess.SetStringRegistryValue("DBStatus", db.Status);
            RegistryAccess.SetStringRegistryValue("DBString", db.connectionName);
        }
    }
}
