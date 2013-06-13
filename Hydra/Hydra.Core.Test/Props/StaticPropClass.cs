using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hydra.Core.Attributes;
using System.Threading;
using Hydra.Core.Enums;

namespace Hydra.Core.Test.Props
{
    [ProcessContainer(InstanceType.StaticParrallelSafe)]
    public static class StaticPropClass
    {
        [Process("StringLength")]
        public static int StringLength(string input)
        {
            return (input??"").Length;
        }

        [Process("Square")]
        public static int StringLength(int input)
        {
            return (int)Math.Pow(input,2);
        }

		[Process("SomeOtherName",false)]
		public static int SlowStringLength(string input)
		{
			Thread.Sleep(1000);
			return (input ?? "").Length;
		}
    }
}
