using Microsoft.Extensions.DependencyInjection;
using Moongate;

namespace Network
{
	class Program
	{
        static bool running = true;
        private static Startup services;
        static Server server;

        static void Main (string[] args)
        {
            services = new Startup();
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
