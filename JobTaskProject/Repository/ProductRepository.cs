using FirstWebAPI.Data;
using JobTaskProject.Interface;
using JobTaskProject.Models;
using JobTaskProject.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace JobTaskProject.Repository
{
    public class ProductRepository : IProductRepository
    {
        #region

        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        #endregion
        public ICollection<Product> GetProducts()
        {
            return _context.Products.Include(c => c.Category).ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Where(p => p.Id== id).FirstOrDefault(); 
        }


        public bool ProductExists(int productId)
        {
            return _context.Products.Any(p => p.Id == productId);
        }
        
        public bool CreateProduct(Product product)
        {
           _context.Add(product);
            return Save();
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
