using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Xml;
using MyMessenger.Client.Commands;

namespace MyMessenger.Client.Xamarin.Forms.Services
{
	public class Client
	{
		public IPEndPoint ServerIp { get; set; }

		public async Task<string> Receive(IPEndPoint ip, string login, string password)
		{
			ServerIp = ip;

			var client = new TcpClient();
			client.Connect(ServerIp);
			var stream = client.GetStream();

			var command = new Login(stream, login, password);

			await command.ExecuteAsync();

			return await Task.FromResult<string>(command.RawResponse);
		}
	}
}