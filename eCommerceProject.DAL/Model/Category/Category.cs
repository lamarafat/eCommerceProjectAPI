using eCommerceProject.Model.Category;

namespace eCommerceProject.Model.Category
{
    
    public class Category : BaseModel
    {
        public int Id { get; set; }
        public List<CategoryTranslation> CategoryTranslations { get; set; } 

    }
}
