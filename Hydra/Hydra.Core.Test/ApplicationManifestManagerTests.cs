using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hydra.Core.Definitions;
using Hydra.Core.Definitions.Builders;
using NUnit.Framework;
using Hydra.Core.Test.Props;
using System.Reflection;
using Hydra.Core.Enums;
using System.Diagnostics;

namespace Hydra.Core.Test
{
    [TestFixture]
    public class ApplicationManifestManagerTests
    {
        [Test]
        public void DoesTakeManifest()
        {
            //Arrange
            //Act
            AssemblyManifest man = ManifestBuilder.BuildManifest(Assembly.GetExecutingAssembly());
            ApplicationManifestManager appman = new ApplicationManifestManager();

            appman.AddManifest(man);           

            bool hasProcessor = appman.HasProcessor<string, int>("StringLength");
            IWorkProcessor<string, int> worker = appman.GetProcessor<string, int>("StringLength");

            //Assert
            Assert.That(hasProcessor);
            Assert.That(worker.Process("string") == 6);

        }

       
    }
}
