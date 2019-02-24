using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;

namespace HashTableLib
{
    internal class HashTableInternal<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {

        private ChainList<KeyValuePair<TKey, TValue>>[] arr;
        private ChainList<KeyValuePair<TKey, TValue>> list;

        private uint m;

        public HashTableInternal(int initCapacity)
        {
            this.arr = new ChainList<KeyValuePair<TKey, TValue>>[initCapacity];
            this.list = new ChainList<KeyValuePair<TKey, TValue>>();

            this.m = GetModulo();
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

            var pair = new KeyValuePair<TKey, TValue>(key, value);
            chainList.Add(pair);

            node = new ChainListNode<KeyValuePair<TKey, TValue>>(pair);
            pair.listNode = node;
            this.list.Add(node);

            return i;
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

            this.list.Remove(node.element.listNode);

        }

        public int Count
        {
            get { return this.list.count; }
        }

        public int Capacity
        {
            get { return this.arr.Length; }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new HashTableEnumerator<TKey, TValue>(this.list);
            //throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    internal class HashTableEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
    {

        private ChainList<KeyValuePair<TKey, TValue>> list;
        private ChainListNode<KeyValuePair<TKey, TValue>> current;

        public HashTableEnumerator(ChainList<KeyValuePair<TKey, TValue>> list)
        {
            this.list = list;
        }

        KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current
        {
            get 
            {
                return this.current.element;
                //throw new NotImplementedException(); 
            }
        }

        void IDisposable.Dispose()
        {
            //throw new NotImplementedException();
        }

        object System.Collections.IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

        bool System.Collections.IEnumerator.MoveNext()
        {
            if (this.current == null)
            {
                if (this.list.head == null)
                    return false;

                this.current = this.list.head;

                return true;
            }

            if (this.current.next == null)
                return false;

            this.current = this.current.next;

            return true;
                
            //throw new NotImplementedException();
        }

        void System.Collections.IEnumerator.Reset()
        {
            this.current = null;
            //throw new NotImplementedException();
        }
    }

    public class KeyValuePair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
        internal ChainListNode<KeyValuePair<TKey, TValue>> listNode;

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

            Add(node);
        }

        public void Add(ChainListNode<T> node)
        {
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
