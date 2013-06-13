using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Hydra.Core.WorkFlow.Workers
{
    public class SimpleWorker<TInput, TOutput>:IWorkProcessor<TInput,TOutput>
    {
        private Func<TInput,TOutput> _work;

        public TOutput Process(TInput workIn)
        {
            return _work(workIn);
        }        

        public SimpleWorker(Func<TInput, TOutput> workFunc)
        {
            _work = workFunc;
        }
    }
}
