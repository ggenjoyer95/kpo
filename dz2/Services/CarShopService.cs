using S2.HseCarShop.Models;
using S2.HseCarShop.Services.Abstractions;

namespace S2.HseCarShop.Services;

public class CarShopService
{
    /// <summary>
    /// Сервис предоставляющий нам автомобили
    /// </summary>
    private readonly ICarProvider _carProvider;

    /// <summary>
    /// Сервис предоставляющий нам покупателей
    /// </summary>
    private readonly ICustomersProvider _customersProvider;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    public CarShopService(ICarProvider carProvider, ICustomersProvider customersProvider)
    {
        ArgumentNullException.ThrowIfNull(carProvider, nameof(carProvider));
        ArgumentNullException.ThrowIfNull(customersProvider, nameof(customersProvider));

        _carProvider = carProvider;
        _customersProvider = customersProvider;
    }

    public void SellCars()
    {
        var customers = _customersProvider.GetCustomers();

        foreach (var customer in customers)
        {
            if (customer.Car != null)
                continue;

            var suitableEngineType = DetermineEngineType(customer);

            if (!suitableEngineType.HasValue)
                continue;

            var car = _carProvider.GetCar(suitableEngineType.Value);

            if (car == null)
                continue;

            customer.Car = car; // иначе вручаем автомобиль
        }
    }

    /// <summary>
    /// Определение типа двинателя который бы подошел пользователю по физическим параметрам
    /// </summary>
    /// <remarks>
    /// В условии это описано по другому, но я искренне полагаю, что прокидывать пользователя в класс двигателя это нарушение SRP.
    /// Мне наиболее логичным местом для проверки типа автомобиля подходящего для покупателя видится все же сам сервим для их продажи.
    /// </remarks>
    /// <param name="customer">Покупатель</param>
    /// <returns>Позвращает тип двигателя который подходит пользователю, если ни один не подходит, то null</returns>
    private static EngineType? DetermineEngineType(Customer customer)
    {
        if (customer.LegStrength > 5)
            return EngineType.Pedal;

        if (customer.HandStrength > 5)
            return EngineType.Hand;

        return null;
    }
}
