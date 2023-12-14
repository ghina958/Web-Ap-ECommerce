using JobTaskProject.Models;

namespace JobTaskProject.Interface
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProductById(int id);       
        bool ProductExists(int productId);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool Save();
    }
}
