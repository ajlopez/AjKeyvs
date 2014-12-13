namespace AjKeyvs.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class CommandInfo
    {
        private string verb;
        private string key;
        private IList<object> parameters;

        public CommandInfo(string verb, string key, IEnumerable<object> parameters)
        {
            this.verb = verb;
            this.key = key;

            if (parameters != null)
                this.parameters = new List<object>(parameters);
        }

        public string Verb { get { return this.verb; } }

        public string Key { get { return this.key; } }

        public IList<object> Parameters { get { return this.parameters; } }

        public void CheckArity(int arity)
        {
            if (arity == 0 && this.parameters != null && this.parameters.Count != 0)
                throw new InvalidDataException("0 parameters expected");

            if (arity != 0 && (this.parameters == null || this.parameters.Count != arity))
                throw new InvalidDataException(string.Format("{0} parameters expected", arity));
        }
    }
}
