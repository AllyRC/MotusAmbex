using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="sale">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return sale;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales.Include(s => s.Products).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
    public async Task<int> GetSaleNumberAsync(CancellationToken cancellationToken = default)
    {
        int maxSaleNumber = await _context.Sales
            .Select(s => (int?)s.SaleNumber) // Transforma em um tipo anulável para evitar exceções
            .MaxAsync(cancellationToken) ?? 0; // Retorna 0 se não houver elementos

        return maxSaleNumber + 1;
    }


    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Sales.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public IQueryable<Sale> GetSaleAsQueryable(int? saleNumber, CancellationToken cancellationToken = default)
    {
        if(saleNumber == null)
        {
            return _context.Sales.Include(s => s.Products).AsQueryable().OrderBy(s => s.SaleNumber);
        }
        else
        {
            return _context.Sales.Where(s => s.SaleNumber == saleNumber).Include(s => s.Products).AsQueryable().OrderBy(s => s.SaleNumber);
        }
        
    }
}
