using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MyMessenger.Core.Parameters
{
	[JsonObject]
	public class RegisterParameters : AbstractParameters
	{
		public override CommandType CommandName { get; set; } = CommandType.Register;

		[JsonProperty]
		public string Nickname { get; set; }
		[JsonProperty]
		public string Password { get; set; }
	}
}
