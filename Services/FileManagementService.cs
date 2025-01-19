using Microsoft.Extensions.Logging;
using System.IO;

namespace SystemPanel.Services
{
    public class FileManagementService
    {
        private readonly string _fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
        private readonly ILogger<FileManagementService> _logger;

        public FileManagementService(ILogger<FileManagementService> logger)
        {
            _logger = logger;
        }

        // Dosyaları listeleme
        public List<FileModel> GetAllFiles()
        {
            var files = new List<FileModel>();

            if (!Directory.Exists(_fileDirectory))
            {
                Directory.CreateDirectory(_fileDirectory);
            }

            foreach (var filePath in Directory.GetFiles(_fileDirectory))
            {
                var fileInfo = new FileInfo(filePath);
                files.Add(new FileModel
                {
                    FileName = fileInfo.Name,
                    FileSize = fileInfo.Length,
                    FilePath = filePath
                });
            }

            return files;
        }

        // Dosya yükleme
        public bool UploadFile(IFormFile file)
        {
            try
            {
                var uniqueFileName = Path.Combine(_fileDirectory, file.FileName);
                using (var stream = new FileStream(uniqueFileName, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file {FileName}.", file.FileName);
                return false;
            }
        }

        // Dosya silme
        public bool DeleteFile(string fileName)
        {
            try
            {
                var filePath = Path.Combine(_fileDirectory, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file {FileName}.", fileName);
                return false;
            }
        }
    }
}
