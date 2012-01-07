namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BigStringSet
    {
        private BigArray<IList<string>> members;

        public bool this[string index]
        {
            get
            {
                if (this.members == null)
                    return false;

                var list = this.members[GetPosition(index)];

                if (list == null)
                    return false;

                return list.Contains(index);
            }

            set
            {
                if (this.members == null)
                    this.members = new BigArray<IList<string>>();

                ulong position = GetPosition(index);

                var list = this.members[position];

                if (list == null)
                {
                    list = new List<string>();
                    this.members[position] = list;
                }

                if (list.Contains(index))
                    return;

                list.Add(index);
            }
        }

        private static ulong GetPosition(string text)
        {
            int hash = text.GetHashCode();

            if (hash < 0)
                hash = -hash;

            return (ulong) hash;
        }
    }
}
