using Ambev.DeveloperEvaluation.Domain.Common;
namespace Ambev.DeveloperEvaluation.Domain.Entities;
public  class SaleItemEntity : BaseEntity
{
    public Guid SaleId { get; private set; }
    public string Product {get; set; } = string.Empty;
    public int Quantity {get; set; }
    public decimal UnitPrice {get; set; }
    public decimal Discount {get; set; }
    public decimal TotalAmount { get; set; }
}
