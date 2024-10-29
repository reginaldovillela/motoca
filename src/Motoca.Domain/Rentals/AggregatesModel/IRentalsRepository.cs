using Motoca.Domain.SeedWork.Interfaces;

namespace Motoca.Domain.Rentals.AggregatesModel;

public interface IRentalsRepository : IRepository<RentalEntity>
{
    Task<RentalEntity> AddAsync(RentalEntity rental, CancellationToken cancellationToken);

    Task<RentalEntity?> BikeHasAlreadyBeenRentals(string bikeId, CancellationToken cancellationToken);

    Task<RentalEntity?> BikeHasAlreadyRentaled(string bikeId, CancellationToken cancellationToken);

    Task<RentalEntity> EndRentalAsync(RentalEntity rental, CancellationToken cancellationToken);

    Task<ICollection<RentalEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<RentalEntity?> GetByEntityIdAsync(Guid entityId, CancellationToken cancellationToken);

    Task<RentalEntity?> GetByIdAsync(string rentalId, CancellationToken cancellationToken);

    Task<RentalEntity?> RiderHasAActiveRental(string riderId, CancellationToken cancellationToken);
}
