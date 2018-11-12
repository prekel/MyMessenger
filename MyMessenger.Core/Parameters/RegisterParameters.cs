using System;
using System.Collections.Generic;
using System.Text;

namespace MyMessenger.Core.Parameters
{
	public class RegisterParameters : AbstractParameters
	{
		public string Nickname { get; set; }
		public string Password { get; set; }
	}
}
