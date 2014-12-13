namespace AjKeyvs.Server.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class CommandReader
    {
        private Tokenizer tokenizer;
        private bool islinereader;
        private bool eof;

        public CommandReader(string text)
            : this(new StringReader(text))
        {
        }

        public CommandReader(TextReader reader)
            : this(reader, false)
        {
        }

        public CommandReader(TextReader reader, bool islinereader)
        {
            this.tokenizer = new Tokenizer(reader);
            this.islinereader = islinereader;
        }

        public CommandInfo NextCommand()
        {
            if (this.eof)
                return null;

            Token token = this.tokenizer.NextToken();

            if (token == null)
                return null;

            if (token.Type != TokenType.Name)
                throw new InvalidDataException("Invalid Verb");

            if (token.Value == "exit")
                return null;

            string verb = token.Value;

            token = this.tokenizer.NextToken();

            if (token == null)
                throw new InvalidDataException("No Key in Command");

            if (token.Type != TokenType.Name)
                throw new InvalidDataException("Invalid Key");

            string key = token.Value;

            IList<object> parameters = null;

            token = this.tokenizer.NextToken();

            while (token != null && token.Type != TokenType.EndOfLine)
            {
                if (parameters == null)
                    parameters = new List<object>();

                if (token.Type == TokenType.Integer)
                    parameters.Add(ulong.Parse(token.Value));
                else
                    parameters.Add(token.Value);

                token = this.tokenizer.NextToken();
            }

            if (token == null)
                this.eof = true;
            else if (this.islinereader && token.Type == TokenType.EndOfLine)
                this.eof = true;

            return new CommandInfo(verb, key, parameters);
        }
    }
}

