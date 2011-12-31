using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AjKeyvs.Server.Parser;

namespace AjKeyvs.Server
{
    public class Processor
    {
        private Tokenizer tokenizer;
        private Repository repository;

        public Processor(Repository repository, string text)
            : this(repository, new StringReader(text))
        {
        }

        public Processor(Repository repository, TextReader reader)
        {
            this.repository = repository;
            this.tokenizer = new Tokenizer(reader);
        }

        public void Process()
        {
            string command = this.GetName();
            string key = this.GetName();
            ulong value = this.GetIntegerValue();
            this.GetEndOfCommand();

            if (command == "set")
                this.repository.SetValue(key, value);
        }

        private string GetName()
        {
            Token token = this.tokenizer.NextToken();

            if (token == null || token.Type != TokenType.Name)
                throw new InvalidDataException();

            return token.Value;
        }

        private ulong GetIntegerValue()
        {
            Token token = this.tokenizer.NextToken();

            if (token == null || token.Type != TokenType.Integer)
                throw new InvalidDataException();

            return ulong.Parse(token.Value);
        }

        private void GetEndOfCommand()
        {
            Token token = this.tokenizer.NextToken();

            if (token == null || token.Type == TokenType.EndOfLine)
                return;

            throw new InvalidDataException();
        }
    }
}
