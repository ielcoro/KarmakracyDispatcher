using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KcyDispatcher;
using FluentAssertions;
using Moq;

namespace KcyDispatcherTests
{
    [TestClass]
    public class KarmkracyServerTests
    {
        private Mock<IKarmacracyConnector> mockConnector;
        private IKarmacracyConnector kcyConnector;
        private KeyWordExtractor extractor;

        [TestInitialize]
        public void Initialize()
        {
            extractor = new KeyWordExtractor();
            mockConnector = new Mock<IKarmacracyConnector>();

            mockConnector.Setup(x => x.ShortLink(It.IsAny<string>()))
                        .Returns("http://kcy.me/9ihs")
                        .Verifiable();

            mockConnector.Setup(x => x.ShareKcy(It.IsAny<string>(), It.IsAny<string>()))
                        .Verifiable();

            kcyConnector = mockConnector.Object;
        }

        [TestMethod]
        public void AcortarEnlace()
        {
            string urlAcortar = "http://www.katayunos.com";

            var kcyService = new KarmakracyService(kcyConnector, extractor);

            string urlCorta = kcyService.Short(urlAcortar);

            urlCorta.Should().Contain("kcy.me");
            mockConnector.Verify(x => x.ShortLink(It.IsAny<string>()));
        }

        [TestMethod]
        public void CompartirKcy()
        {
            string url = "http://kcy.me/9ihs";
            string texto = "CompartirKcy";

            var kcyService = new KarmakracyService(kcyConnector, extractor);

            kcyService.Share(texto, url);

            mockConnector.Verify(x => x.ShareKcy(It.Is<string>(t => t == "CompartirKcy"), It.Is<string>(u => u == "9ihs")));
        }

        [TestMethod]
        public void KeyExtractorExtraeLaUltimaParteDeLaKcy()
        {
            string compartir = "http://kcy.me/9ihs";

            var extractor = new KeyWordExtractor();

            string keyword = extractor.ExtractKeyword(compartir);

            keyword.Should().Be("9ihs");
        }
    }
}
