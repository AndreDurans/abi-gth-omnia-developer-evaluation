using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SaleDto, Sale>();
            CreateMap<SaleItemDto, SaleItem>();
            CreateMap<Sale, SaleDto>();
            CreateMap<SaleItem, SaleItemDto>();

            CreateMap<SaleItemDto, SaleItem>()
                .AfterMap((src, dest) => {
                    dest.ApplyDiscountRules();
                    dest.CalculateTotalAmount();
                });

            CreateMap<SaleDto, Sale>()
            .AfterMap((src, dest) => dest.CalculateTotalAmount());
        }
    }
}
