using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace StockMarket.Unit.Test
{
    [TestFixture]
    public class PortfolioDisplayTests
    {
        private IPortfolio _portfolio;
        private IStock _stock;
        private PortfolioDisplay _uut;

        [SetUp]
        public void Init()
        {
            _portfolio = Substitute.For<IPortfolio>();
            _stock = Substitute.For<IStock>();

            _uut = new PortfolioDisplay();

            _portfolio.Name.Returns("Folder1");
            _portfolio.Total.Returns(1);

            var tempDic = new Dictionary<IStock, int>();
            tempDic.Add(_stock, 40);

            _portfolio.Stocks.Returns(tempDic);

        }


        [Test]
        public void PortfolioDisplay_CanAccessPortfolio()
        {
            
            _uut.PrintInformation(_portfolio);
            _portfolio.Received();
        }
    }
}
