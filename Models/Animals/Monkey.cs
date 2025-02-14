namespace Zoopark;

public class Monkey : Herbivorous
{
    public Monkey(int food, int health, int number, int kindness, string? name = null)
        : base(food, health, number, kindness, name) { }
}
