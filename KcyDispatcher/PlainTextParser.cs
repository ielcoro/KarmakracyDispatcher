using System.Text.RegularExpressions;

namespace KcyDispatcher
{
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