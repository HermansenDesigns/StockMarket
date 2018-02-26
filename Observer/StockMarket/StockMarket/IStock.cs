namespace StockMarket
{
    /// <summary>
    /// IStock interface
    /// This interface binds the derivatives to implement Name and Value.
    /// Instability is a method, that changes the internal state of the object.
    /// </summary>
    public interface IStock
    {
        string Name { get; }
        decimal Value { get; set; }

        void Instability();
    }
}