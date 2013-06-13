using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydra.Core.Interfaces.Infrastructure
{
	public interface IDataProvider<TDataType>
	{
		ActionResult<bool> IsComplete { get; }
	    ActionResult<Tuple<bool, TDataType>> TryGetNext();
	}
}
