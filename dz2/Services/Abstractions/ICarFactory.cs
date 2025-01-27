using S2.HseCarShop.Models;
using S2.HseCarShop.Models.Abstractions;

namespace S2.HseCarShop.Services.Abstractions;

/// <summary>
/// Обобщенный интерфейс фабрики для создания автомобилей
/// </summary>
public interface ICarFactory<TParams>
    where TParams : EngineParamsBase
{
    /// <summary>
    /// Метод для конструкирования автомобилей
    /// </summary>
    Car CreateCar(TParams carParams, int number);
}
