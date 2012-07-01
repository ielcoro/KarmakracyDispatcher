using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

using NUnit.Framework;

using Moq;

namespace KcyPop3Test
{
    [TestFixture]
    public class UrlConverter
    {
        IUrlShortener urlShortener;

        [SetUp]
        public void Setup()
        {
            var mock = new Mock<IUrlShortener>();
            mock.Setup(x => x.Short(It.IsAny<string>())).Returns("http://kcy.me/abcde").Verifiable();

            urlShortener = mock.Object;
        }

        [Test]
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


        [Test]
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


        [Test]
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

        [Test]
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

    public interface IUrlShortener
    {
        string Short(string url);
    }

    public class PlainTextParser
    {
        IUrlShortener _shortener;

        public PlainTextParser(IUrlShortener shortener)
        {
            this._shortener = shortener;
        }

        private readonly static string URL_REGEX = @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])";
        public ParseResult Parse(string plainText)
        {
            string mainUrl = null;
            string text = Regex.Replace( plainText, URL_REGEX, 
                delegate(System.Text.RegularExpressions.Match match)
                {
                    string shortUrl = _shortener.Short(match.ToString());
                    if (string.IsNullOrEmpty(mainUrl))
                        mainUrl = shortUrl;
                    return shortUrl;
                });

            return new ParseResult(text, mainUrl);
        }

        public class ParseResult
        {
            public ParseResult( string text, string url)
            {
                this.Text = text;
                this.MainUrl = url;
            }
            public string Text { get; private set; }
            public string MainUrl { get; private set; }
        }

        public ParseResult Parse(string plainText, int maxSize)
        {
            string mainUrl = null;
            string text = Regex.Replace(plainText, URL_REGEX,
                delegate(System.Text.RegularExpressions.Match match)
                {
                    string shortUrl = _shortener.Short(match.ToString());
                    if (string.IsNullOrEmpty(mainUrl))
                    {
                        mainUrl = shortUrl;
                    }
                    return shortUrl;
                });

            if ( !string.IsNullOrEmpty(mainUrl) && maxSize > 30)
            {
                int index = text.IndexOf(mainUrl);
                if (index > maxSize)
                {

                }
            }

            return new ParseResult(text, mainUrl);
        }
    }
}
