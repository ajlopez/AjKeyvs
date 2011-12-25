namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class BigArrayLeafNode<T>
    {
        private ulong from;
        private ushort size;
        private T[] values;

        internal BigArrayLeafNode(ulong index, ushort size)
        {
            this.from = index - (index % size);
            this.size = size;
            this.values = new T[size];
        }

        internal void SetValue(ulong index, T value)
        {
            this.values[index] = value;
        }

        internal T GetValue(ulong index)
        {
            return this.values[index - this.from];
        }
    }
}

