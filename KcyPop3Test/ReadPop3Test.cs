using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace KcyPop3Test
{
    [TestFixture]
    public class ReadPop3Test
    {
        string username = "karmakracy@gmail.com";
        string password = "katayuno";
        string server = "pop.gmail.com";
        int port = 995;
        bool useSsl = true;


        OpenPop.Pop3.Pop3Client client;

        [SetUp]
        public void Setup()
        {
            client = new OpenPop.Pop3.Pop3Client();
            client.Connect(server, port, useSsl);
            client.Authenticate(username, password);
        }

        [TearDown]
        public void Shutdown()
        {
            if (client != null && client.Connected )
            {
                client.Disconnect();
                client.Dispose();
            }
            client = null;
        }

        [Test]
        public void GetMailTest()
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
            
        }

        static void Main(string[] args)
        {
            var reader = new ReadPop3Test();
            reader.Setup();
            reader.GetMailTest();

        }
    }
}
