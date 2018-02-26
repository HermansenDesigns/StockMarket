using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarket
{
    public class Portfolio : IObserver, IPortfolio
    {
        public string Name { get; set; }
        public IPortfolioDisplay PortfolioDisplay { get; set; }
        public Dictionary<IStock, int> Stocks { get; private set; }

        public Portfolio(string name , IPortfolioDisplay portfolioDisplay)
        {
            Name = name;
            Stocks = new Dictionary<IStock, int>();
            PortfolioDisplay = portfolioDisplay ?? throw new ArgumentNullException(nameof(portfolioDisplay));
        }

        public void AddStock(IStock stock, int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("amount has to be greater than 0");

            if (!Stocks.ContainsKey(stock))
            {
                Stocks.Add(stock, amount);
                ((Subject)stock).Register(this);
                Console.WriteLine($"{stock.Name} has added to the portfolio");
            }
            else
            {
                Stocks[stock] += amount;
                Console.WriteLine($"{amount} has been added to {stock.Name}");
            }
        }

        public void RemoveStock(IStock stock, int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("amount has to be greater than 0");

            if (Stocks.ContainsKey(stock))
            {
                if (amount >= Stocks[stock])
                {
                    Stocks.Remove(stock);
                    ((Subject)stock).Unregister(this);
                    Console.WriteLine($"{stock.Name} has been removed from portfolio");
                }
                else
                {
                    Stocks[stock] -= amount;
                    Console.WriteLine($"{amount} has been removed from {stock.Name}");

                }
            }
            else
            {
                Console.WriteLine($"{stock.Name} is not a part of portfolio");
            }
        }

        public decimal Total
        {
            get { return Stocks.Sum(o => o.Value * o.Key.Value); }
        }

        #region IObserver Members

        public void ValueChanged(Subject value)
        {

            if (StockMarket.PortfolioNotifications)
            {
            PortfolioDisplay.PrintInformation(this);
            }
        }

        #endregion

    }
}