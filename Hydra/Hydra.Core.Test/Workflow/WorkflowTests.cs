using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hydra.Core.Configuration;
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
    public class WorkflowTests
    {
        [Test]
        public void WorkFlowTransitionsBetweenTypes()
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


        [Test]
        public void WorkFlowSimpleBuilds()
        {
            //Arrange
            InlineWorkflowConfiguration configuration = new InlineWorkflowConfiguration(new StepConfiguration[]
                {
                    new StepConfiguration(){InputType = "System.String", OperationName = "StringLength", OutputType = "System.Int32"},
                    new StepConfiguration(){InputType = "System.Int32", OperationName = "Square", OutputType = "System.Int32"},
                    new StepConfiguration(){InputType = "System.Int32", OperationName = "Square", OutputType = "System.Int32"},
                });
            ApplicationManifestManager man = BootStrapper.GenerateApplicationManifest(Environment.CurrentDirectory);

            //Act
            IWorkProcessor<string, int> inlineFlow = SequenceBuilder.BuildSequence<string, int>(configuration, man);
            
            //Assert
            Assert.AreEqual(inlineFlow.Process("string"),1296);
        }
       
    }
}
