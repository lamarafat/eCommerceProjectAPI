using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.Model;
using eCommerceProject.Model.Category;

namespace eCommerceProject.DAL.Model.Brand
{
    public class Brand : BaseModel
    {
        public int Id { get; set; }
        public List<BrandTranslation> BrandTranslations { get; set; }
    }
}
