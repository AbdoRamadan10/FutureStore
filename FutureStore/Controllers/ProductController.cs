using FutureStore.Interfaces;
using FutureStore.Models;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FutureStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Product> products = _productRepository.GetAll();
            return Ok(products);
        }
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            Product product = _productRepository.FindOne(x => x.Id == id);
            if (product == null)
            {
                return NotFound("The Product record couldn't be found.");
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }

            
            _productRepository.Add(product);
            return CreatedAtRoute(
                  "Get",
                  new { Id = product.Id },
                  product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Productloyee is null.");
            }
            Product productToUpdate = _productRepository.FindOne(x => x.Id == id);
            if (productToUpdate == null)
            {
                return NotFound("The Productloyee record couldn't be found.");
            }

            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.Code = product.Code;
            productToUpdate.NameEN = product.NameEN;
            productToUpdate.NameAR = product.NameAR;

            _productRepository.Update(productToUpdate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product employee = _productRepository.FindOne(x => x.Id == id);
            if (employee == null)
            {
                return NotFound("The Productloyee record couldn't be found.");
            }
            _productRepository.Delete(employee);
            return NoContent();
        }
    }
}
