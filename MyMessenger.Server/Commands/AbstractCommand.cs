using System;
using System.Collections.Generic;
using System.Text;

using MyMessenger.Core.Parameters;

namespace MyMessenger.Server.Commands
{
	public abstract class AbstractCommand : ICommand
	{
		protected AbstractParameters Config { get; set; }
		protected MessengerContext Context { get; set; }

		protected AbstractCommand(MessengerContext context, AbstractParameters config)
		{
			Context = context;
			Config = config;
		}

		public abstract void Execute();
	}
}
