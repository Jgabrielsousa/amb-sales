using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleCommand, SaleEntity>();
        CreateMap<UpdateSaleItemCommand, SaleItemEntity>();

        CreateMap<SaleEntity, UpdateSaleResult>();
        CreateMap<SaleItemEntity, UpdateSaleItemResult>();
    }
}