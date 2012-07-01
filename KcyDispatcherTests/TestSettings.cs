using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KcyDispatcher.Properties;

namespace KcyDispatcherTests
{
    class TestSettings
        : IKcyDispatcherSettings
    {

        public string Pop3UserName
        {
            get { return "username"; }
        }

        public string Pop3Password
        {
            get { return "pop3Password"; }
        }

        public string Pop3Server
        {
            get { return "pop3Server"; }
        }

        public int Pop3Port
        {
            get { return 995; }
        }

        public bool Pop3UseSSL
        {
            get { return true; }
        }

        public int Pop3WaitInterval
        {
            get { return 0; }
        }

        public string KcyAppKey
        {
            get { return "yourappkey"; }
        }

        public string KcyUserName
        {
            get { return "username"; }
        }

        public string KcyUserKey
        {
            get { return "userkey"; }
        }

        public string KcyShorterUrl
        {
            get { return "shorterUrl"; }
        }

        public string KcySharerUrl
        {
            get { return "shareUrl"; }
        }

        public string KcyTwitterId
        {
            get { return "twitterId"; }
        }
    }
}
