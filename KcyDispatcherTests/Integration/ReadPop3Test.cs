using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KcyDispatcher.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenPop.Pop3;


namespace KcyDispatcherTests
{
    [TestClass]
    public class ReadPop3TestMethod
    {


        Pop3Client client;
        IKcyDispatcherSettings settings;
        
        [TestInitialize]
        public void TestMethodInitialize()
        {
            settings = new TestSettings();
            client = new Pop3Client();
            client.Connect(settings.Pop3Server, settings.Pop3Port, settings.Pop3UseSSL);
            client.Authenticate(settings.Pop3UserName, settings.Pop3Password);
        }

        [TestCleanup]
        public void Shutdown()
        {
            if (client != null && client.Connected )
            {
                client.Disconnect();
                client.Dispose();
            }
            client = null;
        }

        [TestMethod]
        public void GetMailTestMethod()
        {
            int count = client.GetMessageCount();
            Console.WriteLine("Message.Count : " + count);

            for (int i = 1; i <= count ; ++i)
            {
                var email = client.GetMessage(i);
                Console.WriteLine("Message[" + i + "].Subject : " + email.Headers.Subject);
                var plainTextMessage = email.FindFirstPlainTextVersion();
                if ( plainTextMessage != null )
                    Console.WriteLine("Body:" + plainTextMessage.GetBodyAsText());    
                else
                    Console.WriteLine("No Plain Text Version");
            }

            Assert.IsTrue(true, "Por que hemos llegado hasta aqui");
        }

       
    }
}
