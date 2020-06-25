using CoreSDK;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Test.CoreSDK
{
	public class ClientServerTests
	{
		private Mock<ILogger> logger;

		private CoreServer coreServer;
		private CoreClient coreClient;

		private bool testing = true;

		public ClientServerTests ()
		{
			logger = new Mock<ILogger>();

			coreServer = new CoreServer();
			coreClient = new CoreClient();

			coreServer.Start(8888);
			coreClient.Connect("localhost", 8888);

			var t = Task.Run(() => RunTask());
		}

		void RunTask ()
		{
			while (testing)
			{ 
				coreServer.Run();
				coreClient.Run();
			}
		}

		[Fact]
		public void ClientConnectedTest () 
		{
			Assert.True(coreClient.Connected);
		}

		[Fact]
		public void ServerIsActiveTest ()
		{
			Assert.True(coreServer.Active);
		}

		[Fact]
		public void Client ()
		{

		}
	}
}
