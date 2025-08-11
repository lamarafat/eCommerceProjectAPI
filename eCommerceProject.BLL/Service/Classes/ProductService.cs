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
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;
using eCommerceProject.Model.Category;
using Mapster;

namespace eCommerceProject.BLL.Service.Classes
{
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService

    {
        private readonly IProductRepository _productrepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productrepository, IFileService fileService) : base(productrepository)
        {
            _productrepository = productrepository;
            _fileService = fileService;
        }

        public async Task<int> CreateFile(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            entity.CreatedAt = DateTime.UtcNow;
            if (request.ImageUrl is not null)
            {
                var imagePath = await _fileService.UploadAsync(request.ImageUrl);
                entity.ImageUrl = imagePath;
            }
            return _productrepository.Add(entity);
        }
    }
}
