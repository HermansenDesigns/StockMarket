using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockMarket
{
    class Program
    {
        static void Main(string[] args)
        {
            var pd = new PortfolioDisplay();
            var portfolios = new List<Portfolio>();
            var myPortfolio = new Portfolio("MyPortfolio",pd);
            var hisPortfolio = new Portfolio("HisPortfolio", pd);
            portfolios.Add(myPortfolio);
            portfolios.Add(hisPortfolio);


            var googleStock = new Stock("Google" , 200.25M);
            var vestasStock = new Stock("Vestas", 150.75M);

            myPortfolio.AddStock(googleStock, 51);
            myPortfolio.AddStock(vestasStock, 95);
            hisPortfolio.AddStock(googleStock, 22);

            while (true)
            {
                StockMarket.RefreshStocks();

                Console.WriteLine("Select a portfolio");
                foreach (var portfolio in portfolios)
                {
                    Console.WriteLine($"{portfolio.Name}");
                }

                Console.WriteLine("Type the name of the portfolio to select it");
                var input = Console.ReadLine();
                var index = -1;
                foreach (var portfolio in portfolios)
                {
                    if (input == portfolio.Name)
                    {
                        index = portfolios.IndexOf(portfolio);
                    }
                }

                if (index == -1) continue;
                Console.WriteLine();
                bool cont = true;
                while (cont)
                {
                    Console.WriteLine("Type 'B' to Buy, 'S' To Sell or C to Cancel");
                    char transactionInput = (char)Console.Read();

                    switch (transactionInput)
                    {
                        case 'B':
                            StockMarket.BuyStock(portfolios[index]);
                            break;
                        case 'S':
                            StockMarket.SellStock(portfolios[index]);
                            break;
                        case 'C':
                            cont = false;
                            break;
                        default:
                            Console.WriteLine();
                            break;
                    }
                }

            }
        }
    }
}
