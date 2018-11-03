using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MyMessenger.Server.Entities;

namespace MyMessenger.Server
{
	public class GetMessages : ICommand
	{
		private Query Config { get; set; }
		private MessengerContext Context { get; set; }

		public IEnumerable<Message> Result { get; private set; }

		public GetMessages(MessengerContext context, Query query)
		{
			Context = context;
			Config = query;
		}

		public void Execute()
		{
			Result = from i in Context.Messages where ((Dialog)i.Dialog).Id == Config.DialogId select i;
		}

		public class Query
		{
			public int DialogId { get; set; }
		}
	}
}