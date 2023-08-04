using AirMastera.Application.Services.Interfaces;
using AirMastera.Application.Services.Models;
using AirMastera.Domain.Entities;
using AutoMapper;

namespace AirMastera.Application.Services.PersonService;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;

    public CarService(ICarRepository carRepository, IPersonService personService, IMapper mapper)
    {
        _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        _personService = personService ?? throw new ArgumentNullException(nameof(personService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CarDto> CreateCar(Guid personId, CreateOrUpdateCarRequest request, CancellationToken cancellationToken)
    {
        var person = await _personService.GetPersonAsync(personId, cancellationToken);
        var personRequest = new UpdatePersonRequest
        {
            FullName = person.FullName,
            Phone = person.Phone,
            Car = request
        };

        var carId = Guid.NewGuid();
        return (await _personService.UpdatePersonAsync(personId, carId, personRequest, cancellationToken)).Cars.First();
    }

    public async Task<CarDto> UpdateCarAsync(Guid carId, CreateOrUpdateCarRequest request, CancellationToken cancellationToken)
    {
        request.Id = carId;
        var car = _mapper.Map<Car>(request);
        return await _carRepository.UpdateCarAsync(car, cancellationToken);
    }

    public Task<IEnumerable<CarDto>> GetCarsAsync(Guid personId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return _carRepository.GetCarsAsync(personId, pageNumber, pageSize, cancellationToken);
    }

    public Task<CarDto> GetCarAsync(Guid carId, CancellationToken cancellationToken)
    {
        return _carRepository.GetCarAsync(carId, cancellationToken);
    }

    public async Task DeleteCar(Guid carId, CancellationToken cancellationToken)
    {
        await _carRepository.DeleteCar(carId, cancellationToken);
    }
}