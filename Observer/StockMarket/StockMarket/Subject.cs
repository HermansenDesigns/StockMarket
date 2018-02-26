using System.Collections.Generic;
using System.Linq;
using StockMarket.Interfaces;

namespace StockMarket
{

    /// <summary>
    /// Subject pattern than is used in conjunction with the observer pattern. This class implements the methods allowing it to register and contain Observer objects.
    /// </summary>
    public class Subject
    {
        #region ISubject Members

        /// <summary>
        /// The variable that holds the collection of Observers registered
        /// </summary>
        /// <para>
        /// The HashSet holds unique values and is perfect to disallow the option of
        /// having multiple of the same <see cref="IObserver"/> objects.
        /// </para>
        private HashSet<IObserver> _observers = new HashSet<IObserver>();

        /// <summary>
        /// Allowing Observers to subscribe to the <see cref="Subject"/>.
        /// </summary>
        /// <param name="observer"></param>
        public void Register(IObserver observer)
        {
            _observers.Add(observer);
        }

        /// <summary>
        /// Allows Observers to unsubscribe to the <see cref="Subject"/>
        /// </summary>
        /// <param name="observer"></param>
        public void Unregister(IObserver observer)
        {
            _observers.Remove(observer);
        }

        /// <summary>
        /// Call to notify all Observers that an internal state has changed,
        /// uses the Push methodology as a baseline, but can be used for Pull
        /// </summary>
        public void Notify()
        {
            // Linq foreach loop to notify that the state of object has changed
            _observers.ToList().ForEach(o => o.ValueChanged(this));
        }

        #endregion
    }
}