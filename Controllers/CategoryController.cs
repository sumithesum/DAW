using AutoMapper;
using Daw.DTO;
using Daw.Interfaces;
using Daw.Repository;
using DAW.Interfaces;
using DAW.Modells;
using Microsoft.AspNetCore.Mvc;

namespace Daw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryInterface _categoryInterface;
        private readonly IMapper _mapper;
        public CategoryController(CategoryInterface categoryInterface, IMapper mapper) {
        
            _categoryInterface = categoryInterface;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetGategories()
        {


            var categories = _mapper.Map<List<CategoryDto>>(_categoryInterface.GetCategories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(categories);
        }

    }
}
