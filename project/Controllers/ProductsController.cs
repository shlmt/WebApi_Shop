using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get([FromQuery]float minPrice, [FromQuery]float maxPrice, [FromQuery]List<Category> category, [FromQuery]string? description)
        { 
            List<Product> products = await _productService.GetALlProducts(minPrice, maxPrice, category, description);
            List<ProductDTO> productsDTO = _mapper.Map<List<Product>, List<ProductDTO>>(products);
            if (productsDTO == null)
                return NoContent();
            return Ok(productsDTO);
        }
    }
}
