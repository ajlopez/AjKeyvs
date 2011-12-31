namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HierarchicalDictionary<T>
    {
        private IDictionary<string, T> values;

        public T this[string key]
        {
            get
            {
                if (IsSimpleKey(key))
                    return GetSimpleKeyValue(key);

                return default(T);
            }

            set
            {
                if (this.values == null)
                    this.values = new Dictionary<string, T>();

                this.values[key] = value;
            }
        }

        private static bool IsSimpleKey(string key)
        {
            return key.IndexOf(':') < 0;
        }

        private T GetSimpleKeyValue(string key)
        {
            if (this.values == null || !this.values.ContainsKey(key))
                return default(T);

            return this.values[key];
        }
    }
}
