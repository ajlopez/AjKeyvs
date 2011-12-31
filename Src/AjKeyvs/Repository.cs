namespace AjKeyvs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Repository
    {
        IDictionary<string, object> values = new Dictionary<string, object>();

        public object GetValue(string key)
        {
            if (values.ContainsKey(key))
                return values[key];

            return null;
        }

        public void SetValue(string key, object value)
        {
            values[key] = value;
        }
    }
}
