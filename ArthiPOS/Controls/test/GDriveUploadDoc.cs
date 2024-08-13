using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Upload;
using File = Google.Apis.Drive.v3.Data.File;
using System.Threading.Tasks;
using DataMember.memberlog;
using ArthiPOS.Utill;
using DataMember;
using System.Collections.Generic;

namespace ArthiPOS.Controls.test
{
    public partial class GDriveUploadDoc : Form
    {
        public GDriveUploadDoc()
        {
            InitializeComponent();
        }

        private AdminLog log;

        private async void buttonUpload_Click(object sender, EventArgs e)
        {
            string rootFolderName = "ArthiApp";
            string reportsFolderName = "Reports";
            string backupFolderName = "Backup";
            string configFolderName = "Config";
            DriveService service =await GoogleDriveHelper.GetServiceAsync();

            // Check if root folder exists
            string rootFolderId = await GoogleDriveHelper.GetFolderIdByName(service, rootFolderName);
            if (rootFolderId == null)
            {
                // Create root folder
                rootFolderId = await GoogleDriveHelper.CreateFolder(service, rootFolderName);
            }

            // Check if Reports folder exists
            string reportsFolderId = await GoogleDriveHelper.GetFolderIdByName(service, reportsFolderName, rootFolderId);
            if (reportsFolderId == null)
            {
                // Create Reports folder
                reportsFolderId = await GoogleDriveHelper.CreateFolder(service, reportsFolderName, rootFolderId);
            }

            // Check if Backup folder exists
            string backupFolderId = await GoogleDriveHelper.GetFolderIdByName(service, backupFolderName, rootFolderId);
            if (backupFolderId == null)
            {
                // Create Backup folder
                backupFolderId = await GoogleDriveHelper.CreateFolder(service, backupFolderName, rootFolderId);
            }


            // Check if Backup folder exists
            string configFolderId = await GoogleDriveHelper.GetFolderIdByName(service, configFolderName, rootFolderId);
            if (configFolderId == null)
            {
                // Create Backup folder
                configFolderId = await GoogleDriveHelper.CreateFolder(service, configFolderName, rootFolderId);
            }

            // Determine which folder to upload the file to
            string targetFolderId = Path.GetExtension(textBoxFilePath.Text).ToLower() == ".bak" ? backupFolderId : reportsFolderId;




            await UploadFileToDriveAsync(service,targetFolderId, textBoxFilePath.Text);
        }
        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }
            return mimeType;
        }
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxFilePath.Text = openFileDialog.FileName;
                }
            }
        }
        private void ClearUserCredentials()
        {
            string tokenPath = "token.json";
            if (System.IO.File.Exists(tokenPath))
            {
                System.IO.File.Delete(tokenPath);
            }
        }
        private async Task<string> GetOrCreateFolderId(DriveService service, string folderName)
        {
            // Define the query to search for the folder
            string query = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and trashed=false";

            // Perform the search
            var request = service.Files.List();
            request.Q = query;
            request.Fields = "files(id, name)";
            var result = await request.ExecuteAsync();

            // Check if the folder already exists
            if (result.Files.Count > 0)
            {
                // Return the ID of the existing folder
                return result.Files[0].Id;
            }

            // If the folder does not exist, create it
            var folderMetadata = new File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };

            var createRequest = service.Files.Create(folderMetadata);
            createRequest.Fields = "id";
            var folder = await createRequest.ExecuteAsync();

            // Return the ID of the newly created folder
            return folder.Id;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearUserCredentials();

        }


        private async Task UploadFileToDriveAsync(DriveService service, string folderId, string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Please select a file.");
                    return;
                }

                string date = DateTime.Now.ToString("yyyy-MM-dd"); // Format the date as needed
                string originalFileName = Path.GetFileNameWithoutExtension(filePath);
                string fileExtension = Path.GetExtension(filePath);
                string newFileName = $"{originalFileName}_{date}{fileExtension}";

                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = newFileName, // Set the new file name with date
                    Parents = new List<string> { folderId }
                };

                string mimtype = "";
                if (Path.GetExtension(filePath).Equals(".bak", StringComparison.OrdinalIgnoreCase))
                {
                    mimtype = "application/octet-stream";
                }
                else
                {
                    mimtype = GetMimeType(filePath);
                    fileMetadata = new Google.Apis.Drive.v3.Data.File()
                    {
                        Name = newFileName, // Set the new file name with date
                        Parents = new List<string> { folderId }
                    };
                }

                FilesResource.CreateMediaUpload request;
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    statusLabel.Text = "Preparing...";
                    long fileSize = stream.Length;
                    request = service.Files.Create(fileMetadata, stream, mimtype);
                    request.Fields = "id";
                    request.ProgressChanged += (progress) => Upload_ProgressChanged(progress, fileSize);
                    request.ResponseReceived += Upload_ResponseReceived;

                    try
                    {
                        //request.Upload();

                        await request.UploadAsync();
                    }
                    catch (Exception uploadEx)
                    {
                        MessageBox.Show("File upload failed: " + uploadEx.Message);
                        return;
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private async void UploadFile(DriveService service, string filePath, string mimeType,string result)
        {
           
            
            // Get or create the "Documents" folder ID
            string folderId = await GetOrCreateFolderId(service, result);

            var fileMetadata = new File()
            {
                Name = Path.GetFileName(filePath),
                Parents = new[] { folderId } // Specify the folder ID
            };

            if (service == null)
            {
                MessageBox.Show("Failed to create Google Drive service.");
                return;
            }
            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                statusLabel.Text = "Preparing...";
                long fileSize = stream.Length;
                request = service.Files.Create(fileMetadata, stream, mimeType);
                request.Fields = "id";
                request.ProgressChanged += (progress) => Upload_ProgressChanged(progress, fileSize);
                request.ResponseReceived += Upload_ResponseReceived;

                try
                {
                    //request.Upload();

                    await request.UploadAsync();
                }
                catch (Exception uploadEx)
                {
                    MessageBox.Show("File upload failed: " + uploadEx.Message);
                    return;
                }
            }

            /*var file = request.ResponseBody;
            if (file == null)
            {
                MessageBox.Show("File upload failed.");
            }
            else
            {
                MessageBox.Show("File ID: " + file.Id);
            }*/
        }

        private void Upload_ProgressChanged(IUploadProgress progress, long fileSize)
        {
            if (progress.Status == UploadStatus.Uploading)
            {
                Invoke(new Action(() =>
                {
                    statusLabel.Text = "Uploading...";
                    int progressPercentage = (int)(progress.BytesSent * 100 / fileSize);
                    lbl_prog.Text = progressPercentage+"";
                    progressBar.Value = progressPercentage;
                    statusLabel.Text = $"{progress.Status}: {progressPercentage}% ({progress.BytesSent / 1024} KB sent)";
                }));
            }
        }

        private void Upload_ResponseReceived(File file)
        {
            Invoke(new Action(() =>
            {
                progressBar.Value = 100;
                lbl_prog.Text = "100";
                statusLabel.Text = "Upload complete. File ID: " + file.Id;
            }));
        }


    }
}
