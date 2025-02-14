using System;
using Zoopark;

namespace Zoopark;

public abstract class Animal : IAlive, IInventory
{
    public int Food { get; set; }
    public int Health { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }

    protected Animal(int food, int health, int number, string? name = null)
    {
        if (food < 1 || food > 10)
        {
            throw new ArgumentOutOfRangeException(
                nameof(food),
                "Параметр не входит в промежуток 1-10."
            );
        }
        if (health < 1 || health > 10)
        {
            throw new ArgumentOutOfRangeException(
                nameof(health),
                "Параметр не входит в промежуток 1-10."
            );
        }
        Food = food;
        Health = health;
        Number = number;
        Name = name ?? GetType().Name;
    }
}
