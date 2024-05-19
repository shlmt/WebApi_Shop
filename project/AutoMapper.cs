using AutoMapper;
using DTOs;
using Entities;

namespace project
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();

        }
    }
}
