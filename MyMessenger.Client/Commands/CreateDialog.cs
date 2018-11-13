﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
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

		public CreateDialog(NetworkStream stream, string token, int secondmemberid) : base(stream)
		{
			Config1 = new CreateDialogParameters
			{
				SecondMemberId = secondmemberid,
				Token = token
			};
		}

		public CreateDialog(NetworkStream stream, string token, string secondmembername) : base(stream)
		{
			Config1 = new CreateDialogParameters
			{
				SecondMemberNickname = secondmembername,
				Token = token
			};
		}

		public override void Execute()
		{
			CreateSendQuery();
			
			Response = JsonConvert.DeserializeObject<CreateDialogResponse> (ReceiveResponse());
		}
	}
}