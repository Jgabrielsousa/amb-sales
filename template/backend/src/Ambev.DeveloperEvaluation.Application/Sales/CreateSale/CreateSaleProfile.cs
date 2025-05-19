using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, SaleEntity>();
        CreateMap<CreateSaleItemCommand, SaleItemEntity>();

        CreateMap<SaleEntity, CreateSaleResult>();
        CreateMap<SaleItemEntity, CreateSaleItemResult>();
    }
}