using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydra.Core.Interfaces.Infrastructure
{
	public interface IDataReciever<TDataType>
	{
		ActionResult Send(TDataType Item);
		ActionResult SignalComplete(); 	
	}
}
