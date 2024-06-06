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
        public async Task<ActionResult<Category>> GetAll()
        {
            List<Category> category = await _categoriesService.getCategories();
            if (category != null)
            {
                List<CategoryDTO> categoryDTO = _mapper.Map<List<Category>, List<CategoryDTO>>(category);
                return Ok(categoryDTO);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            Category category = await _categoriesService.getCategoryById(id);
            if (category != null)
            {
                CategoryDTO categoryDTO = _mapper.Map<Category, CategoryDTO>(category);
                return Ok(categoryDTO);
            }
            return NotFound();
        }

/*        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult<Category>> Update(int id, [FromBody] Category newcategory)
        {

            Category c = await _categoriesService.updateCategory(id, newcategory);
            CategoryDTO categoryDTO = _mapper.Map<Category, CategoryDTO>(c);
            return Ok(categoryDTO);
        }*/
    }
}
