using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydra.Core.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true)]
    public class ProcessAttribute:System.Attribute
    {
        public String Name {get;private set;}
        public bool IsParallel { get; private set; }
        
        public ProcessAttribute(string name) : this(name, true) { }
        public ProcessAttribute(string name, bool isParallel)
        {
            Name = name;
            IsParallel = isParallel;
        }
    }
}
