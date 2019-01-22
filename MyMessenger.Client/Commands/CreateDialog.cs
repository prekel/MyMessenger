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
	public class CreateDialog : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] {"createdialog", "cd"});

		public CreateDialogResponse Response { get; private set; }

		private CreateDialogParameters Config1
		{
			get => (CreateDialogParameters) Config;
			set => Config = value;
		}

		public CreateDialog(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{
		}

		public CreateDialog(NetworkStream stream, string token, IList<int> membersids) : base(stream)
		{
			Config1 = new CreateDialogParameters
			{
				MembersIds = membersids,
				Token = token
			};
		}

		public CreateDialog(NetworkStream stream, string token, IList<string> membersnicknames) : base(stream)
		{
			Config1 = new CreateDialogParameters
			{
				MembersNicknames = membersnicknames,
				Token = token
			};
		}

		protected override void ExecuteImpl()
		{
			CreateSendQuery();
			
			Response = JsonConvert.DeserializeObject<CreateDialogResponse> (ReceiveResponse());
		}

		protected override Task ExecuteImplAsync()
		{
			throw new NotImplementedException();
		}
	}
}