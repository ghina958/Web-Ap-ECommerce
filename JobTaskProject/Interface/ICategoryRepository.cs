using JobTaskProject.Models;

namespace JobTaskProject.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategoryById(int id);
        Category GetProductByCategory(int productId);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool CategoryExists(int categoryId);
        bool Save();
    }
}
