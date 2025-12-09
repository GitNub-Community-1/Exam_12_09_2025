using AutoMapper;

namespace Infastructure.MapperProfile;

public class MapperProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<Category, CategoryReadDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();

        
        CreateMap<Product, ProductReadDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();

        
        CreateMap<Supplier, SupplierReadDto>();
        CreateMap<SupplierCreateDto, Supplier>();
        CreateMap<SupplierUpdateDto, Supplier>();

        
        CreateMap<Supply, SupplyReadDto>();
        CreateMap<SupplyCreateDto, Supply>();
        CreateMap<SupplyUpdateDto, Supply>();
    }
}