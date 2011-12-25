namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IBigArrayNode<T>
    {
        IBigArrayNode<T> SetValue(ulong index, T value);

        T GetValue(ulong index);
    }
}
