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
        private ILogger<ProductsController> _logger;
        public ProductsController(IProductService productService, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get([FromQuery]float minPrice, [FromQuery]float maxPrice, [FromQuery]int[] category, [FromQuery]string? description)
        {
            List<Product> products = await _productService.GetALlProducts(minPrice, maxPrice, category, description);
            List<ProductDTO> productsDTO = _mapper.Map<List<Product>, List<ProductDTO>>(products);
            _logger.LogInformation($"GetAllProducts -> {minPrice} {maxPrice} {category} {description}\n products:{products}");
            if (productsDTO == null)
                return NoContent();
            return Ok(productsDTO);
        }
    }
}
