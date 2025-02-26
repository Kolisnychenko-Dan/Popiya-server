using Server;

class Program
{
	static async Task Main(string[] args)
	{
		await WebSocketServer.Start();
	}
}