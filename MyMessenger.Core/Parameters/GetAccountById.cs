using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MyMessenger.Core.Parameters
{
	[JsonObject]
	public class GetAccountByIdParameters : AbstractParameters
	{
		public override CommandType CommandName { get; set; } = CommandType.GetAccountById;
		
		[JsonProperty]
		public string Token { get; set; }
		
		[JsonProperty]
		public int AccountId { get; set; }
	}
}
