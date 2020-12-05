using Moongate;

namespace Network
{
	class Program
	{
        static bool running = true;
        private static DependencyInjection services;
        static Server server;

        static void Main (string[] args)
        {
            server = new Server();

            server.Start(8888);

            while (running)
            {
                server.Run(); 
            }

            server.Stop();
        }

	}
}
