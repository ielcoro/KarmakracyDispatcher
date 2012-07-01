using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KcyDispatcher;

namespace KcyDispatcherTests.Integration
{
    
    [TestClass]
    public class ShareTests
    {
        [TestMethod]
        public void ShareKcy()
        {
            var settings = new TestSettings();

            var kcyConnector = new KarmakracyConnector(settings);
            var keyExtractor = new KeyWordExtractor();

            var kcyService = new KarmakracyService(kcyConnector, keyExtractor);

            string kcy = kcyService.Short("http://www.katayunos.com");

            kcyService.Share("Hola Katayuners", kcy);
        }
    }
}
