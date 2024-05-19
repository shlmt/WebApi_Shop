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
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;
        private IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody] OrderDTO order)
        {
            Order orderToAdd = _mapper.Map<OrderDTO, Order>(order);
            Order theAddOrder = await _orderService.CreateOrder(orderToAdd);
            OrderDTO newAddOrder = _mapper.Map<Order, OrderDTO>(theAddOrder);
            if (newAddOrder != null)
                return Ok(newAddOrder);
            return BadRequest();
        }
    }
}
