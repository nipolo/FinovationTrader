using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;

namespace FinovationTrader.Application.Services
{
    public class FileStorageService : IFileStorageService
    {
        private string _fileStorageLocation;

        public FileStorageService(IConfiguration configuration)
        {
            _fileStorageLocation = configuration.GetSection("FileStorageLocation").Value;

            if (!Directory.Exists(_fileStorageLocation))
            {
                Directory.CreateDirectory(_fileStorageLocation);
            }
        }

        public FormFile DownloadFile(string filePath)
        {
            using var stream = new MemoryStream(File.ReadAllBytes(filePath).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", filePath.Split('/').Last());

            return formFile;
        }

        public async Task RemoveFileAsync(string filePath)
        {
            await Task.Run(() => File.Delete(filePath));
        }

        public async Task<string> UploadFileAsync(IFormFile formFile)
        {
            var filePath = Path.Combine(_fileStorageLocation, Guid.NewGuid() + "_" + formFile.FileName).Replace(Path.DirectorySeparatorChar, '/');
            using var fileStream = File.Create(filePath);

            await formFile.CopyToAsync(fileStream);

            return filePath;
        }
    }
}
