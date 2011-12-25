namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BigArray<T>
    {
        private ushort nodesize;
        private IBigArrayNode<T> root;

        public BigArray()
            : this(256)
        {
        }

        public BigArray(ushort nodesize)
        {
            this.nodesize = nodesize;
        }

        public T this[ulong index]
        {
            get
            {
                if (this.root == null)
                    return default(T);

                return this.root.GetValue(index);
            }

            set
            {
                if (this.root == null)
                    this.root = new BigArrayLeafNode<T>(index, this.nodesize);

                this.root = this.root.SetValue(index, value);
            }
        }
    }
}
