using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
   
    Task<SaleEntity> CreateAsync(SaleEntity sale, CancellationToken cancellationToken = default);

    
    Task<SaleEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<SaleEntity> UpdateAsync(SaleEntity sale, CancellationToken cancellationToken = default);
}
