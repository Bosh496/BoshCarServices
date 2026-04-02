using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BoshCarServices.Services
{
    public class GoogleDriveService
    {
        private DriveService _driveService;
        private readonly object _lock = new object();
        private bool _isInitialized = false;
        private string _errorMessage = null;

        // Lazy initialization of DriveService
        private DriveService GetDriveService()
        {
            if (_driveService != null)
                return _driveService;

            lock (_lock)
            {
                if (_driveService != null)
                    return _driveService;

                try
                {
                    // Try multiple paths to find the credential file
                    string credentialPath = FindCredentialFile();

                    if (string.IsNullOrEmpty(credentialPath))
                    {
                        throw new FileNotFoundException("Could not find google-drive-key.json or credential.json in the project");
                    }

                    GoogleCredential credential;
                    using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
                    {
                        credential = GoogleCredential.FromStream(stream)
                            .CreateScoped(DriveService.ScopeConstants.DriveFile);
                    }

                    _driveService = new DriveService(new BaseClientService.Initializer
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = "BoshCarServices"
                    });

                    _isInitialized = true;
                }
                catch (Exception ex)
                {
                    _errorMessage = ex.Message;
                    throw;
                }
            }

            return _driveService;
        }

        private string FindCredentialFile()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Try different possible filenames and locations
            string[] possiblePaths = new[]
            {
                // In output directory (bin)
                Path.Combine(baseDirectory, "google-drive-key.json"),
                Path.Combine(baseDirectory, "credential.json"),
                
                // In project root (2 levels up from bin)
                Path.Combine(Directory.GetParent(baseDirectory)?.Parent?.Parent?.FullName ?? baseDirectory, "google-drive-key.json"),
                Path.Combine(Directory.GetParent(baseDirectory)?.Parent?.Parent?.FullName ?? baseDirectory, "credential.json"),
                
                // One level up
                Path.Combine(Directory.GetParent(baseDirectory)?.FullName ?? baseDirectory, "google-drive-key.json"),
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return null;
        }

        // Method 1: Upload file to Google Drive and return file URL
        public async Task<string> UploadFileAndGetUrlAsync(Stream fileStream, string fileName, string mimeType)
        {
            try
            {
                var driveService = GetDriveService();

                if (fileStream.CanSeek)
                {
                    fileStream.Position = 0;
                }

                var fileMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = fileName
                };

                var request = driveService.Files.Create(fileMetadata, fileStream, mimeType);
                request.Fields = "id";

                var progress = await request.UploadAsync();

                if (progress.Status == UploadStatus.Completed)
                {
                    var fileId = request.ResponseBody.Id;

                    // Make file publicly accessible
                    var permission = new Google.Apis.Drive.v3.Data.Permission
                    {
                        Type = "anyone",
                        Role = "reader"
                    };
                    await driveService.Permissions.Create(permission, fileId).ExecuteAsync();

                    return $"https://drive.google.com/file/d/{fileId}/view";
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Upload failed: {ex.Message}");
            }
        }

        // Method 2: Get file URL by file ID
        public async Task<string> GetFileUrlAsync(string fileId)
        {
            try
            {
                var driveService = GetDriveService();
                var request = driveService.Files.Get(fileId);
                request.Fields = "id";
                await request.ExecuteAsync();

                return $"https://drive.google.com/file/d/{fileId}/view";
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get file URL: {ex.Message}");
            }
        }

        // Check if service is properly configured
        public bool IsConfigured()
        {
            try
            {
                GetDriveService();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}