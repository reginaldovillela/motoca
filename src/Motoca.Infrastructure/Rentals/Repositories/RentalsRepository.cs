using Motoca.Domain.Rentals.AggregatesModel;
using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Infrastructure.Rentals.Repositories;

public class RentalsRepository : IRentalsRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task<RentalEntity> AddAsync(RentalEntity rental)
    {
        throw new NotImplementedException();
    }

    public Task<RentalEntity> EndRentalAsync(RentalEntity rental)
    {
        throw new NotImplementedException();
    }

    public Task<RentalEntity> GetRentalByIdAsync(string rentalId)
    {
        throw new NotImplementedException();
    }
}
