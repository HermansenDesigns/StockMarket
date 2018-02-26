namespace StockMarket
{
    public interface IStock
    {
        string Name { get; }
        decimal Value { get; set; }

        void Instability();
    }
}