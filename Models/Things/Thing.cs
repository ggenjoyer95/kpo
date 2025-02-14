namespace Zoopark;

public class Thing : IInventory
{
    public int Number { get; set; }
    public string Name { get; set; }

    public Thing(string name, int number)
    {
        Name = name;
        Number = number;
    }
}
