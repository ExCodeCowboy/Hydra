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
    public class ProcessorTests
    {
        


        [Test]
        public void StaticLockingProcessorLocksWhenCalled()
        {
            //Arrange
			MethodInfo staticMeth = typeof(StaticPropClass).GetMethod("SlowStringLength");

            //Act
            ProcessorDefinition def = ProcessorDefintionBuilder.BuildProcessor("FakeName", staticMeth, InstanceType.StaticLockRequired);
            ProcessorDefinitionImp<string, int> castDef = def as ProcessorDefinitionImp<string, int>;
            
            IWorkProcessor<string, int> worker = castDef.GetProcessor();
			IWorkProcessor<string, int> worker2 = castDef.GetProcessor();
			
			//Each Call should take 1 second approx.
			Stopwatch s = new Stopwatch();
			s.Start();
			int length;
			Action call = () => length = worker.Process("SomeString");
			var  result = call.BeginInvoke(null, null);
			int length2 = worker2.Process("SomeString");
			call.EndInvoke(result);
			s.Stop();
            //Assert
            Assert.GreaterOrEqual(s.ElapsedMilliseconds, 2000);

        }
    }
}
