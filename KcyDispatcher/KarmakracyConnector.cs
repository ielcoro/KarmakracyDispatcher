using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using KcyDispatcher.Properties;

namespace KcyDispatcher
{
    public class KarmakracyConnector
        : IKarmacracyConnector
    {
        private IKcyDispatcherSettings settings;

        public KarmakracyConnector(IKcyDispatcherSettings settings)
        {
            this.settings = settings;
        }

        public string ShortLink(string url)
        {
            string kcyShorter = settings.KcyShorterUrl;

            var request = WebRequest.Create(String.Format(kcyShorter, settings.KcyUserName, settings.KcyUserKey, url));

            var response = request.GetResponse();

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        public void ShareKcy(string text, string keyword)
        {
            var encodedText = System.Web.HttpUtility.UrlEncode(text);

            string kcySharer = settings.KcySharerUrl;
            
            var request = WebRequest.Create(String.Format(kcySharer, settings.KcyUserName, settings.KcyUserKey, encodedText, keyword, settings.KcyAppKey, settings.KcyTwitterId));

            var response = request.GetResponse();

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
            }
        }
    }
}
