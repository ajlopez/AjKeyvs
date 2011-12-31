namespace AjKeyvs.Server.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Tokenizer
    {
        private TextReader reader;

        public Tokenizer(string text)
            : this(new StringReader(text))
        {
        }

        public Tokenizer(TextReader reader)
        {
            this.reader = reader;
        }

        public Token NextToken()
        {
            int ich = this.NextChar();

            while (ich != -1 && char.IsWhiteSpace(((char)ich)))
                ich = this.NextChar();

            if (ich == -1)
                return null;

            string value = new String((char)ich, 1);

            for (ich = this.NextChar(); ich != -1 && char.IsLetterOrDigit((char)ich); ich = this.NextChar())
                value += (char)ich;

            return new Token(TokenType.Name, value);
        }

        private int NextChar()
        {
            return reader.Read();
        }
    }
}

