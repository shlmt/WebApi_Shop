using Entities;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
    }
}