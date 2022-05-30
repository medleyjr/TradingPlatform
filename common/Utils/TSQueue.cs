using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Medley.Common.Utils
{
    /// <summary>
    /// A Thread safe Queue class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TSQueue<T> : Queue<T>
    {
        private object syncDataSendList = new object();

        public bool HasDataTS
        {
            get
            {
                bool bHasData;
                lock (syncDataSendList)
                {
                    bHasData = (base.Count > 0);
                }
                return bHasData;
            }
        }

        public T FirstItemTS()
        {
            T firstItem;

            lock (syncDataSendList)
            {
                firstItem = base.Peek();
            }

            return firstItem;
        }

        /// <summary>
        /// Remove and return the oldest item in the list
        /// </summary>
        /// <returns></returns>
        public T DequeueTS()
        {
            T firstItem;

            lock (syncDataSendList)
            {
                firstItem = base.Dequeue();
            }

            return firstItem;
        }      

        public void AddItemTS(T item)
        {
            lock (syncDataSendList)
            {
                base.Enqueue(item);
            }
        }
    }
}
