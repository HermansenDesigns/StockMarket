namespace StockMarket
{
    public class Subject
    {
        #region ISubject Members

        private HashSet<IObserver> _observers = new HashSet<IObserver>();

        public void Register(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unregister(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            _observers.ToList().ForEach(o => o.ValueChanged(this));
        }

        #endregion
    }
}