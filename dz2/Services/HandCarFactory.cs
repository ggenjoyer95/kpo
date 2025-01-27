using S2.HseCarShop.Models;
using S2.HseCarShop.Services.Abstractions;

namespace S2.HseCarShop.Services;

/// <summary>
/// Реализация фабрики для создания автомобилей с ручным приводом
/// </summary>
public class HandCarFactory : ICarFactory<EmptyEngineParams>
{
    /// <summary>
    /// Реализация метода для построения автомобилей с ручным приводом
    /// </summary>
    public Car CreateCar(EmptyEngineParams carParams, int number)
    {
        var engine = new HandEngine();
        return new Car(engine, number: Guid.NewGuid());
    }
}
