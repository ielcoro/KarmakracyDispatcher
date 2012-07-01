using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KcyDispatcher.Properties;
using OpenPop.Pop3;

namespace KcyDispatcher
{
    public class MailDaemon
    {
        private Pop3Client client;
        private PlainTextParser parser;
        private KarmakracyService service;
        private IKcyDispatcherSettings settings;

        public MailDaemon(PlainTextParser parser, KarmakracyService service, IKcyDispatcherSettings settings)
        {
            this.parser = parser;
            this.service = service;
            this.settings = settings;
        }

        public void Start()
        {
            

            while (true)
            {
                ConnectToPop3();
                FetchPendingMail();
                Shutdown();
                Thread.Sleep(settings.Pop3WaitInterval);
            }

            
        }

        private void ConnectToPop3()
        {
            client = new Pop3Client();

            client.Connect(settings.Pop3Server, settings.Pop3Port, settings.Pop3UseSSL);
            client.Authenticate(settings.Pop3UserName, settings.Pop3Password);

        }

        private void Shutdown()
        {
            if (client != null && client.Connected)
            {
                client.Disconnect();
                client.Dispose();
            }
        }

        private void FetchPendingMail()
        {
            int count = client.GetMessageCount();
            Console.WriteLine("Message.Count : " + count);

            for (int i = 1; i <= count; ++i)
            {
                var email = client.GetMessage(i);
               
                Console.WriteLine("Message[" + i + "].Subject : " + email.Headers.Subject);
               
                var plainTextMessage = email.FindFirstPlainTextVersion();

                if (plainTextMessage != null)
                {
                    ProcessMail(plainTextMessage.GetBodyAsText());
                    
                }
                else
                    Console.WriteLine("No Plain Text Version");
            }

        }

        private void ProcessMail(string plainTextMessage)
        {
            Console.WriteLine("Body:" + plainTextMessage);
            var result = parser.Parse(plainTextMessage);

            service.Share(result.Text, result.MainUrl);
        }
    }
}
