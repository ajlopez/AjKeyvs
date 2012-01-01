namespace AjKeyvs.Server.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class CommandReader
    {
        private Tokenizer tokenizer;

        public CommandReader(string text)
            : this(new StringReader(text))
        {
        }

        public CommandReader(TextReader reader)
        {
            this.tokenizer = new Tokenizer(reader);
        }

        public Command NextCommand()
        {
            Token token = this.tokenizer.NextToken();

            if (token == null)
                return null;

            if (token.Type != TokenType.Name)
                throw new InvalidDataException("Invalid Verb");

            string verb = token.Value;

            token = this.tokenizer.NextToken();

            if (token == null)
                throw new InvalidDataException("No Key in Command");

            if (token.Type != TokenType.Name)
                throw new InvalidDataException("Invalid Key");

            string key = token.Value;

            return new Command(verb, key, null);
        }
    }
}

