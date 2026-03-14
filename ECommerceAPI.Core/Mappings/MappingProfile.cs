using AutoMapper;
using ECommerceAPI.Core.DTOs.Address;
using ECommerceAPI.Core.DTOs.Cart;
using ECommerceAPI.Core.DTOs.Category;
using ECommerceAPI.Core.DTOs.Order;
using ECommerceAPI.Core.DTOs.Product;
using ECommerceAPI.Core.DTOs.User;
using ECommerceAPI.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //product
            CreateMap<Product,ProductDTO>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category.Name).ToList()));
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();

            //category
            CreateMap<Category, CategoryDTO>();
            CreateMap<Category, CategoryWithProductsDTO>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductCategories.Select(pc =>pc.Product.ProductName).ToList()));
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();

            //order
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items));
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<UpdateOrderDTO, Order>();

            //cart
            CreateMap<Cart,CartDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.Product.ImageURL))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Product.Price * src.Quantity));
            CreateMap<CreateCartDTO, Cart>();
            CreateMap<UpdateCartDTO, Cart>();

            //user
            CreateMap<User, UserDTO>();
            CreateMap<RegisterDTO, User>();
            CreateMap<UpdateUserDTO, User>();

            //address
            CreateMap<Address,AddressDTO>();
            CreateMap<CreateAddressDTO, Address>();
            CreateMap<UpdateAddressDTO, Address>();
        }
    }
}
