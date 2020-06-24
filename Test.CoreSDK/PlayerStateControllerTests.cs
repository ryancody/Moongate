using CoreSDK;
using CoreSDK.Controllers;
using Moq;
using System;
using Xunit;

namespace Test.CoreSDK
{
	public class PlayerStateControllerTests
	{
		private Mock<ILogger> logger;
		
		public PlayerStateControllerTests ()
		{
			logger = new Mock<ILogger>();
		}

		[Fact]
		public void TestAddPlayerGetPlayer ()
		{
			var playerState = new PlayerState();
			var playerStateController = new PlayerStateController(logger.Object, playerState);

			playerStateController.AddPlayer(0, "guid0", "name0");
			playerStateController.AddPlayer(1, "guid1", "name1");
			playerStateController.AddPlayer(2, "guid2", "name3");

			var player = playerStateController.GetPlayer(1);

			Assert.Equal(1, player.ConnectionId);
			Assert.Equal("guid1", player.Guid);
			Assert.Equal("name1", player.Name);
		}

		[Fact]
		public void TestGetPlayers ()
		{
			var playerState = new PlayerState();
			var playerStateController = new PlayerStateController(logger.Object, playerState);

			playerStateController.AddPlayer(0, "guid0", "name0");
			playerStateController.AddPlayer(1, "guid1", "name1");
			playerStateController.AddPlayer(2, "guid2", "name3");

			var players = playerStateController.GetPlayers();

			Assert.Equal("guid1", players[1].Guid);
		}

		[Fact]
		public void TestGetPlayerByConnectionId ()
		{
			var playerState = new PlayerState();
			var playerStateController = new PlayerStateController(logger.Object, playerState);

			playerStateController.AddPlayer(0, "guid0", "name0");
			playerStateController.AddPlayer(1, "guid1", "name1");
			playerStateController.AddPlayer(2, "guid2", "name3");

			var player = playerStateController.GetPlayer(1);

			Assert.Equal(1, player.ConnectionId);
		}

		[Fact]
		public void TestGetPlayerByGuid ()
		{
			var playerState = new PlayerState();
			var playerStateController = new PlayerStateController(logger.Object, playerState);

			playerStateController.AddPlayer(0, "guid0", "name0");
			playerStateController.AddPlayer(1, "guid1", "name1");
			playerStateController.AddPlayer(2, "guid2", "name3");

			var player = playerStateController.GetPlayer("guid1");

			Assert.Equal("name1", player.Name);
		}

		[Fact]
		public void TestRemovePlayer ()
		{
			var playerState = new PlayerState();
			var playerStateController = new PlayerStateController(logger.Object, playerState);

			playerStateController.AddPlayer(0, "guid0", "name0");
			playerStateController.AddPlayer(1, "guid1", "name1");
			playerStateController.AddPlayer(2, "guid2", "name3");

			playerStateController.RemovePlayer(1);

			Assert.Throws<Exception>(() => playerStateController.GetPlayer(1));
		}
	}
}
