using CoreSDK.Models;
using System;

namespace CoreSDK
{
	public interface IEventArgs
	{
	}

	[Serializable]
	public class EventArgs : IEventArgs
	{
	}

	[Serializable]
	public class MessageArgs : EventArgs
	{
		public ITransmittable Message { get; set; }
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
	public class ControlArgs : EventArgs
	{
		public string ControllerGuid { get; set; }
		public Vector Vector { get; set; }

		public override bool Equals (object obj)
		{
			if (obj == null)
			{
				return false;
			}

			ControlArgs compare;

			try
			{
				compare = (ControlArgs)obj;
			}
			catch
			{
				throw new Exception("Failed to cast object to type ControlArgs");
			}

			if (ControllerGuid != null
				&& ControllerGuid == compare.ControllerGuid
				&& Vector != null
				&& Vector.Equals(compare.Vector))
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