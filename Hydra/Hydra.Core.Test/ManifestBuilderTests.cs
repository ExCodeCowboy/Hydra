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
    public class ManifestBuilderTests
    {
        [Test]
        public void DoesBuildManifest()
        {
            //Arrange
            //Act
            AssemblyManifest man = ManifestBuilder.BuildManifest(Assembly.GetExecutingAssembly());

            bool hasProcessor = man.HasProcessor<string, int>("StringLength");
            IWorkProcessor<string, int> worker = man.GetProcessor<string, int>("StringLength");

            //Assert
            Assert.That(hasProcessor);
            Assert.That(worker.Process("string") == 6);

        }

       
    }
}
