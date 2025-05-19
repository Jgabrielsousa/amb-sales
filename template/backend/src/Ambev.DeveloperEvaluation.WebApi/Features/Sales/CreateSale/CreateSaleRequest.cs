namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public record CreateSaleRequest
(
    string SaleNumber,
    DateTime SaleDate,
    string Customer,
    decimal TotalSaleAmount,
    string Branch,
    List<CreateSaleItemRequest> SalesItem,
    bool IsCancelled
);

public record CreateSaleItemRequest
(
    string Product,
    int Quantity,
    decimal UnitPrice,
    decimal Discount,
    decimal TotalAmount
);