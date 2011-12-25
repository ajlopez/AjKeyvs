namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BigArray<T>
    {
        private ushort nodesize;
        private BigArrayLeafNode<T> root;

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
                if (root == null)
                    return default(T);

                return root.GetValue(index);
            }

            set
            {
                if (root == null)
                    root = new BigArrayLeafNode<T>(index, this.nodesize);

                root.SetValue(index, value);
            }
        }
    }
}
