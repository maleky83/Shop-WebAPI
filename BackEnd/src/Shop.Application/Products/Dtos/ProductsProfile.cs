using AutoMapper;
using Shop.Application.Products.Commands.CreateProduct;
using Shop.Application.Products.Commands.UpdateProduct;
using Shop.Domain.Entities.Catalog;

namespace Shop.Application.Products.Dtos;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {

        CreateMap<UpdateProductCommand, Product>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateProductCommand, Product>();

        CreateMap<Product, ProductDto>()
            .ForMember(
                dest => dest.ProductId,
                opt => opt.MapFrom(src => src.Id)
            );

        CreateMap<CreateProductCommand, Product>();
    }
}
