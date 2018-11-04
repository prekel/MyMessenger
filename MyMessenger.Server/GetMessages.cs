using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
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
			Result = (from i in Context.Messages where i.Dialog1.Id == Config.DialogId select i)
				.Include(p => p.Author1);
		}

		public class Query
		{
			public int DialogId { get; set; }
		}
	}
}