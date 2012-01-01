namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HierarchicalDictionary<T>
    {
        private IDictionary<string, T> values;
        private BigArray<T> valuearray;
        private IDictionary<string, HierarchicalDictionary<T>> subdictionaries;
        private BigArray<HierarchicalDictionary<T>> dictionaryarray;

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
                    SetSimpleKeyValue(key, value);
                else
                    SetCompositeKeyValue(key, value);
            }
        }

        private static bool IsSimpleKey(string key)
        {
            return key.IndexOf(':') < 0;
        }

        private void SetSimpleKeyValue(string key, T value)
        {
            ulong lkey;

            if (ulong.TryParse(key, out lkey))
            {
                SetSimpleKeyValue(lkey, value);
                return;
            }

            if (this.values == null)
                this.values = new Dictionary<string, T>();

            this.values[key] = value;
        }

        private void SetSimpleKeyValue(ulong key, T value)
        {
            if (this.valuearray == null)
                this.valuearray = new BigArray<T>();

            this.valuearray[key] = value;
        }

        private void SetCompositeKeyValue(string key, T value)
        {
            int split = key.IndexOf(':');
            string subkey = key.Substring(0, split);
            string restkey = key.Substring(split + 1);

            ulong lkey;

            if (ulong.TryParse(subkey, out lkey))
            {
                SetCompositeKeyValue(lkey, restkey, value);
                return;
            }

            if (this.subdictionaries == null)
                this.subdictionaries = new Dictionary<string, HierarchicalDictionary<T>>();

            if (!this.subdictionaries.ContainsKey(subkey))
                this.subdictionaries[subkey] = new HierarchicalDictionary<T>();

            this.subdictionaries[subkey][restkey] = value;
        }

        private void SetCompositeKeyValue(ulong lkey, string restkey, T value)
        {
            if (this.dictionaryarray == null)
                this.dictionaryarray = new BigArray<HierarchicalDictionary<T>>();

            if (this.dictionaryarray[lkey] == null)
                this.dictionaryarray[lkey] = new HierarchicalDictionary<T>();

            this.dictionaryarray[lkey][restkey] = value;
        }

        private T GetSimpleKeyValue(string key)
        {
            ulong lkey;

            if (ulong.TryParse(key, out lkey))
                return GetSimpleKeyValue(lkey);

            if (this.values == null || !this.values.ContainsKey(key))
                return default(T);

            return this.values[key];
        }

        private T GetSimpleKeyValue(ulong lkey)
        {
            if (this.valuearray == null)
                return default(T);

            return this.valuearray[lkey];
        }

        private T GetCompositeKeyValue(string key)
        {
            int split = key.IndexOf(':');
            string subkey = key.Substring(0, split);
            string restkey = key.Substring(split + 1);

            ulong lkey;

            if (ulong.TryParse(subkey, out lkey))
                return GetCompositeKeyValue(lkey, restkey);

            if (this.subdictionaries == null)
                return default(T);

            if (!this.subdictionaries.ContainsKey(subkey))
                return default(T);

            return this.subdictionaries[subkey][restkey];
        }

        private T GetCompositeKeyValue(ulong lkey, string restkey)
        {
            if (this.dictionaryarray == null)
                return default(T);

            return this.dictionaryarray[lkey][restkey];
        }
    }
}
