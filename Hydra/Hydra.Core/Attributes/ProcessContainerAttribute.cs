using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hydra.Core.Enums;

namespace Hydra.Core.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ProcessContainerAttribute:System.Attribute
    {
        public InstanceType Type { get; private set; }
        
        public ProcessContainerAttribute(InstanceType type)
        {
            Type = type;
        }

               
    }
}
