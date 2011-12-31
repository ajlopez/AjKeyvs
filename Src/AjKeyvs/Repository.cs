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
            return values[key];
        }

        public void SetValue(string key, object value)
        {
            values[key] = value;
        }
    }
}
