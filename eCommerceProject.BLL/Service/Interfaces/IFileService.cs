using Microsoft.AspNetCore.Http;

namespace eCommerceProject.BLL.Service.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file);
        Task<List<string>> UplodeManyAsync(List<IFormFile> files);
    }
}