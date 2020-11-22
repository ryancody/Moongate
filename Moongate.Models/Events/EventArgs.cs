using Moongate.State.Models;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Models.Events
{
	[Serializable]
	public class MessageArgs : EventArgs
	{
		public int SenderConnectionId { get; set; }
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
		public long InitialTimestamp { get; set; }
		public long Ping { get; set; }
		public string SenderGuid { get; set; }
	}

	[Serializable]
	public class PlayerConnectionArgs : EventArgs
	{
		public int ConnectionId { get; set; }
	}

	[Serializable]
	public class PlayerHandshakeArgs : EventArgs
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
	public class PlayerInputArgs : EventArgs, IEquatable<PlayerInputArgs>
	{
		public string ControllerGuid { get; set; }
		public Vector Vector { get; set; } = new Vector();

		public bool Equals (PlayerInputArgs other)
		{
			if (other == null) throw new NullReferenceException();

			if (ControllerGuid == other.ControllerGuid
				&& Vector.Equals(other.Vector))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
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