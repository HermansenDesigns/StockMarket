using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace StockMarket.Unit.Test
{
    [TestFixture()]
    public class StockTests
    {
        private Stock _uut;
        private IObserver _observer;

        [SetUp]
        public void Init()
        {
            _uut = new Stock("Name", 1M);
            _observer = Substitute.For<IObserver>();
        }

        [TestCase("Kasper")]
        [TestCase(" ")]
        [TestCase("")]
        [TestCase("awdabc")]
        [TestCase("!!##¤¤")]

        public void Stock_CanGetName(string input)
        {
            _uut = new Stock(input, 1M);
            Assert.That(_uut.Name, Is.EqualTo(input));
        }

        [TestCase((double)1, (double)1)]
        [TestCase((double)-1, (double)-1)]
        [TestCase((double)0.5, (double)0.5)]
        [TestCase((double)-0.5, (double)-0.5)]

        public void Stock_Value_Set_Value(decimal a, decimal expected)
        {
            _uut.Value = (decimal)a;
            Assert.That(_uut.Value, Is.EqualTo(expected));
        }

        [Test]
        public void Instability_DoesNotThrow()
        {
            Assert.DoesNotThrow(_uut.Instability);
        }
    }
}
