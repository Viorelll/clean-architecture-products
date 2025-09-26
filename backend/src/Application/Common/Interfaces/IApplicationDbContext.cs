using CleanArchitectureApi.Domain.Entities;

namespace CleanArchitectureApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ProductList> ProductLists { get; }

    DbSet<ProductItem> ProductItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
