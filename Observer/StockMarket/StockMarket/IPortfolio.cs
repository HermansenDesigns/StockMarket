using System.Collections.Generic;

namespace StockMarket
{
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