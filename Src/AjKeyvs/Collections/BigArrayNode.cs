﻿namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BigArrayNode<T> : IBigArrayNode<T>
    {
        private ulong from;
        private ulong to;
        private ushort level;
        private ushort size;
        private ulong divisor;
        private IBigArrayNode<T>[] subnodes;

        public BigArrayNode(ulong index, ushort size, ushort level, IBigArrayNode<T> subnode)
        {
            this.size = size;
            this.level = level;
            this.divisor = 1;

            for (ushort k = 0; k < this.level; k++)
                this.divisor *= this.size;

            ulong modulo = this.divisor * this.size;

            if (modulo == 0)
            {
                this.from = 0;
                this.to = ulong.MaxValue;
            }
            else
            {
                this.from = index - (index % modulo);
                this.to = this.from + (modulo - 1);
            }

            if (level == 1)
                this.subnodes = new BigArrayLeafNode<T>[size];
            else
                this.subnodes = new BigArrayNode<T>[size];
            
            if (this.subnodes != null)
                this.subnodes[this.GetSlotNumber(index)] = subnode;
        }

        public IBigArrayNode<T> SetValue(ulong index, T value)
        {
            if (this.from <= index && index <= this.to)
            {
                ushort position = this.GetSlotNumber(index);

                if (this.subnodes[position] == null)
                {
                    if (this.level == 1)
                        this.subnodes[position] = new BigArrayLeafNode<T>(index, this.size);
                    else
                        this.subnodes[position] = new BigArrayNode<T>(index, this.size, (ushort)(this.level - 1), null);
                }

                this.subnodes[position].SetValue(index, value);

                return this;
            }

            BigArrayNode<T> parent = new BigArrayNode<T>(this.from, this.size, (ushort)(this.level + 1), this);

            return parent.SetValue(index, value);
        }

        public T GetValue(ulong index)
        {
            if (this.from <= index && index <= this.to)
            {
                ushort position = this.GetSlotNumber(index);

                if (this.subnodes[position] == null)
                    return default(T);

                return this.subnodes[position].GetValue(index);
            }

            return default(T);
        }

        private ushort GetSlotNumber(ulong index)
        {
            return (ushort)((index - this.from) / this.divisor);
        }
    }
}
