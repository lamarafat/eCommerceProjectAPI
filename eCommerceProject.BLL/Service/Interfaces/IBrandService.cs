using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.DTO.Response;
using eCommerceProject.DAL.Model.Brand;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;

namespace eCommerceProject.BLL.Service.Interfaces
{
    public interface IBrandService : IGenericService<BrandRequest, BrandResponse, BrandAlt>
    {
        Task<int> CreateFile(BrandRequest request);
    }
}
