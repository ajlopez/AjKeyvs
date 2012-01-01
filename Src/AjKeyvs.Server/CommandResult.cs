namespace AjKeyvs.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CommandResult
    {
        private bool hasvalue;
        private object value;

        public CommandResult()
        {
        }

        public CommandResult(object value)
        {
            this.value = value;
            this.hasvalue = true;
        }

        public bool HasValue { get { return this.hasvalue; } }

        public object Value { get { return this.value; } }
    }
}
