namespace S1.PedalCarAccauntingInformationSystem;

public class Customer
{
    public required string Name { get; set; }

    public Car? Car { get; set; }

    public override string ToString()
    {
        return $"Имя: {Name}, Номер машины: {Car?.Number ?? -1}";
    }
}
