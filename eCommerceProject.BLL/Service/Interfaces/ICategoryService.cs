using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Azure;
using eCommerceProject.DTO.Request;
using eCommerceProject.DTO.Response;
using eCommerceProject.Model.Category;
using eCommerceProject.DAL.Model.Category;

namespace eCommerceProject.BLL.Service.Interfaces
{
    public interface ICategoryService : IGenericService<CategoryRequestDto, CategoryResponseDto, CategoryAlt>
    {
        
    }
}
