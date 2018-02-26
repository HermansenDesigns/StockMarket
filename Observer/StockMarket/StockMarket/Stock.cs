using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarket
{
    public class Stock : Subject, IStock
    {
        /// <summary>
        /// This property holds the name definition of the class
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// This field holds the value of the stock and is of type decimal
        /// </summary>
        private decimal _value;

        /// <summary>
        /// This Property provides access to _value as is crucial for the Observer pattern
        /// </summary>
        /// <para>
        /// as this class inherits from Subject it has to notify the observers whenever a
        /// change has happened to the internal state
        /// </para>
        public decimal Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Notify();
                StockMarket.ValueChanged(this);
            }
        }

        /// <summary>
        /// The Constructor of the <see cref="Stock"/> sets the internal state and
        /// adds itself to <see cref="StockMarket"/>'s list of available Stock.
        /// </summary>
        /// <param name="name">Name definition of the <see cref="Stock"/></param>
        /// <param name="value">Value of the <see cref="Stock"/></param>
        public Stock(string name ,decimal value)
        {
            Name = name;
            Value = value;
            StockMarket.Stocks.Add(this);
        }

        /// <summary>
        /// Instability changes the internal value by maximum of +/- 5% at every call.
        /// ratio is determined by a <see cref="Random"/> Generator
        /// </summary>
        public void Instability()
        {
            Random rnd = new Random();
            Value = Value + Value * rnd.Next(-5, 5) / 100;
        }
    }
}