using S2.HseCarShop.Models;

namespace S2.HseCarShop.Services.Abstractions;

/// <summary>
/// Интерфейс, предоставляющий автомобили для покупателей
/// </summary>
public interface ICarProvider
{
    /// <summary>
    /// Получение автомобиля с соответствующим типом двигателя
    /// </summary>
    /// <param name="engineType">Тип двигателя</param>
    /// <returns>Возвращает подходящую машину, а если таких машин не осталось - null</returns>
    Car? GetCar(EngineType engineType);
}
