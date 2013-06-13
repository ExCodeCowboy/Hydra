using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using Hydra.Core.Attributes;
using Hydra.Core.Configuration;
using Hydra.Core.Enums;
using Hydra.Core.WorkFlow.Workers;

namespace Hydra.Core.Definitions.Builders
{
    public static class SequenceBuilder
    {
        public static IWorkProcessor<TInput, TOutput> BuildSequence<TInput, TOutput>(InlineWorkflowConfiguration config,
                                                                              ApplicationManifestManager applicationManifest)
        {
            Tuple<object,Type> currentSequenceStep = Tuple.Create<object,Type>(null,null);
            foreach (var step in config.Steps)
            {
                currentSequenceStep = AddStep(step, currentSequenceStep, applicationManifest);
            }
            var final = currentSequenceStep.Item1 as IWorkProcessor<TInput, TOutput>;
            if (final == null) throw new Exception("Bad Workflow Configuration");
            return final;
        }

        private static Tuple<object, Type> AddStep(StepConfiguration step,
                                                   Tuple<object, Type> currentSequence, 
                                                   ApplicationManifestManager applicationManifest)
        {
            Type newStepInType = Type.GetType(step.InputType);
            Type newStepOutType = Type.GetType(step.OutputType);

            if (currentSequence.Item1 != null)
            {

                MethodInfo buildCall = Type.GetType("Hydra.Core.Definitions.Builders.SequenceBuilder")
                                           .GetMethod("BuildWrapper", BindingFlags.NonPublic | BindingFlags.Static);
                var call = buildCall.MakeGenericMethod(new Type[] {currentSequence.Item2, newStepInType, newStepOutType});
                object worker = call.Invoke(null,
                                            new object[]
                                                {currentSequence.Item1, step.OperationName, applicationManifest});
                return Tuple.Create<object, Type>(worker, currentSequence.Item2);
            }
            else
            {
                MethodInfo buildCall = applicationManifest.GetType()
                                                          .GetMethod("GetProcessor",BindingFlags.Public|BindingFlags.Instance);
                var call = buildCall.MakeGenericMethod(new Type[] {newStepInType, newStepOutType});
                object worker = call.Invoke(applicationManifest,
                                            new object[] {step.OperationName});
                return Tuple.Create<object, Type>(worker, newStepInType);
            }
        }
        
        private static IWorkProcessor<TSI, TSO> BuildWrapper<TSI, TSM, TSO>(
            IWorkProcessor<TSI, TSM> lastStep,
            string operationName,
            ApplicationManifestManager applicationManifest)
        {
            var newStep = applicationManifest.GetProcessor<TSM, TSO>(operationName);
            IWorkProcessor<TSI,TSO> step  = new WorkerSequence<TSI,TSM,TSO>(lastStep,newStep);
            return step;
        }
        
        
    }
}
