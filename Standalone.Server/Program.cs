namespace Network
{
	class Program
	{
        static bool running = true;
        static Server server = new Server();

        static void Main (string[] args)
		{
            server.Start(8888);

            while (running)
            {
                server.Run(); 
            }

            server.Stop();
        }

	}
}
