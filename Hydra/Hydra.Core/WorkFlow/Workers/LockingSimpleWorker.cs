using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Hydra.Core.WorkFlow.Workers
{
    public class LockingSimpleWorker<TInput, TOutput>:IWorkProcessor<TInput,TOutput>
    {
        private Func<TInput,TOutput> _work;
        private object _lock = new object();

        public TOutput Process(TInput workIn)
        {
            lock(_lock)
            {
                return _work(workIn);
            }
        }

        public LockingSimpleWorker(Func<TInput, TOutput> workFunc)
        {
            _work = workFunc;
        }
    }
}
