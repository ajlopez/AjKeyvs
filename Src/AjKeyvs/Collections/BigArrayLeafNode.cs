namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BigArrayLeafNode<T> : IBigArrayNode<T>
    {
        private ulong from;
        private ushort size;
        private T[] values;

        public BigArrayLeafNode(ulong index, ushort size)
        {
            this.from = index - (index % size);
            this.size = size;
            this.values = new T[size];
        }

        public IBigArrayNode<T> SetValue(ulong index, T value)
        {
            if (this.from <= index && index <= this.from + (ulong) (this.size - 1))
            {
                this.values[index - this.from] = value;
                return this;
            }

            BigArrayNode<T> parent = new BigArrayNode<T>(this.from, this.size, 1, this);

            return parent.SetValue(index, value);
        }

        public T GetValue(ulong index)
        {
            if (this.from <= index && index <= this.from + (ulong)(this.size - 1))          
                return this.values[index - this.from];

            return default(T);
        }
    }
}

