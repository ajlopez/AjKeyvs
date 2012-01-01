namespace AjKeyvs.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Command
    {
        private string verb;
        private string key;
        private IList<object> parameters;

        public Command(string verb, string key, IEnumerable<object> parameters)
        {
            this.verb = verb;
            this.key = key;

            if (parameters != null)
                this.parameters = new List<object>(parameters);
        }

        public string Verb { get { return this.verb; } }

        public string Key { get { return this.key; } }

        public IList<object> Parameters { get { return this.parameters; } }
    }
}
