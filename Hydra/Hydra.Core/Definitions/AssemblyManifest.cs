using System;
using System.Collections.Generic;
using System.Linq;

namespace Hydra.Core.Definitions
{
    public class AssemblyManifest
    {
        public string Name { get; private set; }
        
        private List<ProcessorDefinition> _processors = new List<ProcessorDefinition>();
        public List<ProcessorDefinition> Processors 
        { 
            get { return _processors; } 
        }


        public AssemblyManifest(string name)
        {
            Name = name;
        }

        public IWorkProcessor<TInput, TOutput> GetProcessor<TInput, TOutput>(string Name)
        {
            ProcessorDefinition p = _processors.FirstOrDefault(x => x.OperationName == Name &&
                                                         x.InputType == typeof(TInput) &&
                                                         x.OutputType == typeof(TOutput));
            if (p == null) return null;
            var pImpl = p as ProcessorDefinitionImp<TInput, TOutput>;
            if (pImpl == null) throw new Exception("error casting ProcessorDefinitionImp");

            return pImpl.GetProcessor();
        }

        public bool HasProcessor<TInput, TOutput>(string Name)
        {
            ProcessorDefinition p = _processors.FirstOrDefault(x => x.OperationName == Name &&
                                                         x.InputType == typeof(TInput) &&
                                                         x.OutputType == typeof(TOutput));
            return p != null;            
        }
    }
}
