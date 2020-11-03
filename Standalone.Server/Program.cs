using Microsoft.Extensions.DependencyInjection;
using Moongate;

namespace Network
{
	class Program
	{
        static bool running = true;
        private static Container services;
        static Server server;

        static void Main (string[] args)
        {
            services = new Container();
            server = services.ServiceProvider.GetRequiredService<Server>();

            server.Start(8888);

            while (running)
            {
                server.Run(); 
            }

            server.Stop();
        }

	}
}
