using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydra.Core.Configuration
{
    [Serializable]
    public class InlineWorkflowConfiguration
    {
        public IEnumerable<StepConfiguration> Steps { get; private set; }

        public InlineWorkflowConfiguration(IEnumerable<StepConfiguration> steps)
        {
            Steps = steps;
        }
    }

    
}
