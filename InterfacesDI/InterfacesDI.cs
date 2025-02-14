using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Zoopark;

public interface IAlive
{
    int Food { get; set; }
}

public interface IInventory
{
    int Number { get; set; }
}

public interface IVetClinic
{
    bool CheckAnimalHealth(Animal animal);
}

public interface IZooService
{
    bool AddAnimal(Animal animal);
    void AddThing(Thing thing);
    int GetTotalFoodConsumption();
    int GetAnimalCount();
    IEnumerable<Animal> GetInteractiveAnimals();
    IEnumerable<IInventory> GetInventoryItems();
}

public static class DependencyInjection
{
    public static ServiceProvider Configure()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IVetClinic, VetClinic>();
        services.AddSingleton<IZooService, ZooService>();
        return services.BuildServiceProvider();
    }
}
