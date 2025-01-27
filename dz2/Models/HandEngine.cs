using S2.HseCarShop.Models.Abstractions;

namespace S2.HseCarShop.Models;

/// <summary>
/// Ручной двигатель
/// </summary>
public class HandEngine : IEngine
{
    public EngineType Type => EngineType.Hand;

    public override string ToString()
        => $"Тип: {Type}";

    public bool IsCompatible(Customer customer)
        => customer.HandStrength > 5;
}