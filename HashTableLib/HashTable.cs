using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace HashTableLib
{
    public class HashTable<TKey, TValue>
    {

        private ChainList<KeyValuePair<TKey, TValue>>[] arr;

        private const int _defaultInitCapacity = 128;
        private uint m;

        private double whenEnlarge = 0.8;
        private double enlargeRatio = 0.3;

        private int count;

        public HashTable()
        {
            this.arr = new ChainList<KeyValuePair<TKey, TValue>>[_defaultInitCapacity];

            this.m = GetModulo();
        }

        public HashTable(int initCapacity, double whenEnlarge, double enlargeRatio)
        {
            this.arr = new ChainList<KeyValuePair<TKey, TValue>>[initCapacity];

            this.m = GetModulo();

            this.whenEnlarge = whenEnlarge;
            this.enlargeRatio = enlargeRatio;
        }

        private uint GetModulo()
        {
            uint result = uint.MaxValue / (uint)this.arr.Length;

            if (uint.MaxValue % (uint)this.arr.Length > 0)
                result++;

            return result;
        }

        public uint Add(TKey key, TValue value)
        {
            if (this.count / this.arr.Length >= this.whenEnlarge)
            {
                EnlargeCapacity();
            }

            uint i;
            ChainList<KeyValuePair<TKey, TValue>> chainList;

            var node = GetNode(key, out i, out chainList);

            if (node != null)
                throw new DuplicateKeyException("Key \"" + node.element.Key + "\" 已经存在 。");

            if (chainList == null)
            {
                chainList = new ChainList<KeyValuePair<TKey, TValue>>();
                this.arr[i] = chainList;
            }

            chainList.Add(new KeyValuePair<TKey, TValue>(key, value));

            this.count++;

            return i;
        }

        private void EnlargeCapacity()
        {
            var oldArr = this.arr;

            uint increament = (uint)(this.arr.Length * enlargeRatio);

            if (increament < 1)
                increament = 1;

            uint newCapacity = (uint)this.arr.Length + increament;

            var newArr = new ChainList<KeyValuePair<TKey, TValue>>[newCapacity];

            oldArr.CopyTo(newArr, 0);

            this.arr = newArr;
        }

        public TValue this[ TKey key ]
        {
            get 
            {
                uint i;
                ChainList<KeyValuePair<TKey, TValue>> chainList;

                var node = GetNode(key, out i, out chainList);
                    
                if (node == null)
                    throw new KeyNotFoundException("找不到 Key 是 \"" + key + "\" 的 键值对 。");

                return node.element.Value;
            }
        }

        private ChainListNode<KeyValuePair<TKey, TValue>> GetNode(TKey key, out uint i, out ChainList<KeyValuePair<TKey, TValue>> chainList)
        {
            uint hash = (uint)key.GetHashCode();

            i = hash / m;

            chainList = arr[i];

            if (chainList == null)
            {
                return null;
            }

            var node = chainList.head;

            if (node == null)
                return null;

            while (true)
            {
                if (node.element.Key.Equals(key))
                    return node;

                node = node.next;

                if (node == null)
                    return null;
            }
        }

        public void Remove(TKey key)
        {
            uint i;
            ChainList<KeyValuePair<TKey, TValue>> chainList;

            var node = GetNode(key, out i, out chainList);

            if (node == null)
                return;

            chainList.Remove(node);

            if (chainList.count == 0)
            {
                this.arr[i] = null;
            }

            this.count--;
        }

        public int Count
        {
            get { return this.count; }
        }

        public int Capacity
        {
            get { return this.arr.Length; }
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

    public class KeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public KeyValuePair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }
    }

    internal class ChainList<T>
    {
        public int count;

        public ChainListNode<T> head;
        public ChainListNode<T> tail;

        public void Add(T element)
        {
            var node = new ChainListNode<T>(element);

            if (this.count == 0)
            {
                this.head = node;
                this.tail = node;

                this.count++;

                return;
            }

            node.before = this.tail;
            this.tail.next = node;
            this.tail = node;

            this.count++;

        }

        public void Remove(ChainListNode<T> node)
        {
            if (node == this.head && node == this.tail)
            {
                this.head = null;
                this.tail = null;

                this.count--;

                return;
            }

            if (node == this.tail)
            {
                this.tail = node.before;

                node.before.next = null;
                node.before = null;

                this.count--;

                return;
            }

            if (node == this.head)
            {
                if (node.next == null)
                {
                    this.head = null;
                    this.tail = null;

                    this.count = 0;

                    return;
                }

                this.head = node.next;

                node.next.before = null;
                node.next = null;

                this.count--;

                return;
            }

            node.before.next = node.next;
            node.next.before = node.before;
            node.before = null;
            node.next = null;

            this.count--;
        }
    }

    internal class ChainListNode<T>
    {

        public ChainListNode<T> before;
        public ChainListNode<T> next;

        public T element;

        public ChainListNode(T element)
        {
            this.element = element;
        }
    }
}
