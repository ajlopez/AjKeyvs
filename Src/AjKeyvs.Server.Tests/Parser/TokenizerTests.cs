namespace AjKeyvs.Server.Tests.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using AjKeyvs.Server.Parser;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void ParseName()
        {
            Tokenizer tokenizer = new Tokenizer("name");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("name", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        [TestMethod]
        public void ParseNameWithSpaces()
        {
            Tokenizer tokenizer = new Tokenizer("  name  ");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("name", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        [TestMethod]
        public void ParseInteger()
        {
            Tokenizer tokenizer = new Tokenizer("1234");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("1234", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        [TestMethod]
        public void ParseIntegerWithSpaces()
        {
            Tokenizer tokenizer = new Tokenizer(" 1234  ");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("1234", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException), "Unexpected character '['")]
        public void RaiseIfInvalidCharacter()
        {
            Tokenizer tokenizer = new Tokenizer("[]");
            tokenizer.NextToken();
        }

        [TestMethod]
        public void ParseNewLineAsEndOfLine()
        {
            Tokenizer tokenizer = new Tokenizer("\n");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);
            Assert.AreEqual("\n", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        [TestMethod]
        public void ParseCarriageReturnNewLineAsEndOfLine()
        {
            Tokenizer tokenizer = new Tokenizer("\r\n");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);
            Assert.AreEqual("\n", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        [TestMethod]
        public void ParseCompositeKeyAsName()
        {
            Tokenizer tokenizer = new Tokenizer("users:1:name");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("users:1:name", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        [TestMethod]
        public void ParseNameWithUnderscores()
        {
            Tokenizer tokenizer = new Tokenizer("user_name");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("user_name", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        [TestMethod]
        public void ParseSimpleString()
        {
            Tokenizer tokenizer = new Tokenizer("\"Adam\"");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("Adam", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        [TestMethod]
        public void ParseSimpleStringWithUnicodeOtherSymbol()
        {
            char othersymbol = (char)0xfffd;
            Assert.IsTrue(char.GetUnicodeCategory(othersymbol) == UnicodeCategory.OtherSymbol);

            Tokenizer tokenizer = new Tokenizer(othersymbol + "\"Adam\"");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("Adam", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }

        [TestMethod]
        public void ParseTwoStrings()
        {
            Tokenizer tokenizer = new Tokenizer("\"Adam\" \"Smith\"");

            Token token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("Adam", token.Value);

            token = tokenizer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("Smith", token.Value);

            Assert.IsNull(tokenizer.NextToken());
        }
    }
}
