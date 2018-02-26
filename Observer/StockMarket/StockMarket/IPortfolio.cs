using System.Collections.Generic;

namespace StockMarket
{
    /// <summary>
    /// IPortfolio interface
    /// This interface contains the contract that makes
    /// Portfolio derivatives able to handle IStock
    /// </summary>
    public interface IPortfolio
    {
        string Name { get; set; }
        IPortfolioDisplay PortfolioDisplay { get; set; }
        Dictionary<IStock, int> Stocks { get; }
        decimal Total { get; }

        void AddStock(IStock stock, int amount);
        void RemoveStock(IStock stock, int amount);
    }
}