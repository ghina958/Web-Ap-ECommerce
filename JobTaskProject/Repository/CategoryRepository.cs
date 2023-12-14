using FirstWebAPI.Data;
using JobTaskProject.Interface;
using JobTaskProject.Models;

namespace JobTaskProject.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        #region
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

       
        #endregion
        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(p => p.Id).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(p => p.Id == categoryId);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public Category GetProductByCategory(int productId)
        {
            return _context.Products.Where(p => p.Id == productId).Select(c => c.Category).FirstOrDefault();
        }
    }
}
