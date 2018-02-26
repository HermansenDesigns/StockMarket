using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace StockMarket
{
    public static class StockMarket
    {
        static StockMarket()
        {
            StockNotifications = false;
            PortfolioNotifications = true;
            Stocks = new HashSet<IStock>();
        }

        public static bool StockNotifications { get; set; }
        public static bool PortfolioNotifications { get; set; }


        public static HashSet<IStock> Stocks { get; private set; }
        public static void ValueChanged(IStock stock)
        {
            if (StockNotifications)
            {
                Console.WriteLine("Value");
                Console.WriteLine($"- {stock.Name} changed to {stock.Value:0.##}\n");
            }
        }

        public static void RefreshStocks()
        {
            foreach (var stock in Stocks)
            {
                stock.Instability();
            }
        }

        public static void SellStock(IPortfolio portfolio)
        {
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

            string input = Console.ReadLine();
            input = "Google";

            foreach (var stockItem in portfolio.Stocks.Keys)
            {
                if (input != stockItem.Name) continue;

                Console.WriteLine("How many would you like to sell?");
                var inputAmount = Console.ReadLine();

                Int32.TryParse(inputAmount,out int result);
                if (inputAmount != null) portfolio.RemoveStock(stockItem, result);
                Console.WriteLine("Transaction was successful");
                return;
            }

            Console.WriteLine("The typed stock name wasn't valid, try again");
        }

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
            foreach (var stockItem in Stocks)
            {
                if (input == stockItem.Name)
                {
                    Console.WriteLine("How many would you like to buy?");
                    var inputAmount = Console.ReadLine();

                    Int32.TryParse(inputAmount, out int result);
                    if (inputAmount != null) portfolio.AddStock((Stock)stockItem, result);
                    Console.WriteLine("Transaction was successful");
                    return;
                }
            }

            Console.WriteLine("The typed stock name wasn't valid, try again");
        }
    }
}