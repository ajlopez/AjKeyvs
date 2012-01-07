namespace AjKeyvs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjKeyvs.Collections;

    public class Repository
    {
        HierarchicalDictionary<object> values = new HierarchicalDictionary<object>();

        public object GetValue(string key)
        {
            return this.values[key];
        }

        public void SetValue(string key, object value)
        {
            this.values[key] = value;
        }

        public void SetAddValue(string key, ulong value)
        {
            BigBitSet set = (BigBitSet)this.values[key];

            if (set == null)
            {
                set = new BigBitSet();
                this.values[key] = set;
            }

            set[value] = true;
        }

        public bool SetHasValue(string key, ulong value)
        {
            BigBitSet set = (BigBitSet)this.values[key];

            if (set == null)
                return false;

            return set[value];
        }

        public void SetRemoveValue(string key, ulong value)
        {
            BigBitSet set = (BigBitSet)this.values[key];

            if (set == null)
                return;

            set[value] = false;
        }
    }
}
