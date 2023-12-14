using FirstWebAPI.Data;
using JobTaskProject.Dto;
using JobTaskProject.Interface;
using JobTaskProject.Models;
using JobTaskProject.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTaskProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly DataContext _dataContext;
        public CategoryController(ICategoryRepository categoryRepository, DataContext context) 
        {
        _categoryRepository = categoryRepository;
            _dataContext = context; 
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var category = _categoryRepository.GetCategories();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var category = _categoryRepository.GetCategoryById(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpPost]   
        public IActionResult Create(Category model)
        {
            try
            {
                _dataContext.Add(model);
                _dataContext.SaveChanges();
               return Ok("category created");
               
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Edit(Category model)
        {
            if(model ==null|| model.Id==0 )
            {
                if (model == null) return BadRequest("model data is valid");
                else if (model.Id == 0) return BadRequest($"category Id {model.Id} is Invalid");

            }
            try
            {
                var category = _dataContext.Categories.Find(model.Id);
                if (category == null) return NotFound($"category not found with this {model.Id} ");
                category.Title = model.Title;
                _dataContext.SaveChanges();
                return Ok("category data is updated");
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
                var category = _dataContext.Categories.Find(id);
                if (category == null) return NotFound($"category not found with this {id}");

                _dataContext.Remove(category);
                _dataContext.SaveChanges();
                return Ok("category detail is deleted");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
