using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace project.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoriesService _categoriesService;
        private IMapper _mapper;

        public CategoriesController(ICategoriesService categoriesService, IMapper mapper)
        {
            this._categoriesService = categoriesService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAll()
        {
            List<Category> category = await _categoriesService.getCategories();
            if (category != null)
            {
                List<CategoryDTO> categoryDTOs = _mapper.Map<List<Category>, List<CategoryDTO>>(category);
                return Ok(categoryDTOs);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            Category category = await _categoriesService.getCategoryById(id);
            if (category != null)
            {
                CategoryDTO categoryDTO = _mapper.Map<Category, CategoryDTO>(category);
                return Ok(categoryDTO);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] string categoryName)
        {
            if (categoryName == null || categoryName=="")
                return BadRequest();
            Category category = await _categoriesService.createCategory(categoryName);
            CategoryDTO newCategory = _mapper.Map<Category, CategoryDTO>(category);
            if (newCategory != null)
                return Ok(newCategory);
            return StatusCode(500);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<CategoryDTO>> Update(int id, [FromBody] string categoryName)
        {
            if(categoryName == null || categoryName=="")
                return BadRequest();
            Category category = await _categoriesService.updateCategory(id, categoryName);
            if (category == null)
                return NotFound();
            CategoryDTO categoryDTO = _mapper.Map<Category, CategoryDTO>(category);
            if (categoryDTO != null)
                return Ok(categoryDTO);
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Category category = await _categoriesService.getCategoryById(id);
            if(category == null)
                return NotFound();
            bool isDeleted = await _categoriesService.deleteCategory(id);
            if (isDeleted)
                return NoContent();
            return BadRequest();
        }

    }
}
