using CoreSDK.Models;
using System;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace CoreSDK
{

	[Serializable]
	public class PingArgs
	{
		public int ConnectionId { get; set; }
		public long InitialTimestamp { get; set; }
		public long Ping { get; set; }
	}

	[Serializable]
	public class PlayerConnectionArgs
	{ 
		public int ConnectionId { get; set; }
	}

	[Serializable]
	public class PlayerHandshakeArgs
	{
		public string Name { get; set; }
		public int ConnectionId { get; set; }
		public string Guid { get; set; }
	}

	[Serializable]
	public class CreateEntityArgs
	{
		public Entity Entity { get; set; }
	}

	[Serializable]
	public class UpdatePositionArgs
	{
		public string EntityGuid { get; set; }
		public Position Position { get; set; }
		public Vector Vector { get; set; }
	}

	[Serializable]
	public class GameStateRequestArgs
	{
		public GameState GameState { get; set; }
	}
}