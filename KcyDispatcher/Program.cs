using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPop.Pop3;

namespace KcyDispatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to The AWESOME Karmakracy URL Dispatcher!!!!!!!!!!");

            var connector = new KarmakracyConnector(Properties.Settings.Default);
            var keyExtractor = new KeyWordExtractor();
            var kcyService = new KarmakracyService(connector, keyExtractor);

            var plainTextParser = new PlainTextParser(kcyService);
            var daemon = new MailDaemon(plainTextParser, kcyService, Properties.Settings.Default);

            daemon.Start();

            Console.ReadLine();
        }


       
    }
}
