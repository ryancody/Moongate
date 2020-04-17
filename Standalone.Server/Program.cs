namespace CoreSDK
{
	class Program
	{
        static bool running = true;
        static CoreServer server = new CoreServer();

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
