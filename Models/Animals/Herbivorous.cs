namespace Zoopark;

public abstract class Herbivorous : Animal
{
    public int Kindness { get; set; }

    protected Herbivorous(int food, int health, int number, int kindness, string? name = null)
        : base(food, health, number, name)
    {
        Kindness = kindness;
    }
}
