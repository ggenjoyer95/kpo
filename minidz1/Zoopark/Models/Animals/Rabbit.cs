namespace Zoopark;

public class Rabbit : Herbivorous
{
    public Rabbit(int food, int health, int number, int kindness, string? name = null)
        : base(food, health, number, kindness, name) { }
}
