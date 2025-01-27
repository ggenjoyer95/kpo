using S2.HseCarShop.Models;
using S2.HseCarShop.Services.Abstractions;

namespace S2.HseCarShop.Services;

/// <summary>
/// Реализация фабрики для создания педальных автомобилей
/// </summary>
public class PedalCarFactory : ICarFactory<PedalEngineParams>
{
    /// <summary>
    /// Реализация метода для создания педальных автомобилей
    /// </summary>
    public Car CreateCar(PedalEngineParams carParams, int number)
    {
        var engine = new PedalEngine(carParams.PedalSize);
        return new Car(engine, number: Guid.NewGuid());
    }
}
