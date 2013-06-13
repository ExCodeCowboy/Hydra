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
    public class ProcessorBuilderTests
    {
        [Test]
        public void DoesBuildStaticProcessor()
        {
            //Arrange
            MethodInfo staticMeth = typeof(StaticPropClass).GetMethod("StringLength");

            //Act
            ProcessorDefinition def = ProcessorDefintionBuilder.BuildProcessor("FakeName", staticMeth, InstanceType.StaticParrallelSafe);

            //Assert
            Assert.That(def is ProcessorDefinition);
            Assert.That(def != null);
        }

        [Test]
        public void TestBenchmarkTimes()
        {
            //Arrange
            MethodInfo staticMeth = typeof(StaticPropClass).GetMethod("StringLength");

            //Act
            ProcessorDefinition def = ProcessorDefintionBuilder.BuildProcessor("FakeName", staticMeth, InstanceType.StaticParrallelSafe);
            ProcessorDefinitionImp<string, int> castDef = def as ProcessorDefinitionImp<string, int>;
            IWorkProcessor<string, int> worker = castDef.GetProcessor();

            Stopwatch s = new Stopwatch();
            s.Start();
            for (int i = 0; i < 1000000; i++)
            {
                int length = worker.Process("SomeString");
            }
            s.Stop();

            long eTime = s.ElapsedMilliseconds;

            s.Reset();
            s.Start();
            for (int i = 0; i < 1000000; i++)
            {
                Func<string, int> x = (c) => StaticPropClass.StringLength(c);
                //int length = (int)staticMeth.Invoke(null, new object[] { "SomeString" });
                int length = x("SomeString");
            }
            s.Stop();

            long rTime = s.ElapsedMilliseconds;


            //Assert
            //Assert.That(length == 10);

        }


        [Test]
        public void StaticProcessorWorks()
        {
            //Arrange
            MethodInfo staticMeth = typeof(StaticPropClass).GetMethod("StringLength");

            //Act
            ProcessorDefinition def = ProcessorDefintionBuilder.BuildProcessor("FakeName", staticMeth, InstanceType.StaticParrallelSafe);
            ProcessorDefinitionImp<string, int> castDef = def as ProcessorDefinitionImp<string, int>;
            IWorkProcessor<string, int> worker = castDef.GetProcessor();

          
            int length = worker.Process("SomeString");
           
            //Assert
            Assert.That(length == 10);

        }

        [Test]
        public void StaticProcessorIsTheSameWhenGottenTwice()
        {
            //Arrange
            MethodInfo staticMeth = typeof(StaticPropClass).GetMethod("StringLength");

            //Act
            ProcessorDefinition def = ProcessorDefintionBuilder.BuildProcessor("FakeName", staticMeth, InstanceType.StaticParrallelSafe);
            ProcessorDefinitionImp<string, int> castDef = def as ProcessorDefinitionImp<string, int>;
            
            IWorkProcessor<string, int> worker = castDef.GetProcessor();
            int length = worker.Process("SomeString");

            IWorkProcessor<string, int> worker2 = castDef.GetProcessor();
            int length2 = worker2.Process("SomeString");


            //Assert
            Assert.That(worker.Equals(worker2));

        }
    }
}
