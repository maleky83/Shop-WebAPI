using AutoMapper;
using Shop.Domain.Entities.Catalog;

namespace Shop.Application.Categories.Dtos;

public class CategoriesProfile : Profile
{
    public CategoriesProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(
                dest => dest.CategoryId,
                opt => opt.MapFrom(src => src.Id)
            );
    }
}
