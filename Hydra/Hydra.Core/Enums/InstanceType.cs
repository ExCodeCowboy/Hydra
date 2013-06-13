using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydra.Core.Enums
{
    public enum InstanceType
    {
        InstancePerCall,
        SingleInstanceParrallelSafe,
        SingleInstanceLockRequired,
        StaticParrallelSafe,
        StaticLockRequired
    } 
}
