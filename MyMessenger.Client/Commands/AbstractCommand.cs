using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;
using Newtonsoft.Json;

namespace MyMessenger.Client.Commands
{
	public abstract class AbstractCommand : ICommand
	{
		protected AbstractParameters Config { get; set; }
		protected NetworkStream Stream { get; set; }

		//public static ICollection<string> CommandNames { get; }

		protected AbstractCommand(NetworkStream stream)
		{
			Stream = stream;
		}

		protected AbstractCommand(NetworkStream stream, AbstractParameters config) : this(stream)
		{
			Config = config;
		}
		
		public string RawResponse { get; private set; }

		protected void CreateSendQuery()
		{
			var q = new Query
			{
				Config = Config
			};
			var a = JsonConvert.SerializeObject(q, Formatting.Indented);

			var data = Encoding.UTF8.GetBytes(a);
			Stream.Write(data, 0, data.Length);
		}

		protected string ReceiveResponse()
		{
			var data = new byte[256];
			var response = new StringBuilder();
			do
			{
				var bytes = Stream.Read(data, 0, data.Length);
				response.Append(Encoding.UTF8.GetString(data, 0, bytes));
			} while (Stream.DataAvailable);

			RawResponse = response.ToString();
			return RawResponse;
		}

		public abstract void Execute();
	}
}