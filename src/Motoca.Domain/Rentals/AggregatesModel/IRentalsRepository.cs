using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public interface IRentalsRepository : IRepository<RentalEntity>
{
    Task<RentalEntity> AddAsync(RentalEntity rental);

    Task<RentalEntity> EndRentalAsync(RentalEntity rental);

    Task<RentalEntity?> GetByEntityIdAsync(Guid entityId);

    Task<RentalEntity?> GetByIdAsync(string rentalId);
}
