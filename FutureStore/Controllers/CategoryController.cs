using AutoMapper;
using FutureStore.DTOs.Category;
using FutureStore.Interfaces;
using FutureStore.Models;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutureStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IProductRepository productRepository,
            IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Category> categories = _categoryRepository.GetAll().Include(c=>c.Products);
            var categoriesDtos = categories.Select(c => _mapper.Map<CategoryGet>(c));
            return Ok(categoriesDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = _categoryRepository.FindWithInclude(c => c.Id == id,c=>c.Products);
            if (category == null)
            {
                return NotFound("The Category record couldn't be found.");
            }
            var categoryDto = _mapper.Map<CategoryGet>(category);
            return Ok(categoryDto);
        }
        [HttpPost]
        public IActionResult Post([FromBody] CategoryPost categoryPost)
        {

            if (categoryPost == null)
            {
                return BadRequest("Category is null.");
            }

            var category = _mapper.Map<Category>(categoryPost);
            _categoryRepository.Add(category);
            return CreatedAtAction(
                  nameof(Get),
                  new { Id = category.Id },
                  _mapper.Map <CategoryGet>(category));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryPut categoryPut)
        {
            if (categoryPut == null)
            {
                return BadRequest("Category is null.");
            }

            Category categoryToUpdate = _categoryRepository.FindOne(x => x.Id == id);
            if (categoryToUpdate == null)
            {
                return NotFound("The Category record couldn't be found.");
            }

            categoryToUpdate.NameEN = categoryPut.NameEN;
            categoryToUpdate.NameAR = categoryPut.NameAR;
            categoryToUpdate.Active = categoryPut.Active;
            categoryToUpdate.UpdatedTimeStamp=DateTime.Now;
            categoryToUpdate.Description = categoryPut.Description;
            
            _categoryRepository.Update(categoryToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = _categoryRepository.FindOne(x => x.Id == id);
            if (category == null)
            {
                return NotFound("The Category record couldn't be found.");
            }
            _categoryRepository.Delete(category);
            return NoContent();
        }
    }
}
