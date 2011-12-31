namespace AjKeyvs.Server.Parser
{
    public class Token
    {
        private TokenType type;
        private string value;

        public Token(TokenType type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public TokenType Type { get { return this.type; } }

        public string Value { get { return this.value; } }
    }
}
