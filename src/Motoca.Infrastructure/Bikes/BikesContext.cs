using Motoca.Domain.Bikes.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;
using Motoca.Infrastructure.Extensions;

namespace Motoca.Infrastructure.Bikes;

public class BikesContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    private IDbContextTransaction? _currentTransaction;

    public DbSet<BikeEntity> Bikes { get; set; }

    public BikesContext(DbContextOptions<BikesContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction!;

    public bool HasActiveTransaction => _currentTransaction != null;

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction is not null)
            return null!;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        if (transaction != _currentTransaction)
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            _currentTransaction?.Dispose();
        }
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}
