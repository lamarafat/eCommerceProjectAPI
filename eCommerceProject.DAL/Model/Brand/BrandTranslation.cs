

namespace eCommerceProject.DAL.Model.Brand
{
    public class BrandTranslation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; } = "en";
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
