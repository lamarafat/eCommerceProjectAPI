using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.DTO.Response;
using eCommerceProject.DAL.Model.Brand;
using eCommerceProject.DAL.Model.Products;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace eCommerceProject.BLL.Service.Interfaces
{
    public interface IProductService : IGenericService<ProductRequest, ProductResponse, Product>
    {
        Task<int> CreateFile(ProductRequest request);
        Task<List<ProductResponse>> GetAllProduct(HttpRequest request, bool onlyActive = false, int pageNumber = 1, int pageSize = 1);
    }
}
