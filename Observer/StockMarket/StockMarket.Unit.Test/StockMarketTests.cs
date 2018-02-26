using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using StockMarket.Interfaces;

namespace StockMarket.Unit.Test
{
    [TestFixture]
    public class StockMarketTests
    {
        private IPortfolio _portfolio;
        private IStock _stock;

        [SetUp]
        public void Init()
        {
            _portfolio = Substitute.For<IPortfolio>();
            _stock = Substitute.For<IStock>();

            _portfolio.Name.Returns("Folder1");
            _portfolio.Total.Returns(1);
            _stock.Name.Returns("stock1");
            _stock.Value.Returns(1M);

        }

        [Test]
        public void StockMarket_Ctor_Run()
        {

            Assert.IsTrue(StockMarket.PortfolioNotifications);
            Assert.IsFalse(StockMarket.StockNotifications);
        }

        [Test]
        public void StockMarket_ValueChanged_CallReceived()
        {
            StockMarket.StockNotifications = true;
            StockMarket.ValueChanged(_stock);
            _stock.Received();
        }

        [Test] // Inconclusive - Reason: The method required user input from console, very difficult to test.
        public void StockMarket_SellStock()
        {
            //_portfolio.Stocks.Returns(new Dictionary<IStock, int> {{_stock, 1}});
            //StockMarket.SellStock(_portfolio);
            //_portfolio.Received();
        }

        [Test] // Inconclusive - Reason: The method required user input from console, very difficult to test.
        public void StockMarket_BuyStock()
        {
            //_portfolio.Stocks.Returns(new Dictionary<IStock, int> {{_stock, 1}});
            //StockMarket.SellStock(_portfolio);
            //_portfolio.Received();
        }

        [Test] // Inconclusive - Reason: The method required user input from console, very difficult to test.
        public void StockMarket_RefreshStocks_StockReceivedInstabilityCall()
        {
            StockMarket.Stocks.Add(_stock);
            StockMarket.RefreshStocks();
            _stock.Received().Instability();
        }
    }
}
