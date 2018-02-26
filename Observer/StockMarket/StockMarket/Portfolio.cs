using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarket
{
    /// <summary>
    /// Portfolio is the class that acts as both a folder and a wallet for IStock.
    /// This means that the class should be used in conjunction with IStock.
    /// </summary>

    public class Portfolio : IObserver, IPortfolio
    {
        /// <summary>
        /// Property for the name attribute. This variable contains the identification of the portfolio
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// PortfolioDisplay implements <see cref="IPortfolioDisplay"/>
        /// This is the visual output of the portfolio information
        /// </summary>
        public IPortfolioDisplay PortfolioDisplay { get; set; }

        /// <summary>
        /// Stocks is a property that keeps track of the different <see cref="Stock"/> and amount of these.
        /// </summary>
        public Dictionary<IStock, int> Stocks { get; private set; }

        /// <summary>
        /// The Constructor of the <see cref="Portfolio"/> this initializes the different properties
        /// and implements the given attributes.
        /// </summary>
        /// <param name="name">The name of the portfolio</param>
        /// <param name="portfolioDisplay">The visual output of the portfolio</param>
        public Portfolio(string name , IPortfolioDisplay portfolioDisplay)
        {
            Name = name;
            Stocks = new Dictionary<IStock, int>();
            PortfolioDisplay = portfolioDisplay ?? throw new ArgumentNullException(nameof(portfolioDisplay));
        }

        /// <summary>
        /// AddStock adds a certain amount of a type stock to the property <see cref="Stocks"/>
        /// </summary>
        /// <para>
        /// this method also ensures that the portfolio is registered as an observer for the subject Stock.
        /// If the portfolio is introduced a new type of Stock, it will be registered and added to the Property <see cref="Stocks"/>
        /// </para>
        /// <param name="stock">Type of <see cref="IStock"/></param>
        /// <param name="amount">The amount of <see cref="Stock"/> should be greater than 0</param>
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

        /// <summary>
        /// RemoveStock removes a certain amount of <see cref="Stock"/> from the property <see cref="Stocks"/>
        /// </summary>
        /// <para>
        /// this method ensures concurrency with Stock, and will unregister itself if the Stock amount reaches 0.
        /// This method can remove the Observer object portfolio from the subject subscriber list in <see cref="Stock"/>
        /// </para>
        /// <param name="stock">Type of <see cref="IStock"/></param>
        /// <param name="amount">Amount to remove, has to be greater than 0</param>
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

        /// <summary>
        /// This property returns a total amount of value from all the stock in the portfolio, summed up by Value * Amount
        /// </summary>
        public decimal Total
        {
            get { return Stocks.Sum(o => o.Value * o.Key.Value); }
        }

        #region IObserver Members
        /// <summary>
        /// The value changed, and the method implemented from interface, this takes a <see cref="Subject"/> Abstraction
        /// and prints the changed value. This method is crucial for the Observer pattern.
        /// </summary>
        /// <param name="value">This parameter is the internal state of the Subject received. This ensures that the Observer knows the state</param>
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