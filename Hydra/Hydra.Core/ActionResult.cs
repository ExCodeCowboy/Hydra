using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hydra.Core
{
	public class ActionResult<TReturnType>
	{
		public TReturnType Result {get; private set;}
		public bool IsSuccess {get; private set;}
		public string Message {get; private set;}
		private ActionResult(){}

		public static ActionResult<TReturnType> Success(TReturnType result)
		{
			ActionResult<TReturnType> res = new ActionResult<TReturnType>();
			res.IsSuccess = true;
			res.Message = null;
			res.Result = result;
			return res;
		}

		public static ActionResult<TReturnType> Failure(string message)
		{
			ActionResult<TReturnType> res = new ActionResult<TReturnType>();
			res.IsSuccess = false;
			res.Message = message;
			res.Result = default(TReturnType);
			return res;
		}
	}

	public class ActionResult
	{
		public bool IsSuccess { get; private set; }
		public string Message { get; private set; }
		private ActionResult() { }

		public static ActionResult Success()
		{
			ActionResult res = new ActionResult();
			res.IsSuccess = true;
			res.Message = null;			
			return res;
		}

		public static ActionResult Failure(string message)
		{
			ActionResult res = new ActionResult();
			res.IsSuccess = false;
			res.Message = message;
			return res;
		}
	}
}
