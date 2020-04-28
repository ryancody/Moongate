using CoreSDK;
using CoreSDK.Controllers;
using CoreSDK.Factory;
using CoreSDK.Models;
using CoreSDK.Utils;
using Moq;
using System;
using System.Collections.Generic;
using Telepathy;
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
		public void AddPlayerGetPlayerTest ()
		{
			var playerState = new PlayerState();
			var playerStateController = new PlayerStateController(logger.Object, playerState);

			playerStateController.AddPlayer(0, "guid0", "name0");
			playerStateController.AddPlayer(1, "guid1", "name1");
			playerStateController.AddPlayer(2, "guid2", "name3");

			var player = playerStateController.GetPlayer(1);

			Assert.Equal(1, player.ConnectionId);
			Assert.Equal("guid1", player.GUID);
			Assert.Equal("name1", player.Name);
		}

		[Fact]
		public void GetPlayersTest ()
		{
			var playerState = new PlayerState();
			var playerStateController = new PlayerStateController(logger.Object, playerState);

			playerStateController.AddPlayer(0, "guid0", "name0");
			playerStateController.AddPlayer(1, "guid1", "name1");
			playerStateController.AddPlayer(2, "guid2", "name3");

			var players = playerStateController.GetPlayers();

			Assert.Equal("guid1", players[1].GUID);
		}

		[Fact]
		public void GetPlayerByConnectionIdTest ()
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
		public void GetPlayerByGuidTest ()
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
		public void GetPlayerGuidTest ()
		{
			var playerState = new PlayerState();
			var playerStateController = new PlayerStateController(logger.Object, playerState);

			playerStateController.AddPlayer(0, "guid0", "name0");
			playerStateController.AddPlayer(1, "guid1", "name1");
			playerStateController.AddPlayer(2, "guid2", "name3");

			var guid = playerStateController.GetPlayerGuid(1);

			Assert.Equal("guid1", guid);
		}

		[Fact]
		public void RemovePlayerTest ()
		{
			var playerState = new PlayerState();
			var playerStateController = new PlayerStateController(logger.Object, playerState);

			playerStateController.AddPlayer(0, "guid0", "name0");
			playerStateController.AddPlayer(1, "guid1", "name1");
			playerStateController.AddPlayer(2, "guid2", "name3");

			playerStateController.RemovePlayer(1);
			var player = playerStateController.GetPlayer(1);

			Assert.Null(player);
		}
	}
}
