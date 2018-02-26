namespace StockMarket
{
    /// <summary>
    /// IPortfolioDisplay interface
    /// This interface binds derivatives to PrintInformation
    /// that outputs information from IPortfolio
    /// </summary>
    public interface IPortfolioDisplay
    {
        void PrintInformation(IPortfolio portfolio);
    }
}