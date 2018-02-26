namespace StockMarket
{
    public interface IObserver
    {
        void ValueChanged(Subject stock);
    }
}