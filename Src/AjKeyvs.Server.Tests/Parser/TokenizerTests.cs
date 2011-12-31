using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjKeyvs.Server.Parser;
using System.IO;

namespace AjKeyvs.Server.Tests.Parser
{
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
    }
}
