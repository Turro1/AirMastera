using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;

namespace AirMastera.Application.Services.Interfaces;

public interface ICarRepository
{
    Task<IEnumerable<CarDto>> GetCarsAsync(Guid personId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<CarDto> GetCarAsync(Guid carId, CancellationToken cancellationToken);
    Task<CarDto> UpdateCarAsync(Car car, CancellationToken cancellationToken);
    Task DeleteCar(Guid carId, CancellationToken cancellationToken);
}