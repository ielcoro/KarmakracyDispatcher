using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KcyDispatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace KcyPop3Test
{
    [TestClass]
    public class UrlConverter
    {
        IUrlShortener urlShortener;

        [TestInitialize]
        public void Setup()
        {
            var mock = new Mock<IUrlShortener>();
            mock.Setup(x => x.Short(It.IsAny<string>())).Returns("http://kcy.me/abcde").Verifiable();

            urlShortener = mock.Object;
        }

        [TestMethod]
        public void ConUnaUrl()
        {
            string plainText = "en un lugar de la mancha http://katayunos.com/ del cual blah blah.";
            string expected = "en un lugar de la mancha http://kcy.me/abcde del cual blah blah.";

            string espectedUrl = "http://kcy.me/abcde";
            PlainTextParser parser = new PlainTextParser(urlShortener);

            var result = parser.Parse(plainText);

            Assert.AreEqual(expected, result.Text);
            Assert.AreEqual(espectedUrl, result.MainUrl);
        }


        [TestMethod]
        public void ConDosUrl()
        {
            string plainText = "en un lugar de la mancha http://katayunos.com/ del cual http://katayunos.com/ blah blah.";
            string expected = "en un lugar de la mancha http://kcy.me/abcde del cual http://kcy.me/abcde blah blah.";

            string espectedUrl = "http://kcy.me/abcde";
            PlainTextParser parser = new PlainTextParser(urlShortener);

            var result = parser.Parse(plainText);

            Assert.AreEqual(expected, result.Text);
            Assert.AreEqual(espectedUrl, result.MainUrl);
        }


        [TestMethod]
        public void ConUnaUrlNoExcedeLongitud()
        {
            string plainText = "en un lugar de la mancha http://katayunos.com/ del cual blah blah.";
            string expected = "en un lugar de la mancha http://kcy.me/abcde del cual blah blah.";

            string espectedUrl = "http://kcy.me/abcde";
            PlainTextParser parser = new PlainTextParser(urlShortener);

            var result = parser.Parse(plainText, 140);

            Assert.AreEqual(expected, result.Text);
            Assert.AreEqual(espectedUrl, result.MainUrl);
        }

        [TestMethod]
        [Ignore]
        public void ConUnaUrlExcedeLongitud()
        {
            string plainText = "en un lugar de la mancha http://katayunos.com/ del cual blah blah.";
            string expected = "en un lugar de la... http://kcy.me/abcde";

            string espectedUrl = "http://kcy.me/abcde";
            PlainTextParser parser = new PlainTextParser(urlShortener);

            var result = parser.Parse(plainText, 40);

            Assert.AreEqual(expected, result.Text);
            Assert.AreEqual(espectedUrl, result.MainUrl);
        }

    }

    
}
