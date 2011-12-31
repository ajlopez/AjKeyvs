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
        private Stack<int> chars = new Stack<int>();

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
            this.SkipWhiteSpaces();

            int ich = this.NextChar();

            if (ich == -1)
                return null;

            char ch = (char)ich;

            if (char.IsDigit(ch))
                return NextInteger(ch);

            return NextString(ch);
        }

        private Token NextString(char ch)
        {
            string value = new String(ch, 1);
            int ich;

            for (ich = this.NextChar(); ich != -1 && char.IsLetterOrDigit((char)ich); ich = this.NextChar())
                value += (char)ich;

            if (ich != -1)
                this.PushChar(ich);

            return new Token(TokenType.Name, value);
        }

        private Token NextInteger(char ch)
        {
            string value = new String(ch, 1);
            int ich;

            for (ich = this.NextChar(); ich != -1 && char.IsDigit((char)ich); ich = this.NextChar())
                value += (char)ich;

            if (ich != -1)
                this.PushChar(ich);

            return new Token(TokenType.Integer, value);
        }

        private void SkipWhiteSpaces()
        {
            int ich = this.NextChar();

            while (ich != -1 && char.IsWhiteSpace((char)ich))
                ich = this.NextChar();

            if (ich != -1)
                this.PushChar(ich);
        }

        private int NextChar()
        {
            if (this.chars.Count > 0)
                return this.chars.Pop();

            return reader.Read();
        }

        private void PushChar(int ich)
        {
            this.chars.Push(ich);
        }
    }
}

