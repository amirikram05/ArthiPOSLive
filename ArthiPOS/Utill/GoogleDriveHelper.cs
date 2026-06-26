using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ArthiPOS.Controls.test
{
    public class GoogleDriveHelper
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "ArthiAPP";

        public static void DeleteTokenFile()
        {
            string tokenPath = "token.json";

            if (File.Exists(tokenPath))
            {
                File.Delete(tokenPath);
                Console.WriteLine("Token file deleted. User will need to re-authenticate.");
            }
            else
            {
                FileDataStore f = new FileDataStore(tokenPath, true);
                f.ClearAsync();
                Console.WriteLine("Token file not found.");
            }
        }
        public static async Task<DriveService> GetServiceAsync()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                FileDataStore file = new FileDataStore(credPath, true);
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true));

                Console.WriteLine("Credential file saved to: " + credPath);
            }
            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        public static async Task<List<string>> ListFilesInFolder(DriveService service, string folderId)
        {
            string query = $"'{folderId}' in parents and trashed=false";

            var request = service.Files.List();
            request.Q = query;
            request.Fields = "files(id, name)";
            var result = await request.ExecuteAsync();

            // Extract file names and IDs
            var files = result.Files.Select(f => $"ID: {f.Id}, Name: {f.Name}").ToList();

            return files;
        }
        public static async Task<string> CreateFolder(DriveService service, string folderName, string parentFolderId = "root")
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string> { parentFolderId }
            };

            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = await request.ExecuteAsync();

            return file.Id;
        }
        public static async Task UploadFile(DriveService service, string folderId, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please select a file.");
                return;
            }
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath),
                Parents = new List<string> { folderId }
            };

            FilesResource.CreateMediaUpload request;
            string mimtype = "";
            if (Path.GetExtension(filePath).Equals(".bak", StringComparison.OrdinalIgnoreCase))
            {
                mimtype = "application/octet-stream";
            }
            else
            {
                mimtype = GetMimeType(filePath);
            }

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, mimtype);
                request.Fields = "id";
                await request.UploadAsync();
            }

            var file = request.ResponseBody;
            Console.WriteLine($"File ID: {file.Id}");
        }
        private static string GetMimeType(string fileName)
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

        public static async Task<string> GetFileIdFromRoot(DriveService service, string fileName)
        {
            string query = $"name='{fileName}' and 'root' in parents and trashed=false";

            var request = service.Files.List();
            request.Q = query;
            request.Fields = "files(id, name)";
            var result = await request.ExecuteAsync();

            var file = result.Files.FirstOrDefault(f => f.Name == fileName);
            return file?.Id;
        }

        public static async Task<string> GetFolderIdByName(DriveService service, string folderName, string parentFolderId = "root")
        {
            string query = $"mimeType='application/vnd.google-apps.folder' and name='{folderName}' and '{parentFolderId}' in parents and trashed=false";

            var request = service.Files.List();
            request.Q = query;
            request.Fields = "files(id, name)";
            var result = await request.ExecuteAsync();

            if (result.Files.Count > 0)
            {
                return result.Files[0].Id;
            }

            return null;
        }

        public static async Task<string> GetFileIdByName(DriveService service, string fileName, string parentFolderId)
        {
            string query = $"name='{fileName}' and '{parentFolderId}' in parents and trashed=false";

            var request = service.Files.List();
            request.Q = query;
            request.Fields = "files(id, name)";
            var result = await request.ExecuteAsync();

            if (result.Files.Count > 0)
            {
                return result.Files[0].Id;
            }

            return null;
        }



        public static async Task<List<string>> ListAllFolders(DriveService service)
        {
            string query = "mimeType='application/vnd.google-apps.folder' and trashed=false";

            var request = service.Files.List();
            request.Q = query;
            request.Fields = "files(id, name)";
            var result = await request.ExecuteAsync();

            // Extract folder names and IDs
            var folders = result.Files.Select(f => $"ID: {f.Id}, Name: {f.Name}").ToList();

            return folders;
        }

        public static string GetFileIdInFolder(DriveService service, string folderId, string fileName)
        {
            var request = service.Files.List();
            request.Q = $"'{folderId}' in parents and name='{fileName}' and trashed=false";
            request.Fields = "files(id, name)";
            var result = request.Execute();
            var file = result.Files.FirstOrDefault(f => f.Name == fileName);
            return file?.Id;
        }

        public async static Task DownloadFile(DriveService service, string fileId, string saveTo)
        {
            var request = service.Files.Get(fileId);
            var stream = new MemoryStream();

            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case Google.Apis.Download.DownloadStatus.Downloading:
                        Console.WriteLine(progress.BytesDownloaded);
                        break;
                    case Google.Apis.Download.DownloadStatus.Completed:
                        Console.WriteLine("Download complete.");
                        SaveStream(stream, saveTo);
                        break;
                    case Google.Apis.Download.DownloadStatus.Failed:
                        Console.WriteLine("Download failed.");
                        break;
                }
            };
            request.Download(stream);
        }

        private static void SaveStream(MemoryStream stream, string saveTo)
        {
            using (FileStream file = new FileStream(saveTo, FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(file);
            }
        }
    }

}
