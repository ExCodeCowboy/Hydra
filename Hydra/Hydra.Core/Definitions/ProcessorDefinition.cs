using System;

namespace Hydra.Core.Definitions
{
    public abstract class ProcessorDefinition
    {
        public Type InputType { get; set; }
        public Type OutputType { get; set; }
        public string OperationName { get; set; } 
    }
}
