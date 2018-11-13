using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

using Newtonsoft.Json;

using MyMessenger.Core.Parameters;

namespace MyMessenger.Client.Commands
{
	public class Login : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] { "login", "l" });

		public IList<Message> Result { get; private set; }

		public string Token { get; private set; }

		private LoginParameters Config1 { get => (LoginParameters)Config; set => Config = value; }

		public Login(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{

		}

		public Login(NetworkStream stream, string nickname, string pass) : base(stream)
		{
			Config1 = new LoginParameters
			{
				Login = nickname,
				Password = pass
			};
		}

		public override void Execute()
		{
			//var q = new Query
			//{
			//	Config = Config1
			//};
			//var a = JsonConvert.SerializeObject(q);

			//var data = Encoding.UTF8.GetBytes(a);
			//Stream.Write(data, 0, data.Length);

			CreateSendQuery();

			Token = JsonConvert.DeserializeObject<string>(ReceiveResponse());

			//data = new byte[256];
			//var response = new StringBuilder();
			//do
			//{
			//	var bytes = Stream.Read(data, 0, data.Length);
			//	response.Append(Encoding.UTF8.GetString(data, 0, bytes));
			//} while (Stream.DataAvailable);

			//Result = JsonConvert.DeserializeObject<List<Message>>(response.ToString());
		}
	}
}
