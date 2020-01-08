using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class LabArray<T> : System.Collections.Generic.IList<T>
    {
        private T[] __arr;
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                    throw new ArgumentOutOfRangeException();
                return __arr[index];
            }
            set
            {
                if (index >= Count || index < 0)
                    throw new ArgumentOutOfRangeException();
                __arr[index] = value;
            }
        }

        public int Count
        {
            get => __arr.Length;
        }

        public bool IsReadOnly
        {
            get => __arr.IsReadOnly;
        }

        public void Add(T item)
        {
            if(!IsReadOnly)
            {
                T[] temp = new T[__arr.Length];
                temp = __arr;

                __arr = new T[temp.Length + 1];

                for (int i = 0; i < temp.Length; i++)
                    __arr[i] = temp[i];

                __arr[__arr.Length - 1] = item;
            }

            throw new NotSupportedException();

        }

        public void Clear()
        {
            if (!IsReadOnly)
                __arr = new T[0];
            throw new NotSupportedException();
                
        }

        public bool Contains(T item)
        {
            return __arr.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException();
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();
            if (array.Length - arrayIndex - 1 < __arr.Length)
                throw new ArgumentException();

            for (int i = arrayIndex; i < array.Length; i++)
                array[i] = __arr[i];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)__arr.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            for(int i = 0; i < __arr.Length; i++)
            {
                if (__arr[i].Equals(item))
                    return i;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= __arr.Length)
                throw new ArgumentOutOfRangeException();

            if (IsReadOnly)
                throw new NotSupportedException();

            __arr[index] = item;
        }

        public bool Remove(T item)
        {
            for(int i = 0; i < __arr.Length; i++)
            {
                if(__arr[i].Equals(item))
                {
                    for(int j = i; j < __arr.Length - 1; j++)
                    {
                        __arr[j] = __arr[j + 1];
                    }

                    T[] temp = new T[__arr.Length];
                    temp = __arr;
                    __arr = new T[__arr.Length - 1];

                    return true;
                }
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= __arr.Length)
                throw new ArgumentOutOfRangeException();
            if (IsReadOnly)
                throw new NotSupportedException();

            for (int i = index; i < __arr.Length - 1; i++)
                __arr[i] = __arr[i + 1];

            T[] temp = new T[__arr.Length];
            temp = __arr;

            __arr = new T[__arr.Length - 1];
            __arr = temp;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return  __arr.GetEnumerator();
        }
    }
}
