using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace FinovationTrader.Application.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(IFormFile formFile);

        Task RemoveFileAsync(string filePath);

        FormFile DownloadFile(string filePath);
    }
}
