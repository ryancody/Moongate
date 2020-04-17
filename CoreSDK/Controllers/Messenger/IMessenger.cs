using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Controllers
{
	public interface IMessenger
	{
		void Send (Transmission t);
		void Send (int connectionId, Transmission t);
	}
}
