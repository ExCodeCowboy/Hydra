using Hydra.Core.Definitions;
using NUnit.Framework;
using System.IO;

namespace Hydra.Core.Test
{
    [TestFixture]
    public class BootStrapperTests
    {
        [Test]
        public void DoesTakeManifest()
        {
            //Arrange
            //Act
            ApplicationManifestManager appman = BootStrapper.GenerateApplicationManifest(Directory.GetCurrentDirectory());

            bool hasProcessor = appman.HasProcessor<string, int>("StringLength");
            IWorkProcessor<string, int> worker = appman.GetProcessor<string, int>("StringLength");

            //Assert
            Assert.That(hasProcessor);
            Assert.That(worker.Process("string") == 6);

        }

       
    }
}
