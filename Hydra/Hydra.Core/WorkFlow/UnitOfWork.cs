using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Hydra.Core.Interfaces.Infrastructure;

namespace Hydra.Core.WorkFlow
{
    
	public class UnitOfWork<TInput,TOutput>
	{
		private IDataProvider<TInput> _provider;
		private IDataReciever<TOutput> _reciever;
		private IWorkProcessor<TInput, TOutput> _worker;

        public UnitOfWork(IDataProvider<TInput> inProvider,
                            IDataReciever<TOutput> outReciever,
                            IWorkProcessor<TInput, TOutput> workProcessor)
        {
            _provider = inProvider;
            _reciever = outReciever;
            _worker = workProcessor;
        }

        public void Process()
        {
            
        }

       
	}
}
