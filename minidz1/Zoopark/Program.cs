using System;
using Microsoft.Extensions.DependencyInjection;
using Zoopark;

namespace Zoopark;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = DependencyInjection.Configure();
        var zooService = serviceProvider.GetService<IZooService>()!;
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Главное меню:");
            Console.WriteLine("1. Добавить животное");
            Console.WriteLine("2. Добавить вещь");
            Console.WriteLine("3. Показать количество потребляемой еды");
            Console.WriteLine("4. Показать количество животных");
            Console.WriteLine("5. Показать интерактивных животных");
            Console.WriteLine("6. Показать список инвентаризации");
            Console.WriteLine("7. Выход");
            Console.Write("Введите цифру: ");
            string? choice = Console.ReadLine();
            Console.WriteLine();
            if (choice == "1")
            {
                AddAnimal(zooService);
            }
            else if (choice == "2")
            {
                AddThing(zooService);
            }
            else if (choice == "3")
            {
                Console.WriteLine(
                    $"Общее количество еды: {zooService.GetTotalFoodConsumption()} кг"
                );
            }
            else if (choice == "4")
            {
                Console.WriteLine($"Количество животных: {zooService.GetAnimalCount()}");
            }
            else if (choice == "5")
            {
                Console.WriteLine("Интерактивные животные:");
                foreach (var animal in zooService.GetInteractiveAnimals())
                {
                    Console.WriteLine(
                        $"- {animal.Name} (инвентаризационный номер: {animal.Number})"
                    );
                }
            }
            else if (choice == "6")
            {
                Console.WriteLine("Инвентаризационные объекты:");
                foreach (var item in zooService.GetInventoryItems())
                {
                    if (item is Animal animal)
                        Console.WriteLine(
                            $"- {animal.Name} (инвентаризационный номер: {item.Number})"
                        );
                    else if (item is Thing thing)
                        Console.WriteLine(
                            $"- {thing.Name} (инвентаризационный номер: {item.Number})"
                        );
                }
            }
            else if (choice == "7")
            {
                exit = true;
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
            }
            Console.WriteLine();
        }
    }

    static void AddAnimal(IZooService zooService)
    {
        string? typeChoice;
        bool validType = false;
        do
        {
            Console.WriteLine("Выберите какое животное:");
            Console.WriteLine("1 Monkey");
            Console.WriteLine("2 Rabbit");
            Console.WriteLine("3 Tiger");
            Console.WriteLine("4 Wolf");
            Console.Write("Введите цифру: ");
            typeChoice = Console.ReadLine();
            if (typeChoice == "1" || typeChoice == "2" || typeChoice == "3" || typeChoice == "4")
            {
                validType = true;
            }
            else
            {
                Console.WriteLine("Неверный выбор животного. Попробуйте снова ввести цифру.");
            }
        } while (!validType);
        Console.Write("Введите количество потребляемой еды 1-10: ");
        int food = int.Parse(Console.ReadLine()!);
        Console.Write("Введите уровень здоровья 1-10: ");
        int health = int.Parse(Console.ReadLine()!);
        Console.Write("Введите инвентаризационный номер: ");
        int number = int.Parse(Console.ReadLine()!);
        Console.Write("Введите имя животного или оставьте пустым: ");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            name = null;
        }
        Animal? animal = null;
        if (typeChoice == "1")
        {
            Console.Write("Введите уровень доброты: ");
            int kindness = int.Parse(Console.ReadLine()!);
            animal = new Monkey(food, health, number, kindness, name);
        }
        else if (typeChoice == "2")
        {
            Console.Write("Введите уровень доброты: ");
            int kindness = int.Parse(Console.ReadLine()!);
            animal = new Rabbit(food, health, number, kindness, name);
        }
        else if (typeChoice == "3")
        {
            animal = new Tiger(food, health, number, name);
        }
        else if (typeChoice == "4")
        {
            animal = new Wolf(food, health, number, name);
        }
        if (animal is null)
        {
            Console.WriteLine("Ошибка создания животного.");
            return;
        }
        if (zooService.AddAnimal(animal))
        {
            Console.WriteLine("Животное успешно добавлено в зоопарк.");
        }
        else
        {
            Console.WriteLine("Животное не прошло проверку здоровья и не было добавлено.");
        }
    }

    static void AddThing(IZooService zooService)
    {
        string? typeChoice;
        bool validType = false;
        do
        {
            Console.WriteLine("Выберите тип вещи:");
            Console.WriteLine("1 Table");
            Console.WriteLine("2 Computer");
            Console.Write("Введите цифру: ");
            typeChoice = Console.ReadLine();
            if (typeChoice == "1" || typeChoice == "2")
            {
                validType = true;
            }
            else
            {
                Console.WriteLine("Неверный выбор вещи. Попробуйте снова ввести цифру.");
            }
        } while (!validType);
        Console.Write("Введите наименование вещи: ");
        string name = Console.ReadLine()!;
        Console.Write("Введите инвентаризационный номер: ");
        int number = int.Parse(Console.ReadLine()!);

        Thing? thing = null;
        if (typeChoice == "1")
        {
            thing = new Table(name, number);
        }
        else if (typeChoice == "2")
        {
            thing = new Computer(name, number);
        }

        if (thing is null)
        {
            Console.WriteLine("Ошибка создания вещи.");
            return;
        }
        zooService.AddThing(thing);
        Console.WriteLine("Вещь успешно добавлена.");
    }
}
