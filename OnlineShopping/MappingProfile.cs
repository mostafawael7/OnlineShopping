using AutoMapper;
using OnlineShopping.Dtos;
using OnlineShopping.Models;

namespace OnlineShopping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Supplier, SupplierDto>().ReverseMap();
        }
    }
}
