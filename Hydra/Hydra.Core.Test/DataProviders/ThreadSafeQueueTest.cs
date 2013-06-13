using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hydra.Core.Attributes;
using System.Threading;
using NUnit.Framework;
using Hydra.Core.DataProviders;

namespace Hydra.Core.Test.DataProviders
{
	[TestFixture]
	public class ThreadSafeQueueTest
    {
        [Test]
		public void GetNextReturnsFalseWhenEmpty()
		{
			//Arrange
			var q = new ThreadSafeQueue<string>();

			//Act
			var res = q.TryGetNext();

			//Assert
			Assert.That(res.IsSuccess);
			Assert.That(res.Result.Item1 == false);
		}

    }
}
