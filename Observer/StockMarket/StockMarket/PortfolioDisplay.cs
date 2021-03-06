﻿using System;
using StockMarket.Interfaces;

namespace StockMarket
{
    public class PortfolioDisplay : IPortfolioDisplay
    {
        /// <summary>
        /// This method prints the internal state of the portfolio and acts as a view
        /// </summary>
        /// <code>
        /// Output
        /// |-----------------------------------------------------------------------------------------
        /// |      Name      |         Value        |    Amount   |              Total               |
        /// |-----------------------------------------------------------------------------------------
        /// |{stock.Key.Name}|{stock.Key.Value:0.##}|{stock.Value}|{stock.Value*stock.Key.Value:0.##}|
        /// |-----------------------------------------------------------------------------------------
        /// </code>
        /// <param name="portfolio"></param>
        public void PrintInformation(IPortfolio portfolio)
        {
            Console.WriteLine($"Displaying current portfolio: {portfolio.Name}...");
            Console.WriteLine();

            Console.WriteLine($" | {portfolio.Name} has {portfolio.Total:0.##} in total stock value");
            Console.WriteLine($" | List of Stock");
            Console.WriteLine(" |----------------------------------------");
            Console.WriteLine(" |  Name  |   Value   | Amount |  Total  |");
            Console.WriteLine(" |----------------------------------------");
            foreach (var stock in portfolio.Stocks)
            {
                Console.WriteLine($" | {stock.Key.Name} |  {stock.Key.Value:0.##}   |   {stock.Value}   | {stock.Value * stock.Key.Value:0.##} |");
                Console.WriteLine(" |----------------------------------------");
            }

            Console.WriteLine();
        }
    }
}