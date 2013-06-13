using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydra.Core
{
    public interface IWorkProcessor<TInput,TOutput>
    {
        TOutput Process(TInput workIn); 
    }

}
