using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }
    public async Task<SaleEntity> CreateAsync(SaleEntity sale, CancellationToken cancellationToken = default)
    {
        await _context.Sale.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null)
            return false;

        _context.Sale.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<SaleEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => await _context.Sale.
            Include(c => c.SalesItem)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

    public async Task<SaleEntity> UpdateAsync(SaleEntity sale, CancellationToken cancellationToken = default)
    {
        var items = await _context.SaleItem.Where(o => o.SaleId == sale.Id).ToListAsync(cancellationToken);
        _context.SaleItem.RemoveRange(items);

        _context.Sale.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);

        return sale;
    }
}