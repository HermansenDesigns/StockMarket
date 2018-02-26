using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarket
{
    public class Stock : Subject
    {
        public string Name { get; private set; }
        private decimal _value;
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

        public Stock(string name ,decimal value)
        {
            Name = name;
            Value = value;
            StockMarket.Stocks.Add(this);
        }

        public void Instability()
        {
            Random rnd = new Random();
            Value = Value + Value * rnd.Next(-5, 5) / 100;
        }


    }
}