using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hydra.Core.Interfaces.Infrastructure;
using System.Collections.Concurrent;

namespace Hydra.Core.DataProviders
{
	public class ThreadSafeQueue<TDataType>:IDataProvider<TDataType>,IDataReciever<TDataType>
	{
		private ConcurrentQueue<TDataType> _storage = new ConcurrentQueue<TDataType>();
		private bool _isComplete = false;
		
		public ActionResult<bool> IsComplete
		{
			get { return ActionResult<bool>.Success(_isComplete); }
		}		

		public ActionResult<Tuple<bool,TDataType>> TryGetNext()
		{
			TDataType result;
			bool hadItem = _storage.TryDequeue(out result);
			return ActionResult<Tuple<bool, TDataType>>.Success(new Tuple<bool, TDataType>(hadItem, result));
		}

		public ActionResult Send(TDataType Item)
		{
			_storage.Enqueue(Item);
			return ActionResult.Success();
		}

		public ActionResult SignalComplete()
		{
			_isComplete = true;
			return ActionResult.Success();
		}
	}
}
