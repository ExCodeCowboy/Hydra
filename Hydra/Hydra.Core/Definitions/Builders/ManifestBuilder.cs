using System;
using System.Collections.Generic;
using System.Reflection;
using Hydra.Core.Attributes;
using Hydra.Core.Enums;

namespace Hydra.Core.Definitions.Builders
{
    public static class ManifestBuilder
    {

        public static AssemblyManifest BuildManifest(Assembly targetAssembly)
        {
            AssemblyManifest output = new AssemblyManifest(targetAssembly.FullName);
           
            List<ProcessorDefinition> processors = FindProcessors(targetAssembly);
            output.Processors.AddRange(processors);
            
            return output;
        }


        static private List<ProcessorDefinition> FindProcessors(Assembly assembly)
        {

            Type[] types = assembly.GetTypes();
                       
            List<ProcessorDefinition> results = new List<ProcessorDefinition>();

            foreach (Type cType in types)
            {
                foreach (MethodInfo cMethod in cType.GetMethods(BindingFlags.Static|BindingFlags.Instance|BindingFlags.Public))
                {
                    //Determine Container Type - if applicable
                    InstanceType instanceType = DetermineInstanceType(cType,cMethod);

                    ProcessAttribute[] cProcesses = cMethod.GetCustomAttributes(typeof(ProcessAttribute), false) as ProcessAttribute[];

                    //Make sure we have some Plugin Definitions
                    // also check to make sure that the Plugin doesn't need more than one parameter.
                    if (cProcesses.Length > 0 && cMethod.GetParameters().Length == 1)
                    {
                        foreach (ProcessAttribute cProcessDef in cProcesses)
                        {
                            ProcessorDefinition newProc = ProcessorDefintionBuilder.BuildProcessor(cProcessDef.Name, cMethod, instanceType);
                            if (newProc != null)
                            {
                                results.Add(newProc);
                            }
                        }
                    }
                }
            }
            return results;
        }

        private static InstanceType DetermineInstanceType(Type cType, MethodInfo cMethod)
        {
            ProcessContainerAttribute[] processContainerAttr = cType.GetCustomAttributes(typeof(ProcessContainerAttribute), false) as ProcessContainerAttribute[];
            InstanceType baseType = InstanceType.InstancePerCall;
            if (processContainerAttr.Length > 0)
                baseType = processContainerAttr[0].Type;

            if (cMethod.IsStatic)
            {
                if (baseType == InstanceType.SingleInstanceParrallelSafe ||
                    baseType == InstanceType.StaticParrallelSafe)
                {
                    return InstanceType.StaticParrallelSafe;    
                }
                return InstanceType.StaticLockRequired;
            }
            return baseType;
        }
    }
}
