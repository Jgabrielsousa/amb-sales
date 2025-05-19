namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class GetSaleResult
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; } = string.Empty;
    public decimal TotalSaleAmount { get; set; }
    public string Branch { get; set; } = string.Empty;
    public List<GetSaleItemResult>? SalesItem { get; set; }
    public bool IsCancelled { get; set; }
}

public record GetSaleItemResult
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}