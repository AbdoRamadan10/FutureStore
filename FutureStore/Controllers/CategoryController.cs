//using FutureStore.Interfaces;
//using FutureStore.Models;
//using Microsoft.AspNetCore.DataProtection.Repositories;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace FutureStore.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoryController : ControllerBase
//    {
//        private readonly ICategoryRepository _categoryRepository;
//        public CategoryController(ICategoryRepository categoryRepository)
//        {
//            _categoryRepository = categoryRepository;
//        }
//        [HttpGet]
//        public IActionResult Get()
//        {
//            IEnumerable<Category > categories = _categoryRepository.GetAll();
//            return Ok(categories);
//        }
//        [HttpGet("{id}", Name = "Get")]
//        public IActionResult Get(int id)
//        {
//            Category  category = _categoryRepository.FindOne(x => x.Id == id);
//            if (category == null)
//            {
//                return NotFound("The Category record couldn't be found.");
//            }
//            return Ok(category);
//        }
//        [HttpPost]
//        public IActionResult Post([FromBody] Category  category)
//        {
//            if (category == null)
//            {
//                return BadRequest("Category is null.");
//            }

            
//            _categoryRepository.Add(category);
//            return CreatedAtRoute(
//                  "Get",
//                  new { Id = category.Id },
//                  category);
//        }

//        [HttpPut("{id}")]
//        public IActionResult Put(int id, [FromBody] Category  category)
//        {
//            if (category == null)
//            {
//                return BadRequest("Category is null.");
//            }
//            Category  categoryToUpdate = _categoryRepository.FindOne(x => x.Id == id);
//            if (categoryToUpdate == null)
//            {
//                return NotFound("The Category record couldn't be found.");
//            }

            
//            categoryToUpdate.NameEN = category.NameEN;
//            categoryToUpdate.NameAR = category.NameAR;
//            categoryToUpdate.Description = category.Description;
            

//            _categoryRepository.Update(categoryToUpdate);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public IActionResult Delete(int id)
//        {
//            Category  employee = _categoryRepository.FindOne(x => x.Id == id);
//            if (employee == null)
//            {
//                return NotFound("The Category record couldn't be found.");
//            }
//            _categoryRepository.Delete(employee);
//            return NoContent();
//        }
//    }
//}
