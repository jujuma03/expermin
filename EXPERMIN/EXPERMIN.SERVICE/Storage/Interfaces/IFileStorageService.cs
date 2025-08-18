using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERIVICE.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<(string Path, string Url)> SaveFileAsync(IFormFile file, string? subFolder = null);
        Task DeleteFileAsync(string path);
        Task<(string Path, string Url)> MoveFileAsync(string currentPath, string targetFolder, string fileName);
    }
}
