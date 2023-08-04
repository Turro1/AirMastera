using AirMastera.Application.Services.Models;

namespace AirMastera.Application.Services.Interfaces;

public interface ICarService
{
    Task<CarDto> CreateCar(Guid personId, CreateOrUpdateCarRequest request, CancellationToken cancellationToken);
    Task<CarDto> UpdateCarAsync(Guid carId, CreateOrUpdateCarRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<CarDto>> GetCarsAsync(Guid personId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<CarDto> GetCarAsync(Guid carId, CancellationToken cancellationToken);
    Task DeleteCar(Guid id, CancellationToken cancellationToken);
}