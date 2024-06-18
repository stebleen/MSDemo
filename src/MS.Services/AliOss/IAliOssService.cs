using MS.Entities;
using MS.Entities.admin;
using MS.Models.ViewModel;
using MS.WebCore.Core;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MS.Services
{
    public interface IAliOssService
    {
        Task<string> UploadFileAsync(Stream fileStream, string objectName);
    }
}
