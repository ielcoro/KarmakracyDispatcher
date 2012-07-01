using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KcyDispatcher
{
    public class KarmakracyService
        : IUrlShortener
    {
        private IKarmacracyConnector connector;
        private KeyWordExtractor extractor;

        public KarmakracyService(IKarmacracyConnector connector, KeyWordExtractor extractor)
        {
            this.connector = connector;
            this.extractor = extractor;
        }

        public string Short(string urlAcortar)
        {
            return connector.ShortLink(urlAcortar);
        }

        public void Share(string texto, string kcy)
        {
            string keyword = extractor.ExtractKeyword(kcy);

            connector.ShareKcy(texto, keyword);
        }
    }
}
