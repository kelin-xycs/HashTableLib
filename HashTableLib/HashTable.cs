using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableLib
{
    public class HashTable<TKey, TValue>
    {
        private HashTableInternal<TKey, TValue> hashTableInternal;

        private int initCapacity = 100;

        private double whenEnlarge = 0.8;
        private double enlargeRatio = 0.3;

        public HashTable()
        {
            this.hashTableInternal = new HashTableInternal<TKey, TValue>(this.initCapacity);
        }

        public HashTable(int initCapacity)
        {
            this.initCapacity = initCapacity;
            this.hashTableInternal = new HashTableInternal<TKey, TValue>(this.initCapacity);
        }

        public HashTable(int initCapacity, double whenEnlarge, double enlargeRatio)
        {
            this.initCapacity = initCapacity;
            this.hashTableInternal = new HashTableInternal<TKey, TValue>(this.initCapacity);

            this.whenEnlarge = whenEnlarge;
            this.enlargeRatio = enlargeRatio;
        }

        public uint Add(TKey key, TValue value)
        {
            if (this.hashTableInternal.Count / this.hashTableInternal.Capacity >= this.whenEnlarge)
            {
                EnlargeCapacity();
            }

            return this.hashTableInternal.Add(key, value);
        }

        private void EnlargeCapacity()
        {
            
            int oldCapacity = this.hashTableInternal.Capacity;

            int increament = (int)(oldCapacity * enlargeRatio);

            if (increament == 0)
                increament = 1;

            int newCapacity = oldCapacity + increament;

            var newHashTableInternal = new HashTableInternal<TKey, TValue>(newCapacity);

            foreach(var pair in this.hashTableInternal)
            {
                newHashTableInternal.Add(pair.Key, pair.Value);
            }

            this.hashTableInternal = newHashTableInternal;
        }

        public TValue this[ TKey key ]
        {
            get
            {
                return this.hashTableInternal[key];
            }
        }

        public void Remove(TKey key)
        {
            this.hashTableInternal.Remove(key);
        }

        public int Count
        {
            get { return this.hashTableInternal.Count; }
        }

        public int Capacity
        {
            get { return this.hashTableInternal.Capacity; }
        }

        public double WhenEnlarge
        {
            get { return this.whenEnlarge; }
        }

        public double EnlargeRatio
        {
            get { return this.enlargeRatio; }
        }
    }
}
