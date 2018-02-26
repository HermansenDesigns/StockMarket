namespace StockMarket
{
    /// <summary>
    /// IObserver interface
    /// Used by subject and only contains ValueChanged,
    /// this implementation is suited for a Push methodology.
    /// </summary>
    public interface IObserver
    {
        void ValueChanged(Subject stock);
    }
}