using AutoMapper;
using FirstWebAPI.Data;
using JobTaskProject.Dto;
using JobTaskProject.Interface;
using JobTaskProject.Models;
using JobTaskProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JobTaskProject.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        #region

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public ProductController(IProductRepository productRepository,
             ICategoryRepository categoryRepository
            ,IMapper mapper,
             DataContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _context = context;
           
        }

        #endregion

        [HttpGet]
        [ProducesResponseType(200,Type= typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
            
            var product = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpGet]
        public IActionResult Search(string title)
        {
            try
            {
                var product = _context.Products.Where(p => p.Title == title).FirstOrDefault();
                if (product == null)
                {
                    return NotFound("product not found with this title");
                }
                return Ok(product);
            }catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        
        }


        [HttpGet("{productId}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        public IActionResult GetProduct(int productId)
        {
            if(!_productRepository.ProductExists(productId))           
            {
                return NotFound();
            }
            var product = _mapper.Map <ProductDto>(_productRepository.GetProductById(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpGet("{productId}")]
        public IActionResult Category(int productId)
        {
            var productCategory = _categoryRepository.GetProductByCategory(productId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productCategory);

        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromQuery] int categoryId, [FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            var products = _productRepository.GetProducts()
                .Where(c => c.Title.Trim().ToUpper() == productCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (products != null)
            {
                ModelState.AddModelError("", "product already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);
            productMap.Category = _categoryRepository.GetCategoryById(categoryId);

            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);

            }
            return Ok("Successfully created");
        }

        [HttpPut] 
        public IActionResult Update(Product model)
        {
            if (model == null || model.Id == 0)
            {
                if (model == null) return BadRequest("model data is valid");
                else if (model.Id == 0) return BadRequest($"product Id {model.Id} is Invalid");

            }
            try
            {
                var product = _context.Products.Find(model.Id);
                if (product == null) return NotFound($"product not found with this {model.Id} ");
                product.Title = model.Title;
                product.Description = model.Description;
                product.Brand = model.Brand;
                product.Price = model.Price;          
                product.Rating = model.Rating;
                _context.SaveChanges();
                return Ok("product data is updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null) return NotFound($"product not found with this {id}");

                _context.Remove(product);
                _context.SaveChanges();
                return Ok("product detail is deleted");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }






    }
}
