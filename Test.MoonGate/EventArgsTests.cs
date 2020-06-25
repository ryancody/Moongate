using CoreSDK;
using CoreSDK.Models;
using Moq;
using System;
using Xunit;

namespace Test.CoreSDK
{
	public class EventArgsTests
	{

		public EventArgsTests ()
		{ 
		
		}

		[Fact]
		public void ControlArgsEquals ()
		{
			var args = new ControlArgs()
			{
				ControllerGuid = "abc",
				Vector = new Vector()
				{ 
					x = 1,
					y = 2
				}
			};

			var otherArgs = new ControlArgs()
			{
				ControllerGuid = "abc",
				Vector = new Vector()
				{
					x = 1,
					y = 2
				}
			};

			Assert.True(args.Equals(otherArgs));
		}

		[Fact]
		public void ControlArgsNotEquals ()
		{
			var args = new ControlArgs()
			{
				ControllerGuid = "abc",
				Vector = new Vector()
				{
					x = 1,
					y = 2
				}
			};

			var otherArgs = new ControlArgs();

			Assert.False(args.Equals(otherArgs));
		}

		[Fact]
		public void ControlArgsNullEquals ()
		{
			var args = new ControlArgs()
			{
				ControllerGuid = "abc",
				Vector = new Vector()
				{
					x = 1,
					y = 2
				}
			};

			Assert.Throws<NullReferenceException>(() => args.Equals(null));
		}

		[Fact]
		public void ControlArgsVectorNullEquals ()
		{
			var args = new ControlArgs()
			{
				ControllerGuid = "abc",
				Vector = null
			};

			var other = new ControlArgs()
			{
				ControllerGuid = "abc",
				Vector = null
			};

			Assert.Throws<NullReferenceException>(() => args.Equals(other));
		}
	}
}
