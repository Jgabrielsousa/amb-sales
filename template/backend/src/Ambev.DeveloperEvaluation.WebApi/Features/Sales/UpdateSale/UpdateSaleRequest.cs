namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    public Guid Id { get; private set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; } = string.Empty;
    public decimal TotalSaleAmount { get; set; }
    public string Branch { get; set; } = string.Empty;
    public List<UpdateSaleItemRequest>? SalesItem { get; set; }
    public bool IsCancelled { get; set; }
    public void SetId(Guid id)
        => Id = id;
}

public record UpdateSaleItemRequest
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}