using System;
using System.Collections.Generic;
using System.Text;

namespace MyMessenger.Server.Commands
{
	public class Register : AbstractCommand
	{
		protected override AbstractParameters AbstractConfig { get; set; }
		private Parameters Config { get => (Parameters)AbstractConfig; set => AbstractConfig = value; }

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
