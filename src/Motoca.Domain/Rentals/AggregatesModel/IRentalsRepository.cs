using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public interface IRentalsRepository : IRepository<RentalEntity>
{
    Task<RentalEntity> AddAsync(RentalEntity rental);

    Task<RentalEntity?> BikeHasAlreadyRentaled(string bikeId);

    Task<RentalEntity> EndRentalAsync(RentalEntity rental);

    Task<ICollection<RentalEntity>> GetAllAsync();

    Task<RentalEntity?> GetByEntityIdAsync(Guid entityId);

    Task<RentalEntity?> GetByIdAsync(string rentalId);

    Task<RentalEntity?> RiderHasAActiveRental(string riderId);
}
