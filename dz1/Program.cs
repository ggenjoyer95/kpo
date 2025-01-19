namespace S1.PedalCarAccauntingInformationSystem;

internal class Program
{
    static void Main(string[] args)
    {
        var customers = new List<Customer>
        {
            new() { Name = "Ivan" },
            new() { Name = "Petr" },
            new() { Name = "Sidr" },
        };

        var factory = new FactoryAF(customers);

        for (int i = 0; i < 5; i++)
            factory.AddCar();

        Console.WriteLine("Машины на складе:");
        Console.WriteLine(string.Join(Environment.NewLine, factory.Cars));
        Console.WriteLine("Клиенты в очереди:");
        Console.WriteLine(string.Join(Environment.NewLine, customers));

        factory.SaleCar();

        Console.WriteLine("\nПосле отработки SaleCar:");
        Console.WriteLine("Машины на складе:");
        Console.WriteLine(string.Join(Environment.NewLine, factory.Cars));
        Console.WriteLine("Клиенты, получившие машины:");
        Console.WriteLine(string.Join(Environment.NewLine, customers));
    }
}
