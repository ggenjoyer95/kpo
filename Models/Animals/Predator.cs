namespace Zoopark;

public abstract class Predator : Animal
{
    protected Predator(int food, int health, int number, string? name = null)
        : base(food, health, number, name) { }
}
