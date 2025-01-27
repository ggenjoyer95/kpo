using S2.HseCarShop.Models;
using S2.HseCarShop.Models.Abstractions;
using S2.HseCarShop.Services.Abstractions;

namespace S2.HseCarShop.Services;

public class CarStorage : ICarProvider
{
    /// <summary>
    /// Коллекция для хранения автомобилей
    /// </summary>
    private readonly LinkedList<Car> _cars = new();

    /// <summary>
    /// Методя для добавления автомобиля
    /// </summary>
    public void AddCar<TParams>(ICarFactory<TParams> carFactory, TParams carParams)
        where TParams : EngineParamsBase
    {
        var number = new Random().Next(1, 1000);
        var car = carFactory.CreateCar(carParams, number);
        _cars.AddLast(car);
    }

    public Car? GetCar(EngineType engineType)
    {
        var car = _cars.FirstOrDefault(car => car.Engine.Type == engineType);

        if (car != null)
            _cars.Remove(car);

        return car;
    }
}
