using AutoMapper;
using FutureStore.DTOs.Category;
using FutureStore.Interfaces;
using FutureStore.Models;
using FutureStore.Services;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FutureStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        //private readonly ICategoryRepository _categoryRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Product> _productRepository;

        public ProductController(IProductRepository productRepository,
            IMapper mapper,ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products = _productRepository.GetAll();
            var productsDtos = products.Select(p => _mapper.Map<ProductGet>(p));
            foreach (var productDto in productsDtos)
            {
                productDto.CategoryNameAR = _categoryRepository.FindOne(c => c.Id == productDto.CategoryId).NameAR;
                //productDto.CategoryNameEN = _categoryRepository.FindOne(c => c.Id == productDto.CategoryId).NameEN;

            }
            return Ok(productsDtos);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _productRepository.FindOne(x => x.Id == id);
            if (product == null)
            {
                return NotFound("The Product record couldn't be found.");
            }
            var productDto = _mapper.Map<ProductGet>(product);
            productDto.CategoryNameAR = _categoryRepository.FindOne(c => c.Id == productDto.CategoryId).NameAR;
            productDto.CategoryNameEN = _categoryRepository.FindOne(c => c.Id == productDto.CategoryId).NameEN;
            return Ok(productDto);
        }
        [HttpPost]
        public IActionResult Post([FromBody] ProductPost productPost)
        {
            var categoryModel = _categoryRepository.FindOne(c=>c.Id == productPost.CategoryId);
            if (categoryModel == null)
            {
                return BadRequest("Category is null.");
            }
            if (productPost == null)
            {
                return BadRequest("Product is null.");
            }

            var product = _mapper.Map<Product>(productPost);
            _productRepository.Add(product);
            return CreatedAtAction(
                  nameof(Get),
                  new { Id = product.Id },
                  _mapper.Map<ProductGet>(product));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductPut productPut)
        {
            if (productPut == null)
            {
                return BadRequest("Productloyee is null.");
            }

            Product productToUpdate = _productRepository.FindOne(x => x.Id == id);
            if (productToUpdate == null)
            {
                return NotFound("The Productloyee record couldn't be found.");
            }

            productToUpdate.CategoryId = productPut.CategoryId;
            productToUpdate.Code = productPut.Code;
            productToUpdate.NameEN = productPut.NameEN;
            productToUpdate.NameAR = productPut.NameAR;
            productToUpdate.Active =productPut.Active;
            productToUpdate.UpdatedTimeStamp=DateTime.Now;
            productToUpdate.Description = productPut.Description;


            _productRepository.Update(productToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _productRepository.FindOne(x => x.Id == id);
            if (product == null)
            {
                return NotFound("The Product record couldn't be found.");
            }
            _productRepository.Delete(product);
            return NoContent();
        }
    }
}
