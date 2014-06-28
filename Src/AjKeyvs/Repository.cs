namespace AjKeyvs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjKeyvs.Collections;

    public class Repository
    {
        private HierarchicalDictionary<object> values = new HierarchicalDictionary<object>();

        public object GetValue(string key)
        {
            return this.values[key];
        }

        public void SetValue(string key, object value)
        {
            this.values[key] = value;
        }

        public void SetAddMember(string key, ulong member)
        {
            BigBitSet set = (BigBitSet)this.values[key];

            if (set == null)
            {
                set = new BigBitSet();
                this.values[key] = set;
            }

            set[member] = true;
        }

        public void SetAddMember(string key, string member)
        {
            BigStringSet set = (BigStringSet)this.values[key];

            if (set == null)
            {
                set = new BigStringSet();
                this.values[key] = set;
            }

            set.AddMember(member);
        }

        public bool SetHasMember(string key, ulong member)
        {
            BigBitSet set = (BigBitSet)this.values[key];

            if (set == null)
                return false;

            return set[member];
        }

        public bool SetHasMember(string key, string member)
        {
            BigStringSet set = (BigStringSet)this.values[key];

            if (set == null)
                return false;

            return set.HasMember(member);
        }

        public void SetRemoveMember(string key, ulong member)
        {
            BigBitSet set = (BigBitSet)this.values[key];

            if (set == null)
                return;

            set[member] = false;
        }

        public void SetRemoveMember(string key, string member)
        {
            BigStringSet set = (BigStringSet)this.values[key];

            if (set == null)
                return;

            set.RemoveMember(member);
        }
    }
}
