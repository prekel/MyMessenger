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
	public class GetAccountById : AbstractCommand
	{
		public static ICollection<string> CommandNames { get; } = new List<string>(new[] {"getaccountbyid", "gabyid"});

		public GetAccountByIdResponse Response { get; private set; }

		private GetAccountByIdParameters Config1
		{
			get => (GetAccountByIdParameters) Config;
			set => Config = value;
		}

		public GetAccountById(NetworkStream stream, AbstractParameters config) : base(stream, config)
		{
		}

		public GetAccountById(NetworkStream stream, string token, int accountid) : base(stream)
		{
			Config1 = new GetAccountByIdParameters
			{
				Token = token,
				AccountId = accountid
			};
		}

		protected override void ExecuteImpl()
		{
			CreateSendQuery();
			
			Response = JsonConvert.DeserializeObject<GetAccountByIdResponse> (ReceiveResponse(), new InterfaceConverter<IAccount, Account>());
		}

		protected override async Task ExecuteImplAsync()
		{
			await CreateSendQueryAsync();

			Response = JsonConvert.DeserializeObject<GetAccountByIdResponse>(await ReceiveResponseAsync(), new InterfaceConverter<IAccount, Account>());
		}
	}
}