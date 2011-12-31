using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjKeyvs.Server.Parser;

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
    }
}
