using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> CreateAsync(Sale product, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<int> GetSaleNumberAsync(CancellationToken cancellationToken = default);
        IQueryable<Sale> GetSaleAsQueryable(int? saleNumber, CancellationToken cancellationToken = default);
    }
}
