namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BigStringSet
    {
        private BigArray<IList<string>> members;

        public bool HasMember(string member)
        {
            if (this.members == null)
                return false;

            var list = this.members[GetPosition(member)];

            if (list == null)
                return false;

            return list.Contains(member);
        }

        public void AddMember(string member)
        {
            if (this.members == null)
                this.members = new BigArray<IList<string>>();

            ulong position = GetPosition(member);

            var list = this.members[position];

            if (list == null)
            {
                list = new List<string>();
                this.members[position] = list;
            }

            if (list.Contains(member))
                return;

            list.Add(member);
        }

        public void RemoveMember(string member)
        {
            if (this.members == null)
                return;

            ulong position = GetPosition(member);

            var list = this.members[position];

            if (list == null)
                return;

            if (list.Contains(member)) 
            {
                list.Remove(member);

                if (list.Count == 0)
                    this.members[position] = null;
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
