using System.Net.Sockets;
using MyMessenger.Core.Parameters;

namespace MyMessenger.Client.Commands
{
	public class GetMessageLongPool : AbstractCommand
	{
		public GetMessageLongPool(NetworkStream stream) : base(stream)
		{
		}

		public GetMessageLongPool(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{
		}

		public override void Execute()
		{
			throw new System.NotImplementedException();
		}
	}
}