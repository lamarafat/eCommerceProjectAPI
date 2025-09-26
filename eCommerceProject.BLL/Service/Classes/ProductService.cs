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
using eCommerceProject.Model;
using eCommerceProject.Model.Category;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations;

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
            if(request.SubImages != null)
            {
                var subImagesPath = await _fileService.UplodeManyAsync(request.SubImages);
                entity.SubImages = subImagesPath.Select(img => new ProductImage {ImageName = img}).ToList();
            }
            return _productrepository.Add(entity);
        }
        public async Task<List<ProductResponse>> GetAllProduct (HttpRequest request, bool onlyActive = false, int pageNumber = 1, int pageSize = 1)
        {
            var products = await _productrepository.GetAllProductWithImage();
            if (onlyActive)
            {
                products = products.Where(p => p.Status == Status.Active).ToList();
            }
            var pageProduct = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return pageProduct.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                StockQuantity = p.StockQuantity,
                MainImageUrl = $"{request.Scheme}://{request.Host}{request.PathBase}/images/{p.ImageUrl}",
                SubImagesUrls = p.SubImages.Select(img => $"{request.Scheme}://{request.Host}{request.PathBase}/images/{img.ImageName}").ToList(),
                Reviews = p.Reviews.Select(r => new ReviewResponse
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    Rate = r.Rate,
                    FullName = r.User.FullName
                }).ToList(),

            }).ToList();
            
        }
    }
}
