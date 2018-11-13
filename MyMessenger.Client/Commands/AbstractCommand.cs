using System.Collections.Generic;
using System.Net.Sockets;
using MyMessenger.Core;
using MyMessenger.Core.Parameters;

namespace MyMessenger.Client.Commands
{
	public abstract class AbstractCommand : ICommand
	{
		protected AbstractParameters Config { get; set; }
		protected NetworkStream Stream { get; set; }

		public static ICollection<string> CommandNames { get; }

		protected AbstractCommand(NetworkStream stream)
		{
			Stream = stream;
		}

		public AbstractCommand(NetworkStream stream, AbstractParameters config) : this(stream)
		{
			Config = config;
		}

		public abstract void Execute();
	}
}