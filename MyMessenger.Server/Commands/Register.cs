using System;
using System.Collections.Generic;
using System.Text;

namespace MyMessenger.Server.Commands
{
	public class Register : AbstractCommand
	{
		private Parameters Config1 { get => (Parameters)Config; set => Config = value; }

		public Register(MessengerContext context, AbstractParameters config) : base(context, config)
		{
		}

		public override void Execute()
		{
			throw new NotImplementedException();
		}

		public class Parameters : AbstractParameters
		{
			public int Login { get; set; }
			public string Password { get; set; }
		}

	}
}
