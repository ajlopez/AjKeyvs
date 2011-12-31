namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HierarchicalDictionary<T>
    {
        private IDictionary<string, T> values;
        private IDictionary<string, HierarchicalDictionary<T>> subdictionaries;

        public T this[string key]
        {
            get
            {
                if (IsSimpleKey(key))
                    return GetSimpleKeyValue(key);
                return GetCompositeKeyValue(key);
            }

            set
            {
                if (IsSimpleKey(key))
                    SetSimpleyKeyValue(key, value);
                else
                    SetCompositeKeyValue(key, value);
            }
        }

        private static bool IsSimpleKey(string key)
        {
            return key.IndexOf(':') < 0;
        }

        private void SetSimpleyKeyValue(string key, T value)
        {
            if (this.values == null)
                this.values = new Dictionary<string, T>();

            this.values[key] = value;
        }

        private void SetCompositeKeyValue(string key, T value)
        {
            int split = key.IndexOf(':');
            string subkey = key.Substring(0, split);
            string restkey = key.Substring(split + 1);

            if (this.subdictionaries == null)
                this.subdictionaries = new Dictionary<string, HierarchicalDictionary<T>>();

            if (!this.subdictionaries.ContainsKey(subkey))
                this.subdictionaries[subkey] = new HierarchicalDictionary<T>();

            this.subdictionaries[subkey][restkey] = value;
        }

        private T GetSimpleKeyValue(string key)
        {
            if (this.values == null || !this.values.ContainsKey(key))
                return default(T);

            return this.values[key];
        }

        private T GetCompositeKeyValue(string key)
        {
            if (this.subdictionaries == null)
                return default(T);

            int split = key.IndexOf(':');
            string subkey = key.Substring(0, split);
            string restkey = key.Substring(split + 1);

            if (!this.subdictionaries.ContainsKey(subkey))
                return default(T);

            return this.subdictionaries[subkey][restkey];
        }
    }
}
