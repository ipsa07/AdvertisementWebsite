using System;
using System.IO;
using System.Threading.Tasks;

namespace AdvertiseWebSite.Services
{
    public interface IS3FileUploadService
    {
      Task<bool> UploadFileAsync(string fileName, Stream storageStream);
    }
}
