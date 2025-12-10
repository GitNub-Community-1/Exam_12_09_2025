using AutoMapper;
using Domain;
using Infastructure.DTOs.CategoriDto;
using Infastructure.DTOs.ProductDto;
using Infastructure.DTOs.SupplierDto;
using Infastructure.DTOs.SupplyDto;

namespace Infastructure.MapperProfile;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        
        CreateMap<Category, CategoryGetDto>().ReverseMap();;
        CreateMap<CategoryCreateDto, Category>().ReverseMap();;
        CreateMap<CategoryUpdateDto, Category>().ReverseMap();;

        
        CreateMap<Product, ProductGetDto>().ReverseMap();;
        CreateMap<ProductCreateDto, Product>().ReverseMap();;
        CreateMap<ProductUpdateDto, Product>().ReverseMap();;

        
        CreateMap<Supplier, SupplierGetDto>().ReverseMap();;
        CreateMap<SupplierCreateDto, Supplier>().ReverseMap();;
        CreateMap<SupplierUpdateDto, Supplier>().ReverseMap();;

        
        CreateMap<Supply, SupplyGetDto>().ReverseMap();;
        CreateMap<SupplyCreateDto, Supply>().ReverseMap();;
        CreateMap<SupplyUpdateDto, Supply>().ReverseMap();;
    }
}