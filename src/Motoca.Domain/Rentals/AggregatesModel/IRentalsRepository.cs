using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public interface IRentalsRepository : IRepository<RentalEntity>
{
    Task<RentalEntity> AddAsync(RentalEntity rental);

    Task<RentalEntity> GetRentalByIdAsync(string rentalId);

    Task<RentalEntity> EndRentalAsync(RentalEntity rental);
}
