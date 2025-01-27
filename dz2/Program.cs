using S2.HseCarShop.Models;
using S2.HseCarShop.Services;

namespace S2.HseCarShop;

internal class Program
{
    static void Main(string[] args)
    {
        var customerStorage = new CustomersStorage();

        var customers = new[]
{
            new Customer(name: "Ivan", legStrength: 10, handStrength: 1),
            new Customer(name : "Petr", legStrength : 1, handStrength : 10),
            new Customer(name : "Sidr", legStrength : 1, handStrength : 1),
            new Customer(name : "Sidr", legStrength : 10, handStrength : 10),
        };

        foreach (var customer in customers)
            customerStorage.AddCustomer(customer);

        var pedalCarFactory = new PedalCarFactory();
        var handCarFactory = new HandCarFactory();

        var carStorage = new CarStorage();

        carStorage.AddCar(pedalCarFactory, new PedalEngineParams(pedalSize: 2));
        carStorage.AddCar(pedalCarFactory, new PedalEngineParams(pedalSize: 3));
        carStorage.AddCar(handCarFactory, EmptyEngineParams.Empty);
        carStorage.AddCar(handCarFactory, EmptyEngineParams.Empty);

        var hseCarShop = new CarShopService(carStorage, customerStorage);

        Console.WriteLine("=== Покупатели ===");
        foreach (var customer in customers)
            Console.WriteLine(customer);

        Console.WriteLine("\n=== Продажа автомобилей ===\n");
        hseCarShop.SellCars();

        Console.WriteLine("=== Покупатели ===");
        foreach (var customer in customers)
            Console.WriteLine(customer);
    }
}
