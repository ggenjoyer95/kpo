using System;

namespace Zoopark;

public class VetClinic : IVetClinic
{
    private static readonly Random _random = new Random();

    public bool CheckAnimalHealth(Animal animal)
    {
        return _random.Next(0, 10) < 9;
    }
}
