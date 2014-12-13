namespace AjKeyvs.Server.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Tokenizer
    {
        private static string namechars = ":_";

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

            if (ch == '\n')
                return new Token(TokenType.EndOfLine, "\n");

            if (ch == '"')
                return this.NextString();

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            if (char.IsLetter(ch))
                return this.NextName(ch);

            throw new InvalidDataException(string.Format("Unexpected character '{0}'", ch));
        }

        private Token NextName(char ch)
        {
            string value = new string(ch, 1);
            int ich;

            for (ich = this.NextChar(); ich != -1 && (char.IsLetterOrDigit((char)ich) || namechars.Contains((char)ich)); ich = this.NextChar())
                value += (char)ich;

            if (ich != -1)
                this.PushChar(ich);

            return new Token(TokenType.Name, value);
        }

        private Token NextString()
        {
            string value = string.Empty;
            int ich;

            for (ich = this.NextChar(); ich != -1 && (char)ich != '"'; ich = this.NextChar())
                value += (char)ich;

            if (ich == -1)
                throw new InvalidDataException("Expected '\"'");

            return new Token(TokenType.String, value);
        }

        private Token NextInteger(char ch)
        {
            string value = new string(ch, 1);
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

            while (ich != -1 && (char.IsWhiteSpace((char)ich) || char.GetUnicodeCategory((char)ich) == UnicodeCategory.Control || char.GetUnicodeCategory((char)ich) == UnicodeCategory.OtherSymbol) && ((char)ich) != '\n')
                ich = this.NextChar();

            if (ich != -1)
                this.PushChar(ich);
        }

        private int NextChar()
        {
            if (this.chars.Count > 0)
                return this.chars.Pop();

            return this.reader.Read();
        }

        private void PushChar(int ich)
        {
            this.chars.Push(ich);
        }
    }
}

