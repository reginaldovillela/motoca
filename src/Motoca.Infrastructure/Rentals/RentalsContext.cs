using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;
using Motoca.Infrastructure.Extensions;
using Motoca.Infrastructure.Rentals.Mappings;

namespace Motoca.Infrastructure.Rentals;

public class RentalsContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    private IDbContextTransaction _currentTransaction;

    public DbSet<RentalEntity> Rentals { get; set; }

    public DbSet<PlansEntity> Plans { get; set; }

    public RentalsContext(DbContextOptions<RentalsContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RentalsEntityMapping());
        modelBuilder.ApplyConfiguration(new PlansEntityMapping());

        modelBuilder.Entity<PlansEntity>().HasData(
            new PlansEntity("plano7", 7, 30),
            new PlansEntity("plano15", 15, 28),
            new PlansEntity("plano30", 22, 22),
            new PlansEntity("plano45", 45, 20),
            new PlansEntity("plano50", 50, 18)
        );

        base.OnModelCreating(modelBuilder);
    }

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction is not null)
            return null;

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
