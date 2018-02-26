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
    public class SubjectTests
    {
        private Subject _uut;
        private IObserver _observer;

        [SetUp]
        public void Init()
        {
            _uut = new Subject();
            _observer = Substitute.For<IObserver>();
        }

        [Test]
        public void Subject_Can_Register()
        {
            // Act
            _uut.Register(_observer);
            _uut.Notify();
            _observer.Received().ValueChanged(_uut);
        }

        [Test]
        public void Subject_Can_Unregister()
        {
            // Act
            _uut.Register(_observer);
            _uut.Unregister(_observer);
            _uut.Notify();
            _observer.DidNotReceive().ValueChanged(_uut);
        }
    }
}
