using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.DTO.Response;
using eCommerceProject.DAL.Model.Brand;
using eCommerceProject.DAL.Model.Products;
using eCommerceProject.DAL.Repositories.Classes;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;
using eCommerceProject.Model.Category;
using Mapster;

namespace eCommerceProject.BLL.Service.Classes
{
    public class BrandService : GenericService<BrandRequest, BrandResponse, BrandAlt>, IBrandService

    {
        private readonly IBrandRepository _brandrepository;
        private readonly IFileService _fileService;

        public BrandService(IBrandRepository brandrepository, IFileService fileService) : base(brandrepository)
        {
            _brandrepository = brandrepository;
            _fileService = fileService;
        }

        public async Task<int> CreateFile(BrandRequest request)
        {
            var entity = request.Adapt<BrandAlt>();
            entity.CreatedAt = DateTime.UtcNow;
            if (request.ImageUrl is not null)
            {
                var imagePath = await _fileService.UploadAsync(request.ImageUrl);
                entity.ImageUrl = imagePath;
            }
            return _brandrepository.Add(entity);
        }
    }
}
