using eCommerceProject.Model.Category;

namespace eCommerceProject.DAL.Repositories
{
    public interface ICategoryRepository
    {
        int Add(Category category);
        //void Update(Category category);
        int Delete(int id);
        void DeleteAll();
        //void SaveChanges();
        IEnumerable<Category> GetAll(bool withTracking = false);
        IEnumerable<Category> GetAllByLang(string lang = "en");

        Category GetById(int id);
        int Update (int id, List<CategoryTranslation> translations);
        bool ToggleStatus(int id);
        
    }
}