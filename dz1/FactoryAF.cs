namespace S1.PedalCarAccauntingInformationSystem;

public class FactoryAF
{
    public List<Car> Cars { get; private set; }

    public Queue<Customer> Customers { get; private set; }

    public FactoryAF(IEnumerable<Customer> customers)
    {
        Customers = new Queue<Customer>(customers);
        Cars = new List<Car>();
    }

    internal void SaleCar()
    {
        while (Customers.Count > 0 && Cars.Count > 0)
        {
            var customer = Customers.Dequeue();
            var car = Cars.Last();
            customer.Car = car;
            Cars.RemoveAt(Cars.Count - 1);
        }

        Cars.Clear();
    }

    internal void AddCar()
    {
        var car = new Car { Number = Cars.Count + 1 };
        Cars.Add(car);
    }
}
