namespace BoshCarServices.IServices
{
    public interface IGoogleDriveService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string mimeType);
        Task<bool> DeleteFileAsync(string fileId);
        Task<string> GetFileUrlAsync(string fileId);
        Task<Google.Apis.Drive.v3.Data.File> GetFileMetadataAsync(string fileId);
    }
}
