using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using eCommerceProject.Data;
using eCommerceProject.Model;
using eCommerceProject.Model.Category;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Add(Category category)
        {
            _context.Categories.Add(category);
            
            return _context.SaveChanges();

        }

        public int Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null)
            {
                throw new Exception("Category not found");
            }
            _context.Remove(category);
            return _context.SaveChanges();
        }

        public void DeleteAll()
        {
            var categories = _context.Categories.ToList();
            if (!categories.Any())
            {
                throw new Exception("No categories found to delete");
            }
            _context.RemoveRange(categories);
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll(bool withTracking = false)
        {
            var query = _context.Categories
     .Include(c => c.CategoryTranslations)
     .OrderByDescending(c => c.CreatedAt);

            return withTracking ? query.ToList() : query.AsNoTracking().ToList();

        }

        public IEnumerable<Category> GetAllByLang(string lang = "en")
        {
            return _context.Categories
         .Include(c => c.CategoryTranslations)
         .Where(c => c.Status == Status.Active)
         .OrderByDescending(c => c.CreatedAt)
         .ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories
       .Include(c => c.CategoryTranslations)
       .FirstOrDefault(c => c.Id == id);
        }

       
        public bool ToggleStatus(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null)
            {
                return false;
            }
            category.Status = category.Status == Status.Active ? Status.In_active : Status.Active;
            _context.SaveChanges();
            return true;
        }

      

        public int Update(int id, List<CategoryTranslation> translations)
        {
            var category = _context.Categories.Include(c => c.CategoryTranslations).FirstOrDefault(c => c.Id == id);
            if (category == null)
                throw new Exception("Category not found");

            foreach (var trans in translations)
            {
                var existing = category.CategoryTranslations.FirstOrDefault(t => t.Language == trans.Language);
                if (existing != null)
                {
                    existing.Name = trans.Name;
                }
                else
                {
                    category.CategoryTranslations.Add(new CategoryTranslation
                    {
                        Name = trans.Name,
                        Language = trans.Language,
                        CategoryId = category.Id
                    });
                }
            }

            return _context.SaveChanges();
        }
    }

}
