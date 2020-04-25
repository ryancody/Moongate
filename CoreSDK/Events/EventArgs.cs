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
	public class PingArgs : IEventArgs
	{
		public int ConnectionId { get; set; }
		public long InitialTimestamp { get; set; }
		public long Ping { get; set; }
	}

	[Serializable]
	public class PlayerConnectionArgs : IEventArgs
	{
		public int ConnectionId { get; set; }
	}

	[Serializable]
	public class PlayerHandshakeArgs : IEventArgs
	{
		public string Name { get; set; }
		public int ConnectionId { get; set; }
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
		public int RequestedBy { get; set; }
		public GameState GameState { get; set; }
	}

	[Serializable]
	public class PlayerStateArgs : IEventArgs
	{ 
		public PlayerState PlayerState { get; set; }
	}
}