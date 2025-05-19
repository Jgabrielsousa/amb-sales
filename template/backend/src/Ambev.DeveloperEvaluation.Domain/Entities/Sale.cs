using Ambev.DeveloperEvaluation.Domain.Common;
namespace Ambev.DeveloperEvaluation.Domain.Entities;
public class SaleEntity : BaseEntity
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; } = string.Empty;
    public decimal TotalSaleAmount { get; set; }
    public string Branch { get; set; } = string.Empty;

    public List<SaleItemEntity>? SalesItem { get; set; }

    public bool IsCancelled { get; set; }

    public void AddSalesItems(IEnumerable<SaleItemEntity> salesItem)
      => SalesItem?.AddRange(salesItem);
    public void ItemsClean()
      => SalesItem?.Clear();
}