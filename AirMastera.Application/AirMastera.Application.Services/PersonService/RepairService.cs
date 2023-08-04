using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;
using AutoMapper;

namespace AirMastera.Application.Services.PersonService;

public class RepairService : IRepairService
{
    private readonly IRepairRepository _repairRepository;
    private readonly IMapper _mapper;
    private readonly ICarRepository _carRepository;

    public RepairService(IRepairRepository repairRepository, IMapper mapper, ICarRepository carRepository)
    {
        _repairRepository = repairRepository ?? throw new ArgumentNullException(nameof(repairRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
    }

    public async Task<RepairDto> CreateRepairAsync(Guid carId, CreateOrUpdateRepairRequest request, CancellationToken cancellationToken)
    {
        request.Id = Guid.NewGuid();

        var currentCar = await _carRepository.GetCarAsync(carId, cancellationToken);
        var createRequest = new CreateOrUpdateCarRequest
        {
            Id = carId,
            Name = currentCar.Name,
            Model = currentCar.Model,
            Number = currentCar.Number,
            Avatar = currentCar.Avatar,
            Repair = request
        };
        var car = _mapper.Map<Car>(createRequest);
        return (await _carRepository.UpdateCarAsync(car, cancellationToken)).Repairs.First();
    }

    public async Task<RepairDto> UpdateRepairAsync(Guid repairId, CreateOrUpdateRepairRequest request, CancellationToken cancellationToken)
    {
        request.Id = repairId;
        var repair = _mapper.Map<Repair>(request);
        return await _repairRepository.UpdateRepairAsync(repair, cancellationToken);
    }

    public Task<IEnumerable<RepairDto>> GetRepairs(Guid carId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return _repairRepository.GetRepairs(carId, pageNumber, pageSize, cancellationToken);
    }

    public Task<RepairDto> GetRepair(Guid repairId, CancellationToken cancellationToken)
    {
        return _repairRepository.GetRepair(repairId, cancellationToken);
    }

    public Task DeleteRepairAsync(Guid repairId, CancellationToken cancellationToken)
    {
        return _repairRepository.DeleteRepair(repairId, cancellationToken);
    }
}