using System;
using System.Diagnostics;
using System.Linq;

namespace Zoopark;

public static class Tests
{
    public static void RunAllTests()
    {
        InvalidParams();
        HealthyAdd();
        UnhealthyAdd();
        InteractiveFilter();
        TotalFood();
        InventoryList();
        DefaultName();
        ThingAdd();
        MultipleAdd();
        Console.WriteLine("Все тесты пройдены успешно.");
    }

    static void InvalidParams()
    {
        try
        {
            new Monkey(0, 5, 1, 5);
            Debug.Fail("Ожидалось исключение для food = 0");
        }
        catch (ArgumentOutOfRangeException) { }

        try
        {
            new Rabbit(11, 5, 2, 6);
            Debug.Fail("Ожидалось исключение для food = 11");
        }
        catch (ArgumentOutOfRangeException) { }

        try
        {
            new Tiger(5, 0, 3);
            Debug.Fail("Ожидалось исключение для health = 0");
        }
        catch (ArgumentOutOfRangeException) { }

        try
        {
            new Wolf(5, 11, 4);
            Debug.Fail("Ожидалось исключение для health = 11");
        }
        catch (ArgumentOutOfRangeException) { }
    }

    class AlwaysHealthyVetClinic : IVetClinic
    {
        public bool CheckAnimalHealth(Animal animal) => true;
    }

    class AlwaysUnhealthyVetClinic : IVetClinic
    {
        public bool CheckAnimalHealth(Animal animal) => false;
    }

    static void HealthyAdd()
    {
        var zooService = new ZooService(new AlwaysHealthyVetClinic());
        var monkey = new Monkey(5, 7, 10, 6, "1f");
        bool added = zooService.AddAnimal(monkey);
        Debug.Assert(added, "Животное должно быть добавлено, если здоровое");
        Debug.Assert(zooService.GetAnimalCount() == 1, "В зоопарке должно быть 1 животное");
    }

    static void UnhealthyAdd()
    {
        var zooService = new ZooService(new AlwaysUnhealthyVetClinic());
        var tiger = new Tiger(5, 7, 20, "2");
        bool added = zooService.AddAnimal(tiger);
        Debug.Assert(!added, "Животное не должно быть добавлено, если нездоровое");
        Debug.Assert(zooService.GetAnimalCount() == 0, "В зоопарке не должно быть животных");
    }

    static void InteractiveFilter()
    {
        var zooService = new ZooService(new AlwaysHealthyVetClinic());
        var friendlyMonkey = new Monkey(5, 7, 30, 7, "1f");
        var unfriendlyRabbit = new Rabbit(5, 7, 31, 5, "2");
        var tiger = new Tiger(5, 7, 32, "3");

        zooService.AddAnimal(friendlyMonkey);
        zooService.AddAnimal(unfriendlyRabbit);
        zooService.AddAnimal(tiger);

        var interactive = zooService.GetInteractiveAnimals().ToList();
        Debug.Assert(interactive.Contains(friendlyMonkey), "1f должен быть интерактивным");
        Debug.Assert(!interactive.Contains(unfriendlyRabbit), "2 не должен быть интерактивным");
        Debug.Assert(!interactive.Contains(tiger), "3 не должен попадать в список интерактивных");
    }

    static void TotalFood()
    {
        var zooService = new ZooService(new AlwaysHealthyVetClinic());
        var monkey = new Monkey(3, 7, 40, 6);
        var rabbit = new Rabbit(4, 7, 41, 7);
        var tiger = new Tiger(5, 7, 42);

        zooService.AddAnimal(monkey);
        zooService.AddAnimal(rabbit);
        zooService.AddAnimal(tiger);

        int total = zooService.GetTotalFoodConsumption();
        Debug.Assert(total == 12, $"Ожидалось 12, получено {total}");
    }

    static void InventoryList()
    {
        var zooService = new ZooService(new AlwaysHealthyVetClinic());
        var monkey = new Monkey(5, 7, 50, 6, "1");
        var table = new Table("Wooden Table", 100);
        zooService.AddAnimal(monkey);
        zooService.AddThing(table);

        var inventory = zooService.GetInventoryItems().ToList();
        Debug.Assert(
            inventory.Any(item => item.Number == monkey.Number),
            "Животное должно быть в инвентаризации"
        );
        Debug.Assert(
            inventory.Any(item => item.Number == table.Number),
            "Вещь должна быть в инвентаризации"
        );
        Debug.Assert(inventory.Count() == 2, "В инвентаризации должно быть ровно 2 объекта");
    }

    static void DefaultName()
    {
        var monkey = new Monkey(5, 7, 60, 6);
        Debug.Assert(monkey.Name == "Monkey", $"Вместо обезьяны: {monkey.Name}");

        var tiger = new Tiger(5, 7, 61);
        Debug.Assert(tiger.Name == "Tiger", $"Вместо тигра: {tiger.Name}");
    }

    static void ThingAdd()
    {
        var zooService = new ZooService(new AlwaysHealthyVetClinic());
        var computer = new Computer("4", 200);
        zooService.AddThing(computer);
        var inventory = zooService.GetInventoryItems().ToList();
        Debug.Assert(
            inventory.Any(item => item.Number == computer.Number),
            "Вещь должна быть в инвентаризации"
        );
    }

    static void MultipleAdd()
    {
        var zooService = new ZooService(new AlwaysHealthyVetClinic());
        var monkey = new Monkey(5, 7, 300, 6, "5");
        var rabbit = new Rabbit(4, 7, 301, 7, "6");
        zooService.AddAnimal(monkey);
        zooService.AddAnimal(rabbit);
        Debug.Assert(zooService.GetAnimalCount() == 2, "В зоопарке должно быть 2 животных");
    }
}
