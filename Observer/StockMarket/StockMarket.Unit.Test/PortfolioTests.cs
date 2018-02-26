using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using StockMarket.Interfaces;

namespace StockMarket.Unit.Test
{
    [TestFixture]
    public class PortfolioTests
    {
        private IPortfolio _uut;
        private IObserver _observer;
        private IPortfolioDisplay _portfolioDisplay;
        private Stock _stock;

        [SetUp]
        public void Init()
        {
            _portfolioDisplay = Substitute.For<IPortfolioDisplay>();
            _stock = Substitute.For<Stock>("Name", 1.0M);

            _uut = new Portfolio("Name", _portfolioDisplay);
            _observer = Substitute.For<IObserver>();
        }

        [Test]
        public void Portfolio_Ctor_NoThrow()
        {
            _uut = new Portfolio("Name", _portfolioDisplay);
            Assert.That(_uut.Name, Is.EqualTo(_uut.Name));
            Assert.That(_uut.PortfolioDisplay, Is.EqualTo(_portfolioDisplay));
            Assert.IsEmpty(_uut.Stocks);
        }

        [Test]
        public void Portfolio_Ctor_ThrowsArgumentNullException()
        {
            try
            {
                new Portfolio("Bla", null);
            }
            catch (ArgumentNullException e)
            {

            }
        }

        [Test]
        public void Portfolio_Total_returns1()
        {
            _uut.AddStock(_stock, 1);
            Assert.That(_uut.Total, Is.EqualTo(1));
        }

        [Test]
        public void Portfolio_AddStock_Empty()
        {
            _uut.AddStock(_stock,1);
            Assert.IsTrue(_uut.Stocks.ContainsKey(_stock));
        }

        [Test]
        public void Portfolio_AddStock_NonEmpty()
        {
            _uut.AddStock(_stock, 1);
            _uut.AddStock(_stock, 1);
            Assert.IsTrue(_uut.Stocks.ContainsKey(_stock));
        }

        [Test]
        public void Portfolio_AddStock_ThrowsArgumentException()
        {
            Assert.Catch<ArgumentException>(() => _uut.AddStock(_stock, 0));
        }

        [Test]
        public void Portfolio_RemoveStock_NonEmpty()
        {
            _uut.AddStock(_stock, 2);
            _uut.RemoveStock(_stock, 1);
            Assert.IsTrue(_uut.Stocks.ContainsKey(_stock));
        }

        [Test]
        public void Portfolio_RemoveStock_Empty()
        {
            _uut.AddStock(_stock, 1);
            _uut.RemoveStock(_stock, 1);
            Assert.IsEmpty(_uut.Stocks);
        }

        [Test]
        public void Portfolio_RemoveStock_DoesntExist()
        {
            _uut.RemoveStock(_stock, 1);
            Assert.IsEmpty(_uut.Stocks);
        }

        [Test]
        public void Portfolio_RemoveStock_ThrowsArgumentException()
        {
            Assert.Catch<ArgumentException>(() => _uut.RemoveStock(_stock, 0));
        }
    }
}
