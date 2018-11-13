using System;
using System.Collections.Generic;
using System.Text;

using MyMessenger.Core.Parameters;
using MyMessenger.Core;
using MyMessenger.Core.Responses;

namespace MyMessenger.Server.Commands
{
	public abstract class AbstractCommand : ICommand
	{
		protected AbstractParameters Config { get; set; }
		protected MessengerContext Context { get; set; }

		protected IDictionary<string, IAccount> Tokens { get; set; }
		
		public AbstractResponse Response { get; set; }

		public ResponseCode Code
		{
			get => Response.Code;
			set => Response.Code = value;
		}

		protected AbstractCommand(MessengerContext context, AbstractParameters config)
		{
			Context = context;
			Config = config;
		}

		protected AbstractCommand(MessengerContext context, IDictionary<string, IAccount> tokens, AbstractParameters config) : this(context, config)
		{
			Tokens = tokens;
		}

		public abstract void Execute();
	}
}
