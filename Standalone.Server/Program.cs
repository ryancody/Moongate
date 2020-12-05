using Moongate;

namespace Moongate
{
	class Program
	{
        static bool running = true;
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
