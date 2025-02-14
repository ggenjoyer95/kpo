using System.Collections.Generic;

namespace Zoopark;

public class ZooService : IZooService
{
    private readonly IVetClinic _vetClinic;
    private readonly List<Animal> _animals = new();
    private readonly List<Thing> _things = new();

    public ZooService(IVetClinic vetClinic)
    {
        _vetClinic = vetClinic;
    }

    public bool AddAnimal(Animal animal)
    {
        if (_vetClinic.CheckAnimalHealth(animal))
        {
            _animals.Add(animal);
            return true;
        }
        return false;
    }

    public void AddThing(Thing thing)
    {
        _things.Add(thing);
    }

    public int GetTotalFoodConsumption()
    {
        int total = 0;
        foreach (var animal in _animals)
        {
            total += animal.Food;
        }
        return total;
    }

    public int GetAnimalCount() => _animals.Count;

    public IEnumerable<Animal> GetInteractiveAnimals()
    {
        foreach (var animal in _animals)
        {
            if (animal is Herbivorous herbivorous && herbivorous.Kindness > 5)
            {
                yield return animal;
            }
        }
    }

    public IEnumerable<IInventory> GetInventoryItems()
    {
        foreach (var animal in _animals)
        {
            yield return animal;
        }
        foreach (var thing in _things)
        {
            yield return thing;
        }
    }
}
