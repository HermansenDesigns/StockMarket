using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace StockMarket
{
    /// <summary>
    /// <see cref="StockMarket"/> is a global static class that implements methods that can't or
    /// shouldn't be replicated elsewhere, such as trading of <see cref="Stock"/> and refreshing the state of <see cref="Stock"/>,
    /// supplying new values for use
    /// </summary>
    public static class StockMarket
    {

        /// <summary>
        /// Constructor for the static class, this sets new instances of the properties in the class.
        /// </summary>
        static StockMarket()
        {
            StockNotifications = false;
            PortfolioNotifications = true;
            Stocks = new HashSet<IStock>();
        }

        /// <summary>
        /// Enables / Disables <see cref="Stock"/> Notifications. This means that state: true will output <see cref="Stock"/> info the Console.
        /// false will disable this
        /// </summary>
        public static bool StockNotifications { get; set; }

        /// <summary>
        /// Enables / Disables <see cref="Portfolio"/> Notifications. This means that state: true will output <see cref="Portfolio"/> info the Console.
        /// false will disable this
        /// </summary>
        public static bool PortfolioNotifications { get; set; }

        /// <summary>
        /// Holds a HashSet of all available <see cref="Stock"/>
        /// </summary>
        public static HashSet<IStock> Stocks { get; private set; }

        /// <summary>
        /// Pseudo Observer like method that takes the internal state of a stock and
        /// prints the state if StockNotications are set to true.
        /// Taken as inspiration from global Stock boards.
        /// </summary>
        /// <param name="stock"></param>
        public static void ValueChanged(IStock stock)
        {
            if (StockNotifications)
            {
                Console.WriteLine("Value");
                Console.WriteLine($"- {stock.Name} changed to {stock.Value:0.##}\n");
            }
        }

        /// <summary>
        /// RefreshStocks will update Stock with new values.
        /// </summary>
        public static void RefreshStocks()
        {
            Stocks.ToList().ForEach(s => s.Instability());
        }

        /// <summary>
        /// This Method provides the opportunity to Sell <see cref="Stock"/> and thereby remove it from a selected <see cref="Portfolio"/>
        /// <see cref="Stock"/> is automatically set to GOOGLE
        /// </summary>
        /// <param name="portfolio">Chosen portfolio to sell stock from</param>
        public static void SellStock(IPortfolio portfolio)
        {
            // If stock is not initialized return. could be implemented with an exception
            if (Stocks == null)
                return;

            Console.WriteLine("-- StockMarket --");
            Console.WriteLine("Sell your stock");
            Console.WriteLine("Type the name of the stock to Select");

            foreach (var stockItem in portfolio.Stocks)
            {
                Console.WriteLine($"{stockItem.Key.Name} | {stockItem.Value}");
            }

            Console.WriteLine("--- ");
            Console.WriteLine();

            // Takes input from user.
            string input = Console.ReadLine();
            input = "Google";

            foreach (var stockItem in portfolio.Stocks.Keys)
            {
                if (input != stockItem.Name) continue;

                Console.WriteLine("How many would you like to sell?");
                var inputAmount = Console.ReadLine();

                Int32.TryParse(inputAmount,out int result);

                // Removes the specific amount of Stock
                if (inputAmount != null) portfolio.RemoveStock(stockItem, result);
                Console.WriteLine("Transaction was successful");
                return;
            }

            Console.WriteLine("The typed stock name wasn't valid, try again");
        }

        /// <summary>
        /// This Method provides the opportunity to buy <see cref="Stock"/> and thereby adding it to a selected <see cref="Portfolio"/>
        /// <see cref="Stock"/> is automatically set to GOOGLE
        /// </summary>
        /// <param name="portfolio">Chosen portfolio to buy stock to</param>
        public static void BuyStock(IPortfolio portfolio)
        {
            Console.WriteLine("-- StockMarket --");
            Console.WriteLine("Buy stock");
            Console.WriteLine("Type the name of the stock to Select");
            foreach (var stockItem in Stocks)
            {
                Console.WriteLine($"{stockItem.Name} | {stockItem.Value}");
            }

            Console.WriteLine();
            var input = Console.ReadLine();
            input = "Google";

            foreach (var stockItem in Stocks)
            {
                if (input == stockItem.Name)
                {
                    Console.WriteLine("How many would you like to buy?");
                    var inputAmount = Console.ReadLine();

                    Int32.TryParse(inputAmount, out int result);

                    // Adds the specific amount of Stock
                    if (inputAmount != null) portfolio.AddStock((Stock)stockItem, result);
                    Console.WriteLine("Transaction was successful");
                    return;
                }
            }

            Console.WriteLine("The typed stock name wasn't valid, try again");
        }
    }
}