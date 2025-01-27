namespace S2.HseCarShop.Models.Abstractions;

/// <summary>
/// Базовая структура для передачи параметров двигателя
/// </summary>
public abstract record EngineParamsBase
{
    /// <summary>
    /// Публичное статическое поле, доступное только для чтения, чтобы каждый раз не писать new
    /// </summary>
    public static readonly EmptyEngineParams Empty = new();
}
