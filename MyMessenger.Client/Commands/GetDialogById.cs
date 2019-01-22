using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyMessenger.Core;
using Newtonsoft.Json;
using MyMessenger.Core.Parameters;
using MyMessenger.Core.Responses;

namespace MyMessenger.Client.Commands
{
	public class GetDialogById : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] {"getdialogbyid", "gdbyid"});

		public GetDialogByIdResponse Response { get; private set; }

		private GetDialogByIdParameters Config1
		{
			get => (GetDialogByIdParameters) Config;
			set => Config = value;
		}

		public GetDialogById(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{
		}

		public GetDialogById(NetworkStream stream, string token, int dialogid) : base(stream)
		{
			Config1 = new GetDialogByIdParameters
			{
				Token = token,
				DialogId = dialogid
			};
		}

		protected override void ExecuteImpl()
		{
			CreateSendQuery();
			
			Response = JsonConvert.DeserializeObject<GetDialogByIdResponse> (ReceiveResponse(), new InterfaceConverter<IDialog, Dialog>());
		}

		protected override Task ExecuteImplAsync()
		{
			throw new NotImplementedException();
		}
	}
}