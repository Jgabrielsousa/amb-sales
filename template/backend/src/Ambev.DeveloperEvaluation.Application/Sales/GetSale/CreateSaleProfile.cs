using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<GetSaleCommand, SaleEntity>();
        CreateMap<SaleEntity, GetSaleResult>();
        CreateMap<SaleItemEntity, GetSaleItemResult>();
    }
}