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
                return BadRequest("Product is null.");
            }

            var category = _mapper.Map<Category>(categoryPost);
            _categoryRepository.Add(category);
            return CreatedAtRoute(
                  "Get",
                  new { Id = category.Id },
                  categoryPost);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductPost productPost)
        {
            if (productPost == null)
            {
                return BadRequest("Productloyee is null.");
            }

            Product productToUpdate = _productRepository.FindOne(x => x.Id == id);
            if (productToUpdate == null)
            {
                return NotFound("The Productloyee record couldn't be found.");
            }

            productToUpdate.CategoryId = productPost.CategoryId;
            productToUpdate.Code = productPost.Code;
            productToUpdate.NameEN = productPost.NameEN;
            productToUpdate.NameAR = productPost.NameAR;

            _productRepository.Update(productToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _productRepository.FindOne(x => x.Id == id);
            if (product == null)
            {
                return NotFound("The Productloyee record couldn't be found.");
            }
            _productRepository.Delete(product);
            return NoContent();
        }
    }
}
