using System.Threading.Tasks;

namespace Server
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var localServer = new WebSocketServer();
			await localServer.Start();
		}
	}	
}