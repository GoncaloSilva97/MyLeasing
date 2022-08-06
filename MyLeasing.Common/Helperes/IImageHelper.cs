
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyLeasing.Common.Helperes
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
