    namespace S2.HseCarShop.Models.Abstractions;

    /// <summary>
    /// Интерфейс для описания двигателя
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Тип двигателя
        /// </summary>
        EngineType Type { get; }
        public bool IsCompatible(Customer customer);
    }