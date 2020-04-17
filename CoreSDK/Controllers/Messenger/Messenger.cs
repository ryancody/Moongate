using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Controllers
{
	public abstract class Messenger : IMessenger
	{
		public abstract void Send (Transmission t);
		public abstract void Send (int i, Transmission t);
	}
}
