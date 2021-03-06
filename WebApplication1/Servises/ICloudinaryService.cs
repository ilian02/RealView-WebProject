using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebSite.Servises
{
    public interface ICloudinaryService
    {
        Task<string> UploadImage(IFormFile formFile);
    }
}
