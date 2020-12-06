using Moongate.State.Models;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Models.Events
{
	[Serializable]
	public class MessageArgs : EventArgs
	{
		public int FromConnectionId { get; set; }
		public byte[] Payload { get; set; }
	}

	[Serializable]
	public class TransmissionArgs : EventArgs
	{
		public ITransmittable Transmission { get; set; }	
	}

	[Serializable]
	public class PingArgs : EventArgs
	{
		public string InitiatorGuid { get; set; }
		public long InitialTimestamp { get; set; }
		public long? Ping { get; set; }
	}

	[Serializable]
	public class ClientArgs : EventArgs
	{
		public int ConnectionId { get; set; }
		public string Guid { get; set; }
		public string Name { get; set; }
	}

	[Serializable]
	public class EntityArgs : EventArgs
	{
		public Entity Entity { get; set; }
	}

	[Serializable]
	public class PlayerInputArgs : EventArgs
	{
		public string ControllerGuid { get; set; }
		public Vector Vector { get; set; } = new Vector();
	}

	[Serializable]
	public class GameStateRequestArgs : EventArgs
	{
		public string SenderGuid { get; set; }
		public GameState GameState { get; set; }
	}

	[Serializable]
	public class PlayerStateArgs : EventArgs
	{
		public PlayerState PlayerState { get; set; }
	}
}