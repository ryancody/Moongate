using CoreNET.Controllers.Messenger;
using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace CoreSDK
{
	public interface IEventArgs
	{
		
	}

	[Serializable]
	public class MessageArgs : IEventArgs
	{ 
		public ITransmittable Message { get; set; }
	}

	[Serializable]
	public class PingArgs : IEventArgs
	{
		public ConnectionId ConnectionId { get; set; }
		public long InitialTimestamp { get; set; }
		public long Ping { get; set; }
	}

	[Serializable]
	public class PlayerConnectionArgs : IEventArgs
	{
		public ConnectionId ConnectionId { get; set; }
	}

	[Serializable]
	public class PlayerHandshakeArgs : IEventArgs
	{
		public string Name { get; set; }
		public ConnectionId ConnectionId { get; set; }
		public string Guid { get; set; }
	}

	[Serializable]
	public class EntityArgs : IEventArgs
	{
		public Entity Entity { get; set; }
		public Vector Vector { get; set; }
	}

	[Serializable]
	public class PlayerInputArgs : IEventArgs
	{ 
		public string ControllerGuid { get; set; }
		public Vector Vector { get; set; }
	}

	[Serializable]
	public class GameStateRequestArgs : IEventArgs
	{
		public ConnectionId RequestedBy { get; set; }
		public GameState GameState { get; set; }
	}

	[Serializable]
	public class PlayerStateArgs : IEventArgs
	{ 
		public PlayerState PlayerState { get; set; }
	}
}